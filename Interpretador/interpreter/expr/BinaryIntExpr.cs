using System;

    public class BinaryIntExpr : IntExpr
    {
        private IntExpr left;
        private IntOp op;
        private IntExpr right;

        public BinaryIntExpr(int line, IntExpr left, IntOp op, IntExpr right)
            : base(line)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override int Expr()
        {
            int v1 = left.Expr();
            int v2 = right.Expr();

            switch (op)
            {
                case IntOp.Add:
                    return v1 + v2;
                case IntOp.Sub:
                    return v1 - v2;
                case IntOp.Mul:
                    return v1 * v2;
                case IntOp.Div:
                    return v1 / v2;
                case IntOp.Pot:
                    return (int)Math.Pow(v1, v2);
                case IntOp.Mod:
                default:
                    return v1 % v2;
            }
        }
    }
