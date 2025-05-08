using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;

namespace MVC.Infra;

public sealed class MedicinesRepo(DbContext db)
    : Repo<Medicine, MedicineData>(db, d => new(d)), IMedicinesRepo
{ }
