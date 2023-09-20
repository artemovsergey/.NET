# Pattern matching

## Паттерн типов

Pattern matching фактически выполняет сопоставление некоторого значения с некоторым шаблоном. И если сопоставление прошло успешно, то выполняются определенные действия. Язык C# позволяет выполнять различные типы сопоставлений.

Паттерн типов или type pattern позволяет проверить некоторое значение на соответствие некоторому типу:

```Csharp
значение is тип переменная_типа
```

Например, у нас есть следующие классы:

```Csharp
class Employee
{
    public virtual void Work() => Console.WriteLine("Employee works");
}
 
class Manager : Employee
{
    public override void Work() => Console.WriteLine("Manager works");
    public bool IsOnVacation { get; set; }
}
```

Класс Employee представляет работника, а класс Manager - менеджера. Оба класса реализуют метод Work. Кроме того, класс Manager определяет свойство IsOnVacation.

С помощью паттерна типов проверим, представляет ли объект Employee класс Manager:

```Csharp
Employee tom = new Manager();
UseEmployee(tom);   // Manager works
 
void UseEmployee(Employee emp)
{
    if (emp is Manager manager && manager.IsOnVacation == false)
    {
        manager.Work();
    }
    else
    {
        Console.WriteLine("Преобразование не допустимо");
    }
}
```

Здесь в методе UseEmployee значение emp сопоставляется с типом Manager. То есть в данном случае в качестве шаблона выступает тип Manager. Если сопоставление прошло успешно (то есть значение emp представляет тип Manager), в переменной manager оказывается объект emp. И далее мы можем вызвать у него методы и свойства.

Также мы можем использовать constant pattern - сопоставление с некоторой константой:

```Csharp
var message = "hello";
 
// проверяем, соответствует ли значение message строке "hello"
if (message is "hello")
{
   Console.WriteLine("hello");
}
```

Подобным образом, например, можно проверить значение на null:

```Csharp
Employee? bob = new Employee();
Employee? tom = null;
 
UseEmployee(bob);
UseEmployee(tom);
 
void UseEmployee(Employee? emp)
{
    if (emp is not null)
        emp.Work();
}
```

Кроме конструкции if сопоставление паттернов может применяться в конструкции switch:

```Csharp
Employee bob = new Employee();
Employee tom = new Manager();
UseEmployee(tom);   // Manager works
UseEmployee(bob);   // Object is not manager
 
void UseEmployee(Employee? emp)
{
    switch (emp)
    {
        case Manager manager:
            manager.Work();
            break;
        case null:
            Console.WriteLine("Object is null");
            break;
        default:
            Console.WriteLine("Object is not manager");
            break;
    }
}
```

С помощью выражения when можно вводить дополнительные условия в конструкцию case:

```Csharp
Employee bob = new Manager() { IsOnVacation = true };
Employee tom = new Manager() { IsOnVacation = false };
UseEmployee(tom);   // Manager works
UseEmployee(bob);   // Employee does not work
 
void UseEmployee(Employee? emp)
{
    switch (emp)
    {
        case Manager manager when !manager.IsOnVacation:
            manager.Work();
            break;
        case null:
            Console.WriteLine("Employee is null");
            break;
        default:
            Console.WriteLine("Employee does not work");
            break;
    }
}
```
В этом случае опять же преобразуем объект emp в объект типа Manager и в случае удачного преобразования смотрим на значение свойства IsOnVacation: если оно равно false, то выполняется данный блок case.

## Паттерн свойств

Паттерн свойств позволяет сравнивать со значениями определенных свойств объекта. Например, пусть у нас будет следующий класс:

```Csharp
class Person
{
    public string Name { get; set; } = "";        // имя пользователя
    public string Status { get; set; } = "";     // статус пользователя
    public string Language { get; set; } = "";   // язык пользователя
}
```

Например, в зависимости от языка пользователя выведем ему определенное сообщение, применив паттерн свойств:

```Csharp
Person tom = new Person { Language = "english", Status = "user", Name = "Tom" };
Person pierre = new Person { Language = "french", Status = "user", Name = "Pierre" };
 
SayHello(tom);      // Hello
SayHello(pierre);   // Salut
 
void SayHello(Person person)
{
    if(person is Person { Language: "french" })
    {
        Console.WriteLine("Salut");
    }
    else
    {
        Console.WriteLine("Hello");
    }
}
```
Здесь метод SayHello в качестве параметра принимает объект Person и сопоставляет его с некоторым паттерном. В качестве паттерна выступает выражение Person { Language: "french" }. То есть параметр person должен представлять объект Person, у которого значение свойства Language равно "french".

