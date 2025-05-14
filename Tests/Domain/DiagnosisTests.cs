using MVC.Data;
using MVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Domain;

[TestClass]
public class DiagnosisTests : SealedTests<Diagnosis, Entity<DiagnosisData>>
{
    protected override Diagnosis createObj()
    {
        var d = new DiagnosisData
        {
            Id = 1,
            Medicine = "Paracetamol",
            Description = "Test Description",
        };
        return new Diagnosis(d);
    }
    [TestMethod] public void MedicineNameTest() => equal("Paracetamol", obj?.MedicineName);
    [TestMethod] public void DescriptionTest() => equal("Test Description", obj?.Description);
    [TestMethod] public void IdTest() => equal(1, obj?.Id);
    [TestMethod] public void DataTest() => notNull(obj?.data);
}
