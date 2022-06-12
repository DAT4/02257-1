# 4   Evaluation
In the current report, we present the implementation in F# of a method to automatically structure trees such that they obey certain aesthical rules. The solution is built on recursively arranging the nodes of the tree to allow the 


 method in `F#` that is able to automatically structure trees such that they obey certain aesthical rules. It is validated that the rules are obeyed by using property based testing where FsCheck is used to generate randomly generated input using our own implementation of a random tree generator. Finally, we present a methodology for visualizing the trees by converting to SVG format.

