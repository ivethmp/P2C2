using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Case_Instr : Instruc
    {
        public int lin { get ; set ; }
        public int col { get ; set; }

        public Expr expresion;
        public LinkedList<Instruc> instruccion;

        public Case_Instr(Expr expresion, LinkedList<Instruc> instruccion, int lin, int col)
        {
            this.expresion = expresion;
            this.instruccion = instruccion;
            this.lin = lin;
            this.col = col;
        }

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            return null;
        }
    }
}
