all: report
report:
	pandoc -o report.pdf report/*.md

build: 
	dotnet build src/App
