using Markdig;

namespace Markiz.Sections.Markdig.Tests;

[TestClass]
public class SectionsTests
{
    static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseSections()
        .UseSectionIds()
        .Build();

    [TestMethod]
    public void RendersTopLevelSections()
    {
        var markdown = """
            ## Section A

            ## Section B
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        StringAssert.Contains(html, "<section id=\"section-a\">");
        StringAssert.Contains(html, "<section id=\"section-b\">");
    }

    [TestMethod]
    public void RendersNestedSections()
    {
        var markdown = """
            ## Section A

            ### Subsection AA

            ### Subsection AB

            ## Section B

            ### Subsection BA

            ### Subsection BB
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        StringAssert.Contains(html, "<section id=\"section-a\">");
        StringAssert.Contains(html, "<section id=\"section-b\">");
        StringAssert.Contains(html, "<section id=\"subsection-aa\">");
        StringAssert.Contains(html, "<section id=\"subsection-ab\">");
        StringAssert.Contains(html, "<section id=\"subsection-ba\">");
        StringAssert.Contains(html, "<section id=\"subsection-bb\">");
    }

    [TestMethod]
    public void SubsectionsAreNestedWithinParentSection()
    {
        var markdown = """
            ## Section A

            ### Subsection AA

            ## Section B
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        var sectionAStart = html.IndexOf("<section id=\"section-a\">");
        var sectionAEnd = html.IndexOf("<section id=\"section-b\">");
        var subsectionAAPos = html.IndexOf("<section id=\"subsection-aa\">");

        Assert.IsTrue(sectionAStart < subsectionAAPos && subsectionAAPos < sectionAEnd,
            "Subsection AA should be nested inside Section A");
    }

    [TestMethod]
    public void HeadingIsInsideSection()
    {
        var markdown = "## Section A";

        var html = Markdown.ToHtml(markdown, Pipeline);

        var sectionStart = html.IndexOf("<section id=\"section-a\">");
        var h2Pos = html.IndexOf("<h2>");
        var sectionEnd = html.IndexOf("</section>");

        Assert.IsTrue(sectionStart < h2Pos && h2Pos < sectionEnd,
            "H2 should be inside the section tag");
    }

    [TestMethod]
    public void RendersFullDocumentHtml()
    {
        var markdown = """
            ## Section A

            ### Subsection AA

            ### Subsection AB

            ## Section B

            ### Subsection BA

            ### Subsection BB
            """;

        var expected = """
            <section id="section-a">
                <h2>Section A</h2>
                <section id="subsection-aa">
                    <h3>Subsection AA</h3>
                </section>
                <section id="subsection-ab">
                    <h3>Subsection AB</h3>
                </section>
            </section>
            <section id="section-b">
                <h2>Section B</h2>
                <section id="subsection-ba">
                    <h3>Subsection BA</h3>
                </section>
                <section id="subsection-bb">
                    <h3>Subsection BB</h3>
                </section>
            </section>
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        Assert.AreEqual(Normalize(expected), Normalize(html));
    }

    static string Normalize(string html) =>
        string.Join('\n', html.Split('\n').Select(l => l.Trim()).Where(l => l.Length > 0));
}
