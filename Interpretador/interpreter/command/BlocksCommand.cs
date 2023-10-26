using System;
using System.Collections.Generic;


    public class BlocksCommand : Command
    {
        private List<Command> cmds;

        public BlocksCommand(int line) : base(line)
        {
            cmds = new List<Command>();
        }

        public void addCommand(Command cmd)
        {
            cmds.Add(cmd);
        }

        public override void Execute()
        {
            foreach (Command cmd in cmds)
                cmd.Execute();
        }
    }
