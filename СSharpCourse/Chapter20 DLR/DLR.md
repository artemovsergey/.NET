# Dynamic Language Runtime

## DLR в C#. Ключевое слово dynamic

Хотя C# относится к статически типизированным языкам, в последних версиях языка были добавлены некоторые динамические возможности. Так, начиная с .NET 4.0 была добавлена новая функциональность под названием DLR (Dynamic Language Runtime). DLR представляет среду выполнения динамических языков, например, таких языков как IronPython и IronRuby.

Чтобы понять значение данного нововведение, нужно осознавать разичие между языками со статической и динамической типизицией. В языках со статической типизацией выявление всех типов и их членов - свойств и методов происходит на этапе компиляции. А в динамических языках системе ничего не известно о свойствах и методах типов вплоть до выполнения.

Благодаря этой среде DLR C# может создавать динамические объекты, члены которых выявляются на этапе выполнения программы, и использовать их вместе с традиционными объектами со статической типизацией.

Ключевым моментом использования DLR в C# является применение типов dynamic. Это ключевое слово позволяет опустить проверку типов во время компиляции. Кроме того, объекты, объявленные как dynamic, могут в течение работы программы менять свой тип. Например:

```Csharp
dynamic obj = 3;            // здесь obj - целочисленное int
Console.WriteLine(obj);     // 3
 
obj = "Hello world";        // obj - строка
Console.WriteLine(obj);     // Hello world
 
obj = new Person("Tom", 37); // obj - объект Person
Console.WriteLine(obj);      // Person { Name = Tom, Age = 37 }
 
record class Person(string Name, int Age);
```

Несмотря на то, что переменная x меняет тип своего значения несколько раз, данный код будет нормально работать. В этом использование типов dynamic отличается от применения ключевого слова var. Для переменной, объявленной с помощью ключевого слова var, тип выводится во время компиляции и затем во время выполнения больше не меняется.

Также можно найти общее между использованием dynamic и типом object. Если в предыдущем примере мы заменим dynamic на object: object x = 3;, то результат будет тот же. Однако и тут есть различия. Например:

```Csharp
object obj = 24;
dynamic dyn = 24;
obj += 4; // так нельзя
dyn += 4; // а так можно
```
На строке obj += 4; мы увидим ошибку, так как операция += не может быть применена к типам object и int. С переменной, объявленной как dynamic, это пройдет, так как ее тип будет известен только во время выполнения.

Еще одна отличительная особенность использования dynamic состоит в том, что это ключевое слово применяется не только к переменным, но и к свойствам и методам. Например:

```Csharp
class Person
{
    public string Name { get;}
    public dynamic Age { get; set; }
    public Person(string name, dynamic age)
    {
        Name = name; Age = age;
    }
 
    // выводим зарплату в зависимости от переданного формата
    public dynamic GetSalary(dynamic value, string format)
    {
        if (format == "string") return $"{value} euro";
        else if (format == "int") return value;
        else return 0.0;
    }
 
    public override string ToString() => $"Name: {Name}  Age: {Age}";
}
```
В классе Person определено динамическое свойство Age, поэтому при задании значения этому свойству мы можем написать и person.Age=22, и person.Age="twenty-two". Оба варианта будут допустимыми. А через параметр age в конструкторе этому свойству можно передать любое значение.

Также есть метод GetSalary, который возвращает значение dynamic. Например, в зависимости от параметра мы можем вернуть или строковое представление суммы дохода или численное. Также метод принимает dynamic в качестве параметра. Таким образом, мы можем передать в качестве значения дохода как целое, так и дробное число или строку. Посмотрим на конкретное применение:

```Csharp
dynamic tom = new Person("Tom", 22);
Console.WriteLine(tom);
Console.WriteLine(tom.GetSalary(28, "int"));
 
dynamic bob = new Person("Bob", "twenty-two");
Console.WriteLine(bob);
Console.WriteLine(bob.GetSalary("twenty-eight", "string"));
```
Консольный вывод программы:

