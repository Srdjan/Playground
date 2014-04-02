using System;
using System.Collections.Generic;

namespace Playground.CSharpMixins.StateMachineMixin {
  public class StateMachine {
    public State CurrentState { get; private set; }

    public StateMachine(IReadOnlyList<State> states) {
      if (states.Count == 0) {
        throw new Exception("At least one state required");
      }
      CurrentState = states[0];
    }

    public List<Action> Invoke(string action) {
      CurrentState = CurrentState.Invoke(action);
      return CurrentState.Actions;
    }
  }
}