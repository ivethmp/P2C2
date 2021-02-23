using System;
using System.Collections.Generic;
using System.Text;

namespace P1.TS
{
    //En simbolo se definen la estructura de cada uno de estos
    public class Simb
    {

        private Tipo tip;

        private String id;
        private Object val;

        public Simb(String id, Tipo tip)
        {
            this.tip = tip;
            this.id = id;
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
            ARRAY
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

    }
}
