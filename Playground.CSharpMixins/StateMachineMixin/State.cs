using System;
using System.Collections.Generic;
using System.Linq;

namespace Playground.CSharpMixins.StateMachineMixin {
  public class State {
    readonly Dictionary<string, Action> _actions;
    public List<Action> Actions { get { return _actions.Values.ToList(); } }
    public string Name { get; private set; }

    public State(string name) {
      Name = name;
      _actions = new Dictionary<string, Action>();
    }

    public State Add(Action action) {
      _actions.Add(action.Rel, action);
      return this;
    }

    public List<Action> Invoke(string rel) {
      return _actions[rel].NextState.Actions;
    }
  }
}