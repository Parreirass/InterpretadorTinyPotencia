using System;
using System.Collections.Generic;

namespace lexical
{
    public class SymbolTable
    {
        private Dictionary<string, TokenType> st;

        public SymbolTable()
        {
            st = new Dictionary<string, TokenType>();

            // Symbols
            st[";"] = TokenType.SEMICOLON;
            st["="] = TokenType.ASSIGN;

            // Logic operators
            st["=="] = TokenType.EQUAL;
            st["!="] = TokenType.NOT_EQUAL;
            st["<"] = TokenType.LOWER;
            st["<="] = TokenType.LOWER_EQUAL;
            st[">"] = TokenType.GREATER;
            st[">="] = TokenType.GREATER_EQUAL;

            // Arithmetic operators
            st["+"] = TokenType.ADD;
            st["-"] = TokenType.SUB;
            st["*"] = TokenType.MUL;
            st["/"] = TokenType.DIV;
            st["%"] = TokenType.MOD;
            st["^"] = TokenType.POT;

            // Keywords
            st["program"] = TokenType.PROGRAM;
            st["while"] = TokenType.WHILE;
            st["do"] = TokenType.DO;
            st["done"] = TokenType.DONE;
            st["if"] = TokenType.IF;
            st["then"] = TokenType.THEN;
            st["else"] = TokenType.ELSE;
            st["output"] = TokenType.OUTPUT;
            st["true"] = TokenType.TRUE;
            st["false"] = TokenType.FALSE;
            st["read"] = TokenType.READ;
            st["not"] = TokenType.NOT;
        }

        public bool Contains(string token)
        {
            return st.ContainsKey(token);
        }

        public TokenType Find(string token)
        {
            return Contains(token) ? st[token] : TokenType.VAR;
        }
    }
}
