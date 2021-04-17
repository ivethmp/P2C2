using P1.Arbol;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.TS
{
    //En simbolo se definen la estructura de cada uno de estos
    class Simb:Expr
    {

        public Tipo tip;
        public String id;
        public Object val;

        public int lin { get ; set ; }
        public int col { get ; set ; }

        public Simb(String id, Tipo tip, int lin, int col)
        {
            this.tip = tip;
            this.id = id;
            this.lin = lin;
            this.col = col;
        }

        public enum Tipo
        {
            STRING,
            INT,
            REAL,
            BOOL,
            VOID,
            TYPE,
            OBJ,
            ARRAY,
            INVAL
        }

        public String getId()
        {
            return id;
        }

        public Object getVal()
        {
            return val;
        }

        public void setVal(object valor)
        {
            val = valor;
        }

        public Tipo getTipo(Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            return tip;
        }

        public object getValImp(Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            return val;
        }
    }
}
