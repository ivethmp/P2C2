using P1.Arbol;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Declara : Instruc
    {
        LinkedList<Simb> listaID;
        private Simb.Tipo tipoD;
        private Expr val;//el valor al inicializar una variable 
        public int lin { get; set ; }
        public int col { get ; set ; }


        /*public Declara(LinkedList<Simb> listaID, Simb.Tipo tipoD, int lin, int col)
        {
            this.listaID = listaID;
            this.tipoD = tipoD;
            this.lin = lin;
            this.col = col;
            
        }*/
        public Declara(LinkedList<Simb> listaID, Simb.Tipo tipoD, Expr val, int lin, int col)
        {
            this.listaID = listaID;
            this.tipoD = tipoD;
            this.val = val;
            this.lin = lin;
            this.col = col;
        }

        public object ejecutar(Entor en, TabS tabS)
        {
            object valor;
            Simb.Tipo tipoA;
            if (val != null)//se verifica si la variable esta inicializada o no y el tipo 
            {
                valor = val.getValImp(en, tabS);
                tipoA = val.getTipo(en, tabS);
            }
            else
            {//como no esta inicializada se toma el tipo de variable tal cual y se inicializa
                tipoA = tipoD;
                if (tipoD == Simb.Tipo.BOOL)
                    valor = false;
                else if (tipoD == Simb.Tipo.INT)
                    valor = 0;
                else if (tipoD == Simb.Tipo.REAL)
                    valor = 0.0;
                else if (tipoD == Simb.Tipo.STRING)
                    valor = "";
                else valor = null;// en caso que sea type object array u otro
            }

            foreach(Simb sim in listaID)
            {
                lin = sim.lin;
                col = sim.col;
                if (!en.buscar(sim.id))
                {
                    if(tipoD == tipoA)//esta declarado segun el tipo que se inicializo
                    {
                        sim.val = valor;//se asigna el valor antes de ser agregado
                        en.Agregar(sim.id, sim);
                    }
                    else
                    {
                        
                        Form1.errores.AppendText("Error en Declaracion, id " + sim.id + " se inicializo con el tipo incorrecto, lin:" + lin + " col:" + col);
                        return false;//no se declaro nada
                    }
                    
                }
                else
                {
                    Form1.errores.AppendText("Error, id" + sim.id + " ya fue declarado, linea:" + lin + " columna:" + col);
                    return false;
                }
            }
            return null;
        }
    }
}
