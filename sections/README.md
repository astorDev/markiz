# Sections

Markdown documents are normally rather flat. Headers (lines starting with `#`'s) are rendered as `<hx>` html tags and direct text is rendered as `<p>` tags. Headers allow structuring the document of course, and are getting assigned an `id` attribute by most rendererers. This lets headers to conceptually act as a section pointers. Since headers are always at the beginning of a section, navigation by `id` works perfectly fine. 

However, this does not help extraction match, because headers are on the same level as content. Here we introducing sections to markdown. The concept is simple: a section starts with a first header in a specific level and ends when the next header on the same level appears. It "steals" `id` from the header as well. So for this document:

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
        <h2>Subsection AA</h2>
    </section>
    <section id="subsection-ab">
        <h2>Subsection AB</h2>
    </section>
</section>

<section id="section-b">
    <h2>Section B</h2>
    <section id="subsection-ba">
        <h2>Subsection BA</h2>
    </section>
    <section id="subsection-bb">
        <h2>Subsection BB</h2>
    </section>
</section>
```

This will allow us to extract section a with all it's children in a straight-forward manner, while still "just working" for navigation.