using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Generacion
{
    class GenCod : Instruc
    {

        /*
        //genera del codigo, a traves de cuadruples
        void genCod(String oper, String t1, String t2, String res)
        {

        }

        String newEtiq(LinkedList<String> etiqNew)
        {
            string etiq = "";
            return etiq;
        }
        
        //genera un nuevo temporal
        String newTemp(LinkedList<String> tempNew) 
        {
            String temp = "t" + tempNew.Count;
            tempNew.AddLast(temp);
            return temp;
        }*/
        public int lin { get ; set ; }
        public int col { get ; set ; }

        String temp1;
        String temp2;
        String oper;
        String res;
        String etiqTrue;
        String etiqFalse;

        public GenCod(String temp1, String temp2, String oper, String res, String etiqTrue, String etiqFalse)
        {
            this.temp1 = temp1;
            this.temp2 = temp2;
            this.oper = oper;
            this.res = res;
            this.etiqFalse = etiqFalse;
            this.etiqTrue = etiqTrue;
        }

        public object ejecutar(Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            
            if(this.res == "REL")
            {
                Form1.salir.AppendText("if("+this.temp1+this.oper + this.temp2 +") goto "+ etiqTrue + "\n" + "goto "+etiqFalse+ "\n");
                return 0;
            }
            else if(this.res == "LOG")
            {
                Form1.salir.AppendText(etiqTrue);
            }
            else if(this.res == "IF")
            {
                if (this.etiqFalse == "")
                {
                    Form1.salir.AppendText(etiqTrue);
                    return 0;
                }
                Form1.salir.AppendText(etiqFalse);
                return 0;
            }
            else if(this.res == "STACK")
            {
                Form1.salir.AppendText("Stack["+temp1+"] = "+temp2 +"; \n");
            }
            else
            {
                if(this.oper == "+")
                {
                    Form1.salir.AppendText(res+"="+temp1+oper+temp2+"; \n");
                    return 0;
                }
            }
            return null;
        }
    }
}
