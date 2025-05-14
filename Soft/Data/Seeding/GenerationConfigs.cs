using MVC.Aids.Attributes;
using MVC.Data;
using Random = MVC.Aids.Random;

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
                        Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now))
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
                        (Specialities?)Random.EnumOf(typeof(Specialities))),
                    new PropertyRule("PhoneNumber", GeneratorType.Random, () =>
                        Random.Int64(50000000, 59999999)),
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
                    new PropertyRule("RecordNr", GeneratorType.Random,() =>
                        RecordNrGenerator.GenerateRecordNr()),
                    new PropertyRule("Diagnosis", GeneratorType.OpenAi),
                    new PropertyRule("Description", GeneratorType.OpenAi),
                    new PropertyRule("Medicine", GeneratorType.OpenAi),
                    new PropertyRule("RequiresPrescription", GeneratorType.Random, () =>
                        Random.Boolean())
                },
                OpenAiGenerator = async count =>
                {
                    var data = await openAi.GenerateRandomMedicinesAndDescriptionsAsync(count);
                    return data.Select(d => new Dictionary<string, object>
                    {
                        { "Diagnosis", d.diagnosis },
                        { "Description", d.description },
                        { "Medicine", d.medicine }
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
                        Random.DateTime(DateTime.Now, DateTime.Now.AddYears(5))),
                    new PropertyRule("Room", GeneratorType.OpenAi),
                    new PropertyRule("AppointmentFee", GeneratorType.Random, () =>
                        Random.Int32(5, 100)),
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
                    new PropertyRule("PatientId", GeneratorType.ReferenceId),
                    new PropertyRule("RecordNrId", GeneratorType.ReferenceId),
                    new PropertyRule("DiagnosedOn", GeneratorType.Random, () => 
                        Random.DateTime(DateTime.Now.AddYears(-60), DateTime.Now)),
                    new PropertyRule("PatientFullName", GeneratorType.ReferenceId),
                    new PropertyRule("RecordNr", GeneratorType.ReferenceId),
                }
            },

            _ => throw new NotSupportedException($"No config for type {typeof(TEntity).Name}")
        };
    }
}