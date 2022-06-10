module TestUtilities
open FsCheck
open TreeTypes

// --------------- Generators for FsCheck---------------
let normalFloatGenerator =
    Gen.map NormalFloat.op_Explicit
            Arb.generate<NormalFloat>

type MyNormalFloatGenerators =
    static member float() =
        {new Arbitrary<float>() with
            override this.Generator = normalFloatGenerator
            override this.Shrinker _ = Seq.empty }


type E = | X | C of int | Add of E*E;;
let rec eval x = function
    | X -> x
    | C n -> n
    | Add(e1,e2) -> eval x e1 + eval x e2;;
let prop1 x e = eval x e = eval x (Add(e,C 0));;


let eLeafGen = Gen.oneof [Gen.constant X; Gen.map C Arb.generate<int>]

let rec unSafeEgen() =
    Gen.oneof [
        eLeafGen;
        Gen.map2 (fun x y -> Add(x,y))
            (unSafeEgen())
            (unSafeEgen())
            ];;


let leaf value = Node(value, [])


//type testTree = Leaf of char | Tree of (char, testTree list);;

let leafGen = Gen.map leaf Arb.generate<char>

//let rec unsafeTreeGen() = Gen.oneof [leafGen; Gen.map (fun x -> ) unsafeTreeGen()]


