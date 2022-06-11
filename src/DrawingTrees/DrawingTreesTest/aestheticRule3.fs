// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 10 |
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
