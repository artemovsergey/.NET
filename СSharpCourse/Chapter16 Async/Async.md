# Aсинхронное программирование

## Асинхронные методы, async и await

Нередко программа выполняет такие операции, которые могут занять продолжительное время, например, обращение к сетевым ресурсам, чтение-запись файлов, обращение к базе данных и т.д. Такие операции могут серьезно нагрузить приложение. Особенно это актуально в графических (десктопных или мобильных) приложениях, где продолжительные операции могут блокировать интерфейс пользователя и негативно повлиять на желание пользователя работать с программой, или в веб-приложениях, которые должны быть готовы обслуживать тысячи запросов в секунду. В синхронном приложении при выполнении продолжительных операций в основном потоке этот поток просто бы блокировался на время выполнения операции. И чтобы продолжительные операции не блокировали общую работу приложения, в C# можно задействовать асинхронность.

Асинхронность позволяет вынести отдельные задачи из основного потока в специальные асинхронные методы и при этом более экономно использовать потоки. Асинхронные методы выполняются в отдельных потоках. Однако при выполнении продолжительной операции поток асинхронного метода возвратится в пул потоков и будет использоваться для других задач. А когда продолжительная операция завершит свое выполнение, для асинхронного метода опять выделяется поток из пула потоков, и асинхронный метод продолжает свою работу.

Ключевыми для работы с асинхронными вызовами в C# являются два оператора: async и await, цель которых - упростить написание асинхронного кода. Они используются вместе для создания асинхронного метода.

Асинхронный метод обладает следующими признаками:

- В заголовке метода используется модификатор async

- Метод содержит одно или несколько выражений await

- В качестве возвращаемого типа используется один из следующих:

    - void

    - Task

    - Task<T>

    - ValueTask<T>

Асинхронный метод, как и обычный, может использовать любое количество параметров или не использовать их вообще. Однако асинхронный метод не может определять параметры с модификаторами out, ref и in.

Также стоит отметить, что слово async, которое указывается в определении метода, НЕ делает автоматически метод асинхронным. Оно лишь указывает, что данный метод может содержать одно или несколько выражений await.

Рассмотрим простейший пример определения и вызова асинхронного метода:

```Csharp
await PrintAsync();   // вызов асинхронного метода
Console.WriteLine("Некоторые действия в методе Main");
 
void Print()
{
    Thread.Sleep(3000);     // имитация продолжительной работы
    Console.WriteLine("Hello METANIT.COM");
}
 
// определение асинхронного метода
async Task PrintAsync()
{
    Console.WriteLine("Начало метода PrintAsync"); // выполняется синхронно
    await Task.Run(() => Print());                // выполняется асинхронно
    Console.WriteLine("Конец метода PrintAsync");
}   
```
Здесь прежде всего определен обычный метод Print, который просто выводит некоторую строку на консоль. Для имитации долгой работы в нем используется задержка на 3 секунд с помощью метода Thread.Sleep(). То есть условно Print - это некоторый метод, который выполняет некоторую продолжительную операцию. В реальном приложении это могло бы быть обращение к базе данных или чтение-запись файлов, но для упрощения понимания он просто выводит строку на консоль.

Также здесь определен асинхронный метод PrintAsync(). Асинхронным он является потому, что имеет в определении перед возвращаемым типом модификатор async, его возвращаемым типом является Task, и в теле метода определено выражение await.

Стоит отметить, что явным образом метод PrintAsync не возвращает никакого объекта Task, однако поскольку в теле метода применяется выражение await, то в качестве возвращаемого типа можно использовать тип Task.

Оператор await предваряет выполнение задачи, которая будет выполняться асинхронно. В данном случае подобная операция представляет выполнение метода Print:

```Csharp
await Task.Run(()=>Print());
```
По негласным правилам в названии асинхроннных методов принято использовать суффикс Async - PrintAsync(), хотя в принципе это необязательно делать.

И затем в программе (в данном случае в методе Main) вызывается этот асинхронный метод.

```Csharp
await PrintAsync();   // вызов асинхронного метода
```
Посмотрим, какой у программы будет консольный вывод:

```
Начало метода PrintAsync
Hello METANIT.COM
Конец метода PrintAsync
Некоторые действия в методе Main
```
Разберем поэтапно, что здесь происходит:

