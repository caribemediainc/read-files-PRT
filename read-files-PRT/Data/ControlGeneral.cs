using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace read_files_PRT.Data
{
    public class ControlGeneral
    {
        #region Variables de control general
        /*En esta sección se encuentran las variables mediante las que se realizará la lectura/escritura
        de los archivos correspondientes, así como la variable del control del menú.*/
        public static StreamReader streamReader;
        public static StreamWriter streamWriter;
        public static StreamWriter streamWriterPB_RES;
        public static StreamWriter streamWriterPB_NEG;
        public static string rutaArchivoReadPB = Properties.Resources.rutaArchivoReadPB,
             rutaArchivoWritePB_RES = Properties.Resources.rutaArchivoWritePB_RES, rutaArchivoReadGB = Properties.Resources.rutaArchivoReadGB,
             rutaArchivoWritePB_NEG = Properties.Resources.rutaArchivoWritePB_NEG, rutaArchivoWriteGB = Properties.Resources.rutaArchivoWriteGB,
             rutaArchivoReadBC = Properties.Resources.rutaArchivoReadBC, rutaArchivoWriteBC = Properties.Resources.rutaArchivoWriteBC,
             rutaArchivoPueblosRegiones = Properties.Resources.rutaArchivoPueblosRegiones;
        public static int menuControl = 1;

        #endregion

        #region Instancias de clases
        /*En esta sección se crean los objetos de las clases de acuerdo
         a la sección que se obtendrá por archivo (Blancas Comercial,
        Blancas Comercial por Pueblo y Gobierno)*/
        BlancasComercial secBlancasComercial = new BlancasComercial();
        BlancasComercialPueblo secBlancasComercialPueblo = new BlancasComercialPueblo();
        Gobierno secGobierno = new Gobierno();
        #endregion

        #region Validación de existencia de archivos
        /*CreateFiles: Creación de archivos.
         ValidateFileExistence: Validar si existe el archivo deseado.*/
        public void CreateFiles(string routeWrite, string routeRead)
        {
            if(routeWrite == rutaArchivoWritePB_RES || routeWrite == rutaArchivoWritePB_NEG)
            {
                if (routeWrite == rutaArchivoWritePB_RES)
                {
                    streamReader = new StreamReader(routeRead);
                    streamWriterPB_RES = new StreamWriter(routeWrite);
                }
                else
                {
                    streamReader = new StreamReader(routeRead);
                    streamWriterPB_NEG = new StreamWriter(routeWrite);
                }
            }
            else
            {
                streamReader = new StreamReader(routeRead);
                streamWriter = new StreamWriter(routeWrite);
            }
        }
        public void ValidateFileExistence(string routeWrite, string routeRead)
        {
            if (!File.Exists(routeWrite))
            {
                CreateFiles(routeWrite, routeRead);
            }
            else
            {
                File.Delete(routeWrite);
                CreateFiles(routeWrite, routeRead);
            }
        }
        #endregion

        #region Menu
        public void Menu()
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
                        secBlancasComercial.GenerateBC();
                        break;
                    case 2:
                        secBlancasComercialPueblo.GeneratePB();
                        break;
                    case 3:
                        secGobierno.GenerateGB();
                        break;
                }
            } while (menuControl == 1);
        }
        #endregion

        #region Repetir menú
        public void RepeatMenu()
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
        #endregion
    }
}
