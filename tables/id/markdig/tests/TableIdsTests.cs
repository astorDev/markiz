using Markdig;
using Markiz.Sections.Markdig;

namespace Markiz.Tables.Id.Markdig.Tests;

[TestClass]
public class TableIdsTests
{
    static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseSections()
        .UseSectionIds()
        .UsePipeTables()
        .UseTableIds()
        .Build();

    [TestMethod]
    public void AssignsIdToSingleTableInSection()
    {
        var markdown = """
            ## Revenue

            | Month | Amount |
            |-------|--------|
            | Jan   | 1000   |
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        Assert.Contains("<table id=\"revenue-t1\">", html);
    }

    [TestMethod]
    public void AssignsSequentialIdsToMultipleTablesInSection()
    {
        var markdown = """
            ## Revenue

            | Month | Amount |
            |-------|--------|
            | Jan   | 1000   |

            | Month | Amount |
            |-------|--------|
            | Feb   | 2000   |
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        Assert.Contains("<table id=\"revenue-t1\">", html);
        Assert.Contains("<table id=\"revenue-t2\">", html);
    }

    [TestMethod]
    public void AssignsIdsBasedOnSectionName()
    {
        var markdown = """
            ## Revenue

            | Month | Amount |
            |-------|--------|
            | Jan   | 1000   |

            ## Expenses

            | Month | Amount |
            |-------|--------|
            | Jan   | 500    |
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        Assert.Contains("<table id=\"revenue-t1\">", html);
        Assert.Contains("<table id=\"expenses-t1\">", html);
    }

    [TestMethod]
    public void TableOrderRestartsPerSection()
    {
        var markdown = """
            ## Revenue

            | Month | Amount |
            |-------|--------|
            | Jan   | 1000   |

            | Month | Amount |
            |-------|--------|
            | Feb   | 2000   |

            ## Expenses

            | Month | Amount |
            |-------|--------|
            | Jan   | 500    |
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        Assert.Contains("<table id=\"revenue-t1\">", html);
        Assert.Contains("<table id=\"revenue-t2\">", html);
        Assert.Contains("<table id=\"expenses-t1\">", html);
    }

    [TestMethod]
    public void AssignsIdToTableWithoutSection()
    {
        var markdown = """
            | Month | Amount |
            |-------|--------|
            | Jan   | 1000   |
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        Assert.Contains("<table id=\"t1\">", html);
    }

    [TestMethod]
    public void AssignsSequentialIdsToMultipleTablesWithoutSection()
    {
        var markdown = """
            | Month | Amount |
            |-------|--------|
            | Jan   | 1000   |

            | Month | Amount |
            |-------|--------|
            | Feb   | 2000   |
            """;

        var html = Markdown.ToHtml(markdown, Pipeline);

        Assert.Contains("<table id=\"t1\">", html);
        Assert.Contains("<table id=\"t2\">", html);
    }
}

