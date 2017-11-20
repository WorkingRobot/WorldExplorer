using SparseCollections;
using System.Linq;
using System.IO;
using System;

namespace WorldExplorer
{
    class World
    {
        public Sparse2DMatrix<int, int, RegionFile> regions = new Sparse2DMatrix<int, int, RegionFile>();
        public World(string folder)
        {
            foreach (string file in Directory.GetFiles(folder, "*.*.mca", SearchOption.TopDirectoryOnly))
            {
                string[] pos = file.Split('/').Last().Split('.').ToArray();
                regions[Convert.ToInt32(pos[1]), Convert.ToInt32(pos[2])] = new RegionFile(file);
                return;
            }
        }
    }
}
