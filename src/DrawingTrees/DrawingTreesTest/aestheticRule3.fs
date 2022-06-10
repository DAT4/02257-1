// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 10 |
// Aesthethic 3: Tree drawings should be symmetrical with respect to reflection—a tree and
// its mirror image should produce drawings that are reflections of each other. In
// particular, this means that symmetric trees will be rendered symmetrically.
// So, for example, Figure 1 shows two renderings, the first bad, the second good.
module AestheticRule3
open TreeTypes
open PositionedTree

let rec reflect (PosNode(v, x, subtrees)) =
    PosNode(v, -x, List.map reflect (List.rev subtrees))

let rec reflectpos (PosNode(v, x, subtrees)) =
    PosNode(v, -x, List.map reflectpos subtrees)

let symmetryProperty tree =
    designTree tree = reflect (reflectpos (designTree tree))

//[<Property>]
open NUnit.Framework
[<Test>]
let symmetryOfTrees () =
    Assert.IsTrue(symmetryProperty Program.t)