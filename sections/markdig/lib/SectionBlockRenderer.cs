using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Markiz.Sections.Markdig;

public class SectionBlockRenderer : HtmlObjectRenderer<SectionBlock>
{
    protected override void Write(HtmlRenderer renderer, SectionBlock obj)
    {
        renderer.EnsureLine();
        renderer.Write("<section");
        renderer.WriteAttributes(obj);
        renderer.WriteLine(">");
        renderer.WriteChildren(obj);
        renderer.EnsureLine();
        renderer.WriteLine("</section>");
    }
}
