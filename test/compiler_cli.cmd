@echo off

SET vanilla="../Apps/vanilla"

REM you can config WebAssembly output location in VisualStudio
REM and then compile use a specific profile by setting /profile option
REM
%vanilla% "demo_proj\HelloWorld.vbproj" /profile "Release|AnyCPU"

pause