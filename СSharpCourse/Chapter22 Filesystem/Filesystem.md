# Работа с файловой системой

Большинство задач в программировании так или иначе связаны с работой с файлами и каталогами. Нам может потребоваться прочитать текст из файла или наоборот произвести запись, удалить файл или целый каталог, не говоря уже о более комплексных задачах, как например, создание текстового редактора и других подобных задачах.

Фреймворк .NET предоставляет большие возможности по управлению и манипуляции файлами и каталогами, которые по большей части сосредоточены в пространстве имен System.IO. Классы, расположенные в этом пространстве имен (такие как Stream, StreamWriter, FileStream и др.), позволяют управлять файловым вводом-выводом.

## Работа с дисками
Работу с файловой системой начнем с самого верхнего уровня - дисков. Для представления диска в пространстве имен System.IO имеется класс DriveInfo.

Этот класс имеет статический метод GetDrives(), который возвращает имена всех логических дисков компьютера. Также он предоставляет ряд полезных свойств:

- AvailableFreeSpace: указывает на объем доступного свободного места на диске в байтах

- DriveFormat: получает имя файловой системы

- DriveType: представляет тип диска

- IsReady: готов ли диск (например, DVD-диск может быть не вставлен в дисковод)

- Name: получает имя диска

- RootDirectory: возвращает корневой каталог диска

- TotalFreeSpace: получает общий объем свободного места на диске в байтах

- TotalSize: общий размер диска в байтах

- VolumeLabel: получает или устанавливает метку тома

Получим имена и свойства всех дисков на компьютере:

```Csharp
DriveInfo[] drives = DriveInfo.GetDrives();
 
foreach (DriveInfo drive in drives)
{
    Console.WriteLine($"Название: {drive.Name}");
    Console.WriteLine($"Тип: {drive.DriveType}");
    if (drive.IsReady)
    {
        Console.WriteLine($"Объем диска: {drive.TotalSize}");
        Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
        Console.WriteLine($"Метка диска: {drive.VolumeLabel}");
    }
    Console.WriteLine();
}
```
Консольный вывод в моем случае:

```
Название: C:\ 
Тип: Fixed
Объем диска: 624823205888
Свободное пространство: 58199781376
Метка диска: 

Название: D:\ 
Тип: Fixed
Объем диска: 105372446720
Свободное пространство: 3887951872
Метка диска: Windows7

Название: E:\ 
Тип: Fixed
Объем диска: 17809010688
Свободное пространство: 463650816
Метка диска: RECOVERY

Название: G:\ 
Тип: Fixed
Объем диска: 954200064
Свободное пространство: 275193856
Метка диска: 
```


# Работа с каталогами

Для работы с каталогами в пространстве имен System.IO предназначены сразу два класса: Directory и DirectoryInfo.

## Класс Directory
Статический класс Directory предоставляет ряд методов для управления каталогами. Некоторые из этих методов:

- CreateDirectory(path): создает каталог по указанному пути path

- Delete(path): удаляет каталог по указанному пути path

- Exists(path): определяет, существует ли каталог по указанному пути path. Если существует, возвращается true, если не существует, то false

- GetCurrentDirectory(): получает путь к текущей папке

- GetDirectories(path): получает список подкаталогов в каталоге path

- GetFiles(path): получает список файлов в каталоге path

- GetFileSystemEntries(path): получает список подкаталогов и файлов в каталоге path

- Move(sourceDirName, destDirName): перемещает каталог

- GetParent(path): получение родительского каталога

- GetLastWriteTime(path): возвращает время последнего изменения каталога

- GetLastAccessTime(path): возвращает время последнего обращения к каталогу

- GetCreationTime(path): возвращает время создания каталога

## Класс DirectoryInfo
Данный класс предоставляет функциональность для создания, удаления, перемещения и других операций с каталогами. Во многом он похож на Directory, но не является статическим.

Для создания объекта класса DirectoryInfo применяется конструктор, который в качестве параметра принимает путь к каталогу:

```Csharp
public DirectoryInfo (string path);
```

Основные методы класса DirectoryInfo:

- Create(): создает каталог

- CreateSubdirectory(path): создает подкаталог по указанному пути path

- Delete(): удаляет каталог

- GetDirectories(): получает список подкаталогов папки в виде массива DirectoryInfo

- GetFiles(): получает список файлов в папке в виде массива FileInfo

- MoveTo(destDirName): перемещает каталог

Основные свойства класса DirectoryInfo:

- CreationTime: представляет время создания каталога

- LastAccessTime: представляет время последнего доступа к каталогу

- LastWriteTime: представляет время последнего изменения каталога

- Exists: определяет, существует ли каталог

- Parent: получение родительского каталога

- Root: получение корневого каталога

- Name: имя каталога

- FullName: полный путь к каталогу

## Directory или DirectoryInfo
Как видно из функционала, оба класса предоставляют похожие возможности. Когда же и что использовать? Если надо совершить одну-две операции с одним каталогом, то проще использовать класс Directory. Если необходимо выполнить последовательность операций с одним и тем же каталогом, то лучше воспользоваться классом DirectoryInfo. Почему? Дело в том, что методы класса Directory выполняют дополнительные проверки безопасности. А для класса DirectoryInfo такие проверки не всегда обязательны.

Посмотрим на примерах применение этих классов

## Получение списка файлов и подкаталогов

```Csharp
string dirName = "C:\\";
// если папка существует
if (Directory.Exists(dirName))
{
    Console.WriteLine("Подкаталоги:");
    string[] dirs = Directory.GetDirectories(dirName);
    foreach (string s in dirs)
    {
        Console.WriteLine(s);
    }
    Console.WriteLine();
    Console.WriteLine("Файлы:");
    string[] files = Directory.GetFiles(dirName);
    foreach (string s in files)
    {
        Console.WriteLine(s);
    }
}
```
Обратите внимание на использование слешей в именах файлов. Либо мы используем двойной слеш: "C:\\", либо одинарный, но тогда перед всем путем ставим знак @: @"C:\Program Files"

