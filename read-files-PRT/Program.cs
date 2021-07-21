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
            GeneratePB();
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
                string line = streamReader.ReadLine(), lineClean = Regex.Replace(line, @"  +", ""), commercialName = "", column = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "",
                    undefined2 = "", undefined3 = "";
                var columnValue = lineClean.Split('|');

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

                //if (undefined.EndsWith('R') || undefined.EndsWith('S'))
                //{
                //    undefined2 += line.Substring(69, 4);
                //    phoneOriginal += undefined.Substring(17, 10);
                //    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                //    undefined = undefined.Substring(0, 16);

                //    undefined2 += line.Substring(70, 56);
                //    undefined2 = undefined2.TrimStart();
                //    pueblo += undefined2.Substring(38, 3);
                //    undefinedCode += undefined2.Substring(41, 3);
                //    undefinedCode2 += undefined2.Substring(44, 3);
                //    undefined2 = line.Substring(69, 42);
                //    undefined2 = undefined2.Trim();

                //    originalAddress += line.Substring(125);
                //    originalAddress = originalAddress.TrimStart();

                //    finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //    undefined = undefined.TrimEnd();
                //    undefined2 = undefined2.TrimStart();

                //}
                //else
                //{
                //    undefined2 += line.Substring(69, 5);
                //    phoneOriginal += undefined.Substring(17);
                //    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                //    undefined = undefined.Substring(0, 17);

                //    undefined2 += line.Substring(69, 56);
                //    undefined2 = undefined2.TrimStart();
                //    pueblo += undefined2.Substring(38, 3);
                //    undefinedCode += undefined2.Substring(41, 3);
                //    undefinedCode2 += undefined2.Substring(44, 3);
                //    undefined2 = line.Substring(69, 42);
                //    undefined2 = undefined2.Trim();

                //    originalAddress += line.Substring(125);
                //    originalAddress = originalAddress.TrimStart();

                //    finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //    undefined = undefined.TrimEnd();
                //    undefined2 = undefined2.TrimStart();
                //}

                //undefined2 += line.Substring(69, 56);
                //undefined2 = undefined2.TrimStart();
                //pueblo += undefined2.Substring(38, 3);
                //undefinedCode += undefined2.Substring(41, 3);
                //undefinedCode2 += undefined2.Substring(44, 3);
                //undefined2 = line.Substring(69, 42);
                //undefined2 = undefined2.Trim();

                //originalAddress += line.Substring(125);
                //originalAddress = originalAddress.TrimStart();

                //finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //undefined = undefined.TrimEnd();
                //undefined2 = undefined2.TrimStart();

                //if (!commercialName.StartsWith(' '))
                //{
                //    commercialName = commercialName.TrimEnd();
                //    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|{finalAddress}");
                //}
                //else
                //{
                //    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|{finalAddress}");
                //}

                //if (!lineClean.Substring(0, 31).StartsWith('4'))
                //{
                //    commercialName += lineClean.Substring(0, 31);
                //    undefined += lineClean.Substring(31, 17);
                //    phoneOriginal += lineClean.Substring(48, 10);
                //    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                //    undefined2 += lineClean.Substring(59, 40);

                //    if(undefined2.Length == 40 && lineClean.Substring(99, 3).StartsWith('0'))
                //    {
                //        column += undefined2.Insert(40, "      ");
                //        undefined2 = column;
                //        pueblo += undefined2.Substring(40, 3);
                //        undefinedCode += undefined2.Substring(43, 3);
                //        originalAddress += lineClean.Substring(99);
                //        finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //        undefined2 = undefined2.TrimEnd();
                //    }
                //    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{finalAddress}");
                //}
                //else
                //{
                //    commercialName += " ";
                //    undefined += lineClean.Substring(0, 17);
                //    phoneOriginal += lineClean.Substring(17, 10);
                //    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                //    undefined2 += lineClean.Substring(27, 41);
                //    if(undefined2.Length == 41 && lineClean.Substring(68, 1).StartsWith('0'))
                //    {
                //        column += undefined2.Insert(41, "      ");
                //        undefined2 = column;
                //        pueblo += undefined2.Substring(41, 3);
                //        undefinedCode += undefined2.Substring(44,3);
                //        originalAddress += lineClean.Substring(68);
                //        finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //        undefined2 = undefined2.TrimEnd();
                //    }
                //    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{finalAddress}");
                //}

                //if (undefined2.Length == 40 && lineClean.Substring(99, 3).StartsWith('0'))
                //{
                //    column += undefined2.Insert(40, "      ");
                //    undefined2 = column;
                //    streamWriter.Write($"{undefined2}|");
                //}
                //else
                //{
                //    pueblo += lineClean.Substring(99, 3);
                //    undefinedCode += lineClean.Substring(102, 3);
                //    streamWriter.Write($"{pueblo}|{undefinedCode}");
                //}
                //var phoneTest = phoneFinal.Insert(7,"-");
                //for (int i = 0; i < line.Length; i++)
                //{
                //    column += line[i];
                //    if(column.Length == 42)
                //    {
                //        string commercialName = column;
                //        streamWriter.Write($"{commercialName},");
                //        commercialName = "";
                //    }
                //}

                //for (int i = 0; i < columnValue.Length; i++)
                //{
                //    if (columnValue[i].StartsWith("4") || columnValue[i].StartsWith("5"))
                //    {
                //        undefined += columnValue[i].Substring(0, 17);
                //        phone += columnValue[i].Substring(17);
                //        streamWriter.Write($"{undefined},{phone},");
                //    }
                //    else if (columnValue[i].Length < 47 && columnValue[i].Length > 40)
                //    {
                //        //i++;
                //        if (columnValue[i].Length == 3)
                //        {
                //            //i--;
                //            column += columnValue[i].Insert(41, "      ");
                //            pueblo += column.Substring(41, 3);
                //            undefinedCode += column.Substring(44, 3);
                //            streamWriter.Write($"{column},{pueblo},{undefinedCode},");
                //            column = "";
                //        }
                //        else
                //        {
                //            column += columnValue[i];
                //            streamWriter.Write($"{column},");
                //            column = "";
                //        }
                //        //else
                //        //{
                //        //    i--;
                //        //    column += columnValue[i].Insert(41, "   ");
                //        //    pueblo += column.Substring(41, 3);
                //        //    undefinedCode += column.Substring(45, 3);
                //        //    streamWriter.Write($"{column},{pueblo},{undefinedCode},");
                //        //    column = "";
                //        //}
                //    }
                //else if (columnValue[i].Length == 47)
                //{
                //    column += columnValue[i].Substring(0, 41);
                //    pueblo += columnValue[i].Substring(41, 3);
                //    undefinedCode += columnValue[i].Substring(44, 3);
                //    streamWriter.Write($"{column},{pueblo},{undefinedCode},");
                //    column = "";
                //}
                //else
                //{
                //    if(columnValue[i].StartsWith('0') && (columnValue[i].Length > 80 || columnValue[i].Length <= 60))
                //    {
                //        column += Regex.Replace(columnValue[i], @"([A-Z])", "|$1");
                //        streamWriter.Write($"{column}");
                //        column = "";
                //    }
                //    else
                //    {
                //        column += columnValue[i];
                //        streamWriter.Write($"{column},");
                //        column = "";
                //    }
                //}
                //    //else if(columnValue[i].Length == 7 || columnValue[i].Length == 4)
                //    //{
                //    //    pueblo += columnValue[i].Substring(0, 3);
                //    //    streamWriter.Write($"{pueblo},");
                //    //}
                //    //else if (columnValue[i].Length >= 41)
                //    //{
                //    //    if(columnValue[i].Length > 41)
                //    //    {
                //    //        if(columnValue[i].Length <= 48)
                //    //        {
                //    //            column += columnValue[i].Substring(0, 39);
                //    //            pueblo += columnValue[i].Substring(39, 3);
                //    //            streamWriter.Write($"{column},{pueblo},");
                //    //            column = "";
                //    //            pueblo = "";
                //    //        }
                //    //        else
                //    //        {
                //    //            column += columnValue[i].Substring(0, 38);
                //    //            pueblo += columnValue[i].Substring(38, 3);
                //    //            originalAddress += columnValue[i].Substring(56);
                //    //            finalAddress += Regex.Replace(originalAddress, @"([a-z])([A-Z])", "$1 $2");
                //    //            streamWriter.Write($"{column},{pueblo},{finalAddress}");
                //    //            column = "";
                //    //            pueblo = "";
                //    //        }
                //    //    }
                //    //}
                //    //else if(columnValue[i].StartsWith("0"))
                //    //{
                //    //    finalAddress += columnValue[i].Replace("0", "");
                //    //    streamWriter.Write($"{finalAddress}");
                //    //}
                //    //else
                //    //{
                //    //    column += columnValue[i];
                //    //    streamWriter.Write($"{column},");
                //    //    column = "";
                //    //}
                //}
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
                string line = streamReader.ReadLine(), lineClean = Regex.Replace(line, @"  +", ""), commercialName = "", column = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "",
                    undefined2 = "";
                var columnValue = lineClean.Split('|');

                commercialName += line.Substring(0, 42);
                undefined += line.Substring(42, 17);
                phoneOriginal += line.Substring(59, 10);
                phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                undefined2 += line.Substring(69, 4);
                undefined2 += line.Substring(70, 56);
                undefined2 = undefined2.TrimStart();
                pueblo += undefined2.Substring(38, 3);
                undefinedCode += undefined2.Substring(41, 3);
                undefinedCode2 += undefined2.Substring(44, 3);



                if (undefined.EndsWith('R') || undefined.EndsWith('S'))
                {
                    undefined2 += line.Substring(69, 4);
                    phoneOriginal += undefined.Substring(17, 10);
                    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                    undefined = undefined.Substring(0, 16);

                    undefined2 += line.Substring(70, 56);
                    undefined2 = undefined2.TrimStart();
                    pueblo += undefined2.Substring(38, 3);
                    undefinedCode += undefined2.Substring(41, 3);
                    undefinedCode2 += undefined2.Substring(44, 3);
                    undefined2 = line.Substring(69, 42);
                    undefined2 = undefined2.Trim();

                    originalAddress += line.Substring(125);
                    originalAddress = originalAddress.TrimStart();

                    finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                    undefined = undefined.TrimEnd();
                    undefined2 = undefined2.TrimStart();

                }
                else
                {
                    undefined2 += line.Substring(69, 5);
                    phoneOriginal += undefined.Substring(17);
                    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                    undefined = undefined.Substring(0, 17);

                    undefined2 += line.Substring(69, 56);
                    undefined2 = undefined2.TrimStart();
                    pueblo += undefined2.Substring(38, 3);
                    undefinedCode += undefined2.Substring(41, 3);
                    undefinedCode2 += undefined2.Substring(44, 3);
                    undefined2 = line.Substring(69, 42);
                    undefined2 = undefined2.Trim();

                    originalAddress += line.Substring(125);
                    originalAddress = originalAddress.TrimStart();

                    finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                    undefined = undefined.TrimEnd();
                    undefined2 = undefined2.TrimStart();
                }

                undefined2 += line.Substring(69, 56);
                undefined2 = undefined2.TrimStart();
                pueblo += undefined2.Substring(38, 3);
                undefinedCode += undefined2.Substring(41, 3);
                undefinedCode2 += undefined2.Substring(44, 3);
                undefined2 = line.Substring(69, 42);
                undefined2 = undefined2.Trim();

                originalAddress += line.Substring(125);
                originalAddress = originalAddress.TrimStart();

                finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                undefined = undefined.TrimEnd();
                undefined2 = undefined2.TrimStart();

                if (!commercialName.StartsWith(' '))
                {
                    commercialName = commercialName.TrimEnd();
                    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|{finalAddress}");
                }
                else
                {
                    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|{finalAddress}");
                }

                //if (!lineClean.Substring(0, 31).StartsWith('4'))
                //{
                //    commercialName += lineClean.Substring(0, 31);
                //    undefined += lineClean.Substring(31, 17);
                //    phoneOriginal += lineClean.Substring(48, 10);
                //    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                //    undefined2 += lineClean.Substring(59, 40);

                //    if(undefined2.Length == 40 && lineClean.Substring(99, 3).StartsWith('0'))
                //    {
                //        column += undefined2.Insert(40, "      ");
                //        undefined2 = column;
                //        pueblo += undefined2.Substring(40, 3);
                //        undefinedCode += undefined2.Substring(43, 3);
                //        originalAddress += lineClean.Substring(99);
                //        finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //        undefined2 = undefined2.TrimEnd();
                //    }
                //    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{finalAddress}");
                //}
                //else
                //{
                //    commercialName += " ";
                //    undefined += lineClean.Substring(0, 17);
                //    phoneOriginal += lineClean.Substring(17, 10);
                //    phone += phoneOriginal.Insert(3, " ").Insert(7, "-");
                //    undefined2 += lineClean.Substring(27, 41);
                //    if(undefined2.Length == 41 && lineClean.Substring(68, 1).StartsWith('0'))
                //    {
                //        column += undefined2.Insert(41, "      ");
                //        undefined2 = column;
                //        pueblo += undefined2.Substring(41, 3);
                //        undefinedCode += undefined2.Substring(44,3);
                //        originalAddress += lineClean.Substring(68);
                //        finalAddress += Regex.Replace(originalAddress, @"([A-Z])", "|$1");
                //        undefined2 = undefined2.TrimEnd();
                //    }
                //    streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{finalAddress}");
                //}

                //if (undefined2.Length == 40 && lineClean.Substring(99, 3).StartsWith('0'))
                //{
                //    column += undefined2.Insert(40, "      ");
                //    undefined2 = column;
                //    streamWriter.Write($"{undefined2}|");
                //}
                //else
                //{
                //    pueblo += lineClean.Substring(99, 3);
                //    undefinedCode += lineClean.Substring(102, 3);
                //    streamWriter.Write($"{pueblo}|{undefinedCode}");
                //}
                //var phoneTest = phoneFinal.Insert(7,"-");
                //for (int i = 0; i < line.Length; i++)
                //{
                //    column += line[i];
                //    if(column.Length == 42)
                //    {
                //        string commercialName = column;
                //        streamWriter.Write($"{commercialName},");
                //        commercialName = "";
                //    }
                //}

                //for (int i = 0; i < columnValue.Length; i++)
                //{
                //    if (columnValue[i].StartsWith("4") || columnValue[i].StartsWith("5"))
                //    {
                //        undefined += columnValue[i].Substring(0, 17);
                //        phone += columnValue[i].Substring(17);
                //        streamWriter.Write($"{undefined},{phone},");
                //    }
                //    else if (columnValue[i].Length < 47 && columnValue[i].Length > 40)
                //    {
                //        //i++;
                //        if (columnValue[i].Length == 3)
                //        {
                //            //i--;
                //            column += columnValue[i].Insert(41, "      ");
                //            pueblo += column.Substring(41, 3);
                //            undefinedCode += column.Substring(44, 3);
                //            streamWriter.Write($"{column},{pueblo},{undefinedCode},");
                //            column = "";
                //        }
                //        else
                //        {
                //            column += columnValue[i];
                //            streamWriter.Write($"{column},");
                //            column = "";
                //        }
                //        //else
                //        //{
                //        //    i--;
                //        //    column += columnValue[i].Insert(41, "   ");
                //        //    pueblo += column.Substring(41, 3);
                //        //    undefinedCode += column.Substring(45, 3);
                //        //    streamWriter.Write($"{column},{pueblo},{undefinedCode},");
                //        //    column = "";
                //        //}
                //    }
                //else if (columnValue[i].Length == 47)
                //{
                //    column += columnValue[i].Substring(0, 41);
                //    pueblo += columnValue[i].Substring(41, 3);
                //    undefinedCode += columnValue[i].Substring(44, 3);
                //    streamWriter.Write($"{column},{pueblo},{undefinedCode},");
                //    column = "";
                //}
                //else
                //{
                //    if(columnValue[i].StartsWith('0') && (columnValue[i].Length > 80 || columnValue[i].Length <= 60))
                //    {
                //        column += Regex.Replace(columnValue[i], @"([A-Z])", "|$1");
                //        streamWriter.Write($"{column}");
                //        column = "";
                //    }
                //    else
                //    {
                //        column += columnValue[i];
                //        streamWriter.Write($"{column},");
                //        column = "";
                //    }
                //}
                //    //else if(columnValue[i].Length == 7 || columnValue[i].Length == 4)
                //    //{
                //    //    pueblo += columnValue[i].Substring(0, 3);
                //    //    streamWriter.Write($"{pueblo},");
                //    //}
                //    //else if (columnValue[i].Length >= 41)
                //    //{
                //    //    if(columnValue[i].Length > 41)
                //    //    {
                //    //        if(columnValue[i].Length <= 48)
                //    //        {
                //    //            column += columnValue[i].Substring(0, 39);
                //    //            pueblo += columnValue[i].Substring(39, 3);
                //    //            streamWriter.Write($"{column},{pueblo},");
                //    //            column = "";
                //    //            pueblo = "";
                //    //        }
                //    //        else
                //    //        {
                //    //            column += columnValue[i].Substring(0, 38);
                //    //            pueblo += columnValue[i].Substring(38, 3);
                //    //            originalAddress += columnValue[i].Substring(56);
                //    //            finalAddress += Regex.Replace(originalAddress, @"([a-z])([A-Z])", "$1 $2");
                //    //            streamWriter.Write($"{column},{pueblo},{finalAddress}");
                //    //            column = "";
                //    //            pueblo = "";
                //    //        }
                //    //    }
                //    //}
                //    //else if(columnValue[i].StartsWith("0"))
                //    //{
                //    //    finalAddress += columnValue[i].Replace("0", "");
                //    //    streamWriter.Write($"{finalAddress}");
                //    //}
                //    //else
                //    //{
                //    //    column += columnValue[i];
                //    //    streamWriter.Write($"{column},");
                //    //    column = "";
                //    //}
                //}
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo CSV correctamente.");
            Console.ReadKey();
        }
    }
}
