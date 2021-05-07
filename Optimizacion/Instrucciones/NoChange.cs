using P1.Optimizacion.Arbol;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class NoChange : Instr2
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }

        String cadenaEntera;

        public NoChange(String cadenaEntera)
        {
            this.cadenaEntera = cadenaEntera;
        }

        public object getOptimizar(AST2 arbol, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            throw new NotImplementedException();
        }
    }
}
