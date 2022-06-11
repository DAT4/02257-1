// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 9  |
// | JRA             | June 10 |

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
