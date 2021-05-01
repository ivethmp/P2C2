using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class Declara : Instruc
    {
        public LinkedList<Simb> listaID;
        private Simb.Tipo tipoD;
        public Expr val;//el valor al inicializar una variable 
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

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            bool inicializado = false;
            object valor;
            Simb.Tipo tipoA;
            if (val != null)//se verifica si la variable esta inicializada o no y el tipo 
            {
                inicializado = true;
                valor = val.getValImp(gen,en, arbol, inter);
                tipoA = val.getTipo(gen,en, arbol,inter);
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
                if (!en.buscarActual(sim.id))
                {
                    if(tipoD == tipoA)//esta declarado segun el tipo que se inicializo
                    {
                        sim.val = valor;//se asigna el valor antes de ser agregado
                        //AGREGO EL VALOR DE LA VARIABLE PARA ESTABLECER SU POSICION RELATIVA O APUNTADOR EN LA PILA
                        Stack nuevo = new Stack(0);
                        int apuntador = (int)nuevo.ejecutar(gen,en, arbol, inter);
                        inter.AddLast(nuevo);//el valor realmente no importa 
                        sim.apuntador = apuntador;
                        en.Agregar(sim.id, sim);
                        gen.Agregar(sim.id, sim);
                        Simb ambiGen = en.getSimb(sim.ambito);
                        ambiGen.param = ambiGen.param + 1;
                        en.actParam(sim.ambito, ambiGen);
                        gen.actParam(sim.ambito, ambiGen);

                        if(inicializado == true)//quiere decir que se inicializo la variable con un valor concreto y no con 0, por lo que debo guardar el valor en la pila 
                        {
                            Temp newTemp = new Temp(inter);
                            inter.AddLast(newTemp);//agrego el temporal a la lista de temporales
                            String temp = (String)newTemp.ejecutar(gen,en, arbol, inter);
                            inter.AddLast(new GenCod("sp", "" + apuntador, "+", temp, "", ""));
                            inter.AddLast(new GenCod(temp, "" + valor, "", "STACK", "", ""));
                        }
                    }
                    else
                    {
                        
                        Form1.error.AppendText("Error en Declaracion, id " + sim.id + " se inicializo con el tipo incorrecto, lin:" + lin + " col:" + col);
                        return false;//no se declaro nada
                    }
                    
                }
                else
                {
                    Form1.error.AppendText("Error, id" + sim.id + " ya fue declarado, linea:" + lin + " columna:" + col);
                    return false;
                }
            }
            return null;
        }
    }
}
