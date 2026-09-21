using System.Text;
using static LanguageSpecificationData;

class CodeWriter
{
    private readonly ArithmeticWriter _arithmeticWriter;
    private readonly MemoryWriter _memoryWriter;
    private readonly BranchWriter _branchWriter;
    private readonly FunctionWriter _functionWriter;

    private string FileName = "";
    private StringBuilder _strBuilder = new();

    public CodeWriter()
    {
        _arithmeticWriter = new();
        _memoryWriter = new();
        _branchWriter = new();
        _functionWriter = new();
        InsertBootstrapCode();
    }

    /// <summary>
    /// 翻译VMCommand为Assembly Code
    /// </summary>
    public void TranslateVMCommand(VMCommand command)
    {
        var assemCode = string.Empty;
        // Console.WriteLine($"TranslateVMCommand: {command}");

        switch (command)
        {
            case ArithmeticCommand arithmetic:
                assemCode = _arithmeticWriter.WriteArithmetic(arithmetic.OperatorType);
                break;
            case PushCommand push:
                assemCode = _memoryWriter.WritePush(push.SegmentType, push.Index);
                break;
            case PopCommand pop:
                assemCode = _memoryWriter.WritePop(pop.SegmentType, pop.Index);
                break;
            case BranchCommand branch:
                assemCode = _branchWriter.WriteBranch(branch.BranchType, branch.LabelName);
                break;
            case FunctionCommandBase functionCommand:
                assemCode = _functionWriter.WriteFunctionCommand(functionCommand);
                break;
            default:
                Console.WriteLine($"Unknown command type: {command.CommandType}");
                break;
        }

        _strBuilder.AppendLine(assemCode);
    }

    /// <summary>
    /// 设置文件名
    /// </summary>
    public void SetFileName(string fileName)
    {
        FileName = fileName;
        _memoryWriter.SetFileName(fileName);
        AppendFileNameCommend();
    }

    /// <summary>
    /// 获取Assembly Code
    /// </summary>
    public string GetAssemblyCode()
    {
        return _strBuilder.ToString();
    }

    /// <summary>
    /// 生成引导代码
    /// </summary>
    private void InsertBootstrapCode()
    {
        var bootstrapCode = string.Empty;

        bootstrapCode += "/// Bootstrap Code\n";
        var letAssemCode = $"""
            // SP = 256
            @256
            D=A
            @SP
            M=D
            """;
        bootstrapCode += letAssemCode;
        bootstrapCode += "\n";

        FunctionNameOfCaller = "Bootstrap";
        var callCommand = new CallCommand
        {
            FunctionType = FunctionType.Call,
            FunctionName = "Sys.init",
            ArgumentCount = 0
        };
        var callAssemCode = _functionWriter.WriteFunctionCommand(callCommand);

        bootstrapCode += callAssemCode;
        bootstrapCode += "\n";
        
        _strBuilder.AppendLine(bootstrapCode);
    }

    /// <summary>
    /// 追加文件名注释
    /// </summary>
    private void AppendFileNameCommend()
    {
        _strBuilder.AppendLine($"/// {FileName}.vm");
    }
}