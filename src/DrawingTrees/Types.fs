module TreeTypes

type Tree<'a> = Node of 'a * Tree<'a> list
type PosTree<'a> = PosNode of 'a * float * PosTree<'a> list
type Span = (float * float)
type Extend = Span list
