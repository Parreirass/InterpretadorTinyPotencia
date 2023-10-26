using System;

    public class ConstIntExpr : IntExpr
    {
        private int value;

        public ConstIntExpr(int line, int value)
            : base(line)
        {
            this.value = value;
        }

        public override int Expr()
        {
            return value;
        }
    }

