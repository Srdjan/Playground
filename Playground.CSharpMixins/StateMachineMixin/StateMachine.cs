using System;
using System.Collections.Generic;

namespace Playground.CSharpMixins.StateMachineMixin {
  internal class StateMachine {
    readonly List<State> _states;
    State _currentState;

    public StateMachine(List<State> states) {
      if (_states.Count == 0) {
        throw new Exception("At least one state required");
      }
      _states = states;
      _currentState = _states[0];
    }

    public List<Action> Invoke(string action) {
      _currentState = _currentState.Invoke(action);
      return _currentState.Actions;
    }
  }
}