1. Запускается программа, а точнее метод Main, в котором вызывается асинхронный метод PrintAsync.

2. Метод PrintAsync начинает выполняться синхронно вплоть до выражения await.


  - Console.WriteLine("Начало метода PrintAsync"); // выполняется синхронно
3. Выражение await запускает асинхронную задачу Task.Run(()=>Print())

4. Пока выполняется асинхронная задача Task.Run(()=>Print()) (а она может выполняться довольно продожительное время), выполнение кода возвращается в вызывающий метод - то есть в метод Main.

5. Когда асинхронная задача завершила свое выполнение (в случае выше - вывела строку через три секунды), продолжает работу асинхронный метод PrintAsync, который вызвал асинхронную задачу.

6. После завершения метода PrintAsync продолжает работу метод Main.

# Асинхронный метод Main
Стоит учитывать, что оператор await можно применять только в методе, который имеет модификатор async. И если мы в методе Main используем оператор await, то метод Main тоже должен быть определен как асинхронный. То есть предыдущий пример фактически будет аналогичен следующему:

```Csharp
class Program
{
    async static Task Main(string[] args)
    {
        await PrintAsync();   // вызов асинхронного метода
        Console.WriteLine("Некоторые действия в методе Main");
 
 
        void Print()
        {
            Thread.Sleep(3000);     // имитация продолжительной работы
            Console.WriteLine("Hello METANIT.COM");
        }
 
        // определение асинхронного метода
        async Task PrintAsync()
        {
            Console.WriteLine("Начало метода PrintAsync"); // выполняется синхронно
            await Task.Run(() => Print());                // выполняется асинхронно
            Console.WriteLine("Конец метода PrintAsync");
        }
    }
}
```

# Задержка асинхронной операции и Task.Delay
В асинхронных методах для остановки метода на некоторое время можно применять метод Task.Delay(). В качестве параметра он принимает количество миллисекунд в виде значения int, либо объект TimeSpan, который задает время задержки:

```Csharp
await PrintAsync();   // вызов асинхронного метода
Console.WriteLine("Некоторые действия в методе Main");
 
// определение асинхронного метода
async Task PrintAsync()
{
    await Task.Delay(3000);     // имитация продолжительной работы
    // или так
    //await Task.Delay(TimeSpan.FromMilliseconds(3000));
    Console.WriteLine("Hello METANIT.COM");
}   
```
Причем метод Task.Delay сам по себе представляет асинхронную операцию, поэтому к нему применяется оператор await.

# Преимущества асинхронности

Выше приведенные примеры являются упрощением, и вряд ли их можно считать показательным. Рассмотрим другой пример:

```Csharp
PrintName("Tom");
PrintName("Bob");
PrintName("Sam");
 
void PrintName(string name)
{
    Thread.Sleep(3000);     // имитация продолжительной работы
    Console.WriteLine(name);
}
```
Данный код является синхронным и выполняет последовательно три вызова метода PrintName. Поскольку для имитации продолжительной работы в методе установлена задержка на три секунды, то общее выполнение программы займет не менее 9 секунд. Так как каждый последующий вызов PrintName будет ждать пока завершится предыдущий.

Изменим в программе синхронный метод PrintName на асинхронный:

```Csharp
await PrintNameAsync("Tom");
await PrintNameAsync("Bob");
await PrintNameAsync("Sam");
 
// определение асинхронного метода
async Task PrintNameAsync(string name)
{
    await Task.Delay(3000);     // имитация продолжительной работы
    Console.WriteLine(name);
}
```
Вместо метода PrintName теперь вызывается три раза PrintNameAsync. Для имитации продолжительной работы в методе установлена задержка на 3 секунды с помощью вызова Task.Delay(3000). И поскольку при вызовае каждого метода применяется оператор await, который останавливает выполнение до завершения асинхронного метода, то общее выполнение программы опять же займет не менее 9 секунд. Тем не менее теперь выполнение асинхронных операций не блокирует основной поток.

Теперь оптимизируем программу:

```Csharp
var tomTask = PrintNameAsync("Tom");
var bobTask = PrintNameAsync("Bob");
var samTask = PrintNameAsync("Sam");
 
await tomTask;
await bobTask;
await samTask;
// определение асинхронного метода
async Task PrintNameAsync(string name)
{
    await Task.Delay(3000);     // имитация продолжительной работы
    Console.WriteLine(name);
}
```
В данном случае задачи фактически запускаются при определении. А оператор await применяется лишь тогда, когда нам нужно дождаться завершения асинхронных операций - то есть в конце программы. И в этом случае общее выполнение программы займет не менее 3 секунд, но гораздо меньше 9 секунд.