Аналогичный пример с DirectoryInfo:

```Csharp
string dirName = "C:\";
 
var directory = new DirectoryInfo(dirName);
 
if (directory.Exists)
{
    Console.WriteLine("Подкаталоги:");
    DirectoryInfo[] dirs = directory.GetDirectories();
    foreach (DirectoryInfo dir in dirs)
    {
        Console.WriteLine(dir.FullName);
    }
    Console.WriteLine();
    Console.WriteLine("Файлы:");
    FileInfo[] files = directory.GetFiles();
    foreach (FileInfo file in files)
    {
        Console.WriteLine(file.FullName);
    }
}
```

## Фильтрация папок и файлов
Методы получения папок и файлов позволяют выполнять фильтрацию. В качестве фильтра в эти методы передается шаблон, который может содержать два плейсхолдера: * или символ-звездочка (соответствует любому количеству символов) и ? или вопросительный знак (соответствует одному символу)

Например, найдем все папки, которые начинаются на "books":

```Csharp
// класс Directory
string[] dirs = Directory.GetDirectories(dirName, "books*.");
 
// класс DirectoryInfo
var directory = new DirectoryInfo(dirName);
DirectoryInfo[] dirs = directory.GetDirectories("books*.");
```
Или получим все файлы с расширением ".exe":

```Csharp
// класс Directory
string[] files = Directory.GetFiles(dirName, "*.exe");
 
// класс DirectoryInfo
var directory = new DirectoryInfo(dirName);
FileInfo[] files = directory.GetFiles("*.exe");
```
## Создание каталога
Класс DirectoryInfo

```Csharp
string path = @"C:\SomeDir";
string subpath = @"program\avalon";
DirectoryInfo dirInfo = new DirectoryInfo(path);
if (!dirInfo.Exists)
{
    dirInfo.Create();
}
dirInfo.CreateSubdirectory(subpath);
```
Вначале проверяем, а нету ли такой директории, так как если она существует, то ее создать будет нельзя, и приложение выбросит ошибку. В итоге у нас получится следующий путь: "C:\SomeDir\program\avalon"

Аналогичный пример с классом Directory:

```Csharp
string path = @"C:\SomeDir";
string subpath = @"program\avalon";
if (!Directory.Exists(path))
{
    Directory.CreateDirectory(path);
}
Directory.CreateDirectory($"{path}/{subpath}");
```
## Получение информации о каталоге

```Csharp
string dirName = "C:\\Program Files";
 
DirectoryInfo dirInfo = new DirectoryInfo(dirName);
 
Console.WriteLine($"Название каталога: {dirInfo.Name}");
Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
```
## Удаление каталога
Если мы просто применим метод Delete к непустой папке, в которой есть какие-нибудь файлы или подкаталоги, то приложение нам выбросит ошибку. Поэтому нам надо передать в метод Delete дополнительный параметр булевого типа, который укажет, что папку надо удалять со всем содержимым. Кроме того, перед удалением следует проверить наличие удаляемой папки, иначе приложение выбросит исключение:

```Csharp
string dirName = @"C:\SomeDir";
 
DirectoryInfo dirInfo = new DirectoryInfo(dirName);
if (dirInfo.Exists)
{
    dirInfo.Delete(true);
    Console.WriteLine("Каталог удален");
}
else
{
    Console.WriteLine("Каталог не существует");
}
```
Или так:

```Csharp
string dirName = @"C:\SomeDir";
if (Directory.Exists(dirName))
{
    Directory.Delete(dirName, true);
    Console.WriteLine("Каталог удален");
}
else
{
    Console.WriteLine("Каталог не существует");
}
```
## Перемещение каталога
При перемещении надо учитывать, что новый каталог, в который мы хотим перемесить все содержимое старого каталога, не должен существовать.

```Csharp
string oldPath = @"C:\SomeFolder";
string newPath = @"C:\SomeDir";
DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
if (dirInfo.Exists && !Directory.Exists(newPath))
{
    dirInfo.MoveTo(newPath);
    // или так
    // Directory.Move(oldPath, newPath);
}
```
Перемещение каталога в рамках одной папки (как в примере выше) фактически аналогично переименованию папки

# Работа с файлами. Классы File и FileInfo

Подобно паре Directory/DirectoryInfo для работы с файлами предназначена пара классов File и FileInfo. С их помощью мы можем создавать, удалять, перемещать файлы, получать их свойства и многое другое.

## FileInfo
Некоторые полезные методы и свойства класса FileInfo:

- CopyTo(path): копирует файл в новое место по указанному пути path

- Create(): создает файл

- Delete(): удаляет файл

- MoveTo(destFileName): перемещает файл в новое место

- Свойство Directory: получает родительский каталог в виде объекта DirectoryInfo

- Свойство DirectoryName: получает полный путь к родительскому каталогу

- Свойство Exists: указывает, существует ли файл

- Свойство Length: получает размер файла

- Свойство Extension: получает расширение файла

- Свойство Name: получает имя файла

- Свойство FullName: получает полное имя файла

Для создания объекта FileInfo применяется конструктор, который получает в качестве параметра путь к файлу:

```Csharp
FileInfo fileInf = new FileInfo(@"C:\app\content.txt");
```

## File
Класс File реализует похожую функциональность с помощью статических методов:

- Copy(): копирует файл в новое место

- Create(): создает файл

- Delete(): удаляет файл

- Move: перемещает файл в новое место

- Exists(file): определяет, существует ли файл

## Пути к файлам
Для работы с файлами можно применять как абсолютные, так и относительные пути:

```Csharp
// абсолютные пути
string path1 = @"C:\Users\eugene\Documents\content.txt";  // для Windows
string path2 = "C:\\Users\\eugene\\Documents\\content.txt";  // для Windows
string path3 = "/Users/eugene/Documents/content.txt";  // для MacOS/Linux
 
// относительные пути
string path4 = "MyDir\\content.txt";  // для Windows
string path5 = "MyDir/content.txt";  // для MacOS/Linux
```
## Получение информации о файле

