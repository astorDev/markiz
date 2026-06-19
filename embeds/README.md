# Markdown Embeds

Once you have a [picture](#pictures) or a relative link to another page in markdown, the document is no longer self-contained. You can no longer share it easily and you need to maintain a folder, instead of a single document.

Word documents have much more complexity to them, then the markdown. They are self-contained though. Presumably, this is the feature, making people tread word-documents as a tool for regular people, while markdown stays a format for tech-savvy folks.

This module goal is to investigate potential solutions for marking embeding easier in markdown.

## Pictures

You can already embed a picture inline using base64:

![](data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABwAAAAqCAMAAACX8MsWAAAAn1BMVEUSExR5wP94v/14vvt1uvZ1uPRztvFyte9vsOlur+dvruZrqN5pptxppttmoNRln9NimMlflMNfk8JekL9cjrxcjrtSfqROd5xMcpVHa4xFaYlCY4E+XXg9XHY+W3Y7WHE6V285VW05VWw3UmkuQlMrPk0sPU8nNkQiLTchLDYgKzUfKTMeJzAdJS0YHiIYHSMYHCIXHCAWGh0UFhgTFRehHB4sAAAArklEQVR42u3OxxKCMBSF4XtQsKOiYEHsBRUFy/s/myFmkAhkw5Z/dWa+yeRS1a9o6bEebF29uFca94hbs+XwFWbRZqvNV5RFneiOIoRPm2Kck42iP9EnPXl5HqaxCWg+UKt/cYc09gxgBJj5OEbcNB8XHA8Cb1sJA7C0t0AiCfmpXRIYDGS0AEwEhg3IeHJd9yJwhj/kCeyo0FBhS4WWClcqJDPBIwBTxqdDVWX6AK4QD710iwH+AAAAAElFTkSuQmCC)

However, even for the tiny file (less than 1 KB) the embed takes quite some space, which is especially problematic if you have a line wrap enabled by default. It also, of course, diminishes markdown's human-readability qualities.