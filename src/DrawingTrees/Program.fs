open TreeTypes
open PositionedTree

//let x = Node("A", [Node("B", []) ; Node("B", []) ; Node("C", []) ; Node("D", [])])
let x = Node("A", [Node("B", []) ; Node("C", []) ; Node("D", []) ])
let z = design x
printfn "%A" z