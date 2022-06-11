# Design of Aesthetically Pleasant Renderings
The problem of designing aesthetically pleasant renderings of labelled trees using functional programming techniques is presented and described in Andrew J. Kennedy's paper, "Functional Pearls: Drawing Trees" from the Journal of Functional Programming. The paper presents four rules that an aesthetic tree must abide: 

1. Two nodes at the same level should be placed at least a given distance apart.
2. A parent should be centred over its offspring.
3. Tree drawings should be symmetrical with respect to reflection—a tree and
its mirror image should produce drawings that are reflections of each other. In
particular, this means that symmetric trees will be rendered symmetrically.
4. Identical subtrees should be rendered identically—their position in the larger
tree should not affect their appearance.

## Solution
The paper presents an explanation to the solution to this problem, as well as an implementation of that solution in Standard ML. In the following, we present our independent implementation of the solution in F#.

### Representing Trees (Types)
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

### Building Trees

To construct a tree, we followed the method presented in the paper. 

The basic functions used to construct the tree are:

 - `moveTree` changes the horizontal position of a tree
 - `moveExtent` changes the horizontal position of an extent
 - `mergeExtent` merges two extents that don't overlap
 - `mergeExtentList` performs a `mergeExtent` on a list of extents
 - `rmax` determines which side a tree is on
 - `fit` recursively determines the minimum distance between two root nodes
 - `fitlistl` fits two trees together from the left
 - `fitlistr` fits two trees together from the right
 - `mean` determines the average of two trees
 - `fitlist` finds the `mean` of the left-fitted tree and the right-fitted tree to produce a tree that is fitted together and centered

All these functions are used together in the `blueprint` function to build a tree. **need a little help here describing this**

