# Markdown Get (Fragment Extraction)

Markdown normally presents a flat hierarchy with ids pointing to headers, which is useful for navigation, but make extraction by id rather meaningless.
With introduction of [sections](../sections/README.md) and [table ids](../tables/id/README.md), however, it starts to be meaningful - for example for sections ordering or table data manipulation. Having that said, this module introduces an ability to extract a markdown **node** by id, which doesn't have a _real_ dependency on either `sections` or `table ids` modules - only in a "it make sense if other is used" sense.

## Cli

The CLI **does** depend on `sections` and `table ids`, so you can get (extract) a whole section using its id:

```sh
markiz-get get example.md#section-a
```

Or a table like this:

```sh
markiz-get get example.md#section-ab-t1
```