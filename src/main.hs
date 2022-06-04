data Tree a = Node a [Tree a] 
data PosTree a = PosNode a Float [PosTree a] deriving Show
type Span = (Float, Float)
type Extend = [Span]

moveTree :: (PosTree a, Float) -> PosTree a
moveTree ((PosNode x pos cs), v) = PosNode x (pos+v) cs

moveSpan :: Span -> Float -> Span
moveSpan (x1, x2) v = (x1+v, x2+v)

moveExtend :: (Extend, Float) -> Extend
moveExtend (es, v) = map (\x -> moveSpan x v) es

mergeExtends :: Extend -> Extend -> Extend
mergeExtends ps []                 = ps
mergeExtends [] qs                 = qs
mergeExtends ((p,_):ps) ((_,q):qs) = (p,q) : mergeExtends ps qs

mergeExtendLists :: [Extend] -> Extend
mergeExtendLists = foldr mergeExtends [] 

rmax :: Span -> Float
rmax (p,q) | p > q     = p 
           | otherwise = q

fit :: Extend -> Extend -> Float
fit ((_,p):ps) ((q,_):qs) = rmax(fit ps qs, p-q + 1.0)
fit _ _                   = 0.0

fitlistl' :: Extend -> [Extend] -> [Float]
fitlistl' acc [] = []
fitlistl' acc (e:es) = x : (fitlistl' $ mergeExtends acc $ moveExtend (e,x)) es
    where x = fit acc e

fitlistl :: [Extend] -> [Float]
fitlistl es = fitlistl' [] es

fitlistr' :: Extend -> [Extend] -> [Float]
fitlistr' acc [] = []
fitlistr' acc (e:es) = x : (fitlistl' $ mergeExtends (moveExtend (e,x)) acc) es
    where x = fit acc e

fitlistr :: [Extend] -> [Float]
fitlistr es = reverse $ fitlistr' [] es

mean :: Span -> Float
mean (x,y) = (x+y) / 2

fitlist :: [Extend] -> [Float]
fitlist es = map mean $ zip (fitlistl es) (fitlistr es)

design' :: Tree a -> (PosTree a, Extend)
design' (Node x cs) = (resultTree, resultExtent)
    where 
      (trees, extents) = unzip $ map design' cs
      positions        = fitlist extents
      ptrees           = map moveTree $ zip trees positions
      pextents         = map moveExtend $ zip extents positions
      resultExtent     = (0.0, 0.0) : mergeExtendLists pextents
      resultTree       = PosNode x 0.0 ptrees

design :: Tree a -> PosTree a
design t = fst $ design' t

main = putStrLn $ show $ design (Node "A" [Node "B" [Node "C" [], Node "D" [Node "E" [], Node "F" [], Node "G" [Node "H" [], Node "I" []]]]])
