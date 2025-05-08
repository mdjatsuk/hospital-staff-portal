using MVC.Data;

namespace MVC.Tests.Data;

[TestClass] public class DiagnosesTests() : EnumTests<Diagnoses>(11)
{
    [TestMethod] public void HypertensionTest() => isEnum(0);
    [TestMethod] public void DiabetesTest() => isEnum(1);

    [TestMethod] public void AsthmaTest() => isEnum(2);

    [TestMethod] public void EpilepsyTest() => isEnum(3);

    [TestMethod] public void PneumoniaTest() => isEnum(4);

    [TestMethod] public void TuberculosisTest() => isEnum(5);

    [TestMethod] public void OsteoarthritisTest() => isEnum(6);

    [TestMethod] public void MigraineTest() => isEnum(7);

    [TestMethod] public void AnemiaTest() => isEnum(8);

    [TestMethod] public void GastricUlcerTest() => isEnum(9);

    [TestMethod] public void HepatitisTest() => isEnum(10);
}