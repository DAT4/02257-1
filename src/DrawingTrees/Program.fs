// --------------------------------------------------
// ./src/DrawingTrees/Program.fs
// --------------------------------------------------
// 
// Author: Martin Maartensson 
// Date:   Sat Jun 11 12:26:14 
// 
// Author: Martin Maartensson 
// Date:   Fri Jun 10 14:52:47 
// 
// Author: Martin Maartensson 
// Date:   Thu Jun 9 17:20:49 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 15:38:44 
// 
// Author: Martin Maartensson 
// Date:   Thu Jun 9 12:23:24 
// 
// Author: Martin Maartensson 
// Date:   Thu Jun 9 11:01:38 
// 
// Author: Johan Ott 
// Date:   Tue Jun 7 13:46:07 

open ExampleTrees
open Visualization

[<EntryPoint>]
let main(args) =
    let l = List.ofArray args
    let z = 
        match (l) with
        | [] -> sprintf "pick one of -> reportTree | symTree | idSubTree | simpleTree | complexTree | tree1 | tree2 | monster | pathTree " 
        | (x::_) -> 
            match x with 
            | x when x = "reportTree" -> sprintf "%s" (draw 35 reportTree)
            | x when x = "symTree" -> sprintf "%s" (draw 35 symTree)
            | x when x = "idSubTree" -> sprintf "%s" (draw 35 idSubTree)
            | x when x = "simpleTree" -> sprintf "%s" (draw 35 simpleTree)
            | x when x = "complexTree" -> sprintf "%s" (draw 35 complexTree)
            | x when x = "tree1" -> sprintf "%s" (draw 35 tree1)
            | x when x = "tree2" -> sprintf "%s" (draw 35 tree2)
            | x when x = "monster" -> sprintf "%s" (draw 35 monster)
            | x when x = "pathTree" -> sprintf "%s" (draw 35 pathTree)
            | _ -> sprintf "pick one of -> visual | relative | abs"
    printfn "%s" z
    0
