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
        public String ambito;
        public String rol;
        public int apuntador;
        public int param;

        public int lin { get ; set ; }
        public int col { get ; set ; }

        public Simb(String id, Tipo tip, String ambito, String rol, int apuntador, int param, int lin, int col)
        {
            this.tip = tip;
            this.id = id;
            this.ambito = ambito;
            this.rol = rol;
            this.apuntador = apuntador;
            this.param = param;
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

        public int getApuntador()
        {
            return apuntador;
        }

        public int getParam()
        {
            return param;
        }
        public void setVal(object valor)
        {
            val = valor;
        }

        public Tipo getTipo(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            return tip;
        }

        public object getValImp(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            return val;
        }
    }
}
