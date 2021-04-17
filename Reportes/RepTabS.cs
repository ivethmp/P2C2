using P1.Arbol;
using P1.TS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace P1.Reportes
{
    class RepTabS
    {

        public void GenHTML(Entor en)
        {
            String celdas = "";
            Simb aux;

            celdas = "" +
                "<tr>" +
                "<td> ID() </td>" +
                "<td> TIPO </td>" +
                "<td> AMBITO </td>" +
                "<td> ROL </td>" +
                "<td> APUNTADOR </td>" +
                "<td> PARAMETROS </td>" +
                "<td> LINEA </td>" +
                "<td > COL </td>" +
                "</tr>\n";

            foreach (DictionaryEntry item in en.TabSimb)
            {
                aux = (Simb)item.Value;
                //System.Diagnostics.Debug.WriteLine("el item es: " + item.Key.ToString() + " el valor es " + aux.val + " tipo: " + aux.tip);
                celdas = celdas + "" +
                    "<tr>\n" +
                    "<td> " + aux.id + " </td>\n" +
                    "<td> " + aux.tip + " </td>\n" +
                    "<td> " + aux.ambito + " </td>\n" +
                    "<td> " + aux.rol + " </td>\n" +
                    "<td> " + aux.apuntador + " </td>\n" +
                    "<td> " + aux.param + " </td>\n" +
                    "<td> " + aux.lin + " </td>\n" +
                    "<td> " + aux.col + " </td>\n" +
                    "</tr>\n";
            }

            String html = "" +
                "<html> \n" +
                "<head> Tabla C Simbolo</head>" +
                "<body>" +
                "<center><h1> Tabla de Simbolos </h1>" +
                "<table style=\"text-align:center;\" class=\"egt\">\n" +
                celdas + "\n" +
                "</table>" +
                "</center>" +
                "</body>" +
                "</html>";


            string output = "ReporteTablaS.html";

            File.WriteAllText(output, html);
        }
    }
}
