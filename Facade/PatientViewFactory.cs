using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Facade
{
    public sealed class PatientViewFactory : AbstractViewFactory<PatientData, PatientView> {}
}
