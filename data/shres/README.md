# Shell Results Block

Following the [DuckDB Idea](../duck/README.md), it became clear, that the working with duckdb can be generalized with more generic shell-results idea, with markdown scripts in `shres` would be automatically executed with their output printed to console 

```shres
duckdb -c ".maxrows 10" -c "SELECT * FROM 'stadten.csv' ORDER BY einwohnerzahl DESC, tal DESC;"
```

This of course, raises security concerns. It is also very close to jupyter notebooks. 