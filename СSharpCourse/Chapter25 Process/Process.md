# Процессы и домены приложения
## Процессы

При запуске приложения операционная система создает для него отдельный процесс, которому выделяется определённое адресное пространство в памяти и который изолирован от других процессов. Процесс может иметь несколько потоков. Как минимум, процесс содержит один - главный поток. В приложении на C# точкой входа в программу является метод Main. Вызов этого метода автоматически создает главный поток. А из главного потока могут запускаться вторичные потоки.

В .NET процесс представлен классом Process из пространства имен System.Diagnostics. Этот класс позволяет управлять уже запущенными процессами, а также запускать новые. В данном классе определено ряд свойств и методов, позволяющих получать информацию о процессах и управлять ими:

- Свойство Handle: возвращает дескриптор процесса

- Свойство Id: получает уникальный идентификатор процесса в рамках текущего сеанса ОС

- Свойство MachineName: возвращает имя компьютера, на котором запущен процесс

- Свойство MainModule: представляет основной модуль - исполняемый файл программы, представлен объектом типа ProcessModule

- Свойство Modules: получает доступ к коллекции ProcessModuleCollection, которая в виде объектов ProcessModule хранит набор модулей (например, файлов dll и exe), загруженных в рамках данного процесса

- Свойство ProcessName: возвращает имя процесса, которое нередко совпадает с именем приложения

- Свойство StartTime: возвращает время, когда процесс был запущен

- Свойство PageMemorySize64: возвращает объем памяти, который выделен для данного процесса

- Свойство VirtualMemorySize64: возвращает объем виртуальной памяти, который выделен для данного процесса

- Метод CloseMainWindow(): закрывает окно процесса, который имеет графический интерфейс

- Метод GetProcesses(): возвращает массив всех запущенных процессов

- Метод GetProcessesByName(): возвращает процессы по его имени. Так как можно запустить несколько копий одного приложения, то возвращает массив

- Метод GetProcessById(): возвращает процесс по Id. Так как можно запустить несколько копий одного приложения, то возвращает массив

- Метод Kill(): останавливает процесс

- Метод Start(): запускает новый процесс

Например, исследуем текущий процесс:

```Csharp
using System.Diagnostics;
 
var process = Process.GetCurrentProcess();
Console.WriteLine($"Id: {process.Id}");
Console.WriteLine($"Name: {process.ProcessName}");
Console.WriteLine($"VirtualMemory: {process.VirtualMemorySize64}");
```

Получим все запущенные процессы:

```Csharp
using System.Diagnostics;
 
foreach(Process process in Process.GetProcesses())
{
    // выводим id и имя процесса
    Console.WriteLine($"ID: {process.Id}  Name: {process.ProcessName}");
}
```

Получим id процессов, который представляют запущенные экземпляры Visual Studio:

```Csharp
using System.Diagnostics;
 
Process[] vsProcs = Process.GetProcessesByName("devenv");   // для Windows
// Process[] vsProcs = Process.GetProcessesByName("VisualStudio"); //  для MacOS
foreach (var proc in vsProcs)
    Console.WriteLine($"ID: {proc.Id}");
```

## Потоки процесса
Свойство Threads представляет коллекцию потоков процесса - объект ProcessThreadCollection, каждый поток в которой является объектом ProcessThread. В данном классе можно выделить следующие свойства:

- CurrentPriority: возвращает текущий приоритет потока

- Id: идентификатор потока

- IdealProcessor: позволяет установить процессор для обработки потока

- PriorityLevel: уровень приоритета потока

- StartAddress: адрес в памяти функции, запустившей поток

- StartTime: время запуска потока (поддерживается только на Windows и Linux)

Например, получим все потоки процесса Visual Studio:

```Csharp
using System.Diagnostics;
 
Process proc = Process.GetProcessesByName("devenv")[0];  // Windows
// Process proc = Process.GetProcessesByName("VisualStudio")[0];  // MacOS
ProcessThreadCollection processThreads = proc.Threads;
 
foreach(ProcessThread thread in processThreads)
{
    Console.WriteLine($"ThreadId: {thread.Id}");
}
```

## Модули процесса
Одно приложение может использовать набор различных сторонних библиотек и модулей. Для их получения класс Prosess имеет свойство Modules, которое представляет объект ProcessModuleCollection. Каждый отдельный модуль представлен классом ProcessModule, у которого можно выделить следующие свойства:

- BaseAddress: адрес модуля в памяти
- FileName: полный путь к файлу модуля
- EntryPointAddress: адрес функции в памяти, которая запустила модуль
- ModuleName: название модуля (краткое имя файла)
- ModuleMemorySize: возвращает объем памяти, необходимый для загрузки модуля

