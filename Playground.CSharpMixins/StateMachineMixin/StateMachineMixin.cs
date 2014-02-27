using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Playground.CSharpMixins.StateMachineMixin {
  public interface IStateMachinehMixin { }

  public static class StateMachinehMixin {
    static readonly ConditionalWeakTable<IStateMachinehMixin, State> _table;

    static StateMachinehMixin() {
      _table = new ConditionalWeakTable<IStateMachinehMixin, State>();
    }

    public static List<Action> Invoke(this IStateMachinehMixin target, string name, string action) {
      State state;
      var result = _table.TryGetValue(target, out state);
      if (result) {
        return state.Invoke(action).Actions;
      }
      state = StateMachinehDefinitions.GetFirstState(name);
      _table.Add(target, state);
      return state.Invoke(action).Actions;
    }
  }
}