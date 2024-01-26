# Публикация приложения
## Native AOT

По умолчанию программа на C# компилируется в код на промежуточном языке MSIL. Для запуска подобной программы необходимо, чтобы на компьютере был установлен фреймворк .NET. Однако .NET SDK также позволяет компилировать проект на C# в нативное приложение, которое не использует JIT-компиляцию и может запускаться на компьютере без установленного .NET. Данная модель называется Native AOT и доступна, начиная с .NET 7 (для консольных проектов). Поддержка для ASP.NET Core и для MacOS добавлена в .NET 8.0.

Для использования Native AOT предварительно требуется установить некоторый дополнительный интрументарий. На Windows через установщик Visual Studio необходимо установить нагрузку Desktop development with C++ (Разработка классических приложений на C++).

![](https://metanit.com/sharp/tutorial/pics/publish1.png)

На Ububtu надо установить пакеты clang и zlib1g-dev

```
sudo apt-get install clang zlib1g-dev
```

На MacOS надо установить Command Line Tools for XCode.

Чтобы указать, что проект будет использовать модель публикации ```Native AOT```, в файле проекта в элемент PropertyGroup необходимо добавить элемент ```<PublishAot>true</PublishAot>```:

```
<PropertyGroup>
    <PublishAot>true</PublishAot>
</PropertyGroup>
```
Для компиляции с помощью .NET CLI применяется следующая команда:

```
dotnet publish -r <RID>
```
В частности, некоторые распространенные идентификаторы:

- Для Windows:

- - win-x64

- - win-x86

- - win-arm64

- Для Linux:

- - linux-x64

- - linux-arm64

- Для MacOS:

- - osx-x64

- - osx-arm64

Полный список идентификаторов можно посмотреть на странице https://github.com/dotnet/runtime/blob/main/src/libraries/Microsoft.NETCore.Platforms/src/runtime.json

Например, для компиляции приложения для 64-битной Windows применяется команда:

```
dotnet publish -r win-x64 -c Release
```
А для компиляции для 64-битной Linux на архитектуре ARM (AArch64) используется команда

```
dotnet publish -r linux-arm64 -c Release
```
К примеру, возьмем простейший проект консольного приложения. Сначала в файл проекта csproj добавим параметр

![](https://metanit.com/sharp/tutorial/pics/publish2.png)

Перейдем в консоли к папке проекта. Допустим, нам надо скомпилировать приложение для 64-разрядной Windows. Для этого выполним команду:

```
dotnet publish -r win-x64 -c Release
```
В результате получим примерно следующий вывод консоли:

```
C:\Users\eugen\source\repos\csharp\Native\HelloApp>dotnet publish -r win-x64 -c Release
MSBuild version 17.6.0-preview-23174-01+e7de13307 for .NET
  Determining projects to restore...
  Restored C:\Users\eugen\source\repos\csharp\Native\HelloApp\HelloApp\HelloApp.csproj (in 265 ms).
C:\Program Files\dotnet\sdk\8.0.100-preview.3.23178.7\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInf
erence.targets(287,5): message NETSDK1057: You are using a preview version of .NET. See: https://aka.ms/dotnet-support-
policy [C:\Users\eugen\source\repos\csharp\Native\HelloApp\HelloApp\HelloApp.csproj]
  HelloApp -> C:\Users\eugen\source\repos\csharp\Native\HelloApp\HelloApp\bin\Release\net8.0\win-x64\HelloApp.dll
  Generating native code
  HelloApp -> C:\Users\eugen\source\repos\csharp\Native\HelloApp\HelloApp\bin\Release\net8.0\win-x64\publish\

C:\Users\eugen\source\repos\csharp\Native\HelloApp>
```

После этого в проекте в папке bin\Release\netX.X\win-x64\publish\ можно найти скомпилированное приложение.

![](https://metanit.com/sharp/tutorial/pics/publish3.png)

В итоге скомпилированный файл exe можно запускать на 64-разрядных Windows даже там, где не установлен .NET.

Один из главных минусов компиляции в нативное приложение - это его размер, например, как видно на скриншоте при компиляции с .NET 8.0 Preview простое консольное приложение, которое выводит на консоль строку, занимает 1793 Кб. Но стоит отметить, что по сравнению с .NET 7.0 в .NET 8.0 размер нативного приложения значительно уменьшен, и команда разработчиков .NET продолжает работать над этим, поэтому в будущем размер приложения может ужаться значительно. Кроме того, можно применять различные оптимизации. Самая простейшая - использовать инвариантный режим для параметров глобализации, для чего надо добавить в файл проекта настройку

```Csharp
<InvariantGlobalization>true</InvariantGlobalization>
```
Так, только эта настройка позволила в моем случае уменьшить размер приложения с 1793 КБ до 1567 КБ

В дополнение к большему размеру стоит отметить и некоторые другие недостатки подобной модели публикации. Прежде всего не для всех типов проектов, библиотек и сред данная модель может быть доступна. Так, поддержка консольных приложений добавлена с самого начала - в .NET 7.0. А вот для ASP.NET Core эта модель доступна только начиная с .NET 8.0. Кроме того, поддержка MacOS также добавлена толькр в .NET 8.0 (для Windows и Linux поддержка имеет уже в .NET 7)

Также для таких нативных приложений недоступна динамическая загрузка библиотек (например, с помощью метода Assembly.LoadFile).
