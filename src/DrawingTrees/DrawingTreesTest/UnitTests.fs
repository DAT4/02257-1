// --------------------------------------------------
// ./src/DrawingTrees/DrawingTreesTest/UnitTests.fs
// --------------------------------------------------
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 21:50:43 
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 11:50:52 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:56:46 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 14:50:29 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 23:49:23 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 15:38:44 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 09:51:45 

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
open FsCheck.NUnit
open TestUtils
let meanSymmetryProp (a,b) = mean (a,b)=mean(b,a)


[<Property(Arbitrary=[|typeof<NormalFloatGenerators>|])>]
let symmetryOfMeanTest () =
    meanSymmetryProp
