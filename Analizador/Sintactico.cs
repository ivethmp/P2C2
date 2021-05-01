using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;
using P1.Interfaz;
using P1.TS;
using P1.Arbol;
using P1.Instruccion;
using System.Runtime.Serialization;
using Irony;
using P1.Reportes;
using P1.Generacion;

namespace P1.Analizador
{
    class Sintactico
    {
       
        

        public AST arb { get; set; }

        public void Analizar( String cad)
        {
            System.Diagnostics.Debug.WriteLine("enter a analizar0");
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cad);
            ParseTreeNode raiz = arbol.Root;
            bool error = arbol.HasErrors();
            
            
            if (arbol.HasErrors())
            {
                Form1.error.Text = (BuildParsingErrorMessage(arbol.ParserMessages));
                
                return;
                //throw new InvalidOperationException(BuildParsingErrorMessage(arbol.ParserMessages));
            }
            if (raiz == null)
            {
                System.Diagnostics.Debug.WriteLine("no tiene nada el arbol");
                return;
            }

                LinkedList<Instruc> AST = instrucs(raiz,"Global-"+ raiz.ChildNodes[1].Token.Text);
                arb = new Arbol.AST(AST);
           
            Entor entorno = new Entor(null);
             Entor General = new Entor(null);
            LinkedList<Instruc> inter = new LinkedList<Instruc>();
            Simb inicio = new Simb("Global-" + raiz.ChildNodes[1].Token.Text,Simb.Tipo.VOID, "Global-"+ raiz.ChildNodes[1].Token.Text, "Procedimiento", 0, 0, raiz.ChildNodes[1].Token.Location.Line, raiz.ChildNodes[1].Token.Location.Column);
            entorno.Agregar("Global-" + raiz.ChildNodes[1].Token.Text, inicio);
            General.Agregar("Global-" + raiz.ChildNodes[1].Token.Text, inicio);
            string Encabezado = "#include <stdio.h>\n\n" +
                "Float Heap[100000];\n" +
                "Float Stack[100000];\n\n" +
                "Float sp;\n" +
                "Float hp;\n\n";
            String temporales = "";
            

            

            foreach (Instruc inst in AST)
            {
                if (!(inst is Func))
                {
                    inst.ejecutar(General,entorno, arb, inter);
                }
            }
            RepTabS reporte = new RepTabS();
            reporte.GenHTML(entorno,"TablaSimb-Global");
            RepTabS reporte2 = new RepTabS();
            reporte.GenHTML(General,"TablaSimb-General");
            //GenGraphviz g = new GenGraphviz();
            //g.graficar(raiz);
            GenG.Run(GenGraphviz.getDot(raiz));


            foreach (Instruc inst in inter)
            {
                if (inst is Temp)
                {
                    temporales = temporales + (String)inst.ejecutar(General,entorno, arb, inter) + ",";
                }
            }
            if (temporales != "")
            {
                temporales = "Float " + temporales.TrimEnd(',') + ";\n\n";
            }
            //agrego encabezado y temporales en la salida de texto
            Form1.salir.AppendText(Encabezado + temporales);

            foreach (Instruc inst in inter)
            {
                if( inst is GenCod)
                {
                    inst.ejecutar(General,entorno, arb, inter);
                }
            }
            


        }
        private static string BuildParsingErrorMessage(LogMessageList messages)
        {//este metodo obtiene los mensajes de error sintactico desde la gramatica
            var sb = new StringBuilder();
            sb.AppendLine("Errores de sintaxis:");
            messages.ForEach(msg => sb.AppendLine($"\t{msg.Message + " lin:" + msg.Location.Line + " col:"+ msg.Location.Column}"));
            return sb.ToString();
        }

