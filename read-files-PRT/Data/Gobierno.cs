using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace read_files_PRT.Data
{
    public class Gobierno
    {
        #region Generar archivo Gobierno (GB)
        string subCommercial, subInitial;
        public void GenerateGB()
        {
            ControlGeneral generalControl = new ControlGeneral();
            generalControl.ValidateFileExistence(ControlGeneral.rutaArchivoWriteGB, ControlGeneral.rutaArchivoReadGB);
            PueblosRegiones pueblosRegiones = new PueblosRegiones();
            List<string> commercialNames = new List<string>();

            while (!ControlGeneral.streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = ControlGeneral.streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
                                    identifier = "", undefined3 = "", cod_producto, splitColumnText, clasificado_formula;
                
                commercialName = line.Substring(0, 42);
                undefined = line.Substring(42, 17);
                phoneOriginal = line.Substring(59, 10);
                phone = phoneOriginal.Insert(3, " ").Insert(7, "-");
                identifier = line.Substring(69, 1);
                undefined3 = line.Substring(70, 57).TrimStart();
                pueblo = undefined3.Substring(38, 3);
                undefined3 = line.Substring(70, 41).Trim().Substring(2);
                if (line.Length > 129)
                {
                    finalAddress = line.Substring(129);
                }
                else { finalAddress = ""; }

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
                if (phone == "000 000-0000") phone = "";
                pueblo = pueblo.Replace(';', 'Ñ');
                if (pueblo.EndsWith(' ')) pueblo = pueblo.TrimEnd();
                pueblosRegiones.PuebloRegion(pueblo);
                if(identifier == "S")
                {
                    cod_producto = "TL";
                }
                else if(identifier == "R")
                {
                    cod_producto = "NM";
                }
                else if(commercialName == commercialName.ToUpper() && phone == "")
                {
                    cod_producto = "NAME";
                }
                else
                {
                    cod_producto = "RG";
                }

                if (commercialName != "")
                {
                    subInitial = commercialName.Substring(0, 7);
                }

                if (commercialName.Length > 7)
                {
                    subCommercial = commercialName.Substring(8, commercialName.Length - 8);
                    commercialNames.Add(commercialName);
                    foreach (string item in info2)
                    {
                        var last = info2.LastOrDefault();
                        if(last == "|")
                        {
                            if (item.Length > subCommercial.Length)
                            {
                                if (item.Substring(0, 3) == "U S")
                                {
                                    item.Replace(item.Substring(0, 3), "");
                                }
                                commercialName = commercialName.Replace(subCommercial, item.Substring(0, item.Length - 1));
                                commercialNames.Add(commercialName);
                            }
                        }
                    }
                }
                else if (commercialName.Length > 7 && subInitial == "* GOB R")
                {
                    commercialName = "* GOB M RECREACION Y DEPORTES DEPARTAMENTO DE";
                    commercialNames.Add(commercialName);
                }
                else if(commercialName.Length == 28 && subInitial == "* GOB U")
                {
                    commercialName = "* GOB U S POSTAL SERVICE";
                    commercialNames.Add(commercialName);
                }
                else if(subInitial == "* GOB U" && commercialName.Length > 9 && (commercialName.Substring(6, 7) == "U S U S" || commercialName.Substring(6, 6) == "US U S"))
                {
                    if (commercialName.Substring(6, 7) == "U S U S")
                    {
                        commercialName = commercialName.Replace(commercialName.Substring(6, 7), "U S");
                        commercialNames.Add(commercialName);
                    }
                    else
                    {
                        commercialName = commercialName.Replace(commercialName.Substring(6, 6), "U S");
                        commercialNames.Add(commercialName);
                    }
                }
                else if(commercialName.Length == 7)
                {
                    commercialNames.Add(commercialName);
                }
                else
                {
                    commercialName = commercialNames.Last();
                }

                if (subInitial == "* GOB E")
                {
                    clasificado_formula = "GOBIERNO DEL ELA";
                }
                else if(subInitial == "* GOB M" || subInitial =="* GOB R")
                {
                    clasificado_formula = "GOBIERNO MUNICIPAL";
                }
                else if(subInitial == "* GOB U")
                {
                    clasificado_formula = "GOBIERNO FEDERAL (U S GOVERNMENT)";
                }
                else
                {
                    clasificado_formula = "COURTS";
                }
                commercialName = commercialName.Replace(subInitial, "").TrimStart();
                switch(commercialName)
                {
                    case "S U S POSTAL SERVICE":
                        commercialName = "U S POSTAL SERVICE";
                        break;
                    case "S AGRICULTURE DEPARTMENT OF":
                        commercialName = "AGRICULTURE DEPARTMENT OF";
                        break;
                    case "S JOB CORPS":
                        commercialName = "JOB CORPS";
                        break;
                    case "S ARMY DEPARTMENT OF THE":
                        commercialName = "ARMY DEPARTMENT OF THE";
                        break;
                    case "ECREACION Y DEPORTES DEPARTAMENTO":
                        commercialName = "RECREACION Y DEPORTES DEPARTAMENTO DE";
                        break;
                    case "S CUSTOMS AND BORDER PROTECTION":
                        commercialName = "CUSTOMS AND BORDER PROTECTION";
                        break;
                    case "S HOMELAND SECURITY DEPARTMENT OF":
                        commercialName = "HOMELAND SECURITY DEPARTMENT OF";
                        break;
                    case "S PUERTO RICO AIR NATIONAL GUARD":
                        commercialName = "PUERTO RICO AIR NATIONAL GUARD";
                        break;
                    case "S U S BORDER PATROL":
                        commercialName = "U S BORDER PATROL";
                        break;
                    case "S FEDERAL BUREAU OF INVESTIGATION":
                        commercialName = "FEDERAL BUREAU OF INVESTIGATION";
                        break;
                    case "DEFENSORIA PERSONAS CON IMPEDIMEN":
                        commercialName = "DEFENSORIA PERSONAS CON IMPEDIMENTOS";
                        break;
                }
                ControlGeneral.streamWriter.Write($"{commercialName}|{clasificado_formula}|{undefined}|{phone}|{cod_producto}|{identifier}|{pueblo}|{PueblosRegiones.Pueblo}|{PueblosRegiones.Region}|{undefinedCode}|{undefinedCode2}|");
                foreach (string dir in info2)
                {
                    var last = info2.LastOrDefault();
                    if(last == "|") ControlGeneral.streamWriter.Write($"{dir}");
                }
                ControlGeneral.streamWriter.WriteLine();
            }
            ControlGeneral.streamWriter.Close();
            Console.WriteLine("Se ha exportado el archivo txt correctamente.");
            generalControl.RepeatMenu();
        }
        #endregion

        bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]) && !char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }
    }
}
