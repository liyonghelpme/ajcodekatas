
@REM  -------------------------------------------------------------
@REM  You can change server.
@REM  -------------------------------------------------------------
SET sqlServer=%1%
if "%1%"=="" set sqlServer=.\SQLEXPRESS

call CreateDatabase %sqlServer%

rem TBD:
rem call LinkData %sqlServer%
rem call InsertData %sqlServer%

goto end

:end
