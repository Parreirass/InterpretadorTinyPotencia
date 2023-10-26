using System;
//using interpreter.command;
//using interpreter.expr;
using lexical;

namespace syntatic
{
    public class SyntaticAnalysis
    {
        private lexical.LexicalAnalysis lex;
        private lexical.Lexeme current;

        public SyntaticAnalysis(lexical.LexicalAnalysis lex)
        {
            this.lex = lex;
            this.current = lex.NextToken();
        }

        private void Advance()
        {
            current = lex.NextToken();
        }

        private void Eat(lexical.TokenType type)
        {
            Console.WriteLine("Esperando: " + type);
            Console.WriteLine("Encontrado: " + current.Token + "," + current.Type);
            if (type == current.Type)
            {
                Advance();
            }
            else
            {
                ShowError();
            }
        }

        private void ShowError()
        {
            Console.WriteLine($"{lex.GetLine():00}: ");

            switch (current.Type)
            {
                case lexical.TokenType.INVALID_TOKEN:
                    Console.WriteLine($"Lexema inválido [{current.Token}]");
                    break;
                case lexical.TokenType.UNEXPECTED_EOF:
                case lexical.TokenType.END_OF_FILE:
                    Console.WriteLine("Fim de arquivo inesperado");
                    break;
                default:
                    Console.WriteLine($"Lexema não esperado [{current.Token}]");
                    break;
            }

            Environment.Exit(1);
        }

       public BlocksCommand Start()
        {
            BlocksCommand cmds = ProcProgram();
            Eat(lexical.TokenType.END_OF_FILE);

            return cmds;
        } 

        // <program>   ::= program <cmdlist>
        private BlocksCommand ProcProgram()
        {
            Eat(lexical.TokenType.PROGRAM);
            return ProcCmdList();
        }

        // <cmdlist>   ::= <cmd> { <cmd> }
        private BlocksCommand ProcCmdList()
        {
            BlocksCommand cb = new BlocksCommand(-1);

            Command c = ProcCmd();
            cb.addCommand(c);
            while (current.Type == lexical.TokenType.VAR ||
                    current.Type == lexical.TokenType.OUTPUT ||
                    current.Type == lexical.TokenType.IF ||
                    current.Type == lexical.TokenType.WHILE)
            {
                c = ProcCmd();
                cb.addCommand(c);
            }

            return cb;
        }

        // <cmd>       ::= (<assign> | <output> | <if> | <while>) ;
        private Command ProcCmd()
        {
            Command c = null;
            if (current.Type == lexical.TokenType.VAR)
                c = ProcAssign();
            else if (current.Type == lexical.TokenType.OUTPUT)
                c = ProcOutput();
            else if (current.Type == lexical.TokenType.IF)
                c = ProcIf();
            else if (current.Type == lexical.TokenType.WHILE)
                c = ProcWhile();
            else
                ShowError();

            Eat(lexical.TokenType.SEMICOLON);

            return c;
        }

        // <assign>    ::= <var> = <intexpr>
        private AssignCommand ProcAssign()
        {
            Variable var = ProcVar();
            int line = lex.GetLine();

            Eat(lexical.TokenType.ASSIGN);
            IntExpr expr = ProcIntExpr();

            return new AssignCommand(line, var, expr);
        }

        // <output>    ::= output <intexpr>
        private OutputCommand ProcOutput()
        {
            Eat(lexical.TokenType.OUTPUT);
            int line = lex.GetLine();

            IntExpr expr = ProcIntExpr();

            return new OutputCommand(line, expr);
        }

        // <if>        ::= if <boolexpr> then <cmdlist> [ else <cmdlist> ] done
        private IfCommand ProcIf()
        {
            Eat(lexical.TokenType.IF);
            int line = lex.GetLine();

            BoolExpr cond = ProcBoolExpr();
            Eat(lexical.TokenType.THEN);
            BlocksCommand thenCmds = ProcCmdList();

            BlocksCommand elseCmds = null;
            if (current.Type == lexical.TokenType.ELSE)
            {
                Advance();
                elseCmds = ProcCmdList();
            }
            Eat(lexical.TokenType.DONE);

            return new IfCommand(line, cond, thenCmds, elseCmds);
        }

        // <while>     ::= while <boolexpr> do <cmdlist> done
        private WhileCommand ProcWhile()
        {
            Eat(lexical.TokenType.WHILE);
            int line = lex.GetLine();

            BoolExpr cond = ProcBoolExpr();
            Eat(lexical.TokenType.DO);
            Command cmds = ProcCmdList();
            Eat(lexical.TokenType.DONE);

            return new WhileCommand(line, cond, cmds);
        }

