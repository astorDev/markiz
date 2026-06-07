using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;

namespace Markiz.Sections.Markdig;

public class SectionsExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        pipeline.DocumentProcessed += ProcessDocument;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is HtmlRenderer htmlRenderer)
            htmlRenderer.ObjectRenderers.AddIfNotAlready(new SectionBlockRenderer());
    }

    private static void ProcessDocument(MarkdownDocument document)
    {
        var blocks = document.ToList();
        while (document.Count > 0) document.RemoveAt(document.Count - 1);
        foreach (var block in GroupIntoSections(blocks))
            document.Add(block);
    }

    private static List<Block> GroupIntoSections(List<Block> blocks)
    {
        var headings = blocks.OfType<HeadingBlock>().ToList();
        if (headings.Count == 0) return blocks;

        int minLevel = headings.Min(h => h.Level);
        var result = new List<Block>();
        SectionBlock? current = null;

        foreach (var block in blocks)
        {
            if (block is HeadingBlock h && h.Level == minLevel)
            {
                if (current != null) FinalizeSection(current, result);
                current = [h];
            }
            else if (current != null)
                current.Add(block);
            else
                result.Add(block);
        }

        if (current != null) FinalizeSection(current, result);
        return result;
    }

    private static void FinalizeSection(SectionBlock section, List<Block> result)
    {
        var children = section.Skip(1).ToList();
        while (section.Count > 1) section.RemoveAt(section.Count - 1);
        var grouped = GroupIntoSections(children);
        foreach (var b in grouped) section.Add(b);
        result.Add(section);
    }

}

public static class SectionsExtensionMethods
{
    public static MarkdownPipelineBuilder UseSections(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<SectionsExtension>();
        return pipeline;
    }
}