Получим все модули, используемые Visual Studio:

```Csharp
using System.Diagnostics;
 
Process proc = Process.GetProcessesByName("devenv")[0]; // для Windows
// Process proc = Process.GetProcessesByName("VisualStudio")[0]; // для MacOS
ProcessModuleCollection modules = proc.Modules;
 
foreach(ProcessModule module in modules)
{
    Console.WriteLine($"Name: {module.ModuleName}  FileName: {module.FileName}");
}
```

## Запуск нового процесса
С помощью статического метода Process.Start() можно запустить новый процесс. Например:

```Csharp
// обращение к исполняемой программе
Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome");
 
// Process.Start("/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"); // на MacOS
```

В данном случае запускается браузер Google Chrome

При обращении к исполняемому файлу .NET запускает приложение.

Однако при запуске некоторых программ может потребоваться передать им различные параметры. В этом случае можно использовать перегруженную версию метода, передавая в качестве второго параметра параметры:

```Csharp
Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome", "https://metanit.com");
```
Чтобы отделить настройку параметров запуска от самого запуска можно использовать класс ProcessStartInfo:

```Csharp
ProcessStartInfo procInfo = new ProcessStartInfo();
// исполняемый файл программы - браузер хром
procInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome";
// аргументы запуска - адрес интернет-ресурса
procInfo.Arguments = "https://metanit.com";
Process.Start(procInfo);
```

# Домены приложений

При запуске приложения, написанного на C#, операционная система создает процесс, а среда CLR создает внутри этого процесса логический контейнер, который называется доменом приложения и внутри которого работает запущенное приложение.

Для управления домена платформа .NET предоставляет класс AppDomain. Рассмотрим некоторые основные методы и свойства данного класса:

- Свойство BaseDirectory: базовый каталог, который используется для получения сборок (как правило, каталог самого приложения)

- Свойство CurrentDomain: домен текущего приложения

- Свойство FriendlyName: имя домена приложения

- Свойство SetupInformation: представляет объект AppDomainSetup и хранит конфигурацию домена приложения

- Метод ExecuteAssembly(): запускает сборку exe в рамках текущего домена приложения

- Метод GetAssemblies(): получает набор сборок .NET, загруженных в домен приложения

Получим имя и базовый каталог текущего домена и выведем все загруженные в домен сборки:

```Csharp
using System.Reflection;
 
AppDomain domain = AppDomain.CurrentDomain;
Console.WriteLine($"Name: {domain.FriendlyName}");
Console.WriteLine($"Base Directory: {domain.BaseDirectory}");
Console.WriteLine();
 
Assembly[] assemblies = domain.GetAssemblies();
foreach (Assembly asm in assemblies)
    Console.WriteLine(asm.GetName().Name);
```
Консольный вывод:

```
Name: HelloApp
Base Directory: /Users/eugene/Projects/HelloApp/HelloApp/bin/Debug/net6.0/

System.Private.CoreLib
HelloApp
System.Runtime
System.Console
System.Threading
Microsoft.Win32.Primitives
System.Collections
System.Memory
```

# AssemblyLoadContext и динамическая загрузка и выгрузка сборок

В статье ```Динамическая загрузка сборок и позднее связывание рассматривалось```, динамически загружать в приложение сборки и использовать их функционал. Но фреймворк .NET также позволяет выгружать сборки, что позволяет уменьшить объем потребляемой памяти. Для этого применяется класс AssemblyLoadContext из пространства имен System.Runtime.Loader, который представляет контекст загрузки и выгрузки сборок. Рассмотрим, как его использовать.

Допустим, у нас есть консольный проект MyApp со следующим файлом Program.cs:

```Csharp
namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = 5;
            var result = Square(number);
            Console.WriteLine($"Квадрат {number} равен {result}");
        }
        static int Square(int n) => n * n;
    }
}
```

Эта программа содержит метод Square для вычисления квадрата, и по умолчанию она компилируется в сборку MyApp.dll. Загрузим эту сборку, чтобы использовать ее метод Square.

Для создания объекта AssemblyLoadContext применяется следующий конструктор:

```Csharp
public AssemblyLoadContext (string? name, bool isCollectible = false);
```

В конструкторе первый параметр устанавливает имя контекста - это может произвольная строка. Второй параметр - isCollectible устанавливает, можно ли загруженные сборки выгружать. Значение true указывает, что загруженные сборки можно выгружать.

Для загрузки сборок класс AssemblyLoadContext предоставляет ряд методов. Некоторые из них:

- Assembly LoadFromAssemblyName (AssemblyName assemblyName): загружает определенную сборку по имени, которое представлено типом System.Reflection.AssemblyName

