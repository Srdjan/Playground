//using System.Collections.Generic;
//using System.Runtime.CompilerServices;

//namespace Playground.CSharpMixins.StateMachineMixin {
//  public interface IStateMachinehMixin { }

//  public static class StateMachinehMixin {
//    static readonly ConditionalWeakTable<IStateMachinehMixin, StateMachine> _table;

//    static StateMachinehMixin() {
//      _table = new ConditionalWeakTable<IStateMachinehMixin, StateMachine>();
//    }

//    public static List<Action> Invoke(this IStateMachinehMixin target, int id, string action) {
//      var stateMachine = StateMachineRuntimeStore.Get(id);
//      return stateMachine.Invoke(action);
//    }
//  }
//}