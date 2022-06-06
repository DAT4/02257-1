data Tree a = Node a [Tree a] 
data PosTree a = PosNode a Float [PosTree a] deriving Show
type Span = ( Float, Float )
type Extend = [Span]

moveTree :: Float -> PosTree a -> PosTree a
moveTree v ( PosNode x pos cs ) = PosNode x ( pos+v ) cs

moveExtend :: Float -> Extend -> Extend
moveExtend v es = map ( \( a,b ) -> ( a+v, b+v ) ) es

mergeExtends :: Extend -> Extend -> Extend
mergeExtends ps []                 = ps
mergeExtends [] qs                 = qs
mergeExtends ( ( p,_ ):ps ) ( ( _,q ):qs ) = ( p,q ) : mergeExtends ps qs

mergeExtendLists :: [Extend] -> Extend
mergeExtendLists = foldr mergeExtends [] 

rmax :: Span -> Float
rmax ( p,q ) | p > q = p | otherwise = q

fit :: Extend -> Extend -> Float
fit ( ( _,p ):ps ) ( ( q,_ ):qs ) = rmax( fit ps qs, p-q + 1 )
fit _ _                   = 0

mean :: Span -> Float
mean ( x,y ) = ( x+y ) / 2

fitlist :: [Extend] -> [Float]
fitlist es = map mean $ zip ( h es mergeExtends ) ( h es $ flip mergeExtends )
    where h :: [Extend] -> ( Extend -> Extend -> Extend ) -> [Float]
          h es f = g [] es
              where g :: Extend -> [Extend] -> [Float]
                    g acc [] = []
                    g acc ( e:es ) = v : ( g $ f acc $ moveExtend v e ) es 
                      where v = fit acc e

design' :: Tree a -> ( PosTree a, Extend )
design' ( Node x cs ) = ( resultTree, resultExtent )
    where 
      ( trees, extents ) = unzip $ map design' cs
      positions          = fitlist extents
      ptrees             = map ( \( t,v ) -> moveTree v t ) $ zip trees positions
      pextents           = map ( \( t,v ) -> moveExtend v t ) $ zip extents positions
      resultExtent       = ( 0, 0 ) : mergeExtendLists pextents
      resultTree         = PosNode x 0 ptrees

design :: Tree a -> PosTree a
design t = fst $ design' t

main = putStrLn $ show $ design ( Node "A" [Node "B" [Node "C" [], Node "D" [Node "E" [], Node "F" [], Node "G" [Node "H" [], Node "I" []]]]] )
