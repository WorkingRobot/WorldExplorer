using System.Collections.Generic;
using fNbt;
using System.IO;
using SparseCollections;
using System.IO.Compression;

namespace WorldExplorer
{
    class RegionFile
    {
        public Sparse2DMatrix<int, int, Column> columns = new Sparse2DMatrix<int, int, Column>();

        public static RegionFile Read(string filename)
        {
            List<Column> columns = new List<Column>();
            FileStream stream = File.OpenRead(filename);

            byte[] mcaHeader = new byte[8192];
            stream.Read(mcaHeader, 0, 8192);
            stream.Seek(0, SeekOrigin.Begin);
            List<byte[]> chunkLocs = new List<byte[]>();
            for (int i = 0; i < 4096; i += 4)
            {
                byte[] inp = new byte[4];
                stream.Read(inp, 0, 4);
                chunkLocs.Add(inp);
            }

            foreach (byte[] bytes in chunkLocs)
            {
                if (((bytes[2] & 0xFF) | ((bytes[1] & 0xFF) << 8) | ((bytes[0] & 0x0F) << 16)) != 0) // Don't operate on ones which have an offset of 0. These have yet to be generated.
                {
                    int chunkPosition = ((bytes[2] & 0xFF) | ((bytes[1] & 0xFF) << 8) | ((bytes[0] & 0x0F) << 16)) * 4096; // Get the position of the chunk in bytes in the file.

                    int sectorLength = bytes[3]; // How many sectors this chunk takes up. 1 sector = 4096 bytes
                    byte[] chunkPayload = new byte[(4096 * sectorLength) - 5]; // The contents of the chunk. Zlib compressed
                    stream.Seek(chunkPosition + 5, SeekOrigin.Begin);
                    stream.Read(chunkPayload, 0, (4096 * sectorLength) - 5);

                    MemoryStream memstr = new MemoryStream(chunkPayload);
                    memstr.ReadByte();
                    memstr.ReadByte();
                    NbtReader reader = new NbtReader(new DeflateStream(memstr,CompressionMode.Decompress));
                    columns.Add(new Column(((NbtCompound)reader.ReadAsTag()).Get<NbtCompound>("Level")));
                    //return new RegionFile(columns);
                }
                        
            }
            return new RegionFile(columns);
        }
        
        public RegionFile(List<Column> columns)
        {
            foreach (Column column in columns)
            {
                this.columns[column.xPos, column.zPos] = column;
            }
        }

        public RegionFile(string filename)
        {
            columns = RegionFile.Read(filename).columns;
        }
    }
}
