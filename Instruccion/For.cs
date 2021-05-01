using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class For : Instruc
    {
        public int lin { get ; set; }
        public int col { get; set; }
        Instruc Asignar;
        Expr final;
        String id;
        LinkedList<Instruc> instrucciones;

        public For(Instruc Asignar, String id,Expr final, LinkedList<Instruc> instrucciones, int lin, int col)
        {
            this.lin = lin;
            this.col = col;
            this.id = id;
            this.Asignar = Asignar;
            this.final = final;
            this.instrucciones = instrucciones;
        }

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            //genero el 3d del la asignacion del valor inicial 
            Asignar.ejecutar(gen,en, arbol, inter);
            
           //creo la etiqueta para iniciar el bucle
            Instruc NewEtiq = new Etiq(inter, "");
            inter.AddLast(NewEtiq);//agrego la etiqueta a la lista de etiquetas
            String EtiqNueva = (String)NewEtiq.ejecutar(gen,en, arbol, inter);
            //genero codigo de la etiqueta para el ciclo for
            inter.AddLast(new GenCod("", "", "", "IF", "\n" + EtiqNueva + ":\n", ""));
            //genero la condicion para el for
            Expr Condicion = new Oper(new IdentVal(id, Asignar.lin, Asignar.col), final, Oper.getOperador("<="));
           
            //obtengo las etiquetas verdaderas y falsas de acuerdo a la condicion del for
            LinkedList<Instruc> etiquetas = (LinkedList<Instruc>)Condicion.getValImp(gen,en, arbol, inter);
            String etiqV = "";
            String etiqF = "";

            foreach (Etiq eti in etiquetas)
            {
                if (eti.cond == "true") etiqV = etiqV + "L" + eti.numero + ":\n";
                if (eti.cond == "false") etiqF = etiqF + "L" + eti.numero + ":\n";
            }
            //generno el codigo para la intruccion verdadera
            inter.AddLast(new GenCod("", "", "", "IF", etiqV, ""));
            foreach (Instruc ins in instrucciones)
            {
                ins.ejecutar(gen,en, arbol, inter);
            }
            //genero la operacion de aumento del valor de la variable 
            Expr contador = new Oper(new IdentVal(id, Asignar.lin, Asignar.col), new Prim(1,0,0), Oper.getOperador("+"));
            Asig AsignoN = new Asig(id, contador, Asignar.lin, Asignar.col);
            AsignoN.ejecutar(gen,en, arbol,inter);
            //GENERO EL goto del la etiqueta verdadera para generar el ciclo
            inter.AddLast(new GenCod("", "", "", "GOTO", EtiqNueva, ""));
            // goto siguiente;
            //genero la etiqueta de la condicion falsa
            inter.AddLast(new GenCod("", "", "", "IF", "", etiqF));

            return null;


        }
    }
}
