using MVC.Soft.Data;
using MVC.Soft.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Soft.Data.Seeding;

[TestClass] public class OpenAiGeneratorTests : BaseTests
{
    protected override Type setType() => typeof(OpenAiGenerator);
    [TestMethod]
    public async Task OpenAiGeneratorTest()
    {
        bool called = false;
        OpenAiGenerator generator = async (count) =>
        {
            called = true;
            await Task.Yield();
            return new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object> { { "Value", count } }
                };
        };
        var config = new EntityGenerationConfig { OpenAiGenerator = generator };
        var result = await config.OpenAiGenerator!(5);
        isTrue(called);
        equal(1, result.Count);
        equal(5, result[0]["Value"]);
    }
}