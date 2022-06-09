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

The SVG format depends on absolute coordinates in relation to the canvas of the image output where the x and y axis starts in the top left corner. So assuming we already have a correctly positioned tree using the code from the article, we can convert each node in the tree to a node which has a `x` and a `y` coordinate which will be used when plotting the node on the SVG.

Each node in the positioned tree has a position relative to its parent. To determine the absolute position of each node we need to first determine the absolute position of the root node. We already know the y coordinate of the root node because it is the one on the top of the canvas, so it is 0. To find the x coordinate of the root node we will need to find the outermost node in one of the sides of the tree and then accumulate the horizontal space all the way back to the root node. We can use the extends given by the `blueprint` function to find the coordinates of the horizontal poles of the Tree

```fs
let extremes (e: Extend): float*float = 
    let (lefts, rights) = List.unzip ( e )
    -List.min(lefts), List.max(rights)
```

Then we can use the right extreme to compare with the right most element in each node while traversing to the down in the right side of the tree, when recursion is done each position will be returned and the root nodes absolute position in relation to the right side is given.

**TODO: This function should be refactored**
```fs
let firstPos (rightExtreme: float) (t : PosTree<'a>) : float = 
    let rec f (PosNode(_, pos, cs)) =  
        match (pos, cs) with 
        | _, [] -> pos
        | pos, _ when pos < rightExtreme -> pos + (f (List.last cs))
        | _, _ -> pos
    rightExtreme - f t
```

The position is still not fully absolute in relation to the canvas, because the extend value is given 

