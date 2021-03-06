using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class ETIQUETA : Instr2
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }
        public String id;
        public ETIQUETA(String id, int lin, int col)
        {
            this.id = id;
            this.lin = lin;
            this.col = col;
        }

        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            if (lin == -10) return id;

            nuevo.AddLast(new NewCod(id+":"));
            return id;
        }
    }
}
