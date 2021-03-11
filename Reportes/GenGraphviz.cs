using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

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
            nodo = "diagraph G{";
            nodo += "nodo0[label =\"" + raiz.ToString() + "\"];";
            cont = 1;
            //se deber recorrer el AST  
            recorreArbol("nodo0", raiz);
            nodo += "}";
            return nodo;
        }

        private static void recorreArbol(String padre, ParseTreeNode nodos)
        {
            foreach(ParseTreeNode hoja in nodos.ChildNodes)
            {
                String nombre = "nodo" + cont.ToString();
                nodo +=nombre+"[label=\"" + espacio(hoja.ToString())+ "\"];\n";
                nodo += padre + "->" + nombre + ";\n";
                cont++;
                recorreArbol(nombre, hoja);
            }
        }

        private static String espacio(String cad)
        {
            cad = cad.Replace("\\", "\\\\");
            cad = cad.Replace("\"", "\\\"");
            return cad;
        }


    }
}
