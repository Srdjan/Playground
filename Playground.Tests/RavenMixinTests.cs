using NUnit.Framework;
using Playground.CSharpMixins.RavenDbMixin;
using Playground.CSharpMixins.StopWatchMixin;

namespace Playground.Tests {
  public class Apple {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal Weight { get; set; }
  }

  public class Service : IRavenDbMixin, IStopWatchMixin { }

  public class RavenMixinTests {
    [Test]
    public void how_can_this_possibly_pass() {
      var service = new Service();
      service.StartTimer();

      var apple = service.QuerySingleOrDefault<Apple>(a => a.Name == "Kanzi");
      if (apple == null) {
        service.Store(new Apple() { Id = 0, Name = "Kanzi" });
        service.Save();
      }

      service.StopTimer();

      var apple1 = service.Load<Apple>(1);
      Assert.IsNotNull(apple1);
      Assert.IsTrue(apple1.Name == "Kanzi");
    }
  }
}