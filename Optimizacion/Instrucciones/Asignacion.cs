using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;


namespace P1.Optimizacion.Instrucciones
{
    class Asignacion : Instr2
    {
        public int lin { get ; set; }
        public int col { get ; set; }

        String id;
        Expr2 val;


        public Asignacion(String id, Expr2 val,int lin, int col)
        {
            this.lin = lin;
            this.col = col;
            this.id = id;
            this.val = val;
        }

        public object getOptimizar(AST2 arbol, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
           return null;
        }
    }
}
