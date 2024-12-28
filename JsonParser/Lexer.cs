using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
    public class Lexer
    {
        private string _jsonString;

        private int _position;
        private char _currentChar;
        private char _nextChar;

        public Lexer(string jsonString)
        {
            _jsonString = jsonString;

            _position = -1;
            _currentChar = '\0';

        }

        private void Next()
        {
            _position++;
            _currentChar = _position <= _jsonString.Length - 1 ? _jsonString[_position] : '\0';
        }

        private void SkipWhiteSpace()
        {
            while (_currentChar != '\0' && char.IsWhiteSpace(_currentChar))
            {
                Next();
            }
        }

        public Token Lex()
        {
            Next();
            SkipWhiteSpace();

            switch (_currentChar)
            {
                case '{': return new Token(TokenType.OpenCurlyBrace, "{");
                case '}': return new Token(TokenType.ClosedCurlyBrace, "}");
                case '[': return new Token(TokenType.OpenSquareBracket, "[");
                case ']': return new Token(TokenType.ClosedSquareBracket, "]");
                case ':': return new Token(TokenType.Colon, ":");
                case ',': return new Token(TokenType.Comma, ",");
                case '\0': return new Token(TokenType.EOF, "\0");
                case '"': return MakeStringLiteral();
                default:
                    if (char.IsDigit(_currentChar)) return MakeNumericalLiteral();
                    if (char.IsLetter(_currentChar)) return MatchCaseorThrow();
                    throw new InvalidOperationException($"Unexpected character: {_currentChar}");
            }
        }

        private Token MatchCaseorThrow()
        {
            if (MatchCase("true")) return new Token(TokenType.BooleanLiteral, "true");
            if (MatchCase("false")) return new Token(TokenType.BooleanLiteral, "false");
            throw new InvalidOperationException();
        }
        private bool MatchCase(string matcher)
        {
            int tempPosition = _position;
            char tempCurrentChar = _currentChar;

            for (int i = 0; i < matcher.Length; ++i)
            {
                if (matcher[i] != tempCurrentChar)
                {
                    return false;
                }

                tempPosition++;
                tempCurrentChar = tempPosition <= _jsonString.Length - 1 ? _jsonString[tempPosition] : '\0';
            }

            if (char.IsLetterOrDigit(tempCurrentChar) && tempCurrentChar != '\0')
            {
                return false;
            }

            _position = tempPosition;
            _currentChar = tempCurrentChar;
            return true;
        }
        private Token MakeNumericalLiteral()
        {
            int positionPlaceHolder = _position;
            int decimalCount = 0;
            Next();

            while (_currentChar != '\0' && (char.IsDigit(_currentChar) || _currentChar == '.'))
            {
                if (_currentChar == '.')
                {
                    decimalCount++;
                }
                Next();
            }
            string NumericLiteral = _jsonString.Substring(positionPlaceHolder, _position - positionPlaceHolder + 1);
            if (decimalCount > 1) throw new FormatException(NumericLiteral);
            return new Token(TokenType.NumberLiteral, NumericLiteral);

        }

        private Token MakeStringLiteral()
        {
            int positionPlaceholder = _position;
            Next();

            while (_currentChar != '\0' && _currentChar != '"')
            {
                Next();
            }

            string stringLiteral = _jsonString.Substring(positionPlaceholder, _position - positionPlaceholder + 1);
            return new Token(TokenType.StringLiteral, stringLiteral);

        }
    }
}