```Csharp
string path = @"C:\Users\eugene\Documents\content.txt";
// string path = "/Users/eugene/Documents/content.txt";  // для MacOS/Linux
FileInfo fileInfo = new FileInfo(path);
if (fileInfo.Exists)
{
    Console.WriteLine($"Имя файла: {fileInfo.Name}");
    Console.WriteLine($"Время создания: {fileInfo.CreationTime}");
    Console.WriteLine($"Размер: {fileInfo.Length}");
}
```
## Удаление файла

```Csharp
string path = @"C:\app\content.txt";
FileInfo fileInf = new FileInfo(path);
if (fileInf.Exists)
{
   fileInf.Delete();
   // альтернатива с помощью класса File
   // File.Delete(path);
}
```
## Перемещение файла

```Csharp
string path =  @"C:\OldDir\content.txt";
string newPath = @"C:\NewDir\index.txt";
FileInfo fileInf = new FileInfo(path);
if (fileInf.Exists)
{
   fileInf.MoveTo(newPath);       
   // альтернатива с помощью класса File
   // File.Move(path, newPath);
}
```
Если файл по новому пути уже существует, то с помощью дополнительного параметра можно указать, надо ли перезаписать файл (при значении true файл перезаписывается)

```Csharp
string path =  @"C:\OldDir\content.txt";
string newPath = @"C:\NewDir\index.txt";
FileInfo fileInf = new FileInfo(path);
if (fileInf.Exists)
{
   fileInf.MoveTo(newPath, true);       
   // альтернатива с помощью класса File
   // File.Move(path, newPath, true);
}
```
## Копирование файла

```Csharp
string path =  @"C:\OldDir\content.txt";
string newPath = @"C:\NewDir\index2.txt";
FileInfo fileInf = new FileInfo(path);
if (fileInf.Exists)
{
   fileInf.CopyTo(newPath, true);      
   // альтернатива с помощью класса File
   // File.Copy(path, newPath, true);
}
```
Метод CopyTo класса FileInfo принимает два параметра: путь, по которому файл будет копироваться, и булевое значение, которое указывает, надо ли при копировании перезаписывать файл (если true, как в случае выше, файл при копировании перезаписывается). Если же в качестве последнего параметра передать значение false, то если такой файл уже существует, приложение выдаст ошибку.

Метод Copy класса File принимает три параметра: путь к исходному файлу, путь, по которому файл будет копироваться, и булевое значение, указывающее, будет ли файл перезаписываться.

## Чтение и запись файлов
В дополнение к вышерассмотренным методам класс File также предоставляет ряд методов для чтения-записи текстовых и бинарных файлов:

- AppendAllLines(String, IEnumerable<String>) / AppendAllLinesAsync(String, IEnumerable<String>, CancellationToken)

добавляют в файл набор строк. Если файл не существует, то он создается

- AppendAllText(String, String) / AppendAllTextAsync(String, String, CancellationToken)

добавляют в файл строку. Если файл не существует, то он создается

- byte[] ReadAllBytes (string path) / Task<byte[]> ReadAllBytesAsync (string path, CancellationToken cancellationToken)

считывают содержимое бинарного файла в массив байтов

- string[] ReadAllLines (string path) / Task<string[]> ReadAllLinesAsync (string path, CancellationToken cancellationToken)

считывают содержимое текстового файла в массив строк

- string ReadAllText (string path) / Task<string> ReadAllTextAsync (string path, CancellationToken cancellationToken)

считывают содержимое текстового файла в строку

- IEnumerable<string> ReadLines (string path)

считывают содержимое текстового файла в коллекцию строк

- void WriteAllBytes (string path, byte[] bytes) / Task WriteAllBytesAsync (string path, byte[] bytes, CancellationToken cancellationToken)

записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует, то перезаписывается

- void WriteAllLines (string path, string[] contents) / Task WriteAllLinesAsync (string path, IEnumerable<string> contents, CancellationToken cancellationToken)

записывают массив строк в текстовый файл. Если файл не существует, он создается. Если существует, то перезаписывается

- WriteAllText (string path, string? contents) / Task WriteAllTextAsync (string path, string? contents, CancellationToken cancellationToken)

записывают строку в текстовый файл. Если файл не существует, он создается. Если существует, то перезаписывается

Как видно, эти методы покрывают практически все основные сценарии - чтение и запись текстовых и бинарных файлов. Причем в зависимости от задачи можно применять как синхронные методы, так и их асинхронные аналоги.

Например, запишем и считаем обратно в строку текстовый файл:

```Csharp
string path = @"c:\app\content.txt";
 
string originalText = "Hello Metanit.com";
// запись строки
await File.WriteAllTextAsync(path, originalText);
// дозапись в конец файла
await File.AppendAllTextAsync(path, "\nHello work");
 
// чтение файла
string fileText = await File.ReadAllTextAsync(path);
Console.WriteLine(fileText);
```
Консольный вывод:

```
Hello Metanit.com
Hello work
```
Стоит отметить, что при добавлении текста я добавил в строку последовательность "\n", которая выполняет перевод на следующую строку. Благодаря этому добавляемый текст располагается в файле на новой строке.

Если мы хотим, что в файле изначально шло добавление на новую строку, то для записи стоит использовать метод WriteAllLines/ WriteAllLinesAsync, а для добавления - AppendAllLines / AppendAllLinesAsync

```Csharp
await File.WriteAllLinesAsync(path, new[] { "Hello Metanit.com", "Hello work" });
```
Аналогично при чтении файла если мы хотим каждую строку файла считать отдельно, то вместо ReadAllText / ReadAllTextAsync применяется ReadAllLines / ReadAllLinesAsync.

### Кодировка
В качестве дополнительного параметра методы чтения-записи текстовых файлов позволяют установить кодировку в виде объекта System.Text.Encoding:

