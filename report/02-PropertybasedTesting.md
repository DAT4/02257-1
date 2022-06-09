# Property-Based Testing: Validation of rendering properties
In this section we describe how we test our implementation of designing aesthetically pleasant renderings of trees. We will describe the use of property based testing (PBT) for validating the four aesthetic rules described in [Functional Pearls: Drawing Trees]. Specifically, we use FsCheck.NUnit for integrating the FsCheck PBT tool into a unit testing framework. In separate subsections, we describe the four different aesthetic rules of the paper and specify how these rules can be described as boolean properties to be tested by FsCheck. Lastly, we analyze the notion of correctness as described in the paper and show how the correctness properties are tested, but first, we briefly describe how property based testing works with the simple case of the 'mean' function.

## Simple case - mean


## Rule 1
'Two nodes at the same level should be placed at least a given distance apart.'



## Rule 2
'A parent should be centred over its offspring.'

## Rule 3
'Tree drawings should be symmetrical with respect to reflection—a tree and
its mirror image should produce drawings that are reflections of each other. In
particular, this means that symmetric trees will be rendered symmetrically.
So, for example, Figure 1 shows two renderings, the first bad, the second good.'

## Rule 4
'Identical subtrees should be rendered identically—their position in the larger
tree should not affect their appearance. In Figure 2 the tree on the left fails
the test, and the one on the right passes.'

## Correctness