При этом можно задействовать набор свойств. Например, добавим проверку по свойству Status:

```Csharp
Person tom = new Person { Language = "english", Status = "user", Name = "Tom" };
Person pierre = new Person { Language = "french", Status = "user", Name = "Pierre" };
Person admin = new Person { Language = "english", Status = "admin", Name = "Admin" };
 
SayHello(admin);    // Hello, admin
SayHello(tom);      // Hello
SayHello(pierre);   // Salut
 
void SayHello(Person person)
{
    if(person is Person { Language: "english", Status: "admin" })
    {
        Console.WriteLine("Hello, admin");
    }
    else if (person is Person { Language: "french"})
    {
        Console.WriteLine("Salut");
    }
    else
    {
        Console.WriteLine("Hello");
    }
}
```

Теперь выражение if проверяет, соответствует ли параметр person объекту Person, у которого свойства Language и Status имеют определенные значения.

Подобным образом можно применять паттерн свойств в конструкции switch:

```Csharp
string GetMessage(Person? p) => p switch
{
    { Language: "english" } => "Hello!",
    { Language: "german", Status: "admin" } => "Hallo, admin!",
    { Language: "french" } => "Salut!",
    { } => "undefined",
    null => "null"       // если Person p = null
};
```

Паттерны свойств предполагают использование фигурных скобок, внутри которых указываются свойства и через двоеточие их значение {свойство: значение}. И со значением свойства в фигурных скобках сравнивается свойство передаваемого объекта. При этом в фигурных скобках мы можем указать несколько свойств и их значений { Language: "german", Status: "admin" } - тогда свойства передаваемого объекта должны соответствовать всем этим значениям.

Можно оставить пустые фигурные скобки, как в последнем случае { } => "undefined!" - передаваемый объект будет соответствовать пустым фигурным скобкам, если он не соответствует всем предыдущим значениям, или например, если его свойства не указаны или имеют значение null.

То есть в данном случае, если у объекта Person p выполняется равенство Language = "english", будет возвращаться строка "Hello!".

Если у объекта Person p одновременно выполняются два равенства Language = "german" и Role="admin", будет возвращаться строка "Hallo, admin!".

Если у объекта Person p выполняется равенство Language = "french", будет возвращаться строка "Salut!".

Если объект Person будет сопоставляться с пустыми фигурными скобками {}, и будет возвращаться строка "undefined".

Последняя проверка проверяет значение на null.

Применение:

```Csharp
Person pierre = new Person { Language = "french", Status = "user", Name = "Pierre" };
string message = GetMessage(pierre);
Console.WriteLine(message);     // Salut!
 
Person tomas = new Person { Language = "german", Status = "admin", Name = "Tomas" };
Console.WriteLine(GetMessage(tomas));     // Hallo, admin!
 
Person pablo = new Person { Language = "spanish", Status = "user", Name = "Pablo" };
Console.WriteLine(GetMessage(pablo));     // undefined
 
Console.WriteLine(GetMessage(null));     // null
```

Кроме того, мы можем определять в паттерных свойств переменные, передавать этим переменным значения объекта и использовать при возвращении значения:

```Csharp
string GetMessage(Person? p) => p switch
{
    { Language: "german", Status: "admin" } => "Hallo, admin!",
    { Language: "french", Name: var name } => $"Salut, {name}!",
    { Language: var lang} => $"Unknown language: {lang}",
    null => "null"
};
```

Так, подвыражение Name: var name говорит, что надо передать в переменную name значение свойства Name. Затем ее можно применить при генерации выходного значения: => $"Salut, {name}!"

Применение:

```Csharp
Person pierre = new Person { Language = "french", Status = "user", Name = "Pierre" };
string message = GetMessage(pierre);
Console.WriteLine(message);             // Salut, Pierre!
 
Person tomas = new Person { Language = "german", Status = "admin", Name = "Tomas" };
Console.WriteLine(GetMessage(tomas));     // Hallo, admin!
 
Person pablo = new Person { Language = "spanish", Status = "user", Name = "Pablo" };
Console.WriteLine(GetMessage(pablo));     // Unknown language: spanish
 
Person? bob = null;
Console.WriteLine(GetMessage(bob));         // null
```

