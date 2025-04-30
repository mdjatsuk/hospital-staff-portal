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
            DiagnosisName = Diagnoses.Tuberculosis,
            Description = "Test Description",
            RequiresSurgery = true
        };
        return new Diagnosis(d);
    }
    [TestMethod] public void DiagnosisNameTest() => equal(Diagnoses.Tuberculosis, obj?.DiagnosisName);
    [TestMethod] public void DescriptionTest() => equal("Test Description", obj?.Description);
    [TestMethod] public void RequiresSurgeryTest() => equal(true, obj?.RequiresSurgery);
    [TestMethod] public void IdTest() => equal(1, obj?.Id);
    [TestMethod] public void DataTest() => notNull(obj?.data);
}
