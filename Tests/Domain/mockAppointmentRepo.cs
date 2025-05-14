using MVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Domain;

internal class mockAppointmentRepo : mockRepo<Appointment>, IAppointmentsRepo { }
