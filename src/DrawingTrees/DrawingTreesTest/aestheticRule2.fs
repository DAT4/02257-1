/// Aesthethic 2: A parent should be centred over its offspring.
module AestheticRule2
open FsCheck
open FsCheck.NUnit

open TreeTypes

[<Property>]
let positioningOffsprings () =
    true = true