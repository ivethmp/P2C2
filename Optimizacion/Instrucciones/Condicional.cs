using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class Condicional : Instr2
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }

        Expr2 condicion;
        Instr2 GOTOO;
        public Condicional(Expr2 condicion, Instr2 GOTOO, int lin, int col)
        {
            this.condicion = condicion;
            this.GOTOO = GOTOO;
            this.lin = lin;
            this.col = col;
        }
        public object getOptimizar(AST2 arbol, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            throw new NotImplementedException();
        }
    }
}
