using static LanguageSpecificationData;

/// <summary>
/// 内存指令翻译器
/// </summary>
class MemoryWriter
{
    private string FileName = "";

    /// <summary>
    /// 设置文件名
    /// </summary>
    public void SetFileName(string fileName)
    {
        FileName = fileName;
    }

    /// <summary>
    /// 翻译pop指令为Assembly Code
    /// </summary>
    public string WritePop(SegmentType segmentType, int index)
    {
        string? assemblyCode;

        if (segmentType == SegmentType.temp)
        {
            assemblyCode = $"""
                // pop temp {index}
                @{TempBaseAddress}
                D=A
                @{index}
                D=D+A
                @R13
                M=D
                @SP
                M=M-1
                A=M
                D=M
                @R13
                A=M
                M=D
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.pointer && index == ThisPointerIndex)
        {
            assemblyCode = $"""
                // pop {segmentType} {index}
                @SP
                M=M-1
                A=M
                D=M
                @THIS
                M=D
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.pointer && index == ThatPointerIndex)
        {
            assemblyCode = $"""
                // pop {segmentType} {index}
                @SP
                M=M-1
                A=M
                D=M
                @THAT
                M=D
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.pointer && index == ThatPointerIndex)
        {
            assemblyCode = $"""
                // pop {segmentType} {index}
                @THAT
                D=M
                @SP
                M=M-1
                A=M
                D=M
                @R13
                A=M
                M=D
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.@static)
        {
            var staticVariable = GenerateStaticVariableName(index);
            assemblyCode = $"""
                // pop {segmentType} {index}
                @SP
                M=M-1
                A=M
                D=M
                @{staticVariable}
                M=D
                """;
            return assemblyCode;
        }

        var predefinedSymbol = GetPredefinedSymbol(segmentType);
        assemblyCode = $"""
            // pop {segmentType} {index}
            @{predefinedSymbol}
            D=M
            @{index}
            D=D+A
            @R13
            M=D
            @SP
            M=M-1
            A=M
            D=M
            @R13
            A=M
            M=D
            """;

        return assemblyCode;
    }

    /// <summary>
    /// 翻译push指令为Assembly Code
    /// </summary>
    public string WritePush(SegmentType segmentType, int index)
    {
        var predefinedSymbol = GetPredefinedSymbol(segmentType);

        string? assemblyCode;
        if (segmentType == SegmentType.constant)
        {
            assemblyCode = $"""
                // push constant {index}
                @{index}
                D=A
                @SP
                A=M
                M=D
                @SP
                M=M+1
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.temp)
        {
            assemblyCode = $"""
                // push temp {index}
                @{TempBaseAddress}
                D=A
                @{index}
                A=D+A
                D=M
                @SP
                A=M
                M=D
                @SP
                M=M+1
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.pointer && index == ThisPointerIndex)
        {
            assemblyCode = $"""
                // push {segmentType} {index}
                @THIS
                D=M
                @SP
                A=M
                M=D
                @SP
                M=M+1
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.pointer && index == ThatPointerIndex)
        {
            assemblyCode = $"""
                // push {segmentType} {index}
                @THAT
                D=M
                @SP
                A=M
                M=D
                @SP
                M=M+1
                """;
            return assemblyCode;
        }

        if (segmentType == SegmentType.@static)
        {
            var staticVariable = GenerateStaticVariableName(index);
            assemblyCode = $"""
                // push {segmentType} {index}
                @{staticVariable}
                D=M
                @SP
                A=M
                M=D
                @SP
                M=M+1
                """;
            return assemblyCode;
        }

        assemblyCode = $"""
            // push {segmentType} {index}
            @{predefinedSymbol}
            D=M
            @{index}
            A=D+A
            D=M
            @SP
            A=M
            M=D
            @SP
            M=M+1
            """;
        return assemblyCode;
    }

    /// <summary>
    /// 生成静态变量的名称
    /// </summary>
    private string GenerateStaticVariableName(int index)
    {
        return $"{FileName}.{index}";
    }

    /// <summary>
    /// 获取预定义符号（Assembly预定义符号关键字）
    /// </summary>
    private string? GetPredefinedSymbol(SegmentType segmentType)
    {
        var predefinedSymbol = SegmentTypeToPredefinedSymbolMapDict.TryGetValue(segmentType, out var symbol) ? symbol : null;
        if (predefinedSymbol == null)
        {
            Console.WriteLine($"SegmentType {segmentType} is not mapped to a predefined symbol.");
            return null;
        }

        return predefinedSymbol;
    }
}