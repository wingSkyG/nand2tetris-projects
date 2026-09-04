using System.Runtime.CompilerServices;

internal class Program
{
    enum InstructionType
    {
        A_INSTRUCTION,
        C_INSTRUCTION,
        L_INSTRUCTION,
        NONE
    }

    private static void Main(string[] args)
    {
        // read file
        string filePath = Path.Combine("TestCases", "CInstruction.asm");
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found: " + filePath);
            return;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            var currentLine = "";

            var instructionType = InstructionType.NONE;
            var symbol = "";
            var dest = "";
            var result = "";

            while (HasMoreLines(reader))
            {
                currentLine = GetCurrentLine(reader);
                if (ShouldSkipLine(currentLine))
                {
                    continue;
                }

                instructionType = ParseInstructionType(currentLine);
                result = instructionType.ToString();

                symbol = ParseSymbol(instructionType, currentLine);
                result = string.Join(",", result, symbol);

                dest = ParseDest(instructionType, currentLine);
                result = string.Join(",", result, dest);
            }

            System.Console.WriteLine(result);
        }
    }

    /// <summary>
    /// 解析Comp
    /// </summary>
    private static string ParseComp(string line)
    {
        if (!line.Contains('='))
        {
            return string.Empty;
        }

        var equalIndex = 0;
        var dest = "";

        if (!line.Contains(';'))
        {
            equalIndex = line.IndexOf('=');
            dest = line.Substring(equalIndex + 1);
            return dest;
        }

        var semicolonIndex = 0;
        var destLength = 0;
        semicolonIndex = line.IndexOf(';');
        destLength = semicolonIndex - equalIndex + 1;
        dest = line.Substring(equalIndex, destLength);
        return dest;
    }

    /// <summary>
    /// 解析Dest
    /// </summary>
    private static string ParseDest(InstructionType type, string line)
    {
        if(type != InstructionType.C_INSTRUCTION)
        {
            return string.Empty;
        }

        var equalIndex = line.IndexOf('=');
        var leftPart = line.Substring(0, equalIndex);
        return leftPart;
    }

    /// <summary>
    /// 解析Symbol
    /// </summary>
    private static string ParseSymbol(InstructionType type, string line)
    {
        var symbol = "";

        if (type == InstructionType.A_INSTRUCTION)
        {
            var startIndex = line.IndexOf('@') + 1;
            var result = line.Substring(startIndex);
            symbol = result;
        }
        if (type == InstructionType.L_INSTRUCTION)
        {
            var startIndex = line.IndexOf('(') + 1;
            var endIndex = line.IndexOf(')');
            var length = endIndex - startIndex;

            var result = line.Substring(startIndex, length);
            symbol = result;
        }

        return symbol;
    }

    /// <summary>
    /// 解析InstructionType
    /// </summary>
    private static InstructionType ParseInstructionType(string line)
    {
        if (line.StartsWith('@'))
        {
            return InstructionType.A_INSTRUCTION;
        }

        if (line.StartsWith('('))
        {
            return InstructionType.L_INSTRUCTION;
        }

        return InstructionType.C_INSTRUCTION;
    }

    /// <summary>
    /// advance to the next line if the current line is empty or a comment
    /// </summary>
    private static bool ShouldSkipLine(string currentLine)
    {
        return string.IsNullOrEmpty(currentLine) || currentLine.StartsWith("//");
    }

    /// <summary>
    /// 获取当前行
    /// </summary>
    private static string GetCurrentLine(StreamReader reader)
    {
        return reader.ReadLine();
    }

    /// <summary>
    /// 是否有更多的行
    /// </summary>
    private static bool HasMoreLines(StreamReader reader)
    {
        return reader.Peek() >= 0;
    }
}