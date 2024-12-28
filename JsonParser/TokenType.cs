using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
    public enum TokenType
    {
        OpenCurlyBrace,
        ClosedCurlyBrace,

        OpenSquareBracket,
        ClosedSquareBracket,

        Colon,
        Comma,

        StringLiteral,
        NumberLiteral,
        BooleanLiteral,

        EOF
    }
}
