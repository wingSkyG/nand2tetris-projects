using static LanguageSpecificationData;

class CodeWriter
{
    /// <summary>
    /// 翻译VMCommand为Assembly Code
    /// </summary>
    public string TranslateVMCommand(VMCommand command)
    {
        var assemblyCode = string.Empty;
        System.Console.WriteLine($"TranslateVMCommand: {command}");

        switch (command)
        {
            case ArithmeticCommand arithmetic:
                assemblyCode = WriteArithmetic(arithmetic.OperatorType);
                break;
            case PushCommand push:
                assemblyCode = WritePush(push.SegmentType, push.Index);
                break;
            case PopCommand pop:
                assemblyCode = WritePop(pop.SegmentType, pop.Index);
                break;
            default:
                Console.WriteLine($"Unknown command type: {command.CommandType}");
                break;
        }

        return assemblyCode;
    }

    /// <summary>
    /// 翻译算术逻辑指令为Assembly Code
    /// </summary>
    private string WriteArithmetic(ArithmeticOperatorType operatorType)
    {
        var assemblyCode = string.Empty;

        switch (operatorType)
        {
            case ArithmeticOperatorType.add:
                assemblyCode = WriteAdd();
                break;
            case ArithmeticOperatorType.sub:
                assemblyCode = WriteSub();
                break;
            case ArithmeticOperatorType.neg:
                assemblyCode = WriteNeg();
                break;
            case ArithmeticOperatorType.eq:
                assemblyCode = WriteEq();
                break;
            case ArithmeticOperatorType.gt:
                assemblyCode = WriteGt();
                break;
            case ArithmeticOperatorType.lt:
                assemblyCode = WriteLt();
                break;
            case ArithmeticOperatorType.and:
                assemblyCode = WriteAnd();
                break;
            case ArithmeticOperatorType.or:
                assemblyCode = WriteOr();
                break;
            case ArithmeticOperatorType.not:
                assemblyCode = WriteNot();
                break;
            default:
                Console.WriteLine($"WriteArithmetic error: Unrecognized operator '{operatorType}'.");
                break;
        }

        return assemblyCode;
    }

    /// <summary>
    /// 翻译not指令为Assembly Code
    /// </summary>
    private string WriteNot()
    {
        var assemblyCode = $"""
            // not
            @SP
            M=M-1
            A=M
            M=!M
            @SP
            M=M+1
            """;
        return assemblyCode;
    }

    /// <summary>
    /// 翻译or指令为Assembly Code
    /// </summary>
    private string WriteOr()
    {
        var assemblyCode = $"""
            // or
            @SP
            M=M-1
            A=M
            D=M
            @SP
            M=M-1
            A=M
            M=D|M
            @SP
            M=M+1
            """;
        return assemblyCode;
    }

    /// <summary>
    /// 翻译and指令为Assembly Code
    /// </summary>
    private string WriteAnd()
    {
        var assemblyCode = $"""
            // and
            @SP
            M=M-1
            A=M
            D=M
            @SP
            M=M-1
            A=M
            M=D&M
            @SP
            M=M+1
            """;
        return assemblyCode;
    }

    /// <summary>
    /// 翻译lt指令为Assembly Code
    /// </summary>
    private string WriteLt()
    {
        var LT_TRUELabel = GenerateComparisonLabelName(ComparisonLabelType.LT_TRUE);
        var LT_ENDLabel = GenerateComparisonLabelName(ComparisonLabelType.LT_END);

        var assemblyCode = $"""
            // lt
            @SP
            M=M-1
            A=M
            D=M
            @SP
            M=M-1
            A=M
            D=M-D
            @{LT_TRUELabel}
            D;JLT
            @SP
            A=M
            M=0
            @{LT_ENDLabel}
            0;JMP
            ({LT_TRUELabel})
            @SP
            A=M
            M=-1
            ({LT_ENDLabel})
            @SP
            M=M+1
            """;
        return assemblyCode;
    }

