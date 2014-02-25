//using System;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;

//namespace Playground.CSharpMixins {
//  public interface IncludeStopWatch { }

//  public static class StopWatchMixinProvider {
//    static readonly ConditionalWeakTable<IncludeStopWatch, StopWatchMixinData> _table = new ConditionalWeakTable<IncludeStopWatch, StopWatchMixinData>();
    
//    sealed class StopWatchMixinData {
//      internal readonly Stopwatch stopwatch;

//      public StopWatchMixinData(Stopwatch stopwatch) {
//        this.stopwatch = stopwatch;
//      }
//    }

//    public static void StartTimer(this IncludeStopWatch map) {
//      StopWatchMixinData stopWatchMixinData;
//      var exists = _table.TryGetValue(map, out stopWatchMixinData);
//      if (exists) {
//        _table.GetOrCreateValue(map).stopwatch.Reset();
//      }
//      else {
//        _table.Add(map, new StopWatchMixinData(new Stopwatch()));
//      }
//      _table.GetOrCreateValue(map).stopwatch.Start();
//    }

//    public static TimeSpan StopTimer(this IncludeStopWatch map) {
//      StopWatchMixinData stopWatchMixinData;
//      _table.TryGetValue(map, out stopWatchMixinData);
//      stopWatchMixinData.stopwatch.Stop();
//      return getElipsedTime(map);
//    }

//    public static TimeSpan ElapsedTime(this IncludeStopWatch map) {
//      return getElipsedTime(map);
//    }

//    static TimeSpan getElipsedTime(IncludeStopWatch map) {
//      StopWatchMixinData stopWatchMixinData;
//      _table.TryGetValue(map, out stopWatchMixinData);
//      return stopWatchMixinData.stopwatch.Elapsed;
//    }

//    public static long ElapsedTicks(this IncludeStopWatch map) {
//      StopWatchMixinData stopWatchMixinData;
//      _table.TryGetValue(map, out stopWatchMixinData);
//      return stopWatchMixinData.stopwatch.ElapsedTicks;
//    }
//  }
//}