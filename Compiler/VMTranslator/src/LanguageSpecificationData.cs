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

    /// <summary>
    /// 内存访问指令的key words
    /// </summary>
    public static readonly List<string> MemoryAccessKeyWords =
    [
        "push",
        "pop"
    ];

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
}