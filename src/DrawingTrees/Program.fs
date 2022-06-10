// | Author(s)       | date    |
//------------------------------
// | JRA             | June 2  |
// | AA, JL, JRA, MM | June 3  |
// | MM              | June 8  |
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
            | x when x = "visual" -> sprintf "%s" (draw 25 t)
            | x when x = "relative" -> sprintf "%A" (designTree t)
            | x when x = "abs" -> sprintf "%A" (absolutify 1 t)
            | _ -> sprintf "pick one of -> visual | relative | abs"
    printfn "%s" z
    0
