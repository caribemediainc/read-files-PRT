using System;
using System.Collections.Generic;
using System.Text;

namespace read_files_PRT.Data
{
    public class BlancasComercialPueblo
    {
        #region Generar archivo Blancas Comercial por Pueblo (PB)
        public void GeneratePB()
        {
            ControlGeneral generalControl = new ControlGeneral();
            generalControl.ValidateFileExistence(ControlGeneral.rutaArchivoWritePB, ControlGeneral.rutaArchivoReadPB);

            while (!ControlGeneral.streamReader.EndOfStream)
            {
                Console.WriteLine("Escribiendo archivo...");
                string line = ControlGeneral.streamReader.ReadLine(), commercialName = "", undefined = "", undefinedCode = "", undefinedCode2 = "", phoneOriginal = "", phone = "", pueblo = "", originalAddress = "", finalAddress = "", newAddress = "",
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
                ControlGeneral.streamWriter.Write($"{commercialName}|{sbUndefined}|{phone}|{sbUndefined2}|{pueblo}|{undefinedCode}|{undefinedCode2}|");
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