Стоит отметить, что начиная с версии C# 10 было упрощено сопоставление со свойствами вложенных объектов. Допустим, у нас есть следующие классы:

```Csharp
class Employee
{
    public string Name { get;}
    public Company Company { get; set; }
    public Employee(string name, Company company)
    {
        Name = name;
        Company = company;
    }
 
}
class Company
{
    public string Title { get;}
    public Company(string title) => Title = title;
}
```

Класс Company определяет свойство Title, которое хранит название компании. Класс Employee определяет сотрудника компании и в свойстве Company хранит компанию. Применим паттерн свойств на основе свойств вложенного объекта Company:

```Csharp
var microsoft = new Company("Microsoft");
var google = new Company("Google");
var tom = new Employee("Tom", microsoft);
var bob = new Employee("Bob", google);
 
PrintCompany(tom);    // 
PrintCompany(bob);    // 
 
void PrintCompany(Employee employee)
{
    if (employee is Employee { Company:{Title: "Microsoft" } })
    {
        Console.WriteLine($"{employee.Name} works in Microsoft");
    }
    else
    {
        Console.WriteLine($"{employee.Name} works someware");
    }
}
```

В методе PrintCompany объект employee сопоставляется с паттерном Employee { Company:{Title: "Microsoft" } }. То есть сотрудник компании должен представлять объект Employee, у которого название компании равно "Microsoft"

Однако мы также можем сократить данный паттерн следующим образом:

```Csharp
void PrintCompany(Employee employee)
{
    if (employee is Employee { Company.Title: "Microsoft" })
    {
        Console.WriteLine($"{employee.Name} works in Microsoft");
    }
    else
    {
        Console.WriteLine($"{employee.Name} works someware");
    }
}
```

## Паттерны кортежей

Паттерны кортежей позволяют сравнивать значения кортежей. Например, передадим в конструкцию switch кортеж с названием языка и времени дня и в зависимости от переданных данных возвратим определенное сообщение:

```Csharp
string GetWelcome(string lang, string daytime) => (lang, daytime) switch
{
    ("english", "morning") => "Good morning",
    ("english", "evening") => "Good evening",
    ("german", "morning") => "Guten Morgen",
    ("german", "evening") => "Guten Abend",
    _ => "Здрасьть"
};
```

Здесь в метод передаются два значения, из которых создается кортеж (можно и сразу передать в метод кортеж). Далее в конструкции switch с помощью круглых скобок определяются значения, которым должны соответствовать элементы кортежа. Например, выражение ("english", "morning") => "Good morning" будет выполняться, если одновременно lang="english" и datetime="morning".

Применение:

```Csharp
string message = GetWelcome("english", "evening");
Console.WriteLine(message);  // Good evening
 
message = GetWelcome("french", "morning");
Console.WriteLine(message);  // Здрасьть
```

Нам не обязательно сравнивать все значения кортежа, мы можем использовать только некоторые элементы кортежа. В случае, если мы не хотим использовать элемент кортежа, то вместо него ставим прочерк:

```Csharp
string GetWelcome(string lang, string daytime, string status) => (lang, daytime, status) switch
{
    ("english", "morning", _) => "Good morning",
    ("english", "evening", _) => "Good evening",
    ("german", "morning", _) => "Guten Morgen",
    ("german", "evening", _) => "Guten Abend",
    (_, _, "admin") => "Hello, Admin",
    _ => "Здрасьть"
};
```

Теперь кортеж состоит из трех элементов. Но первые четыре выражения не используют последний элемент кортежа, допустим, он не важен, поэтому вместо него ставится прочерк ("english", "morning", _).

А в предпоследнем примере, наоборот, не важны первые два элемента, а важен третий элемент: (_, _, "admin") =>.

Но в любом случае нам надо указать конкретные значения или прочерки для всех элементов кортежа.

```Csharp
string message = GetWelcome("english", "evening", "user");
Console.WriteLine(message);  // Good evening
 
message = GetWelcome("french", "morning", "admin");
Console.WriteLine(message);  // Hello, Admin
```

## Позиционный паттерн

Позиционный паттерн применяется к типу, у которого определен метод деконструктора. Например, определим следующий класс:

