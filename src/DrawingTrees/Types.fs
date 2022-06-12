// --------------------------------------------------
// ./src/DrawingTrees/Types.fs
// --------------------------------------------------
// 
// Author: Johan Ott 
// Date:   Sat Jun 11 02:11:03 
// 
// Author: Johan Ott 
// Date:   Thu Jun 9 15:38:44 
// 
// Author: Martin Maartensson 
// Date:   Thu Jun 9 11:01:38 
// 
// Author: Johan Ott 
// Date:   Tue Jun 7 13:46:07 

module TreeTypes

type Span = (float * float)
type Extent = Span list
type Coordinates = int * int

type Tree<'a> = Node of 'a * Tree<'a> list
type PosTree<'a> = PosNode of 'a * float * PosTree<'a> list
type AbsPosTree<'a> = AbsPosNode of 'a * Coordinates * AbsPosTree<'a> list

