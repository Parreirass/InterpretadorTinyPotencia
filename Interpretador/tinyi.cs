//using interpreter.command;
using lexical;
using syntatic;
using System;

public class Tinyi
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: dotnet tinyi.dll [source file]");
            return;
        }

        try
        {
            using (LexicalAnalysis l = new LexicalAnalysis(args[0]))
            {
                SyntaticAnalysis s = new SyntaticAnalysis(l);

                BlocksCommand program = s.Start();
                program.Execute();
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Internal error: " + e.Message);
        }
    }
}
