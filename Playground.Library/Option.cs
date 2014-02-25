using System;

namespace Playground.Library {
  public sealed class Option<T> {
    readonly bool _hasValue;
    readonly T _value;

    public Option(T value) {
      _hasValue = true;
      _value = value;
    }

    Option() {
      _hasValue = false;
      _value = default(T);
    }

    public static Option<T> None { get { return new Option<T>(); } }

    public bool HasValue { get { return _hasValue; } }

    public T Value {
      get {
        if (!HasValue) {
          throw new InvalidOperationException("Option does not have a value");
        }
        return _value;
      }
    }

    public static implicit operator Option<T>(Option option) {
      return None;
    }
  }

  public sealed class Option {
    static readonly Option empty = new Option();

    Option() {}

    public static Option None { get { return empty; } }

    public static Option<T> Create<T>(T value) {
      return new Option<T>(value);
    }
  }
}