```
Name: Tom  Age: 22
28
Name: Bob  Age: twenty-two
twenty-eight euro
```

# DynamicObject и ExpandoObject

Интересные возможности при разработке в C# и .NET с использованием DLR предоставляет пространство имен System.Dynamic и в частности класс ExpandoObject. Он позволяет создавать динамические объекты, наподобие тех, что используются в javascript:

```Csharp
// определяем объект, который будет хранять ряд значений
dynamic person = new System.Dynamic.ExpandoObject();
person.Name = "Tom";
person.Age = 46;
person.Languages = new List<string> { "english", "german", "french" };
 
Console.WriteLine($"{person.Name} - {person.Age}");
foreach (var lang in person.Languages)
    Console.WriteLine(lang);
 
// объявляем метод
person.IncrementAge = (Action<int>)(x => person.Age += x);
person.IncrementAge(6); // увеличиваем возраст на 6 лет
Console.WriteLine($"{person.Name} - {person.Age}");
</int></string>
```
Консольный вывод:

```
Tom - 46
english
german
french
Tom - 52
```
У динамического объекта ExpandoObject можно объявить любые свойства, например, Name, Age, Languages, которые могут представлять самые различные объекты. Кроме того, можно задать методы с помощью делегатов.

## DynamicObject

На ExpandoObject по своему действию похож другой класс - DynamicObject. Он также позволяет задавать динамические объекты, но применяется в более изощренных и сложных ситуациях и когда необходим больший контроль над динамическими объектами. Тогда как ExpandoObject больше подходит для простых ситуаций, где не требуется определять какие-то специфические операции или статические компоненты.

Для использования DynamicObject надо создать свой класс, унаследовав его от DynamicObject и реализовав его методы:

- TryBinaryOperation(): выполняет бинарную операцию между двумя объектами. Эквивалентно стандартным бинарным операциям, например, сложению x + y)

- TryConvert(): выполняет преобразование к определенному типу. Эквивалентно базовому преобразованию в C#, например, (SomeType) obj

- TryCreateInstance(): создает экземпляр объекта

- TryDeleteIndex(): удаляет индексатор

- TryDeleteMember(): удаляет свойство или метод

- TryGetIndex(): получает элемент по индексу через индексатор. В C# может быть эквивалентно следующему выражению int x = collection[i]

- TryGetMember(): получаем значение свойства. Эквивалентно обращению к свойству, например, string n = person.Name

- TryInvoke(): вызов объекта в качестве делегата

- TryInvokeMember(): вызов метода

- TrySetIndex(): устанавливает элемент по индексу через индексатор. В C# может быть эквивалентно следующему выражению collection[i] = x;

- TrySetMember(): устанавливает свойство. Эквивалентно присвоению свойству значения, например: person.Name = "Tom"

- TryUnaryOperation(): выполняет унарную операцию подобно унарным операциям в C#: x++

Каждый из этих методов имеет одну и ту же модель определения: все они возвращают логическое значение, показывающее, удачно ли прошла операция. В качестве первого параметра все они принимают объект связывателя или binder. Если метод представляет вызов индексатора или метода объекта, которые могут принимать параметры, то в качестве второго параметра используется массив object[] - он хранит переданные в метод или индексатор аргументы.

Почти все операции, кроме установки и удаления свойств и индексаторов, возвращают определенное значение (например, если мы получаем значение свойства). В этом случае применяется третий параметр out object vaue, который предназначен для хранения возвращаемого объекта.

Например, определение метода TryInvokeMember():

```Csharp
public virtual bool TryInvokeMember (InvokeMemberBinder binder, object?[]? args, out object? result)
```
Параметр InvokeMemberBinder binder является связывателем - получает свойства и методы объекта, object?[]? args хранит передаваемые аргументы, out object? result предназначен для хранения выходного результата.

Рассмотрим на примере. Создадим класс динамического объекта:

