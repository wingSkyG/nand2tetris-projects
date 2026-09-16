using static LanguageSpecificationData;

/// <summary>
/// 算术逻辑指令翻译器
/// </summary>
class ArithmeticWriter
{
    /// <summary>
    /// 翻译算术逻辑指令为Assembly Code
    /// </summary>
    public string WriteArithmetic(ArithmeticOperatorType operatorType)
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
}