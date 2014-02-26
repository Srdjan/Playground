using System;
using System.Collections.Generic;

namespace Playground.CSharpMixins.StateMachineMixin {
  internal class StateMachine {
    readonly List<State> _states;
    State _currentState;

    public StateMachine() {
      _currentState = null;
      _states = new List<State>();
    }

    public void Configure(List<State> states) {
      if (states.Count == 0) {
        throw new Exception("At least one state required");
      }
      _states.Clear();
      _states.AddRange(states);
      _currentState = _states[0];
    }

    public List<Action> Invoke(string action) {
      return _currentState.Invoke(action);
    }
  }
}