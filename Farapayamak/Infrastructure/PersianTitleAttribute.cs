namespace Farapayamak.Infrastructure;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PersianTitleAttribute : Attribute
{
    private string _title;
    public PersianTitleAttribute(string title)
    {
        _title = title;
    }

    public string Title => _title;
}