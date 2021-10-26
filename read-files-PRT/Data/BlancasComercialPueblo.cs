using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace read_files_PRT.Data
{
    public class BlancasComercialPueblo
    {
        #region Generar archivo Blancas Comercial por Pueblo (PB)
        public void GeneratePB()
        {
            ControlGeneral generalControl = new ControlGeneral();
            PueblosRegiones pueblosRegiones = new PueblosRegiones();
            generalControl.ValidateFileExistence(ControlGeneral.rutaArchivoWritePB_RES, ControlGeneral.rutaArchivoReadPB);
            generalControl.ValidateFileExistence(ControlGeneral.rutaArchivoWritePB_NEG, ControlGeneral.rutaArchivoReadPB);

            while (!ControlGeneral.streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = ControlGeneral.streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
                    undefined2 = "", splitColumnText, finalAddressWrite;
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
                pueblo = pueblo.TrimEnd();
                commercialName = commercialName.Replace(';', 'ñ');
                pueblosRegiones.PuebloRegion(pueblo);

                //Se determina si el registro es de tipo Negocio o Residencial:
                if (sbUndefined.ToString().Substring(16,2) == "2*") //NEGOCIO
                {
                    ControlGeneral.streamWriterPB_NEG.Write($"{commercialName}|{sbUndefined}|{sbUndefined.ToString().Substring(16, 2)}|{phone}|{sbUndefined2}|{pueblo}|{PueblosRegiones.Pueblo}|{PueblosRegiones.Region}|{undefinedCode}|{undefinedCode2}|");
                    //if(info2.Count == 12)
                    //{
                    //    info2.RemoveAt(10);
                    //    info2.RemoveAt(10);
                    //}
                    if(undefinedCode2 == "BIO")
                    {
                        info2[10] = info2[10].Insert(0, "Bo ");
                    }
                    if (undefinedCode == "AVE" || undefinedCode == "CAL" || undefinedCode == "BIO" || undefinedCode == "CAR" || info2.Count >= 12)
                    {
                        info2[8] = info2[8].TrimEnd('|');
                        switch (undefinedCode)
                        {
                            case "AVE":
                                info2[8] = $"{info2[8]} Ave {info2[9]}";
                                ControlGeneral.streamWriterPB_NEG.Write(info2[8]);
                                break;
                            case "CAL":
                                info2[8] = $"{info2[8].TrimEnd()} Calle {info2[9]}";
                                ControlGeneral.streamWriterPB_NEG.Write(info2[8]);
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
                                    ControlGeneral.streamWriterPB_NEG.Write(finalAddressWrite);
                                }
                                else
                                {
                                    info2[9] = $"Carr {info2[9]}";
                                    finalAddressWrite = $"{info2[9].TrimEnd('|')} {info2[10]}";
                                    ControlGeneral.streamWriterPB_NEG.Write(finalAddressWrite);
                                }
                                break;
                            default:
                                info2[8] = $"{info2[8].TrimEnd('|')}{info2[9]}";
                                ControlGeneral.streamWriterPB_NEG.Write(info2[8]);
                                break;
                        }
                        if(undefinedCode2 == "URB")
                        {
                            info2[8] = $"{info2[8].TrimEnd('|')} Urb {info2[10]}";
                            ControlGeneral.streamWriterPB_NEG.Write(info2[8]);
                        }
                    }
                    
                    foreach (string dir in info2)
                    {
                        //ControlGeneral.streamWriterPB_NEG.Write($"{dir}");
                    }
                    ControlGeneral.streamWriterPB_NEG.WriteLine();
                }
                else //RESIDENCIAL
                {
                    ControlGeneral.streamWriterPB_RES.Write($"{commercialName}|{sbUndefined}|{sbUndefined.ToString().Substring(16, 2)}|{phone}|{sbUndefined2}|{pueblo}|{PueblosRegiones.Pueblo}|{PueblosRegiones.Region}|{undefinedCode}|{undefinedCode2}|");
                    foreach (string dir in info2)
                    {
                        ControlGeneral.streamWriterPB_RES.Write($"{dir}");
                    }
                    ControlGeneral.streamWriterPB_RES.WriteLine();
                }
            }
            ControlGeneral.streamWriterPB_RES.Close();
            ControlGeneral.streamWriterPB_NEG.Close();
            Console.WriteLine("Se ha exportado el archivo txt correctamente.");
            generalControl.RepeatMenu();
        }
        #endregion
    }
}
