using P1.Instruccion;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Arbol
{
    class AST
    {
        private LinkedList<Object> objs;
        private LinkedList<Instruc> instrucciones;

        public AST(LinkedList<Instruc> instrucciones)
        {
            this.instrucciones = instrucciones;
        }
        /*
        public LinkedList<Instruc> Instrucciones { 
            get => instrucciones; 
            set => instrucciones = value; }
        */
        public void addObject(Object a)
        {
            objs.AddLast(a);
        }

        public LinkedList<Instruc> Instrucciones
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



        public Func getFuncion(String id)
        {
            
            foreach (Instruc ins in instrucciones)
            {
                if(ins is Func)
                {
                    if (((Func)ins).id.ToLower().Equals(id.ToLower()))
                    {
                        return (Func)ins;
                    }
                }
                
            }
            return null;
        }

        

    }
}
