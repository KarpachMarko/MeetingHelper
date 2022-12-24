namespace Base.Domain;

public class MultiLangString : Dictionary<string, string>
{
    private const string DefaultCulture = "en-US";

    public MultiLangString()
    {
    }

    public MultiLangString(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
    }

    public MultiLangString(string value, string culture)
    {
        this[culture] = value;
    }

    public string? Translate(string? culture = null)
    {
        if (Count == 0) return null;

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        if (ContainsKey(culture))
        {
            return this[culture];
        }

        var neutralCulture = culture.Split("-")[0];
        if (ContainsKey(neutralCulture))
        {
            return this[neutralCulture];
        }


        if (ContainsKey(DefaultCulture))
        {
            return this[DefaultCulture];
        }

        return null;
    }

    public void SetTranslation(string value)
    {
        this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    }
    
    public void SetTranslation(string value, string culture)
    {
        this[culture] = value;
    }

    public override string ToString()
    {
        return Translate() ?? "???";
    }
    
    public static implicit operator string(MultiLangString? langStr) => langStr?.ToString() ?? "null";
    
    public static implicit operator MultiLangString(string? value) => new(value);
}