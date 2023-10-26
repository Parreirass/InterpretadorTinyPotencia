using System;
//using interpreter.expr;


   public class IfCommand : Command
    {
        private BoolExpr cond;
        private Command thenCmds;
        private Command elseCmds;

        public IfCommand(int line, BoolExpr cond, Command thenCmds)
            : this(line, cond, thenCmds, null)
        {
        }

        public IfCommand(int line, BoolExpr cond, Command thenCmds, Command elseCmds)
            : base(line)
        {
            this.cond = cond;
            this.thenCmds = thenCmds;
            this.elseCmds = elseCmds;
        }

        public override void Execute()
        {
            if (cond.Expr())
            {
                thenCmds.Execute();
            }
            else
            {
                if (elseCmds != null)
                {
                    elseCmds.Execute();
                }
            }
        }
    }

