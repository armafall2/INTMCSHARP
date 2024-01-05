using System;
using System.Collections.Generic;

namespace Serie_IV
{
    public static class BracketsControl
    {
        public static bool BracketsControls(string sentence)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char character in sentence)
            {
                if (IsOpeningBracket(character))
                {
                    stack.Push(character);
                }
                else if (IsClosingBracket(character))
                {
                    if (stack.Count == 0 || !AreBracketPairs(stack.Pop(), character))
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }

        private static bool IsOpeningBracket(char bracket)
        {
            return bracket == '{' || bracket == '(' || bracket == '[';
        }

        private static bool IsClosingBracket(char bracket)
        {
            return bracket == '}' || bracket == ')' || bracket == ']';
        }

        private static bool AreBracketPairs(char openingBracket, char closingBracket)
        {
            return (openingBracket == '{' && closingBracket == '}') ||
                   (openingBracket == '(' && closingBracket == ')') ||
                   (openingBracket == '[' && closingBracket == ']');
        }
    }
}
