using Markdig;
using Markiz.Get.Markdig;
using Markiz.Sections.Markdig;
using Markiz.Tables.Id.Markdig;

namespace Markiz.Get.Markdig.Tests;

[TestClass]
public class ContentTests
{
    static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseSections()
        .UseSectionIds()
        .UsePipeTables()
        .UseTableIds()
        .Build();

    [TestMethod]
    public void ContentOfSection()
    {
        var markdown = "## Revenue";

        var block = Markdown.Parse(markdown, Pipeline).Search("revenue");

        Assert.AreEqual("## Revenue", block!.Content());
    }

    [TestMethod]
    public void ContentOfSectionWithChildren()
    {
        var markdown = "## Revenue\n\n### Q1\n\n### Q2";

        var block = Markdown.Parse(markdown, Pipeline).Search("revenue");

        Assert.AreEqual("## Revenue\n\n### Q1\n\n### Q2", block!.Content());
    }

    [TestMethod]
    public void ContentOfTable()
    {
        var markdown = "## Revenue\n\n| Month | Amount |\n|-------|--------|\n| Jan   | 1000   |";

        var block = Markdown.Parse(markdown, Pipeline).Search("revenue-t1");

        Assert.AreEqual("| Month | Amount |\n|-------|--------|\n| Jan   | 1000   |", block!.Content());
    }

    [TestMethod]
    public void ContentOfNestedSection()
    {
        var markdown = "## Revenue\n\n### Q1\n\n### Q2";

        var block = Markdown.Parse(markdown, Pipeline).Search("q1");

        Assert.AreEqual("### Q1", block!.Content());
    }
}
