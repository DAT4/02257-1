// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
module DrawingTreesTest

open NUnit.Framework
open PositionedTree

[<Test>]
let testMeanEqualInput () =
    let expected = 1.0
    let actual = mean (1.0, 1.0)
    Assert.AreEqual (expected, actual)

[<Test>]
let testMeanDifferentInput1 () =
    let expected = 1.0
    let actual = mean (0.0, 2.0)
    Assert.AreEqual (expected, actual)

[<Test>]
let testMeanDifferentInput2 () =
    let expected = 1.5
    let actual = mean (0.0, 3.0)
    Assert.AreEqual (expected, actual)

open FsCheck
//let nf = NormalFloat.op_Explicit
let meanSymmetryProp (a,b) = mean (a,b)=mean(b,a)
//    mean (nf a, nf b) = mean (nf b, nf a)

let normalFloatGenerator =
    Gen.map NormalFloat.op_Explicit
            Arb.generate<NormalFloat>

open FsCheck.NUnit

type MyGenerators =
    static member float() =
        {new Arbitrary<float>() with
            override this.Generator = normalFloatGenerator
            override this.Shrinker _ = Seq.empty }

[<Property(Arbitrary=[|typeof<MyGenerators>|])>]
let symmetryOfMeanTest () =
    meanSymmetryProp