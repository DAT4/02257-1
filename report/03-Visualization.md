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

The SVG format depends on absolute coordinates in relation to the canvas of the image output where the x and y axis starts in the top left corner. So assuming we already have a correctly positioned tree using the code from the article, we can convert each node in the tree to a node which has a `x` and a `y` coordinate which will be used when plotting the node on the SVG.

## Getting the absolute coordinates

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

The x coordinate is still not fully absolute in relation to the canvas, because every coordinate on the left of the root node has a negative value. To get the absolute value we just need to shift the element to the right by adding it with the inverted value of the left extreme.

The implementation resulted in a function which takes a simple tree, and a scale which is used to modify the distance between the coordinates of the nodes.

All number values used in the trees and extends are floats but in SVG we will need integers for the coordinates. The float values always follow the interval of 0.5 so we can get the same precision using integers by multiplying the values by 2 before we cast them to integers.

The inner function will recursively traverse through the tree and apply the absolute positions for the x and y coordinates to each node. At last the absolute positioned tree will be returned in a tuple together with the width and the height of the whole frame.

```fs
let absolutify (scale: int) (t: Tree<'a>) =
    let (tree, extends) = blueprint t
    let (left, right)   = extremes extends 
    let width           = int((left + right) * 2.0)
    let start           = int(firstPos right tree) 
    let rec f (depth: int) (px: float) (PosNode(x, pos, cs)) =
        let (t, d) = 
            match cs with 
            | []  -> [], depth
            | _  -> List.map (f (depth+1) (pos+px)) cs |> List.unzip |> fun (t, d) -> t, List.max d
        AbsPosNode( x, (int((pos+px+left)*2.0)*scale, depth*2*scale),  t ), d 
    let (out, depth) = f 0 start tree 
    out, (width * scale, depth * 2 * scale )
```

## Mapping absolute coordinate tree to SVG image

When the absolute positions of each node is already given the mapping to SVG is simple. We define the SVG frame using the width and the height and the content of the SVG file is given by mapping the coordinates and the value of the nodes to the text and the line SVG objects.

```fs
let draw (scale: int) (t: Tree<'a>) =
    let tree, (width, height) = absolutify scale t
    let svg (content) = sprintf "<svg height=\"%i\" width=\"%i\">\n%s\n</svg>" height width content

    let text px py x y v = 
        let text = sprintf "<text x=\"%i\" y=\"%i\" fill=\"black\">%A</text>\n" x y v
        let line = sprintf "<line x1=\"%i\" y1=\"%i\" x2=\"%i\" y2=\"%i\" style=\"stroke:rgb(0,0,0);stroke-width:2\"/>" px py x y
        if (px+py) = 0 then text else text+line

    let rec content (px: int, py: int) (AbsPosNode(v, (x, y), cs)) =
        let out = text px py x y v
        match cs with
        | [] -> out
        | _ -> out + "\n" + (List.map (content (x,y)) cs |> (String.concat "\n"))
    svg (content (0,0) tree)
```
