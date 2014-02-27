using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Playground.CSharpMixins.StateMachineMixin {
  public static class StateMachinehDefinitions {
    static readonly ConcurrentDictionary<string, StateMachine> _stateMachines = new ConcurrentDictionary<string, StateMachine>();

    public static void Configure(string name, List<State> states) {
      var stateMachine = new StateMachine(states);
      if (_stateMachines.ContainsKey(name)) {
        update(name, stateMachine);
      }
      else {
        add(name, stateMachine);
      }
    }

    public static StateMachine Get(string name) {
      return _stateMachines[name];
    }

    static void update(string name, StateMachine stateMachine) {
      _stateMachines.TryUpdate(name, stateMachine, _stateMachines[name]);
    }

    static void add(string name, StateMachine stateMachine) {
      _stateMachines.TryAdd(name, stateMachine);
    }
  }
}