using read_files_PRT.Data;
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
             rutaArchivoWritePB = Properties.Resources.rutaArchivoWritePB_RES, rutaArchivoReadGB = Properties.Resources.rutaArchivoReadGB,
                rutaArchivoWriteGB = Properties.Resources.rutaArchivoWriteGB, rutaArchivoReadBC = Properties.Resources.rutaArchivoReadBC,
                rutaArchivoWriteBC = Properties.Resources.rutaArchivoWriteBC;
        public static int menuControl = 1;
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            ControlGeneral controlGeneral = new ControlGeneral();
            controlGeneral.Menu();
        }
    }
}