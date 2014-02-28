using System;
using System.Collections.Generic;
using Playground.CSharpUnderscore;

namespace Playground.Play {
  class Program {
    static void Main(string[] args) {
      var list = new List<double>() { 0.1, 1.1, 2.2, 3.2, 4.1, 5.0, 6.7, 7.4 };
      _.each(list, i => print(i));

      var list1 = _.map(list, Math.Truncate);
      _.each(list1, i => print(i));

      var list2 = _.reduce(list1, i => i < 12);
      _.each(list2, i => print(i));

      Console.ReadLine();
    }

    static void print(dynamic data) {
      Console.WriteLine("{0}", data.ToString());
    }
  }
}

