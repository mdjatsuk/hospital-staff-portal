using MVC.Domain;
using MVC.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Domain;

internal class mockMedicalRecordRepo : mockRepo<MedicalRecord>, IMedicalRecordsRepo { }
