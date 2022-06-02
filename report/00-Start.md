---
title: Report
author: Group 01
date: \today
geometry: margin=2cm
output: pdf_document
---

# Section 1
How to write functional programming 

```fsharp
let rec sum = function
    | [] -> 0
    | x::tail -> x + sum tail

printfn "%i" (sum [1;2;3;4;5;6;7;8;9;10])
```

