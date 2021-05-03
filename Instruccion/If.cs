﻿using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class If : Instruc
    {
        public int lin { get ; set; }
        public int col { get ; set  ; }

        public Expr cond;
        public LinkedList<Instruc> instrucciones;
        private LinkedList<Instruc> instElse;
        private LinkedList<If> listaIfElse;

        public If(Expr cond, LinkedList<Instruc> instrucciones, LinkedList<Instruc> instElse, LinkedList<If> listaIfElse, int lin, int col)
        {
            this.cond = cond;
            this.instrucciones = instrucciones;
            this.instElse = instElse;
            this.listaIfElse = listaIfElse;
            this.lin = lin;
            this.col = col;

        }
        

        public object ejecutar(Entor gen,Entor en, AST arbol, LinkedList<Instruc> inter)
        {
            //se evalua la condicion// true o false
            LinkedList<Instruc> etiquetas = (LinkedList<Instruc>)cond.getValImp(gen,en, arbol, inter);//retorna lista con etiquetas verdaderas y falsas
            String etiqV= "";
            String etiqF = "";
            
                foreach (Etiq eti in etiquetas)
                {
                    if (eti.cond == "true") etiqV = etiqV + "L" + eti.numero+":\n";
                    if (eti.cond == "false") etiqF = etiqF + "L" + eti.numero + ":\n";
                 }
            inter.AddLast(new GenCod("", "", "", "IF", etiqV,""));
            Instruc salto = new Etiq(inter, "");//genero nueva etiqueta de salto al finalizar instruccion del if
            //Obtengo la etiqueta, pero no genero su codigo
            String saltos = etiqF;
            Entor tabLoc = new Entor(en);
                foreach (Instruc ins in instrucciones)
                {
                    ins.ejecutar(gen,tabLoc, arbol, inter);//si no hay return, break o continue, simplemente se ejecutan las instrucciones dentro del if
                    if (ins is Continue || ins is Break)
                    {
                        inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                    }

            }

            //else
            //{
            if (listaIfElse != null)//quiere decir que hay if else
            {
                
                //agrego el salto, para luego generar la etiqueta de condicion del if else
                inter.AddLast(salto);
                saltos = (String)salto.ejecutar(gen,en, arbol, inter);
                inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                inter.AddLast(new GenCod("", "", "", "IF", "", etiqF));//seria como el else si no se cumple la condicion 

                foreach (If ifElse in listaIfElse)
                {
                    //ifElse.ejecutar(en, arbol, inter);
                    LinkedList<Instruc> etiquetas2 = (LinkedList<Instruc>)ifElse.cond.getValImp(gen,en, arbol, inter);//retorna lista con etiquetas verdaderas y falsas
                    String etiqV2 = "";
                    String etiqF2 = "";

                    foreach (Etiq eti in etiquetas2)
                    {
                        if (eti.cond == "true") etiqV2 = etiqV2 + "L" + eti.numero + ":\n";
                        if (eti.cond == "false") etiqF2 = etiqF2 + "L" + eti.numero + ":\n";
                    }
                    inter.AddLast(new GenCod("", "", "", "IF", etiqV2, ""));

                    Entor tabLoc2 = new Entor(en);
                    foreach (Instruc ins in instrucciones)
                    {
                        ins.ejecutar(gen,tabLoc2, arbol, inter);//si no hay return, break o continue, simplemente se ejecutan las instrucciones dentro del if
                        if (ins is Continue || ins is Break)
                        {
                            inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                        }
                    }
                    //agrego el salto en cada if ya que indica que se cumplio esta condicion y debe salir del if
                    inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                    inter.AddLast(new GenCod("", "", "", "IF", "", etiqF2));//seria como el else si no se cumple la condicion 
                    /* if((bool)ifElse.cond.getValImp(en,arbol, inter))//se evaluan las condiciones de cada if 
                     {
                          tabLoc = new Entor(en);//nueva tabla para el if
                         foreach(Instruc ins in ifElse.instrucciones)//instrucciones dentro del if actual
                         {
                             /*if (ins is Break || ins is Continue)
                             {
                                 return ins;
                             }
                             else if (ins is Return)
                             {
                                 return ins.ejecutar(tabLoc, arbol);
                             }*/
                    /*     ins.ejecutar(tabLoc, arbol, inter);
                      }
                  }*/
                }


            }
            else if (instElse != null)//no venia else if solo else
            {
                inter.AddLast(salto);
                saltos = (String)salto.ejecutar(gen,en, arbol, inter);
                inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                inter.AddLast(new GenCod("", "", "", "IF", "", etiqF));//seria como el else si no se cumple la condicion 
            }else
            {
               // inter.AddLast(new GenCod("", "", "", "IF", "", etiqF));//seria como el else si no se cumple la condicion 
            }


            if (instElse != null)//significa que es un else
            {                                                       // Entor tabLoc = new Entor(en);
                foreach (Instruc ins in instElse)
                    {
                        ins.ejecutar(gen,tabLoc, arbol, inter);
                        if (ins is Continue || ins is Break)
                        {
                            inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                        }
                }
                }
            //}
            inter.AddLast(new GenCod("", "", "", "IF", saltos+":\n", ""));
            return null;
        }
    }
 }

