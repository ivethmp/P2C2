using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace P1.Reportes
{
    class GenG
    {
        /* para ejecutarse se debe utilizar de la siguiente forma:
         * El primer String contiene la structura de graphviz
         * String graphVizString = @" digraph g{ label=""Graph""; labelloc=top;labeljust=left;}";
            Bitmap bm = FileDotEngine.Run(graphVizString);
*/
        public static Bitmap Run(string dot)
        {
            string executable = @"dot.exe";
            //string output = "C:\\Users\\Mateo\\Desktop\\C\\salida.txt";
            string output = "salida.txt";
            //string imagen = "C:\\Users\\Mateo\\Desktop\\C\\salida.jpg";
            string imagen = "salida.jpg";
            File.WriteAllText(output, dot);

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = executable;
            process.StartInfo.Arguments = string.Format(" -Tjpg " + output + " -o" + imagen);

            // Go
            process.Start();
            // and wait dot.exe to complete and exit
            process.WaitForExit();
            Bitmap bitmap = null; ;
            using (Stream bmpStream = System.IO.File.Open(imagen, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                bitmap = new Bitmap(image);
            }
            //File.Delete(output);
            File.Delete(output + ".jpg");
            return bitmap;
        }
    }
}
