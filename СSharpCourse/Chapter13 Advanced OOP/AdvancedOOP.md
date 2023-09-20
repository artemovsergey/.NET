# Дополнительные классы и структуры .NET

## Отложенная инициализация и тип Lazy

Приложение может использовать множество классов и объектов. Однако есть вероятность, что не все создаваемые объекты будут использованы. Особенно это касается больших приложений. Например:

```Csharp
class Reader
{
    Library library = new Library();
    public void ReadBook()
    {
        library.GetBook();
        Console.WriteLine("Читаем бумажную книгу");
    }
 
    public void ReadEbook()
    {
        Console.WriteLine("Читаем книгу на компьютере");
    }
}
 
class Library
{
    private string[] books = new string[99];
 
    public void GetBook()
    {
        Console.WriteLine("Выдаем книгу читателю");
    }
}
```

Есть класс Library, представляющий библиотеку и хранящий некоторый набор книг в виде массива. Есть класс читателя Reader, который хранит ссылку на объект библиотеки, в которой он записан. У читателя определено два метода: для чтения электронной книги и для чтения обычной книги. Для чтения обычной книги необходимо обратиться к методу класса Library, чтобы получить эту книгу.

Но что если читателю вообще не придется читать обычную книгу, а только электронные, например, в следующем случае:

```Csharp
Reader reader = new Reader();
reader.ReadEbook();
```

В этом случае объект library в классе читателя никак не будет использоваться и будет только занимать место памяти. Хотя надобности в нем не будет.

Для подобных случаев в .NET определен специальный класс Lazy<T>. Изменим класс читателя следующим образом:

```Csharp
class Reader
{
    Lazy<Library> library = new Lazy<Library>();
    public void ReadBook()
    {
        library.Value.GetBook();
        Console.WriteLine("Читаем бумажную книгу");
    }
 
    public void ReadEbook()
    {
        Console.WriteLine("Читаем книгу на компьютере");
    }
}
```

Класс Library остается прежнем. Но теперь класс читателя содержит ссылку на библиотеку в виде объекта Lazy<Library>. А чтобы обратиться к самой библиотеке и ее методам, надо использовать выражение library.Value - это и есть объект Library.

Что меняет в поведении класса Reader эта замена? Рассмотрим его применение:

```Csharp
Reader reader = new Reader();
reader.ReadEbook();
reader.ReadBook();
```

Непосредственно объект Library задействуется здесь только на третьей строке в методе reader.ReadBook(), который вызывает в свою очередь метод library.Value.GetBook(). Поэтому вплоть до третьей строки объект Library, используемый читателем, не будет создан. Если мы не будем применять в программе метод reader.ReadBook(), то объект библиотеки тогда вобще не будет создан, и мы избежим лишних затрат памяти. Таким образом, Lazy<T> гарантирует нам, что объект будет создан только тогда, когда в нем есть необходимость.

# Математические вычисления и класс Math

Для выполнения различных математических операций в библиотеке классов .NET предназначен класс Math. Он является статическим, поэтому все его методы также являются статическими.

Рассмотрим основные методы класса Math:

- Abs(double value): возвращает абсолютное значение для аргумента value

```Csharp
double result = Math.Abs(-12.4); // 12.4
```

- Acos(double value): возвращает арккосинус value. Параметр value должен иметь значение от -1 до 1

```Csharp
double result = Math.Acos(1); // 0
```

- Asin(double value): возвращает арксинус value. Параметр value должен иметь значение от -1 до 1
- Atan(double value): возвращает арктангенс value
- BigMul(int x, int y): возвращает произведение x * y в виде объекта long

```Csharp
double result = Math.BigMul(100, 9340); // 934000
```

- Ceiling(double value): возвращает наименьшее целое число с плавающей точкой, которое не меньше value

```Csharp
double result = Math.Ceiling(2.34); // 3
```

- Cos(double d): возвращает косинус угла d
- Cosh(double d): возвращает гиперболический косинус угла d  

- DivRem(int a, int b, out int result): возвращает результат от деления a/b, а остаток помещается в параметр result

```Csharp
int result;
int div = Math.DivRem(14, 5, out result);
//result = 4
// div = 2
```

- Exp(double d): возвращает основание натурального логарифма, возведенное в степень d

- Floor(decimal d): возвращает наибольшее целое число, которое не больше d

```Csharp
double result = Math.Floor(2.56); // 2
```

