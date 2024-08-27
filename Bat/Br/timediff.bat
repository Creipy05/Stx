@echo off
setlocal enabledelayedexpansion

set "start=%1"
set "end=%3"

set "endTime=%time%"

:: Az ido formátumának ellenorzése és vezeto nulla hozzáadása, ha szükséges
if "%startTime:~0,1%"==" " set "start=0%startTime:~1%"
if "%endTime:~0,1%"==" " set "end=0%endTime:~1%"

:: Órák, percek, másodpercek és ezredmásodpercek kivonása
set "startH=%startTime:~0,2%"
set "startM=%startTime:~3,2%"
set "startS=%startTime:~6,2%"
set "startSS=%startTime:~9,2%"

set "endH=%endTime:~0,2%"
set "endM=%endTime:~3,2%"
set "endS=%endTime:~6,2%"
set "endSS=%endTime:~9,2%"

:: Kiírjuk az ido formátumokat ellenorzés céljából
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

:: Idok konvertálása másodpercekbe és ezredmásodpercekbe
set /a startTotal=(startH * 3600) + (startM * 60) + startS
set /a endTotal=(endH * 3600) + (endM * 60) + endS

:: Idok különbsége másodpercekben és ezredmásodpercekben
set /a diffSeconds=endTotal-startTotal
set /a diffSS=endSS-startSS

:: Ha az ezredmásodpercek negatívak, korrigáljuk a különbséget
if !diffSS! lss 0 (
    set /a diffSeconds=diffSeconds-1
    set /a diffSS=100+diffSS
)

:: Teljes eltelt ido másodpercben és ezredmásodpercben
set /a totalDiffSS=(diffSeconds * 100) + diffSS

set /a wholeSeconds=totalDiffSS / 100
set /a fractionalSS=totalDiffSS %% 100

:: Átváltás a tört rész másodpercre
set /a fractionalSeconds=fractionalSS * 100 / 100

:: Kiírjuk az eltelt idot másodpercben törtként
echo Eltelt ido: %wholeSeconds%.%fractionalSeconds% másodperc


echo running time: %totalDiffMS% ms


endlocal & set diff=%totalDiff%

:: Kiírjuk az eltelt idot
rem echo Eltelt ido: %totalDiff% ezredmásodperc

exit /b
