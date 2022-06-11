// | Author(s)       | date    |
//------------------------------
// | MM              | June 8  |
// Note: JL worked on a parallel implementation with a python interface, but it was collectively decided to use this.
module Visualization
open PositionedTree
open TreeTypes

let extremes (e: Extent): float*float = 
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
    let (tree, extents) = blueprint t
    let (left, right)   = extremes extents 
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

    let text px py x y v = 
        let text = sprintf "<text x=\"%i\" y=\"%i\" fill=\"black\">%A</text>\n" x y v
        let line = sprintf "<line x1=\"%i\" y1=\"%i\" x2=\"%i\" y2=\"%i\" style=\"stroke:rgb(0,0,0);stroke-width:2\"/>" px py x y
        if (px+py) = 0 then text else text+line

    let rec content (px: int, py: int) (AbsPosNode(v, (x, y), cs)) =
        let out = text px py x y v
        match cs with
        | [] -> out
        | _ -> out + "\n" + (List.map (content (x,y)) cs |> (String.concat "\n"))
    svg (content (0,0) tree)
