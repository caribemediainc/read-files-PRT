using System;
using System.Collections.Generic;
using System.Text;

namespace read_files_PRT.Data
{
    public class BlancasComercial
    {
        #region Generar archivo Blancas Comercial (BC)
        public void GenerateBC()
        {
            ControlGeneral generalControl = new ControlGeneral();
            PueblosRegiones pueblosRegiones = new PueblosRegiones();
            generalControl.ValidateFileExistence(ControlGeneral.rutaArchivoWriteBC, ControlGeneral.rutaArchivoReadBC);
            
            while (!ControlGeneral.streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = ControlGeneral.streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
                                    undefined2 = "", undefined3 = "", splitColumnText;

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
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
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
                pueblosRegiones.PuebloRegion(pueblo);
                ControlGeneral.streamWriter.Write($"{commercialName}|{undefined}|{phone}|{undefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|");
                foreach (string dir in info2)
                {
                    ControlGeneral.streamWriter.Write($"{dir}");
                }
                ControlGeneral.streamWriter.WriteLine();
            }
            ControlGeneral.streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo txt correctamente.");
            generalControl.RepeatMenu();
        }
        #endregion
    }
}
