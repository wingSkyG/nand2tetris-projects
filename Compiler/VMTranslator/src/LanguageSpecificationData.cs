public static class LanguageSpecificationData
{
    public enum CommandType
    {
        C_ARITHMETIC,
        C_PUSH,
        C_POP,
        C_LABEL,
        C_GOTO,
        C_IF,
        C_FUNCTION,
        C_RETURN,
        C_CALL,
        NULL
    }

    public enum ArithmeticOperatorType
    {
        add,
        sub,
        neg,
        eq,
        gt,
        lt,
        and,
        or,
        not
    }

    public enum SegmentType
    {
        local,
        argument,
        @this,
        that,
        constant,
        @static,
        pointer,
        temp
    }

    /// <summary>
    /// 算术逻辑指令的key words
    /// </summary>
    public static readonly List<string> ArithmeticLogicalKeyWords =
    [
        "add",
        "sub",
        "neg",
        "eq",
        "gt",
        "lt",
        "and",
        "or",
        "not"
    ];

    public static readonly string PushKeyWord = "push"; // 推送指令的key word
    public static readonly string PopKeyWord = "pop"; // 弹出指令的key word
    public static readonly string LabelKeyWord = "label"; // 标签指令的key word
    public static readonly string GotoKeyWord = "goto"; // 跳转指令的key word
    public static readonly string IfKeyWord = "if-goto"; // 条件跳转指令的key word
    public static readonly string FunctionKeyWord = "function"; // 函数指令的key word
    public static readonly string ReturnKeyWord = "return"; // 返回指令的key word
    public static readonly string CallKeyWord = "call"; // 调用指令的key word   

    /// <summary>
    /// 段类型（VM language地址段关键字）到预定义符号（Assembly预定义符号关键字）的映射
    /// </summary>
    public static readonly Dictionary<SegmentType, string> SegmentTypeToPredefinedSymbolMapDict = new()
    {
        [SegmentType.local] = "LCL",
        [SegmentType.argument] = "ARG",
        [SegmentType.@this] = "THIS",
        [SegmentType.that] = "THAT",
        [SegmentType.constant] = "@CONSTANT",
        [SegmentType.@static] = "@STATIC",
        [SegmentType.pointer] = "POINTER",
        [SegmentType.temp] = "TEMP"
    };

    public static int TempBaseAddress = 5;
    public static int ThisPointerIndex = 0;
    public static int ThatPointerIndex = 1;
    public static int StaticVariableBaseAddress = 16;
    public static string FileName = "";
}