using static LanguageSpecificationData;

/// <summary>
/// 跳转指令类
/// </summary>
class BranchCommand : VMCommand
{
    public BranchType BranchType { get; set; }
    public required string LabelName { get; set; }
}