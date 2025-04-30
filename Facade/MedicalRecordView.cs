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
    [DisplayName("Medical Record")]
    public sealed class MedicalRecordView : EntityView
    {
        [Display(Name = diagnosis)] public int DiagnosisId { get; set; }
        [Display(Name = patient)] public int PatientId { get; set; }
        [Display(Name = diagnosis)] public string? Diagnosis { get; set; }
        [Display(Name = patient)] public string? Patient { get; set; }
        [Display(Name = "Diagnosed on"), Required, DataType(DataType.Date)] public DateTime? DiagnosedOn { get; set; }

    }
}
