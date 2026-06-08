using Markdig;
using Markiz.Get.Markdig;
using Markiz.Sections.Markdig;
using Markiz.Tables.Id.Markdig;

public class GetCommand : Command
{
    private readonly Argument<string> pathArgument = new("path")
    {
        Description = "The path to the markdown file and fragment id, e.g. file.md#section-id.",
        Arity = ArgumentArity.ExactlyOne
    };

    public GetCommand() : base("get", "Extract a markdown fragment by id.")
    {
        Add(pathArgument);
        SetAction(Execute);
    }

    private void Execute(ParseResult parseResult)
    {
        var path = parseResult.GetRequiredValue(pathArgument);
        var content = GetContent(path);
        Console.WriteLine(content);
    }

    public static string GetContent(string path)
    {
        var hashIndex = path.LastIndexOf('#');
        if (hashIndex < 0) throw new ArgumentException("Path must be in format file.md#id");

        var filePath = path[..hashIndex];
        var id = path[(hashIndex + 1)..];

        var markdown = File.ReadAllText(filePath);

        var pipeline = new MarkdownPipelineBuilder()
            .UseSections()
            .UseSectionIds()
            .UsePipeTables()
            .UseTableIds()
            .Build();

        var document = Markdown.Parse(markdown, pipeline);
        var block = document.Get(id);

        return block.Content();
    }
}