using static LanguageSpecificationData;

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
            case BranchType.Goto:
                assemblyCode = $"""
                    // goto {labelName}
                    @{labelName}
                    0;JMP
                    """;
                break;
            case BranchType.IfGoto:
                assemblyCode = $"""
                    // if-goto {labelName}
                    @SP
                    M=M-1
                    A=M
                    D=M
                    @{labelName}
                    D;JGT
                    """;
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
            // {labelName}
            {labelName}
            0;JMP
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
            // if {labelName}
            @SP
            M=M-1
            A=M
            D=M
            @{labelName}
            D;JGT
            """;
    }
}