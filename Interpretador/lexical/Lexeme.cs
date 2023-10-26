using System;
namespace lexical
{
    public class Lexeme
    {
        // Propriedades públicas para acesso fácil.
        public string Token { get; set; }
        public TokenType Type { get; set; }

        public Lexeme(string token, TokenType type)
        {
            Token = token;
            Type = type;
        }
    }
}
