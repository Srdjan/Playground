using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Playground.CSharpMixins.StopWatchMixin {
  public interface IStopWatchMixin { }

  public static class StopWatchMixinProvider {
    static readonly ConditionalWeakTable<IStopWatchMixin, Stopwatch> _table;

    static StopWatchMixinProvider() {
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