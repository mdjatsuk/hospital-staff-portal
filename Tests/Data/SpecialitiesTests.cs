using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Data;

[TestClass] public class SpecialitiesTests() : EnumTests<Specialities>(10)
{
    [TestMethod] public void NephrologyTest() => isEnum(0);
    [TestMethod] public void EndocrinologyTest() => isEnum(1);
    [TestMethod] public void AllergyImmunologyTest() => isEnum(2);
    [TestMethod] public void PulmonologyTest() => isEnum(3);
    [TestMethod] public void CardiologyTest() => isEnum(4);
    [TestMethod] public void InterventionalCardiologyTest() => isEnum(5);
    [TestMethod] public void InfectiousDiseaseTest() => isEnum(6);
    [TestMethod] public void NeurologyTest() => isEnum(7);
    [TestMethod] public void RheumatologyTest() => isEnum(8);
    [TestMethod] public void GastroenterologyTest() => isEnum(9);
}