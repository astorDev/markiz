namespace Markiz.Get;

public class MarkdownSegmentExtractorManager(IEnumerable<Func<string, IMarkdownSegmentDirective?>> directiveFactories)
{
    public string Extract(IEnumerable<string> markdownLines, string directive)
    {
        foreach (var directiveFactory in directiveFactories)
        {
            var parsedDirective = directiveFactory(directive);
            if (parsedDirective != null)
            {
                // Process the directive
            }
        }

        throw new NotImplementedException();
    }
}

public interface IMarkdownSegmentDirective
{
    //static abstract IMarkdownSegmentDirective? From(string directivePart);
    bool MatchesBeginningOfSegment(string line);
    bool MatchesEndOfSegment(string line);
}
