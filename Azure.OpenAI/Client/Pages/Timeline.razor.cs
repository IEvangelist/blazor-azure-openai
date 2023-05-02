// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Pages;

public sealed partial class Timeline
{
    internal Color[] Colors { get; } = new[]
    {
        Color.Primary,
        Color.Secondary,
        Color.Tertiary,
        Color.Info,
        Color.Warning,
        Color.Success
    };

    internal ISet<TimelineItem> Items { get; } = new HashSet<TimelineItem>
    {
        new(
            Date: new DateOnly(2002, 1, 1),
            Label: "C# 1.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-10-1",
            Details: new[]
            {
                new Detail("Classes (`class` type)", "https://learn.microsoft.com/dotnet/csharp/fundamentals/types/classes"),
                new Detail("Structs (`struct` type)", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct"),
                new Detail("Interfaces (`interface` type)", "https://learn.microsoft.com/dotnet/csharp/fundamentals/types/interfaces"),
                new Detail("Events (`event` keyword)", "https://learn.microsoft.com/dotnet/csharp/events-overview"),
                new Detail("Properties", "https://learn.microsoft.com/dotnet/csharp/properties"),
                new Detail("Delegates (`delegate`)", "https://learn.microsoft.com/dotnet/csharp/delegates-overview"),
                new Detail("Operators and expressions","https://learn.microsoft.com/dotnet/csharp/language-reference/operators/"),
                new Detail("Statements","https://learn.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/statements"),
                new Detail("Attributes", "https://learn.microsoft.com/dotnet/csharp/advanced-topics/reflection-and-attributes"),
            }),

        new(
            Date: new DateOnly(2003, 1, 1),
            Label: "C# 1.2",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-12",
            Details: new[]
            {
                new Detail("Introduced `foreach` loop calls `IDisposable.Dispose`")
            }),

        new(
            Date: new DateOnly(2005, 11, 1),
            Label: "C# 2.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-20",
            Details: new[]
            {
                new Detail("Generics", "https://learn.microsoft.com/dotnet/csharp/fundamentals/types/generics"),
                new Detail("Partial (`partial`) types", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods#partial-classes"),
                new Detail("Anonymous methods", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/delegate-operator"),
                new Detail("Nullable value types", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/nullable-value-types"),
                new Detail("Iterators", "https://learn.microsoft.com/dotnet/csharp/programming-guide/concepts/iterators"),
                new Detail("Covariance and contravariance", "https://learn.microsoft.com/dotnet/csharp/programming-guide/concepts/covariance-contravariance/"),
                new Detail("Getter/setter separate accessibility"),
                new Detail("Method group conversion (`delegate`)"),
                new Detail("Introduced `static` classes"),
                new Detail("Delegate inference"),
            }),

        new(
            Date: new DateOnly(2007, 11, 1),
            Label: "C# 3.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-30",
            Details: new[]
            {
                new Detail("Auto-implemented properties", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties"),
                new Detail("Anonymous types", "https://learn.microsoft.com/dotnet/csharp/fundamentals/types/anonymous-types"),
                new Detail("Query expressions", "https://learn.microsoft.com/dotnet/csharp/linq/query-expression-basics"),
                new Detail("Lambda expressions", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/lambda-expressions"),
                new Detail("Expression trees"),
                new Detail("Extensions methods"),
                new Detail("Implicitly typed local variables (`var` type)"),
                new Detail("Introduced `partial` methods"),
                new Detail("Object and collection initializers"),
            }),

        new(
            Date: new DateOnly(2010, 4, 1),
            Label: "C# 4.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-40",
            Details: new[]
            {
                new Detail("Dynamic binding (`dynamic` type)"),
                new Detail("Named/optional arguments"),
                new Detail("Generic covariant and contravariant"),
                new Detail("Embedded interop types"),
            }),

        new(
            Date: new DateOnly(2012, 10, 1),
            Label: "C# 5.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-50",
            Details: new[]
            {
                new Detail("Asynchronous members (`async` and `await`)"),
                new Detail("Caller info attributes"),
            }),

        new(
            Date: new DateOnly(2015, 7, 1),
            Label: "C# 6.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-60",
            Details: new[]
            {
                new Detail("Static imports (`using static`)"),
                new Detail("Exception filters, i.e.; `when (<expression>)`"),
                new Detail("Auto-property initializers"),
                new Detail("Expression-bodied members"),
                new Detail("Null propagator"),
                new Detail("String interpolation"),
                new Detail("Introduced `nameof` operator")
            }),

        new(
            Date: new DateOnly(2017, 3, 1),
            Label: "C# 7.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-70",
            Details: new[]
            {
                new Detail("Out variables (`out var x`)"),
                new Detail("Tuples (`ValueTuple`) and deconstruction"),
                new Detail("Pattern matching"),
                new Detail("Expanded expression bodied members"),
                new Detail("Introduced `ref` locals"),
                new Detail("Introduced `ref` returns"),
                new Detail("Discards (`_`)"),
                new Detail("Binary literals and digit separators"),
                new Detail("Throw expressions (`?? throw`)"),
            }),

        new(
            Date: new DateOnly(2017, 8, 1),
            Label: "C# 7.1",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-71",
            Details: new[]
            {
                new Detail("Introduced `async Main`"),
                new Detail("Introduced `default` literals expressions"),
                new Detail("Inferred tuple element names"),
                new Detail("Pattern matching on generic type parameters"),
            }),

        new(
            Date: new DateOnly(2017, 11, 1),
            Label: "C# 7.2",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-72",
            Details: new[]
            {
                new Detail("Initializers on `stackalloc` arrays"),
                new Detail("Use `fixed` statements on any valid pattern"),
                new Detail("Access `fixed` fields without pinning"),
                new Detail("Reassign `ref` local variables"),
                new Detail("Declare `readonly struct` types"),
                new Detail("Parameter `in` modifier"),
                new Detail("Use `ref readonly` modifier on method `return`"),
                new Detail("Declare `ref struct` types"),
                new Detail("Additional generic constraints"),
                new Detail("Non-trailing named arguments"),
                new Detail("Leading underscores in numeric literals"),
                new Detail("Introduced `private protected` access modifier"),
                new Detail("Conditional `ref` expressions"),
            }),

        new(
            Date: new DateOnly(2018, 5, 1),
            Label: "C# 7.3",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-73",
            Details: new[]
            {
                new Detail("Introduced `==` and `!=` with tuple types"),
                new Detail("Attach attributes to the backing field of auto-implemented properties"),
                new Detail("Method resolution when arguments differ by `in`"),
                new Detail("Overload resolution has few ambiguous cases"),
            }),

        new(
            Date: new DateOnly(2019, 9, 1),
            Label: "C# 8.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-80",
            Details: new[]
            {
                new Detail("Introduced `readonly` members"),
                new Detail("Default interface methods"),
                new Detail("Pattern matching: switch expression enhancements"),
                new Detail("Pattern matching: property expression enhancements"),
                new Detail("Pattern matching: tuple expression enhancements"),
                new Detail("Pattern matching: positional expression enhancements"),
                new Detail("Introduced `using` declarations"),
                new Detail("Introduced `static` local functions"),
                new Detail("Disposable `ref struct`"),
                new Detail("Nullable reference types"),
                new Detail("Introduced `async` streams"),
                new Detail("Null-coalescing assignment"),
                new Detail("Unmanaged constructed types"),
                new Detail("Introduced `stackalloc` in nested expressions"),
                new Detail("Enhancement of interpolated verbatim string"),
            }),

        new(
            Date: new DateOnly(2020, 11, 1),
            Label: "C# 9.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-90",
            Details: new[]
            {
                new Detail("Introduced `record` types"),
                new Detail("Init-only property setter"),
                new Detail("Top-level statements"),
                new Detail("Pattern matching enhancements"),
                new Detail("Native sized integers"),
                new Detail("Function pointers"),
                new Detail("Suppress emitting `localsinit` flag"),
                new Detail("Target-type `new` expressions"),
                new Detail("Introduced `static` anonymous functions"),
                new Detail("Target-type conditional expressions"),
                new Detail("Covariant return types"),
                new Detail("Extension `GetEnumerator` support for `foreach` loops"),
                new Detail("Lambda discard parameters"),
                new Detail("Attributes on local functions"),
                new Detail("Introduced `with` expressions"),
                new Detail("Source generators: module initializers"),
                new Detail("Source generators: new features for `partial` methods"),
            }),

        new(
            Date: new DateOnly(2021, 11, 1),
            Label: "C# 10.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-10",
            Details: new[]
            {
                new Detail("Introduced `record struct` types"),
                new Detail("Improvements on structure types"),
                new Detail("Interpolated string handlers"),
                new Detail("Introduced `global using` directives"),
                new Detail("File-scoped namespace declaration"),
                new Detail("Extended property patterns"),
                new Detail("Improvements on lambda expressions"),
                new Detail("Allow `const` interpolated strings"),
                new Detail("Record types can `public override sealed string ToString()`"),
                new Detail("Improved definite assignment"),
                new Detail("Allow both assignment and declaration in the same deconstruction"),
                new Detail("Allow `AsyncMethodBuilder` attribute on methods"),
                new Detail("Introduced `CallerArgumentExpression` attribute"),
                new Detail("Enhanced `#line pragma`"),
                new Detail("Introduced `static abstract` members in interfaces"),
            }),
        new(
        Date: new DateOnly(2022, 11, 1),
        Label: "C# 11.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-11",
        Details: new[]
        {
            new Detail("Raw `string` literals"),
            new Detail("Generic math support"),
            new Detail("Generic attributes"),
            new Detail("UTF-8 `string` literals"),
            new Detail("Newlines in `string` interpolation expressions"),
            new Detail("List patterns"),
            new Detail("File-local types"),
            new Detail("Required (`required`) members"),
            new Detail("Auto-default structs"),
            new Detail("Pattern match `Span<char>` on a constant `string`"),
            new Detail("Extended `nameof` scope"),
            new Detail("Numeric `IntPtr`"),
            new Detail("Introduced `ref` fields and scoped `ref`"),
            new Detail("Improved method group conversion to `delegate`"),
        }),
    };
}