```Csharp
class MessageDetails
{
    public string Language { get; set; } = "";    // язык пользователя
    public string DateTime { get; set; } = "";    // время суток
    public string Status { get; set; } = "";     // статус пользователя
 
    public void Deconstruct(out string lang, out string datetime, out string status)
    {
        lang = Language;
        datetime = DateTime;
        status = Status;
    }
}
```

Теперь используем позиционный паттерн и в зависимости от значений объекта MessageDetails возвратим определенное сообщение:

```Csharp
string GetWelcome(MessageDetails details) => details switch
{
    ("english", "morning", _) => "Good morning",
    ("english", "evening", _) => "Good evening",
    ("german", "morning", _) => "Guten Morgen",
    ("german", "evening", _) => "Guten Abend",
    (_, _, "admin") => "Hello, Admin",
    _ => "Здрасьть"
};
```

Фактически этот паттерн похож на пример с кортежами выше, только теперь вместо кортежа в конструкцию switch передается объект MessageDetails. Через метод деконструктора мы можем получить набор выходных параметров в виде кортежа и опять же по позиции сравнивать их с некоторыми значениями в конструкции switch.

Применение:

```Csharp
MessageDetails details1 = new MessageDetails { Language = "english", DateTime = "evening", Status = "user" };
string message = GetWelcome(details1);
Console.WriteLine(message);  // Good evening
 
MessageDetails details2 = new MessageDetails { Language = "french", DateTime = "morning", Status = "admin" };
message = GetWelcome(details2);
Console.WriteLine(message);  // Hello, Admin
```

Также мы можем взять значения объекта MessageDetails и использовать их при создании результата метода:

```Csharp
string GetWelcome(MessageDetails details) => details switch
{
    ("english", "morning", _) => "Good morning",
    ("english", "evening", _) => "Good evening",
    ("german", "morning", _) => "Guten Morgen",
    ("german", "evening", _) => "Guten Abend",
    (_, _, "admin") => "Hello, Admin",
    (var lang, var datetime, var status) => $"{lang} not found, {datetime} unknown, {status} undefined",
    _ => "Здрасьть"
};
```

В предпоследней инструкции в конструкции switch получаем по позиции значения из MessageDetails в переменные lang, datetime и status и используем их для создания сообщения:

```Csharp
MessageDetails details1 = new MessageDetails 
{ 
    Language = "chinese", 
    DateTime = "night", 
    Status = "moderator"
};
string message = GetWelcome(details1);
Console.WriteLine(message);  // chinese not found, night unknown, moderator undefined
```

## Реляционный и логический паттерны

В C# 9.0 в язык были добавлены дополнительные паттерны - реляционный (relational pattern) и логический (logical pattern) паттерны.

Реляционный паттерн позволяет сравнить передаваемое в конструкцию значение с некоторыми значениями с помощью операций сравнения. Например, в зависимости от суммы вклада проценты по этому вкладу могут отличаться. Рассчитаем сумму процентов в зависимости от суммы вклада с использованием реляционного паттерна:

```Csharp
decimal Calculate(decimal sum)
{
    return sum switch {
        <= 0 => 0,              // если sum меньше или равно 0, возвращаем 0
        < 50000 => sum * 0.05m, // если sum меньше 50000, возвращаем sum * 0.05m
        < 100000 => sum * 0.1m, // если sum меньше 100000, возвращаем sum * 0.1m
        _ => sum * 0.2m        // в остальных случаях возвращаем sum * 0.2m
    };
}
```

Применение:

```
Console.WriteLine(Calculate(-200));     // 0
Console.WriteLine(Calculate(0));        // 0 
Console.WriteLine(Calculate(10000));    // 500
Console.WriteLine(Calculate(60000));    // 6000
Console.WriteLine(Calculate(200000));   // 40000
```

Логический паттерн позволяет использовать логические операторы and (логическое умножение или операция логического И) и or (логическое сложение или операция логического ИЛИ) для объединения операций сравнения. Например, передадим в метод возраст и возвратим соответствующее сообщение:

```Csharp
string CheckAge(int age)
{
    return age switch
    {
        <1 or >110 => "Недействительный возраст",   // если age больше 110 и меньше 1
        >=1 and <18 => "Доступ запрещен",           // если age равно или больше 1 и меньше 18
        _ => "Доступ разрешен"                      // в остальных случаях
    };
}
```

Применение:

