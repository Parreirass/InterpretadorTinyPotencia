using System;
//using interpreter.expr;


    public class AssignCommand : Command
    {
        private Variable var;
        private IntExpr expr;

        public AssignCommand(int line, Variable var, IntExpr expr) : base(line)
        {
            this.var = var;
            this.expr = expr;
        }

        public override void Execute()
        {
            int value = expr.Expr();
            var.SetValue(value);
        }
    }
