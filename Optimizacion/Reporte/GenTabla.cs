using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace P1.Optimizacion.Reporte
{
    class GenTabla
    {
        public void GenHTML2(LinkedList<CodigoC> reporte, String nombre)
        {
            String celdas = "";
            
            celdas = "" +
                "<tr>" +
                "<td> TIPO-OPTIMIZACION </td>" +
                "<td> REGLA </td>" +
                "<td> CODIGO-ELIMINADO </td>" +
                "<td> CODIGO-AGREGADO </td>" +
                "<td> FILA </td>" +
                "</tr>\n";

            foreach (CodigoC cod in reporte)
            {
                
                //System.Diagnostics.Debug.WriteLine("el item es: " + item.Key.ToString() + " el valor es " + aux.val + " tipo: " + aux.tip);
                celdas = celdas + "" +
                    "<tr>\n" +
                    "<td> " + cod.tipoOp + " </td>\n" +
                    "<td> " + "Regla "+cod.regla + " </td>\n" +
                    "<td> " + cod.CodDelete + " </td>\n" +
                    "<td> " + cod.CodAdd + " </td>\n" +
                    "<td> " + cod.fila + " </td>\n" +
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


            string output = nombre + ".html";

            File.WriteAllText(output, html);
        }
    }
}
