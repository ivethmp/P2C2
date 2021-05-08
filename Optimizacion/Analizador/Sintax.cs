using System;
using System.Collections.Generic;
using System.Text;
using Irony;
using Irony.Ast;
using Irony.Parsing;
using P1.Optimizacion.Arbol;
using P1.Optimizacion.GenerarN;
using P1.Optimizacion.Instrucciones;
using P1.Optimizacion.Interfaz;
using P1.Optimizacion.Reporte;

namespace P1.Optimizacion.Analizador
{
    class Sintax
    {
        public AST2 arb { get; set; }
        public void Analizar(String cad)
        {
            System.Diagnostics.Debug.WriteLine("Analizador Optimizar");
            Gramatica2 gramatica = new Gramatica2();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cad);
            ParseTreeNode raiz = arbol.Root;
            
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

            LinkedList<Instr2> AST = instrucs(raiz);
            arb = new Arbol.AST2(AST);
            LinkedList<CodigoC> report = new LinkedList<CodigoC>();
            LinkedList<String> temps = new LinkedList<string>();
            LinkedList<Instr2> nuevo = new LinkedList<Instr2>();
            int cont = 0;
            foreach (Instr2 inst in AST)
            {
                inst.getOptimizar(arb,nuevo, report, temps);
                cont++;
            }
            String cadena = "";
            
            

            foreach (String str in temps)
            {
                cadena = cadena + str + ",";
            }
            if (cadena != "")
            {
                cadena = "float " + cadena.TrimEnd(',') + ";\n\n";
            }
            nuevo.AddFirst(new NewCod(cadena));
            cadena = "#include <stdio.h>\n";
            nuevo.AddFirst(new NewCod(cadena));
            foreach (Instr2 ins in nuevo)
            {
                ins.getOptimizar(arb, nuevo, report, temps);
            }
            
            GenTabla reporte = new GenTabla();
            reporte.GenHTML2(report, "Reporte-Optimizacion");


        }

        private static string BuildParsingErrorMessage(LogMessageList messages)
        {//este metodo obtiene los mensajes de error sintactico desde la gramatica
            var sb = new StringBuilder();
            sb.AppendLine("Errores de sintaxis:");
            messages.ForEach(msg => sb.AppendLine($"\t{msg.Message + " lin:" + msg.Location.Line + " col:" + msg.Location.Column}"));
            return sb.ToString();
        }

        public LinkedList<Instr2> instrucs(ParseTreeNode nodoA)
        {
            LinkedList<Instr2> lista = new LinkedList<Instr2>();

            if (nodoA.Term.Name == "INSTR")
            {
                foreach (ParseTreeNode instruction in nodoA.ChildNodes)
                {
                    lista.AddLast(instr(instruction.ChildNodes[0]));
                }
                return lista;
            }

            if (nodoA.ChildNodes[1].Term.Name == "BLOQ-VAR")
            {
                foreach(ParseTreeNode variable in nodoA.ChildNodes[1].ChildNodes)
                {
                    lista.AddLast(instr(variable));
                }    
            }
            if (nodoA.ChildNodes[2].Term.Name == "BLOQ-VOID")
            {
                foreach (ParseTreeNode voidmetod in nodoA.ChildNodes[2].ChildNodes)
                {
                    lista.AddLast(instr(voidmetod));
                }
            }
            
            return lista;

        }

