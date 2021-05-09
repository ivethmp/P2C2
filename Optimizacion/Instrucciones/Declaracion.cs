using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class Declaracion : Instr2
    {
        public int lin { get ; set ; }
        public int col { get; set ; }
        Simbolo.Tipo tipo;
        LinkedList<String> id;
        object val;


        public Declaracion(LinkedList<String> id, object val,Simbolo.Tipo tipo, int lin, int col)
        {
            this.val = val;
            this.id = id;
            this.tipo = tipo;
            this.lin = lin;
            this.col = col;
        }

        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            String cadena = "";
            
            if (id.Count > 1)
            {
                foreach (String ide in id)
                {
                    cadena = cadena + ide.ToString() + ",";
                    
                }
                if (cadena != "")
                {
                    cadena =  cadena.TrimEnd(',') + ";\n\n";
                    nuevo.AddLast(new NewCod(tipo.ToString().ToLower() + " " + cadena ));
                }
            } else
            {
                foreach (String ide in id)
                {
                   // if (temp.Contains(ide.ToString()) == false) temp.AddLast(ide.ToString());
                    if (val.ToString() != "f") nuevo.AddLast(new NewCod(tipo.ToString().ToLower() + " " + ide + "[" + val.ToString() + "];"));
                    else nuevo.AddLast(new NewCod(tipo.ToString().ToLower() + " " + ide + ";"));
                }
            }


             
            return null;
        }
    }
}
