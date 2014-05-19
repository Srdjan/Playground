namespace Playground.Library {
  using System;
  using System.Linq.Expressions;
  using System.Reflection;
  using System.Text;

  //also review this library: https://code.google.com/p/sharpcut/


    public static class TypeFactory {
      /// <summary>
      /// Obtains a delegate to invoke a parameterless constructor
      /// </summary>
      /// <typeparam name="TResult">The base/interface type to yield as the
      /// new value; often object except for factory pattern implementations</typeparam>
      /// <param name="type">The Type to be created</param>
      /// <returns>A delegate to the constructor if found, else null</returns>
      public static TResult Create<TResult>(this Type type) {
        var ci = getConstructor(type, Type.EmptyTypes);
        var ctor = Expression.Lambda<Func<TResult>>(Expression.New(ci)).Compile();
        return ctor();
      }

      /// <summary>
      /// Obtains a delegate to invoke a constructor which takes a parameter
      /// </summary>
      /// <typeparam name="TArg1">The type of the constructor parameter</typeparam>
      /// <typeparam name="TResult">The base/interface type to yield as the
      /// new value; often object except for factory pattern implementations</typeparam>
      /// <param name="type">The Type to be created</param>
      /// <returns>A delegate to the constructor if found, else null</returns>
      public static TResult Create<TArg1, TResult>(this Type type, TArg1 arg1) {
        var ci = getConstructor(type, typeof(TArg1));
        var param1 = Expression.Parameter(typeof(TArg1), "arg1");

        var ctor = Expression.Lambda<Func<TArg1, TResult>>(Expression.New(ci, param1), param1).Compile();
        return ctor(arg1);
      }

      /// <summary>
      /// Obtains a delegate to invoke a constructor with multiple parameters
      /// </summary>
      /// <typeparam name="TArg1">The type of the first constructor parameter</typeparam>
      /// <typeparam name="TArg2">The type of the second constructor parameter</typeparam>
      /// <typeparam name="TResult">The base/interface type to yield as the
      /// new value; often object except for factory pattern implementations</typeparam>
      /// <param name="type">The Type to be created</param>
      /// <returns>A delegate to the constructor if found, else null</returns>
      public static TResult Create<TArg1, TArg2, TResult>(this Type type, TArg1 arg1, TArg2 arg2) {
        var ci = getConstructor(type, typeof(TArg1), typeof(TArg2));
        var param1 = Expression.Parameter(typeof(TArg1), "arg1");
        var param2 = Expression.Parameter(typeof(TArg2), "arg2");

        var ctor = Expression.Lambda<Func<TArg1, TArg2, TResult>>(Expression.New(ci, param1, param2), param1, param2).Compile();
        return ctor(arg1, arg2);
      }

      /// <summary>
      /// Obtains a delegate to invoke a constructor with multiple parameters
      /// </summary>
      /// <typeparam name="TArg1">The type of the first constructor parameter</typeparam>
      /// <typeparam name="TArg2">The type of the second constructor parameter</typeparam>
      /// <typeparam name="TArg3">The type of the third constructor parameter</typeparam>
      /// <typeparam name="TResult">The base/interface type to yield as the
      /// new value; often object except for factory pattern implementations</typeparam>
      /// <param name="type">The Type to be created</param>
      /// <returns>A delegate to the constructor if found, else null</returns>
      public static Func<TArg1, TArg2, TArg3, TResult> Ctor<TArg1, TArg2, TArg3, TResult>(this Type type) {
        var ci = getConstructor(type, typeof(TArg1), typeof(TArg2), typeof(TArg3));
        ParameterExpression param1 = Expression.Parameter(typeof(TArg1), "arg1"),
                            param2 = Expression.Parameter(typeof(TArg2), "arg2"),
                            param3 = Expression.Parameter(typeof(TArg3), "arg3");

        return
          Expression.Lambda<Func<TArg1, TArg2, TArg3, TResult>>(
                                                                Expression.New(ci, param1, param2, param3),
                                                                param1,
                                                                param2,
                                                                param3).Compile();
      }

      /// <summary>
      /// Obtains a delegate to invoke a constructor with multiple parameters
      /// </summary>
      /// <typeparam name="TArg1">The type of the first constructor parameter</typeparam>
      /// <typeparam name="TArg2">The type of the second constructor parameter</typeparam>
      /// <typeparam name="TArg3">The type of the third constructor parameter</typeparam>
      /// <typeparam name="TArg4">The type of the fourth constructor parameter</typeparam>
      /// <typeparam name="TResult">The base/interface type to yield as the
      /// new value; often object except for factory pattern implementations</typeparam>
      /// <param name="type">The Type to be created</param>
      /// <returns>A delegate to the constructor if found, else null</returns>
      public static Func<TArg1, TArg2, TArg3, TArg4, TResult> Ctor<TArg1, TArg2, TArg3, TArg4, TResult>(this Type type) {
        var ci = getConstructor(type, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4));
        ParameterExpression param1 = Expression.Parameter(typeof(TArg1), "arg1"),
                            param2 = Expression.Parameter(typeof(TArg2), "arg2"),
                            param3 = Expression.Parameter(typeof(TArg3), "arg3"),
                            param4 = Expression.Parameter(typeof(TArg4), "arg4");

        return
          Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(
                                                                       Expression.New(ci, param1, param2, param3, param4),
                                                                       param1,
                                                                       param2,
                                                                       param3,
                                                                       param4).Compile();
      }

      static ConstructorInfo getConstructor(Type type, params Type[] argumentTypes) {
        if (type == null) throw new Exception("type arg is null");
        if (argumentTypes == null) throw new Exception("argumentTypes arg is null");

        var ci = type.GetConstructor(argumentTypes);
        if (ci != null) {
          return ci;
        }
        var sb = new StringBuilder();
        sb.Append(type.Name).Append(" has no ctor(");
        for (var i = 0; i < argumentTypes.Length; i++) {
          if (i > 0) {
            sb.Append(',');
          }
          sb.Append(argumentTypes[i].Name);
        }
        sb.Append(')');
        throw new InvalidOperationException(sb.ToString());
      }
    }
  }
