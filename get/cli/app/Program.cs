var builder = new CliBuilder();

builder.AddCommand<GetCommand>();

using var app = builder.Build("A get CLI application.");

return app.Run(args);