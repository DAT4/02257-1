type Tree<'a> = Node of 'a * Tree<'a> list
type PosTree<'a> = PosNode of 'a * float * PosTree<'a> list
type Span = (float * float)
type Extend = Span list

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

let extremes (e: Extend): float*float = 
    let (lefts, rights) = List.unzip ( e )
    List.min(lefts), List.max(rights)

let firstPos (rightExtreme: float) (t : PosTree<'a>) : float = 
    let rec f (PosNode(_, pos, cs)) =  
        match pos with 
        | pos when pos < rightExtreme -> pos + (f (List.last cs))
        | _ -> pos
    rightExtreme - f t

//let x = Node("A", [Node("B", []) ; Node("B", []) ; Node("C", []) ; Node("D", [])])

let flatten(start: float) (t: PosTree<'a>) =
    let rec inner (depth: int) (parentPos: float)  (PosNode(x, pos, cs)) = 
        (x,( pos+parentPos, depth)) :: List.collect (inner (depth+1) (parentPos + pos)) cs 
    inner 0 start t

let makeGlobalPositionedLayerCake(t: Tree<'a>) =
    let (tree, extends) = blueprint t
    let (l, r) = extremes extends 
    let width = -int(l*2.0) + int(r*2.0)
    printfn "left: %A right: %A" l r
    let start = int(firstPos r tree) 
    let lines = flatten start tree
    let height = List.max (List.map (fun (_,(_,x)) -> x) lines) * 2
    List.map (fun (a, (h,v)) -> (a, (int((h-l)*2.0), v*2))) lines, width*5, height*5

let draw(t: Tree<'a>) =
    let (layerCake, width, height) = makeGlobalPositionedLayerCake t
    let scaledLayerCake = List.map (fun (a,(v,h)) -> (a,(v*5, h*5))) layerCake
    let svg (content) = sprintf "<svg height=\"%i\" width=\"%i\">\n%A\n</svg>" (height) (width) content
    svg scaledLayerCake

let x = Node("A", [Node("B", []) ; Node("C", []) ; Node("D", []); Node("E", [])])
let z = draw(x)
printfn "%A" z

