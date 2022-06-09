// | Author(s)       | date    |
//------------------------------
// | JRA             | June 2  |
// | AA, JL, JRA, MM | June 3  |
// | MM              | June 8  |
module TreeTypes

type Span = (float * float)
type Extend = Span list
type Coordinates = int * int

type Tree<'a> = Node of 'a * Tree<'a> list
type PosTree<'a> = PosNode of 'a * float * PosTree<'a> list
type AbsPosTree<'a> = AbsPosNode of 'a * Coordinates * AbsPosTree<'a> list

