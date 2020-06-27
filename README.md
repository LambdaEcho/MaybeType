# `Maybe<T>` Type
Yet another Maybe! The `Maybe<T>` is an immutable [monad type](https://en.wikipedia.org/wiki/Monad_(functional_programming)) with enhancements for functional programming in C#. A `Maybe<T>` encapsulates a variable of type `T`, while `T` can be any basic type of C#.

A `Maybe<T>` is called a **Something**, if the value of the encapsulated variable is set.

A `Maybe<T>` is called a **Nothing**, if the value of the encapsulated variable is not set.

## Why should I use `Maybe<T>`?
- The `Maybe<T>` has powerful semantics that enrich the legibility of your code.
- Implementation of `IEquatable<Maybe<T>>`, `IComparable<Maybe<T>>`, `IComparable<T>`and `IComparable` lead to a very smooth and pleasant interoperability with existing code.
- By wrapping any basic type into an own value type, `Maybe<T>` reduces boilerplate code significantly.
- Sophisticated fluent functional enhancements enables you to write program statments that can be read like a sentence and therefore understood by everyone.
- Replace `null` by `Maybe<T>` and avoid the [billion-dollar mistake](https://en.wikipedia.org/wiki/Tony_Hoare).

## Why is the implementation of `IEnumerable<T>` missing?
While former version implemented `IEnumerable<Maybe<T>>`, the most recent version does not. Although the `Maybe<T>` could really benefit from `IEnumerable`'s `SelectMany()`, other methods are ambiguous and redundant, like `Any()` and `Count()`. Even worse, method `All()` wouldn't work at all and would lead to incorrect results, and, thus, break the semantics!

## Usage
First of all, the `Maybe<T>` is [available for download](https://www.nuget.org/packages/LambdaEcho.Maybe/) as package [in the NuGet Gallery](https://www.nuget.org/packages/LambdaEcho.Maybe/).

The usage of `Maybe<T>` is really simple and straigtforward!

**Create a new `Maybe<T>`**
```
var maybe = Maybe.Create<int>(42); // returns a Something or a Nothing, depending of argument

var something = new Maybe<int>(42);

var something = 42.ToMaybe();

var maybe = new Nullable<int>(42).ToMaybe();

var nothing = default(Maybe<int>);

var nothing = Maybe.Nothing<int>();
```

**Access encapsulated value**
```
if (maybe.HasValue)
{
    var value = maybe.Value;
}

var maybe = Maybe.Nothing<int>();
var value = maybe.ValueOr(42); // returns 42 because Maybe<int> is a Nothing
var value = maybe.ValueOr(() => { new Maybe<int>(42) }); // returns 42 after invocation of Func<T>
```

**Transformations**
```
var something = Maybe.Crete<int>(42);
var somethingAsString = something.Map(value => value.ToString()); // returns "42"

var nothing = Maybe.Nothing<int>();
var nothingAsString = nothing.Map(value => value.ToString(), () => "There is no int"); // returns "There is no int"
```
