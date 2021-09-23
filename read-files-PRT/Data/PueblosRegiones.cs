using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace read_files_PRT.Data
{
    public class PueblosRegiones
    {
        public static string Pueblo { get; set; }
        public static string Region { get; set; }

        public void PuebloRegion(string codPueblo)
        {
            var linePuebloRegion = File.ReadLines(ControlGeneral.rutaArchivoPueblosRegiones).ToList();
            foreach (var item in linePuebloRegion)
            {
                var splitLine = item.Split(';');
                if (codPueblo == splitLine[0])
                {
                    Pueblo = splitLine[1];
                    Region = splitLine[2];
                }
            }
        }
    }
}
