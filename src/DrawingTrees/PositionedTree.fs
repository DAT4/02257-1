// | Author(s)       | date    |
//------------------------------
// | JRA             | June 2  |
// | AA, JL, JRA, MM | June 3  |
module PositionedTree
open TreeTypes

let moveTree (v: float) (PosNode(x, pos, cs)): PosTree<'a> = 
    PosNode(x, pos+v, cs)

let moveExtent (v : float) (e : Extent) : Extent = 
    List.map (fun (a,b) -> (a+v,b+v)) e

let rec mergeExtent (ps : Extent) (qs: Extent) = 
    match (ps,qs) with 
    | ([], qs) -> qs
    | (ps, []) -> ps
    | ((p,_)::ps', (_,q)::qs') -> (p,q)::(mergeExtent ps' qs')

let megergeExtentList (es: Extent list) = 
    List.fold mergeExtent [] es

let rmax (p: float) (q: float) = 
    if p > q then p else q

let rec fit (e1: Extent) (e2: Extent) = 
    match (e1, e2) with 
    | ((_, p)::ps, (q, _)::qs) -> rmax (fit ps qs) (p-q + 1.0)
    | _ -> 0.0

let fitlistl (es: Extent list) : float list =
    let rec f (acc: Extent) (es': Extent list) =
        match es' with
        | [] -> []
        | (e::es'') -> fit acc e |> fun x -> x::f (mergeExtent acc (moveExtent x e)) es''
    f [] es

let fitlistr (es: Extent list): float list =
    let rec f acc es' =
        match es' with
        | [] -> []
        | (e::es'') -> -(fit e acc) |> fun x -> x::f (mergeExtent (moveExtent x e) acc ) es''
    (List.rev (f [] (List.rev es)))

let mean (x: float, y: float) : float = 
    (x+y)/2.0

let fitlist (es: Extent list) : float list = 
    List.map mean (List.zip (fitlistl es) (fitlistr es))

let rec blueprint (Node(x, xs)) =
    List.unzip (List.map blueprint xs) |> fun (ts, es) -> 
    let positions           = fitlist es 
    let ptrees              = List.map (fun (v,t) -> moveTree v t) (List.zip positions ts )
    let ptExtents           = List.map (fun (v,e) -> moveExtent v e) (List.zip positions es )
    let resultExtent        = (0.0, 0.0) :: megergeExtentList ptExtents
    let resulttree          = PosNode(x, 0.0, ptrees)
    (resulttree, resultExtent)

let designTree (t: Tree<'a>) : PosTree<'a> =
    fst (blueprint t)

let designExtents (t: Tree<'a>) : Extent = 
    snd (blueprint t)