- IEEERemainder(double a, double b): возвращает остаток от деления a на b

```Csharp
double result = Math.IEEERemainder(26, 4); // 2 = 26-24
```

- Log(double d): возвращает натуральный логарифм числа d

- Log(double a, double newBase): возвращает логарифм числа a по основанию newBase

- Log10(double d): возвращает десятичный логарифм числа d

- Max(double a, double b): возвращает максимальное число из a и b

- Min(double a, double b): возвращает минимальное число из a и b

- Pow(double a, double b): возвращает число a, возведенное в степень b

- Round(double d): возвращает число d, округленное до ближайшего целого числа

```Csharp
double result1 = Math.Round(20.56); // 21
double result2 = Math.Round(20.46); //20
```

- Round(double a, int b): возвращает число a, округленное до определенного количества знаков после запятой, представленного параметром b

```Csharp
double result1 = Math.Round(20.567, 2); // 20,57
double result2 = Math.Round(20.463, 1); //20,5
```

- Sign(double value): возвращает число 1, если число value положительное, и -1, если значение value отрицательное. Если value равно 0, то возвращает 0

```Csharp
int result1 = Math.Sign(15); // 1
int result2 = Math.Sign(-5); //-1
```

- Sin(double value): возвращает синус угла value

- Sinh(double value): возвращает гиперболический синус угла value

- Sqrt(double value): возвращает квадратный корень числа value

```Csharp
double result1 = Math.Sqrt(16); // 4
```

- Tan(double value): возвращает тангенс угла value

- Tanh(double value): возвращает гиперболический тангенс угла value

- Truncate(double value): отбрасывает дробную часть числа value, возвращаяя лишь целое значние

```Csharp
double result = Math.Truncate(16.89); // 16
```

Также класс Math определяет две константы: Math.E и Math.PI. Например, вычислим площадь круга:

```Csharp
double radius = 50;
double area = Math.PI * Math.Pow(radius, 2);
Console.WriteLine($"Площадь круга с радиусом {radius} равна {Math.Round(area,2)}");
Площадь круга с радиусом 50 равна 7853,98
```

# Преобразование типов и класс Convert

## Методы Parse и TryParse

Все примитивные типы имеют два метода, которые позволяют преобразовать строку к данному типу. Это методы Parse() и TryParse().

Метод Parse() в качестве параметра принимает строку и возвращает объект текущего типа. Например:

```Csharp
int a = int.Parse("10");
double b = double.Parse("23,56");
decimal c = decimal.Parse("12,45");
byte d = byte.Parse("4");
Console.WriteLine($"a={a}  b={b}  c={c}  d={d}");
```

Стоит отметить, что парсинг дробных чисел зависит от настроек текущей культуры. В частности, для получения числа double я передаю строку "23,56" с запятой в качестве разделителя. Если бы я передал точку вместо запятой, то приложение выдало ошибку выполнения. На компьютерах с другой локалью, наоборот, использование запятой вместо точки выдало бы ошибку.

Чтобы не зависеть от культурных различий мы можем установить четкий формат с помощью класса NumberFormatInfo и его свойства NumberDecimalSeparator:

```Csharp
using System.Globalization;
 
IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
double number = double.Parse("23.56", formatter);
Console.WriteLine(number);   // 23,56
```

В данном случае в качестве разделителя устанавливается точка. Однако тем не менее потенциально при использовании метода Parse мы можем столкнуться с ошибкой, например, при передачи алфавитных символов вместо числовых. И в этом случае более удачным выбором будет применение метода TryParse(). Он пытается преобразовать строку к типу и, если преобразование прошло успешно, то возвращает true. Иначе возвращается false:

```Csharp
Console.WriteLine("Введите строку:");
string? input = Console.ReadLine();
 
bool result = int.TryParse(input, out var number);
if (result == true)
    Console.WriteLine($"Преобразование прошло успешно. Число: {number}");
else
    Console.WriteLine("Преобразование завершилось неудачно");
```

Если преобразование пройдет неудачно, то исключения никакого не будет выброшено, просто метод TryParse возвратит false, а переменная number будет содержать значение по умолчанию.

## Convert

Класс Convert представляет еще один способ для преобразования значений. Для этого в нем определены следующие статические методы:

- ToBoolean(value)

- ToByte(value)

- ToChar(value)

- ToDateTime(value)

- ToDecimal(value)

- ToDouble(value)

