all: run build publish visual 

build:
	dotnet build src/DrawingTrees/ 
run: 
	dotnet run --project src/DrawingTrees/ relative

runAll:
	dotnet run --project src/DrawingTrees/ relative
	dotnet run --project src/DrawingTrees/ abs
	dotnet run --project src/DrawingTrees/ visual
read:
	pandoc --toc --template report/template/latex.template -V breakurl -V hyphens=URL --pdf-engine=xelatex -o local_report.pdf report/*.md && mupdf local_report.pdf
publish:
	pandoc --toc --template report/template/latex.template -V breakurl -V hyphens=URL --pdf-engine=xelatex -o local_report.pdf report/*.md 
visual:
	dotnet run --project src/DrawingTrees/ visual > out/visual.svg && inkscape out/visual.svg -o out/visual.png

test:
	dotnet test src/DrawingTrees/DrawingTreesTest 
