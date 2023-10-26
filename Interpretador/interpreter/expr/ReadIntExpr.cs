using System;

public class ReadIntExpr : IntExpr
{
    public ReadIntExpr(int line)
        : base(line)
    {
    }

    public override int Expr()
    {
        try
        {
            string str = Console.ReadLine();
            int n;
            if (int.TryParse(str, out n))
            {
                return n;
            }
            else
            {
                return 0; // Ou outra ação apropriada em caso de entrada inválida.
            }
        }
        catch (Exception e)
        {
            return 0; // Lidar com exceções, se necessário.
        }
    }
}
