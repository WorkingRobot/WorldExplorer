using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Wind(new World("C:/Users/Aleks.Aleks-PC/Desktop/generator/NBT/WORLD/region/"));
            //new World("C:/Users/Aleks.Aleks-PC/Desktop/generator/NBT/WORLD/region/");
            //RegionFile.Read("C:/Users/Aleks.Aleks-PC/Desktop/generator/NBT/WORLD/region/r.0.0.mca");
            //System.Threading.Thread.Sleep(50000);
        }
    }
}
