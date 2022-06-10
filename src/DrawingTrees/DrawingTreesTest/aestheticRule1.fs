// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JL              | June 9  |
// Aesthethic 1: Two nodes at the same level should be placed at least a given distance apart.
module AestheticRule1
open FsCheck
open FsCheck.NUnit
open TreeTypes

[<Property>]
let minimum_distance_check (t: PosTree<'a>) = 
    let inner (nodes) = 
        match nodes with
        | [] -> true
        | [(_, (_, _), (_, _))] -> true
        | (x1, (_, _), ( pos1, depth1)) :: (x2, (_, _), ( pos2, depth2)) :: xs ->
            if depth1 = depth2 then 
                if abs (pos2 - pos1) < 1 then false
            else inner ((x2, (_, _), ( pos2, depth2)) :: xs)

    inner (flatten 0 t)
    
