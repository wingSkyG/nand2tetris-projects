class CallCommand : FunctionCommandBase
{
    public required string FunctionName { get; set; }
    public int ArgumentCount { get; set; }
}