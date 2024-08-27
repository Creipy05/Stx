pause


set PGPASSWORD "hgo7mpSa"
set IP=localhost
set PORT=5433
::set IP=217.144.53.190
::set PORT=31454

pause
call timestart.bat

"c:\Program Files\PostgreSQL\16\bin\pg_dump.exe" -h %IP% -p %PORT% -U postgres -d Stx1 -F c -f "Stx.dump"

call timediff.bat
pause