        public LinkedList<Instruc> instrucs( ParseTreeNode nodoA, String ambito)
        {
            LinkedList<Instruc> lista = new LinkedList<Instruc>();
            // if (nodoA.ChildNodes[3].Term.Name == "BLOQ_VAR-GLOB")
            //bloque de var en pascal
           
            if (nodoA.ChildNodes[3].ChildNodes.Count >0)
                {
                    foreach (ParseTreeNode son in nodoA.ChildNodes[3].ChildNodes)
                    {
                         bloques(son, lista, ambito);
                    }
                }
           
            //bloque de procedimiento o funcion 
            if (nodoA.ChildNodes[4].ChildNodes.Count > 0)
            {
                foreach(ParseTreeNode son in nodoA.ChildNodes[4].ChildNodes)
                {
                      bloques(son, lista,ambito);
                }
            }
            //bloque de begin del main en pascal
            if(nodoA.ChildNodes[5].ChildNodes.Count > 0)
            {
                 bloques(nodoA.ChildNodes[5], lista,ambito);
            }
            //bloque de procedimiento o funcion, si viene despues del begin end del metodo principal
            if (nodoA.ChildNodes[7].ChildNodes.Count > 0)
            {
                foreach (ParseTreeNode son in nodoA.ChildNodes[7].ChildNodes)
                {
                     bloques(son, lista,ambito);
                }
            }
            return lista;

        }

