using Markdig;
using Markiz.Get.Markdig;
using Markiz.Sections.Markdig;
using Markiz.Tables.Id.Markdig;

namespace Markiz.Get.Markdig.Tests;

[TestClass]
public class GetTests
{
    static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseSections()
        .UseSectionIds()
        .UsePipeTables()
        .UseTableIds()
        .Build();

    [TestMethod]
    public void GetsSectionById()
    {
        var markdown = """
            ## Revenue

            Our revenue was 10 million dollars in 2023.

            ## Expenses
            """;

        var document = Markdown.Parse(markdown, Pipeline);

        var block = document.Get("revenue");
        var content = block.Content();

        var expected = """
            ## Revenue

            Our revenue was 10 million dollars in 2023.
            """;

        Assert.AreEqual(expected, content);
    }

    [TestMethod]
    public void ReturnsNullWhenIdNotFound()
    {
        var markdown = """
            ## Revenue
            """;

        var document = Markdown.Parse(markdown, Pipeline);

        var block = document.Search("expenses");

        Assert.IsNull(block);
    }

    [TestMethod]
    public void GetsTableById()
    {
        var markdown = """
            ## Revenue

            | Month | Amount |
            |-------|--------|
            | Jan   | 1000   |
            """;

        var document = Markdown.Parse(markdown, Pipeline);

        var block = document.Get("revenue-t1");
        var content = block.Content();

        Assert.AreEqual("| Month | Amount |\n|-------|--------|\n| Jan   | 1000   |", content);
    }

    [TestMethod]
    public void GetsNestedSectionById()
    {
        var markdown = """
            ## Revenue

            ### Q1

            ### Q2
            """;

        var document = Markdown.Parse(markdown, Pipeline);

        var block = document.Get("q1");
        var content = block.Content();

        Assert.AreEqual("### Q1", content);
    }

    [TestMethod]
    public void GetsWithNestedSectionsContent()
    {
        var markdown = """
            ## Revenue

            Here's our revenue breakdown by quarter:

            ### Q1

            In Q1, our revenue was 2 million dollars.

            ### Q2

            In Q2, our revenue was 3 million dollars.

            ## Expenses

            Here's our expenses information...
            """;

        var document = Markdown.Parse(markdown, Pipeline);

        var block = document.Get("revenue");
        var content = block.Content();

        var expected = """
            ## Revenue

            Here's our revenue breakdown by quarter:

            ### Q1

            In Q1, our revenue was 2 million dollars.

            ### Q2

            In Q2, our revenue was 3 million dollars.
            """;

        Assert.AreEqual(expected, content);
    }
}

