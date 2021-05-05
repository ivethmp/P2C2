using P1.Arbol;
using P1.Generacion;
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

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {
           
            Instruc NewEtiq = new Etiq(inter,"");
            inter.AddLast(NewEtiq);
            String EtiqNueva = (String)NewEtiq.ejecutar(gen,en, arbol, inter);
            //genero codigo de la etiqueta del ciclo while
            inter.AddLast(new GenCod("", "", "", "IF", "\n"+EtiqNueva+":\n", ""));
            //Retorna las etiquetas verdaderas y falsas del condicional del while
            LinkedList<Instruc> etiquetas = (LinkedList<Instruc>)condi.getValImp(gen,en, arbol, inter);
            String etiqV = "";
            String etiqF = "";

            foreach (Etiq eti in etiquetas)
            {
                if (eti.cond == "true") etiqV = etiqV + "L" + eti.numero;
                if (eti.cond == "false") etiqF = etiqF + "L" + eti.numero;
            }
            //generno el codigo para la intruccion verdadera
            inter.AddLast(new GenCod("", "", "", "IF", etiqV+ ":\n", ""));
                foreach (Instruc ins in instrucciones)
                {
                    
                    if (ins is Continue)
                    {
                        inter.AddLast(new GenCod("", "", "", "GOTO", EtiqNueva, ""));
                    }
                    else if (ins is Break)
                    {
                        inter.AddLast(new GenCod("", "", "", "GOTO", etiqF, ""));
                    }
                ins.ejecutar(gen, en, arbol, inter);
            }
            //GENERO EL goto del la etiqueta verdadera para generar el ciclo
            inter.AddLast(new GenCod("", "", "", "GOTO", EtiqNueva, ""));
            // goto siguiente;
            //genero la etiqueta de la condicion falsa
            inter.AddLast(new GenCod("", "", "", "IF", "", etiqF+ ":\n"));

            return null;
        }
    }
}
