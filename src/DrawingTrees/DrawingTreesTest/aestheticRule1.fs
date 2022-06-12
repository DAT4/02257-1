// --------------------------------------------------
// ./src/DrawingTrees/DrawingTreesTest/aestheticRule1.fs
// --------------------------------------------------
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 23:08:22 
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 11:50:52 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:56:46 
// 
// Author: Johan Ott 
// Date:   Fri Jun 10 20:39:21 
// 
// Author: Martin Maartensson 
// Date:   Fri Jun 10 14:52:39 
// 
// Author: Martin Maartensson 
// Date:   Fri Jun 10 13:11:17 
// 
// Author: 2015-INSPIRER 
// Date:   Fri Jun 10 09:09:38 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 15:38:44 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 11:58:52 
// 
// Author: Johan Ott 
// Date:   Wed Jun 8 10:33:08 

module AestheticRule1

open FsCheck.NUnit
open TreeTypes
open TestUtils

[<Property(Arbitrary=[|typeof<TreeGenerator>|])>]
let minimum_distance_check(t: Tree<char>) = 
    let isInOrder xs = xs
                       |> Seq.pairwise 
                       |> Seq.forall (fun (a, b) -> a <= b-1.0)
    flatten t |> Seq.forall isInOrder
