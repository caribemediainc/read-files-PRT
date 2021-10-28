using System;
using System.Collections.Generic;
using System.Linq;
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

            List<string> codeRepeatCommercial = new List<string>();
            List<string> commercialWrited = new List<string>();
            int id = 0;

            while (!ControlGeneral.streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = ControlGeneral.streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
                                    undefined2 = "", undefined3 = "", splitColumnText= "", lastCodeRepeat = "", beforeCodeRepeat = "", lastCommercial = "", finalAddressWrite = "";

                commercialName = line.Substring(0, 42);
                undefined = line.Substring(42, 17);
                phoneOriginal = line.Substring(59, 10);
                phone = phoneOriginal.Insert(3, " ").Insert(7, "-");
                undefined2 = line.Substring(69, 1);
                undefined3 = line.Substring(70, 57).TrimStart();
                pueblo = undefined3.Substring(38, 3);
                undefined3 = line.Substring(69,51).TrimStart().Substring(2);
                undefinedCode = undefined3.Substring(39, 3);
                undefinedCode2 = undefined3.Substring(42, 3);

                if (line.Length > 129)
                {
                    finalAddress = line.Substring(129);
                }
                else 
                { 
                    finalAddress = ""; 
                }
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
                pueblo = pueblo.Replace(';', 'Ñ');
                pueblo = pueblo.TrimEnd();
                commercialName = commercialName.Replace(';', 'ñ');
                pueblosRegiones.PuebloRegion(pueblo);
                string repeatCommercial = undefined.ToString().Substring(4, 4);
                codeRepeatCommercial.Add(repeatCommercial);
                id++;
                if (commercialWrited.Count > 0) { lastCommercial = commercialWrited.Last(); }
                if (codeRepeatCommercial.Count > 1)
                {
                    lastCodeRepeat = codeRepeatCommercial.Last();
                    beforeCodeRepeat = codeRepeatCommercial.ElementAt(codeRepeatCommercial.Count - 2);

                    if (lastCodeRepeat == beforeCodeRepeat)
                    {
                        ControlGeneral.streamWriter.Write($"{id}|{lastCommercial.Replace(';', 'ñ')}{phone}|{PueblosRegiones.Pueblo}|{PueblosRegiones.Region}|");
                    }
                    else
                    {
                        ControlGeneral.streamWriter.Write($"{id}|{info2[2].Replace(';', 'ñ')}{phone}|{PueblosRegiones.Pueblo}|{PueblosRegiones.Region}|");
                        commercialWrited.Add(info2[2]);
                    }
                }
                else
                {
                    ControlGeneral.streamWriter.Write($"{id}|{info2[2].Replace(';', 'ñ')}{phone}|{PueblosRegiones.Pueblo}|{PueblosRegiones.Region}|");
                    commercialWrited.Add(info2[2]);
                }

                if (undefinedCode2 == "BIO")
                {
                    info2[10] = info2[10].Insert(0, "Bo ");
                }
                if (undefinedCode == "AVE" || undefinedCode == "CAL" || undefinedCode == "BIO" || undefinedCode == "CAR" || info2.Count >= 12)
                {
                    info2[8] = info2[8].TrimEnd('|');
                    if (undefinedCode == "   " && info2[9] == "|")
                    {
                        ControlGeneral.streamWriter.Write(info2[2].Replace(';', 'ñ'));
                    }
                    else
                    {
                        switch (undefinedCode)
                        {
                            case "AVE":
                                info2[8] = $"{info2[8]} Ave {info2[9]}";
                                ControlGeneral.streamWriter.Write(info2[8].Replace(';', 'ñ'));
                                break;
                            case "CAL":
                                info2[8] = $"{info2[8].TrimEnd()} Calle {info2[9]}";
                                ControlGeneral.streamWriter.Write(info2[8].Replace(';', 'ñ'));
                                break;
                            case "BIO":
                                info2[10] = info2[10].Insert(0, "Bo ");
                                break;
                            case "CAR":
                                if (undefinedCode == "CAR" && undefinedCode2 == "PAR")
                                {
                                    info2[9] = $"Carr {info2[9].TrimEnd('|')} {info2[10]}";
                                    info2[10] = "|";
                                    finalAddressWrite = $"{info2[9].TrimEnd('|')} {info2[10]}";
                                    ControlGeneral.streamWriter.Write(finalAddressWrite.Replace(';', 'ñ'));
                                }
                                else
                                {
                                    info2[9] = $"Carr {info2[9]}";
                                    finalAddressWrite = $"{info2[9].TrimEnd('|')} {info2[10]}";
                                    ControlGeneral.streamWriter.Write(finalAddressWrite.Replace(';', 'ñ'));
                                }
                                break;
                            case "   ":
                                if (undefinedCode == "   " && undefinedCode2 == "   ")
                                {
                                    info2[8] = $"{info2[8].TrimEnd('|')}{info2[9]}";
                                    ControlGeneral.streamWriter.Write(info2[8].Replace(';', 'ñ'));
                                }
                                else
                                {
                                    info2[8] = $"{info2[8].TrimEnd('|')}{info2[9]}";
                                }
                                break;
                        }
                    }
                    if (undefinedCode2 == "URB")
                    {
                        info2[8] = $"{info2[8].TrimEnd('|')} Urb {info2[10]}";
                        ControlGeneral.streamWriter.Write(info2[8].Replace(';', 'ñ'));
                    }
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
