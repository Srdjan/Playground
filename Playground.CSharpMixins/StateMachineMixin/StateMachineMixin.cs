using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Playground.CSharpMixins.StateMachineMixin {
  public interface IStateMachinehMixin { }

  public static class StateMachinehMixin {
    static readonly ConditionalWeakTable<IStateMachinehMixin, StateMachine> _table;

    static StateMachinehMixin() {
      _table = new ConditionalWeakTable<IStateMachinehMixin, StateMachine>();
    }

    public static List<Action> Invoke(this IStateMachinehMixin target, string name, string action) {
      StateMachine stateMachine;
      var result = _table.TryGetValue(target, out stateMachine);
      if (result) {
        return stateMachine.Invoke(action);
      }
      stateMachine = StateMachinehDefinitions.Get(name);
      _table.Add(target, stateMachine);
      return stateMachine.Invoke(action);
    }
  }
}