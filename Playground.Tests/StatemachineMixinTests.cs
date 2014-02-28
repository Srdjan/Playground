using System.Collections.Generic;
using NUnit.Framework;
using Playground.CSharpMixins.StateMachineMixin;

namespace Playground.Tests {
  abstract class ResourceBase<T> {
    static int nextId;
    public int Id { get; private set; }

    protected ResourceBase() {
      Id = nextId++;
    }
    
    protected ResourceBase(int id) {
      Id = id;
    }

    public List<Action> Invoke(string action) {
      var stateMachine = StateMachineRuntimeStore.Get(Id, StateMachineConfigurationStore.Get(GetType().Name));
      return stateMachine.Invoke(action);
    }
  }

  internal class ApplesResource : ResourceBase<Apple> {
    public ApplesResource() { }
    public ApplesResource(int id) : base(id) { }
  }
  
  [TestFixture]
  public class StatemachineMixinTests {
    [TestFixtureSetUp]
    public void SetupStates() {
      var states = new List<State>();

      var initialState = new State("none");
      states.Add(initialState);
      var growingState = new State("growing");
      states.Add(growingState);
      var harvestedState = new State("harvested");
      states.Add(harvestedState);
      var consumingState = new State("consuming");
      states.Add(consumingState);
      var goneState = new State("eaten");
      states.Add(goneState);

      initialState.Add(new Action("post", "create", "/apples", growingState));
      growingState.Add(new Action("put", "change-color", "/apples/{id}", growingState));
      growingState.Add(new Action("put", "add-weight", "/apples/{id}", growingState));
      growingState.Add(new Action("put", "harvest", "/apples/{id}", harvestedState));
      harvestedState.Add(new Action("put", "eat", "/apples/{id}", consumingState));
      consumingState.Add(new Action("delete", "last-bite", "/apples/{id}", goneState));

      StateMachineConfigurationStore.Configure("ApplesResource", states);
    }

    [Test]
    public void test_sucessfull_initial() {
      var applesResource = new ApplesResource();

      //.. invoke api

      var actions = applesResource.Invoke("create");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("add-weight");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("change-color");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("harvest");
      Assert.IsTrue(actions.Count == 1);
    }

    [Test]
    public void test_sucessfull_subsequent() {
      var applesResource = new ApplesResource(2);

      //.. invoke api

      var actions = applesResource.Invoke("create");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("add-weight");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("change-color");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("harvest");
      Assert.IsTrue(actions.Count == 1);
    }
  }
}