```Csharp
using System.Text;
 
string path = "/Users/eugene/Documents/app/content.txt";
 
string originalText = "Привет Metanit.com";
// запись строки
await File.WriteAllTextAsync(path, originalText, Encoding.Unicode);
// дозапись в конец файла
await File.AppendAllTextAsync(path, "\nПривет мир", Encoding.Unicode);
 
// чтение файла
string fileText = await File.ReadAllTextAsync(path, Encoding.Unicode);
Console.WriteLine(fileText);
```

Для установки кодировки при записи и чтении здесь применяется встроенное значение Encoding.Unicode. Также можно указать название кодировки, единственное следует удостовериться, что текущая операционная система поддерживает выбранную кодировку:

```Csharp
using System.Text;
 
string path = @"c:\app\content.txt";
 
string originalText = "Hello Metanit.com";
// запись строки
await File.WriteAllTextAsync(path, originalText, Encoding.GetEncoding("iso-8859-1"));
// дозапись в конец файла
await File.AppendAllTextAsync(path, "\nHello code", Encoding.GetEncoding("iso-8859-1"));
 
// чтение файла
string fileText = await File.ReadAllTextAsync(path, Encoding.GetEncoding("iso-8859-1"));
Console.WriteLine(fileText);
```

# FileStream. Чтение и запись файла

Класс FileStream представляет возможности по считыванию из файла и записи в файл. Он позволяет работать как с текстовыми файлами, так и с бинарными.

## Создание FileStream
Для создания объекта FileStream можно использовать как конструкторы этого класса, так и статические методы класса File. Конструктор FileStream имеет множество перегруженных версий, из которых отмечу лишь одну, самую простую и используемую:

```Csharp
FileStream(string filename, FileMode mode)
```

Здесь в конструктор передается два параметра: путь к файлу и перечисление FileMode. Данное перечисление указывает на режим доступа к файлу и может принимать следующие значения:

- Append: если файл существует, то текст добавляется в конец файл. Если файла нет, то он создается. Файл открывается только для записи.

- Create: создается новый файл. Если такой файл уже существует, то он перезаписывается

- CreateNew: создается новый файл. Если такой файл уже существует, то приложение выбрасывает ошибку

- Open: открывает файл. Если файл не существует, выбрасывается исключение

- OpenOrCreate: если файл существует, он открывается, если нет - создается новый

- Truncate: если файл существует, то он перезаписывается. Файл открывается только для записи.

Другой способ создания объекта FileStream представляют статические методы класса File:

```Csharp
FileStream File.Open(string file, FileMode mode);
FileStream File.OpenRead(string file);
FileStream File.OpenWrite(string file);
```

Первый метод открывает файл с учетом объекта FileMode и возвращает файловой поток FileStream. У этого метода также есть несколько перегруженных версий. Второй метод открывает поток для чтения, а третий открывает поток для записи.

## Закрытие потока
Класс FileStream для освобождения всех реусрсов, связанных с файлом, реализует интерфейс IDisposable. Соответственно после завершения работы с FileStream необходимо освободить связанный с ним файл вызовом метода Dispose. Для корректного закрытия можно вызвать метод Close(), который вызывает метод Dispose:

```Csharp
FileStream? fstream = null;
try
{
    fstream = new FileStream("note3.dat", FileMode.OpenOrCreate);
    // операции с fstream
}
catch(Exception ex)
{  }
finally
{
    fstream?.Close();
}
```
Либо можно использовать конструкцию using, которая автоматически освободит все связанные с FileStream ресурсы:

```Csharp
using (FileStream fstream = new FileStream("note3.dat", FileMode.OpenOrCreate))
{
    // операции с fstream
}
```

## Свойства и методы FileStream
Рассмотрим наиболее важные свойства класса FileStream:

- Свойство Length: возвращает длину потока в байтах

- Свойство Position: возвращает текущую позицию в потоке

- Свойство Name: возвращает абсолютный путь к файлу, открытому в FileStream

Для чтения/записи файлов можно применять следующие методы класса FileStream:

- void CopyTo(Stream destination): копирует данные из текущего потока в поток destination

- Task CopyToAsync(Stream destination): асинхронная версия метода CopyTo

- void Flush(): сбрасывает содержимое буфера в файл

- Task FlushAsync(): асинхронная версия метода Flush

- int Read(byte[] array, int offset, int count): считывает данные из файла в массив байтов и возвращает количество успешно считанных байтов. Принимает три параметра:

- array - массив байтов, куда будут помещены считываемые из файла данные

- offset представляет смещение в байтах в массиве array, в который считанные байты будут помещены

- count - максимальное число байтов, предназначенных для чтения. Если в файле находится меньшее количество байтов, то все они будут считаны.

- Task<int> ReadAsync(byte[] array, int offset, int count): асинхронная версия метода Read

- long Seek(long offset, SeekOrigin origin): устанавливает позицию в потоке со смещением на количество байт, указанных в параметре offset.

- void Write(byte[] array, int offset, int count): записывает в файл данные из массива байтов. Принимает три параметра:

- array - массив байтов, откуда данные будут записываться в файл

- offset - смещение в байтах в массиве array, откуда начинается запись байтов в поток

- count - максимальное число байтов, предназначенных для записи

- Task WriteAsync(byte[] array, int offset, int count): асинхронная версия метода Write

## Чтение и запись файлов
FileStream представляет доступ к файлам на уровне байтов, поэтому, например, если вам надо считать или записать одну или несколько строк в текстовый файл, то массив байтов надо преобразовать в строки, используя специальные методы. Поэтому для работы с текстовыми файлами применяются другие классы.

В то же время при работе с различными бинарными файлами, имеющими определенную структуру, FileStream может быть очень даже полезен для извлечения определенных порций информации и ее обработки.

Посмотрим на примере считывания-записи в текстовый файл:

