using static LanguageSpecificationData;

/// <summary>
/// 跳转指令类
/// </summary>
class BranchCommand
{
    public BranchType BranchType { get; set; }
    public string? LabelName { get; set; }
}