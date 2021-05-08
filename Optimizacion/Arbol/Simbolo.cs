using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Optimizacion.Arbol
{
    class Simbolo : Expr2
    {
        public Tipo tip;
        public String id;
        public Object val;
        public String ambito;
        public String rol;
        public int apuntador;
        public int param;

        public int lin { get; set; }
        public int col { get; set; }

        public Simbolo(String id, Tipo tip, int lin, int col)
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
            FLOAT,
            CHAR,
            VOID,
            TYPE,
            OBJ,
            ARRAY,
            ARRAY0,
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



        public object getOptimizar(AST2 arbol, LinkedList<Instr2> nuevo, LinkedList<CodigoC> reporte, LinkedList<string> temp)
        {
            return null;
        }
    }
}
