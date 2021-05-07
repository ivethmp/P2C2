using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;

namespace P1.Optimizacion.Analizador
{
    class Gramatica2: Grammar
        {

            public Gramatica2() : base(caseSensitive: true)
            {

                #region ER
                StringLiteral CADENA = new StringLiteral("cadena", "\"");
                var INT = new NumberLiteral("entero");
                
                IdentifierTerminal IDENT = new IdentifierTerminal("id");

                CommentTerminal comLinea = new CommentTerminal("CMLine", "//", "\n", "\r\n");
                CommentTerminal coMLinea1 = new CommentTerminal("CMMLine", "/*", "*/)");
                #endregion

                #region Terminales

                var INCLUDE = ToTerm("include");
                var STIDIO = ToTerm("stdio.h");
                var FLOAT = ToTerm("float");
                var ENTER = ToTerm("int");
                var CHAR = ToTerm("char");
                var VOID = ToTerm("void");
                var IF = ToTerm("if");
                var GOTO = ToTerm("goto");
                var PRINT = ToTerm("printf");
                var RETUR = ToTerm("return");
                //MarkReservedWords

                var IGUAL = ToTerm("=");
            
                var NUMERAL = ToTerm("#");
                var PUNTO = ToTerm(".");
                var PTCOMA = ToTerm(";");
                var COMA = ToTerm(",");
                var DOSPTS = ToTerm(":");
                var PARIZQ = ToTerm("(");
                var PARDER = ToTerm(")");
                var CORIZQ = ToTerm("[");
                var CORDER = ToTerm("]");
                var LLAIZQ = ToTerm("{");
                var LLADER = ToTerm("}");
                var MAS = ToTerm("+");
                var MENOS = ToTerm("-");
                var POR = ToTerm("*");
                var DIVIDIDO = ToTerm("/");
                var MAYORQ = ToTerm(">");
                var MENORQ = ToTerm("<");
                var MAYIQ = ToTerm(">=");
                var MENIQ = ToTerm("<=");
                var DIF = ToTerm("!=");
                var error = SyntaxError;

                RegisterOperators(1, Associativity.Right, IGUAL);
                RegisterOperators(2, Associativity.Left, COMA);
                RegisterOperators(5, Associativity.Left, IGUAL, DIF);
                RegisterOperators(6, Associativity.Neutral, MAYIQ, MAYORQ, MENIQ, MENORQ);
                RegisterOperators(7, Associativity.Left, MAS, MENOS);
                RegisterOperators(8, Associativity.Left, POR, DIVIDIDO);
                RegisterOperators(10, Associativity.Left, PARIZQ, PARDER);


                NonGrammarTerminals.Add(comLinea);
                NonGrammarTerminals.Add(coMLinea1);

                #endregion

                #region No Terminales
                NonTerminal ini = new NonTerminal("ini");
                NonTerminal bloqVoid = new NonTerminal("BLOQ-VOID");
                NonTerminal voidPF = new NonTerminal("VOID");
                NonTerminal instr = new NonTerminal("INSTR");
                NonTerminal instrucs = new NonTerminal("INSTRS");
                NonTerminal encabezado = new NonTerminal("ENCABEZADO");
                NonTerminal bloqVar = new NonTerminal("BLOQ-VAR");
                NonTerminal variables = new NonTerminal("VAR");
                NonTerminal tipoVar = new NonTerminal("TIPO-VAR");
                NonTerminal expr = new NonTerminal("EXPR");
                NonTerminal exprArit = new NonTerminal("ARITMETICAS");
                NonTerminal exprRela = new NonTerminal("RELACIONALES");
                NonTerminal prim = new NonTerminal("PRIMITIVOS");
                NonTerminal callFuncPro = new NonTerminal("CALL-FUN-PRO");
                NonTerminal imprimir = new NonTerminal("PRINTF");
                NonTerminal if_instr = new NonTerminal("INST-IF");
                NonTerminal goto_instr = new NonTerminal("INSTR-GOTO");
                NonTerminal listaID = new NonTerminal("LIST-ID");
                NonTerminal asig = new NonTerminal("ASIGNAR");
            NonTerminal array = new NonTerminal ("ARRAY");


            #endregion

            #region Gramatica

            ini.Rule = encabezado + bloqVar + bloqVoid
                         | error;

            encabezado.Rule = NUMERAL + INCLUDE + MENORQ + STIDIO + MAYORQ; // #include <stdio.h>
            bloqVoid.Rule = MakePlusRule(bloqVoid, voidPF);
            
            voidPF.Rule = VOID + IDENT + PARIZQ + PARDER + LLAIZQ + instr + LLADER; // VOID MAIN () { INSTRUCCIONES }

                instr.Rule = MakePlusRule(instr, instrucs);

            bloqVar.Rule = MakeStarRule(bloqVar, variables);

                variables.Rule = tipoVar + listaID + PTCOMA
                            | tipoVar + IDENT +CORIZQ+INT + CORDER + PTCOMA;
            listaID.Rule = MakeListRule(listaID,COMA,IDENT);


            instrucs.Rule = imprimir + PTCOMA
                            | callFuncPro + PTCOMA
                            | if_instr + PTCOMA
                            | goto_instr + PTCOMA
                            | IDENT + DOSPTS //ETIQUETA :
                            | asig + PTCOMA
                            | RETUR + PTCOMA;

            asig.Rule =  array + IGUAL + expr;

            imprimir.Rule = PRINT + PARIZQ + CADENA + COMA + PARIZQ +tipoVar+PARDER + expr + PARDER
                            | PRINT + PARIZQ + CADENA + PARDER;

            callFuncPro.Rule = IDENT + PARIZQ + PARDER;

            if_instr.Rule = IF + PARIZQ + expr + PARDER + GOTO + IDENT;

            goto_instr.Rule = GOTO + IDENT;
            

            array.Rule = IDENT + CORIZQ + PARIZQ + tipoVar + PARDER + IDENT + CORDER //STACK[(INT)A]
                         | IDENT;
            

                tipoVar.Rule = ENTER
                              | FLOAT
                              | CHAR;


            expr.Rule = //EXPRESIONES
                         prim
                       | exprArit
                       | exprRela
                       | PARIZQ + expr + PARDER;

                exprArit.Rule = //EXPRESIONES ARITMETICAS
                              MENOS + expr
                            | expr + MAS + expr
                            | expr + MENOS + expr
                            | expr + POR + expr
                            | expr + DIVIDIDO + expr;

            exprRela.Rule =//EXPRESIONES RELACIONALES
                         expr + MAYIQ + expr
                       | expr + MAYORQ + expr
                       | expr + MENIQ + expr
                       | expr + MENORQ + expr
                       | expr + DIF + expr
                       | expr + IGUAL + IGUAL + expr;



            prim.Rule = //VALORES PRIMITIVOS
                          INT
                        | array;



             
            #endregion

            #region Preferencias
            //MarkPunctuation(";");
            this.Root = ini;

                #endregion

            }

        }
    
}
