module TestUtils
open TreeTypes
open PositionedTree

let flatten (t: Tree<'a>) = 
    let rec f d px (PosNode(_, pos, cs)) = (pos+px, d) :: List.collect (f (d+1) (pos+px)) cs 
    f 0 0 (designTree t) |> List.groupBy (fun (_,d) -> d)
                         |> List.map ( fun (_, xs) -> xs |> List.map (fun x -> fst x) )

let isOrdered xs = xs
                  |> Seq.pairwise 
                  |> Seq.forall (fun (a, b) -> a <= b)
