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
            StringLiteral CADENA = new StringLiteral("cadena", "\"");
            var INT = new NumberLiteral("entero");
            var DECIMAL = new RegexBasedTerminal("decimal", "[0-9]+'.'[0-9]+");
            IdentifierTerminal IDENT = new IdentifierTerminal("id");

            CommentTerminal comLinea = new CommentTerminal("CMLine", "//", "\n", "\r\n");
            CommentTerminal coMLinea1 = new CommentTerminal("CMMLine", "(*", "*)");
            CommentTerminal coMLineaV2 = new CommentTerminal("CMMLine2", "{", "}");
            #endregion

            #region Terminales
            var PROG = ToTerm("program");
            var VARI = ToTerm("var");
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

            var IGUAL = ToTerm("=");
            
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

            RegisterOperators(1, MAS, MENOS);
            RegisterOperators(2, POR, DIVIDIDO);

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
            NonTerminal bloqBegin = new NonTerminal("BLOQ_BEGIN");
            #endregion

            #region Gramatica
            ini.Rule = instrs;

            instrs.Rule = instrs + instr
                | PROG + IDENT + PTCOMA + instr;

            //iniProg.Rule = PROG + IDENT + PTCOMA;

            instr.Rule = //INSTRUCCIONES 
                          VARI + bloqVar
                        | BBEGIN + bloqBegin;

            bloqVar.Rule = //BLOQUE DE DECLARACION VAR EN PASCAL
                          declar + PTCOMA;

            bloqBegin.Rule =//BLOQUE DE BEGIN EN PASCAL
                          asig + PTCOMA
                        | print;





            //DECLARACION RULE
            declar.Rule = listDec + DOSPTS + tipo
                         | IDENT + DOSPTS + tipo + IGUAL + prim; //DECLARACION Y ASIGNACION;
            //LISTA DE DECLARACIONES
            listDec.Rule = listDec + COMA + IDENT
                           | IDENT;

            asig.Rule = IDENT + DPTSIGUAL + expr; //ESTA ES UNA ASIGNACION EN BEGIN
                        

            tipo.Rule = //TIPOS DE VARIABLES
                          ENTERO
                        | CADENAS
                        | REAL
                        | BOOL;

            
            print.Rule = writ + PARIZQ + expr + PARDER + PTCOMA;

            writ.Rule = IMPR
                       | IMPR2;
            
            expr.Rule = //EXPRESIONES
                         prim
                       | IDENT
                       | exprAritmetica
                       | exprRelacional
                       | exprLogica
                       | PARIZQ + expr + PARDER;

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
                          ENTERO
                        | CADENA
                        | DECIMAL
                        | TRUE
                        | FALSE;

            #endregion

            #region Preferencias
            this.Root = ini;
            #endregion

        }

    }
}
