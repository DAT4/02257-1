// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 9  |

// Aesthethic 2: A parent should be centred over its offspring.
module AestheticRule2
open FsCheck
open FsCheck.NUnit
open TreeTypes
open PositionedTree

let rec checkSubtreePositions (parentPosition:float) (subtreePositions :float list) =
    match subtreePositions with
    | [] -> true
    | headPosition::tailList -> 
        match tailList |> List.rev with
        | [] -> parentPosition = headPosition
        | tailPosition::middlePositions ->
            if parentPosition = mean(headPosition, tailPosition) then
                true //checkSubtreePositions parentPosition middlePositions
            else
                false

let rec centeringProperty (PosNode (_, pos, subtrees) as tree ) =
    let getSubtreePosition (PosNode (_, pos, _)) = pos
    let subtreePositions = subtrees |> List.map getSubtreePosition
    if checkSubtreePositions pos subtreePositions then
        subtrees |> List.map centeringProperty
                 |> List.tryFind (fun propertyObeyed -> propertyObeyed = false)
                 |> Option.defaultValue true
    else
        false

let testProperty posTreePropertyFunction tree =
    designTree tree |> posTreePropertyFunction
//[<Property>]
open NUnit.Framework
[<Test>]
let positioningOffsprings () =
    Assert.IsTrue(testProperty centeringProperty Program.t)
