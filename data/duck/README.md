# DuckDb-Based Markdown Tables

The idea is basically to have `duckdb` code snippet, which on rendering will show the actual table result:

```duckdb
SELECT *
FROM 'table.csv'
```

The good news is that duckdb already fully handles dealing with local files and supports multiple file formats with CSV and JSON being the most useful for the case.

## Inspirations

- [DuckData Obsidian Plugin](https://community.obsidian.md/plugins/duckdata?utm_source=chatgpt.com): Very similar, but slightly more complex and obsidian-specific.
