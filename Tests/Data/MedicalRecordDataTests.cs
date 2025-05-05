using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class MedicalRecordDataTests : SealedTests<MedicalRecordData, EntityData<MedicalRecordData>>
{
    [TestMethod] public void PatientIdTest() => isProperty<int>();
    [TestMethod] public void DescriptionIdTest() => isProperty<int>();
    [TestMethod] public void DiagnosedOnTest() => isProperty<DateTime?>();
    [TestMethod] public void DiagnosisTest() => isProperty<Diagnoses?>();
    [TestMethod] public void DescriptionNameTest() => isProperty<string>();
    [TestMethod] public void PatientFullNameTest() => isProperty<string>();

    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        if (obj == null) return;
        obj.PatientId = 1;
        obj.DescriptionId = 2;
        obj.DiagnosedOn = DateTime.Today;
        obj.DescriptionName = "Feeling dizzy and lightheaded";
        obj.PatientFullName = "Liam Thompson";
    }
    [TestMethod] public void CloneTest()
    {
        var d = obj?.Clone();
        notNull(d);
        equal(1, d?.PatientId);
        equal(2, d?.DescriptionId);
        equal(DateTime.Today, d?.DiagnosedOn);
        equal("Feeling dizzy and lightheaded", d?.DescriptionName);
        equal("Liam Thompson", d?.PatientFullName);
    }
}