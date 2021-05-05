using P1.Arbol;
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

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            Object valor = val.getValImp(gen,en, arbol, inter);
            if (valor.ToString().ToLower() == "true") valor = 1;
            else if (valor.ToString().ToLower() == "false") valor = 0;
            //  Simb.Tipo tipoA = val.getTipo(en, arbol,inter);

            if (en.buscar(ide))//id encontrada
            {
                Simb actual = en.getSimb(ide);//obtengo el simbolo actual a evaluar
               
                    actual.val = valor;//se asigna el valor antes de ser agregado
                    
                    Temp newTemp = new Temp(inter);//genero el nuevo temporal
                    inter.AddLast(newTemp);//agrego el temporal a la lista de temporales
                    String temp = (String)newTemp.ejecutar(gen,en, arbol, inter);//obtengo el temporal
                    

                bool respuesta = en.buscarActual(ide);
                Instruc temp3 = new Temp(inter);

                String temp33 = (String)temp3.ejecutar(gen, en, arbol, inter);
                if (respuesta == true)
                {
                    inter.AddLast(new GenCod("sp", "" + actual.apuntador, "+", temp, "", ""));
                   // inter.AddLast(new GenCod("sp", "" + simbol.apuntador, "+", temp11, "", ""));

                   // inter.AddLast(new GenCod(temp11, temp22, "", "GETSTACK", "", ""));
                    //retorno el temporal que contiene el valor de la variable
                }
                else
                {
                    inter.AddLast(temp3);
                    Simb simbol2 = en.getSimb(actual.ambito);
                    if(simbol2.ambito=="Global") inter.AddLast(new GenCod("", "" + "0", "", temp, "", ""));
                    else inter.AddLast(new GenCod("sp", "" + simbol2.param, "-", temp, "", ""));
                    String temp2 = temp;
                    temp = temp33;
                    inter.AddLast(new GenCod(temp2, "" + actual.apuntador, "+", temp, "", ""));
                }


                if (actual.tip == Simb.Tipo.STRING && valor is String[])
                {
                    Object[] val = valor as object[];
                    valor = val[0];
                    int cont = 0;

                    foreach (P apunta in P.Stack0)
                    {
                        System.Diagnostics.Debug.WriteLine("la salida en el stack " + apunta.val + "tama;O" + apunta.punt);
                        if (cont == actual.apuntador + P.apu)//hacia donde apunta en la pila RELATIVAMENTE Y el valor actual del ambito
                        {
                            apunta.val = Convert.ToDecimal(val[1]);//modifico el valor en la pila para que apunte a donde esta el heap
                        }
                        cont++;
                    }
                    inter.AddLast(new GenCod(temp, "" + valor, "", "STACK", "", ""));

                }
                else if (actual.tip == Simb.Tipo.STRING )
                {
                    

                    inter.AddLast(new GenCod(temp, "-1", "", "STACK", "", ""));
                    Temp heapT = new Temp(inter);//nuevo temporal
                    inter.AddLast(heapT);//temporal
                    String tempHeap = (String)heapT.ejecutar(gen, en, arbol, inter);//string temporal
                    inter.AddLast(new GenCod(tempHeap, "", "", "SETHEAP", "", ""));//temporal = hp
                                                                                   //Heap nuevo0 = new Heap(0);
                    int heapp = P.Heap.Count;//obtengo tama;o actual del heap
                    int cont = 0;

                    
                    foreach(P apunta in P.Stack0)
                    {   
                        System.Diagnostics.Debug.WriteLine("la salida en el stack " + apunta.val + "tama;O" + apunta.punt);
                        if(cont == actual.apuntador+P.apu)//hacia donde apunta en la pila RELATIVAMENTE Y el valor actual del ambito
                        {
                            apunta.val = heapp;
                        }
                        cont++;
                    }
                    System.Diagnostics.Debug.WriteLine("el valor del heap en para el stack ahora deberia ser:" + heapp);
                    //separo la cadena para asignar al heap
                    string salida = valor.ToString() + "$";
                    byte[] byteArray = Encoding.ASCII.GetBytes(salida);
                    foreach (byte caracter in byteArray)
                    {
                        inter.AddLast(new GenCod("" + caracter, "", "", "HEAP", "", ""));//Heap[hp]=caracter;
                                                                                         // P.Heap0[heapp] = caracter;
                        P.Heap.AddLast(caracter);//agrego valor al heap
                        inter.AddLast(new GenCod("hp", "1", "+", "hp", "", ""));//hp = hp +1

                    }
                    Temp newTemp2 = new Temp(inter);
                    inter.AddLast(newTemp2);//agrego nuevo temporal a la lista de temporales
                    String temp2 = (String)newTemp2.ejecutar(gen, en, arbol, inter);
                    Instruc temp4 = new Temp(inter);

                    String temp44 = (String)temp4.ejecutar(gen, en, arbol, inter);
                    if (respuesta == true)
                    {
                        Simb simbol2 = en.getSimb(actual.ambito);
                        if (simbol2.ambito == "Global") inter.AddLast(new GenCod("", "" + "0", "", temp, "", ""));
                        else inter.AddLast(new GenCod("sp", "" + actual.apuntador, "+", temp2, "", ""));
                        inter.AddLast(new GenCod(temp2, tempHeap, "", "STACK", "", "")); //Stack[temp2]=tempHeap
                    }
                    else
                    {
                        inter.AddLast(temp4);
                        Simb simbol2 = en.getSimb(actual.ambito);
                        inter.AddLast(new GenCod("sp", "" + simbol2.param, "-", temp2, "", ""));
                        String temp02 = temp2;
                        temp2 = temp44;
                        inter.AddLast(new GenCod(temp02, "" + actual.apuntador, "+", temp2, "", ""));

                        inter.AddLast(new GenCod(temp2, tempHeap, "", "STACK", "", "")); //Stack[temp2]=tempHeap

                    }

                    

                    foreach (P apunta in P.Stack0)
                    {
                        System.Diagnostics.Debug.WriteLine("la salida en el stack 222" + apunta.val + "tama;O " + apunta.punt);
                    }


                }
                else
                {
                    //inter.AddLast(nuevo);//el valor realmente no importa agrego  en el stack
                    //P.Stack0.AddLast(0);
                    inter.AddLast(new GenCod(temp, "" + valor, "", "STACK", "", ""));
                }

                en.actualizar(actual.id, actual);//se ha actualizado el valor de la variable

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
