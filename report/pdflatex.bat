cd C:\Users\Dave\Desktop\FYP\fyp\report\report

call:cleanup
pdflatex -shell-escape %2
makeindex %2
bibtex  %2
pdflatex -shell-escape %2
pdflatex -shell-escape %2
call:cleanup

"C:\Program Files (x86)\Foxit Software\Foxit Reader\Foxit Reader.exe" %2.pdf


:cleanup
del /s *.dvi
del /s *.aux
del /s *.bbl
del /s *.blg
del /s *.brf
del /s *.out
del /s *.log
del /s *.lof
del /s *.lot
del /s *.idx
del /s *.glo
del /s *.ilg
del /s *.ind
del /s *.toc
del /s *.pyg
goto:eof