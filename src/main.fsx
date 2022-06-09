type Tree<'a> = Node of 'a * Tree<'a> list
type PosTree<'a> = PosNode of 'a * float * PosTree<'a> list
type Coordinates = int * int
type AbsPosTree<'a> = AbsPosNode of 'a * Coordinates * AbsPosTree<'a> list
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
    -List.min(lefts), List.max(rights)

let firstPos (rightExtreme: float) (t : PosTree<'a>) : float = 
    let rec f (PosNode(_, pos, cs)) =  
        match (pos, cs) with 
        | _, [] -> pos
        | pos, _ when pos < rightExtreme -> pos + (f (List.last cs))
        | _, _ -> pos
    rightExtreme - f t

let absolutify (scale: int) (t: Tree<'a>) =
    let (tree, extends) = blueprint t
    let (left, right)   = extremes extends 
    let width           = int((left + right) * 2.0)
    let start           = int(firstPos right tree) 
    let rec f (depth: int) (px: float) (PosNode(x, pos, cs)) =
        let (t, d) = 
            match cs with 
            | []  -> [], depth
            | _  -> List.map (f (depth+1) (pos+px)) cs |> List.unzip |> fun (t, d) -> t, List.max d
        AbsPosNode( x, (int((pos+px+left)*2.0)*scale, depth*2*scale),  t ), d 
    let (out, depth) = f 0 start tree 
    out, (width * scale, depth * 2 * scale )

let draw (scale: int) (t: Tree<'a>) =
    let tree, (width, height) = absolutify scale t
    let svg (content) = sprintf "<svg height=\"%i\" width=\"%i\">\n%s\n</svg>" height width content
    let text px py x y v = sprintf "<text x=\"%i\" y=\"%i\" fill=\"black\">%A</text>\n\
                                    <line x1=\"%i\" y1=\"%i\" x2=\"%i\" y2=\"%i\" \
				    style=\"stroke:rgb(0,0,0);stroke-width:2\"/>" x y v px py x y
    let rec content (px: int, py: int) (AbsPosNode(v, (x, y), cs)) =
        let out = text px py x y v
        match cs with
        | [] -> out
        | _ -> out + "\n" + (List.map (content (x,y)) cs |> (String.concat "\n"))
    svg (content (0,0) tree)


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

//let z = absolutify 75 x
//printfn "%A" z