```Csharp
Console.WriteLine(CheckAge(200));     // Недействительный возраст
Console.WriteLine(CheckAge(0));      // Недействительный возраст 
Console.WriteLine(CheckAge(17));    // Доступ запрещен
Console.WriteLine(CheckAge(18));   // Доступ разрешен
```

Еще один логический оператор - not используется для отрицания и возращает true, если его аргумент равен false:

```Csharp
string CheckAge(int age) => age switch
{
    not 33 => "Обычный возраст",   // если age НЕ равен 33
    _ => "Вам 33 года"             // в остальных случаях, то есть если age = 33
};
```

## Паттерны списков

Паттерны списков (list pattern) позволяют сопоставлять выражения со списками и массивами. Данный паттерн пока доступен начиная с версии C# 11.

Полное совпадение с массивом/списком:

```Csharp
Console.WriteLine(GetNumber(new[] { 1, 2, 3, 4, 5 }));  // 1
Console.WriteLine(GetNumber(new[] { 1, 2}));            // 3
Console.WriteLine(GetNumber(new int[] {}));             // 4
Console.WriteLine(GetNumber(new[] { 1, 2, 5 }));        // 5
 
int GetNumber(int[] values)=> values switch
{
        [1, 2, 3, 4, 5] => 1,
        [1, 2, 3] => 2,
        [1, 2] => 3,
        [] => 4,
        _ => 5
};
```

Аналогично вместо массивов можно применять списки:

```Csharp
List<int> numbers = new List<int>{1, 2, 3};
 
Console.WriteLine(GetNumber(numbers));  // 2
 
int GetNumber(List<int> values) => values switch
{
    [1, 2, 3, 4, 5] => 1,
    [1, 2, 3] => 2,
    [1, 2] => 3,
    [] => 4,
    _ => 5
};
```

Аналогичным образом паттерны списков можно использовать в конструкции if:

```Csharp
int[] numbers = { 1, 2, 3, 4, 5 };
if(numbers is [1, 2, 3, 4, 5])
{
    Console.WriteLine("[1, 2, 3, 4, 5]");
}
```

## Подстановка _

С помощью паттерна _ можно обозначить одиночный элемент, который имеет любое значение. Например, паттерн [2, _, 5] соответствует любому массиву из трех элементов, в котором между 2 и 5 располагается произвольное значение. А массив [_, _] соответствует любому массиву из двух произвольных элементов

Несколько примеров:

```Csharp
Console.WriteLine(GetNumber(new[] { 2, 3, 5}));       // 1
Console.WriteLine(GetNumber(new[] { 2, 4, 6 }));      // 2
Console.WriteLine(GetNumber(new[] { 1, 2, 5 }));      // 3
Console.WriteLine(GetNumber(new[] { 1, 2, 3}));      // 4
Console.WriteLine(GetNumber(new int[] { }));         // 5
 
int GetNumber(int[] values) => values switch
{
    [2, _, 5] => 1,
    [2, _, _] => 2,
    [_, _, 5] => 3,
    [_, _, _] => 4,
    _ => 5
};
```

### slice-паттерн

Для передачи произвольного количества элементов массива/списка применяется slice-паттерн ... Например, паттерн [1, 2, .., 5] соответствует массиву, в который начинается на 1, за которым идет 2. А последний элемент в массиве - 5. При этом между 2 и 5 может располагаться произвольное количество произвольных целых чисел. То есть паттерн [1, 2, .., 5] будет соответствовать таким массивам как

```Csharp
int[] arr1 = {  1, 2, 3, 4, 5 };
int[] arr2 = { 1, 2, 5 };
int[] arr3 = { 1, 2, 66, 77, 88, 5 };
```

С помощью паттерна .. можно задавать произвольное количество элементов как в начале, так и в конце массива/списка. Например, паттерн [2,..] представляет массив, который начинается на 2. А паттерн [.., 5] представляет массив, который заканчивается элементом 5. Паттерн [..] представляет массив, который содержит произвольное количество элементов. Например:

