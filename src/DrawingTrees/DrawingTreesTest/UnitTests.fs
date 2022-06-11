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

//open FsCheck
//open FsCheck.NUnit
//open TestUtils
let meanSymmetryProp (a,b) = mean (a,b)=mean(b,a)


//[<Property(Arbitrary=[|typeof<MyNormalFloatGenerators>|])>]
//let symmetryOfMeanTest () =
//    meanSymmetryProp
