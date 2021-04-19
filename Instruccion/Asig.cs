﻿using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Asig : Instruc
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }
        private String ide;
        private Expr val;

        public Asig(String ide, Expr val, int lin, int col)
        {
            this.ide = ide;
            this.val = val;
            this.lin = lin;
            this.col = col;
        }

        public object ejecutar(Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            Object valor = val.getValImp(en, arbol, inter);
          //  Simb.Tipo tipoA = val.getTipo(en, arbol,inter);

            if (en.buscar(ide))//id encontrada
            {
                Simb actual = en.getSimb(ide);//obtengo el simbolo actual a evaluar
                //if (actual.getTipo(en, arbol,inter) == tipoA)//valida que el tipo obtenido sea el mismo que el tipo del valor
               // {
                    actual.val = valor;//se asigna el valor antes de ser agregado
                    
                    Temp newTemp = new Temp(inter);//genero el nuevo temporal
                    inter.AddLast(newTemp);//agrego el temporal a la lista de temporales
                    String temp = (String)newTemp.ejecutar(en, arbol, inter);//obtengo el temporal
                    inter.AddLast(new GenCod("sp", "" + actual.apuntador, "+", temp, "", ""));
                    inter.AddLast(new GenCod(temp, "" + valor, "", "STACK", "", ""));
                    en.actualizar(actual.id, actual);//se ha actualizado el valor de la variable
               /* }
                else
                {

                    Form1.error.AppendText("Error en Asignar, id " + actual.id + " posee otro tipo de de datos , lin:" + val.lin + " col:" + val.col);
                    return false;//no se asigno nada 
                }*/
            }
            else
            {
                Form1.error.AppendText("Error, No existe el id " + ide + " lin:" + lin + " col:" + col);
                return null;
            }
            return null;
        }
    }
}
