open TreeTypes
open PositionedTree
open Visualization

//let x = Node("A", [Node("B", []) ; Node("B", []) ; Node("C", []) ; Node("D", [])])
//let x = Node("A", [Node("B", []) ; Node("C", []) ; Node("D", []) ])
let x = Node("A", [
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
let z = draw 75 x
printfn "%s" z