```Csharp
Console.WriteLine(GetNumber(new[] { 2, 5 }));           // 1
Console.WriteLine(GetNumber(new[] { 2, 3, 4, 5 }));     // 1
 
Console.WriteLine(GetNumber(new[] { 2}));               // 2
Console.WriteLine(GetNumber(new[] { 2, 3, 4}));         // 2
 
Console.WriteLine(GetNumber(new[] {3, 4, 5}));          // 3
Console.WriteLine(GetNumber(new[] { 5 }));              // 3
 
Console.WriteLine(GetNumber(new int[] {  }));           // 4
Console.WriteLine(GetNumber(new[] { 1 }));              // 4
Console.WriteLine(GetNumber(new[] { 1, 2, 3 }));        // 4
 
int GetNumber(int[] values)=> values switch
{
    [2, .., 5] => 1,    // если первый элемент - 2, а последний - 5
    [2,..] => 2,        // если первый элемент - 2
    [.., 5] => 3,       // если последний элемент - 5
    [..] => 4          // произвольное количество элементов
};
```

slice-паттерн можно сочетать с символов подстановки _, например:

```Csharp
int GetNumber(int[] values) => values switch
{
    [_, .., _] => 1,
    [..] => 2
};
```

В данном случае паттерн [_, .., _] предполагает массив, который состоит как минимум из двух произвольных элементов, и между первым и последним элементром может находиться произвольное количество других элементов:

```Csharp
Console.WriteLine(GetNumber(new[] { 1, 2, 3, 4}));   // 1
Console.WriteLine(GetNumber(new[] { 1, 2, 3}));     // 1
Console.WriteLine(GetNumber(new[] { 1, 2}));        // 1
Console.WriteLine(GetNumber(new[] { 1 }));          // 2
Console.WriteLine(GetNumber(new int[] { }));       // 2
 
int GetNumber(int[] values) => values switch
{
    [_, .., _] => 1,
    [..] => 2
};
```

## Получение элементов в переменные

Отдельные значения массива/списка можно получить в переменные, например:

```Csharp
int[] numbers = { 2, 3, 5 };
if(numbers is [var first, var second, .., var last])
{
    Console.WriteLine($"first: {first}, second: {second}  last: {last}");
}
```

В данном случае получаем первый элемент массива в переменную first, второй элемент - в переменную second, а последний элемент - в переменную last.

Пример с различными массивами:

```Csharp
Console.WriteLine(GetData(new[] { 1, 2, 3 }));        // First: 1  Second: 2  Last: 3
Console.WriteLine(GetData(new[] { 2, 4, 6, 8 }));    // First: 2  Second: 4  Last: 8
Console.WriteLine(GetData(new[] { 1, 2 }));          // Array has less than 3 elements
 
string GetData(int[] values) => values switch
{
    [var first, var second, .., var last] => $"First: {first}  Second: {second}  Last: {last}",
    [..] => "Array has less than 3 elements"
};
```

В данном случае получаем первый элемент массива в переменную first, второй элемент - в переменную second, а последний элемент - в переменную last.

При этом значения, которые проектируются на паттерн .., также можно получить в переменную. Например, в паттерне [2, .. var middle, 5] элементы, которые проектируются на .., можно передаются в переменную middle. Несколько примеров:

```Csharp
Console.WriteLine(GetSlice(new[] { 2, 3, 4, 5}));       // Middle: 3, 4
Console.WriteLine(GetSlice(new[] { 2, 4, 6, 8 }));      // End: 4, 6, 8
Console.WriteLine(GetSlice(new[] { 1, 2, 3, 5 }));      // Start: 1, 2, 3
Console.WriteLine(GetSlice(new[] { 1, 2, 3, 4 }));      // All: 1, 2, 3, 4
Console.WriteLine(GetSlice(new int[] { }));             // All: 
 
string GetSlice(int[] values) => values switch
{
    [2, .. var middle, 5] => $"Middle: {string.Join(", ", middle)}",
    [2, .. var end] => $"End: {string.Join(", ", end)}",
    [.. var start, 5] => $"Start: {string.Join(", ", start)}",
    [.. var all] => $"All: {string.Join(", ", all)}"
};
```

## Свойства коллекций

Стоит отметить, что, поскольку массивы и списки - обычные классы C#, которые имеют свойства, то для них мы также можем применять паттерн свойств. Объединение паттерна свойств и паттерна списков позволяет упростить решение некоторых задач. Например, у нас есть задача: если массив имеет три элемента, то разложить его на три переменных:

```Csharp
int[] numbers = { 2, 3, 5 };
if(numbers is { Length:3} and [var first, var second, var third])
{
    Console.WriteLine($"first: {first}, second: {second}  third: {third}");
}
```