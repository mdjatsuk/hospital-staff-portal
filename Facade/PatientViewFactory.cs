using MVC.Data;
using MVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Facade
{
    public sealed class PatientViewFactory : AbstractViewFactory<PatientData, PatientView> 
    {
        public override async Task<PatientView> CreateView(PatientData? d, bool loadLazy = false)
        {
            var v = await base.CreateView(d, loadLazy);
            if (!loadLazy) return v;
            var o = new Patient(d);
            await o.LoadLazy();
            v.Diagnosis = o.Diagnosis?.DiagnosisName;
            return v;
        }
    }
}