# Определение асинхронного лямбда-выражения
Асинхронную операцию можно определить не только с помощью отдельного метода, но и с помощью лямбда-выражения:

```Csharp
// асинхронное лямбда-выражение
Func<string, Task> printer = async (message) =>
{
    await Task.Delay(1000);
    Console.WriteLine(message);
};
 
await printer("Hello World");
await printer("Hello METANIT.COM");
```

# Возвращение результата из асинхронного метода

В качестве возвращаемого типа в асинхронном методе должны использоваться типы void, Task, Task<T> или ValueTask<T>

## void
При использовании ключевого слова void асинхронный метод ничего не возвращает:

```Csharp
PrintAsync("Hello World");
PrintAsync("Hello METANIT.COM");
 
Console.WriteLine("Main End");
await Task.Delay(3000); // ждем завершения задач
 
// определение асинхронного метода
async void PrintAsync(string message)
{
    await Task.Delay(1000);     // имитация продолжительной работы
    Console.WriteLine(message);
}   
```

Однако асинхронных void-методов следует избегать и следует использовать только там, где эти подобные методы представляют единственный возможный способ определения асинхронного метода. Прежде всего, мы не можем применить к подобным методам оператор await. Также потому что исключения в таких методах сложно обрабатывать, так как они не могут быть перехвачены вне метода. Кроме того, подобные void-методы сложно тестировать.

Тем не менее есть ситуации, где без подобных методов не обойтись - например, при обработке событий:

```Csharp
Account account = new Account();
account.Added += PrintAsync;
 
account.Put(500);
 
await Task.Delay(2000); // ждем завершения
 
// определение асинхронного метода
async void PrintAsync(object? obj, string message)
{
    await Task.Delay(1000);     // имитация продолжительной работы
    Console.WriteLine(message);
}
 
class Account
{
    int sum = 0;
    public event EventHandler<string>? Added;
    public void Put(int sum)
    {
        this.sum += sum;
        Added?.Invoke(this, $"На счет поступило {sum} $");
    }
}
```
В данном случае событие Added в классе Account представляет делегат EventHandler, который имеет тип void. Соответственно под это событие можно определить только метод-обработчик с типом void.

# Task
Возвращение объекта типа Task:

```Csharp
await PrintAsync("Hello Metanit.com");
 
// определение асинхронного метода
async Task PrintAsync(string message)
{
    await Task.Delay(1000);     // имитация продолжительной работы
    Console.WriteLine(message);
}
```

Здесь формально метод PrintAsync не использует оператор return для возвращения результата. Однако если в асинхронном методе выполняется в выражении await асинхронная операция, то мы можем возвращать из метода объект Task.

Для ожидания завершения асинхронной задачи можно применить оператор await. Причем его необязательно использовать непосредственно при вызове задачи. Его можно применить лишь там, где нам нужно гарантировано получить результат задачи или удостовериться, что задача завершена.

```Csharp
var task = PrintAsync("Hello Metanit.com"); // задача начинает выполняться
Console.WriteLine("Main Works");
 
await task; // ожидаем завершения задачи
 
// определение асинхронного метода
async Task PrintAsync(string message)
{
    await Task.Delay(0);
    Console.WriteLine(message);
}
```

# ```Task<T>```
Метод может возвращать некоторое значение. Тогда возвращаемое значение оборачивается в объект Task, а возвращаемым типом является Task<T>:
 
```Csharp
int n1 = await SquareAsync(5);
int n2 = await SquareAsync(6);
Console.WriteLine($"n1={n1}  n2={n2}"); // n1=25  n2=36
 
async Task<int> SquareAsync(int n)
{
    await Task.Delay(0);
    return n * n;
}
```
В данном случае метод Square возвращает значение типа int - квадрат числа. Поэтому возвращаемым типом в данном случае является типа Task<int>.

Чтобы получить результат асинхронного метода применяем оператор await при вызове SquareAsync:

```Csharp
int n1 = await SquareAsync(5);
```
Подобным образом можно получать данные других типов:

