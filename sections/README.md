# Sections

Markdown documents are normally rather flat. Headers (lines starting with `#`) are rendered as `<hx>` HTML tags and direct text is rendered as `<p>` tags. Headers allow structuring the document, and most renderers assign them an `id` attribute. This lets headers conceptually act as section pointers. Since headers are always at the beginning of a section, navigation by `id` works perfectly fine.

However, this does not help with content extraction, because headers are on the same level as their content. Here we introduce sections for Markdown. The concept is simple: a section starts with the first header at a given level and ends when the next header at the same level appears. It "steals" the `id` from the header as well. So for this document:

```markdown
## Section A

### Subsection AA

### Subsection AB

## Section B

### Subsection BA

### Subsection BB
```

we will get roughly this structure:

```html
<section id="section-a">
    <h2>Section A</h2>
    <section id="subsection-aa">
        <h3>Subsection AA</h3>
    </section>
    <section id="subsection-ab">
        <h3>Subsection AB</h3>
    </section>
</section>

<section id="section-b">
    <h2>Section B</h2>
    <section id="subsection-ba">
        <h3>Subsection BA</h3>
    </section>
    <section id="subsection-bb">
        <h3>Subsection BB</h3>
    </section>
</section>
```

This will allow us to extract Section A with all its children in a straightforward manner, while still "just working" for navigation.