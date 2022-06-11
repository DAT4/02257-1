# 1   Design of Aesthetically Pleasant Renderings
The problem of designing aesthetically pleasant renderings of labelled trees using functional programming techniques is presented and described in Andrew J. Kennedy's paper, "Functional Pearls: Drawing Trees" from the Journal of Functional Programming. The paper presents four rules that an aesthetic tree must abide: 

1. Two nodes at the same level should be placed at least a given distance apart.
2. A parent should be centred over its offspring.
3. Tree drawings should be symmetrical with respect to reflection—a tree and
its mirror image should produce drawings that are reflections of each other. In
particular, this means that symmetric trees will be rendered symmetrically.
4. Identical subtrees should be rendered identically—their position in the larger
tree should not affect their appearance.

## 1.1   Solution
The paper presents an explanation to the solution to this problem, as well as an implementation of that solution in Standard ML. In the following, we present our independent implementation of the solution in F#.

### 1.1.1   Representing Trees (Types)
The tree is represented in our code using polymorphism so that nodes can be of any type.
```fsharp
type Tree<'a> = Node of 'a * Tree<'a> list
```
Notice the recursiveness of the datastructure that allows to contain trees of arbitrary size in a simple type.

To represent the width of a given node, we created a datatype called Span that is a pair of floats corresponding to the left and right horizontal positions. To represent the extent of the whole Tree, we created a type Extent that is a list of Spans, i.e.
```fsharp
type Span = (float * float)
type Extent = Span list
```

Using the above datatypes, it is possible to derive the positions of the nodes as we explain in the next section. We store these positioned trees in a datatype PosTree which is similar to a Tree but each Node is a PosNode that includes a float representing the horizontal position of the Node
```fsharp
type PosTree<'a> = PosNode of 'a * float * PosTree<'a> list
```
Notice that this differs from the article, where polymorphism of the Tree type is used instead by representing the positioned tree by `Tree<('a, float)>`.

Finally, we created two datatypes that are used for the visualization of the tree. 
The Coordinates type is a pair of ints that represent a horizontal and vertical position given a unit measure.

`type Coordinates = int * int`

The AbsPosTree type is also similar to a Tree, but it includes Coordinates.

`type AbsPosTree<'a> = AbsPosNode of 'a * Coordinates * AbsPosTree<'a> list`

Again, here we could have used the polymorphism of the `Tree<'a>`, but instead we have chosen to define a separate type for transparency.

### 1.1.2   Building Trees

To construct a tree, we followed the method presented in the paper. 

The basic functions used to construct the tree are:

 - `moveTree` changes the horizontal position of a tree by changing the pos component of the root node.
 - `moveExtent` changes the horizontal position of an extent by using a map to change the value of each span in the list.
 - `mergeExtent` merges two extents that don't overlap using pattern matching to determine if either Extent list is empty, in which case we return the non-empty list, or, in the case where both lists are non-empty, we take the first float in the first span in the first list and the second float in the first span in the second list and concatenate that to a recursive call to mergeExtent using the rest of both lists.
 - `mergeExtentList` performs a `mergeExtent` on a list of extents using a Fold.
 - `rmax` returns the largest of two floats, used in fit to determine the minimum distance between root nodes.
 - `fit` recursively determines the minimum distance between two root nodes by repeatedly taking the maximum of the distance between two nodes plus one, the minimum difference, and a recursive call to fit using the rest of the Extent.
 - `fitlistl` recursively fits two trees together from the left by using pattern matching to merge each subtree.
 - `fitlistr` recursively fits two trees together from the right by using pattern matching to merge each subtree, reversing the lists and the polarity of the recursive call to achieve a right-fit instead of a left-fit.
 - `mean` determines the average of two floats.
 - `fitlist` finds the `mean` of the left-fitted tree and the right-fitted tree to produce a tree that is fitted together and centered.

All these functions are used together in a function we call `blueprint` to build a tree
```fsharp
let rec blueprint (Node(x, xs)) =
    List.unzip (List.map blueprint xs) |> fun (ts, es) -> 
    let positions           = fitlist es 
    let ptrees              = List.map (fun (v,t) -> moveTree v t) (List.zip positions ts )
    let ptExtents           = List.map (fun (v,e) -> moveExtent v e) (List.zip positions es )
    let resultExtent        = (0.0, 0.0) :: mergeExtentList ptExtents
    let resulttree          = PosNode(x, 0.0, ptrees)
    (resulttree, resultExtent)
```
The blueprint takes a `Tree<'a>` as input and recursively goes through the subtrees to first identify the node postions using the extents, then move the trees and extents to the corresponding positions and subsequently append them to the resulting tree and extent of the tree and the pair of these are returned. For transparency, two helper functions are used to get the first and second arguments, i.e.
```fsharp
let designTree (t: Tree<'a>) : PosTree<'a> =
    fst (blueprint t)

let designExtents (t: Tree<'a>) : Extent = 
    snd (blueprint t)
```

With the above code, it is possible to design trees that obey the four aesthetic rules stated in the beginning of this section. In the next section, we use property based testing to ensure that the rules are indeed obeyed.