- ToInt16(value)

- ToInt32(value)

- ToInt64(value)

- ToSByte(value)

- ToSingle(value)

- ToUInt16(value)

- ToUInt32(value)

- ToUInt64(value)

В качестве параметра в эти методы может передаваться значение различных примитивных типов, необязательно строки:

```Csharp
int n = Convert.ToInt32("23");
bool b = true;
double d = Convert.ToDouble(b);
Console.WriteLine($"n={n}  d={d}");
```

Однако опять же, как и в случае с методом Parse, если методу не удастся преобразовать значение к нужному типу, то он выбрасывает исключение FormatException.

# Класс Array и массивы

Все массивы в C# построены на основе класса Array из пространства имен System. Этот класс определяет ряд свойств и методов, которые мы можем использовать при работе с массивами. Основные свойства и методы:

- Свойство Length возвращает длину массива

- Свойство Rank возвращает размерность массива

- int BinarySearch (Array array, object? value) выполняет бинарный поиск в отсортированном массиве и возвращает индекс найденного элемента

- void Clear (Array array) очищает массив, устанавливая для всех его элементов значение по умолчанию

- void Copy (Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length) копирует из массива sourceArray начиная с индекс sourceIndex length элементов в массив destinationArray начиная с индекса destinationIndex

- bool Exists<T> (T[] array, Predicate<T> match) проверяет, содержит ли массив array элементы, которые удовлеворяют условию делегата match

- void Fill<T> (T[] array, T value) заполняет массив array значением value

- T? Find<T> (T[] array, Predicate<T> match) находит первый элемент, который удовлеворяет определенному условию из делегата match. Если элемент не найден, то возвращается null

- T? FindLast<T> (T[] array, Predicate<T> match) находит последний элемент, который удовлеворяет определенному условию из делегата match. Если элемент не найден, то возвращается null

- int FindIndex<T> (T[] array, Predicate<T> match) возвращает индекс первого вхождения элемента, который удовлеворяет определенному условию делегата match

- int FindLastIndex<T> (T[] array, Predicate<T> match) возвращает индекс последнего вхождения элемента, который удовлеворяет определенному условию

- T[] FindAll<T> (T[] array, Predicate<T> match) возвращает все элементы в виде массива, которые удовлеворяет определенному условию из делегата match

- int IndexOf (Array array, object? value) возвращает индекс первого вхождения элемента в массив

- int LastIndexOf (Array array, object? value) возвращает индекс последнего вхождения элемента в массив

- void Resize<T> (ref T[]? array, int newSize) изменяет размер одномерного массива

- void Reverse (Array array) располагает элементы массива в обратном порядке

- void Sort (Array array) сортирует элементы одномерного массива

Разберем самые используемые методы.

## Поиск индекса элемента

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
// находим индекс элемента "Bob"
int bobIndex = Array.BinarySearch(people, "Bob");
// находим индекс первого элемента "Tom"
int tomFirstIndex = Array.IndexOf(people, "Tom");
// находим индекс последнего элемента "Tom"
int tomLastIndex = Array.LastIndexOf(people, "Tom");
// находим индекс первого элемента, у которого длина строки больше 3
int lengthFirstIndex = Array.FindIndex(people, person => person.Length > 3);
// находим индекс последнего элемента, у которого длина строки больше 3
int lengthLastIndex = Array.FindLastIndex(people, person => person.Length > 3);
 
Console.WriteLine($"bobIndex: {bobIndex}");                 // 2
Console.WriteLine($"tomFirstIndex: {tomFirstIndex}");       // 0
Console.WriteLine($"tomLastIndex: {tomLastIndex}");         // 4
Console.WriteLine($"lengthFirstIndex: {lengthFirstIndex}"); // 3
Console.WriteLine($"lengthLastIndex: {lengthLastIndex}");   // 5
```

Если элемент не найден в массиве, то методы возвращают -1.

## Поиск элемента по условию

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
// находим первый и последний элементы
// где длина строки больше 3 символов
string? first = Array.Find(people, person => person.Length > 3);
Console.WriteLine(first); // Kate
string? last = Array.FindLast(people, person => person.Length > 3);
Console.WriteLine(last); // Alice
 
// находим элементы, у которых длина строки равна 3
string[] group = Array.FindAll(people, person => person.Length == 3);
foreach (var person in group) Console.WriteLine(person);
// Tom Sam Bob Tom
```

