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

open TreeTypes
open PositionedTree
open Visualization

//let x = Node("A", [Node("B", []) ; Node("B", []) ; Node("C", []) ; Node("D", [])])
//let x = Node("A", [Node("B", []) ; Node("C", []) ; Node("D", []) ])
let t = Node("A", [
            Node("B", []) ; 
            Node("C", []) ;
            Node("D", [
                Node("D", [
                    Node("E", []);
                    Node("F", [])
                    ])
                ]);
                Node("G", [
                    Node("H", []) ;
                    Node("I", []) ;
                    Node("J", [
                        Node("K", []) ;
                        Node("L", [
                            Node("M", []) ;
                            Node("N", [
                                Node("O", []) ;
                                Node("P", []) ;
                            ]) ;
                        ]) ;
                        Node("Q", []) ;
                        Node("R", [
                            Node("S", []) ;
                            Node("T", [
                                Node("U", []) ;
                                Node("V", []) ;
                            ]) ;
                        ]) ;
                    ]) ;
                ])
            ])

[<EntryPoint>]
let main(args) =
    let l = List.ofArray args
    let z = 
        match (l) with
        | [] -> sprintf "pick one of -> visual | relative | abs" 
        | (x::_) -> 
            match x with 
            | x when x = "visual" -> sprintf "%s" (draw 35 t)
            | x when x = "relative" -> sprintf "%A" (designTree t)
            | x when x = "abs" -> sprintf "%A" (absolutify 1 t)
            | _ -> sprintf "pick one of -> visual | relative | abs"
    printfn "%s" z
    0