```Csharp
using System.Text;
 
string path = @"C:\app\note.txt";   // путь к файлу
 
string text = "Hello METANIT.COM"; // строка для записи
 
// запись в файл
using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
{
    // преобразуем строку в байты
    byte[] buffer = Encoding.Default.GetBytes(text);
    // запись массива байтов в файл
    await fstream.WriteAsync(buffer, 0, buffer.Length);
    Console.WriteLine("Текст записан в файл");
}
 
// чтение из файла
using (FileStream fstream = File.OpenRead(path))
{
    // выделяем массив для считывания данных из файла
    byte[] buffer = new byte[fstream.Length];
    // считываем данные
    await fstream.ReadAsync(buffer, 0, buffer.Length);
    // декодируем байты в строку
    string textFromFile = Encoding.Default.GetString(buffer);
    Console.WriteLine($"Текст из файла: {textFromFile}");
}
```

Разберем этот пример. Вначале определяем путь к файлу и текст для записи в файл.

И при чтении, и при записи для создания и удаления объекта FileStream используется конструкция using, по завершению которой у созданного объекта FileStream автоматически вызывается метод Dispose, и, таким образом, объект уничтожается.

Поскольку операции с файлами могут занимать продолжительное время и являются узким местом в работе программы, рекомендуется использовать асинхронные версии методов FileStream. И при записи, и при чтении применяется объект кодировки Encoding.Default из пространства имен System.Text. В данном случае мы используем два его метода: GetBytes для получения массива байтов из строки и GetString для получения строки из массива байтов.

В итоге введенная нами строка записывается в файл note.txt. И мы получим следующий консольный вывод:

```
Текст записан в файл
Текст из файла: Hello METANIT.COM
```
Записанный файл по сути представляет бинарный файл (не текстовый), хотя если мы в него запишем только строку, то сможем посмотреть в удобочитаемом виде этот файл, открыв его в текстовом редакторе. Однако если мы в него запишем случайные байты, например:

```Csharp
fstream.WriteByte(13);
fstream.WriteByte(103);
```
То у нас могут возникнуть проблемы с его пониманием. Поэтому для работы непосредственно с текстовыми файлами предназначены отдельные классы - StreamReader и StreamWriter.

## Произвольный доступ к файлам
Нередко бинарные файлы представляют определенную структуру. И, зная эту структуру, мы можем взять из файла нужную порцию информации или наоброт записать в определенном месте файла определенный набор байтов. Например, в wav-файлах непосредственно звуковые данные начинаются с 44 байта, а до 44 байта идут различные метаданные - количество каналов аудио, частота дискретизации и т.д.

С помощью метода Seek() мы можем управлять положением курсора потока, начиная с которого производится считывание или запись в файл. Этот метод принимает два параметра: offset (смещение) и позиция в файле. Позиция в файле описывается тремя значениями:

- SeekOrigin.Begin: начало файла

- SeekOrigin.End: конец файла

- SeekOrigin.Current: текущая позиция в файле

Курсор потока, с которого начинается чтение или запись, смещается вперед на значение offset относительно позиции, указанной в качестве второго параметра. Смещение может быть отрицательным, тогда курсор сдвигается назад, если положительное - то вперед.

Рассмотрим простой пример:

```Csharp
using System.Text;
 
string path = "note.dat";
 
string text = "hello world";
 
using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
{
    // преобразуем строку в байты
    byte[] input = Encoding.Default.GetBytes(text);
    // запись массива байтов в файл
    fstream.Write(input, 0, input.Length);
    Console.WriteLine("Текст записан в файл");
}
// чтение части файла
using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
{ 
    // перемещаем указатель в конец файла, до конца файла- пять байт
    fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока
 
    // считываем четыре символов с текущей позиции
    byte[] output = new byte[5];
    await fstream.ReadAsync(output, 0, output.Length);
    // декодируем байты в строку
    string textFromFile = Encoding.Default.GetString(output);
    Console.WriteLine($"Текст из файла: {textFromFile}"); // world
}
```
Вначале записываем в файл текст "hello world". Затем снова обращаемся к файла для считывания. Сначала перемещаем курсор на пять символов назад относительно конца файлового потока:

```Csharp
fstream.Seek(-5, SeekOrigin.End)
```

