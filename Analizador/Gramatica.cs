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
            var IMPR = ToTerm("write");
            var IMPR2 = ToTerm("writeln");
            var PTCOMA = ToTerm(";");
            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");
            var CORIZQ = ToTerm("[");
            var CORDER = ToTerm("]");
            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIVIDIDO = ToTerm("/");

            RegisterOperators(1, MAS, MENOS);
            RegisterOperators(2, POR, DIVIDIDO);

            NonGrammarTerminals.Add(comLinea);
            NonGrammarTerminals.Add(coMLinea1);
            NonGrammarTerminals.Add(coMLineaV2);

            #endregion

            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instr = new NonTerminal("instr");
            NonTerminal instrs = new NonTerminal("instrs");
            NonTerminal expr = new NonTerminal("expr");
            NonTerminal writ = new NonTerminal("writel");
            #endregion

            #region Gramatica
            ini.Rule = instrs;

            instrs.Rule = instr + instrs
                | instr;

            instr.Rule = writ + CORIZQ + expr + CORDER + PTCOMA;

            writ.Rule = IMPR
                    | IMPR2;

            expr.Rule = MENOS + expr
                | expr + MAS + expr
                | expr + MENOS + expr
                | expr + POR + expr
                | expr + DIVIDIDO + expr
                | INT
                | PARIZQ + expr + PARDER;

            #endregion

            #region Preferencias
            this.Root = ini;
            #endregion

        }

    }
}
