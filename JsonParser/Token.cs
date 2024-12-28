using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
    public record Token(TokenType TokenType, string Literal)
    {
    }
}
