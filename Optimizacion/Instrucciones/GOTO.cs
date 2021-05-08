using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Instrucciones
{
    class GOTO : Instr2
    {
        public int lin { get ; set; }
        public int col { get ; set ; }
        public String id;

        public GOTO(String id, int lin, int col)
        {
            this.id = id;
            this.lin = lin;
            this.col = col;
        }

        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo,LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {

            string cadenagoto = "goto "+id + ";";
            if (lin == -10) return cadenagoto;//codigo eliminado

            bool bandera = false;
            int cont = 0;
            String optimi = "";
            String elimi = "";
            String agregar = "";
            int regla = 0;
            int fila = 0; ;
            foreach (Instr2 ins in arbol.Instrucciones)
            {
                System.Diagnostics.Debug.WriteLine("esto en goto "+cont);

                if (bandera == true)
                {
                    System.Diagnostics.Debug.WriteLine("la salida de la instrucciones " + ins);
                    if (ins is ETIQUETA)
                    {
                        int aux = ins.lin;
                        ins.lin = -10;
                        agregar = agregar + ins.getOptimizar(arbol,nuevo,reporte,temp);
                        ins.lin = aux;
                        if(regla!=0) reporte.AddLast(new CodigoC(optimi, regla, elimi, agregar, fila));
                        return null;
                    }
                    else
                    {//marcamos el codigo eliminado
                        ins.lin = -10;
                        regla = 1;
                        elimi = elimi + ins.getOptimizar(arbol, nuevo, reporte, temp);
                       
                    }
                }

                if (ins.lin == lin && ins.col == col && ins is GOTO) {
                    bandera = true;
                    cadenagoto = "goto" + id + ";";
                    agregar = cadenagoto;
                    optimi = "Eliminación de código muerto";
                    fila = ins.lin;
                    elimi = elimi + cadenagoto + "\n";

                    nuevo.AddLast(new NewCod(cadenagoto));

                    

                }
                cont++;
            }

            

            return cadenagoto;
        }
    }
}
