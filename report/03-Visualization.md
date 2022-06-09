# Visualization of trees

To visualize the tree we will need to map the tree structure into an image. 

In the case of a tree of single letters we would need the following objects for each node:

- one letter
- one line from letter to its parent (except the root node which does not have a parent)

This task can easily be done using the SVG (Scalable Vector Graphics) format

## SVG

SVG files are just text files following the XML (Extensible Markup Language) format.

An SVG representation example of just the root node would look like this:

```svg
<svg height="300" width="600">
<text x="300" y="0" fill="black">"A"</text>
</svg>
```

And if we add a child to the root node we will also need a line between the nodes

```svg
<svg height="300" width="600">
<text x="300" y="0" fill="black">"A"</text>
<text x="0" y="150" fill="black">"B"</text>
<line x1="300" y1="0" x2="0" y2="150" style="stroke:rgb(0,0,0);stroke-width:2"/>
</svg>
```

## The mapping 

The SVG format depends on absolute values


