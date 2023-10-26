using System;

    public class NotBoolExpr : BoolExpr
    {
        private BoolExpr expr;

        public NotBoolExpr(int line, BoolExpr expr)
            : base(line)
        {
            this.expr = expr;
        }

        public override bool Expr()
        {
            return !expr.Expr();
        }
    }

