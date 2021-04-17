using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace P1.Analizador
{
    class Gramatica : Grammar
    {

        public Gramatica():base(caseSensitive:false)
        {

            #region ER
            StringLiteral CADENA = new StringLiteral("cadena", "\'");
            var INT = new NumberLiteral("entero");
            IdentifierTerminal IDENT = new IdentifierTerminal("id");

            CommentTerminal comLinea = new CommentTerminal("CMLine", "//", "\n", "\r\n");
            CommentTerminal coMLinea1 = new CommentTerminal("CMMLine", "(*", "*)");
            CommentTerminal coMLineaV2 = new CommentTerminal("CMMLine2", "{", "}");
            #endregion

            #region Terminales
            var PROG = ToTerm("program");
            var VARI = ToTerm("var");
            var CONST = ToTerm("const");
            var BBEGIN = ToTerm("begin");
            var END = ToTerm("end");
            var IMPR = ToTerm("write");
            var IMPR2 = ToTerm("writeln");
            var ENTERO = ToTerm("integer");
            var CADENAS = ToTerm("string");
            var REAL = ToTerm("real");
            var BOOL = ToTerm("boolean");
            var VOID = ToTerm("void");
            var TIPO = ToTerm("type");
            var TRUE = ToTerm("true");
            var FALSE = ToTerm("false");
            var NOT = ToTerm("not");
            var OOR = ToTerm("or");
            var AAND = ToTerm("and");
            var FUNC = ToTerm("function");
            var PROC = ToTerm("procedure");
            var IF = ToTerm("if");
            var ELSE = ToTerm("else");
            var THEN = ToTerm("then");
            var EXIT = ToTerm("exit");
            var WHILE = ToTerm("while");
            var DO = ToTerm("do");

            //MarkReservedWords

            var IGUAL = ToTerm("=");
            var PUNTO = ToTerm(".");
            var PTCOMA = ToTerm(";");
            var COMA = ToTerm(",");
            var DOSPTS = ToTerm(":");
            var DPTSIGUAL = ToTerm(":=");
            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");
            var CORIZQ = ToTerm("[");
            var CORDER = ToTerm("]");
            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIVIDIDO = ToTerm("/");
            var MAYORQ = ToTerm(">");
            var MENORQ = ToTerm("<");
            var MAYIQ = ToTerm(">=");
            var MENIQ = ToTerm("<=");
            var DIF = ToTerm("<>");
            var error = SyntaxError;

            RegisterOperators(1, Associativity.Right, IGUAL);
            RegisterOperators(2, Associativity.Left, COMA);
            RegisterOperators(3, Associativity.Left, OOR);
            RegisterOperators(4, Associativity.Left, AAND);
            RegisterOperators(5, Associativity.Left, IGUAL, DIF);
            RegisterOperators(6, Associativity.Neutral, MAYIQ, MAYORQ, MENIQ, MENORQ);
            RegisterOperators(7, Associativity.Left, MAS, MENOS);
            RegisterOperators(8, Associativity.Left, POR, DIVIDIDO);
            RegisterOperators(9, Associativity.Right, NOT);
            RegisterOperators(10, Associativity.Left, PARIZQ, PARDER);


            NonGrammarTerminals.Add(comLinea);
            NonGrammarTerminals.Add(coMLinea1);
            NonGrammarTerminals.Add(coMLineaV2);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instr = new NonTerminal("INSTR");
            NonTerminal instrs = new NonTerminal("INSTRS");
            NonTerminal expr = new NonTerminal("EXPRS");
            NonTerminal exprAritmetica = new NonTerminal("EXPR_ARIT");
            NonTerminal exprRelacional = new NonTerminal("EXPR_REL");
            NonTerminal exprLogica = new NonTerminal("EXPR_LOG");
            NonTerminal writ = new NonTerminal("WRITE");
            NonTerminal print = new NonTerminal("WRITEL");
            NonTerminal declar = new NonTerminal("DECLARA");
            NonTerminal listDec = new NonTerminal("LISTDECLARA");
            NonTerminal asig = new NonTerminal("ASIGNA");
            NonTerminal tipo = new NonTerminal("TIPO");
            NonTerminal iniProg = new NonTerminal("PROGRAM");
            NonTerminal prim = new NonTerminal("PRIMITIVOS");
            NonTerminal bloqVar = new NonTerminal("BLOQ_VAR");
            NonTerminal OpcVar = new NonTerminal("OPCION_VAR");
            NonTerminal bloqBegin = new NonTerminal("BLOQ_BEGIN");
            NonTerminal instrBegin = new NonTerminal("INSTR_BEGIN");
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal funcProc = new NonTerminal("BLOQ_FUNC-PROC");
            NonTerminal decFunPro = new NonTerminal("DECLARA_FUN-PROC");
            NonTerminal listDecFP = new NonTerminal("LISTA_DECLA");
            NonTerminal CallFunProc = new NonTerminal("CALL_FUNC-PROC");
            NonTerminal pntComa = new NonTerminal("PUNTO-PTCOMA");
            NonTerminal ExpWrite = new NonTerminal("ExpWrite");
            NonTerminal listElse = new NonTerminal("LIST_ELSE");
            NonTerminal bloqIF = new NonTerminal("BLOQ_IF");
            NonTerminal ifSimp = new NonTerminal("IF_SIM");
            NonTerminal ifElse = new NonTerminal("IF_ELSE");
            NonTerminal ElseIf = new NonTerminal("ELSE_IF");
            NonTerminal ElseS = new NonTerminal("ELSE");
            NonTerminal instBegin = new NonTerminal("INSTRS_BEGIN");
            NonTerminal parIzq = new NonTerminal("op_parIzq");
            NonTerminal parDer = new NonTerminal("op_parDer");
            NonTerminal insFunProc = new NonTerminal("INS_FUNC-PROC");
            NonTerminal insFuPr = new NonTerminal("INS_FUN-PRO");
            NonTerminal retorno = new NonTerminal("EXIT");
            NonTerminal whileL = new NonTerminal("WHILE");
            NonTerminal BloqWhile = new NonTerminal("BLOQ_WHILE");
            NonTerminal BloqGVar = new NonTerminal("BLOQ_VAR-GLOB");
            NonTerminal GLVar = new NonTerminal("VAR-GLOBAL");
            NonTerminal BloqGBegin = new NonTerminal("BLOQ_BEG-GLOB");


            #endregion

            #region Gramatica

            ini.Rule = PROG + IDENT + PTCOMA + BloqGVar + instrs + BloqGBegin + PUNTO + instrs //6 HIJOS
                     | error;

            BloqGVar.Rule = MakeStarRule(BloqGVar, GLVar);
            /*BloqGVar.Rule = BloqGVar + GLVar
                        | GLVar
                        | Empty;
            */
            GLVar.Rule = OpcVar + bloqVar + PTCOMA;

            BloqGBegin.Rule = BBEGIN + bloqBegin + END;

            instrs.Rule = MakeStarRule(instrs, instr);
            //CUANTAS FUNCIONES Y PROCEDIMIENTOS QUIERA
            /*instrs.Rule = instrs + instr 
                | instr
                | Empty
                | error;*/

            //iniProg.Rule = PROG + IDENT + PTCOMA;

            instr.Rule = //BLOQUE INSTRUCCIONES VAR, BEGIN
                        //  OpcVar + bloqVar + PTCOMA
                       // | BBEGIN + bloqBegin + END + PUNTO
                          FUNC + funcProc + DOSPTS + tipo + PTCOMA + insFunProc + instBegin + PTCOMA  
                        | PROC + funcProc + PTCOMA + insFunProc + instBegin + PTCOMA
                        | error;

            insFunProc.Rule = OpcVar + bloqVar + PTCOMA 
                            | Empty
                            | error;


            pntComa.Rule = PUNTO
                           | PTCOMA;

            CallFunProc.Rule = IDENT + PARIZQ+ expresion + PARDER;
            expresion.Rule = MakeStarRule(expresion,COMA,expr);
            //bloqFunProc.Rule = 
            //BLOQUE DE FUNCIONES
            //funcion.Rule = FUNC + funcProc + DOSPTS + tipo + instrFun ;

            /*instrFun.Rule = listDecFP + PTCOMA + BBEGIN + bloqBegin + PTCOMA + END + PTCOMA
                          | BBEGIN + bloqBegin + PTCOMA + END + PTCOMA
                          | error;
            */
            funcProc.Rule = IDENT + PARIZQ + listDecFP + PARDER
                            | IDENT + PARIZQ + PARDER;

            listDecFP.Rule = MakePlusRule(listDecFP, PTCOMA,decFunPro);
            
            decFunPro.Rule = OpcVar + declar
                            | declar;
            //Opcion var o constante
            OpcVar.Rule = VARI
                        | CONST;

            bloqVar.Rule = //BLOQUE DE VAR EN PASCAL
                        MakePlusRule(bloqVar, PTCOMA, declar);

            //INSTRUCCIONES EN EL VAR
            declar.Rule = listDec + DOSPTS + tipo
                         | IDENT + DOSPTS + tipo
                         | IDENT + DOSPTS + tipo + IGUAL + expr; //DECLARACION Y ASIGNACION;

            //LISTA DE DECLARACIONES
            listDec.Rule = MakePlusRule(listDec, COMA, IDENT);

            bloqBegin.Rule =//BLOQUE DE BEGIN EN PASCAL
                             MakePlusRule(bloqBegin, instrBegin);

            // INSTRUCCIONES EN EL BEGIN
            instrBegin.Rule = asig + PTCOMA
                            | print + PTCOMA
                            | CallFunProc + PTCOMA
                            | bloqIF
                            | retorno + PTCOMA
                            | BloqWhile + PTCOMA
                            ;

            retorno.Rule = EXIT + PARIZQ + expr + PARDER;

            bloqIF.Rule = ifSimp + PTCOMA+ ifElse;

            ifElse.Rule = ElseS + PTCOMA
                          | listElse + PTCOMA
                          | listElse + PTCOMA + ElseS + PTCOMA
                          | Empty;

            BloqWhile.Rule = WHILE + expr + DO + instBegin;



            listElse.Rule = MakePlusRule(listElse, PTCOMA, ElseIf);

            ElseIf.Rule = ELSE + IF + parIzq + expr + parDer + THEN + instBegin;

            ifSimp.Rule = IF + parIzq + expr + parDer + THEN + instBegin;
                        
            ElseS.Rule = ELSE + instBegin;
            
            parIzq.Rule = PARIZQ
                         | Empty;
            parDer.Rule = PARDER
                         | Empty;


            instBegin.Rule = BBEGIN + bloqBegin + END ;

            asig.Rule = IDENT + DPTSIGUAL + expr //ESTA ES UNA ASIGNACION EN BEGIN
                        ;
            
            tipo.Rule = //TIPOS DE VARIABLES
                          ENTERO
                        | CADENAS
                        | REAL
                        | BOOL;

            
            print.Rule = writ + PARIZQ + ExpWrite + PARDER;

            ExpWrite.Rule = expr

                        | expr + COMA + expr;

            writ.Rule = IMPR
                       | IMPR2;
            
            expr.Rule = //EXPRESIONES
                         prim
                       | exprAritmetica
                       | exprRelacional
                       | exprLogica
                       | PARIZQ + expr + PARDER
                       | CallFunProc;

            exprAritmetica.Rule = //EXPRESIONES ARITMETICAS
                          MENOS + expr
                        | expr + MAS + expr
                        | expr + MENOS + expr
                        | expr + POR + expr
                        | expr + DIVIDIDO + expr;

            exprRelacional.Rule =//EXPRESIONES RELACIONALES
                         expr + MAYIQ + expr
                       | expr + MAYORQ + expr
                       | expr + MENIQ + expr
                       | expr + MENORQ + expr
                       | expr + IGUAL + expr
                       | expr + DIF + expr;

            exprLogica.Rule =//EXPRESIONES LOGICAS
                         NOT + expr
                       | expr + OOR + expr
                       | expr + AAND + expr;

            prim.Rule = //VALORES PRIMITIVOS
                          INT
                        | CADENA
                        | TRUE
                        | FALSE
                        | IDENT;

            #endregion

            #region Preferencias
            //MarkPunctuation(";");
            this.Root = ini;
            
            #endregion

        }

    }
}
