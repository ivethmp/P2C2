using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class Primitivo: Expr2
    {
        public int lin { get; set; }
        public int col { get; set; }

        private object val;

        public Primitivo(object val, int col, int lin)
        {
            this.val = val;
            this.col = col;
            this.lin = lin;
        }

        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo,LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            return val;
        }
    }
}
