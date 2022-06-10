// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 10 |
// Aesthethic 4:  Identical subtrees should be rendered identically—their position in the larger 
// tree should not affect their appearance. In Figure 2 the tree on the left fails 
// the test, and the one on the right passes.
module AestheticRule4
open TreeTypes
open TestUtils

let rec getListOfListOfPositions acc (PosNode(_, x, subtrees)) =
        match subtrees with
        | [] -> []::acc
        | _ ->
            let test = subtrees |> List.map getSubtreePositions
            let d = subtrees |> List.map (getListOfListOfPositions []) |> List.concat
            test::d

let areListsIdentical (numberOfSubtrees : int, listOfListOfPositions : float list list) =
    (listOfListOfPositions |> List.distinct |> List.length) = 1

let equalTreesAreIdenticalProperty tree =
    let listOfListOfPositions = getListOfListOfPositions [] tree
    let test =
        listOfListOfPositions |> List.groupBy List.length
                              |> List.forall areListsIdentical


    test
(*
getListOfListOfPositions [] (designTree idSubTree)
getListOfListOfPositions [] (designTree idSubTree)|> List.groupBy List.length
getListOfListOfPositions [] (designTree idSubTree)|> List.groupBy List.length |> List.forall areListsIdentical
*)

//[<Property>]
open NUnit.Framework
[<Test>]
let equalTreesAreIdentical () =
    Assert.IsTrue(testProperty equalTreesAreIdenticalProperty Program.t)