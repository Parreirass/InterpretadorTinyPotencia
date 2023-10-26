using System;

    public abstract class BoolExpr
    {
        private int line;

        protected BoolExpr(int line)
        {
            this.line = line;
        }

        public int GetLine()
        {
            return line;
        }

        public abstract bool Expr();
    }

