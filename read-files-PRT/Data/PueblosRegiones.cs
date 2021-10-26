using System.IO;
using System.Linq;

namespace read_files_PRT.Data
{
    public class PueblosRegiones
    {
        /*Esta clase se utiliza para colocar el pueblo y región de cada cliente,
         de acuerdo al código del pueblo que viene incluido en los archivos originales.*/
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
