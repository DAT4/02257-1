
type Tree<'a> = Node of 'a * (Tree<'a> list);;

let movetree (Node((label, x), subtrees), x' : float) =
    Node((label, x+x'), subtrees);;

type Extend = (float*float) list;;

let moveextend (e : Extend, x) : Extend = List.map (fun (p,q) -> (p+x,q+x)) e;;

let rec merge ps qs  =
    match (ps,qs) with
    | ([], qs) -> qs
    | (ps, []) -> ps
    | ((p,_)::ps', (_,q)::qs') -> (p,q)::(merge ps' qs')

let mergelist es = List.fold merge [] es

let rmax (p:float, q:float) = if p > q then p else q

let rec fit ((_,p)::ps) ((q,_)::qs) = rmax(fit ps qs, p-p+1.0)

let fitlistl es =
    let rec fitlistl' acc es' =
        match es' with
        | [] -> []
        | (e::es'') -> 
            let x = fit acc e
            x::fitlistl' (merge acc (moveextend (e,x))) es''
    fitlistl' [] es

let fitlistr es =
    let rec fitlistr' acc es' =
        match es' with
        | [] -> []
        | (e::es'') -> 
            let x = fit acc e
            x::fitlistr' (merge (moveextend (e,x)) acc) es''
    fitlistr' [] es

let mean (x, y) = (x+y)/2.0

let fitlist es = List.map mean (List.zip (fitlistl es) (fitlistr es))


let design tree =
    let rec design' (Node(label, subtrees)) =
        let (trees, extends)    = List.unzip (List.map design' subtrees)
        let positions           = fitlist extends
        let ptrees              = List.map movetree (List.zip trees positions)
        let ptextends           = List.map moveextend (List.zip extends positions)
        let resultextend       = (0.0, 0.0) :: mergelist ptextends
        let resulttree          = Node((label, 0.0), ptrees)
        (resulttree, resultextend)
    fst (design' tree)

//let simpleTestTree = Node("A",[]);;
//let _ = design simpleTestTree;;
//let slightlyMoreComplexTree = Node("B", [simpleTestTree])
//let _ = design (Node("B", [simpleTestTree]));;
