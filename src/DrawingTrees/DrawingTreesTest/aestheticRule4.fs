// --------------------------------------------------
// ./src/DrawingTrees/DrawingTreesTest/aestheticRule4.fs
// --------------------------------------------------
// 
// Author: Johan Ott 
// Date:   Sun Jun 12 21:56:43 
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 11:50:52 
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 04:05:43 
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 03:19:40 
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 00:15:00 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 15:38:44 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 11:58:52 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 10:33:08 

// Aesthethic 4:  Identical subtrees should be rendered identically—their position in the larger 
// tree should not affect their appearance. In Figure 2 the tree on the left fails 
// the test, and the one on the right passes.
module AestheticRule4
open TreeTypes
open PositionedTree
open TestUtils
open FsCheck.NUnit



let rec compareTreeShapes (tree1 : PosTree<'a>, tree2 : PosTree<'a>) =

    match  (tree1, tree2) with
    | (PosNode(_, _, subtrees1), PosNode(_, _, subtrees2))
        when List.length subtrees1 <> List.length subtrees2 -> false
    | (PosNode(_, x1, subtrees1), PosNode(_, x2, subtrees2)) ->
        let comparePositions (PosNode(_, subtree1, _)) (PosNode(_, subtree2, _)) =
            subtree1 = subtree2
        List.forall2 comparePositions subtrees1 subtrees2
            && List.zip subtrees1 subtrees2  |> List.forall compareTreeShapes

let movingTreeIsUnalteredProperty x tree =
    compareTreeShapes ((moveTree x tree), tree)

[<Property(Arbitrary=[|typeof<TreeGenerator>|])>]
let movingTreeIsUnaltered () =
    movingTreeIsUnalteredProperty 3.15 (designTree Program.t)
