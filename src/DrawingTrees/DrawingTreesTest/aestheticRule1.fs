// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JL              | June 9  |
// Aesthethic 1: Two nodes at the same level should be placed at least a given distance apart.
module AestheticRule1
open FsCheck
open TreeTypes
open NUnit.Framework 
open TestUtils

let minimum_distance_check(t: Tree<'a>) = 
    flatten t |> Seq.forall isOrdered 

[<Test>]
let aestheticrule1 () = 
    Assert.IsTrue(minimum_distance_check Program.t)   
