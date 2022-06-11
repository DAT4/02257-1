# Project 1 

![report](https://img.shields.io/github/workflow/status/dat4/02257-1/report?label=Report&style=for-the-badge)
![tests](https://img.shields.io/github/workflow/status/dat4/02257-1/tests?label=tests&style=for-the-badge)
![tests](https://img.shields.io/github/workflow/status/dat4/02257-1/build?label=build&style=for-the-badge)

By:

- Abby Audet, s212544
- Johan Raunkjær Ott, s032060
- Jinsong Li, s202354
- Martin Mårtensson, s195469

## Tasks

### Report

#### Main Sections

- [x] Project Declaration
- [ ] Design of aesthetic pleasant renderings
  - [ ] Some of the nice to have
  - [ ] What if content size varies (Nice to have)
- [x] Visualization of trees
- [ ] Property based testing
  - [ ] Generators
  - [x] Test 1
  - [x] Test 2
  - [x] Test 3
  - [ ] Test 4
- [ ] Evaluation

#### Other possible Sections

- [ ] Systematic testing
- [ ] Correctness
- [ ] Algorithmic analysis
- [ ] Application

### Implementation

- [x] Add header comment to each file
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
  - [x] Move letter to center of position and shorten lines a bit
- [x] Make generator for tests
- [x] Make a set of trees to use when starting the program
  - [x] Symmetrical tree
  - [x] Identical sub trees
  - [x] Simple tree
  - [x] Complex tree
  - [x] Trees from paper
- [ ] Fix spelling mistakes in code
  
### Tests

- [x] Property based 
  - [x] Two nodes at the same level should be placed at least a given distance apart.
  - [x] A parent should be centred over its offspring.
  - [x] Tree drawings should be symmetrical 
  - [ ] Identical sub trees should be rendered identically—their
- [ ] Unit tests
