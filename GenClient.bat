@echo off
for %%i in (*.proto) do (
   echo gen %%~nxi...
   protoc.exe --csharp_out=../Tools/cs  %%~nxi)

echo finish... 
pause