using P1.Arbol;
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
            Object condicional = cond.getValImp(gen, en, arbol, inter);
            LinkedList<Instruc> etiquetas = new LinkedList<Instruc>();
            object salida =null;
            if (condicional is string)//significa que es un temporal y otra cosa disti
            {
                
                Etiq eti = new Etiq(inter, "true");
                inter.AddLast(eti);
                etiquetas.AddLast(eti);

                Etiq etiF = new Etiq(inter, "false");
                inter.AddLast(etiF);
                etiquetas.AddLast(etiF);

                inter.AddLast(new GenCod("", condicional+"","", "UNO", (String)eti.ejecutar(gen, en, arbol, inter), (String)eti.ejecutar(gen, en, arbol, inter)));
            }
            else
            {
                etiquetas = (LinkedList<Instruc>)condicional;//retorna lista con etiquetas verdaderas y falsas
            }
           
            String etiqV= "";
            String etiqF = "";
            String gotoF = "";
                foreach (Etiq eti in etiquetas)
                {
                    if (eti.cond == "true") etiqV = etiqV + "L" + eti.numero +":\n";
                if (eti.cond == "false")
                {
                    etiqF = etiqF + "L" + eti.numero + ":\n";
                    gotoF = etiqF + "L" + eti.numero;
                }
                 }
            inter.AddLast(new GenCod("", "", "", "IF", etiqV, ""));
            Instruc salto = new Etiq(inter, "");//genero nueva etiqueta de salto al finalizar instruccion del if
            //Obtengo la etiqueta, pero no genero su codigo
            String saltos = etiqF;
            Entor tabLoc = new Entor(en);
                foreach (Instruc ins in instrucciones)
                {
                    
                    if (ins is Continue || ins is Break)
                    {
                        inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                    //salida = "break";
                    }
                ins.ejecutar(gen, tabLoc, arbol, inter);//si no hay return, break o continue, simplemente se ejecutan las instrucciones dentro del if
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
                        if (eti.cond == "true") etiqV2 = etiqV2 + "L" + eti.numero +":\n";
                        if (eti.cond == "false") etiqF2 = etiqF2 + "L" + eti.numero  +":\n";
                    }
                    inter.AddLast(new GenCod("", "", "", "IF", etiqV2 , ""));

                    Entor tabLoc2 = new Entor(en);
                    foreach (Instruc ins in instrucciones)
                    {
                       
                        if (ins is Continue || ins is Break)
                        {
                            inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                        }
                        ins.ejecutar(gen, tabLoc2, arbol, inter);//si no hay return, break o continue, simplemente se ejecutan las instrucciones dentro del if
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
                inter.AddLast(new GenCod("", "", "", "IF", "", etiqF+ ":\n"));//seria como el else si no se cumple la condicion 
            }else
            {
               // inter.AddLast(new GenCod("", "", "", "IF", "", etiqF));//seria como el else si no se cumple la condicion 
            }


            if (instElse != null)//significa que es un else
            {                                                       // Entor tabLoc = new Entor(en);
                foreach (Instruc ins in instElse)
                    {
                        
                        if (ins is Continue || ins is Break)
                        {
                            inter.AddLast(new GenCod("", "", "", "GOTO", saltos, ""));
                        }
                    ins.ejecutar(gen, tabLoc, arbol, inter);
                }
                }
            //}
            inter.AddLast(new GenCod("", "", "", "IF", saltos+":\n", ""));
            return null;
        }
    }
 }

