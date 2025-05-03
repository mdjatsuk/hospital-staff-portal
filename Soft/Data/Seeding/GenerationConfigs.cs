using MVC.Aids.Attributes;
using MVC.Data;

namespace MVC.Soft.Data.Seeding;
public static class GenerationConfigs
{
    public static EntityGenerationConfig Get<TEntity>(OpenAiService openAi)
    {
        return typeof(TEntity) switch
        {
            var t when t == typeof(PatientData) => new EntityGenerationConfig
            {
                PropertyRules = new()
                {
                    new PropertyRule("FirstName", GeneratorType.OpenAi),
                    new PropertyRule("LastName", GeneratorType.OpenAi),
                    new PropertyRule("Gender", GeneratorType.OpenAi),
                    new PropertyRule("DateOfBirth", GeneratorType.Random, () =>
                        MVC.Aids.Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now))
                },
                OpenAiGenerator = async count =>
                {
                    var data = await openAi.GenerateRandomNamesAndGendersAsync(count);
                    return data.Select(d => new Dictionary<string, object>
                    {
                        { "FirstName", d.fullName.Split(" ")[0] },
                        { "LastName", d.fullName.Split(" ")[1] },
                        { "Gender", d.gender }
                    }).ToList();
                }
            },

            var t when t == typeof(DoctorData) => new EntityGenerationConfig
            {
                PropertyRules = new()
                {
                    new PropertyRule("FirstName", GeneratorType.OpenAi),
                    new PropertyRule("LastName", GeneratorType.OpenAi),
                    new PropertyRule("Specialization", GeneratorType.Random, () =>
                        (Specialities?)MVC.Aids.Random.EnumOf(typeof(Specialities))),
                    new PropertyRule("PhoneNumber", GeneratorType.Random, () =>
                        MVC.Aids.Random.Int64(10000000, 99999999)),
                    new PropertyRule("EmailAddress", GeneratorType.OpenAi)
                },
                OpenAiGenerator = async count =>
                {
                    var data = await openAi.GenerateRandomNamesAndGendersAsync(count);
                    return data.Select(d =>
                    {
                        var first = d.fullName.Split(" ")[0];
                        var last = d.fullName.Split(" ")[1];
                        return new Dictionary<string, object>
                        {
                            { "FirstName", first },
                            { "LastName", last },
                            { "EmailAddress", EmailGenerator.Generate(first, last) }
                        };
                    }).ToList();
                }
            },


            var t when t == typeof(DiagnosisData) => new EntityGenerationConfig
            {
                PropertyRules = new()
                {
                    new PropertyRule("DiagnosisName", GeneratorType.OpenAi),
                    new PropertyRule("Description", GeneratorType.OpenAi),
                    new PropertyRule("RequiresSurgery", GeneratorType.Random, () =>
                        MVC.Aids.Random.Boolean())
                },
                OpenAiGenerator = async count =>
                {
                    var data = await openAi.GenerateRandomDiagnosisDescriptionsAsync(count);
                    return data.Select(d => new Dictionary<string, object>
                    {
                        { "DiagnosisName", d.diagnosis},
                        { "Description", d.description}
                    }).ToList();
                }
            },

            var t when t == typeof(AppointmentData) => new EntityGenerationConfig
            {
                PropertyRules = new()
                {
                    new PropertyRule("DoctorId", GeneratorType.ReferenceId),
                    new PropertyRule("PatientId", GeneratorType.ReferenceId),
                    new PropertyRule("Date", GeneratorType.Random, () => 
                        MVC.Aids.Random.DateTime(DateTime.Now, DateTime.Now.AddYears(5))),
                    new PropertyRule("Room", GeneratorType.OpenAi),
                    new PropertyRule("AppointmentFee", GeneratorType.Random, () =>
                        MVC.Aids.Random.Int32(5, 100)),
                    new PropertyRule("DoctorFullName", GeneratorType.ReferenceId),
                    new PropertyRule("PatientFullName", GeneratorType.ReferenceId)
                },
                OpenAiGenerator = async count =>
                {
                    var data = await openAi.GenerateRandomRoomsAsync(count);
                    return data.Select(d => new Dictionary<string, object>
                    {
                        { "Room", d }
                    }).ToList();
                }

            },

            var t when t == typeof(MedicalRecordData) => new EntityGenerationConfig
            {
                PropertyRules = new()
                {
                    new PropertyRule("DescriptionId", GeneratorType.ReferenceId),
                    new PropertyRule("PatientId", GeneratorType.ReferenceId),
                    new PropertyRule("DiagnosedOn", GeneratorType.Random, () => 
                        MVC.Aids.Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now)),
                    new PropertyRule("Diagnosis", GeneratorType.Random,() =>
                        (Diagnoses?)MVC.Aids.Random.EnumOf(typeof(Diagnoses))),
                    new PropertyRule("DescriptionName", GeneratorType.ReferenceId),
                    new PropertyRule("PatientFullName", GeneratorType.ReferenceId)
                }
            },

            _ => throw new NotSupportedException($"No config for type {typeof(TEntity).Name}")
        };
    }
}