using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P1.Generacion;

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
        public String ambito;

        public Func(String id,String ambito, LinkedList<Declara> param, LinkedList<Instruc> instrucciones, Simb.Tipo tipoF, int lin, int col)
        {
            this.id = id;
            this.param = param;
            this.instrucciones = instrucciones;
            this.tipoF = tipoF;
            this.lin = lin;
            this.col = col;
            this.ambito = ambito;
            
        }
        /*public Func(String id, LinkedList<Instruc> instrucciones, Simb.Tipo tipoF, int lin, int col)
        {
            this.id = id;
            this.instrucciones = instrucciones;
            this.tipoF = tipoF;
            this.lin = lin;
            this.col = col;
        }*/

        public object ejecutar(Entor gen, Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            Entor tabL = new Entor(en);
            tabL.Clone();

            int parametros = 0;
            if (tipoF != Simb.Tipo.VOID) parametros = 1;//corrimiento para el return en caso sea una funcion

            gen.Agregar("FunProc-"+id+ en.TabSimb.Count, new Simb("FunProc-"+id, tipoF, ambito, "Proc-Fun", 0,parametros, lin, col));
            tabL.Agregar("FunProc-"+id, new Simb("FunProc-"+id, tipoF, ambito, "Proc-Fun", 0,parametros, lin, col));
            

            inter.AddLast(new GenCod("void "+id+"(){\n\n", "", "", "TEXTO", "", ""));
            /*Etiq etiqueta = new Etiq(inter, "");
            String salida = (String)etiqueta.ejecutar(gen, en, arbol, inter);
            
            inter.AddLast(new GenCod("", "", "", "GOTO", salida, ""));
            */
            Simb aux = en.getSimb(ambito);
            P.apu = P.apu+ aux.apuntador;
       
            if (valParam == null)//cuando no tiene parametros inicializados//posible procedimiento
            {
                valParam = new LinkedList<Expr>();
            }
            

            //Valor de los parametros en su entorno
            LinkedList<Expr> valores = new LinkedList<Expr>();
           
            foreach (Expr e in valParam)//valParam son de la llamada a funcion 
            {
                //dejo todo con valores de 0
                valores.AddLast(new Prim(0,0,0));
                //valores.AddLast(new Prim(e.getValImp(gen,en, arbol, inter), 0, 0));
            }

            int contParam = 0;
            foreach (Declara dec in param)//cada declaracion en parametros
            {
                foreach(Simb sim in dec.listaID)//cada simbolo en la declaracion 
                {
                    contParam++;
                }
                dec.ejecutar(gen,tabL, arbol, inter);
            }

         //   if (contParam == valores.Count)// verifica que las variables son el mismo numero de la enviada por la llamada
          //  {
                //NO NECESITO ACTUALIZAR VALORES 
                //declaracion de variables
                /*  contParam = 0;
                  foreach (Declara dec in param)//vuelvo a evaluar en cada declaracion para obtener los simbolos de cada una
                  {
                      //contParam = 0;
                      foreach (Simb sim in dec.listaID)// evaluo cada simbolo
                      {
                          Expr exp = valores.ElementAt(contParam);//obtengo el valor en el orden del la llama a funcion o procedimiento
                          contParam++;//aumento contador para el siguiente parametro en la llamada a funcion 
                          (new Asig(sim.id, exp, sim.lin, sim.col)).ejecutar(gen,tabL, arbol,inter);// actualizo los valores, para evaluar bien los envio a asignacion 
                      }

                  }*/

               // inter.AddLast(new GenCod("", "", "", "IF", salida+":\n\n", ""));

                foreach (Instruc e in instrucciones)//ejecuto cada instruccion dentro de la funcion o procedimiento
                {
                    Object res = e.ejecutar(gen,tabL, arbol, inter);
      //              return res;

                    if( e is ExitR)
                    {
                        //deberia de retornar un temporal correspondiente a la salida de exit
                        Temp retorno1 = new Temp(inter);
                        inter.AddLast(retorno1);
                        String retor1 = (String)retorno1.ejecutar(gen, en, arbol, inter);
                        inter.AddLast(new GenCod("sp", "0", "+", retor1, "", ""));
                        inter.AddLast(new GenCod(retor1, res + "", "", "GETSTACK", "", ""));
                        //return res;
                    }

                     if (res != null )//casos donde la funcion retorna un valor
                    {
                        //return res;
                    }
                }
          //  }
           /* else// significa que no contiene el mismo numero de parametro la funcion con la llamada a esta
            {
               // Form1.error.AppendText("Error semantico en ejecutar " + this.id + ", lin:"+ lin + " y col:" + col + ", no se tienen los mismos parametros!!\n");
                return null;
            }*/

            inter.AddLast(new GenCod("return;\n}\n\n", "", "", "TEXTO", "", ""));

               int totales = tabL.getSimb("FunProc-" + id).param;

               for (int i = 0; i < totales; i++)
               {
                P.Stack0.RemoveLast();
                  /* foreach(Instruc ins in inter)
                   {
                       if (ins is Stack)
                       {
                           inter.Remove(ins);
                           break;
                       }
                   }*/
               }


            P.apu = P.apu - aux.apuntador;


                return null;
        }
        //tiene que ver con la llamada a funcion, actualiza los valores de esta lista para poder evaluar al ejecutar la funcion 
        public void setValParam(LinkedList<Expr> a)
        {
            valParam = a;
        }
    }
}

