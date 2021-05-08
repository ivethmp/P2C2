using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class vacio : Instr2
    {
        public int lin { get; set; }
        public int col { get; set; }
        public vacio()
        {

        }
        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevos, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            return null;
        }
    }
}
