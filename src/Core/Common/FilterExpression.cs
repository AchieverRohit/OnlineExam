namespace thinkschool.OnlineExam.Core.Common;
public class FilterExpression<T>
{
    public LogicalOperator CombineWith { get; set; } = LogicalOperator.And;
    public List<FilterOption<T>> Filters { get; set; }
}
public class FilterOption<T>
{
    public T Value { get; set; }
    public ComparisonOperator ComparisonOperator { get; set; }
}