## Изменение порядка элементов массива

Например, изменим порядок элементов:

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
Array.Reverse(people);
 
foreach (var person in people) 
    Console.Write($"{person} ");
// "Alice", "Tom", "Kate", "Bob", "Sam", "Tom"
```

Также можно изменить порядок только части элементов:

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
// изменяем порядок 3 элементов начиная c индекса 1  
Array.Reverse(people, 1, 3);
 
foreach (var person in people) 
    Console.Write($"{person} ");
// "Tom", "Kate", "Bob", "Sam", "Tom", "Alice"
```

В данном случае изменяем порядок только 3 элементов начиная c индекса 1.

## Изменение размера массива

Для изменения размера массива применяется метод Resize. Его первый параметр - изменяемый массив, а второй параметр - количество элементов, которые должны быть в массиве. Если второй параметр меньше длины массива, то массив усекается. Если значение параметра, наоборот, больше, то массив дополняется дополнительными элементами, которые имеют значение по умолчанию. Причем первый параметр передается по ссылке:

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
// уменьшим массив до 4 элементов
Array.Resize(ref people, 4);
 
foreach (var person in people) 
    Console.Write($"{person} ");
// "Tom", "Sam", "Bob", "Kate"
```

## Копирование массива

Метод Copy копирует часть одного массива в другой:

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
var employees = new string[3];
 
// копируем 3 элемента из массива people c индекса 1  
// и вставляем их в массив employees начиная с индекса 0
Array.Copy(people,1, employees,0, 3);
 
foreach (var person in employees) 
    Console.Write($"{person} ");
// Sam Bob Kate
```

В данном случае копируем 3 элемента из массива people начиная c индекса 1 и вставляем их в массив employees начиная с индекса 0.

## Сортировка массива

Отсортируем массив с помощью метода Sort():

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
Array.Sort(people);
 
foreach (var person in people) 
    Console.Write($"{person} ");
     
// Alice Bob Kate Sam Tom Tom
```

Этот метод имеет много перегрузок. Например, одна из версий позволяет отсортировать только часть массива:

```Csharp
var people = new string[] { "Tom", "Sam", "Bob", "Kate", "Tom", "Alice" };
 
// сортируем с 1 индекса 3 элемента
Array.Sort(people, 1, 3);
 
foreach (var person in people) 
    Console.Write($"{person} ");
     
// Tom Bob Kate Sam Tom Alice
```

# Span

Тип Span представляет непрерывную область памяти. Цель данного типа - повысить производительность и эффективность использования памяти. Span позволяет избежать дополнительных выделений памяти при операции с наборами данных. Поскольку Span является структурой, то объект этого типа располагаетс в стеке, а не в хипе.

## Создание Span

Для создания объекта Span можно применять один из его конструкторов:

- Span(): создает пустой объект Span

- Span(T[] array): создает объект Span из массива array

- Span(void* pointer, int length): создает объект Span, который получает length байт памяти, начиная с указателя pointer

- Span(T[] array, int start, int length): создает объект Span, который получает из массива array length элементов, начиная с индекса start

Например, простейшее создание Span:

```Csharp
string[] people = new string[] { "Tom", "Alice", "Bob" };
Span<string> peopleSpan = new Span<string>(people);
```

Мы также можем непосредственно присвоить массив, и он неявно будет преобразован в Span:

```Csharp
string[] people = new string[] { "Tom", "Alice", "Bob" };
Span<string> peopleSpan = people;
```

Далее мы можем получать, устанавливать или перебирать данные также, как в массиве:

```Csharp
string[] people = new string[] { "Tom", "Alice", "Bob" };
Span<string> peopleSpan = people;
peopleSpan[1] = "Ann";              // переустановка значения элемента
Console.WriteLine(peopleSpan[2]);   // получение элемента
Console.WriteLine(peopleSpan.Length);   // получение длины Span
 
