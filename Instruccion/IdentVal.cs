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

        public Simb.Tipo getTipo(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
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

        public object getValImp(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            if (en.buscar(ide))
            {
                Simb simbol = en.getSimb(ide);
                bool respuesta = en.buscarActual(ide);
                
                //genero un nuevo temporal 
                Instruc temp1 = new Temp(inter);
                inter.AddLast(temp1);//agrego el temporal
                String temp11 = (String)temp1.ejecutar(gen,en, arbol, inter);
                Instruc temp2 = new Temp(inter);
                inter.AddLast(temp2);
                String temp22 = (String)temp2.ejecutar(gen, en, arbol, inter);
                //asigno el temporal con su apuntador y posicion relativa
                if (respuesta == true)
                {
                    inter.AddLast(new GenCod("sp", ""+ simbol.apuntador, "+", temp11, "", ""));
                   
                    inter.AddLast(new GenCod(temp11,temp22,"","GETSTACK","",""));
                    //retorno el temporal que contiene el valor de la variable
                }
                else
                {
                    Instruc temp3 = new Temp(inter);
                    inter.AddLast(temp3);
                    
                    String temp33 = (String)temp3.ejecutar(gen, en, arbol, inter);
                    String temp02 = temp22;
                    temp22 = temp33;
                    Simb simbol2 = en.getSimb(simbol.ambito);
                    if (simbol2.ambito == "Global") inter.AddLast(new GenCod("", "" + "0", "", temp11, "", ""));
                    else inter.AddLast(new GenCod("sp", "" + simbol2.param, "-", temp11, "", ""));
                    inter.AddLast(new GenCod(temp11, "" + simbol.apuntador, "+", temp02, "", ""));
                    inter.AddLast(new GenCod(temp02, temp22, "", "GETSTACK", "", ""));
                }
                if (simbol.tip == Simb.Tipo.STRING)
                {

                    int cont = 0;
                    int apuntId = simbol.apuntador;
                    decimal valHeap = 0;
                    foreach (P apunta in P.Stack0)
                    {
                        System.Diagnostics.Debug.WriteLine("la salida en el stack " + apunta.val + "tama;O" + apunta.punt);
                        if (cont == apuntId + P.apu)//hacia donde apunta en la pila RELATIVAMENTE Y el valor actual del ambito
                        {
                            valHeap = apunta.val;//valor al que apunta al heap
                        }
                        cont++;
                    }
                    String[] salida = new string[2];
                    salida[0] = temp22;
                    salida[1] = ""+valHeap;

                    

                    return salida;
                }
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
