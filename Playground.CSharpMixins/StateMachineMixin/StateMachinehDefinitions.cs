using System.Collections.Concurrent;

namespace Playground.CSharpMixins.StateMachineMixin {
  internal static class StateMachinehDefinitions {
    //todo; this is temporary, this mixing needs real persistance
    static readonly ConcurrentDictionary<string, StateMachine> _resourcesStateMachines =
      new ConcurrentDictionary<string, StateMachine>();

    public static bool ContainsKey(string name) {
      return _resourcesStateMachines.ContainsKey(name);
    }

    public static void Update(string name, StateMachine stateMachine) {
      _resourcesStateMachines.TryUpdate(name, stateMachine, _resourcesStateMachines[name]);
    }

    public static void Add(string name, StateMachine stateMachine) {
      _resourcesStateMachines.TryAdd(name, stateMachine);
    }

    public static StateMachine Get(string name) {
      return _resourcesStateMachines[name];
    }
  }
}