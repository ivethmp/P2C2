using P1.Optimizacion.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Arbol
{
    class AST2
    {

        private LinkedList<Object> objs;
        private LinkedList<Instr2> instrucciones;

        public AST2(LinkedList<Instr2> instrucciones)
        {
            this.instrucciones = instrucciones;
        }
        
        /*public LinkedList<Instr2> Instrucciones { 
            get => instrucciones; 
            set => instrucciones = value; 
        }*/
        
        public void addObject(Object a)
        {
            objs.AddLast(a);
        }

        public LinkedList<Instr2> Instrucciones
        {
            get
            {
                return instrucciones;
            }

            set
            {
                instrucciones = value;
            }
        }



      /*  public Func getFuncion(String id)
        {

            foreach (Instruc ins in instrucciones)
            {
                if (ins is Func)
                {
                    if (((Func)ins).id.ToLower().Equals(id.ToLower()))
                    {
                        return (Func)ins;
                    }
                }

            }
            return null;
        }*/



    }
}
