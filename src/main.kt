class Tree<T>(a: T, List<Tree>)
class PosTree<T>(a: T, pos: Double, List<PosTree>)
type Span = Pair<Double, Double>
type Extend = List<Span>

fun moveTree<T>(v: Double, node: PosTree<T>) = PosTree(node.a, node.

