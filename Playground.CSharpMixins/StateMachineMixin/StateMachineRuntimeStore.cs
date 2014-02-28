using System.Collections.Concurrent;

namespace Playground.CSharpMixins.StateMachineMixin {
 public static class StateMachineRuntimeStore {
    static readonly ConcurrentDictionary<int, StateMachine> _stateMachines = new ConcurrentDictionary<int, StateMachine>();

    public static StateMachine Get(int id, StateMachine stateMachine) {
      if (! _stateMachines.ContainsKey(id)) {
        _stateMachines.TryAdd(id, stateMachine);
      }
      return _stateMachines[id];
    }
 }
}