        // <boolexpr>  ::= false | true |
        //                 not <boolexpr> |
        //                 <intterm> (== | != | < | <= | > | >=) <intterm>
        private BoolExpr ProcBoolExpr()
        {
            if (current.Type == lexical.TokenType.FALSE)
            {
                Advance();
                return new ConstBoolExpr(lex.GetLine(), false);
            }
            else if (current.Type == lexical.TokenType.TRUE)
            {
                Advance();
                return new ConstBoolExpr(lex.GetLine(), true);
            }
            else if (current.Type == lexical.TokenType.NOT)
            {
                Advance();
                int line = lex.GetLine();
                BoolExpr tmp = ProcBoolExpr();
                return new NotBoolExpr(line, tmp);
            }
            else
            {
                IntExpr left = ProcIntTerm();
                int line = lex.GetLine();

                RelOp op = 0;
                switch (current.Type)
                {
                    case lexical.TokenType.EQUAL:
                        op = RelOp.Equal;
                        break;
                    case lexical.TokenType.NOT_EQUAL:
                        op = RelOp.NotEqual;
                        break;
                    case lexical.TokenType.LOWER:
                        op = RelOp.LowerThan;
                        break;
                    case lexical.TokenType.LOWER_EQUAL:
                        op = RelOp.LowerEqual;
                        break;
                    case lexical.TokenType.GREATER:
                        op = RelOp.GreaterThan;
                        break;
                    case lexical.TokenType.GREATER_EQUAL:
                        op = RelOp.GreaterEqual;
                        break;
                    default:
                        ShowError();
                        break;
                }

                Advance();

                IntExpr right = ProcIntTerm();

                return new SingleBoolExpr(line, left, op, right);
            }
        }

        // <intexpr>   ::= [ + | - ] <intterm> [ (+ | - | * | / | %) <intterm> ]
        private IntExpr ProcIntExpr()
        {
            bool isNegative = false;
            if (current.Type == lexical.TokenType.ADD)
            {
                Advance();
            }
            else if (current.Type == lexical.TokenType.SUB)
            {
                Advance();
                isNegative = true;
            }

            IntExpr left;
            if (isNegative)
            {
                int line = lex.GetLine();
                IntExpr tmp = ProcIntTerm();
                left = new NegIntExpr(line, tmp);
            }
            else
            {
                left = ProcIntTerm();
            }

            if (current.Type == lexical.TokenType.ADD ||
                current.Type == lexical.TokenType.SUB ||
                current.Type == lexical.TokenType.MUL ||
                current.Type == lexical.TokenType.DIV ||
                current.Type == lexical.TokenType.MOD ||
                current.Type == lexical.TokenType.POT)
            {
                int line = lex.GetLine();

                IntOp op;
                switch (current.Type)
                {
                    case lexical.TokenType.ADD:
                        op = IntOp.Add;
                        break;
                    case lexical.TokenType.SUB:
                        op = IntOp.Sub;
                        break;
                    case lexical.TokenType.MUL:
                        op = IntOp.Mul;
                        break;
                    case lexical.TokenType.DIV:
                        op = IntOp.Div;
                        break;
                    case lexical.TokenType.POT:
                        op = IntOp.Pot;
                        break;
                    case lexical.TokenType.MOD:
                    default:
                        op = IntOp.Mod;
                        break;
                }

                Advance();

                IntExpr right = ProcIntTerm();

                return new BinaryIntExpr(line, left, op, right);
            }
            else
            {
                return left;
            }
        }

        // <intterm>   ::= <var> | <const> | read
        private IntExpr ProcIntTerm()
        {
            if (current.Type == lexical.TokenType.VAR)
            {
                return ProcVar();
            }
            else if (current.Type == lexical.TokenType.NUMBER)
            {
                return ProcConst();
            }
            else
            {
                Eat(lexical.TokenType.READ);
                int line = lex.GetLine();
                return new ReadIntExpr(line);
            }
        }

        // <var>       ::= id
        private Variable ProcVar()
        {
            string name = current.Token;
            Eat(lexical.TokenType.VAR);

            return Variable.Instance(name);
        }

        // <const>     ::= number
        private ConstIntExpr ProcConst()
        {
            string tmp = current.Token;
            Eat(lexical.TokenType.NUMBER);
            int line = lex.GetLine();

            int number;
            try
            {
                number = int.Parse(tmp);
            }
            catch (Exception e)
            {
                number = 0;
            }

            return new ConstIntExpr(line, number);
        }
    }
}
