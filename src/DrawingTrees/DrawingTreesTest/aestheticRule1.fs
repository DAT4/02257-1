// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JL              | June 9  |
// | MM              | June 10  |
// Aesthethic 1: Two nodes at the same level should be placed at least a given distance apart.
module AestheticRule1
open FsCheck
open TreeTypes
open NUnit.Framework 
open TestUtils

let minimum_distance_check(t: Tree<'a>) = 
    let isInOrder xs = xs
                       |> Seq.pairwise 
                       |> Seq.forall (fun (a, b) -> a <= b-1.0)
    flatten t |> Seq.forall isInOrder

[<Test>]
let aestheticrule1 () = 
    Assert.IsTrue(minimum_distance_check Program.t)   
