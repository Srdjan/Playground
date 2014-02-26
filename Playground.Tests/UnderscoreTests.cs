using System.Collections.Generic;
using NUnit.Framework;
using Playground.CSharpUnderscore;

namespace Playground.Tests {
  public class UnderscoreTests {
    [Test]
    public void test_sucessfull_found() {
      var list = new List<decimal>() { 0.1m, 1.1m, 2.2m, 3.2m, 4.1m, 5.0m, 6.7m, 7.4m };
      var result = _.find(list, i => i > 3.0m);

      Assert.IsTrue(result.Value == 3.2m);
    }

    [Test]
    public void test_sucessfull_not_found() {
      var list = new List<decimal>() { 0.1m, 1.1m, 2.2m, 3.2m, 4.1m, 5.0m, 6.7m, 7.4m };
      var result = _.find(list, i => i > 13.0m);

      Assert.IsFalse(result.HasValue);
    }
  }
}