using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Generacion
{
    class GenCod : Instruc
    {

       
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
        

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            
            if(this.res == "UNO")
            {
                Form1.salir.AppendText("if("+ this.temp2 + ") goto " + etiqTrue + ";\n" + "goto " + etiqFalse + ";\n");
                return 0;
            }
            else if(this.res == "REL")
            {
                Form1.salir.AppendText("if("+this.temp1+this.oper + this.temp2 +") goto "+ etiqTrue + ";\n" + "goto "+etiqFalse+ ";\n");
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
            else if(res == "GOTO")
            {
                Form1.salir.AppendText("goto "+etiqTrue+ "; \n\n");
            }
            else if(this.res == "STACK")
            {
                Form1.salir.AppendText("Stack[(int)"+temp1+"] = "+temp2 +"; \n");
            }else if(this.res == "GETSTACK")
            {
                Form1.salir.AppendText(temp2+"= Stack[(int)"+temp1+"];\n");
            }else if(res == "TEXTO")
            {
                Form1.salir.AppendText(temp1);
            }
            else if(res == "SETHEAP")
            {
                Form1.salir.AppendText(temp1 + "= hp;\n");
            }
            else if (res == "HEAP")
            {
                Form1.salir.AppendText("Heap[(int)hp]=" + temp1 + ";\n");
            }
            else
            {
                if(this.oper == "+" || oper == "-" || oper == "*" || oper == "/" || oper == "")
                {
                    Form1.salir.AppendText(res+"="+temp1+oper+temp2+"; \n");
                    return 0;
                }
            }
            return null;
        }
    }
}
