namespace Playground.CSharpMixins.StateMachineMixin {
  public class Action {
    public string Method { get; private set; }
    public string Rel { get; private set; }
    public string Url { get; private set; }
    public State NextState { get; private set; }

    public Action(string method, string rel, string url, State next) {
      Method = method;
      Rel = rel;
      Url = url;
      NextState = next;
    }
  }
}