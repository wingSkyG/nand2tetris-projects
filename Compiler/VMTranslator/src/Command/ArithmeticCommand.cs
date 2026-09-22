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

public class ArithmeticCommand : VMCommand
{
    public ArithmeticOperatorType OperatorType;
}