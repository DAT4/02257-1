all: run build publish visual 

build:
	dotnet build src/DrawingTrees/ 
run: 
	dotnet run --project src/DrawingTrees/ simpleTree
read:
	pandoc --toc --template report/template/latex.template -V breakurl -V hyphens=URL --pdf-engine=xelatex -o local_report.pdf report/*.md && mupdf local_report.pdf
overview:
	pandoc --template declaration/template/latex.template -V breakurl -V hyphens=URL --pdf-engine=xelatex -o local_declaration.pdf declaration/*.md && mupdf local_declaration.pdf
reportTree:
	dotnet run --project src/DrawingTrees/ reportTree > out/visual.svg && inkscape out/visual.svg -o out/visual.png
monster:
	dotnet run --project src/DrawingTrees/ monster > out/monster.svg && inkscape out/monster.svg -o out/monster.png
test:
	dotnet test src/DrawingTrees/DrawingTreesTest 
