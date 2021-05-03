using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Repeat : Instruc
    {
        public int lin { get; set; }
        public int col { get; set; }
        LinkedList<Instruc> instrucciones;
        Expr Condicion;

        public Repeat(LinkedList<Instruc>instrucciones, Expr Condicion, int lin, int col)
        {
            this.instrucciones = instrucciones;
            this.Condicion = Condicion;
            this.lin = lin;
            this.col = col;
        }
        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            Instruc NewEtiq = new Etiq(inter, "");
            inter.AddLast(NewEtiq);
            String EtiqNueva = (String)NewEtiq.ejecutar(gen,en, arbol, inter);
            //genero codigo de la etiqueta del ciclo repeat
            inter.AddLast(new GenCod("", "", "", "IF", "\n" + EtiqNueva + ":\n", ""));

            foreach (Instruc ins in instrucciones)
            {//ejecuto cada instruccion dentro del repeat
                ins.ejecutar(gen,en, arbol, inter);
               
            }
            //genero el 3D del condicional Until
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
            //salto al bucle del ciclo repeat
            inter.AddLast(new GenCod("", "", "", "GOTO", EtiqNueva, ""));
            //etiqueta de condicion falsa
            inter.AddLast(new GenCod("", "", "", "IF", "", etiqF));

            return null;
        }
    }
}
