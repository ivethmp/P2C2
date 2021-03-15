using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class CallFunc : Instruc, Expr
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }
        String id;
        LinkedList<Expr> valoresP;

        public CallFunc(String id,LinkedList<Expr> valoresP, int lin, int col)
        {
            this.id = id;
            this.valoresP = valoresP;
            this.lin = lin;
            this.col = col;
        }

       

        public Simb.Tipo getTipo(Entor ent, AST arbol)
        {
            Func f = arbol.getFuncion(id);//IDE.getFunction(id);
            if (null != f)
            {
                return f.tipoF;
            }
            else
            {
                Form1.error.AppendText("La func/procedure:" + id + " no existen, lin:" + lin + " col:" + col + "\n");
                return Simb.Tipo.VOID;
            }
        }

        

        public object ejecutar(Entor ent, AST arbol)
        {
            return getValImp(ent, arbol);
        }

        public object getValImp(Entor en, AST arbol)
        {
            Func f = arbol.getFuncion(id);//IDE.getFunction(id);
            if (null != f)
            {
                f.setValParam(valoresP);
                Object resul = f.ejecutar(en, arbol);

                //si viene un return dentro
                
                  if (resul is ExitR)
                 {
                     return null;
                 }
                 else
                 {
                     return resul;
                 }
            }
            else
            {
                Form1.error.AppendText("La func/procedure " + id + " no existen, lin:" + lin + " col: " + col + "\n");
            }
            return null;
        }
    }
}
