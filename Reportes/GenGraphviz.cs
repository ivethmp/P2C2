using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace P1.Reportes
{
    class GenGraphviz
    {
        /*   public static Image dibujarGrafo(String grafo_en_DOT)
           {
              /* WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
               WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafo_en_DOT);
               byte[] imageBytes = Convert.FromBase64String(img.ToBase64String());
               MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
               ms.Write(imageBytes, 0, imageBytes.Length);
               Image imagen = Image.FromStream(ms, true);
               return imagen;
           }*/

        private static string nodo;
        private static int cont;
        public static String getDot(ParseTreeNode raiz)
        {
            nodo = "digraph G{\n";
            nodo += "node[shape=\"box\"]";
            nodo += "nodo0[label =\"" + raiz.ToString() + "\"];";
            cont = 1;
            //se deber recorrer el AST  
            recorreArbol("nodo0", raiz);
            nodo += "}";
            return nodo;
        }

        private static void recorreArbol(String padre, ParseTreeNode nodos)
        {
            String ignor="";
            foreach (ParseTreeNode hoja in nodos.ChildNodes)
            {
                System.Diagnostics.Debug.WriteLine(hoja.ToString().Split(' ')[0]);
                ignor = hoja.ToString().Split(' ')[0];
                if (!(ignor.Equals(";") || ignor == ")" || ignor == "("))
                {
                    String nombre = "nodo" + cont.ToString();
                    nodo += nombre + "[label=\"" + espacio(hoja.ToString()) + "\"];\n";
                    nodo += padre + "->" + nombre + ";\n";
                    cont++;
                    recorreArbol(nombre, hoja);
                }
                
            }
        }

        private static String espacio(String cad)
        {
            cad = cad.Replace("\\", "\\\\");
            cad = cad.Replace("\"", "\\\"");
            return cad;
        }



        private int index;

        public void graficar(ParseTreeNode nodo)
        {
            StreamWriter archivo = new StreamWriter("ArbolSintactico.dot");
            string contenido = "graph G {";
            contenido += "node [shape = egg];";
            index = 0;
            definirNodos(nodo, ref contenido);
            index = 0;
            enlazarNodos(nodo, 0, ref contenido);
            contenido += "}";
            archivo.Write(contenido);
            archivo.Close();
            DialogResult verImagen = MessageBox.Show("¿Desea visualizar el AST de la cadena ingresada?", "Grafica AST", MessageBoxButtons.YesNo);
            if (verImagen == DialogResult.Yes)
            {
                /*
                ProcessStartInfo startInfo = new ProcessStartInfo(rutaExeDot);
                startInfo.Arguments = "-Tpng ArbolSintactico.dot -o ArbolSintactico.png";
                Process.Start(startInfo);
                Thread.Sleep(2000);
                startInfo.FileName = "ArbolSintactico.png";
                Process.Start(startInfo);*/

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dot.exe",
                    Arguments = "-Tpng ArbolSintactico.dot -o ArbolSintactico.png",
                    UseShellExecute = false
                };
                Process.Start(startInfo);

                Thread.Sleep(2000);
                // generarPagina();

                /*     startInfo = new ProcessStartInfo
                     {
                         FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
                         Arguments = "ReporteAST.html",
                         UseShellExecute = false
                     };
                     Process.Start(startInfo);
                 }*/


            }
            void definirNodos(ParseTreeNode nodo, ref string contenido)
            {
                if (nodo != null)
                {
                    contenido += "node" + index.ToString() + "[label = \"" + nodo.ToString() + "\", style = filled, color = lightblue];";
                    index++;

                    foreach (ParseTreeNode hijo in nodo.ChildNodes)
                    {
                        definirNodos(hijo, ref contenido);
                    }
                }
            }

            void enlazarNodos(ParseTreeNode nodo, int actual, ref string contenido)
            {
                if (nodo != null)
                {
                    foreach (ParseTreeNode hijo in nodo.ChildNodes)
                    {
                        index++;
                        contenido += "\"node" + actual.ToString() + "\"--" + "\"node" + index.ToString() + "\"";
                        enlazarNodos(hijo, index, ref contenido);
                    }
                }
            }

        }
    }
}
