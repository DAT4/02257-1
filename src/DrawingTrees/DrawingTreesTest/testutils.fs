module TestUtils
open TreeTypes
open PositionedTree

let flatten (t: Tree<'a>) = 
    let rec f d px (PosNode(_, pos, cs)) = (pos+px, d) :: List.collect (f (d+1) (pos+px)) cs 
    f 0 0 (designTree t) |> List.groupBy (fun (_,d) -> d)
                         |> List.map ( fun (_, xs) -> xs |> List.map (fun x -> fst x) )

let testProperty posTreePropertyFunction tree =
    designTree tree |> posTreePropertyFunction

let getSubtreePositions (PosNode (_, pos, _)) = pos

// --------------- Generators for FsCheck---------------
open FsCheck

// --------------- Tree Generator ---------------

let tree<'a> =
    let rec tree' s =
        match s with
        | 0 -> Gen.map (fun v -> Node(v, [])) Arb.generate<'a>
        | n when n>0 ->
            let subtrees = tree' (n/2)  |> Gen.sample 0 5 |> Gen.constant 
            Gen.map2 (fun v ts -> Node(v, ts)) Arb.generate<'a> subtrees 
        | _ -> invalidArg "s" "Only positive args are allowed"
    Gen.sized tree'

type TreeGenerator =
    static member Tree() =
        {new Arbitrary<Tree<char>>() with
            override x.Generator = tree<char>
            override x.Shrinker t = Seq.empty }

Arb.register<TreeGenerator>() |> ignore

// --------------- Tree Generator ---------------

//let normalFloatGenerator =
//    Gen.map NormalFloat.op_Explicit
//            Arb.generate<NormalFloat>
//
//type MyNormalFloatGenerators =
//    static member float() =
//        {new Arbitrary<float>() with
//            override this.Generator = normalFloatGenerator
//            override this.Shrinker _ = Seq.empty }
//
//
//type E = | X | C of int | Add of E*E;;
//let rec eval x = function
//    | X -> x
//    | C n -> n
//    | Add(e1,e2) -> eval x e1 + eval x e2;;
//let prop1 x e = eval x e = eval x (Add(e,C 0));;
//
//
//let eLeafGen = Gen.oneof [Gen.constant X; Gen.map C Arb.generate<int>]
//
//let rec unSafeEgen() =
//    Gen.oneof [
//        eLeafGen;
//        Gen.map2 (fun x y -> Add(x,y))
//            (unSafeEgen())
//            (unSafeEgen())
//            ];;
//
//
//let leaf value = Node(value, [])
//
//
////type testTree = Leaf of char | Tree of (char, testTree list);;
//
//let leafGen = Gen.map leaf Arb.generate<char>
//
////let rec unsafeTreeGen() = Gen.oneof [leafGen; Gen.map (fun x -> ) unsafeTreeGen()]
