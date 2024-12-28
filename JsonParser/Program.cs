// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using JsonParser;

string jsonString = "{\"isCool\":true,\"name\":\"John\",\"age\":\"function () {return 30;}\",\"city\":\"New York\"}";
Lexer lexer = new Lexer(jsonString);

while (true)
{
    var token = lexer.Lex();
    Console.WriteLine(token);

    if (token.TokenType == TokenType.EOF) break;
}
