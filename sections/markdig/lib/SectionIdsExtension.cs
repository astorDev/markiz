using Markdig;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markiz.Sections.Markdig;

public class SectionIdsExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        pipeline.DocumentProcessed += AssignIds;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer) { }

    private static void AssignIds(MarkdownDocument document)
    {
        foreach (var section in document.Descendants<SectionBlock>())
        {
            var heading = section.OfType<HeadingBlock>().FirstOrDefault();
            if (heading == null) continue;
            section.GetAttributes().Id = GenerateId(heading);
        }
    }

    private static string GenerateId(HeadingBlock heading)
    {
        if (heading.Inline is null) return "section";
        var writer = new StringWriter();
        var stripRenderer = new HtmlRenderer(writer)
        {
            EnableHtmlForInline = false,
            EnableHtmlEscape = false
        };
        stripRenderer.Render(heading.Inline);
        return LinkHelper.Urilize(writer.ToString(), allowOnlyAscii: false);
    }
}

public static class SectionIdsExtensionMethods
{
    public static MarkdownPipelineBuilder UseSectionIds(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<SectionIdsExtension>();
        return pipeline;
    }
}
