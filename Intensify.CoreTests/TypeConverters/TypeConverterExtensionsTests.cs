using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intensify.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intensify.Core.Tests;

[TestClass()]
public class TypeConverterExtensionsTests
{
    [TestMethod()]
    public void ConvertToTest()
    {
        double value = 1d;

        var result = value.ConvertTo<int>();

        Assert.IsTrue(result.GetType() == typeof(int));
    }
}
