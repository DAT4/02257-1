module AesteticRule2

open FsCheck
let nf = NormalFloat.op_Explicit
open FsCheck.NUnit
[<Property>]
let positioningOffsprings () =
    true = true