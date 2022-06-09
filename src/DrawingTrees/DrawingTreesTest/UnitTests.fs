// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
module DrawingTreesTest

open NUnit.Framework
open TreeTypes
open PositionedTree

[<SetUp>]
let Setup () =
    ()


[<Test>]
let Test1 () =
    Assert.Pass()


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
let nf = NormalFloat.op_Explicit
let meanSymmetryProp (a,b) =
    mean (nf a, nf b) = mean (nf b, nf a)

open FsCheck.NUnit
[<Property>]
let symmetryOfMeanTest () =
    meanSymmetryProp