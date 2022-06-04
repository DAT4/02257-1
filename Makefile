all: report build

report:
	pandoc -o report.pdf report/*.md

