using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P1.Instruccion
{
    class Func : Instruc
    {
        public int lin { get ; set; }
        public int col { get; set; }
        LinkedList<Declara> param;
        public String id;
        public Simb.Tipo tipoF;
        LinkedList<Instruc> instrucciones;
        LinkedList<Expr> valParam;

        public Func(String id, LinkedList<Declara> param, LinkedList<Instruc> instrucciones, Simb.Tipo tipoF, int lin, int col)
        {
            this.id = id;
            this.param = param;
            this.instrucciones = instrucciones;
            this.tipoF = tipoF;
            this.lin = lin;
            this.col = col;
        }
        /*public Func(String id, LinkedList<Instruc> instrucciones, Simb.Tipo tipoF, int lin, int col)
        {
            this.id = id;
            this.instrucciones = instrucciones;
            this.tipoF = tipoF;
            this.lin = lin;
            this.col = col;
        }*/

        public object ejecutar(Entor en, AST arbol)
        {
            Entor tabL = new Entor(en);

            if (valParam == null)//cuando no tiene parametros inicializados
            {
                valParam = new LinkedList<Expr>();
            }

            //Valor de los parametros en su entorno
            LinkedList<Expr> valores = new LinkedList<Expr>();
            foreach (Expr e in valParam)//valParam son de la llamada a funcion 
            {
                valores.AddLast(new Prim(e.getValImp(en, arbol), 0, 0));
            }


            int contParam = 0;
            foreach (Declara dec in param)//cada declaracion en parametros
            {
                foreach(Simb sim in dec.listaID)//cada simbolo en la declaracion 
                {
                    contParam++;
                }
                dec.ejecutar(tabL, arbol);
            }

            if (contParam == valores.Count)// verifica que las variables son el mismo numero de la enviada por la llamada
            {
                //declaracion de variables
                
                foreach (Declara dec in param)//vuelvo a evaluar en cada declaracion para obtener los simbolos de cada una
                {
                    contParam = 0;
                    foreach (Simb sim in dec.listaID)// evaluo cada simbolo
                    {
                        Expr exp = valores.ElementAt(contParam);//obtengo el valor en el orden del la llama a funcion o procedimiento
                        contParam++;//aumento contador para el siguiente parametro en la llamada a funcion 
                        (new Asig(sim.id, exp, sim.lin, sim.col)).ejecutar(tabL, arbol);// actualizo los valores, para evaluar bien los envio a asignacion 
                    }

                }
                


                foreach (Instruc e in instrucciones)//ejecuto cada instruccion dentro de la funcion o procedimiento
                {
                    Object res = e.ejecutar(tabL, arbol);
      //              return res;

                    if( e is ExitR)
                    {
                        return res;
                    }

                     if (res != null )//casos donde la funcion retorna un valor
                    {
                        //return res;
                    }
                }
            }
            else// significa que no contiene el mismo numero de parametro la funcion con la llamada a esta
            {
                Form1.error.AppendText("Error semantico en ejecutar " + this.id + ", lin:"+ lin + " y col:" + col + ", no se tienen los mismos parametros!!\n");
                return null;
            }
            return null;
        }
        //tiene que ver con la llamada a funcion, actualiza los valores de esta lista para poder evaluar al ejecutar la funcion 
        public void setValParam(LinkedList<Expr> a)
        {
            valParam = a;
        }
    }
}

