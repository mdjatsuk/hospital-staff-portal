using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Soft.Data;
using MVC_Project.Soft.Models;

namespace MVC_Project.Soft.Controllers;

public class AppointmentDatasController(ApplicationDbContext c) : BaseController<AppointmentData>(c) { }

