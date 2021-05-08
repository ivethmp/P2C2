using P1.Optimizacion.Arbol;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Interfaz
{
    interface Instr2
    {
        public int lin { get; set; }
        public int col { get; set; }
        object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevos, LinkedList<CodigoC> reporte, LinkedList<String>temp);
    }
}
