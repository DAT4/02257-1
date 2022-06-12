// --------------------------------------------------
// ./src/DrawingTrees/DrawingTreesTest/aestheticRule2.fs
// --------------------------------------------------
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 21:57:09 
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 11:50:52 
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 00:15:00 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:56:46 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:37:40 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:18:32 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 14:50:29 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 23:49:23 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 22:40:29 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 15:38:44 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 14:05:51 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 11:58:52 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 10:33:08 

// Aesthethic 2: A parent should be centred over its offspring.
module AestheticRule2
open PositionedTree
open FsCheck
open FsCheck.NUnit
open TreeTypes
open TestUtils

[<Property(Arbitrary=[|typeof<TreeGenerator>|])>]
let centeringProperty (t: Tree<char>) =
    let rec f(PosNode (_, _, subtrees)) =
        match subtrees with
        | [] -> true
        | sts ->
            let subtreePositions = subtrees |> List.map getSubtreePositions
            if List.min subtreePositions = - List.max subtreePositions then
                sts |> List.forall f
            else
                false
    f (designTree t)