        private LinkedList<Instruc> bloques(ParseTreeNode bloque, LinkedList<Instruc> lista, String ambito)
        {
            if (bloque.ChildNodes[1].Term.Name == "BLOQ_VAR")
            {
                
                foreach (ParseTreeNode declarar in bloque.ChildNodes[1].ChildNodes)
                {
                    System.Diagnostics.Debug.WriteLine("entre al for");//le mando el tipo de variable global
                    lista.AddLast(instruc(declarar, bloque.ChildNodes[0].ChildNodes[0].Token.Text, ambito));
                }
                return lista;
            }else if (bloque.ChildNodes[1].Term.Name == "BLOQ_BEGIN")
            {
                foreach (ParseTreeNode instbegin in bloque.ChildNodes[1].ChildNodes)
                {
                    lista.AddLast(instruc(instbegin.ChildNodes[0],"", ambito));
                }
                return lista;
            }else if (bloque.ChildNodes[1].Term.Name == "BLOQ_FUNC-PROC")
            {
                lista.AddLast(instruc(bloque,"Funcion",ambito));
                return lista;
            }
            return null;
        }
        private Simb.Tipo tipoVar(ParseTreeNode nodoB)
        {
            String tipo = nodoB.Token.Text;
            switch (tipo.ToLower())
            {
                case "integer": return Simb.Tipo.INT;
                case "string": return Simb.Tipo.STRING;
                case "real": return Simb.Tipo.REAL;
                case "boolean": return Simb.Tipo.BOOL;
                default: return Simb.Tipo.INVAL;
            }
        }
        private Instruc instruc( ParseTreeNode nodoA, String VarG, String ambito)
        {
            //(1) ES EL BLOQUE VAR/BEGIN (0) YA ES LA INSTRUCCION EN SI
            //string tokenOp = nodoA.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            string tokenOp = nodoA.ToString().Split(' ')[0];
            //ParseTreeNode actual = nodoA.ChildNodes.ElementAt(0);
            //ParseTreeNode actual2 = nodoA.ChildNodes.ElementAt(1).ChildNodes.ElementAt[0];

            
            System.Diagnostics.Debug.WriteLine("el token es " +tokenOp);
            //System.Diagnostics.Debug.WriteLine("el token es 2 " + actual2);
            switch (tokenOp.ToLower())
            {
                case "declara":
                    {
                        LinkedList<Simb> ides = new LinkedList<Simb>();
                        Simb.Tipo tipoA;
                        if (nodoA.ChildNodes[0].Term.Name == "LISTDECLARA")//lista de ides de declaraciones
                        {
                            tipoA = tipoVar(nodoA.ChildNodes[2].ChildNodes[0]);
                            System.Diagnostics.Debug.WriteLine("el valor en lista es");
                            foreach (ParseTreeNode son in nodoA.ChildNodes[0].ChildNodes)
                            {
                                System.Diagnostics.Debug.WriteLine("el valor es" + son.Token.Text,"el ambito es"+ambito);
                                Simb simbol = new Simb(son.Token.Text, tipoA, ambito, "Variable", 0, 0, son.Token.Location.Line, son.Token.Location.Column);
                                ides.AddLast(simbol);
                            }

                            return (new Declara(ides, tipoA, null, 0, 0));
                        }
                        else // solo es una variable declarada
                        {
                            System.Diagnostics.Debug.WriteLine("entre a declara 1");
                            //System.Diagnostics.Debug.WriteLine("el ambito es" + ambito);
                            tipoA = tipoVar(nodoA.ChildNodes[2].ChildNodes[0]);
                            Simb simbol = new Simb(nodoA.ChildNodes[0].Token.Text, tipoA,ambito, "Variable", 0, 0, nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                            ides.AddLast(simbol);
                            if (nodoA.ChildNodes.Count == 3)//significa que no tiene valor
                                return new Declara(ides, tipoA, null, 0, 0);
                            else//quiere decir que tiene valor inicializado
                            {
                                return new Declara(ides, tipoA, expresion(nodoA.ChildNodes[4], VarG, ambito), 0, 0);
                            }

                        }

                    }



                case "asigna":
                    {
                        return new Asig(nodoA.ChildNodes[0].Token.Text, expresion(nodoA.ChildNodes[2], VarG, ambito), 0, 0);
                    }
                case "writel":
                    {//El primer if es de writeln
                        /*Expr valor;
                        if (nodoA.ChildNodes[2].ChildNodes.Count > 1)
                        {
                            valor = expresion(nodoA.ChildNodes[2]);
                        }else valor = expresion(nodoA.ChildNodes[2]);*/
                        if (nodoA.ChildNodes[0].ChildNodes[0].Term.Name == "writeln")
                        {//significa que es un salto de linea 

                            return new Print(expresion(nodoA.ChildNodes[2], VarG, ambito), nodoA.ChildNodes[1].Token.Location.Line, nodoA.ChildNodes[1].Token.Location.Column, true);
                        }//significa que es un write sin salto de linea por lo que la bandera es false
                        return new Print((Expr)expresion(nodoA.ChildNodes[2], VarG, ambito), nodoA.ChildNodes[1].Token.Location.Line, nodoA.ChildNodes[1].Token.Location.Column, false);
                        //(Expr)analizr(nodoA, linea,columna)
                    }
                // return new Print(expresion_cadena(actual.ChildNodes.ElementAt(2)));
                case "call_func-proc":
                    {
                        LinkedList<Expr> valParam = new LinkedList<Expr>();
                        if (nodoA.ChildNodes[2].ChildNodes.Count > 0)//significa que tiene parametros de entrada
                        {

                            foreach (ParseTreeNode hijo in nodoA.ChildNodes[2].ChildNodes)
                            {
                                valParam.AddLast(expresion(hijo, VarG, ambito));
                            }
                            return new CallFunc(nodoA.ChildNodes[0].Token.Text, valParam, nodoA.ChildNodes[1].Token.Location.Line, nodoA.ChildNodes[1].Token.Location.Column);
                        }
                        return new CallFunc(nodoA.ChildNodes[0].Token.Text, valParam, nodoA.ChildNodes[1].Token.Location.Line, nodoA.ChildNodes[1].Token.Location.Column);

                    }
                case "bloq_while":
                    {
                        Expr cond = expresion(nodoA.ChildNodes[1], VarG, ambito);
                        LinkedList<Instruc> list = new LinkedList<Instruc>();
                        return new While(cond, bloques(nodoA.ChildNodes[3], list, ambito), nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                    }
                case "bloq_for":
                    {
                        Instruc Asignar = new Asig(nodoA.ChildNodes[1].ChildNodes[0].Token.Text, expresion(nodoA.ChildNodes[1].ChildNodes[2], VarG, ambito), 0, 0);

                        Expr final = expresion(nodoA.ChildNodes[3], VarG, ambito);
                        LinkedList<Instruc> list = new LinkedList<Instruc>();
                        return new For(Asignar, nodoA.ChildNodes[1].ChildNodes[0].Token.Text, final, bloques(nodoA.ChildNodes[5], list, ambito), nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                    }
                case "bloq_repeat":
                    {
                        LinkedList<Instruc> list = new LinkedList<Instruc>();
                        return new Repeat(bloques(nodoA, list, ambito), expresion(nodoA.ChildNodes[3], VarG, ambito), nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                    }

                case "bloq_case":
                    {
                        Expr valor1 = expresion(nodoA.ChildNodes[1], VarG, ambito);
                        LinkedList<Case_Instr> listCase = new LinkedList<Case_Instr>();
                        LinkedList<Instruc> instElse = new LinkedList<Instruc>();
                        foreach (ParseTreeNode caso in nodoA.ChildNodes[3].ChildNodes)
                        {
                            LinkedList<Instruc> list = new LinkedList<Instruc>();
                            listCase.AddLast(new Case_Instr(expresion(caso.ChildNodes[0],VarG,ambito),bloques(caso.ChildNodes[2],list,ambito),caso.ChildNodes[1].Token.Location.Line,caso.ChildNodes[1].Token.Location.Column));
                        }
                        if(nodoA.ChildNodes[5].ChildNodes.Count > 1)
                        {
                            return new Case(valor1, listCase, bloques(nodoA.ChildNodes[5].ChildNodes[0].ChildNodes[1], instElse, ambito), nodoA.ChildNodes[4].Token.Location.Line, nodoA.ChildNodes[4].Token.Location.Column);

                        }
                        return new Case(valor1, listCase,null, nodoA.ChildNodes[4].Token.Location.Line, nodoA.ChildNodes[4].Token.Location.Column);

                    }
                case "bloq_if":
                    {
                        Expr cond = expresion(nodoA.ChildNodes[0].ChildNodes[2], VarG, ambito);
                        LinkedList<Instruc> insIfS = new LinkedList<Instruc>();
                        insIfS = bloques(nodoA.ChildNodes[0].ChildNodes[5], insIfS,ambito);
                        LinkedList<Instruc> insElse = new LinkedList<Instruc>();
                        LinkedList<If> listIfElse = new LinkedList<If>();

                        if (nodoA.ChildNodes[2].ChildNodes.Count == 2)//es if else
                        {
                            if(nodoA.ChildNodes[2].ChildNodes[0].Term.Name == "ELSE")//ELSE SIMPLE
                                insElse = bloques(nodoA.ChildNodes[2].ChildNodes[0].ChildNodes[1], insElse, ambito);
                            else //if else if else if else sin else al final
                            {
                                foreach (ParseTreeNode ifs in nodoA.ChildNodes[2].ChildNodes[0].ChildNodes)
                                {
                                    LinkedList<Instruc> list = new LinkedList<Instruc>();
                                    listIfElse.AddLast(new If(expresion(ifs.ChildNodes[3], VarG, ambito), bloques(ifs.ChildNodes[6], list,ambito), null, null, ifs.ChildNodes[0].Token.Location.Line, ifs.ChildNodes[0].Token.Location.Column));
                                }
                                
                            }
                        }
                        else if(nodoA.ChildNodes[2].ChildNodes.Count == 4)//  if ifelse ifelse else
                        {
                            foreach (ParseTreeNode ifs in nodoA.ChildNodes[2].ChildNodes[0].ChildNodes)
                            {
                                LinkedList<Instruc> list = new LinkedList<Instruc>();
                                listIfElse.AddLast(new If(expresion(ifs.ChildNodes[3], VarG, ambito), bloques(ifs.ChildNodes[6], list,ambito), null, null, ifs.ChildNodes[0].Token.Location.Line, ifs.ChildNodes[0].Token.Location.Column));
                            }//else 
                            insElse = bloques(nodoA.ChildNodes[2].ChildNodes[2].ChildNodes[1], insElse, ambito);
                        }
                        return new If(cond, insIfS, insElse, listIfElse, nodoA.ChildNodes[0].ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].ChildNodes[0].Token.Location.Column);
                    }

                case "exit":
                    {
                        return new ExitR(expresion(nodoA.ChildNodes[2], VarG, ambito),nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                    }
                 case "numero":
                     string tokenValor = nodoA.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                    return null;
                     //return new Declaracion(tokenValor, Simbolo.Tipo.NUMERO);

                 default:
                    {
                        if (nodoA.ChildNodes[0].Token.Text == "function" || nodoA.ChildNodes[0].Token.Text == "procedure")
                        {
                            Simb.Tipo tipoA;
                            if (nodoA.ChildNodes[0].Token.Text == "procedure")//es un procedimiento
                                tipoA = Simb.Tipo.VOID;
                            else    tipoA = tipoVar(nodoA.ChildNodes[3].ChildNodes[0]);//funcion

                            LinkedList<Declara> param = new LinkedList<Declara>();
                            LinkedList<Instruc> ins = new LinkedList<Instruc>();
                            if (nodoA.ChildNodes[1].ChildNodes.Count == 3)//no tiene parametros
                            {
                                if(nodoA.ChildNodes[3].ChildNodes.Count>1) ins = bloques(nodoA.ChildNodes[3], ins,"Funcion-"+ nodoA.ChildNodes[1].ChildNodes[0].Token.Text);//VARIABLES
                                ins = bloques(nodoA.ChildNodes[4], ins, "Funcion-" + nodoA.ChildNodes[1].ChildNodes[0].Token.Text);//BEGIN
                                return new Func(nodoA.ChildNodes[1].ChildNodes[0].Token.Text,param,ins, tipoA, nodoA.ChildNodes[2].Token.Location.Line, nodoA.ChildNodes[2].Token.Location.Column);
                            }
                            else//tiene parametros
                            {
                                
                                foreach (ParseTreeNode parametro in nodoA.ChildNodes[1].ChildNodes[2].ChildNodes)
                                {
                                    if (parametro.ChildNodes.Count == 2)
                                    {
                                        if (parametro.ChildNodes[1].ChildNodes.Count != 3)
                                        {
                                            Form1.error.AppendText("Error de Sintaxis, no se puede inicializar un parametro, lin:" + parametro.ChildNodes[0].ChildNodes[0].Token.Location.Line + " col:" + parametro.ChildNodes[0].ChildNodes[0].Token.Location.Column);
                                            return null;
                                        }
                                        param.AddLast((Declara)instruc(parametro.ChildNodes[1], VarG, "Funcion-" + nodoA.ChildNodes[1].ChildNodes[0].Token.Text));
                                    }
                                    else
                                    {
                                        if (parametro.ChildNodes[0].ChildNodes.Count != 3)
                                        {
                                            Form1.error.AppendText("Error de Sintaxis, no se puede inicializar un parametro, lin:" + parametro.ChildNodes[0].ChildNodes[2].Token.Location.Line + " col:" + parametro.ChildNodes[0].ChildNodes[2].Token.Location.Column);
                                            return null;
                                        }
                                        param.AddLast((Declara)instruc(parametro.ChildNodes[0], VarG, "Funcion-" + nodoA.ChildNodes[1].ChildNodes[0].Token.Text));
                                    }
                                }
                                if(nodoA.ChildNodes[5].ChildNodes.Count >1) ins = bloques(nodoA.ChildNodes[5], ins, "Funcion-" + nodoA.ChildNodes[1].ChildNodes[0].Token.Text);//VARIABLES
                                ins = bloques(nodoA.ChildNodes[6], ins, "Funcion-" + nodoA.ChildNodes[1].ChildNodes[0].Token.Text);//BEGIN

                                return new Func(nodoA.ChildNodes[1].ChildNodes[0].Token.Text, param, ins,tipoA,nodoA.ChildNodes[2].Token.Location.Line, nodoA.ChildNodes[2].Token.Location.Column);
                            }
                            
                        }
                        return null;

                    }
             }
            
        }

        private Expr expresion(ParseTreeNode nodoA, String VarG, String ambito)
        {
          /*  case "exprs":
                    {
                return instruc(nodoA.ChildNodes[0]);
            }
                case "expr_arit":
                    {
                if (nodoA.ChildNodes.Count == 3)
                {
                    Oper.tipOper salid = Oper.getOperador(nodoA.ChildNodes[1].Token.Text);
                    return new Oper((Expr)nodoA.ChildNodes[0], (Expr)nodoA.ChildNodes[2], salid);
                }
            }*/
          switch (nodoA.ChildNodes[0].Term.Name)
            {
                case "CALL_FUNC-PROC":
                    {
                        return (Expr)instruc(nodoA.ChildNodes[0], VarG, ambito);
                    }
                case "EXPR_ARIT":
                    {
                        if(nodoA.ChildNodes[0].ChildNodes.Count == 3)
                        {
                            //return new Oper((Expr)nodoA.ChildNodes[0].ChildNodes[0], (Expr)nodoA.ChildNodes[0].ChildNodes[2], Oper.getOperador(nodoA.ChildNodes[0].ChildNodes[1].Token.Text));
                            return new Oper((Expr)expresion(nodoA.ChildNodes[0].ChildNodes[0], VarG, ambito), (Expr)expresion(nodoA.ChildNodes[0].ChildNodes[2], VarG, ambito), Oper.getOperador(nodoA.ChildNodes[0].ChildNodes[1].Token.Text));
                        }//else if (nodoA.ChildNodes.Count == 2)
                        
                        return new Oper((Expr)nodoA.ChildNodes[1], Oper.tipOper.MENOSU);  
                    }
                case "EXPR_REL":
                    {
                        return new Oper((Expr)expresion(nodoA.ChildNodes[0].ChildNodes[0], VarG, ambito), (Expr)expresion(nodoA.ChildNodes[0].ChildNodes[2], VarG, ambito),Oper.getOperador(nodoA.ChildNodes[0].ChildNodes[1].Token.Text));
                    }
                case "EXPR_LOG":
                    {
                        if(nodoA.ChildNodes[0].ChildNodes.Count == 3)
                        {
                            return new Oper((Expr)expresion(nodoA.ChildNodes[0].ChildNodes[0], VarG, ambito), (Expr)expresion(nodoA.ChildNodes[0].ChildNodes[2], VarG, ambito), Oper.getOperador(nodoA.ChildNodes[0].ChildNodes[1].Token.Text));
                        }else
                            return new Oper((Expr)expresion(nodoA.ChildNodes[0].ChildNodes[1], VarG, ambito), Oper.tipOper.NOT);

                    }
                case "PRIMITIVOS":
                    {
                        object val = nodoA.ChildNodes[0].ChildNodes[0].Token.Text;
                        ParseTreeNode aux = nodoA.ChildNodes[0].ChildNodes[0];
                        if (aux.Term.Name == "entero")
                        {
                            try
                            {
                                int res = Convert.ToInt32(val.ToString());
                                return new Prim(res, aux.Token.Location.Line, aux.Token.Location.Column);
                            }
                            catch
                            {
                                Decimal res = 0;
                                if (Decimal.TryParse(val.ToString(), out res))//comprobando si es decimal
                                    return new Prim(res, aux.Token.Location.Line, aux.Token.Location.Column);
                                return new Prim(val, aux.Token.Location.Line, aux.Token.Location.Column);
                            }
                        } else if (aux.Term.Name == "cadena")
                        {
                            return new Prim(val.ToString().Replace("'", ""), aux.Token.Location.Line, aux.Token.Location.Column);
                        }
                        else if (aux.Term.Name== "true")
                        {
                            return new Prim(true,aux.Token.Location.Line, aux.Token.Location.Column);
                        }
                        else if (aux.Term.Name == "false")
                        {
                            return new Prim(false, aux.Token.Location.Line, aux.Token.Location.Column);
                        }
                        else if (aux.Term.Name == "id")
                        {
                            return new IdentVal(val.ToString(), aux.Token.Location.Line, aux.Token.Location.Column);
                        } else
                            return null;
                    } 
                default:
                    {
                        if(nodoA.Term.Name == "ExpWrite")
                        { 
                            if (nodoA.ChildNodes.Count == 3)
                            {
                                if (nodoA.ChildNodes[1].Token.Text == ",")
                                {
                                    return new Oper((Expr)expresion(nodoA.ChildNodes[0], VarG, ambito), (Expr)expresion(nodoA.ChildNodes[2], VarG, ambito), Oper.getOperador(nodoA.ChildNodes[1].Token.Text));

                                }
                            }
                            return (expresion(nodoA.ChildNodes[0], VarG, ambito));
                        }

                        return expresion(nodoA.ChildNodes[1], VarG, ambito);
                    }
            }
            
        }
        
    
    }
}