        private Simbolo.Tipo tipoVar(ParseTreeNode nodoB)
        {
            String tipo = nodoB.ToString().Split(' ')[0];
            if (tipo == "TIPO-VAR") tipo = nodoB.ChildNodes[0].Token.Text;
            switch (tipo.ToLower())
            {
                case "int": return Simbolo.Tipo.INT;
                case "float": return Simbolo.Tipo.FLOAT;
                case "char": return Simbolo.Tipo.CHAR;
                case "void": return Simbolo.Tipo.VOID;
                default: return Simbolo.Tipo.INVAL;
            }
        }
        private Instr2 instr (ParseTreeNode nodoA)
        {
            string tokenOp = nodoA.ToString().Split(' ')[0];
            System.Diagnostics.Debug.WriteLine("el token es " + tokenOp);

            switch (tokenOp.ToLower())
            {
                case "var":
                    {
                        LinkedList<String> ids = new LinkedList<string>();
                        if (nodoA.ChildNodes.Count == 3)
                        {
                            foreach(ParseTreeNode variable in nodoA.ChildNodes[1].ChildNodes)
                            {
                                ids.AddLast(variable.Token.Text);
                            }
                            return new Declaracion(ids, 0, tipoVar(nodoA.ChildNodes[0].ChildNodes[0]), nodoA.ChildNodes[2].Token.Location.Line, nodoA.ChildNodes[2].Token.Location.Column);
                        }
                        ids.AddLast(nodoA.ChildNodes[1].Token.Text);
                        return new Declaracion(ids, nodoA.ChildNodes[3], tipoVar(nodoA.ChildNodes[0].ChildNodes[0]), nodoA.ChildNodes[2].Token.Location.Line, nodoA.ChildNodes[2].Token.Location.Column);

                    }
                case "void":
                    {
                        LinkedList<Instr2> instruccion = new LinkedList<Instr2>();

                        //foreach (ParseTreeNode ins in nodoA.ChildNodes[5].ChildNodes)
                            
                            instruccion = instrucs(nodoA.ChildNodes[5]);
                        
                        return new VoidMetod(nodoA.ChildNodes[1].Token.Text,tipoVar(nodoA.ChildNodes[0]),instruccion,nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                    }
                case "asignar":
                    {
                        if(nodoA.ChildNodes[0].ChildNodes.Count  == 1) //solo es un id
                        {
                            return new Asignacion(nodoA.ChildNodes[0].ChildNodes[0].Token.Text,expresion(nodoA.ChildNodes[2].ChildNodes[0]),nodoA.ChildNodes[1].Token.Location.Line,nodoA.ChildNodes[1].Token.Location.Column);
                        }
                        //quiere decir que es un stack[(int)tn] = xxx;
                        return new ArrayS(nodoA.ChildNodes[0].ChildNodes[0].Token.Text,tipoVar(nodoA.ChildNodes[0].ChildNodes[3].ChildNodes[0]), nodoA.ChildNodes[0].ChildNodes[5].Token.Text,Simbolo.Tipo.ARRAY0, expresion(nodoA.ChildNodes[2].ChildNodes[0]), nodoA.ChildNodes[1].Token.Location.Line, nodoA.ChildNodes[1].Token.Location.Column);
                        
                    }
                case "printf":
                    {
                        if(nodoA.ChildNodes.Count > 4)
                        {
                            return new Printf(nodoA.ChildNodes[2].Token.Text, expresion(nodoA.ChildNodes[7]), tipoVar(nodoA.ChildNodes[5].ChildNodes[0]), nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                        }
                        else
                        {
                            String cadena = "printf(\""+nodoA.ChildNodes[2].Token.Text+"\");";
                            return new NoChange(cadena);
                        }
                    }
                case "call-fun-pro":
                    {
                        string cadena = nodoA.ChildNodes[0].Token.Text + "();\n";
                        return new NoChange(cadena);
                    }
                case "return":
                    {
                        return new NoChange("return;\n");
                    }
                case "instr-goto":
                    {
                        return new GOTO(nodoA.ChildNodes[1].Token.Text, nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                    }
                case "inst-if":
                    {
                        Expr2 condi = expresion(nodoA.ChildNodes[2]);
                        GOTO guto = new GOTO(nodoA.ChildNodes[5].Token.Text, nodoA.ChildNodes[5].Token.Location.Line, nodoA.ChildNodes[5].Token.Location.Column);
                        return new Condicional(condi, guto, nodoA.ChildNodes[0].Token.Location.Line, nodoA.ChildNodes[0].Token.Location.Column);
                    }
                default:
                    {
                        return new ETIQUETA(nodoA.Token.Text, nodoA.Token.Location.Line, nodoA.Token.Location.Column);
                    }
            }   
                    return null;
        }


        private Expr2 expresion(ParseTreeNode nodoA)
        {
            if (nodoA.Term.Name == "EXPR") nodoA = nodoA.ChildNodes[0];
        
            switch (nodoA.Term.Name)
            {
               /* case "CALL_FUNC-PROC":
                    {
                        return (Expr)instruc(nodoA.ChildNodes[0], VarG, ambito);
                    }*/
                case "ARITMETICAS":
                    {
                        if (nodoA.ChildNodes.Count == 3)
                        {
                            //return new Oper((Expr)nodoA.ChildNodes[0].ChildNodes[0], (Expr)nodoA.ChildNodes[0].ChildNodes[2], Oper.getOperador(nodoA.ChildNodes[0].ChildNodes[1].Token.Text));
                            return new Operacion((Expr2)expresion(nodoA.ChildNodes[0]), (Expr2)expresion(nodoA.ChildNodes[2]), Operacion.getOperador(nodoA.ChildNodes[1].Token.Text));
                        }//else if (nodoA.ChildNodes.Count == 2)

                        return new Operacion((Expr2)expresion(nodoA.ChildNodes[1]), Operacion.tipOper.MENOSU);
                    }
                case "RELACIONALES":
                    {
                        return new Operacion((Expr2)expresion(nodoA.ChildNodes[0]), (Expr2)expresion(nodoA.ChildNodes[2]), Operacion.getOperador(nodoA.ChildNodes[1].Token.Text));
                    }
                
                case "PRIMITIVOS":
                    {
                        if (nodoA.ChildNodes[0].ToString() == "ARRAY") nodoA = nodoA.ChildNodes[0];
                        object val = nodoA.ChildNodes[0].Token.Text;
                        ParseTreeNode aux = nodoA.ChildNodes[0];
                        if (aux.Term.Name == "entero")
                        {
                            try
                            {
                                int res = Convert.ToInt32(val.ToString());
                                return new Primitivo(res, aux.Token.Location.Line, aux.Token.Location.Column);
                            }
                            catch
                            {
                                Decimal res = 0;
                                if (Decimal.TryParse(val.ToString(), out res))//comprobando si es decimal
                                    return new Primitivo(res, aux.Token.Location.Line, aux.Token.Location.Column);
                                return new Primitivo(val, aux.Token.Location.Line, aux.Token.Location.Column);
                            }
                        }
                        else if (aux.Term.Name == "cadena")
                        {
                            return new Primitivo(val.ToString().Replace("'", ""), aux.Token.Location.Line, aux.Token.Location.Column);
                        }
                        
                        else if (aux.Term.Name == "id")
                        {
                            if(nodoA.ChildNodes.Count == 1) 
                                return new Identificador(val.ToString(), aux.Token.Location.Line, aux.Token.Location.Column);
                            else//quiere decir que es un stack[(int)tn]
                            {
                               // Identificador ident = new Identificador(nodoA.ChildNodes[5].Token.Text, nodoA.ChildNodes[5].Token.Location.Line, nodoA.ChildNodes[5].Token.Location.Column);
                                return new ArrayS(val.ToString(),tipoVar(nodoA.ChildNodes[3]), nodoA.ChildNodes[5].Token.Text, Simbolo.Tipo.ARRAY,0,nodoA.ChildNodes[6].Token.Location.Line, nodoA.ChildNodes[6].Token.Location.Column);
                            }
                        }

                            return null;
                    }
                /*default:
                    {
                        if (nodoA.Term.Name == "ExpWrite")
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
                    }*/
            }
            return null;
        }


        }
}
