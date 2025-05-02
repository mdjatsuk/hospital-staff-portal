using MVC.Data;
using MVC.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Facade
{
    [TestClass] public class MedicalRecordViewTests : SealedTests<MedicalRecordView, EntityView>
    {
        [TestMethod] public override void DisplayNameTest() => isDisplayName("Medical Records");
        [TestMethod] public void PatientIdTest() => isProperty<int>("Patient");
        [TestMethod] public void DescriptionIdTest() => isProperty<int?>("Description");
        [TestMethod] public void DescriptionTest() => isProperty<string?>("Description");
        [TestMethod] public void PatientFullNameTest() => isProperty<string?>("Patient");
        [TestMethod] public void PatientTest() => isProperty<string?>("Patient");
        [TestMethod] public void DiagnosedOnTest() => isProperty<DateTime?>("Diagnosed on", DataType.Date);
        [TestMethod] public void DiagnosTest() => isProperty<Diagnoses?>("Diagnoses");
        [TestMethod] public void DescriptionNameTest() => isProperty<string?>("Description");
        protected override Type setType() => typeof(MedicalRecordView);
        private MedicalRecordView? view;
        [TestInitialize]
        public void TestInitialize()
        {
            view = new MedicalRecordView
            {
                Id = 1,
                PatientId = 2,
                DescriptionId = 3,
                Description = "Valid Description",
                Patient = "Liam Thompson",
                DiagnosedOn = DateTime.Today.AddDays(-1),
                Diagnos = Diagnoses.Asthma,
                DescriptionName = "Feeling dizzy and lightheaded",
                PatientFullName = "Liam Thompson"
            };
        }
    }
}
