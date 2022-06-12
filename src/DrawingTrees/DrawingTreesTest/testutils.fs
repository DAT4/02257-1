// --------------------------------------------------
// ./src/DrawingTrees/DrawingTreesTest/testutils.fs
// --------------------------------------------------
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 21:57:09 
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 21:50:43 
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 11:50:52 
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 00:15:00 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:56:46 
// 
// Author: Martin Maartensson 
// Date:   Fri Jun 10 14:52:39 
// 
// Author: Martin Maartensson 
// Date:   Fri Jun 10 13:09:59 

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

// --------------- NormalFloat Generator ---------------

let normalFloatGenerator =
    Gen.map NormalFloat.op_Explicit
            Arb.generate<NormalFloat>

type NormalFloatGenerators =
    static member float() =
        {new Arbitrary<float>() with
            override this.Generator = normalFloatGenerator
            override this.Shrinker _ = Seq.empty }
