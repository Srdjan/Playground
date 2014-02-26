using System.Collections.Generic;
using NUnit.Framework;
using Playground.CSharpMixins.StateMachineMixin;

namespace Playground.Tests {
  internal class ApplesResource : ResourceBase<Apple>, IStateMachinehMixin {
    public override void ConfigureStates() {
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

      this.Configure("apples", states);
    }
  }


  abstract class ResourceBase<T> {
    abstract public void ConfigureStates();
  }

  [TestFixture]
  public class StatemachineMixinTests {
    List<State> _states;

    [TestFixtureSetUp]
    public void SetupStates() {
      _states = new List<State>();

      var initialState = new State("initial");
      _states.Add(initialState);
      var growingState = new State("growing");
      _states.Add(growingState);
      var harvestState = new State("harvest");
      _states.Add(harvestState);

      initialState.Add(new Action("post", "create", "/apples", growingState));
      growingState.Add(new Action("put", "change-color", "/apples/{id}", growingState));
      growingState.Add(new Action("put", "add-weight", "/apples/{id}", growingState));
      growingState.Add(new Action("put", "ready", "/apples/{id}", harvestState));
    }

    [Test]
    public void test_sucessfull_found() {
      var applesResource = new ApplesResource();

      //.. invoke post

      var actions = applesResource.Invoke("apples", "create");

      Assert.IsTrue(actions.Count == 3);
    }
  }
}