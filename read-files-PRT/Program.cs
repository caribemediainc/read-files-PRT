using System;
using System.IO;
using System.Text.RegularExpressions;

namespace read_files_PRT
{
    class Program
    {
        public static StreamReader streamReader;
        public static StreamWriter streamWriter;
        static void Main(string[] args)
        {
            string rutaArchivoReadPB = Properties.Resources.rutaArchivoReadPB,
                rutaArchivoWritePB = Properties.Resources.rutaArchivoWritePB,
                fileName = Properties.Resources.fileName, fullRouteWrite = $"{rutaArchivoWritePB}{fileName}";
            
            if(!File.Exists(fullRouteWrite))
            {
                streamReader = new StreamReader(rutaArchivoReadPB);
                streamWriter = new StreamWriter(fullRouteWrite);
            }
            else
            {
                File.Delete(fullRouteWrite);
                streamReader = new StreamReader(rutaArchivoReadPB);
                streamWriter = new StreamWriter(fullRouteWrite);
            }

            while(!streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = streamReader.ReadLine(), lineClean = Regex.Replace(line, @"  +", ";"), column = "", undefined = "", phone = "";
                var columnValue = lineClean.Split(';');
                //name += columnValue[0];
                //column2 += columnValue[1];
                //undefined += column2.Substring(0, 17);
                //phone += column2.Substring(17);
                //column3 += columnValue[2];
                //column4 += columnValue[3];

                //streamWriter.Write($"{name},{undefined},{phone},{column3},{column4}");
                //streamWriter.WriteLine();

                for (int i = 0; i < columnValue.Length; i++)
                {
                    if (columnValue[i].StartsWith("4") || columnValue[i].StartsWith("5"))
                    {
                        undefined += columnValue[i].Substring(0, 17);
                        phone += columnValue[i].Substring(17);
                        streamWriter.Write($"{undefined},{phone},");
                    }
                    else
                    {
                        column += columnValue[i];
                        streamWriter.Write($"{column},");
                        column = "";            
                    }
                }
                streamWriter.WriteLine();
                //for (int i = 0; i < columnValue.Length; i++)
                //{
                //    if (columnValue[i].StartsWith("4") || columnValue[i].StartsWith("5"))
                //    {
                //        phone += columnValue[i].Substring(17);
                //    }
                //    else
                //    {
                //        name += columnValue[i];
                //        streamWriter.Write($"{name},{phone}");
                //    }
                //}
                //for (int i = 0; i < line.Length; i++)
                //{
                //if (line[i]!="")
                //{
                //    column1 += line[i] + " ";
                //}


                //}
                //var clean = line.Replace(" ",";");
                //var separate = line.Split(' ');

            }
            streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo CSV correctamente.");
            Console.ReadKey();
        }
    }
}
