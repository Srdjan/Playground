using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Playground.CSharpMixins.StateMachineMixin {
 public static class StateMachineConfigurationStore {
    static readonly ConcurrentDictionary<string, StateMachine> _stateMachines = new ConcurrentDictionary<string, StateMachine>();

    public static void Configure(string resourceName, List<State> states) {
      if (_stateMachines.ContainsKey(resourceName)) {
        update(resourceName, states);
      }
      else {
        add(resourceName, states);
      }
    }

    static void update(string resourceName, IReadOnlyList<State> states) {
      _stateMachines.TryUpdate(resourceName, new StateMachine(states), _stateMachines[resourceName]);
    }

    static void add(string resourceName, IReadOnlyList<State> states) {
      _stateMachines.TryAdd(resourceName, new StateMachine(states));
    }

   public static StateMachine Get(string resourceName) {
     return _stateMachines[resourceName];
   }
 }
}