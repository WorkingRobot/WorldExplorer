using fNbt;
using SparseCollections;
using System;

namespace WorldExplorer
{
    class Column
    {
        public NbtCompound compoundTag { get; private set; }
        public int xPos { get; private set; }
        public int zPos { get; private set; }
        public Sparse3DMatrix<byte, byte, byte, Block> blocks = new Sparse3DMatrix<byte, byte, byte, Block>();
        public Column(NbtCompound tag)
        {
            compoundTag = tag;
            xPos = tag.Get<NbtInt>("xPos").Value;
            zPos = tag.Get<NbtInt>("zPos").Value;
            NbtList sections = tag.Get<NbtList>("Sections");
            foreach (NbtCompound section in sections)
            {
                int sectionOffset = section.Get<NbtByte>("Y").Value*16;
                byte[] blox = section.Get<NbtByteArray>("Blocks").Value;
                byte[] _data = section.Get<NbtByteArray>("Data").Value;
                byte[] data = new byte[4096]; //each value is a nibble. split em up.
                int i = 0;
                foreach(byte value in _data)
                {
                    data[i] = (byte)(value & 0x0F);
                    data[i + 1] = (byte)(value & 0xF0);
                    i += 2;
                }
                i = 0;
                for (byte y = 0; y < 16; ++y)
                {
                    for (byte z = 0; z < 16; ++z)
                    {
                        for (byte x = 0; x < 16; ++x)
                        {
                            if (blox[i] != 0)
                            {
                                blocks[x, (byte)(y + sectionOffset), z] = new Block(x, y, z, blox[i], data[i]);
                                //Console.WriteLine(x + ", " + y + ", " + z + ": " + blox[i]);
                            }
                            ++i;
                        }
                    }
                }
            }
            //Console.WriteLine(tag.ToString("    "));
        }
    }
}
