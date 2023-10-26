using System.Collections.Generic;
using System;



    public class Variable : IntExpr
    {
        private static Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

        private string name;
        private int value;

        private Variable(string name) : base(-1)
        {
            this.name = name;
            this.value = 0;
        }

        public string GetName()
        {
            return name;
        }

        public override int Expr()
        {
            return value;
        }

        public void SetValue(int value)
        {
            this.value = value;
        }

        public static Variable Instance(string name)
        {
            if (!variables.ContainsKey(name))
            {
                Variable v = new Variable(name);
                variables[name] = v;
            }

            return variables[name];
        }
    }

