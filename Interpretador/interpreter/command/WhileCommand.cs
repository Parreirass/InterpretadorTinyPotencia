using System;
//using interpreter.expr;


    public class WhileCommand : Command
    {
        private BoolExpr cond;
        private Command cmds;

        public WhileCommand(int line, BoolExpr cond, Command cmds)
            : base(line)
        {
            this.cond = cond;
            this.cmds = cmds;
        }

        public override void Execute()
        {
            while (cond.Expr())
            {
                cmds.Execute();
            }
        }
    }