```Csharp
Person person = await GetPersonAsync("Tom");
Console.WriteLine(person.Name); // Tom
// определение асинхронного метода
async Task<Person> GetPersonAsync(string name)
{
    await Task.Delay(0);
    return new Person(name);
}
record class Person(string Name);
```
Опять же получение непосредственных результатов асинхронной задачи можно отложить до того момента, когда они непосредственно нужны:

```Csharp
var square5 = SquareAsync(5);
var square6 = SquareAsync(6);
 
Console.WriteLine("Остальные действия в методе Main");
 
int n1 = await square5;
int n2 = await square6;
Console.WriteLine($"n1={n1}  n2={n2}"); // n1=25  n2=36
 
async Task<int> SquareAsync(int n)
{
    await Task.Delay(0);
    var result = n * n;
    Console.WriteLine($"Квадрат числа {n} равен {result}");
    return result;
}
```
Пример работы программы (ввывод не детерминирован):

```
Квадрат числа 5 равен 25
Квадрат числа 6 равен 36
Остальные действия в методе Main
n1=25  n2=36
```

# ```ValueTask<T>```
Использование типа ValueTask<T> во многом аналогично применению Task<T> за исключением некоторых различий в работе с памятью, поскольку ValueTask - структура, которая содержит большее количество полей. Поэтому применение ValueTask вместо Task приводит к копированию большего количества данных и соответственно создает некоторые дополнительные издержки.

Преимуществом ValueTask перед Task является то, что данный тип позволяет избежать дополнительных выделений памяти в хипе. Например, иногда требуется синхронно возвратить некоторое значение. Так, возьмем следующий пример:

```Csharp
var result = await AddAsync(4, 5);
Console.WriteLine(result);
 
Task<int> AddAsync(int a, int b)
{
    return Task.FromResult(a + b);
}
```
Здесь метод AddAsync синхронно возвращает некоторое значение - в данном случае сумму двух чисел. С помощью статического метода Task.FromResult можно синхронно возвратить некоторое значение. Однако использование типа Task приведет к выделению дополнительной задачи с сопутствующими выделениями памяти в хипе. ValueTask решает эту задачу:

```Csharp
var result = await AddAsync(4, 5);
Console.WriteLine(result);
 
ValueTask<int> AddAsync(int a, int b)
{
    return new ValueTask<int>(a + b);
}
```
В данном случае дополнительный объект Task не будет создаваться и соответственно дополнительная память не будет выделяться. Поэтому ValueTask обычно применяется, когда результат асинхронной операции уже имеется.

При необходимости также можно преобразовать ValueTask в объект Task с помощью метода AsTask():

```Csharp
var getMessage = GetMessageAsync();
string message = await getMessage.AsTask();
Console.WriteLine(message); // Hello
 
async ValueTask<string> GetMessageAsync()
{
    await Task.Delay(0);
    return "Hello";
}
```

# Последовательное и параллельное выполнение. Task.WhenAll и Task.WhenAny

Асинхронный метод может содержать множество выражений await. Когда система встречает в блоке кода оператор await, то выполнение в асинхронном методе останавливается, пока не завершится асинхронная задача. После завершения задачи управление переходит к следующему оператору await и так далее. Это позволяет вызывать асинхронные задачи последовательно в определенном порядке. Например:

```Csharp
await PrintAsync("Hello C#");
await PrintAsync("Hello World");
await PrintAsync("Hello METANIT.COM");
 
async Task PrintAsync(string message)
{
    await Task.Delay(2000);     // имитация продолжительной операции
    Console.WriteLine(message);
}
```
Консольный вывод данной программы:

```Csharp
Hello C#
Hello World
Hello METANIT.COM
```
То есть мы видим, что вызовы PrintAsync выполняются последовательно в том порядке, в котором они определены в коде. Каждая задача выполняется как минимум 2 секунды, соответственно общее время выполнения трех задач будет как минимум 6 секунд. И в данном случае вывод строго детерминирован.

Нередко такая последовательность бывает необходима, если одна задача зависит от результатов другой.

Однако это не всегда необходимо. В подобном случае мы можем сразу запустить все задачи параллельно и применить оператор await там, где необходимо гарантировать завершение выполнения задачи, например, в самом конце программы.

