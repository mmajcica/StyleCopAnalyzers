﻿namespace StyleCop.Analyzers.ReadabilityRules
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// A call to a member is not prefixed with the <c>this.</c>, <c>base.</c>, <c>object.</c> or <c>typename.</c>
    /// prefix to indicate the intended method call, within a C# code file.
    /// </summary>
    /// <remarks>
    /// <para>A violation of this rule occurs whenever the code contains a call to a member which is not prefixed
    /// correctly.</para>
    ///
    /// <para>In some case from source code analysis we cannot be sure which prefix is required. It could be
    /// <c>this</c>, <c>base</c>, <c>object</c>, the typename of the class we're in, or one of our base classes.</para>
    /// </remarks>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1126PrefixCallsCorrectly : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SA1126";
        internal const string Title = "A call to a member is not prefixed with the 'this.', 'base.', 'object.' or 'typename.' prefix to indicate the intended method call, within a C# code file.";
        internal const string MessageFormat = "TODO: Message format";
        internal const string Category = "StyleCop.CSharp.ReadabilityRules";
        internal const string Description = "A call to a member is not prefixed with the 'this.', 'base.', 'object.' or 'typename.' prefix to indicate the intended method call, within a C# code file.";
        internal const string HelpLink = "http://www.stylecop.com/docs/SA1126.html";

        public static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, AnalyzerConstants.DisabledNoTests, Description, HelpLink);

        private static readonly ImmutableArray<DiagnosticDescriptor> _supportedDiagnostics =
            ImmutableArray.Create(Descriptor);

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return _supportedDiagnostics;
            }
        }

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            // TODO: Implement analysis
        }
    }
}
