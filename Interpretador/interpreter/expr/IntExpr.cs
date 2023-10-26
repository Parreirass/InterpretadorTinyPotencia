using System;

    public abstract class IntExpr
    {
        private int line;

        protected IntExpr(int line)
        {
            this.line = line;
        }

        public int GetLine()
        {
            return line;
        }

        public abstract int Expr();
    }