- Assembly LoadFromAssemblyPath (string assemblyPath): загружает сборку по определенному пути (путь должен быть абсолютным)

- Assembly LoadFromStream (System.IO.Stream stream): загружает определенную сборку из потока Stream

Использовав один из этих методов, мы можем получить доступ к сборке через тип Assembly и обращаться к ее функционалу.

После завершения работы со сборкой мы можем вызвать у AssemblyLoadContext метод Unload() и выгрузить контекст со всеми загруженными сборками и тем самым снизить потребление памяти и увеличить общую производительность.

Рассмотрим полный пример:

```Csharp
using System.Reflection;
using System.Runtime.Loader;
 
Square(8);
// очистка памяти
GC.Collect();
GC.WaitForPendingFinalizers();
 
Console.WriteLine();
// смотрим, какие сборки после выгрузки
foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
    Console.WriteLine(asm.GetName().Name);
 
 
void Square(int number)
{
    var context = new AssemblyLoadContext(name: "Square", isCollectible: true);
    // установка обработчика выгрузки
    context.Unloading += Context_Unloading;
 
    // получаем путь к сборке MyApp
    var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "MyApp.dll");
    // загружаем сборку
    Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
 
    // получаем тип Program из сборки MyApp.dll
    var type = assembly.GetType("MyApp.Program");
    if (type != null)
    {
        // получаем его метод Square
        var squareMethod = type.GetMethod("Square", BindingFlags.Static | BindingFlags.NonPublic);
        // вызываем метод
        var result = squareMethod?.Invoke(null, new object[] { number });
        if (result is int)
        {
            // выводим результат метода на консоль
            Console.WriteLine($"Квадрат числа {number} равен {result}");
        }
    }
 
    // смотим, какие сборки у нас загружены
    foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
        Console.WriteLine(asm.GetName().Name);
 
    // выгружаем контекст
    context.Unload();
}
 
// обработчик выгрузки контекста
void Context_Unloading(AssemblyLoadContext obj)
{
    Console.WriteLine("Библиотека MyApp выгружена");
}
```
Все эти действия оформляются в виде отдельного метода Square(). В качестве параметра он принимает число, квадрат которого надо вычислить.

Вначале в методе создается объект AssemblyLoadContext:

```Csharp
var context = new AssemblyLoadContext(name: "Square", isCollectible: true);
```
Обратите внимание, что параметру isCollectible передается значение true, что позволит выгружать ранее загруженные сборки.

Класс AssemblyLoadContext определяет событие Unloadig, благодаря чему мы можем повесить обработчик и определить момент выгрузки контекста.

```Csharp
context.Unloading += Context_Unloading;
```
Далее используется метод LoadFromAssemblyPath для загузки сборки MyApp.dll по абсолютному пути. В данном случае предполагается, что файл сборки находится в одной папке с текущим приложением.

```Csharp
Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
```
Получив сборку, с помощью рефлексии обращаемся к методу Square и получаем квадрат числа.

Затем смотрим, какие сборки загружены в текущий домен. Среди них мы сможем найти и MyApp.dll. И в конце выгружаем контекст:

```Csharp
context.Unload();
```
Данный метод Square вызывается в методе Main:

```Csharp
Square(8);
// очистка
GC.Collect();
GC.WaitForPendingFinalizers();
```
Но обратите внимание, что выгрузка контекста сама по себе не означает немедленной очистки памяти. Вызов метода Unload только инициирует процесс выгрузки, реальная выгрузка произойдет лишь тогда, когда в дело вступит автоматический сборщик мусора и удалит соответствующие объекты. Поэтому для более быстрой очистки в конце вызываются методы GC.Collect() и GC.WaitForPendingFinalizers().

Консольный вывод:

```
Квадрат числа 8 равен 64

System.Private.CoreLib
HelloApp
System.Runtime
Microsoft.Extensions.DotNetDeltaApplier
System.IO.Pipes
System.Linq
System.Collections
System.Console
System.Runtime.Loader
MyApp
System.Collections.Concurrent
System.Threading
System.Text.Encoding.Extensions
Библиотека MyApp выгружена

System.Private.CoreLib
HelloApp
System.Runtime
Microsoft.Extensions.DotNetDeltaApplier
System.IO.Pipes
System.Linq
System.Collections
System.Console
System.Runtime.Loader
System.Threading.Overlapped
System.Collections.Concurrent
System.Threading
System.Text.Encoding.Extensions
```
Как видно, после выгрузки контекста AssemblyLoadContext сборки MyApp в списке загруженных сборок больше не наблюдается.