// перебор Span
foreach (var s in peopleSpan)
{
    Console.WriteLine(s);
}
```

Если Span ведет себя внешне как массив, то в чем его преимущество или когда он нам может пригодиться? Рассмотрим простейшую ситуацию - у нас есть массив со значениями дневных температур воздуха за месяц, и нам надо получить их него два набора - набор температур за первую декаду и за последнюю декаду. Используя массивы, мы могли бы сделать так:

```Csharp
int[] temperatures = new int[]
{
    10, 12, 13, 14, 15, 11, 13, 15, 16, 17,
    18, 16, 15, 16, 17, 14,  9,  8, 10, 11,
    12, 14, 15, 15, 16, 15, 13, 12, 12, 11
};
int[] firstDecade = new int[10];    // выделяем память для первой декады
int[] lastDecade = new int[10];     // выделяем память для второй декады
Array.Copy(temperatures, 0, firstDecade, 0, 10);    // копируем данные в первый массив
Array.Copy(temperatures, 20, lastDecade, 0, 10);    // копируем данные во второй массив
```

Для хранения данных создаются два дополнительных массива для дневных температур каждой декады. С помощью метода Array.Copy данные из исходного массива temperatures копируются в два остальных массива. Но суть в данном случае в том, что для обоих массивов мы вынуждены выделить память. То есть оба массива по сути содержат те же данные, что и temperatures, однако в отдельных частях памяти.

Span позволяет работать с памятью более эффективно и избежать ненужных выделений памяти. Так, используем вместо массивов Span:

```Csharp
int[] temperatures = new int[]
{
    10, 12, 13, 14, 15, 11, 13, 15, 16, 17,
    18, 16, 15, 16, 17, 14,  9,  8, 10, 11,
    12, 14, 15, 15, 16, 15, 13, 12, 12, 11
};
Span<int> temperaturesSpan = temperatures;
 
Span<int> firstDecade = temperaturesSpan.Slice(0, 10);    // нет выделения памяти под данные
Span<int> lastDecade = temperaturesSpan.Slice(20, 10);    // нет выделения памяти под данные
```

Для создания производных объектов Span применяется метод Slice, который из Spana выделяет часть и возвращает ее в виде другого объекта Span. Теперь объекты Span firstDecade и lastDecade работают с теми же данными, что и temperaturesSpan, а дополнительно память не выделяется. То есть во всех трех случаях мы работаем с тем же массивом temperatures. Мы даже можем в одном Span изменить данные, и данные изменятся в другом:

```Csharp
int[] temperatures = new int[]
{
    10, 12, 13, 14, 15, 11, 13, 15, 16, 17,
    18, 16, 15, 16, 17, 14,  9,  8, 10, 11,
    12, 14, 15, 15, 16, 15, 13, 12, 12, 11
};
Span<int> temperaturesSpan = temperatures;
 
Span<int> firstDecade = temperaturesSpan.Slice(0, 10);
 
temperaturesSpan[0] = 25; // меняем в temperatureSpan
Console.WriteLine(firstDecade[0]); //25
```

За счет чего это достигается? Для понимания работы Span можно обратиться к исходному коду типа. В частности, мы можем в нем увидеть следующее свойство:

```Csharp
public readonly ref struct Span<T>
{
    //....
    public ref T this[int index] { get { ... } }
    //....
}
```

Здесь мы видим, что индексатор возвращает ref-ссылку, благодаря чем мы получаем доступ непосредственно к объекту и можем его изменять.

В данном случае, конечно, преимущества от отсутствия выделения дополнительной памяти под хранение объектов минимальны. Но при более интенсивной работе с данными выигрыш в производительности неизбежно должен возрастать.

## Методы Span

Основные методы Span:

- void Fill(T value): заполняет все элементы Span значением value

- T[] ToArray(): преобразует Span в массив

- Span<T> Slice(int start, int length): выделяет из Span length элементов начиная с индекса start в виде другого Span

- void Clear(): очищает Span

- void CopyTo(Span<T> destination): копирует элементы текущего Span в другой Span

- bool TryCopyTo(Span<T> destination): копирует элементы текущего Span в другой Span, но при этом также возвращает значение bool, которое указывает, удачно ли прошла операция копирования

## ReadOnlySpan

Структура ReadOnlySpan аналогична Span, только предназначена для неизменяемых данных. Например:

```Csharp
string text = "hello, world";
string worldString = text.Substring(startIndex: 7, length: 5);           // есть выделение памяти под символы
ReadOnlySpan<char> worldSpan = text.AsSpan().Slice(start: 7, length: 5); // нет выделения памяти под символы
//worldSpan[0] = 'a'; // Нельзя изменить
Console.WriteLine(worldSpan[0]); // выводим первый символ
 
