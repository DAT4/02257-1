// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 9  |
// | JRA             | June 10  |

// Aesthethic 2: A parent should be centred over its offspring.
module AestheticRule2
open FsCheck
open FsCheck.NUnit
open TreeTypes
open TestUtils

let rec centeringProperty (PosNode (_, _, subtrees) as tree ) =
    match subtrees with
    | [] -> true
    | sts ->
        let subtreePositions = subtrees |> List.map getSubtreePositions
        if List.min subtreePositions = - List.max subtreePositions then
            sts |> List.forall centeringProperty
        else
            false

//[<Property>]
open NUnit.Framework
[<Test>]
let positioningOffsprings () =
    Assert.IsTrue(testProperty centeringProperty Program.t)
