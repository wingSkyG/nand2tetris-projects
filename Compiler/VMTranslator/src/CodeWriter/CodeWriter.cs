using static LanguageSpecificationData;

class CodeWriter
{
    private readonly ArithmeticWriter _arithmeticWriter = new();
    public readonly MemoryWriter _memoryWriter = new();
    public readonly BranchWriter _branchWriter = new();
    public readonly FunctionWriter _functionWriter = new();

    /// <summary>
    /// 生成引导代码
    /// </summary>
    public string GenerateBootstrapCode()
    {
        var bootstrapCode = string.Empty;

        var firstAssemCode = $"""
            // SP = 256
            @256
            D=A
            @SP
            M=D
            """;
        bootstrapCode += firstAssemCode;
        bootstrapCode += "\n";
        
        FunctionNameOfCaller = "Bootstrap";
        var callCommand = new CallCommand
        {
            FunctionType = FunctionType.Call,
            FunctionName = "Sys.init",
            ArgumentCount = 0
        };
        var secondAssemCode = _functionWriter.WriteFunctionCommand(callCommand);

        bootstrapCode += secondAssemCode;
        bootstrapCode += "\n";
        return bootstrapCode;
    }

    /// <summary>
    /// 翻译VMCommand为Assembly Code
    /// </summary>
    public string TranslateVMCommand(VMCommand command)
    {
        var assemblyCode = string.Empty;
        // Console.WriteLine($"TranslateVMCommand: {command}");

        switch (command)
        {
            case ArithmeticCommand arithmetic:
                assemblyCode = _arithmeticWriter.WriteArithmetic(arithmetic.OperatorType);
                break;
            case PushCommand push:
                assemblyCode = _memoryWriter.WritePush(push.SegmentType, push.Index);
                break;
            case PopCommand pop:
                assemblyCode = _memoryWriter.WritePop(pop.SegmentType, pop.Index);
                break;
            case BranchCommand branch:
                assemblyCode = _branchWriter.WriteBranch(branch.BranchType, branch.LabelName);
                break;
            case FunctionCommandBase functionCommand:
                assemblyCode = _functionWriter.WriteFunctionCommand(functionCommand);
                break;
            default:
                Console.WriteLine($"Unknown command type: {command.CommandType}");
                break;
        }

        return assemblyCode;
    }
}