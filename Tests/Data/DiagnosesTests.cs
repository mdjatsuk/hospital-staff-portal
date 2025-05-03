using MVC.Data;

namespace MVC.Tests.Data;

[TestClass] public class DiagnosesTests() : EnumTests<Diagnoses>(12)
{
    [TestMethod] public void UnknownTest() => isEnum(0);
    [TestMethod] public void HypertensionTest() => isEnum(1);
    [TestMethod] public void DiabetesTest() => isEnum(2);

    [TestMethod] public void AsthmaTest() => isEnum(3);

    [TestMethod] public void EpilepsyTest() => isEnum(4);

    [TestMethod] public void PneumoniaTest() => isEnum(5);

    [TestMethod] public void TuberculosisTest() => isEnum(6);

    [TestMethod] public void OsteoarthritisTest() => isEnum(7);

    [TestMethod] public void MigraineTest() => isEnum(8);

    [TestMethod] public void AnemiaTest() => isEnum(9);

    [TestMethod] public void GastricUlcerTest() => isEnum(10);

    [TestMethod] public void HepatitisTest() => isEnum(11);
}