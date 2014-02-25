using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Playground.CSharpMixins {
  public interface IStopWatchMixin { }

  public static class StopWatchMixin {
    static readonly ConditionalWeakTable<IStopWatchMixin, Stopwatch> _table;

    static StopWatchMixin() {
      _table = new ConditionalWeakTable<IStopWatchMixin, Stopwatch>();
    }
    
    public static void StartTimer(this IStopWatchMixin target) {
      _table.GetOrCreateValue(target).Start();
    }

    public static TimeSpan StopTimer(this IStopWatchMixin target) {
      Stopwatch stopwatch;
      if(_table.TryGetValue(target, out stopwatch)) {
        stopwatch.Stop();
       return stopwatch.Elapsed;
      }
      throw new Exception("Stopwatch was never created!");
    }
  }
}