```Csharp
// определяем и запускаем задачи
var task1 = PrintAsync("Hello C#");
var task2 = PrintAsync("Hello World");
var task3 = PrintAsync("Hello METANIT.COM");
 
// ожидаем завершения задач
await task1;
await task2;
await task3;
 
async Task PrintAsync(string message)
{
    await Task.Delay(2000);     // имитация продолжительной операции
    Console.WriteLine(message);
}
```
В этом случае все задачи запускаются и выполняются параллельно, соответственно общее время выполнения будет меньше 6 секунд, а консольный вывод программы недетерминирован. Например, он может быть следующим:

```Csharp
Hello METANIT.COM
Hello C#
Hello World
```
Однако .NET позволяет упростить отслеживание выполнения набора задач с помощью метода Task.WhenAll. Этот метод принимает набор асинхронных задач и ожидает завершения всех этих задач. Этот метод является аналогом статического метода Task.WaitAll(), однако предназначен непосредственно для асинхронных методов и позволяет применять оператор await:

```Csharp
// определяем и запускаем задачи
var task1 = PrintAsync("Hello C#");
var task2 = PrintAsync("Hello World");
var task3 = PrintAsync("Hello METANIT.COM");
 
// ожидаем завершения всех задач
await Task.WhenAll(task1, task2, task3);
 
async Task PrintAsync(string message)
{
    await Task.Delay(2000);     // имитация продолжительной операции
    Console.WriteLine(message);
}
```
Вначале запускаются три задачи. Затем Task.WhenAll создает новую задачу, которая будет автоматически выполнена после выполнения всех предоставленных задач, то есть задач task1, task2, task3. А с помощью оператора await ожидаем ее завершения.

Если нам надо дождаться, когда будет выполнена хотя бы одна задача из некоторого набора задач, то можно применять метод Task.WhenAny(). Это аналог метода Task.WaitAny() - он завершает выполнение, когда завершается хотя бы одна задача. Но для ожидания выполнения к Task.WhenAny() применяется оператор await:

```Csharp
// определяем и запускаем задачи
var task1 = PrintAsync("Hello C#");
var task2 = PrintAsync("Hello World");
var task3 = PrintAsync("Hello METANIT.COM");
 
// ожидаем завершения хотя бы одной задачи
await Task.WhenAny(task1, task2, task3);
 
async Task PrintAsync(string message)
{   
    await Task.Delay(new Random().Next(1000, 2000));     // имитация продолжительной операции
    Console.WriteLine(message);
}

```

# Получение результата
Задачи, передаваемые в Task.WhenAll и Task.WhenAny, могут возвращать некоторое значение. В этом случае из методов Task.WhenAll и Task.WhenAny можно получить массив, который будет содержать результаты задач:

```Csharp
// определяем и запускаем задачи
var task1 = SquareAsync(4);
var task2 = SquareAsync(5);
var task3 = SquareAsync(6);
 
// ожидаем завершения всех задач
int[] results = await Task.WhenAll(task1, task2, task3);
// получаем результаты:
foreach (int result in results)
    Console.WriteLine(result);
 
async Task<int> SquareAsync(int n)
{
    await Task.Delay(1000);
    return n * n;
}
```
В данном случае метод Square возвращает число int - квадрат передаваемого в метод числа. И переменная results будет содержать результат вызова Task.WhenAll - по сути результаты всех трех запущенных задач. Поскольку все передаваемые в Task.WhenAll задачи возвращают int, то соответственно результат Task.WhenAll будет представлять массив значений int.

Также после завершения задачи ее результат можно получить стандартным образом через свойство Result:

```Csharp
// определяем и запускаем задачи
var task1 = SquareAsync(4);
var task2 = SquareAsync(5);
var task3 = SquareAsync(6);
 
await Task.WhenAll(task1, task2, task3);
// получаем результат задачи task2
Console.WriteLine($"task2 result: {task2.Result}"); // task2 result: 25
 
async Task<int> SquareAsync(int n)
{
    await Task.Delay(1000);
    return n * n;
}
```

# Обработка ошибок в асинхронных методах

Обработка ошибок в асинхронных методах, использующих ключевые слова async и await, имеет свои особенности.

Для обработки ошибок выражение await помещается в блок try:

