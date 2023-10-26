using System;

    public class NegIntExpr : IntExpr
    {
        private IntExpr expr;

        public NegIntExpr(int line, IntExpr expr)
            : base(line)
        {
            this.expr = expr;
        }

        public override int Expr()
        {
            return -expr.Expr();
        }
    }

