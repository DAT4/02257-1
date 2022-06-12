// --------------------------------------------------
// ./src/DrawingTrees/ExampleTrees.fs
// --------------------------------------------------
// 
// Author: Abby Audet 
// Date:   Fri Jun 10 16:05:47 
// 
// Author: Abby Audet 
// Date:   Fri Jun 10 14:53:19 
// 
// Author: Abby Audet 
// Date:   Fri Jun 10 14:47:25 

module ExampleTrees
open TreeTypes

// Symmetrical Tree
let symTree = Node("A", [
                Node("B", [
                    Node("E", []) ;
                    Node("F", [])
                ]) ;
                Node("C", [
                    Node("G", [])
                ]) ;
                Node("D", [
                    Node("H", []) ;
                    Node("I", [])
                ])
])

// Identical SubTrees
let idSubTree = Node("A", [
                    Node("B", [
                        Node("D", []) ;
                        Node("E", [])
                    ]) ;
                    Node("C", [
                        Node("F", []) ;
                        Node("G", [])
                    ])
])

// Simple Tree
let simpleTree = Node("A", [
                    Node("B", []) ;
                    Node("C", [])
])

// Complex Tree
let complexTree = Node("A", [
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

// Tree 1 from paper
let tree1 = Node("A", [
                Node("B", [
                    Node("C", []) ;
                    Node("D", []) ;
                    Node("E", []) 
                ]) ;
                Node("F", []) ;
                Node("G", [
                    Node("H", []) ;
                    Node("I", []) ;
                    Node("J", []) 
                ])
])

// Tree 2 from paper
let tree2 = Node("A", [
                Node("B", [
                    Node("C", []) ;
                    Node("D", []) ;
                    Node("E", []) 
                ]) ;
                Node("F", [
                    Node("G", [
                        Node("H", []) ;
                        Node("I", []) ;
                        Node("J", []) 
                    ])
                ])
])

// Monster tre
let monster = Node("A", [
                Node("B", [
                    Node("C", [
                        Node("D", []) ;
                        Node("E", [
                            Node("F", [
                                Node("G", []) ;
                                Node("H", [
                                    Node("I", []) ;
                                    Node("J", []) ;
                                    Node("K", []) ;
                                    Node("L", []) 
                                ]) ;
                                Node("M", []) ;
                                Node("N", [
                                    Node("O", []) 
                                ]) 
                            ]) 
                        ]) 
                    ]) ;
                    Node("P", [
                        Node("Q", []) ;
                        Node("R", []) 
                    ]) 
                ]) ;
                Node("S", [
                    Node("T", [
                        Node("U", []) ;
                        Node("V", []) ;
                        Node("W", [
                            Node("X", [
                                Node("Y", []) 
                            ]) ;
                            Node("Z", [
                                Node("a", []) ;
                                Node("b", []) ;
                                Node("c", []) ;
                                Node("d", []) 
                            ]) 
                        ]) 
                    ]) ;
                    Node("e", [
                        Node("f", [
                            Node("g", []) 
                        ]) ;
                        Node("h", []) ;
                        Node("i", [
                            Node("j", [
                                Node("k", []) ;
                                Node("l", []) ;
                                Node("m", []) ;
                                Node("n", []) 
                            ]) 
                        ]) 
                    ]) 
                ]) ;
                Node("o", [
                    Node("p", [
                        Node("q", [
                            Node("r", []) ;
                            Node("s", []) ;
                            Node("t", []) ;
                            Node("u", []) 
                        ]) ;
                        Node("v", [
                            Node("w", []) ;
                            Node("x", [
                                Node("y", []) ;
                                Node("z", []) 
                            ]) ;
                            Node("0", []) ;
                            Node("1", []) 
                        ]) ;
                        Node("2", []) 
                    ]) 
                ])
])

// Pathological Tree
let pathTree = Node("A", [
                Node("B", [
                    Node("C", [
                        Node("D", [
                            Node("E", []) ;
                            Node("F", []) 
                        ]) ;
                        Node("G", [
                            Node("H", []) 
                        ]) 
                    ]) ;
                    Node("I", [
                        Node("J", [
                            Node("K", []) 
                        ]) 
                    ]) 
                ]) ;
                Node("L", [
                    Node("M", [
                        Node("N", [
                            Node("O", []) 
                        ]) 
                    ]) 
                ]) 
])
