using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Markiz.Get.Markdig;

public static class MarkizGetExtensions
{
    public static Block? Search(this MarkdownDocument document, string id)
    {
        var descendants = document.Descendants<Block>();
        var found = descendants.FirstOrDefault(b => b.GetAttributes().Id == id);
        return found;
    }

    public static Block Get(this MarkdownDocument document, string id)
    {
        return Search(document, id) ?? throw new($"Block with id '{id}' not found");
    }

    /// <summary>
    /// Presents original content (markdown) of the block, including the content of its descendants.
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    public static string Content(this Block block)
    {
        var span = EffectiveSpan(block);
        var source = FindSource(block, span.End);
        if (source is null) return string.Empty;
        return source[span.Start..(span.End + 1)];
    }

    private static SourceSpan EffectiveSpan(Block block)
    {
        if (block is ContainerBlock container && container.Count > 0)
        {
            var firstChildStart = EffectiveSpan(container[0]).Start;
            // If the container's Start is 0 but children start later, its Start
            // was never set (custom post-parse container like SectionBlock).
            // Recompute from children so the span reflects actual source positions.
            if (block.Span.Start == 0 && firstChildStart > 0)
            {
                int start = int.MaxValue, end = int.MinValue;
                foreach (var child in container)
                {
                    var childSpan = EffectiveSpan(child);
                    start = Math.Min(start, childSpan.Start);
                    end = Math.Max(end, childSpan.End);
                }
                return new SourceSpan(start, end);
            }
        }
        return block.Span;
    }

    private static string? FindSource(Block block, int requiredEnd)
    {
        if (block is LeafBlock leaf && leaf.Inline != null)
        {
            foreach (var literal in leaf.Inline.Descendants<LiteralInline>())
                if (literal.Content.Text.Length > requiredEnd) return literal.Content.Text;
        }

        foreach (var descendant in block.Descendants<LeafBlock>())
        {
            if (descendant.Inline is null) continue;
            foreach (var literal in descendant.Inline.Descendants<LiteralInline>())
                if (literal.Content.Text.Length > requiredEnd) return literal.Content.Text;
        }

        var parent = block.Parent;
        while (parent != null)
        {
            foreach (var descendant in parent.Descendants<LeafBlock>())
            {
                if (descendant.Inline is null) continue;
                foreach (var literal in descendant.Inline.Descendants<LiteralInline>())
                    if (literal.Content.Text.Length > requiredEnd) return literal.Content.Text;
            }
            parent = parent.Parent;
        }

        return null;
    }
}


