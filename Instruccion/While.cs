using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class While : Instruc
    {
        public int lin { get; set; }
        public int col { get; set; }

        Expr condi;
        LinkedList<Instruc> instrucciones;

        public While(Expr condi,LinkedList<Instruc>instrucciones, int lin, int col)
        {
            this.condi = condi;
            this.instrucciones = instrucciones;
            this.lin = lin;
            this.col = col;
        }

        public object ejecutar(Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            Entor TabWhile = new Entor(en);// nuevo entornno para el while
            //TabWhile.NewEntor(en);
            //TabWhile = en;
            
            siguiente:
            if ((bool)condi.getValImp(TabWhile, arbol,inter))
            {
                foreach (Instruc ins in instrucciones)
                {
                    ins.ejecutar(TabWhile, arbol,inter);
                }
                goto siguiente;
            }
            return null;
        }
    }
}
