using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class Identificador: Expr2
    {
        public int lin { get; set; }
        public int col { get; set; }
        private String ide;

        public Identificador(String ide, int lin, int col)
        {
            this.ide = ide;
            this.lin = lin;
            this.col = col;
        }

        public object getOptimizar(AST2 arbol, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            return ide;
        }
    }
}
