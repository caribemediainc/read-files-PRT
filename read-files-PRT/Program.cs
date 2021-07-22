using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace read_files_PRT
{
    class Program
    {
        public static StreamReader streamReader;
        public static StreamWriter streamWriter;
        static void Main(string[] args)
        {
            GenerateGB();
        }
        static void GeneratePB()
        {
            string rutaArchivoReadPB = Properties.Resources.rutaArchivoReadPB,
                    rutaArchivoWritePB = Properties.Resources.rutaArchivoWritePB,
                    fileName = Properties.Resources.fileName, fullRouteWrite = $"{rutaArchivoWritePB}{fileName}";

            if (!File.Exists(fullRouteWrite))
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

            while (!streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "",
                    undefined2 = "";

                commercialName += line.Substring(0, 42);
                undefined += line.Substring(42, 17);
                phoneOriginal += line.Substring(59, 10);
                phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                undefined2 += line.Substring(69, 56);
                undefined2 = undefined2.TrimStart();
                pueblo += undefined2.Substring(38, 3);
                undefinedCode += undefined2.Substring(41, 3);
                undefinedCode2 += undefined2.Substring(44, 3);
                undefined2 = undefined2.Trim();
                originalAddress += line.Substring(125);
                originalAddress = originalAddress.TrimStart();

                finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                undefined = undefined.TrimEnd();
                undefined2 = line.Substring(69, 42).TrimStart();

                if (!commercialName.StartsWith(' '))
                {
                    commercialName = commercialName.TrimEnd();
                    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|{finalAddress}");
                }
                else
                {
                    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|{finalAddress}");
                }
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo CSV correctamente.");
            Console.ReadKey();
        }

        static void GenerateGB()
        {
            string rutaArchivoReadPB = Properties.Resources.rutaArchivoReadPB,
                rutaArchivoWritePB = Properties.Resources.rutaArchivoWritePB,
                fileName = Properties.Resources.fileName, fullRouteWrite = $"{rutaArchivoWritePB}{fileName}";

            if (!File.Exists(fullRouteWrite))
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

            while (!streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = streamReader.ReadLine(), lineClean = Regex.Replace(line, @"  +", ""), commercialName = "", undefined = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "",
                    undefined2 = "", undefined3 = "", validateUppercase;

                commercialName += line.Substring(0, 42);
                undefined += line.Substring(42, 17);
                phoneOriginal += line.Substring(59, 10);
                phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                undefined2 += line.Substring(69, 1);
                undefined3 += line.Substring(70, 57).TrimStart();
                pueblo += undefined3.Substring(38, 3);
                undefined3 = line.Substring(70, 41).Trim().Substring(1);
                originalAddress += line.Substring(125);
                originalAddress = originalAddress.TrimStart().TrimStart('0');
                finalAddress += originalAddress;

                //validateUppercase = originalAddress.ToUpper();
                //if (originalAddress == validateUppercase)
                //{
                //    finalAddress += originalAddress;
                //}
                //else
                //{
                //    finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //}

                if (!commercialName.StartsWith(' '))
                {
                    commercialName = commercialName.TrimEnd();
                    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{undefined3}|{pueblo}|{finalAddress}");
                }
                else
                {
                    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{undefined3}|{pueblo}|{finalAddress}");
                }

                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo TXT correctamente.");
            Console.ReadKey();
           
        }
    }
}
