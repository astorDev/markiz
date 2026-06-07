using Markdig;
using Markdig.Extensions.Tables;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markiz.Sections.Markdig;

namespace Markiz.Tables.Id.Markdig;

public class TableIdsExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        pipeline.DocumentProcessed += AssignIds;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer) { }

    private static void AssignIds(MarkdownDocument document)
    {
        var sectionCounters = new Dictionary<string, int>();

        foreach (var table in document.Descendants<Table>())
        {
            var sectionId = FindClosestSectionId(table);
            var key = sectionId ?? "";
            sectionCounters.TryGetValue(key, out var count);
            sectionCounters[key] = ++count;

            table.GetAttributes().Id = sectionId != null
                ? $"{sectionId}-t{count}"
                : $"t{count}";
        }
    }

    private static string? FindClosestSectionId(Block block)
    {
        var current = block.Parent;
        while (current != null)
        {
            if (current is SectionBlock section)
                return section.GetAttributes().Id;
            current = current.Parent;
        }
        return null;
    }
}

public static class TableIdsExtensionMethods
{
    public static MarkdownPipelineBuilder UseTableIds(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<TableIdsExtension>();
        return pipeline;
    }
}
