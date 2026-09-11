using static LanguageSpecificationData;

class CodeWriter
{
    public string WriteArithmetic(string command)
    {
        var assemblyCode = string.Empty;

        if (!ArithmeticLogicalKeyWords.Contains(command))
        {
            Console.WriteLine($"WriteArithmetic error: Unrecognized command '{command}'.");
            return string.Empty;
        }

        if (command == "add")
        {
            assemblyCode = "10101010";
        }

        return assemblyCode;
    }
}