using System.Collections.Generic;
using NUnit.Framework;
using Playground.CSharpMixins.StateMachineMixin;

namespace Playground.Tests {
  abstract class ResourceBase<T> { }

  internal class ApplesResource : ResourceBase<Apple>, IStateMachinehMixin {
    public static void ConfigureStates(List<State> states) {
      StateMachinehDefinitions.Configure("apples", states);
    }
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

      ApplesResource.ConfigureStates(states);
    }

    [Test]
    public void test_sucessfull_found() {
      var applesResource = new ApplesResource();

      //.. invoke api

      var actions = applesResource.Invoke("apples", "create");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("apples", "add-weight");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("apples", "change-color");
      Assert.IsTrue(actions.Count == 3);

      actions = applesResource.Invoke("apples", "harvest");
      Assert.IsTrue(actions.Count == 1);
    }
  }
}