```Csharp
try
{
    await PrintAsync("Hello METANIT.COM");
    await PrintAsync("Hi");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
 
async Task PrintAsync(string message)
{
    // если длина строки меньше 3 символов, генерируем исключение
    if (message.Length < 3)
        throw new ArgumentException($"Invalid string length: {message.Length}");
    await Task.Delay(100);     // имитация продолжительной операции
    Console.WriteLine(message);
}
```

В данном случае асинхронный метод PrintAsync генерирует исключение ArgumentException, если методу передается строка с длиной меньше 3 символов.

Для обработки исключения в методе Main выражение await помещено в блок try. В итоге при выполнении вызова await PrintAsync("Hi") будет сгенерировано исключение, что привет к генерации исключения. Однако программа не остановит аварийно свою работу, а обработает исключение и продолжит дальнейшие вычисления.

Консольный вывод программы:

```
Hello METANIT.COM
Invalid string length: 2
```
Следует учитывать, что если асинхронный метод имеет тип void, то в этом случае исключение во вне не передается, соответственно мы не сможем обработать исключение при вызове метода:

```Csharp
try
{
    PrintAsync("Hello METANIT.COM");
    PrintAsync("Hi");       // здесь программа сгенерирует исключение и аварийно остановится
    await Task.Delay(1000); // ждем завершения задач
}
catch (Exception ex)    // исключение НЕ будет обработано
{
    Console.WriteLine(ex.Message);
}
 
async void PrintAsync(string message)
{
    // если длина строки меньше 3 символов, генерируем исключение
    if (message.Length < 3)
        throw new ArgumentException($"Invalid string length: {message.Length}");
    await Task.Delay(100);     // имитация продолжительной операции
    Console.WriteLine(message);
}
```
В данном случае, не смотря на то, что асинхронные методы вызываются в блоке try, исключение не будет перехвачено и обработано. В этом один из минусов применения асинхронных void-методов. Правда, в этом случае мы можем определить обработку исключения в самом асинхронном методе:

```Csharp
PrintAsync("Hello METANIT.COM");
PrintAsync("Hi");
await Task.Delay(1000); // ждем завершения задач
 
async void PrintAsync(string message)
{
    try
    {
        // если длина строки меньше 3 символов, генерируем исключение
        if (message.Length < 3)
            throw new ArgumentException($"Invalid string length: {message.Length}");
        await Task.Delay(100);     // имитация продолжительной операции
        Console.WriteLine(message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
 
}
```
# Исследование исключения
При возникновении ошибки у объекта Task, представляющего асинхронную задачу, в которой произошла ошибка, свойство IsFaulted имеет значение true. Кроме того, свойство Exception объекта Task содержит всю информацию об ошибке. Проинспектируем данное свойство:

```Csharp
var task = PrintAsync("Hi");
try
{
    await task;
}
catch
{
    Console.WriteLine(task.Exception?.InnerException?.Message); // Invalid string length: 2
    Console.WriteLine($"IsFaulted: {task.IsFaulted}");  // IsFaulted: True
    Console.WriteLine($"Status: {task.Status}");        // Status: Faulted
}
 
async Task PrintAsync(string message)
{
    // если длина строки меньше 3 символов, генерируем исключение
    if (message.Length < 3)
        throw new ArgumentException($"Invalid string length: {message.Length}");
    await Task.Delay(1000);     // имитация продолжительной операции
    Console.WriteLine(message);
}
```
И если мы передадим в метод строку с длиной меньше 3 символов, то task.IsFaulted будет равно true.

# Обработка нескольких исключений. WhenAll
Если мы ожидаем выполнения сразу нескольких задач, например, с помощью Task.WhenAll, то мы можем получить сразу несколько исключений одномоментно для каждой выполняемой задачи. В этом случае мы можем получить все исключения из свойства Exception.InnerExceptions:

```Csharp
// определяем и запускаем задачи
var task1 = PrintAsync("H");
var task2 = PrintAsync("Hi");
var allTasks = Task.WhenAll(task1, task2);
try
{
    await allTasks;
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
    Console.WriteLine($"IsFaulted: {allTasks.IsFaulted}");
    if(allTasks.Exception is not null)
    {
        foreach (var exception in allTasks.Exception.InnerExceptions)
        {
            Console.WriteLine($"InnerException: {exception.Message}");
        }
    }
}
 
async Task PrintAsync(string message)
{
    // если длина строки меньше 3 символов, генерируем исключение
    if (message.Length < 3)
        throw new ArgumentException($"Invalid string: {message}");
    await Task.Delay(1000);     // имитация продолжительной операции
    Console.WriteLine(message);
}
```
Здесь в два вызова метода PrintAsync передаются заведомо некорректные значения. Таким образом, при обоих вызовах будет сгенерирована ошибка.

