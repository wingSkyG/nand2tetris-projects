using static LanguageSpecificationData;

class Parser
{
    /// <summary>
    /// 解析VMCommand
    /// </summary>
    /// <returns>包含VMCommand组件的字典(key: firstParameter, value: secondParameter)</returns>
    public VMCommand? ParseVMCommand(string command)
    {
        var vmCommand = new VMCommand();

        if (Advance(command))
        {
            return null;
        }

        var trimmedCommand = TrimInlineComment(command);

        var commandType = ParseCommandType(trimmedCommand);
        // Console.WriteLine($"ParseVMCommand: {commandType}");

        switch (commandType)
        {
            case CommandType.C_ARITHMETIC:
                var arithmeticCommand = new ArithmeticCommand();
                arithmeticCommand.CommandType = commandType;
                arithmeticCommand.OperatorType = Enum.Parse<ArithmeticOperatorType>(trimmedCommand);
                vmCommand = arithmeticCommand;
                break;
            case CommandType.C_PUSH:
                var pushCommand = new PushCommand();
                pushCommand.SegmentType = Enum.Parse<SegmentType>(ParseFirstParameter(trimmedCommand));
                pushCommand.Index = int.Parse(ParseSecondParameter(trimmedCommand));
                vmCommand = pushCommand;
                break;
            case CommandType.C_POP:
                var popCommand = new PopCommand();
                popCommand.SegmentType = (SegmentType)Enum.Parse(typeof(SegmentType), ParseFirstParameter(trimmedCommand));
                popCommand.Index = int.Parse(ParseSecondParameter(trimmedCommand));
                vmCommand = popCommand;
                break;
        }

        return vmCommand;
    }

    /// <summary>
    /// 解析CommandType
    /// </summary>
    private CommandType ParseCommandType(string command)
    {
        var commandTypeString = command.Split(" ")[0];

        if (ArithmeticLogicalKeyWords.Contains(commandTypeString))
        {
            return CommandType.C_ARITHMETIC;
        }

        if (commandTypeString == PushKeyWord)
        {
            return CommandType.C_PUSH;
        }

        if (commandTypeString == PopKeyWord)
        {
            return CommandType.C_POP;
        }

        if (commandTypeString == LabelKeyWord)
        {
            return CommandType.C_LABEL;
        }

        if (commandTypeString == GotoKeyWord)
        {
            return CommandType.C_GOTO;
        }

        if (commandTypeString == IfKeyWord)
        {
            return CommandType.C_IF;
        }

        if (commandTypeString == FunctionKeyWord)
        {
            return CommandType.C_FUNCTION;
        }

        if (commandTypeString == ReturnKeyWord)
        {
            return CommandType.C_RETURN;
        }

        if (commandTypeString == CallKeyWord)
        {
            return CommandType.C_CALL;
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