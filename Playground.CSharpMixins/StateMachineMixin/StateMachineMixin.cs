using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Playground.CSharpMixins.StateMachineMixin {
  public interface IStateMachinehMixin { }

  public static class StateMachinehMixin {
    //todo; this is temporary, this mixing needs real persistance
    static readonly ConcurrentDictionary<string, StateMachine> _resourcesStateMachines = new ConcurrentDictionary<string, StateMachine>(); 
    static readonly ConditionalWeakTable<IStateMachinehMixin, StateMachine> _table;

    static StateMachinehMixin() {
      _table = new ConditionalWeakTable<IStateMachinehMixin, StateMachine>();
    }

    public static void Configure(this IStateMachinehMixin target, string name, List<State> states) {
      var stateMachine = new StateMachine();
      stateMachine.Configure(states);
      if (_resourcesStateMachines.ContainsKey(name)) {
        _resourcesStateMachines.TryUpdate(name, stateMachine, _resourcesStateMachines[name]);
      }
      else {
        _resourcesStateMachines.TryAdd(name, stateMachine);
      }
    }

    public static List<Action> Invoke(this IStateMachinehMixin target, string name, string action) {
      StateMachine stateMachine;
      var result = _table.TryGetValue(target, out stateMachine);
      if (! result) {
        stateMachine = _resourcesStateMachines[name];
        _table.Add(target, stateMachine);
      }
      return stateMachine.Invoke(action);
    }
  }
}