```Csharp
using System.Dynamic;
 
class PersonObject : DynamicObject
{
    // словарь для хранения всех свойств
    Dictionary<string, object> members = new Dictionary<string, object>();
 
    // установка свойства
    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        if(value is not null)
        {
            members[binder.Name] = value;
            return true;
        }
        return false;
    }
    // получение свойства
    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        result = null;
        if (members.ContainsKey(binder.Name))
        {
            result = members[binder.Name];
            return true;
        }
        return false;
    }
    // вызов метода
    public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
    {
        result = null;
        if(args?[0] is int number)
        {
            // получаем метод по имен
            dynamic method = members[binder.Name];
            // вызываем метод, передавая его параметру значение args?[0]
            result = method(number);
        }
        // если result не равен null, то вызов метода прошел успешно
        return result != null;
    }
}
```
Класс наследуется от DynamicObject, так как непосредственно создавать объекты DynamicObject мы не можем. И также здесь переопределяется три унаследованных метода.

Для хранения всех членов класса, как свойств, так и методов, определен словарь Dictionary<string, object> members. Ключами здесь являются названия свойств и методов, а значениями - значения этих свойств.

В методе TrySetMember() производится установка свойства:

```Csharp
bool TrySetMember(SetMemberBinder binder, object? value)
```
Параметр binder хранит название устанавливаемого свойства (binder.Name), а value - значение, которое ему надо установить.

Для получения значения свойства переопределен метод TryGetMember:

```Csharp
bool TryGetMember(GetMemberBinder binder, out object? result)
```
Опять же binder содержит название свойства, а параметр result будет содержать значение получаемого свойства.

Для вызова методов определен метод TryInvokeMember:

```Csharp
public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
{
    result = null;
    if(args?[0] is int number)
    {
        // получаем метод по имен
        dynamic method = members[binder.Name];
        // вызываем метод, передавая его параметру значение args?[0]
        result = method(number);
    }
    // если result не равен null, то вызов метода прошел успешно
    return result != null;
}
```
Сначала с помощью bindera получаем метод и затем передаем ему аргумент args[0], предварительно приведя его к типу int, и результат метода устанавливаем в параметре result. То есть в данном случае подразумевается, что метод будет принимать один параметр типа int и возвращать какой-то результат. Если метод возвращает true, то будем считать, что вызов метод прошел успешно.

Теперь применим класс в программе:

```Csharp
using System.Dynamic;
 
// создаем объект
dynamic person = new PersonObject();
// устанавливаем ряд свойств
person.Name = "Tom";
person.Age = 23;
// определяем метод для изменения свойства Age
Func<int, int> increment = (int n) => { person.Age += n; return person.Age; };
person.IncrementAge = increment;
 
Console.WriteLine($"{person.Name} - {person.Age}"); // Tom - 23
person.IncrementAge(4); // применяем метод
Console.WriteLine($"{person.Name} - {person.Age}"); // Tom - 27
```

Выражение person.Name = "Tom" будет вызывать метод TrySetMember, в который в качестве второго параметра будет передаваться строка "Tom".

Выражение return person.Age; вызывает метод TryGetMember.

Также у объекта person определен метод IncrementAge, который представляет действия лямбда-выражения (int n) => { person.Age += n; return person.Age; };. Это выражение принимает число n, увеличивает на это число свойство Age и возвращает новое значение person.Age. И при вызове этого метода будет происходить обращение к методу TryInvokeMember. И, таким образом, произойдет приращение значения свойства person.Age.

# Использование IronPython в .NET

Одним из ключевых достоинств среды DLR является поддержка таких динамических языков как IronPython и IronRuby. Казалось бы, зачем нам нужны еще языки, тем более которые применяются в рамках другого языка C#?

На самом деле динамические языки, возможно, не часто используются, однако есть сферы, где их применение является целесообразным. Например, написание клиентских сценариев. Возможно, пользователь нашей программы захочет внести какое-то дополнительное поведение в программу и для этого может использоваться IronPython. Можно даже сказать, что создание клиентских сценариев широко распространено в наши дни, многие программы и даже игры поддерживают добавление клиентских сценариев, написанных на различных языках.

