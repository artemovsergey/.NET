# Нововведения в C# 11

## Паттерны списков
Паттерны списков (list pattern) позволяют сопоставлять выражения со списками и массивами. Можно сопоставлять для списка-паттерна можно установить все элементы массивы:

```Csharp
[2, 3, 4, 5] // этот паттерн соответствует любому массиву/списку, 
            // который равен [2, 3, 4, 5]
```

Также с помощью символа подчеркивания _ можно задать произвольный элемент, значение которого не имеет значения:

```Csharp
[_, 5] // этот паттерн соответствует любому массиву/списку из двух элементов
        // где второй элемент равен 5 (например, [1, 5] или [25, 5])
```
И, кроме того, с помощью slice-паттерна .. можно задать произвольное количество элементов:

```Csharp
[2, .., 5] // этот паттерн соответствует любому массиву/списку, где первый элемент равен 2
        // а последний элемент равен 5, а между 2 и 5 может быть произвольное количество произвольных элементов
        // (например, [2, 5] или [2, 3, 9, 5])
```
Пример применения:

```Csharp
Console.WriteLine(GetNumber(new[] { 2, 3, 5 }));        // 1
Console.WriteLine(GetNumber(new[] { 2, 4, 5 }));        // 2
Console.WriteLine(GetNumber(new[] { 2, 3, 4, 5 }));     // 3
Console.WriteLine(GetNumber(new[] { 1, 2, 3 }));        // 4
Console.WriteLine(GetNumber(new int[] { }));            // 4
 
int GetNumber(int[] values) => values switch
{
    [2, 3, 5] => 1,
    [2, _, 5] => 2,
    [2, .., 5] => 3,
    [..] => 4
};
```
## Обобщения в атрибутах
В C# 11 можно определять обобщенные классы атрибутов:

```Csharp
class C
{
    [Reader<int>]
    public void M() 
    {
    }
}
 
class ReaderAttribute<T> : Attribute
{
}
```

## Многострочные строки
С помощью трех пар двойных кавычек можно оформить многострочный текст, в том числе с применением интерполяции:

```Csharp
Print();
PrintValue("hello");
 
void Print()
{
    string text = """
              <element attr="content">
                <body>
                </body>
              </element>
              """;
    Console.WriteLine(text);
}
 
void PrintValue(string val)
{
    string text = $"""
              <element attr="content">
                <body>
                {val}
                </body>
              </element>
              """;
    Console.WriteLine(text);
}
```
## Строковые литералы UTF-8
C# 11 позволяет автоматически сконвертировать строки с символами UTF-8 в их бинарное представление в виде объекта ```ReadOnlySpan<byte>```.

```Csharp
ReadOnlySpan<byte> span2 = "hello"u8; 
```
Чтобы указать, что строка представляет utf-8, к строке добавляется суффикс u8

## Файл как область видимости
C# 11 добавил еще один модификатор видимости - file, который с применяется к классам, структурам, делегатам, перечислениям, интерфейсам. Типы с этим модификаторо могут использоваться только внутри текущего файла кода.

```Csharp
file class Person
{
}
```

Данный модификатор не может использоваться в паре с другими модификаторами.

