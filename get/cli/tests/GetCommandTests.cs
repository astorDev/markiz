namespace Markiz.Get.Cli.Tests;

[TestClass]
public sealed class GetCommandTests
{
    [TestMethod]
    public void Section()
    {
        var result = GetCommand.GetContent("example.md#section-a");

        var expected = "## Section A\n\nSome content in section A.\n\n### Section AB\n\n| Col1 | Col2 |\n|------|------|\n| val1 | val2 |";

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Table()
    {
        var result = GetCommand.GetContent("example.md#section-ab-t1");

        Assert.AreEqual("| Col1 | Col2 |\n|------|------|\n| val1 | val2 |", result);
    }
}
