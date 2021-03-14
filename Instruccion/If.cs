using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class If : Instruc
    {
        public int lin { get ; set; }
        public int col { get ; set  ; }

        public Expr cond;
        public LinkedList<Instruc> instrucciones;
        private LinkedList<Instruc> instElse;
        private LinkedList<If> listaIfElse;

        public If(Expr cond, LinkedList<Instruc> instrucciones, LinkedList<Instruc> instElse, LinkedList<If> listaIfElse, int lin, int col)
        {
            this.cond = cond;
            this.instrucciones = instrucciones;
            this.instElse = instElse;
            this.listaIfElse = listaIfElse;
            this.lin = lin;
            this.col = col;

        }

        public object ejecutar(Entor en, AST arbol)
        {
            //se evalua la condicion// true o false
            if ((bool)cond.getValImp(en, arbol))//primer if true
            {
                Entor tabLoc = new Entor(en);
                foreach (Instruc ins in instrucciones)
                {
                    /* if (ins is Break || inst is Continue)
                     {
                         return ins;
                     }
                     else if (ins is Return)
                     {
                         return ins.ejecutar(local, arbol);
                     }*/
                    ins.ejecutar(tabLoc, arbol);//si no hay return, break o continue, simplemente se ejecutan las instrucciones dentro del if

                }
                return null;
            }
            else
            {
                foreach (If ifElse in listaIfElse)
                {
                    if((bool)ifElse.cond.getValImp(en,arbol))//se evaluan las condiciones de cada if 
                    {
                        Entor tabLoc = new Entor(en);//nueva tabla para el if
                        foreach(Instruc ins in ifElse.instrucciones)//instrucciones dentro del if actual
                        {
                            /*if (ins is Break || ins is Continue)
                            {
                                return ins;
                            }
                            else if (ins is Return)
                            {
                                return ins.ejecutar(tabLoc, arbol);
                            }*/
                            ins.ejecutar(tabLoc, arbol);
                        }
                    }
                }
                
                if (instElse != null)//significa que es un else
                {
                    Entor tabLoc = new Entor(en);
                    foreach (Instruc ins in instElse)
                    {
                       /* if (ins is Break || ins is Continue)
                        {
                            return ins;
                        }
                        else if (ins is Return)
                        {
                            return ins.ejecutar(local, arbol);
                        }*/
                        ins.ejecutar(tabLoc, arbol);
                    }
                }
            }
            return null;
        }
    }
 }

