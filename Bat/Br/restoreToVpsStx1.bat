
set PGPASSWORD "hgo7mpSa"

set IP=217.144.53.190
set PORT=31454
::set IP=localhost
::set PORT=5433

"c:\Program Files\PostgreSQL\16\bin\psql.exe" -h %IP% -p %PORT% -U postgres -c "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = 'Stx1';"
"c:\Program Files\PostgreSQL\16\bin\psql.exe" -h %IP% -p %PORT% -U postgres -c "DROP DATABASE IF EXISTS \"Stx1\";"
"c:\Program Files\PostgreSQL\16\bin\createdb" -h %IP% -p %PORT% -U postgres Stx1

call timestart.bat

"c:\Program Files\PostgreSQL\16\bin\pg_restore.exe" -h %IP% -p %PORT% -U postgres -d Stx1 -1 "Stx.dump"

call timediff.bat

pause