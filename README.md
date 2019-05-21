# PassStorage2

Offline and safe password manager for Windows.

App written in C#. It's completely offline. There is two factor login to get content. First password is needed to open application (password is saved in code as hash - used SHA512 algorythm), then user gives master password which is used to encode file content (RijndaelManaged algorythm was used) and it's not saved anywhere. When master password is wrong, everything will be ok but passwords will be wrong decoded.

## Tech Stack
- Visual Studio 2017 / 2019
- .NET Framework 4.6.1
- WPF
- Entity Framework
- WPF UI Framework: _MaterialDesignColors_ and _MaterialDesignThemes_
