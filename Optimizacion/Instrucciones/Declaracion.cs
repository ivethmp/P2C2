using P1.Optimizacion.Arbol;
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

        public object getOptimizar(AST2 arbol, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            return null;
        }
    }
}