## модификатор required
Модификатор required (добавлен в C# 11) указывает, что поле или свойства с этим модификатором обязательно должны быть инициализированы. Например, в следующем примере мы получим ошибку:

```Csharp
Person tom = new Person();  // ошибка - свойства Name и Age не инициализированы
public class Person
{
    public required string Name { get; set; }
    public required int Age { get; set; }
}
```

Здесь свойства Name и Age отмечены как обязательные для инициализации с помощью модификатора required, поэтому необходимо использовать инициализатор для их инициализации:

```Csharp
Person tom = new Person { Name = "Tom", Age = 38 }; // ошибки нет
```

Причем не важно, устанавливаем эти свойства в конструкторе или инициализируем при определении, все равно надо использовать инициализатор для установки их значений. Например, в следующем примере мы получим ошибку:

```Csharp
Person bob = new Person("Bob"); // ошибка - свойства Name и Age все равно надо установить в инициализаторе
 
public class Person
{
    public Person(string name)
    {
        Name = name;
    }
    public required string Name { get; set; }
    public required int Age { get; set; } = 22;
}
```


# Нововведения в C# 12

## Выражения коллекций
В C# 12 упрощен способ создания массивов и коллекций с помощью выражения коллекций (collection expression), которые представляют унифицированный подход к созданию коллекций. Так, если раньше создание массивов выглядело так:

```Csharp
int[] nums1 = { 1, 2, 3, 4 };
int[] nums2 = new int[] { };   // пустой массив
```
Теперь можно писать так:

```Csharp
int[] nums1 = [ 1, 2, 3, 4 ];
int[] nums2 = [];   // пустой массив
```
Аналогичным образом можно использовать выражения коллекций для создания других типов коллекций:

```Csharp
List<int> list1 = [1, 2, 3, 4];
List<int> list2 = []; //  пустой список
Span<int> span1 = [1, 2, 3, 4];
```

## Первичные конструкторы
Первичные конструкторы (Primary constructors) позволяют добавлять параметры к определению класса/структуры и использовать эти параметры внутри класса/структуры:

```Csharp
var tom = new Person("Tom", 38);
Console.WriteLine(tom);
 
public class Person(string name, int age)
{
    public Person(string name) : this(name, 18) { }
    public string Name => name;
    public int Age => age;
 
    public override string ToString() => $"name: {name}, age: {age}";
}
```
Здесь для класса Person определен первичный конструктор с двумя параметрами - name и age. Эти параметры применяются для инициализации свойств Name и Age и используются в методе ToString().

За кадром для каждого параметра первичного конструктора в классе создается приватное поле, которое хранит значение параметра. Благодаря этому они могут использоваться в теле класса.

Кроме первичных конструкторов класс может определять дополнительные конструкторы, как примере выше. Но эти дополнительные конструкторы должны вызывать первичный конструктор:

```Csharp
public Person(string name) : this(name, 18) { }
```

## Псевдонимы типов
C# 12 позволяет определять псевдонимы для любых типов. Например:

```Csharp
using People = System.Collections.Generic.List<Person>;
 
People people = new(){ new ("Tom", 38), new ("Bob", 42) };
people.ForEach(Console.WriteLine);
 
public record Person(string Name, int Age);
```

Здесь "People" выступает в качестве псевдонима для типа List<Person>

Другой пример:

```Csharp
using user = (string, int);
user tom = ("Tom", 38);
Console.WriteLine(tom);     // (Tom, 38)
```

Здесь имя user выступает в качестве псевдонима для кортежа типа (string, int)

Причем мы можем дать элементам кортежа имя:

```Csharp
using user = (string name, int age);
user tom = ("Tom", 38);
Console.WriteLine(tom.name);     // Tom
Console.WriteLine(tom.age);     // 38
```
Еще один пример:

```Csharp
using BinaryOp = System.Func<int, int, int>;
 
BinaryOp sum = (a, b) => a + b;
BinaryOp subtract = (a, b) => a - b;
 
DoOperation(10, 6, sum);        // 16
DoOperation(10, 6, subtract);   // 4
 
void DoOperation(int a, int b, BinaryOp op)
{
    Console.WriteLine(op(a, b));
}
```

Здесь для делегата типа System.Func<int, int, int> присваивается псевдоним BinaryOp.

## Значения по умолчанию для параметров лямбда-выражений
Начиная с C# 12 параметры лямбда-выражений могут иметь значения по умолчанию:

```Csharp
var welcome = (string message = "hello")=> Console.WriteLine(message);
 
welcome("hello world"); // hello world
welcome();              // hello
```

## ref-параметры только для чтения
Чтобы гарантировать, что ref-параметр не изменит своего значения, начиная с версии C# 12 можно применять ref-параметры только для чтения. Такие параметры предваряются ключевым словом readonly:

```Csharp
void Increment(ref readonly int n)
{
    // n++; // нельзя, иначе будет очишка компиляции
    Console.WriteLine($"Число в методе Increment: {n}");
}
  
int number = 5;
Increment(ref number);
Console.WriteLine($"Число после метода Increment: {number}");
```

В данном случае в метод Increment параметр n передается по ссылке и при этом он доступен только для чтения. При попытке изменить его значение мы получим ошибку на этапе компиляции.

## Упрощение определения типов
Начиная с версии C# 12, если тип - класс/структура/интерфейс имеют пустое определение (не содержат полей, свойств, методов), то фигурные скобки после названия типа можно не использовать:

```Csharp
class Person;
struct User;
interface Human;
```


