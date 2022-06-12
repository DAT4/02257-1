// --------------------------------------------------
// ./src/DrawingTrees/DrawingTreesTest/aestheticRule4.fs
// --------------------------------------------------
// 
// Author: Johan Ott 
// Date:   Sun Jun 12 21:35:43 
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
open NUnit.Framework

let compareRelativeDistance x1 x2 (PosNode(_, subtreex1, _)) (PosNode(_, subtreex2, _)) =
    x1-subtreex1 = x2-subtreex2

let rec compareTreeShapes (tree1 : PosTree<'a>, tree2 : PosTree<'a>) =
    match  (tree1, tree2) with
    | (PosNode(_, _, subtrees1), PosNode(_, _, subtrees2))
        when List.length subtrees1 <> List.length subtrees2 -> false
    | (PosNode(_, x1, subtrees1), PosNode(_, x2, subtrees2)) ->
        List.forall2 (compareRelativeDistance x1 x2) subtrees1 subtrees2
            && List.zip subtrees1 subtrees2  |> List.forall compareTreeShapes

let movingTreeIsUnalteredProperty x tree =
    compareTreeShapes ((moveTree x tree), tree)

[<Test>]
let movingTreeIsUnaltered () =
    Assert.IsTrue(movingTreeIsUnalteredProperty 1.0 (designTree Program.t))

let rec comparExtentShapes (extent1 : Extent, extent2 : Extent) =
    match (extent1, extent2) with
    | (extent1, extent2)
        when (List.length extent1) <> (List.length extent2) -> false
    | ((l1,r1)::extent1rest, (l2,r2)::extent2rest) -> 
        l1-r1 = l2-r2 && comparExtentShapes (extent1rest, extent2rest)

let movingExtentIsUnalteredProperty x extent =
    comparExtentShapes (moveExtent x extent, extent)

[<Test>]
let movingExtentIsUnaltered () =
    Assert.IsTrue(movingExtentIsUnalteredProperty 1.0 (designExtents Program.t))