    /// <summary>
    /// 翻译gt指令为Assembly Code
    /// </summary>
    private string WriteGt()
    {
        var GT_TRUELabel = GenerateComparisonLabelName(ComparisonLabelType.GT_TRUE);
        var GT_ENDLabel = GenerateComparisonLabelName(ComparisonLabelType.GT_END);

        var assemblyCode = $"""
            // gt
            @SP
            M=M-1
            A=M
            D=M
            @SP
            M=M-1
            A=M
            D=M-D
            @{GT_TRUELabel}
            D;JGT
            @SP
            A=M
            M=0
            @{GT_ENDLabel}
            0;JMP
            ({GT_TRUELabel})
            @SP
            A=M
            M=-1
            ({GT_ENDLabel})
            @SP
            M=M+1
            """;
        return assemblyCode;
    }

    /// <summary>
    /// 翻译eq指令为Assembly Code
    /// </summary>
    private string WriteEq()
    {
        var EQ_TRUELabel = GenerateComparisonLabelName(ComparisonLabelType.EQ_TRUE);
        var EQ_ENDLabel = GenerateComparisonLabelName(ComparisonLabelType.EQ_END);

        var assemblyCode = $"""
            // eq
            @SP
            M=M-1
            A=M
            D=M
            @SP
            M=M-1
            A=M
            D=D-M
            @{EQ_TRUELabel}
            D;JEQ
            @SP
            A=M
            M=0
            @{EQ_ENDLabel}
            0;JMP
            ({EQ_TRUELabel})
            @SP
            A=M
            M=-1
            ({EQ_ENDLabel})
            @SP
            M=M+1
            """;
        return assemblyCode;
    }

    /// <summary>
    /// 生成比较指令的标签名
    /// </summary>
    private string GenerateComparisonLabelName(ComparisonLabelType labelType)
    {
        var labelName = string.Empty;
        var labelIndex = 0;

        switch (labelType)
        {
            case ComparisonLabelType.EQ_TRUE:
                labelIndex = EQ_TRUELabelIndex++;
                labelName = $"EQ_TRUE_{labelIndex}";
                break;
            case ComparisonLabelType.EQ_END:
                labelIndex = EQ_ENDLabelIndex++;
                labelName = $"EQ_END_{labelIndex}";
                break;
            case ComparisonLabelType.GT_TRUE:
                labelIndex = GT_TRUELabelIndex++;
                labelName = $"GT_TRUE_{labelIndex}";
                break;
            case ComparisonLabelType.GT_END:
                labelIndex = GT_ENDLabelIndex++;
                labelName = $"GT_END_{labelIndex}";
                break;
            case ComparisonLabelType.LT_TRUE:
                labelIndex = LT_TRUELabelIndex++;
                labelName = $"LT_TRUE_{labelIndex}";
                break;
            case ComparisonLabelType.LT_END:
                labelIndex = LT_ENDLabelIndex++;
                labelName = $"LT_END_{labelIndex}";
                break;
            default:
                Console.WriteLine($"GenerateComparisonLabelName error: Unrecognized label type '{labelType}'.");
                return "";
        }

        return labelName;
    }

    /// <summary>
    /// 翻译neg指令为Assembly Code
    /// </summary>
    private string WriteNeg()
    {
        var assemblyCode = $"""
            // neg
            @SP
            M=M-1
            A=M
            M=-M
            @SP
            M=M+1
            """;

        return assemblyCode;
    }

    /// <summary>
    /// 翻译sub指令为Assembly Code
    /// </summary>
    private string WriteSub()
    {
        var assemblyCode = $"""
            // sub
            @SP
            M=M-1
            A=M
            D=M
            @SP
            M=M-1
            A=M
            M=M-D
            @SP
            M=M+1
            """;

        return assemblyCode;
    }

    /// <summary>
    /// 翻译add指令为Assembly Code
    /// </summary>
    private string WriteAdd()
    {
        var assemblyCode = $"""
            // add
            @SP
            M=M-1
            A=M
            D=M
            @SP
            M=M-1
            A=M
            M=D+M
            @SP
            M=M+1
            """;

        return assemblyCode;
    }

    /// <summary>
    /// 翻译pop指令为Assembly Code
    /// </summary>
    private string WritePop(SegmentType segmentType, int index)
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

        if(segmentType == SegmentType.@static)
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
    private string WritePush(SegmentType segmentType, int index)
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

        if(segmentType == SegmentType.pointer && index == ThisPointerIndex)
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
        
        if(segmentType == SegmentType.@static)
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