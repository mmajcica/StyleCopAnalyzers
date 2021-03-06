﻿namespace StyleCop.Analyzers.SpacingRules
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// A single-line comment within a C# code file does not begin with a single space.
    /// </summary>
    /// <remarks>
    /// <para>A violation of this rule occurs when a single-line comment does not begin with a single space. For
    /// example:</para>
    ///
    /// <code language="cs">
    /// private void Method1()
    /// {
    ///     //A single-line comment.
    ///     //   A single-line comment.
    /// }
    /// </code>
    ///
    /// <para>The comments should begin with a single space after the leading forward slashes:</para>
    ///
    /// <code language="cs">
    /// private void Method1()
    /// {
    ///     // A single-line comment.
    ///     // A single-line comment.
    /// }
    /// </code>
    ///
    /// <para>An exception to this rule occurs when the comment is being used to comment out a line of code. In this
    /// case, the space can be omitted if the comment begins with four forward slashes to indicate out-commented code.
    /// For example:</para>
    ///
    /// <code language="cs">
    /// private void Method1()
    /// {
    ///     ////int x = 2;
    ///     ////return x;
    /// }
    /// </code>
    /// </remarks>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1005SingleLineCommentsMustBeginWithSingleSpace : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SA1005";
        internal const string Title = "Single Line Comments Must Begin With Single Space";
        internal const string MessageFormat = "Single line comment must begin with a space.";
        internal const string Category = "StyleCop.CSharp.SpacingRules";
        internal const string Description = "A single-line comment within a C# code file does not begin with a single space.";
        internal const string HelpLink = "http://www.stylecop.com/docs/SA1005.html";

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
            foreach (var trivia in root.DescendantTrivia())
            {
                switch (trivia.CSharpKind())
                {
                case SyntaxKind.SingleLineCommentTrivia:
                    HandleSingleLineCommentTrivia(context, trivia);
                    break;

                default:
                    break;
                }
            }
        }

        private void HandleSingleLineCommentTrivia(SyntaxTreeAnalysisContext context, SyntaxTrivia trivia)
        {
            string text = trivia.ToFullString();
            if (text.Equals("//") || text.StartsWith("// "))
                return;

            // special case: commented code
            if (text.StartsWith("////"))
                return;

            // Single line comment must begin with a space.
            context.ReportDiagnostic(Diagnostic.Create(Descriptor, trivia.GetLocation()));
        }
    }
}
