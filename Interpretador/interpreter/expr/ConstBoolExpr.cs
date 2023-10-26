using System;   

    public class ConstBoolExpr : BoolExpr
    {
        private bool value;

        public ConstBoolExpr(int line, bool value)
            : base(line)
        {
            this.value = value;
        }

        public override bool Expr()
        {
            return value;
        }
    }

