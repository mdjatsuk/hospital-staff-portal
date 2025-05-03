namespace MVC.Soft.Data.Seeding;

public class PropertyRule
{
    public string PropertyName { get; }
    public GeneratorType GeneratorType { get; }
    public Func<object?>? RandomGenerator { get; }

    public PropertyRule(string propertyName, GeneratorType generatorType, Func<object?>? randomGenerator = null)
    {
        PropertyName = propertyName;
        GeneratorType = generatorType;
        RandomGenerator = randomGenerator;
    }
}
