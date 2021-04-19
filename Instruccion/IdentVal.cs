using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class IdentVal : Expr
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }
        private String ide;

        public IdentVal(String ide, int lin, int col)
        {
            this.ide = ide;
            this.lin = lin;
            this.col = col;
        }

        public Simb.Tipo getTipo(Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            if (en.buscar(ide))
            {
                Simb simbol = en.getSimb(ide);
                return simbol.tip;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("no encontro el id el tipo saber cual es");
                return Simb.Tipo.VOID;
            }
        }

        public object getValImp(Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            if (en.buscar(ide))
            {
                Simb simbol = en.getSimb(ide);
                //genero un nuevo temporal 
                Instruc temp1 = new Temp(inter);
                inter.AddLast(temp1);//agrego el temporal
                String temp11 = (String)temp1.ejecutar(en, arbol, inter);
                //asigno el temporal con su apuntador y posicion relativa
                inter.AddLast(new GenCod("sp", ""+ simbol.apuntador, "+", temp11, "", ""));
                Instruc temp2 = new Temp(inter);
                inter.AddLast(temp2);
                String temp22 = (String)temp2.ejecutar(en, arbol, inter);
                inter.AddLast(new GenCod(temp11,temp22,"","GETSTACK","",""));
                //retorno el temporal que contiene el valor de la variable
                return temp22;
            }
            else
            {
                Form1.error.AppendText("Error, No existe el id " + ide + " lin:" + lin + " col:" + col);
                return null;
            }
        }
    }
}
