using System;
using System.Collections.Generic;

namespace Playground.CSharpMixins.StateMachineMixin {
  public class StateMachine {
    readonly List<State> _states;
    public State CurrentState { get; private set; }

    public StateMachine(List<State> states) {
      _states = states;
      if (_states.Count == 0) {
        throw new Exception("At least one state required");
      }
      CurrentState = _states[0];
    }

    public List<Action> Invoke(string action) {
      CurrentState = CurrentState.Invoke(action);
      return CurrentState.Actions;
    }
  }
}