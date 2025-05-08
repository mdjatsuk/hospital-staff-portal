using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Domain;
using MVC.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests.Infra;

[TestClass] public class MedicinesRepoTests
   : RepoBaseTests<MedicinesRepo, Medicine, MedicineData>
{
    protected override Medicine? createEntity(Func<MedicineData> getData)
        => new(getData());
    protected override MedicinesRepo createObj() => new(dbContext!);
}
