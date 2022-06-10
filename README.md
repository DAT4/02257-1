# Project 1 

![report](https://github.com/DAT4/02257-1/actions/workflows/main/badge.svg)
![tests](https://github.com/DAT4/02257-1/actions/workflows/dotnet/badge.svg)

## Tasks

### Report

#### Main Sections

- [ ] Project Declaration
- [ ] Design of aesthetic pleasant renderings
  - [ ] what if content size varies 
- [x] Visualization of trees
- [ ] Property based testing
- [ ] Evaluation

#### Other possible Sections

- [ ] Systematic testing
- [ ] Correctness
- [ ] Algorithmic analysis
- [ ] Application

### Implementation

- [ ] Add header comment to each file
  - date
  - author
- [x] Translate code from article
  - [x] Make F# version
  - [x] Make sure that it works
  - [x] Make sure output is correct
- [x] Visualization
  - [x] Make absolute positioned trees 
  - [x] Scale positions for SVG
  - [x] Map absolute positioned trees to SVG
  - [x] No line on root node
- [ ] Find and fix the error about horizontal distance between nodes
- [ ] Make generator for tests
- [ ] Make a set of trees to use when starting the program
  - [ ] Symmetrical tree
  - [ ] Identical sub trees
  - [ ] Simple tree
  - [ ] Complex tree
  - [ ] Trees from paper
  
### Tests

- [ ] Property based 
  - [ ] Two nodes at the same level should be placed at least a given distance apart.
  - [ ] A parent should be centred over its offspring.
  - [ ] Tree drawings should be symmetrical 
  - [ ] Identical sub trees should be rendered identically—their
- [ ] Unit tests
