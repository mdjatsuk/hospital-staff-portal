using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Tests;

public abstract class ClassTests<TClass, TBaseClass> : BaseClassTests<TClass, TBaseClass>
    where TClass : class, new()
    where TBaseClass : class
{
    protected override TClass createObj() => new();
}

