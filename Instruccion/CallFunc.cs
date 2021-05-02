using P1.Arbol;
using P1.Generacion;
using P1.Interfaz;
using P1.TS;
using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Instruccion
{
    class CallFunc : Instruc, Expr
    {
        public int lin { get ; set ; }
        public int col { get ; set ; }
        String id;
        LinkedList<Expr> valoresP;

        public CallFunc(String id,LinkedList<Expr> valoresP, int lin, int col)
        {
            this.id = id;
            this.valoresP = valoresP;
            this.lin = lin;
            this.col = col;
        }

       

        public Simb.Tipo getTipo(Entor gen,Entor ent, AST arbol, LinkedList<Instruc>inter)
        {
            Func f = arbol.getFuncion(id);//IDE.getFunction(id);
            if (null != f)
            {
                return f.tipoF;
            }
            else
            {
                Form1.error.AppendText("La func/procedure:" + id + " no existen, lin:" + lin + " col:" + col + "\n");
                return Simb.Tipo.VOID;
            }
        }

        

        public object ejecutar(Entor gen,Entor ent, AST arbol, LinkedList<Instruc> inter)
        {
            return getValImp(gen,ent, arbol, inter);
        }

        public object getValImp(Entor gen,Entor en, AST arbol, LinkedList<Instruc>inter)
        {
            
            Func f = arbol.getFuncion(id);//IDE.getFunction(id);
            //encuentro la funcion
            Simb aux = en.getSimb(f.ambito);
            int contador =0 ;
            //recorro la funcion primero para obtener el tamaño de la funcion
            if (null != f)
            {
                f.setValParam(valoresP);
                if (f.tipoF != Simb.Tipo.VOID) contador = 1;//reservo el espacio para el return

                foreach (Expr e in valoresP)//valParam son de la llamada a funcion 
                {
                    
                    Temp newTemp = new Temp(inter);
                    String newApunt = (String)newTemp.ejecutar(gen, en, arbol, inter);
                    inter.AddLast(newTemp);//agergo el nuevo temporal
                    inter.AddLast(new GenCod("sp", "" + aux.getParam(), "+", newApunt, "", ""));//movimiento del apuntador
                    Temp newTemp2 = new Temp(inter);
                    String newApunt2 = (String)newTemp2.ejecutar(gen, en, arbol, inter);
                    inter.AddLast(newTemp2);//agergo el nuevo temporal
                    inter.AddLast(new GenCod(newApunt, "" + contador, "+", newApunt2, "", ""));//moviendo el nuevo apuntador
                    contador++;
                    //primero coloco en su nueva posicion a los valores de entrada

                    Object resultado = e.getValImp(gen, en, arbol, inter);
                    if (resultado is String || resultado is int || resultado is Double || resultado is Decimal)
                    {
                        inter.AddLast(new GenCod(newApunt2, ""+resultado, "", "STACK", "", ""));
                    }
                    else
                    {
                    }
                }


                //Ahora tengo que mover el apuntador
                inter.AddLast(new GenCod("sp", "" + aux.getParam(), "+", "sp", "", ""));
                inter.AddLast(new GenCod(id + "();\n\n", "", "", "TEXTO", "", ""));
                if (f.tipoF != Simb.Tipo.VOID) { 
                    Temp retorno1 = new Temp(inter);
                    inter.AddLast(retorno1);
                    String retor1 = (String)retorno1.ejecutar(gen, en, arbol, inter);
                    inter.AddLast(new GenCod("sp", "0", "+", retor1, "", ""));
                    Temp retorno2 = new Temp(inter);
                    inter.AddLast(retorno2);
                    String retor2 = (String)retorno2.ejecutar(gen, en, arbol, inter);

                    inter.AddLast(new GenCod(retor1, retor2, "", "GETSTACK", "", ""));

                    inter.AddLast(new GenCod("sp", "" + aux.getParam(), "-", "sp", "", ""));

                    return retor2;
                }
                inter.AddLast(new GenCod("sp", "" + aux.getParam(), "-", "sp", "", ""));
                //Object resul = f.ejecutar(gen,en, arbol,inter);

                //si viene un return dentro

                /*   if (resul is ExitR)
                    {
                        return null;
                    }
                    else
                    {
                        return resul;
                    }*/
            }
            else
            {
                Form1.error.AppendText("La func/procedure " + id + " no existen, lin:" + lin + " col: " + col + "\n");
            }
            return null;
        }
    }
}