![](https://metanit.com/sharp/tutorial/pics/5.3.png)

То есть после выполнения этого вызова курсор будет стоять на позиции символа "w".

После этого считываем пять байт начиная с символа "w". В кодировке по умолчанию 1 символ будет представлять 1 байт. Поэтому чтение 5 байт будет эквивалентно чтению пяти сиволов: "world".

Соответственно мы получим следующий консольный вывод:

```
Текст записан в файл
Текст из файла: world
```
Рассмотрим чуть более сложный пример - с записью начиная с некоторой позиции:

```Csharp
using System.Text;
 
string path = "note2.dat";
 
string text = "hello world";
 
// запись в файл
using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
{
    // преобразуем строку в байты
    byte[] input = Encoding.Default.GetBytes(text);
    // запись массива байтов в файл
    fstream.Write(input, 0, input.Length);
    Console.WriteLine("Текст записан в файл");
}
using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
{ 
    // заменим в файле слово world на слово house
    string replaceText = "house";
    fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока
    byte[] input = Encoding.Default.GetBytes(replaceText);
    await fstream.WriteAsync(input, 0, input.Length);
 
    // считываем весь файл
    // возвращаем указатель в начало файла
    fstream.Seek(0, SeekOrigin.Begin);
    byte[] output = new byte[fstream.Length];
    await fstream.ReadAsync(output, 0, output.Length);
    // декодируем байты в строку
    string textFromFile = Encoding.Default.GetString(output);
    Console.WriteLine($"Текст из файла: {textFromFile}"); // hello house
}
```
Здесь также вначале записываем в файл строку "hello world". Затем также открываем файл и опять же перемещаемся в конец файла, не доходя до конца пять символов (то есть опять же с позиции символа "w"), и осуществляем запись строки "house". Таким образом, строка "house" заменяет строку "world".

Чтобы после этого считать весь файл, сдвигаем курсор на самое начало

```Csharp
fstream.Seek(0, SeekOrigin.Begin);
```
Консольный вывод программы:

```
Текст записан в файл
Текст из файла: hello house
```

# Чтение и запись текстовых файлов. StreamReader и StreamWriter

Для работы непосредственно с текстовыми файлами в пространстве System.IO определены специальные классы: StreamReader и StreamWriter.

Запись в файл и StreamWriter
Для записи в текстовый файл используется класс StreamWriter. Некоторые из его конструкторов, которые могут применяться для создания объекта StreamWriter:

- StreamWriter(string path): через параметр path передается путь к файлу, который будет связан с потоком

- StreamWriter(string path, bool append): параметр append указывает, надо ли добавлять в конец файла данные или же перезаписывать файл. Если равно true, то новые данные добавляются в конец файла. Если равно false, то файл перезаписываетсяя заново

- StreamWriter(string path, bool append, System.Text.Encoding encoding): параметр encoding указывает на кодировку, которая будет применяться при записи

Свою функциональность StreamWriter реализует через следующие методы:

- int Close(): закрывает записываемый файл и освобождает все ресурсы

- void Flush(): записывает в файл оставшиеся в буфере данные и очищает буфер.

- Task FlushAsync(): асинхронная версия метода Flush

- void Write(string value): записывает в файл данные простейших типов, как int, double, char, string и т.д. Соответственно имеет ряд перегруженных версий для записи данных элементарных типов, например, Write(char value), Write(int value), Write(double value) и т.д.

- Task WriteAsync(string value): асинхронная версия метода Write. Обратите внимание, что асинхронные версии есть не для всех перегрузок метода Write.

- void WriteLine(string value): также записывает данные, только после записи добавляет в файл символ окончания строки

- Task WriteLineAsync(string value): асинхронная версия метода WriteLine

Рассмотрим запись в файл на примере:

```Csharp
string path = "note1.txt";
string text = "Hello World\nHello METANIT.COM";
 
// полная перезапись файла 
using (StreamWriter writer = new StreamWriter(path, false))
{
    await writer.WriteLineAsync(text);
}
// добавление в файл
using (StreamWriter writer = new StreamWriter(path, true))
{
    await writer.WriteLineAsync("Addition");
    await writer.WriteAsync("4,5");
}
```
В данном случае два раза создаем объект StreamWriter. В первом случае если файл существует, то он будет перезаписан. Если не существует, он будет создан. И в нее будет записан текст из переменной text. Во втором случае файл открывается для дозаписи, и будут записаны атомарные данные - строка и число.

По завершении в папке программы мы сможем найти файл note.txt, который будет иметь следующие строки:

```
Hello World
Hello METANIT.COM
Addition
4,5
```
В пример выше будет использоваться кодировка по умолчанию. но также можно задать ее явным образом:

```Csharp
using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default))
{
    // операции с writer
}
```

## Чтение из файла и StreamReader
Класс StreamReader позволяет нам легко считывать весь текст или отдельные строки из текстового файла.

Некоторые из конструкторов класса StreamReader:

- StreamReader(string path): через параметр path передается путь к считываемому файлу

- StreamReader(string path, System.Text.Encoding encoding): параметр encoding задает кодировку для чтения файла

Среди методов StreamReader можно выделить следующие:

- void Close(): закрывает считываемый файл и освобождает все ресурсы

- int Peek(): возвращает следующий доступный символ, если символов больше нет, то возвращает -1

- int Read(): считывает и возвращает следующий символ в численном представлении. Имеет перегруженную версию: Read(char[] array, int index, int count), где array - массив, куда считываются символы, index - индекс в массиве array, начиная с которого записываются считываемые символы, и count - максимальное количество считываемых символов

- Task<int> ReadAsync(): асинхронная версия метода Read

- string ReadLine(): считывает одну строку в файле

- string ReadLineAsync(): асинхронная версия метода ReadLine

- string ReadToEnd(): считывает весь текст из файла

- string ReadToEndAsync(): асинхронная версия метода ReadToEnd

Сначала считаем текст полностью из ранее записанного файла:

```Csharp
string path = "note1.txt";
// асинхронное чтение
using (StreamReader reader = new StreamReader(path))
{
    string text = await reader.ReadToEndAsync();
    Console.WriteLine(text);
}
```
Считаем текст из файла построчно:

```Csharp
string path = "/Users/eugene/Documents/app/note1.txt";
 
// асинхронное чтение
using (StreamReader reader = new StreamReader(path))
{
    string? line;
    while ((line = await reader.ReadLineAsync()) != null)
    {
        Console.WriteLine(line);
    }
}
```

В данном случае считываем построчно через цикл while: while ((line = await reader.ReadLineAsync()) != null) - сначала присваиваем переменной line результат функции reader.ReadLineAsync(), а затем проверяем, не равна ли она null. Когда объект sr дойдет до конца файла и больше строк не останется, то метод reader.ReadLineAsync() будет возвращать null.

# Бинарные файлы. BinaryWriter и BinaryReader

Для работы с бинарными файлами предназначена пара классов BinaryWriter и BinaryReader. Эти классы позволяют читать и записывать данные в двоичном формате.

## BinaryWriter
Для создания объекта BinaryWriter можно применять ряд конструкторов. Возьмем наиболее простую:

```Csharp
BinaryWriter(Stream stream)
```
в его конструктор передается объект Stream (обычно это объект FileStream).

Основные методы класса BinaryWriter

- Close(): закрывает поток и освобождает ресурсы

- Flush(): очищает буфер, дописывая из него оставшиеся данные в файл

- Seek(): устанавливает позицию в потоке

- Write(): записывает данные в поток. В качестве параметра этот метод может принимать значения примитивных данных:

- Write(bool)

- Write(byte)

- Write(char)

- Write(decimal)

- Write(double)

- Write(Half)

- Write(short)

- Write(int)

- Write(long)

- Write(sbyte)

- Write(float)

- Write(string)

- Write(ushort)

- Write(uint)

- Write(ulong)

Либо можно передать массивы типов byte и char

- Write(byte[])

- Write(char[])

- Write(ReadOnlySpan<byte>)

- Write(ReadOnlySpan<char>)

При записи массива дополнительно можно указать, с кого элемента массива надо выполнять запись, а также число записываемых элементов массива:

- Write(byte[], int, int)

- Write(char[], int, int)

Рассмотрим простейшую запись бинарного файла:

```Csharp
string path = "person.dat";
 
// создаем объект BinaryWriter
using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
{
    // записываем в файл строку
    writer.Write("Tom");
    // записываем в файл число int
    writer.Write(37);
    Console.WriteLine("File has been written");
}
```
Здесь в файл person.dat записываются два значения: строка "Tom" и число 37. Для создание объекта применяется вызов new BinaryWriter(File.Open(path, FileMode.OpenOrCreate))

Подобным образом можно сохранять более сложные данные. Например, сохраним в файл массив объектов:

```Csharp
string path = "people.dat";
 
// массив для записи
Person[] people =
{
    new Person("Tom", 37),
    new Person("Bob", 41)
};
 
using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
{
    // записываем в файл значение каждого свойства объекта
    foreach (Person person in people)
    {
        writer.Write(person.Name);
        writer.Write(person.Age);
    }
    Console.WriteLine("File has been written");
}
 
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```
В данном случае последовательно сохраняем в файл people.dat данные объектов Person из массива people.

## BinaryReader
Для создания объекта BinaryReader можно применять ряд конструкторов. Возьмем наиболее простую версию:

```Csharp
Reader(Stream stream)
```
в его конструктор также передается объект Stream (также обычно это объект FileStream).

Основные методы класса BinaryReader

- Close(): закрывает поток и освобождает ресурсы

- ReadBoolean(): считывает значение bool и перемещает указатель на один байт

- ReadByte(): считывает один байт и перемещает указатель на один байт

- ReadChar(): считывает значение char, то есть один символ, и перемещает указатель на столько байтов, сколько занимает символ в текущей кодировке

- ReadDecimal(): считывает значение decimal и перемещает указатель на 16 байт

- ReadDouble(): считывает значение double и перемещает указатель на 8 байт

- ReadInt16(): считывает значение short и перемещает указатель на 2 байта

- ReadInt32(): считывает значение int и перемещает указатель на 4 байта

- ReadInt64(): считывает значение long и перемещает указатель на 8 байт

- ReadSingle(): считывает значение float и перемещает указатель на 4 байта

- ReadString(): считывает значение string. Каждая строка предваряется значением длины строки, которое представляет 7-битное целое число

С чтением бинарных данных все просто: соответствующий метод считывает данные определенного типа и перемещает указатель на размер этого типа в байтах, например, значение типа int занимает 4 байта, поэтому BinaryReader считает 4 байта и переместит указатель на эти 4 байта.

Например, выше в примере с BinaryWriter в файл person.dat записывалась строка и число. Считаем их с помощью BinaryReader:

```Csharp
using (BinaryReader reader = new BinaryReader(File.Open("person.dat", FileMode.Open)))
{
    // считываем из файла строку
    string name = reader.ReadString();
    // считываем из файла число 
    int age = reader.ReadInt32();
    Console.WriteLine($"Name: {name}  Age: {age}");
}
```
Конструктор класса BinaryReader также в качестве параметра принимает объект потока, только в данном случае устанавливаем в качестве режима FileMode.Open: new BinaryReader(File.Open("person.dat", FileMode.Open)).

В каком порядке данные были записаны в файл, в таком порядке мы их можем оттуда считать. То есть если сначала записывалась строка, а потом число, то в данном порядке мы их можем считать из файла.

Или подобным образом считаем данные из файла people.dat, который был записан в примере выше и который содержит данные объектов Person:

```Csharp
// список для считываемых данных
List<Person> people = new List<Person>();
 
// создаем объект BinaryWriter
using (BinaryReader reader = new BinaryReader(File.Open("people.dat", FileMode.Open)))
{
    // пока не достигнут конец файла
    // считываем каждое значение из файла
    while (reader.PeekChar() > -1)
    {
        string name = reader.ReadString();
        int age = reader.ReadInt32();
        // по считанным данным создаем объект Person и добавляем в список
        people.Add(new Person(name, age));
    }
}
// выводим содержимое списка people на консоль
foreach(Person person in people)
{
    Console.WriteLine($"Name: {person.Name}  Age: {person.Age}");
}
 
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```
Здесь в цикле while считываем данные. Чтобы узнать окончание потока, вызываем метод PeekChar(). Этот метод считывает следующий символ и возвращает его числовое представление. Если символ отсутствует, то метод возвращает -1, что будет означать, что мы достигли конца файла.

В цикле последовательно считываем значения для свойств объектов Person в том же порядке, в каком они записывались.

# Архивация и сжатие файлов

Кроме классов чтения-записи .NET предоставляет классы, которые позволяют сжимать файлы, а также затем восстанавливать их в исходное состояние.

Это классы ZipFile, DeflateStream и GZipStream, которые находятся в пространстве имен System.IO.Compression и представляют реализацию одного из алгоритмов сжатия Deflate или GZip.

## GZipStream и DeflateStream
Для создания объекта GZipStream можно использовать один из его конструкторов:

- GZipStream(Stream stream, CompressionLevel level): stream представляет данные, а level задает уровень сжатия
- GZipStream(Stream stream, CompressionMode mode): mode указывает, будут ли данные сжиматься или, наоборот, восстанавливаться и может принимать два значения:
CompressionMode.Compress: данные сжимаются

CompressionMode.Decompress: данные восстанавливаются

Если данные сжимаются, то stream указывает на поток архивируемых данных. Если данные восстанавливаются, то stream указывает на поток, куда будут передаваться восстановленные данные.

- GZipStream(Stream stream, CompressionLevel level, bool leaveMode): параметр leaveMode указывает, надо ли оставить открытым поток stream после удаления объекта GZipStream. Если значение true, то поток остается открытым
- GZipStream(Stream stream, CompressionMode mode, bool leaveMode)
Для управления сжатием/восстанавлением данных GZipStream предоставляет ряд методов. Основые из них:

- void CopyTo(Stream destination): копирует все данные в поток destination
- Task CopyToAsync(Stream destination): асинхронная версия метода CopyTo
- void Flush(): очищает буфер, записывая все его данные в файл
- Task FlushAsync(): асинхронная версия метода Flush
- int Read(byte[] array, int offset, int count): считывает данные из файла в массив байтов и возвращает количество успешно считанных байтов. Принимает три параметра:

array - массив байтов, куда будут помещены считываемые из файла данные

offset представляет смещение в байтах в массиве array, в который считанные байты будут помещены

count - максимальное число байтов, предназначенных для чтения. Если в файле находится меньшее количество байтов, то все они будут считаны.

- int Read(byte[] array, int offset, int count): считывает данные из файла в массив байтов и возвращает количество успешно считанных байтов. Принимает три параметра:

array - массив байтов, куда будут помещены считываемые из файла данные

offset представляет смещение в байтах в массиве array, в который считанные байты будут помещены

count - максимальное число байтов, предназначенных для чтения. Если в файле находится меньшее количество байтов, то все они будут считаны.

Task<int> ReadAsync(byte[] array, int offset, int count): асинхронная версия метода Read

- long Seek(long offset, SeekOrigin origin): устанавливает позицию в потоке со смещением на количество байт, указанных в параметре offset.

- void Write(byte[] array, int offset, int count): записывает в файл данные из массива байтов. Принимает три параметра:

array - массив байтов, откуда данные будут записываться в файл

offset - смещение в байтах в массиве array, откуда начинается запись байтов в поток

count - максимальное число байтов, предназначенных для записи

- Task WriteAsync(byte[] array, int offset, int count): асинхронная версия метода Write

Рассмотрим применение класса GZipStream на примере:

```Csharp
using System.IO.Compression;
 
string sourceFile = "book.pdf"; // исходный файл
string compressedFile = "book.gz"; // сжатый файл
string targetFile = "book_new.pdf"; // восстановленный файл
 
// создание сжатого файла
await CompressAsync(sourceFile, compressedFile);
// чтение из сжатого файла
await DecompressAsync(compressedFile, targetFile);
 
async Task CompressAsync(string sourceFile, string compressedFile)
{
    // поток для чтения исходного файла
    using FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate);
    // поток для записи сжатого файла
    using FileStream targetStream = File.Create(compressedFile);
 
    // поток архивации
    using GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
    await sourceStream.CopyToAsync(compressionStream); // копируем байты из одного потока в другой
 
    Console.WriteLine($"Сжатие файла {sourceFile} завершено.");
    Console.WriteLine($"Исходный размер: {sourceStream.Length}  сжатый размер: {targetStream.Length}");
}
 
async Task DecompressAsync(string compressedFile, string targetFile)
{
    // поток для чтения из сжатого файла
    using FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate);
    // поток для записи восстановленного файла
    using FileStream targetStream = File.Create(targetFile);
    // поток разархивации
    using GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);
    await decompressionStream.CopyToAsync(targetStream);
    Console.WriteLine($"Восстановлен файл: {targetFile}");
}
```
В данном случае подразумевается, что в папке с программой располагается файл book.pdf, который собственно и будет архивироваться.

Метод CompressAsync получает название исходного файла, который надо архивировать, и название будущего сжатого файла.

Сначала создается поток для чтения из исходного файла - FileStream sourceStream. Затем создается поток для записи в сжатый файл - FileStream targetStream. Поток архивации GZipStream compressionStream инициализируется потоком targetStream и с помощью метода CopyToAsync() получает данные от потока sourceStream.

Метод DecompressAsync производит обратную операцию по восстановлению сжатого файла в исходное состояние. Он принимает в качестве параметров пути к сжатому файлу и будущему восстановленному файлу.

Здесь в начале создается поток для чтения из сжатого файла FileStream sourceStream, затем поток для записи в восстанавливаемый файл FileStream targetStream. В конце создается поток GZipStream decompressionStream, который с помощью метода CopyToAsync() копирует восстановленные данные в поток targetStream.

Чтобы указать потоку GZipStream, для чего именно он предназначен - сжатия или восстановления - ему в конструктор передается параметр CompressionMode, принимающий два значения: Compress и Decompress.

Пример консольного вывода программы:

```
Сжатие файла book.pdf завершено.
Исходный размер: 3235353  сжатый размер: 2574401
Восстановлен файл: book_new.pdf 
```
Если бы захотели бы использовать другой класс сжатия - DeflateStream, то мы могли бы просто заменить в коде упоминания GZipStream на DeflateStream, без изменения остального кода. Их использование идентично.

В то же время при использовании этих классов есть некоторые ограничения, в частности, мы можем сжимать только один файл. Для архивации группы файлы лучше выбрать другие инструменты, например, ZipFile.

## ZipFile
Статический класс ZipFile из простанства имен System.IO.Compression предоставляет дополнительные возможности для создания архивов. Он позволяет создавать архив из каталогов. Его основные методы:

- void CreateFromDirectory(string sourceDirectoryName, string destinationFileName): архивирует папку по пути sourceDirectoryName в файл с названием destinationFileName

- void ExtractToDirectory(string sourceFileName, string destinationDirectoryName): извлекает все файлы из zip-файла sourceFileName в каталог destinationDirectoryName

Оба метода имеют ряд дополнительных перегруженных версий. Рассмотрим их применение.

```Csharp
using System.IO.Compression;
 
string sourceFolder = "D://test/"; // исходная папка
string zipFile = "D://test.zip"; // сжатый файл
string targetFolder = "D://newtest"; // папка, куда распаковывается файл
 
ZipFile.CreateFromDirectory(sourceFolder, zipFile);
Console.WriteLine($"Папка {sourceFolder} архивирована в файл {zipFile}");
ZipFile.ExtractToDirectory(zipFile, targetFolder);
 
Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
```

В данном случае папка "D://test/" методом ZipFile.CreateFromDirectory архивируется в файл test.zip. Затем метод ZipFile.ExtractToDirectory() распаковывает данный файл в папку "D://newtest" (если такой папки нет, она создается).

