// --------------------------------------------------
// ./src/DrawingTrees/DrawingTreesTest/aestheticRule3.fs
// --------------------------------------------------
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 11:50:52 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 22:12:50 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 21:03:28 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:56:46 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:39:21 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 15:38:44 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 11:58:52 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 10:33:08 

// Aesthethic 3: Tree drawings should be symmetrical with respect to reflection—a tree and
// its mirror image should produce drawings that are reflections of each other. In
// particular, this means that symmetric trees will be rendered symmetrically.
// So, for example, Figure 1 shows two renderings, the first bad, the second good.
module AestheticRule3
open TestUtils
open TreeTypes
open PositionedTree
open FsCheck.NUnit

[<Property(Arbitrary=[|typeof<TreeGenerator>|])>]
let symmetryProperty (t: Tree<char>) =
    let rec reflect (Node(v, subtrees)) =
        Node(v, List.map reflect (List.rev subtrees))
    let rec reflectpos (PosNode(v, x, subtrees)) =
        PosNode(v, -x, List.map reflectpos  (List.rev subtrees))
    designTree t = reflectpos (designTree (reflect (t)))
