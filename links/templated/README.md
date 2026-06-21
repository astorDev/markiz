# Markdown Templated Links

Markdown provides a powerful, yet simple, mechanism of building references between documents via links. However, to get any information from the referenced document is by navigating to it. This also mean that there's no built-in way to reflect changes in referenc**ed** document in the referenc**ing** document. 

The module solves the problem, by introducing the utilizing the following templating syntax in the original document:

```markdown
- Task 1: [{title} - {status}](task-1.md)
- Advance even further. [#{id} {status}](task-2.md)
```

In the end, `title` and `status` must be extracted from the referenced document (normally, from the frontmatter) and displayed in the referencing document like this: 

- Task 1: [Make First Step - 🔵 In Progress](#)
- Advance even further. [#2: ⚪ TO DO](#)

## Two-Way Binding

The capability also sets a stage for updating linked document directly from a referencing one. This introduces Two-Way Binding capability, which is explored in [the dedicated submodule](./twoway/README.md).