Кроме того, возможно, есть библиотеки на Python, функциональность которых может отсутствовать в .NET. И в этом случае опять же нам может помочь IronPython.

Рассмотрим на примере применение IronPython. Но для начала необходимо добавить в проект несколько пакетов через пакетный менеджер NuGet. Для того нажмем в окне проекта на узел Dependencies правой кнопкой мыши и выберем в появившемся списке пункт Manage NuGet Packages... (Управление NuGet-пакетами):

![](https://metanit.com/sharp/tutorial/pics/9.1.png)

И перед нами откроется окно пакетного менеджера. Чтобы найти нужный пакет, введем в поле поиска "DLR", и менеджер отобразит ряд результатов, из которых первый - пакет DynamicLanguageRuntime необходимо установить.

![](https://metanit.com/sharp/tutorial/pics/9.2.png)

После этого в проект в узел Dependencies добавляется библиотека Microsoft.Scripting.

Теперь также нам надо добавить пакет IronPython. Для этого введем в поле поиска "IronPython" и после этого установим одноименный пакет:

![](https://metanit.com/sharp/tutorial/pics/9.3.png)

После установки пакета в узле Dependencies добавляется библиотека IronPython.

Теперь напишем примитивную программу:

```Csharp
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
 
ScriptEngine engine = Python.CreateEngine();
engine.Execute("print('hello, world')");
```
Консольный вывод:

```
hello, world
```
Здесь используется выражение print('hello, world') языка Python, которое выводит на консоль строку. Для создания движка, выполняющего скрипт, применяется класс ScriptEngine. А его метод Execute() выполняет скрипт.

Мы также могли бы определить файл hello.py, то есть обычный текстовый файл с кодом на языке Python, со следующим содержимым:

```
print ("hello, metanit.com")
```
И запустить его в программе:

```Csharp
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
 
ScriptEngine engine = Python.CreateEngine();
engine.ExecuteFile("hello.py");
```
В данном случае предполагается, что скрипт находится в проекте:

![](https://metanit.com/sharp/tutorial/pics/9.4.png)

Также можно использовать абсолютные пути, например, если скрипт располагается по пути "D://hello.py":

```Csharp
ScriptEngine engine = Python.CreateEngine();
engine.ExecuteFile("D://hello.py");
```

## ScriptScope
Объект ScriptScope позволяет взаимодействовать со скриптом, получая или устанавливая его переменные, получая ссылки на функции. Например, напишем простейший скрипт hello2.py, который использует переменные:

```
x = 10
z = x + y
print(z)
```
Теперь напишем программу, которая будет взаимодействовать со скриптом:

```Csharp
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
 
int y = 22;
 
ScriptEngine engine = Python.CreateEngine();
ScriptScope scope = engine.CreateScope();
scope.SetVariable("y", y);
engine.ExecuteFile("hello.py", scope);
dynamic x = scope.GetVariable("x");
dynamic z = scope.GetVariable("z");
Console.WriteLine($"{x} + {y} = {z}");
```
Объект ScriptScope с помощью метода SetVariable позволяет установить переменные в скрипте, а с помощью метода GetVariable() - получить их.

Консольный вывод:

```
32
10 + 22 = 32
```

## Вызов функций из IronPython
Определим в файле hello.py функцию для вычисления квадрата числа:

```
def square(n):
   return n * n
```
Теперь обратимся к этой функции в коде C#:

```Csharp
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
 
int number = 5;
 
ScriptEngine engine = Python.CreateEngine();
ScriptScope scope = engine.CreateScope();
 
engine.ExecuteFile("hello.py", scope);
dynamic square = scope.GetVariable("square");
// вызываем функцию и получаем результат
dynamic result = square(number);
Console.WriteLine(result);      // 25
```
Получить объект функции можно также, как и переменную: scope.GetVariable("square");. Затем с этим объектом работаем также, как и с любым другим методом. В итоге при передаче в метод/функцию square числа 5 его результатом будет 25.
