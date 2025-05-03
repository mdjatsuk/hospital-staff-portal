namespace MVC.Soft.Data.Seeding;

public class EntityGenerationConfig
{
    public List<PropertyRule> PropertyRules { get; set; } = new();
    public OpenAiGenerator? OpenAiGenerator { get; set; }
}

public delegate Task<List<Dictionary<string, object>>> OpenAiGenerator(int count);