using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Case : Instruc
    {
        public int lin { get; set; }
        public int col { get; set; }
        Expr valor1;
        LinkedList<Case_Instr> listCase;
        LinkedList<Instruc> listaElse;

        public Case(Expr valor1,  LinkedList<Case_Instr> listCase, LinkedList<Instruc> listaElse, int lin, int col)
        {
            this.valor1 = valor1;
            this.listCase = listCase;
            this.listaElse = listaElse;
            this.col = col;
            this.lin = lin;

        }
        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            Instruc salto = new Etiq(inter, "");//genero nueva etiqueta de salto para cada caso exitoso
            //Obtengo la etiqueta, pero no genero su codigo
            String saltos = (String)salto.ejecutar(gen,en, arbol, inter);
            inter.AddLast(salto);
            foreach (Case_Instr caso  in listCase)
            {
                Oper operacion = new Oper(valor1, caso.expresion, Oper.tipOper.IGUAL);
                LinkedList<Instruc> etiquetas = (LinkedList<Instruc>)operacion.getValImp(gen,en, arbol, inter);
                String etiqV = "";
                String etiqF = "";
                foreach (Etiq eti in etiquetas)
                {
                    if (eti.cond == "true") etiqV = etiqV + "L" + eti.numero + ":\n";
                    if (eti.cond == "false") etiqF = etiqF + "L" + eti.numero + ":\n";
                }
                //Agregamos la etiqueta verdadera
                inter.AddLast(new GenCod("", "", "", "IF", etiqV, ""));
                //ejecutamos las instrucciones dentro del caso
                foreach (Instruc ins in caso.instruccion)
                {
                    ins.ejecutar(gen,en, arbol, inter);
                }
                //agrego el salto en cada if ya que indica que se cumplio esta condicion y debe salir del if
                inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                inter.AddLast(new GenCod("", "", "", "IF", "", etiqF));//seria como el else si no se cumple la condicion 

            }
            if(listaElse != null)//quiere decir que hay contenido en el else
            {
                foreach(Instruc ins in listaElse)
                {
                    ins.ejecutar(gen,en, arbol, inter);
                }

            }

            inter.AddLast(new GenCod("", "", "", "IF", saltos + ":\n", ""));
            return null;

        }
    }
}
