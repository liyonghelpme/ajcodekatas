@echo off
@echo off

if "%1"=="/?" goto HELP

@REM  -------------------------------------------------------------
@REM  You can change server.
@REM  -------------------------------------------------------------
SET sqlServer=%1%
if "%1%"=="" set sqlServer=.\SQLEXPRESS

echo.
echo ********************************************************
echo Creating database NHibernate3SimpleMapping on server %sqlServer%
echo ********************************************************

OSQL -S %sqlServer% -E -b -n -i CreateDatabase.sql"
IF ERRORLEVEL 1 GOTO error

GOTO exit

@REM  -------------------------------------------
@REM  Handle errors
@REM
@REM  Use the following after any call to exit
@REM  and return an error code when errors occur
@REM
@REM  if errorlevel 1 goto :error	
@REM  -------------------------------------------
:error
@ECHO An error occured in CreateDatabase.cmd [%errorLevel%]


GOTO EXIT

@REM  -------------------------------------------
@REM  Help
@REM
@REM  Display usage and exit.
@REM  -------------------------------------------
:HELP
echo .
echo Usage: CreateDatabase.cmd [sqlServer] [/?]

:EXIT
