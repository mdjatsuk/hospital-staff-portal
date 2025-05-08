using MVC.Data;
using MVC.Domain;
using MVC.Facade;
using MVC.Soft.Data;

namespace MVC.Soft.Controllers;

public class MedicinesController(ApplicationDbContext c)
    : BaseController<Medicine, MedicineData, MedicineView>(c, new MedicineViewFactory(), d => new(d)) {}