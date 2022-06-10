all: run build publish visual 

build:
	dotnet build src/DrawingTrees/ 
run:
	dotnet run --project src/DrawingTrees/ relative
	dotnet run --project src/DrawingTrees/ abs
	dotnet run --project src/DrawingTrees/ visual
read:
	pandoc --template report/template/latex.template -V breakurl -V hyphens=URL --pdf-engine=xelatex -o local_report.pdf report/*.md && mupdf local_report.pdf
publish:
	pandoc --template report/template/latex.template -V breakurl -V hyphens=URL --pdf-engine=xelatex -o local_report.pdf report/*.md 
visual:
	dotnet run --project src/DrawingTrees/ visual > visual.svg && inkscape visual.svg
