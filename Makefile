all: report build

report:
	pandoc -o report.pdf report/*.md

visual:
	dotnet run --project src/DrawingTrees/ > visual.svg && inkscape visual.svg
