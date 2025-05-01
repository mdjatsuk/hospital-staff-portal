using MVC.Data;
using MVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Facade
{
    [DisplayName(medicalRecords)]
    public sealed class MedicalRecordView : EntityView
    {
        [Display(Name = patient)] public int PatientId { get; set; }
        [Display(Name = "Description")] public string? DescriptionId { get; set; }
        [Display(Name = "Description")] public string? Description { get; set; }
        [Display(Name = patient)] public string? Patient { get; set; }
        [Display(Name = "Diagnosis")] public Diagnoses? Diagnos { get; set; }
        [Display(Name = "Description")] public string? DescriptionName { get; set; }
        [Display(Name = "Patient")] public string? PatientFullName { get; set; }
        [Display(Name = "Diagnosed on"), Required, DataType(DataType.Date)] public DateTime? DiagnosedOn { get; set; }

    }
}
