using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Playground.CSharpMixins.StateMachineMixin {
  public static class StateMachinehDefinitions {
    static readonly ConcurrentDictionary<string, List<State>> _stateMachines = new ConcurrentDictionary<string, List<State>>();

    public static void Configure(string name, List<State> states) {
      if (_stateMachines.ContainsKey(name)) {
        update(name, states);
      }
      else {
        add(name, states);
      }
    }

    public static State GetFirstState(string name) {
      return _stateMachines[name].First();
    }
    
    static void update(string name, List<State> states) {
      _stateMachines.TryUpdate(name, states, _stateMachines[name]);
    }

    static void add(string name, List<State> states) {
      _stateMachines.TryAdd(name, states);
    }
  }
}