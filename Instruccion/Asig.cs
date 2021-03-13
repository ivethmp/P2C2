using P1.Arbol;
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

        public object ejecutar(Entor en, TabS tabS)
        {
            Object valor = val.getValImp(en, tabS);
            Simb.Tipo tipoA = val.getTipo(en, tabS);

            if (en.buscar(ide))//id encontrada
            {
                Simb actual = en.getSimb(ide);//obtengo el simbolo actual a evaluar
                if (actual.getTipo(en,tabS) == tipoA)//valida que el tipo obtenido sea el mismo que el tipo del valor
                {
                    actual.val = valor;//se asigna el valor antes de ser agregado
                    en.actualizar(actual.id, actual);//se ha actualizado el valor de la variable
                }
                else
                {

                    Form1.error.AppendText("Error en Asignar, id " + actual.id + " posee otro tipo de de datos , lin:" + val.lin + " col:" + val.col);
                    return false;//no se asigno nada 
                }
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
