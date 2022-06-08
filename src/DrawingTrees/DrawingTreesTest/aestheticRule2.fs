/// Aesthethic 2: A parent should be centred over its offspring.
module AestheticRule2
open FsCheck
open FsCheck.NUnit
open TreeTypes

[<Property>]
let positioningOffsprings () =
    let centeringProperty (PosNode (_, pos, subtrees) as tree ) =
        match subtrees with
        | [] -> true
        | PosNode(_, posSubtree, subsubtrees)::[] ->
            if posSubtree = pos then
                true
            else
                false
        | stl::middleSubtrees::str -> true
    centeringProperty

