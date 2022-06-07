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


