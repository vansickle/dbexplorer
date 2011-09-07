[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={#AppId}
AppName={#AppName}
AppVerName={#AppVerName}
AppPublisher=VHQApps
AppPublisherURL=http://www.vhqapps.com/
AppSupportURL=http://www.vhqapps.com/
AppUpdatesURL=http://www.vhqapps.com/
DefaultDirName={pf}\{#AppName}
DefaultGroupName={#AppName}
OutputBaseFilename={#AppName}
SetupIconFile={#Icon}
Compression=lzma
SolidCompression=true
OutputDir=bin

[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}

[Files]
Source: ..\{#AppName}\bin\Debug\{#AppName}.exe; DestDir: {app}; Flags: ignoreversion; Excludes: log*.txt, *.xml, *.pdb
Source: ..\{#AppName}\bin\Debug\*; DestDir: {app}; Flags: ignoreversion recursesubdirs createallsubdirs; Excludes: log*.txt, *.xml, *.pdb, *.vshost*
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: {group}\{#AppName}; Filename: {app}\{#AppName}.exe
Name: {group}\{cm:UninstallProgram,{#AppName}}; Filename: {uninstallexe}
Name: {commondesktop}\{#AppName}; Filename: {app}\{#AppName}.exe; Tasks: desktopicon
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\{#AppName}; Filename: {app}\{#AppName}.exe; Tasks: quicklaunchicon

[Run]
Filename: {app}\{#AppName}.exe; Description: {cm:LaunchProgram,{#AppName}}; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Name: {app}\*.*; Type: filesandordirs
