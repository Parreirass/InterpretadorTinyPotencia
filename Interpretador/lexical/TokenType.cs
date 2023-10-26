using System;
namespace lexical
{
    public enum TokenType
    {
        // Specials
        UNEXPECTED_EOF,
        INVALID_TOKEN,
        END_OF_FILE,

        // Symbols
        SEMICOLON,     // ;
        ASSIGN,        // =

        // Operadores lógicos
        EQUAL,         // ==
        NOT_EQUAL,     // !=
        LOWER,         // <
        LOWER_EQUAL,   // <=
        GREATER,       // >
        GREATER_EQUAL, // >=

        // Operadores aritméticos
        ADD,           // +
        SUB,           // -
        MUL,           // *
        DIV,           // /
        MOD,           // %
        POT,           // ^

        // Palavras-chave
        PROGRAM,       // program
        WHILE,         // while
        DO,            // do
        DONE,          // done
        IF,            // if
        THEN,          // then
        ELSE,          // else
        OUTPUT,        // output
        TRUE,          // true
        FALSE,         // false
        READ,          // read
        NOT,           // not

        // Outros
        NUMBER,        // number
        VAR            // variable
    }
}
