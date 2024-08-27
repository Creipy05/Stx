@echo off
setlocal enabledelayedexpansion

set "start=%1"
set "end=%3"

set "endTime=%time%"

:: Az ido form�tum�nak ellenorz�se �s vezeto nulla hozz�ad�sa, ha sz�ks�ges
if "%startTime:~0,1%"==" " set "start=0%startTime:~1%"
if "%endTime:~0,1%"==" " set "end=0%endTime:~1%"

:: �r�k, percek, m�sodpercek �s ezredm�sodpercek kivon�sa
set "startH=%startTime:~0,2%"
set "startM=%startTime:~3,2%"
set "startS=%startTime:~6,2%"
set "startSS=%startTime:~9,2%"

set "endH=%endTime:~0,2%"
set "endM=%endTime:~3,2%"
set "endS=%endTime:~6,2%"
set "endSS=%endTime:~9,2%"

:: Ki�rjuk az ido form�tumokat ellenorz�s c�lj�b�l
echo startTime: %startTime%
echo endTime: %endTime%
echo Start Hour: %startH%
::echo Start Minute: %startM%
::echo Start Second: %startS%
::echo Start Milliseconds: %startMS%
echo End Hour: %endH%
::echo End Minute: %endM%
::echo End Second: %endS%
::echo End Milliseconds: %endMS%

:: Idok konvert�l�sa m�sodpercekbe �s ezredm�sodpercekbe
set /a startTotal=(startH * 3600) + (startM * 60) + startS
set /a endTotal=(endH * 3600) + (endM * 60) + endS

:: Idok k�l�nbs�ge m�sodpercekben �s ezredm�sodpercekben
set /a diffSeconds=endTotal-startTotal
set /a diffSS=endSS-startSS

:: Ha az ezredm�sodpercek negat�vak, korrig�ljuk a k�l�nbs�get
if !diffSS! lss 0 (
    set /a diffSeconds=diffSeconds-1
    set /a diffSS=100+diffSS
)

:: Teljes eltelt ido m�sodpercben �s ezredm�sodpercben
set /a totalDiffSS=(diffSeconds * 100) + diffSS

set /a wholeSeconds=totalDiffSS / 100
set /a fractionalSS=totalDiffSS %% 100

:: �tv�lt�s a t�rt r�sz m�sodpercre
set /a fractionalSeconds=fractionalSS * 100 / 100

:: Ki�rjuk az eltelt idot m�sodpercben t�rtk�nt
echo Eltelt ido: %wholeSeconds%.%fractionalSeconds% m�sodperc


echo running time: %totalDiffMS% ms


endlocal & set diff=%totalDiff%

:: Ki�rjuk az eltelt idot
rem echo Eltelt ido: %totalDiff% ezredm�sodperc

exit /b
