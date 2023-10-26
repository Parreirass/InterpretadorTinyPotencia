using System;
//using interpreter.expr;


    public class OutputCommand : Command
    {
        private IntExpr expr;

        public OutputCommand(int line, IntExpr expr)
            : base(line)
        {
            this.expr = expr;
        }

        public override void Execute()
        {
            Console.WriteLine(expr.Expr());
        }
    }

