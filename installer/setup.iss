[Setup]
AppName=EstoqueApp
AppVersion=1.0
DefaultDirName={pf}\EstoqueApp
DefaultGroupName=EstoqueApp
OutputDir=output
OutputBaseFilename=Instalador_EstoqueApp
Compression=lzma
SolidCompression=yes

[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\EstoqueApp"; Filename: "{app}\EstoqueApp.exe"
Name: "{commondesktop}\EstoqueApp"; Filename: "{app}\EstoqueApp.exe"

[Run]
Filename: "{app}\EstoqueApp.exe"; Description: "Executar EstoqueApp"; Flags: nowait postinstall skipifsilent