Хотя блок catch через переменную Exception ex будет получать одно перехваченное исключение, но с помощью коллекции Exception.InnerExceptions мы сможем получить информацию обо всех возникших исключениях.

В итоге при выполнении этого метода мы получим следующий консольный вывод:

```
Exception: Invalid string: H
IsFaulted: True
InnerException: Invalid string: H
InnerException: Invalid string: Hi
```

# Асинхронные стримы

Начиная с версии C# 8.0 в C# были добавлены асинхронные стримы, которые упрощают работу с потоками данных в асинхронном режиме. Хотя асинхронность в C# существует уже довольно давно, тем не менее асинхронные методы до сих пор позволяли получать один объект, когда асинхронная операция была готова предоставить результат. Для возвращения нескольких значений в C# могут применяться итераторы, но они имеют синхронную природу, блокируют вызывающий поток и не могут использоваться в асинхронном контексте. Асинхронные стримы обходят эту проблему, позволяя получать множество значений и возвращать их по мере готовности в асинхронном режиме.

По сути асинхронный стрим представляет метод, который обладает тремя характеристиками:

- метод имеет модификатор async

- метод возвращает объект IAsyncEnumerable<T>. Интерфейс IAsyncEnumerable определяет метод GetAsyncEnumerator, который возвращает IAsyncEnumerator:

```Csharp
public interface IAsyncEnumerable<out T>
{
    IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
}
 
public interface IAsyncEnumerator<out T> : IAsyncDisposable
{
    T Current { get; }
    ValueTask<bool> MoveNextAsync();
}
public interface IAsyncDisposable
{
    ValueTask DisposeAsync();
}
```
- метод содержит выражения yield return для последовательного получения элементов из асинхронного стрима

Фактически асинхронный стрим объединяет асинхронность и итераторы. Рассмотрим простейший пример:

```Csharp
await foreach (var number in GetNumbersAsync())
{
    Console.WriteLine(number);
}
 
async IAsyncEnumerable<int> GetNumbersAsync()
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(100);
        yield return i;
    }
}
```
Итак, метод GetNumbersAsync() фактически и представляет асинхронный стрим. Этот метод является асинхронным. Его возвращаемый тип - IAsyncEnumerable<int>. А его суть сводится к тому, что он возвращает с помощью yield return каждые 100 миллисекунд некоторое число. То есть фактически метод должен вернуть 10 чисел от 0 до 9 с промежутком в 100 миллисекунд.

Для получения данных из стрима в методе Main используется цикл foreach:

```Csharp
await foreach (var number in GetNumbersAsync())
```

Важно отметить, что он предваряется оператором await. В итоге, каждый раз когда асинхронный стрим будет возвращать очередное число, цикл будет его получать и выводить на консоль.

Где можно применять асинхронные стримы? Асинхронные стримы могут применяться для получения данных из какого-нибудь внешнего хранилища. Например, пусть имеется следующий класс некоторого хранилища:

```Csharp
class Repository
{
    string[] data = { "Tom", "Sam", "Kate", "Alice", "Bob" };
    public async IAsyncEnumerable<string> GetDataAsync()
    {
        for (int i = 0; i < data.Length; i++)
        {
            Console.WriteLine($"Получаем {i + 1} элемент");
            await Task.Delay(500);
            yield return data[i];
        }
    }
}
```
Для упрощения примера данные здесь представлены в виде простого внутреннего массива строк. Для имитации задержки в получении применяется метод Task.Delay.

Получим эти данные в программе:

```Csharp
Repository repo = new Repository();
IAsyncEnumerable<string> data = repo.GetDataAsync();
await foreach (var name in data)
{
    Console.WriteLine(name);
}
 
class Repository
{
    string[] data = { "Tom", "Sam", "Kate", "Alice", "Bob" };
    public async IAsyncEnumerable<string> GetDataAsync()
    {
        for (int i = 0; i < data.Length; i++)
        {
            Console.WriteLine($"Получаем {i + 1} элемент");
            await Task.Delay(500);
            yield return data[i];
        }
    }
}
```
