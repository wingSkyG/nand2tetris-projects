using static LanguageSpecificationData;

class Parser
{
    /// <summary>
    /// 解析VMCommand
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Dictionary<string, string>? ParseVMCommand(string command)
    {
        var components = new Dictionary<string, string>();

        if (Advance(command))
        {
            return null;
        }

        var trimmedCommand = TrimInlineComment(command);

        var commandType = ParseCommandType(trimmedCommand);

        if (commandType != CommandType.C_RETURN)
        {
            components.Add("firstParameter", ParseFirstParameter(trimmedCommand));
        }

        if (ShouldParseSecondParameterBeCalled(commandType))
        {
            components.Add("secondParameter", ParseSecondParameter(trimmedCommand));
        }

        return components;
    }

    /// <summary>
    /// 解析CommandType
    /// </summary>
    private CommandType ParseCommandType(string command)
    {
        var firstKeyword = command.Split(" ")[0];

        if (MemoryAccessKeyWords.Contains(firstKeyword))
        {
            return CommandType.C_PUSH;
        }

        if (ArithmeticLogicalKeyWords.Contains(firstKeyword))
        {
            return CommandType.C_ARITHMETIC;
        }

        Console.WriteLine($"Parse error: Unrecognized command type for command '{command}'.");
        return CommandType.NULL;
    }

    /// <summary>
    /// 解析第一个Parameter
    /// </summary>
    private string ParseFirstParameter(string command)
    {
        var firstParameter = command.Split(" ")[1];
        return firstParameter;
    }
    
    /// <summary>
    /// 解析第二个Parameter
    /// </summary>
    private string ParseSecondParameter(string command)
    {
        var secondParameter = command.Split(" ")[2];
        return secondParameter;
    }

    /// <summary>
    /// 判断是否需要解析第二个Parameter
    /// </summary>
    private bool ShouldParseSecondParameterBeCalled(CommandType commandType)
    {
        if (commandType == CommandType.C_PUSH || commandType == CommandType.C_POP || commandType == CommandType.C_FUNCTION || commandType == CommandType.C_CALL)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// trim inline comment
    /// </summary>
    private string TrimInlineComment(string command)
    {
        var trimmedCommand = command.Split("//")[0].Trim();
        return trimmedCommand;
    }

    /// <summary>
    /// advance to the next line if the current line is empty or a comment
    /// </summary>
    private bool Advance(string command)
    {
        return string.IsNullOrEmpty(command) || command.StartsWith("//");
    }
}