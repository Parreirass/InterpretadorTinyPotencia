using System;


    public abstract class Command
    {
        private int line;

        protected Command(int line)
        {
            this.line = line;
        }

        public int GetLine()
        {
            return line;
        }

        public abstract void Execute();
    }