// перебор символов
foreach(var c in worldSpan)
{
    Console.Write(c);
}
```

В данном случае с помощью метода AsSpan() преобразуем строку в объект ReadOnlySpan<char> и затем выделяем из него диапазон символов "world". Поскольку ReadOnlySpan предназначен только для чтения, то соответственно мы не можем изменить через него данные, но получить можем. В остальном работа с ReadOnlySpan идет так же, как с Span.

## Ограничения Span

Как структура, определенная с модификатором ref, Span имеет ряд ограничений: она не может быть присвоена переменной типа Object, dynamic или переменной типа интерфейса. Она не может быть полем в объекте ссылочного типа (а только внутри ref-структур). Она не может использоваться в пределах операций await или yield.

# Индексы и диапазоны

В C# 8.0 была добавлена новая функциональность - индексы и диапазоны, которые упрощают получение из массивов подмассивов. Для этого в C# есть два типа: System.Range и System.Index. Оба типа являются структурами. Тип Range представляет некоторый диапазон значений в некоторой последовательность, а тип Index - индекс в последовательности.

## Индексы

Индекс фактически представляет числовое значение, и при определении индекса мы можем указать это значение:

```Csharp
Index myIndex = 2;
```

В данном случае индекс представляет третий элемент последовательности (индексация начинается с 0).

С помощью специального оператора ^ можно задать индекс относительно конца последовательности.

```Csharp
Index myIndex = ^2;
```

Теперь индекс представляет второй элемент с конца последовательности, то есть предпоследний элемент.

Используем индексы для получения элементов массива:

```Csharp
Index myIndex1 = 2;     // третий элемент
Index myIndex2 = ^2;    // предпоследний элемент
 
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
string selected1 = people[myIndex1];    // Sam
string selected2 = people[myIndex2];    // Kate
Console.WriteLine(selected1);   
Console.WriteLine(selected2);
```

Фактически для данной задачи индексы не нужны, и мы можем воспользоваться стандартными возможностями массивов:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
string selected1 = people[2];    // Sam
string selected2 = people[people.Length - 2];    // Kate
Console.WriteLine(selected1);   
Console.WriteLine(selected2);
```

То есть в подобных ситуациях плюсом индексов является большая удобочитаемость. Так, people[^2] более читабельно, чем people[people.Length - 2].

## Диапазон

Диапазон представляет часть последовательности, которая ограничена двумя индексами. Начальный индекс включается в диапазон, а конечный индекс НЕ входит в диапазон. Для определения диапазона применяется оператор ..:

```Csharp
Range myRange1 = 1..4; // по 1-го индекса включая по 4-й индекс не включая
```

В данном случае диапазон myRange1 влючает элементы с 1 индекса по 4-й индекс (не включая). При этом элемент по 4-му индексу не включается в диапазон. При этом границы диапазона задаются не просто числами, а именно объектами Index. То есть следующие определения диапазонов будут равноценны:

```Csharp
Index start = 1;
Index end = 4;
Range myRange1 = start..end;
             
Range myRange2 = 1..4;
```

Практическое применение диапазонов - получим со второго по четвертый элементы массива:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
string[] peopleRange = people[1..4]; // получаем 2, 3 и 4-й элементы из массива
foreach(var person in peopleRange)
{
    Console.WriteLine(person);
}
```

Результатом операции people[1..4] является подмассив элементов с 1 по 3 индексы (включая). Консольный вывод:

```
Bob
Sam
Kate
```

Мы можем задать для диапазона только конечный индекс. В этом случае начальным индексом по умолчанию будет 0.

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
string[] peopleRange = people[..4];     // Tom, Bob, Sam, Kate
```

Либо, наоборот, задать только начальный индекс, тогда конечным индексом будет последний индекс последовательности:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
string[] peopleRange = people[1..];     // Bob, Sam, Kate, Alice
```

Используя индексы относительно конца последовательности, можно получать диапазон относительно конца последовательности:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
string[] peopleRange1 = people[^2..];       // два последних - Kate, Alice
string[] peopleRange2 = people[..^1];       // начиная с предпоследнего - Tom, Bob, Sam, Kate
string[] peopleRange3 = people[^3..^1];     // два начиная с предпоследнего - Sam, Kate
```

Кроме массивов индексы и диапазоны также применяются к объектам Span и ReadOnlySpan:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Kate", "Alice" };
Span<string> peopleSpan = people;
Span<string> selectedPeopleSpan = peopleSpan[1..4];
foreach (var person in selectedPeopleSpan)
{
    Console.WriteLine(person);
}
```