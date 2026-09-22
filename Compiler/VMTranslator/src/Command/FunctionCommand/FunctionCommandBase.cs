public enum FunctionType
{
    Function,
    Call,
    Return
}

class FunctionCommandBase : VMCommand
{
    public FunctionType FunctionType { get; set; }
}