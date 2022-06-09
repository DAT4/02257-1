// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 9  |
// Aesthethic 2: A parent should be centred over its offspring.
module AestheticRule2
open FsCheck
open FsCheck.NUnit
open TreeTypes

let centeringProperty (PosNode (_, pos, subtrees) as tree ) =
    match subtrees with
    | [] -> true
    | PosNode(_, posSubtree, subsubtrees)::[] ->
        if posSubtree = pos then
            true
        else
            false
    | stl::middleSubtrees::str -> 
        if pos = mean (snd stl, snd str) then
            
        else 
            false
[<Property>]
let positioningOffsprings () =

    centeringProperty

