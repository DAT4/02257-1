class Tree<T>(val a: T, val cs: List<Tree<T>> = emptyList())

class PosTree<T>(val a: T, val pos: Double, val cs: List<PosTree<T>> = emptyList()) {
    override fun toString(): String = toString(0)
    private fun toString(i: Int): String =
        "\t".repeat(i) + "$a $pos \n" + cs.joinToString("") { it.toString(i + 1) }
}

typealias Span = Pair<Double, Double>
typealias Extend = List<Span>

fun <T> moveTree(v: Double): (PosTree<T>) -> PosTree<T> = { t -> PosTree(t.a, t.pos + v, t.cs) }

fun moveExtend(v: Double): (Extend) -> Extend = { e -> e.map { (a, b) -> Span(a + v, b + v) } }

fun mergeExtends(e1: Extend): (Extend) -> Extend = { e2 ->
    when {
        e1.isEmpty() -> e2
        e2.isEmpty() -> e1
        else -> listOf(Pair(e1.first().first, e2.first().second)) + mergeExtends(e1.drop(1))(e2.drop(1))
    }
}

fun mergeExtendsList(es: List<Extend>): Extend = es.fold(emptyList()) { a, b -> mergeExtends(a)(b) }

fun rMax(a: Double): (Double) -> Double = { b -> if (a > b) a else b }

fun fit(e1: Extend): (Extend) -> Double = { e2 ->
    when {
        e1.isNotEmpty() && e2.isNotEmpty() ->
            rMax(fit(e1.drop(1))(e2.drop(1)))(e1.first().second - e2.first().first + 1)
        else ->
            0.0
    }
}

fun mean(a: Double, b: Double): Double = (a + b) / 2

fun fitList(es: List<Extend>): List<Double> {
    fun h(es: List<Extend>): ((Extend) -> (Extend) -> Extend) -> List<Double> = { f ->
        fun g(acc: Extend): (List<Extend>) -> List<Double> = { es ->
            when {
                es.isEmpty() -> emptyList()
                else -> fit(acc)(es.first()).let { v ->
                    listOf(v) + g(f(acc)(moveExtend(v)(es.first())))(es.drop(1))
                }
            }
        }
        g(emptyList())(es)
    }
    return h(es)() { p -> { q -> mergeExtends(p)(q) } }
        .zip(h(es)() { p -> { q -> mergeExtends(q)(p) } })
        .map { (a, b) -> mean(a, b) }
}

fun <T> design(tree: Tree<T>): PosTree<T> {
    fun f(tree: Tree<T>): Pair<PosTree<T>, Extend> {
        val (trees, extends) = tree.cs.map { t -> f(t) }.unzip()
        val positions = fitList(extends)
        val pTrees = trees.zip(positions).map { (t, v) -> moveTree<T>(v)(t) }
        val pExtends = extends.zip(positions).map { (e, v) -> moveExtend(v)(e) }
        val resultExtend = listOf(Pair(0.0, 0.0)) + mergeExtendsList(pExtends)
        val resultTree = PosTree(tree.a, 0.0, pTrees)
        return Pair(resultTree, resultExtend)
    }
    return f(tree).first
}

fun main() {
    println(
        design(
            Tree(
                "A", listOf(
                    Tree("B"),
                    Tree("C"),
                    Tree(
                        "D",
                        listOf(
                            Tree("G"),
                            Tree("H")
                        )
                    ),
                    Tree("E"),
                    Tree("F")
                )
            )
        )
    )
}