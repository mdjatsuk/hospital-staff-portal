using MVC.Domain;

namespace MVC.Tests.Domain;

internal class mockAppointmentRepo : mockRepo<Appointment>, IAppointmentsRepo { }
