using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class ArrayS : Expr2, Instr2
    {
        public int lin { get; set ; }
        public int col { get ; set; }

        String id1;
        Simbolo.Tipo tipo;
        Simbolo.Tipo tipo2;
        String id2;
        object val;

        public ArrayS(String id1, Simbolo.Tipo tipo, String id2, Simbolo.Tipo tipo2,object val ,int lin, int col)
        {
            this.id1 = id1;
            this.tipo = tipo;
            this.id2 = id2;
            this.tipo2 = tipo2;
            this.val = val;
            this.lin = lin;
            this.col = col;
        }

        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo,LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            String cadena = id1+"[("+tipo+")"+id2+"]";
            if (!temp.Contains(id2)) temp.AddLast(id2);
            return cadena;
        }
    }
}
