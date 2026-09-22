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

public class PushCommand : VMCommand
{
    public SegmentType SegmentType;
    public int Index;
}