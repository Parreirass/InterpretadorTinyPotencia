using System;
using System.IO;

namespace lexical
{
    public class LexicalAnalysis : IDisposable
    {
        private int line;
        private SymbolTable st;
        private StreamReader input;
        private int unreadChar = -1;

        public LexicalAnalysis(string filename)
        {
            try
            {
                input = new StreamReader(filename);
            }
            catch (Exception e)
            {
                throw new LexicalException("Unable to open file: " + filename);
            }

            st = new SymbolTable();
            line = 1;
        }

        public void Dispose()
        {
            try
            {
                input.Close();
            }
            catch (Exception e)
            {
                throw new LexicalException("Unable to close file");
            }
        }

        public int GetLine()
        {
            return this.line;
        }

        public Lexeme NextToken()
        {
            Lexeme lex = new Lexeme("", TokenType.END_OF_FILE);

            int state = 1;
            while (state != 7 && state != 8)
            {
                int c = unreadChar != -1 ? unreadChar : GetC();
                unreadChar = -1;

                switch (state)
                {
                    case 1:
                        if (c == ' ' || c == '\r' || c == '\t')
                        {
                            state = 1;
                        }
                        else if (c == '\n')
                        {
                            line++;
                            state = 1;
                        }
                        else if (c == '#')
                        {
                            state = 2;
                        }
                        else if (c == '=' || c == '<' || c == '>')
                        {
                            lex.Token += (char)c;
                            state = 3;
                        }
                        else if (c == '!')
                        {
                            lex.Token += (char)c;
                            state = 4;
                        }
                        else if (c == ';' || c == '+' || c == '-' ||
                                 c == '*' || c == '/' || c == '%' || c == '^')
                        {
                            lex.Token += (char)c;
                            state = 7;
                        }
                        else if (char.IsLetter((char)c) || c == '_')
                        {
                            lex.Token += (char)c;
                            state = 5;
                        }
                        else if (char.IsDigit((char)c))
                        {
                            lex.Token += (char)c;
                            state = 6;
                        }
                        else if (c == -1)
                        {
                            lex.Type = TokenType.END_OF_FILE;
                            state = 8;
                        }
                        else
                        {
                            lex.Token += (char)c;
                            lex.Type = TokenType.INVALID_TOKEN;
                            state = 8;
                        }

                        break;
                    case 2:
                        if (c == '\n')
                        {
                            line++;
                            state = 1;
                        }
                        else if (c == -1)
                        {
                            lex.Type = TokenType.END_OF_FILE;
                            state = 8;
                        }
                        else
                        {
                            state = 2;
                        }

                        break;
                    case 3:
                        if (c == '=')
                        {
                            lex.Token += (char)c;
                            state = 7;
                        }
                        else
                        {
                            UngetC(c);
                            state = 7;
                        }

                        break;
                    case 4:
                        if (c == '=')
                        {
                            lex.Token += (char)c;
                            state = 7;
                        }
                        else if (c == -1)
                        {
                            lex.Type = TokenType.UNEXPECTED_EOF;
                            state = 8;
                        }
                        else
                        {
                            lex.Type = TokenType.INVALID_TOKEN;
                            state = 8;
                        }

                        break;
                    case 5:
                        if (char.IsLetter((char)c) ||
                            char.IsDigit((char)c) ||
                            c == '_')
                        {
                            lex.Token += (char)c;
                            state = 5;
                        }
                        else
                        {
                            UngetC(c);
                            state = 7;
                        }

                        break;
                    case 6:
                        if (char.IsDigit((char)c))
                        {
                            lex.Token += (char)c;
                            state = 6;
                        }
                        else
                        {
                            UngetC(c);
                            lex.Type = TokenType.NUMBER;
                            state = 8;
                        }

                        break;
                    default:
                        throw new LexicalException("Unreachable");
                }
            }

            if (state == 7)
                lex.Type = st.Find(lex.Token);

            return lex;
        }

        private int GetC()
        {
            try
            {
                int c = input.Read();
                if (c == '\n')
                    line++;
                return c;
            }
            catch (Exception e)
            {
                throw new LexicalException("Unable to read file");
            }
        }

        private void UngetC(int c)
        {
            unreadChar = c;
        }
    }
}
