using System;
using System.Collections.Generic;
using System.Linq;
using Playground.Library;

namespace Playground.CSharpUnderscore {
  //todo: implement pipre oparator using ConditionalWeakTable?
  //  var list1 = _.map(list, i => Math.Truncate(i)).each(i => print(i));
  //  var list1 = _.map(list, i => Math.Truncate(i)).each(...)
  public static class _ {
    public static void each<T>(IEnumerable<T> enumerable, Action<T> action) {
      foreach (var _item in enumerable) {
        action(_item);
      }
    }

    public static Option<T> find<T>(IEnumerable<T> enumerable, Predicate<T> func) {
      foreach (var _item in enumerable.Where(_item => func(_item))) {
        return Option.Create(_item);
      }
      return Option.None;
    }

    public static IEnumerable<R> map<T, R>(IEnumerable<T> enumerable, Func<T, R> func) {
      return enumerable.Select(func).ToList();
    }

    public static IEnumerable<T> reduce<T>(IEnumerable<T> enumerable, Predicate<T> func) {
      return enumerable.Where(_item => func(_item)).ToList();
    }
  }
}