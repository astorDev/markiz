public class GetCommand : Command
{
    private readonly Option<string> nameOption = new("--name")
    {
        Description = "The name of the person to greet.",
        Required = true
    };

    private readonly Argument<string> pathArgument = new("path")
    {
        Description = "The path from which to read files.",
        Arity = ArgumentArity.ExactlyOne
    };

    public GetCommand() : base("get", "Greet a person by name.")
    {
        Add(pathArgument);
        Add(nameOption);
        SetAction(Execute);
    }

    private void Execute(ParseResult parseResult)
    {
        var name = parseResult.GetRequiredValue(nameOption);
        var path = parseResult.GetRequiredValue(pathArgument);

        Console.WriteLine($"Hello, {name}!");
        Console.WriteLine($"ls from the path you've provided:");
        Directory.GetFileSystemEntries(path)
            .Select(Path.GetFileName)
            .ToList()
            .ForEach(Console.WriteLine);
    }
}