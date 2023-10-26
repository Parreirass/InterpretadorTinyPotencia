using System;

    public class SingleBoolExpr : BoolExpr
    {
        private IntExpr left;
        private RelOp op;
        private IntExpr right;

        public SingleBoolExpr(int line, IntExpr left, RelOp op, IntExpr right)
            : base(line)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override bool Expr()
        {
            int v1 = left.Expr();
            int v2 = right.Expr();

            switch (op)
            {
                case RelOp.Equal:
                    return v1 == v2;
                case RelOp.NotEqual:
                    return v1 != v2;
                case RelOp.LowerThan:
                    return v1 < v2;
                case RelOp.LowerEqual:
                    return v1 <= v2;
                case RelOp.GreaterThan:
                    return v1 > v2;
                case RelOp.GreaterEqual:
                default:
                    return v1 >= v2;
            }
        }
    }

