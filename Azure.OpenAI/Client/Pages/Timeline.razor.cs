// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Pages;

public sealed partial class Timeline
{
    internal Color[] Colors { get; } =
    [
        Color.Primary,
        Color.Secondary,
        Color.Tertiary,
        Color.Info,
        Color.Warning,
        Color.Success
    ];

    internal ISet<TimelineItem> Items { get; } = new HashSet<TimelineItem>
    {
        new(
            Date: new DateOnly(2002, 1, 1),
            Label: "C# 1.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-10-1",
            Details:
            [
                new Detail("Classes (`class` type)", "https://learn.microsoft.com/dotnet/csharp/fundamentals/types/classes"),
                new Detail("Structs (`struct` type)", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct"),
                new Detail("Interfaces (`interface` type)", "https://learn.microsoft.com/dotnet/csharp/fundamentals/types/interfaces"),
                new Detail("Events (`event` keyword)", "https://learn.microsoft.com/dotnet/csharp/events-overview"),
                new Detail("Properties", "https://learn.microsoft.com/dotnet/csharp/properties"),
                new Detail("Delegates (`delegate`)", "https://learn.microsoft.com/dotnet/csharp/delegates-overview"),
                new Detail("Operators and expressions","https://learn.microsoft.com/dotnet/csharp/language-reference/operators/"),
                new Detail("Statements","https://learn.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/statements"),
                new Detail("Attributes", "https://learn.microsoft.com/dotnet/csharp/advanced-topics/reflection-and-attributes"),
            ]),

        new(
            Date: new DateOnly(2003, 1, 1),
            Label: "C# 1.2",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-12",
            Details:
            [
                new Detail("Introduced `foreach` loop calls `IDisposable.Dispose`")
            ]),

        new(
            Date: new DateOnly(2005, 11, 1),
            Label: "C# 2.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-20",
            Details:
            [
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
            ]),

        new(
            Date: new DateOnly(2007, 11, 1),
            Label: "C# 3.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-30",
            Details:
            [
                new Detail("Auto-implemented properties", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties"),
                new Detail("Anonymous types", "https://learn.microsoft.com/dotnet/csharp/fundamentals/types/anonymous-types"),
                new Detail("Query expressions", "https://learn.microsoft.com/dotnet/csharp/linq/query-expression-basics"),
                new Detail("Lambda expressions", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/lambda-expressions"),
                new Detail("Expression trees", "https://learn.microsoft.com/dotnet/csharp/advanced-topics/expression-trees"),
                new Detail("Extensions methods", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods"),
                new Detail("Implicitly typed local variables (`var` type)", "https://learn.microsoft.com/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables"),
                new Detail("Introduced `partial` methods", "https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/partial-method"),
                new Detail("Object and collection initializers", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers"),
            ]),

        new(
            Date: new DateOnly(2010, 4, 1),
            Label: "C# 4.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-40",
            Details:
            [
                new Detail("Dynamic binding (`dynamic` type)", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/reference-types"),
                new Detail("Named/optional arguments", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments"),
                new Detail("Generic covariant and contravariant", "https://learn.microsoft.com/dotnet/standard/generics/covariance-and-contravariance"),
                new Detail("Embedded interop types", "https://learn.microsoft.com/dotnet/framework/interop/type-equivalence-and-embedded-interop-types"),
            ]),

        new(
            Date: new DateOnly(2012, 10, 1),
            Label: "C# 5.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-50",
            Details:
            [
                new Detail("Asynchronous members (`async` and `await`)", "https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/"),
                new Detail("Caller info attributes", "https://learn.microsoft.com/dotnet/csharp/language-reference/attributes/caller-information"),
            ]),

        new(
            Date: new DateOnly(2015, 7, 1),
            Label: "C# 6.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-60",
            Details:
            [
                new Detail("Static imports (`using static`)", "https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/using-directive"),
                new Detail("Exception filters, i.e.; `when (<expression>)`", "https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/when"),
                new Detail("Auto-property initializers", "https://learn.microsoft.com/dotnet/csharp/properties"),
                new Detail("Expression-bodied members", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition"),
                new Detail("Null propagator", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-"),
                new Detail("String interpolation", "https://learn.microsoft.com/dotnet/csharp/language-reference/tokens/interpolated"),
                new Detail("Introduced `nameof` operator", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/nameof")
            ]),

        new(
            Date: new DateOnly(2017, 3, 1),
            Label: "C# 7.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-70",
            Details:
            [
                new Detail("Out variables (`out var x`)"),
                new Detail("Tuples (`ValueTuple`) and deconstruction", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/value-tuples"),
                new Detail("Pattern matching", "https://learn.microsoft.com/dotnet/csharp/fundamentals/functional/pattern-matching"),
                new Detail("Local functions", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/local-functions"),
                new Detail("Expanded expression bodied members", "https://learn.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members"),
                new Detail("Introduced `ref` locals", "https://learn.microsoft.com/dotnet/csharp/language-reference/statements/declarations#ref-locals"),
                new Detail("Introduced `ref` returns", "https://learn.microsoft.com/dotnet/csharp/language-reference/statements/jump-statements#ref-returns"),
                new Detail("Discards (`_`)", "https://learn.microsoft.com/dotnet/csharp/fundamentals/functional/discards"),
                new Detail("Binary literals and digit separators"),
                new Detail("Throw expressions (`?? throw`)", "https://learn.microsoft.com/dotnet/csharp/language-reference/statements/exception-handling-statements#the-throw-expression"),
            ]),

        new(
            Date: new DateOnly(2017, 8, 1),
            Label: "C# 7.1",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-71",
            Details:
            [
                new Detail("Introduced `async Main`", "https://learn.microsoft.com/dotnet/csharp/fundamentals/program-structure/main-command-line"),
                new Detail("Introduced `default` literals expressions", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/default#default-literal"),
                new Detail("Inferred tuple element names"),
                new Detail("Pattern matching on generic type parameters"),
            ]),

        new(
            Date: new DateOnly(2017, 11, 1),
            Label: "C# 7.2",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-72",
            Details:
            [
                new Detail("Initializers on `stackalloc` arrays"),
                new Detail("Use `fixed` statements on any valid pattern"),
                new Detail("Access `fixed` fields without pinning"),
                new Detail("Reassign `ref` local variables"),
                new Detail("Declare `readonly struct` types"),
                new Detail("Parameter `in` modifier"),
                new Detail("Use `ref readonly` modifier on method `return`"),
                new Detail("Declare `ref struct` types"),
                new Detail("Additional generic constraints"),
                new Detail("Non-trailing named arguments", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments"),
                new Detail("Leading underscores in numeric literals"),
                new Detail("Introduced `private protected` access modifier", "https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/access-modifiers"),
                new Detail("Conditional `ref` expressions"),
            ]),

        new(
            Date: new DateOnly(2018, 5, 1),
            Label: "C# 7.3",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-73",
            Details:
            [
                new Detail("Introduced `==` and `!=` with tuple types"),
                new Detail("Attach attributes to the backing field of auto-implemented properties"),
                new Detail("Method resolution when arguments differ by `in`"),
                new Detail("Overload resolution has few ambiguous cases"),
            ]),

        new(
            Date: new DateOnly(2019, 9, 1),
            Label: "C# 8.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-80",
            Details:
            [
                new Detail("Introduced `readonly` members", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct#readonly-instance-members"),
                new Detail("Default interface methods", "https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/interface#default-interface-members"),
                new Detail("Pattern matching: switch expression enhancements", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/patterns"),
                new Detail("Pattern matching: property expression enhancements", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/patterns"),
                new Detail("Pattern matching: tuple expression enhancements", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/patterns"),
                new Detail("Pattern matching: positional expression enhancements", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/patterns"),
                new Detail("Introduced `using` declarations", "https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/using-directive"),
                new Detail("Introduced `static` local functions", "https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/local-functions"),
                new Detail("Disposable `ref struct`", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/ref-struct"),
                new Detail("Nullable reference types", "https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/nullable-reference-types"),
                new Detail("Introduced asynchronous streams (`await foreach`)", "https://learn.microsoft.com/dotnet/csharp/language-reference/statements/iteration-statements#await-foreach"),
                new Detail("Null-coalescing assignment (`??=`)", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/assignment-operator#null-coalescing-assignment"),
                new Detail("Unmanaged constructed types", "https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/where-generic-type-constraint"),
                new Detail("Introduced `stackalloc` in nested expressions", "https://learn.microsoft.com/dotnet/csharp/language-reference/operators/stackalloc"),
                new Detail("Enhancement of interpolated verbatim string", "https://learn.microsoft.com/dotnet/csharp/language-reference/tokens/interpolated"),
            ]),

        new(
            Date: new DateOnly(2020, 11, 1),
            Label: "C# 9.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-90",
            Details:
            [
                new Detail("Introduced `record` types", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-9#record-types"),
                new Detail("Init-only property setter", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-9#init-only-setters"),
                new Detail("Top-level statements", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-9#top-level-statements"),
                new Detail("Pattern matching enhancements", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-9#pattern-matching-enhancements"),
                new Detail("Native sized integers", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/native-integers"),
                new Detail("Function pointers", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/function-pointers"),
                new Detail("Suppress emitting `localsinit` flag", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/skip-localsinit"),
                new Detail("Target-type `new` expressions", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new"),
                new Detail("Introduced `static` anonymous functions", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/static-anonymous-functions"),
                new Detail("Target-type conditional expressions", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-conditional-expression"),
                new Detail("Covariant return types", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/covariant-returns"),
                new Detail("Extension `GetEnumerator` support for `foreach` loops", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/extension-getenumerator"),
                new Detail("Lambda discard parameters", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/lambda-discard-parameters"),
                new Detail("Attributes on local functions", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/local-function-attributes"),
                new Detail("Introduced `with` expressions"),
                new Detail("Source generators: module initializers", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/module-initializers"),
                new Detail("Source generators: new features for `partial` methods", "https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-9.0/extending-partial-methods"),
            ]),

        new(
            Date: new DateOnly(2021, 11, 1),
            Label: "C# 10.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-10",
            Details:
            [
                new Detail("Introduced `record struct` types", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#record-structs"),
                new Detail("Improvements on structure types", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#improvements-of-structure-types"),
                new Detail("Interpolated string handlers", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#interpolated-string-handler"),
                new Detail("Introduced `global using` directives", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#global-using-directives"),
                new Detail("File-scoped namespace declaration", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#file-scoped-namespace-declaration"),
                new Detail("Extended property patterns", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#extended-property-patterns"),
                new Detail("Improvements on lambda expressions", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#lambda-expression-improvements"),
                new Detail("Allow `const` interpolated strings", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#constant-interpolated-strings"),
                new Detail("Record types can `public override sealed string ToString()`", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#record-types-can-seal-tostring"),
                new Detail("Improved definite assignment", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#improved-definite-assignment"),
                new Detail("Allow both assignment and declaration in the same deconstruction", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#assignment-and-declaration-in-same-deconstruction"),
                new Detail("Allow `AsyncMethodBuilder` attribute on methods", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#allow-asyncmethodbuilder-attribute-on-methods"),
                new Detail("Introduced `CallerArgumentExpression` attribute", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#callerargumentexpression-attribute-diagnostics"),
                new Detail("Enhanced `#line pragma`", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-10#enhanced-line-pragma"),
                new Detail("Introduced `static abstract` members in interfaces", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#generic-math-support"),
            ]),

        new(
            Date: new DateOnly(2022, 11, 1),
            Label: "C# 11.0",
            Reference: "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-version-history#c-version-11",
            Details:
            [
                new Detail("Raw `string` literals", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#raw-string-literals"),
                new Detail("Generic math support", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#generic-math-support"),
                new Detail("Generic attributes", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#generic-attributes"),
                new Detail("UTF-8 `string` literals", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#utf-8-string-literals"),
                new Detail("Newlines in `string` interpolation expressions", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#newlines-in-string-interpolations"),
                new Detail("List patterns", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#list-patterns"),
                new Detail("File-local types", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#file-local-types"),
                new Detail("Required (`required`) members", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#required-members"),
                new Detail("Auto-default structs", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#auto-default-struct"),
                new Detail("Pattern match `Span<char>` on a constant `string`", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#pattern-match-spanchar-or-readonlyspanchar-on-a-constant-string"),
                new Detail("Extended `nameof` scope", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#extended-nameof-scope"),
                new Detail("Numeric `IntPtr`", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#numeric-intptr-and-uintptr"),
                new Detail("Introduced `ref` fields and scoped `ref`", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#ref-fields-and-ref-scoped-variables"),
                new Detail("Improved method group conversion to `delegate`", "https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-11#improved-method-group-conversion-to-delegate"),
            ]),
    };
}
