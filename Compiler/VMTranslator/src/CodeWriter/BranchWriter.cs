using static LanguageSpecificationData;

/// <summary>
/// 跳转指令翻译器
/// </summary>
class BranchWriter
{
    /// <summary>
    /// 翻译跳转指令为Assembly Code
    /// </summary>
    public string WriteBranch(BranchType branchType, string labelName)
    {
        var assemblyCode = string.Empty;

        switch (branchType)
        {
            case BranchType.Label:
                assemblyCode = WriteLabel(labelName);
                break;
            case BranchType.Goto:
                assemblyCode = WriteGoto(labelName);
                break;
            case BranchType.IfGoto:
                assemblyCode = WriteIf(labelName);
                break;
        }

        return assemblyCode;
    }

    /// <summary>
    /// 翻译跳转指令为Assembly Code
    /// </summary>
    private string WriteLabel(string labelName)
    {
        return $"""
            // label {labelName}
            ({labelName})
            """;
    }

    /// <summary>
    /// 翻译跳转指令为Assembly Code
    /// </summary>
    private string WriteGoto(string labelName)
    {
        return $"""
            // goto {labelName}
            @{labelName}
            0;JMP
            """;
    }

    /// <summary>
    /// 翻译跳转指令为Assembly Code
    /// </summary>
    private string WriteIf(string labelName)
    {
        return $"""
            // if-goto {labelName}
            @SP
            M=M-1
            A=M
            D=M
            @{labelName}
            D;JGT
            """;
    }
}