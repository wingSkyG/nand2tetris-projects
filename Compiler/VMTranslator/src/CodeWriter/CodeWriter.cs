class CodeWriter
{
    private readonly ArithmeticWriter _arithmeticWriter = new();
    public readonly MemoryWriter _memoryWriter = new();
    public readonly BranchWriter _branchWriter = new();

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
            default:
                Console.WriteLine($"Unknown command type: {command.CommandType}");
                break;
        }

        return assemblyCode;
    }
}