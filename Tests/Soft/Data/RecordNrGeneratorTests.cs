using MVC.Soft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVC.Tests.Soft.Data;

[TestClass] public class RecordNrGeneratorTests : BaseTests
{
    protected override Type setType() => typeof(RecordNrGenerator);
    [TestMethod] public void GenerateRecordNrTest()
    {
        var set = new HashSet<string>();
        for (int i = 0; i < 100; i++)
        {
            var recordNr = RecordNrGenerator.GenerateRecordNr();
            isFalse(set.Contains(recordNr), $"Duplicate record number generated: {recordNr}");
            set.Add(recordNr);
        }
    }
}
