﻿namespace StyleCop.Analyzers.SpacingRules
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// An opening attribute bracket within a C# element is not spaced correctly.
    /// </summary>
    /// <remarks>
    /// <para>A violation of this rule occurs when the spacing around an opening attribute bracket is not
    /// correct.</para>
    ///
    /// <para>An opening attribute bracket should never be followed by whitespace, unless the bracket is the last
    /// character on the line.</para>
    /// </remarks>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1016OpeningAttributeBracketsMustBeSpacedCorrectly : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SA1016";
        internal const string Title = "Opening Attribute Brackets Must Be Spaced Correctly";
        internal const string MessageFormat = "Opening attribute brackets must not be followed by a space.";
        internal const string Category = "StyleCop.CSharp.SpacingRules";
        internal const string Description = "An opening attribute bracket within a C# element is not spaced correctly.";
        internal const string HelpLink = "http://www.stylecop.com/docs/SA1016.html";

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
            context.RegisterSyntaxTreeAction(HandleSyntaxTree);
        }

        private void HandleSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            SyntaxNode root = context.Tree.GetCompilationUnitRoot(context.CancellationToken);
            foreach (var token in root.DescendantTokens())
            {
                switch (token.CSharpKind())
                {
                case SyntaxKind.OpenBracketToken:
                    HandleOpenBracketToken(context, token);
                    break;

                default:
                    break;
                }
            }
        }

        private void HandleOpenBracketToken(SyntaxTreeAnalysisContext context, SyntaxToken token)
        {
            if (token.IsMissing)
                return;

            if (!token.Parent.IsKind(SyntaxKind.AttributeList))
                return;

            if (!token.HasTrailingTrivia || token.TrailingTrivia.Any(SyntaxKind.EndOfLineTrivia))
                return;

            // Opening attribute brackets must not be followed by a space.
            context.ReportDiagnostic(Diagnostic.Create(Descriptor, token.GetLocation()));
        }
    }
}
