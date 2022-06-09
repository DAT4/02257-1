// | Author(s)       | date    |
//------------------------------
// | JRA             | June 2  |
// | AA, JL, JRA, MM | June 3  |
module PositionedTree
open TreeTypes

let moveTree (v: float) (PosNode(x, pos, cs)): PosTree<'a> = 
    PosNode(x, pos+v, cs)

let moveExtend (v : float) (e : Extend) : Extend = 
    List.map (fun (a,b) -> (a+v,b+v)) e

let rec mergeExtend (ps : Extend) (qs: Extend) = 
    match (ps,qs) with 
    | ([], qs) -> qs
    | (ps, []) -> ps
    | ((p,_)::ps', (_,q)::qs') -> (p,q)::(mergeExtend ps' qs')

let megergeExtendList (es: Extend list) = 
    List.fold mergeExtend [] es

let rmax (p: float) (q: float) = 
    if p > q then p else q

let rec fit (e1: Extend) (e2: Extend) = 
    match (e1, e2) with 
    | ((_, p)::ps, (q, _)::qs) -> rmax (fit ps qs) (p-q + 1.0)
    | _ -> 0.0

let fitlistl (es: Extend list) : float list =
    let rec f (acc: Extend) (es': Extend list) =
        match es' with
        | [] -> []
        | (e::es'') -> fit acc e |> fun x -> x::f (mergeExtend acc (moveExtend x e)) es''
    f [] es

let fitlistr (es: Extend list): float list =
    let rec f acc es' =
        match es' with
        | [] -> []
        | (e::es'') -> -(fit e acc) |> fun x -> x::f (mergeExtend (moveExtend x e) acc ) es''
    (List.rev (f [] (List.rev es)))

let mean (x: float, y: float) : float = 
    (x+y)/2.0

let fitlist (es: Extend list) : float list = 
    List.map mean (List.zip (fitlistl es) (fitlistr es))

let rec blueprint (Node(x, xs)) =
    List.unzip (List.map blueprint xs) |> fun (ts, es) -> 
    let positions           = fitlist es 
    let ptrees              = List.map (fun (v,t) -> moveTree v t) (List.zip positions ts )
    let ptextends           = List.map (fun (v,e) -> moveExtend v e) (List.zip positions es )
    let resultextend        = (0.0, 0.0) :: megergeExtendList ptextends
    let resulttree          = PosNode(x, 0.0, ptrees)
    (resulttree, resultextend)

let designTree (t: Tree<'a>) : PosTree<'a> =
    fst (blueprint t)

let designExtends (t: Tree<'a>) : Extend = 
    snd (blueprint t)

