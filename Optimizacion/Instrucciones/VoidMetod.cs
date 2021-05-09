using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class VoidMetod : Instr2
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }
        String id;
        LinkedList<Instr2> instruccion;
        Simbolo.Tipo tipo;

        public VoidMetod(String id,Simbolo.Tipo tipo, LinkedList<Instr2>instruccion, int lin, int col)
        {
            this.id = id;
            this.tipo = tipo;
            this.instruccion = instruccion;
            this.lin = lin;
            this.col = col;
        }

        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo,LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            AST2 ASTEMP  = new Arbol.AST2(instruccion);
            System.Diagnostics.Debug.WriteLine("las instrucciones son: " + instruccion.Count);
            nuevo.AddLast(new NewCod("void " + id + "()\n{"));
            foreach (Instr2 ins in instruccion)
            {
                ins.getOptimizar(ASTEMP, nuevo, reporte, temp);
                //System.Diagnostics.Debug.Write("la instruccion es: " + ins + " la linea es: " + ins.lin);
                //ASTEMP.Instrucciones.Remove(ins);
            }
            //System.Diagnostics.Debug.WriteLine("las instrucciones2 son: " + instruccion.Count);
            nuevo.AddLast(new NewCod("\n}\n\n"));
            return null;
        }
    }
}
