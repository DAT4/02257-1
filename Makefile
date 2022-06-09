all: report build

report:
	pandoc -o report.pdf report/*.md

visual:
	dotnet fsi src/main.fsx > visual.svg && inkscape visual.svg

