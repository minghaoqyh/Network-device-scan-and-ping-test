@echo off

echo ****** PostBuildStep ******

echo *** Copying Thirdparty binaries into the output folder ***
echo *** Current folder: %CD% ***

echo *** Copying Uno Service Tool *******
xcopy "bin\Release\FlexnetDecode.dll" "..\UnoServiceTool\" /y /f /i /s

echo *** Copying Uno Service Tool *******
xcopy "bin\Release\FlxConnect64.dll" "..\UnoServiceTool\" /y /f /i /s

echo *** Copying Uno Service Tool *******
xcopy "bin\Release\FlxCore64.dll" "..\UnoServiceTool\" /y /f /i /s

echo *** Copying Uno Service Tool *******
xcopy "bin\Release\MD5Generator.dll" "..\UnoServiceTool\" /y /f /i /s

echo *** Copying Uno Service Tool *******
xcopy "bin\Release\UnoServiceTool.exe" "..\UnoServiceTool\" /y /f /i /s

