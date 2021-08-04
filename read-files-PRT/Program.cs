using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace read_files_PRT
{
    class Program
    {
        public static StreamReader streamReader;
        public static StreamWriter streamWriter;
        public static string rutaArchivoReadPB = Properties.Resources.rutaArchivoReadPB,
             rutaArchivoWritePB = Properties.Resources.rutaArchivoWritePB;
        public static int menuControl = 1;
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Menu();
            
        }
        static void Menu()
        {
            int optionSel;
            do
            {
                Console.WriteLine("*******************************LECTURA ARCHIVOS PRT*******************************\n");
                Console.WriteLine("¿Cuál archivo desea obtener formateado?\n");
                Console.WriteLine(" 1. Blancas Comercial (BC)\n");
                Console.WriteLine(" 2. Blancas Comercial por Pueblo (PB)\n");
                Console.WriteLine(" 3. Gobierno (GB)\n");
                Console.Write("Ingrese el número de la opción deseada: ");
                while (!int.TryParse(Console.ReadLine(), out optionSel))
                {
                    Console.Write(" Sólo se permiten números. Intente nuevamente: ");
                }

                switch (optionSel)
                {
                    case 1:
                        GenerateBC();
                        break;
                    case 2:
                        GeneratePB();
                        break;
                    case 3:
                        GenerateGB();
                        break;
                }
            } while (menuControl == 1);

        }
        static void GeneratePB()
        {
            if (!File.Exists(rutaArchivoWritePB))
            {
                streamReader = new StreamReader(rutaArchivoReadPB);
                streamWriter = new StreamWriter(rutaArchivoWritePB);
            }
            else
            {
                File.Delete(rutaArchivoWritePB);
                streamReader = new StreamReader(rutaArchivoReadPB);
                streamWriter = new StreamWriter(rutaArchivoWritePB);
            }

            while (!streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");

                string line = streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
                    undefined2 = "", splitColumnText;
                int totalChar = 33;

                commercialName = line.Substring(0, 42);
                undefined = line.Substring(42, 17);
                phoneOriginal = line.Substring(59, 10);
                phone = phoneOriginal.Insert(3, " ").Insert(7, "-");
                undefined2 = line.Substring(69, 56);
                undefined2 = undefined2.TrimStart();
                pueblo = undefined2.Substring(38, 3);
                undefinedCode = undefined2.Substring(41, 3);
                undefinedCode2 = undefined2.Substring(44, 3);
                undefined2.Trim();

                if (line.Length > 129) { finalAddress = line.Substring(129); }
                else { finalAddress = ""; }
                undefined = undefined.TrimEnd();
                StringBuilder sbUndefined = new StringBuilder(undefined);
                sbUndefined.Insert(undefined.Length, "*");
                undefined2 = line.Substring(69, 42).TrimStart().Substring(2);
                StringBuilder sbUndefined2 = new StringBuilder(undefined2);
                sbUndefined2.Insert(undefined2.Length, "*");

                List<string> info = new List<string>();
                List<string> info2 = new List<string>();

                commercialName = commercialName.TrimEnd();
                for (int i = 0; i < 12; i++)
                {
                    splitColumnText = undefined2.Substring(totalChar, 3);
                    info.Add(splitColumnText);
                    totalChar = totalChar - 3;
                }
                info.Reverse();
                newAddress = finalAddress;

                foreach (string item in info)
                {
                    try
                    {
                        int espacios = Convert.ToInt32(item);
                        if (espacios > 0)
                        {
                            newAddress = newAddress.Substring(espacios);
                            finalAddress = finalAddress.Substring(0, espacios);

                            info2.Add(finalAddress + "|");
                            finalAddress = newAddress;
                        }
                        else
                        {
                            info2.Add("|");
                        }
                    }
                    catch
                    {

                    }
                    
                }
                pueblo = pueblo.Replace(';', 'Ñ');
                commercialName = commercialName.Replace(';', 'ñ');
                streamWriter.Write($"{commercialName}|{sbUndefined}|{phone}|{sbUndefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|");
                foreach (string dir in info2)
                {
                    streamWriter.Write($"{dir}");
                }
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo txt correctamente.");
            RepeatMenu();
        }
        static void RepeatMenu()
        {
            Console.Write(" ¿Desea obtener otro archivo? (S/N): ");
            string answer = Console.ReadLine();
            while (!Regex.IsMatch(answer, @"[a-zA-Z]"))
            {
                Console.Write(" No se permiten números. Intente nuevamente: ");
                answer = Console.ReadLine();
            }
            if (answer == "s" || answer == "S")
            {
                Console.Clear();
                Menu();
            }
            else if (answer == "n" || answer == "N")
            {
                menuControl = 2;
                Console.Clear();
            }
            else
            {
                Console.Write(" Sólo se permite Sí o No: S, N, s, n. Volviendo al menú principal...");
                Console.Clear();
                Menu();
            }
        }
        static void GenerateGB()
        {
            string rutaArchivoReadGB = Properties.Resources.rutaArchivoReadGB,
                rutaArchivoWriteGB = Properties.Resources.rutaArchivoWriteGB;

            if (!File.Exists(rutaArchivoWriteGB))
            {
                streamReader = new StreamReader(rutaArchivoReadGB);
                streamWriter = new StreamWriter(rutaArchivoWriteGB);
            }
            else
            {
                File.Delete(rutaArchivoWriteGB);
                streamReader = new StreamReader(rutaArchivoReadGB);
                streamWriter = new StreamWriter(rutaArchivoWriteGB);
            }

            while (!streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
                                    undefined2 = "", undefined3 = "", splitColumnText, columnText, columnText2, columnText3, columnText4, columnText5, columnText6, columnText7, columnText8, columnText9, columnText10, columnText11, columnText12;

                commercialName = line.Substring(0, 42);
                undefined = line.Substring(42, 17);
                phoneOriginal = line.Substring(59, 10);
                phone = phoneOriginal.Insert(3, " ").Insert(7, "-");
                undefined2 = line.Substring(69, 1);
                undefined3 = line.Substring(70, 57).TrimStart();
                pueblo = undefined3.Substring(38, 3);
                undefined3 = line.Substring(70, 41).Trim().Substring(2);
                if (line.Length > 129)
                {
                    finalAddress = line.Substring(129);
                }
                else { finalAddress = ""; }
                /* if (finalAddress.ToLower().Equals("ext"))
                 {
                     phone = "*" + phoneOriginal.Substring(6);
                 }
                 else if (finalAddress.ToLower().EndsWith(" ext") && (int.Parse(phoneOriginal.Substring(0, 1)) > 0 || phoneOriginal.Contains("000000")))
                 {
                     phone = "*" + phoneOriginal.Substring(6);

                 }
                 else if (finalAddress.ToLower().Contains("ext") && (int.Parse(phoneOriginal.Substring(0, 1)) > 0 || phoneOriginal.Contains("000000")))
                 {
                     phone = "*" + phoneOriginal.Substring(6);

                 }
                 else
                 {
                     phone = phoneOriginal.Insert(3, " ").Insert(7, "-");
                 }*/
                int totalChar = 33;
                List<string> info = new List<string>();
                List<string> info2 = new List<string>();
                int espacios = 0;

                commercialName = commercialName.TrimEnd();
                for (int i = 0; i < 12; i++)
                {
                    splitColumnText = undefined3.Substring(totalChar, 3);
                    info.Add(splitColumnText);
                    totalChar = totalChar - 3;
                }
                info.Reverse();
                newAddress = finalAddress;

                foreach (string item in info)
                {
                    try
                    {
                        espacios = Convert.ToInt32(item);
                        if (espacios > 0)
                        {
                            newAddress = newAddress.Substring(espacios);
                            finalAddress = finalAddress.Substring(0, espacios);

                            info2.Add(finalAddress + "|");
                            finalAddress = newAddress;
                        }
                        else
                        {
                            info2.Add("|");
                        }
                    }
                    catch (Exception ioe)
                    {
                        Console.WriteLine("Error: " + ioe.Message);
                        espacios = newAddress.Length;
                        if (espacios > 0)
                        {
                            newAddress = newAddress.Substring(espacios);
                            finalAddress = finalAddress.Substring(0, espacios);

                            info2.Add(finalAddress + "|");
                            finalAddress = newAddress;
                        }
                        else
                        {
                            info2.Add("|");
                        }


                    }
                }
                streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|");
                foreach (string dir in info2)
                {
                    streamWriter.Write($"{dir}");
                }
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo CSV correctamente.");
            Console.ReadKey();
        }

        static void GenerateBC()
        {
            string rutaArchivoReadBC = Properties.Resources.rutaArchivoReadBC,
                rutaArchivoWriteBC = Properties.Resources.rutaArchivoWriteBC;

            if (!File.Exists(rutaArchivoWriteBC))
            {
                streamReader = new StreamReader(rutaArchivoReadBC);
                streamWriter = new StreamWriter(rutaArchivoWriteBC);
            }
            else
            {
                File.Delete(rutaArchivoWriteBC);
                streamReader = new StreamReader(rutaArchivoReadBC);
                streamWriter = new StreamWriter(rutaArchivoWriteBC);
            }

            while (!streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
                                    undefined2 = "", undefined3 = "", splitColumnText, columnText, columnText2, columnText3, columnText4, columnText5, columnText6, columnText7, columnText8, columnText9, columnText10, columnText11, columnText12;

                commercialName = line.Substring(0, 42);
                undefined = line.Substring(42, 17);
                phoneOriginal = line.Substring(59, 10);
                phone = phoneOriginal.Insert(3, " ").Insert(7, "-");
                undefined2 = line.Substring(69, 1);
                undefined3 = line.Substring(70, 57).TrimStart();
                pueblo = undefined3.Substring(38, 3);
                undefined3 = line.Substring(70, 41).Trim().Substring(2);
                if (line.Length > 129)
                {
                    finalAddress = line.Substring(129);
                }
                else { finalAddress = ""; }
                /*if (finalAddress.ToLower().Equals("ext"))
                {
                    phone = "*" + phoneOriginal.Substring(6);
                }
                else if (finalAddress.ToLower().EndsWith(" ext") && (int.Parse(phoneOriginal.Substring(0, 1)) > 0 || phoneOriginal.Contains("000000")))
                {
                    phone = "*" + phoneOriginal.Substring(6);

                }
                else if (finalAddress.ToLower().Contains("ext") && (int.Parse(phoneOriginal.Substring(0, 1)) > 0 || phoneOriginal.Contains("000000")))
                {
                    phone = "*" + phoneOriginal.Substring(6);

                }
                else
                {
                    phone = phoneOriginal.Insert(3, " ").Insert(7, "-");
                }*/
                int totalChar = 33;
                List<string> info = new List<string>();
                List<string> info2 = new List<string>();
                int espacios = 0;

                commercialName = commercialName.TrimEnd();
                for (int i = 0; i < 12; i++)
                {
                    splitColumnText = undefined3.Substring(totalChar, 3);
                    info.Add(splitColumnText);
                    totalChar = totalChar - 3;
                }
                info.Reverse();
                newAddress = finalAddress;

                foreach (string item in info)
                {
                    try
                    {
                        espacios = Convert.ToInt32(item);
                        if (espacios > 0)
                        {
                            newAddress = newAddress.Substring(espacios);
                            finalAddress = finalAddress.Substring(0, espacios);

                            info2.Add(finalAddress + "|");
                            finalAddress = newAddress;
                        }
                        else
                        {
                            info2.Add("|");
                        }
                    }
                    catch (Exception ioe)
                    {
                        Console.WriteLine("Error: " + ioe.Message);
                        espacios = newAddress.Length;
                        if (espacios > 0)
                        {
                            newAddress = newAddress.Substring(espacios);
                            finalAddress = finalAddress.Substring(0, espacios);

                            info2.Add(finalAddress + "|");
                            finalAddress = newAddress;
                        }
                        else
                        {
                            info2.Add("|");
                        }


                    }
                }
                streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|");
                foreach (string dir in info2)
                {
                    streamWriter.Write($"{dir}");
                }
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo CSV correctamente.");
            Console.ReadKey();
        }
    }
}