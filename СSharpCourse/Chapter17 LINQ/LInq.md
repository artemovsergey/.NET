# LINQ
## Основы LINQ

LINQ (Language-Integrated Query) представляет простой и удобный язык запросов к источнику данных. В качестве источника данных может выступать объект, реализующий интерфейс IEnumerable (например, стандартные коллекции, массивы), набор данных DataSet, документ XML. Но вне зависимости от типа источника LINQ позволяет применить ко всем один и тот же подход для выборки данных.

Существует несколько разновидностей LINQ:

- LINQ to Objects: применяется для работы с массивами и коллекциями

- LINQ to Entities: используется при обращении к базам данных через технологию Entity Framework

- LINQ to XML: применяется при работе с файлами XML

- LINQ to DataSet: применяется при работе с объектом DataSet

- Parallel LINQ (PLINQ): используется для выполнения параллельных запросов

В этой главе речь пойдет прежде всего о LINQ to Objects, но в последующих материалах также будут затронуты и другие разновидности LINQ. Основная часть функциональности LINQ сосредоточена в пространстве имен System.LINQ. В проектах под .NET 6 данное пространство имен подключается по умолчанию.

В чем же удобство LINQ? Посмотрим на простейшем примере. Выберем из массива строки, которые начинаются на определенную букву, например, букву "T", и отсортируем полученный список:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };
 
// создаем новый список для результатов
var selectedPeople = new List<string>();
// проходим по массиву
foreach (string person in people)
{
    // если строка начинается на букву T, добавляем в список
    if (person.ToUpper().StartsWith("T"))
        selectedPeople.Add(person);
}
// сортируем список
selectedPeople.Sort();
 
foreach (string person in selectedPeople)
    Console.WriteLine(person);
```
Для отфильтрованных строк создается специальный список. Затем в цикле проходим по всем элементам массива и, если они соответствуют условию (начинаются на букву T), то добавляем их в этот список. Затем сортируем список по возрастанию. И в конце элементы полученного списка выводим на консоль:

```
Tim
Tom
Tomas
```
Хотя подобный подход вполне работает, однако LINQ позволяет значительно сократить код с помощью интуитивно понятного и краткого синтаксиса.

Для работы с колекциями можно использовать два способа:

- Операторы запросов LINQ
- Методы расширений LINQ

Рассмотрим оба способа.


# Операторы запросов LINQ
Операторы запросов LINQ в каком-то роде частично напоминают синтаксис запросов SQL, поэтому если вы работали когда-нибудь с sql-запросами, то будет проще понять общую концепцию. Итак, изменим предыдущий пример, применив операторы запросов LINQ:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };
 
// создаем новый список для результатов
var selectedPeople = from p in people // передаем каждый элемент из people в переменную p
                    where p.ToUpper().StartsWith("T") //фильтрация по критерию
                    orderby p  // упорядочиваем по возрастанию
                    select p; // выбираем объект в создаваемую коллекцию
 
foreach (string person in selectedPeople)
    Console.WriteLine(person);
```
Прежде всего, как мы видим, код стал меньше и проще, а результат будет тем же. В принципе все выражение можно было бы записать в одну строку:

```Csharp
var selectedPeople = from p in people where p.ToUpper().StartsWith("T") orderby p  select p;
```
Но для более понятной логической разбивки я поместил каждое отдельное подвыражение на отдельной строке.

Простейшее определение запроса LINQ выглядит следующим образом:

```sql
from переменная in набор_объектов
select переменная;
```

Итак, что делает этот запрос LINQ? Выражение from p in people проходит по всем элементам массива people и определяет каждый элемент как p. Используя переменную p мы можем проводить над ней разные операции.

Несмотря на то, что мы не указываем тип переменной p, выражения LINQ являются строго типизированными. То есть среда автоматически распознает, что набор people состоит из объектов string, поэтому переменная p будет рассматриваться в качестве строки.

Далее с помощью оператора where проводится фильтрация объектов, и если объект соответствует критерию (в данном случае начальная буква должна быть "T"), то этот объект передается дальше.

Оператор orderby упорядочивает по возрастанию, то есть сортирует выбранные объекты.

Оператор select передает выбранные значения в результирующую выборку, которая возвращается LINQ-выражением.

В данном случае результатом выражения LINQ является объект IEnumerable<T>. Нередко результирующая выборка определяется с помощью ключевого слова var, тогда компилятор на этапе компиляции сам выводит тип.

# Методы расширения LINQ
Кроме стандартного синтаксиса from .. in .. select для создания запроса LINQ мы можем применять специальные методы расширения, которые определены для интерфейса IEnumerable. Как правило, эти методы реализуют ту же функциональность, что и операторы LINQ типа where или orderby.

Например:

```Csharp
string[] people = { "Tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };
 
var selectedPeople = people.Where(p => p.ToUpper().StartsWith("T")).OrderBy(p => p);
 
foreach (string person in selectedPeople)
    Console.WriteLine(person);
```
Запрос people.Where(p=>p.ToUpper().StartsWith("T")).OrderBy(p => p) будет аналогичен предыдущему. Он состоит из цепочки методов Where и OrderBy. В качестве аргумента эти методы принимают делегат или лямбда-выражение.

И хотя ряд действий мы можем реализовать как с помощью операторов запросов LINQ, так и с помощью методов расширений LINQ, но не каждый метод расширения имеет аналог среди операторов LINQ. И в этом случае можно сочетать оба подхода. Например, используем стандартный синтаксис linq и метод расширения Count(), который возвращает количество элементов в выборке:

```Csharp
int number = (from p in people where p.ToUpper().StartsWith("T") select p).Count();
Console.WriteLine(number); // 3
```
Список используемых методов расширения LINQ
Select: определяет проекцию выбранных значений

- Where: определяет фильтр выборки

- OrderBy: упорядочивает элементы по возрастанию

- OrderByDescending: упорядочивает элементы по убыванию

- ThenBy: задает дополнительные критерии для упорядочивания элементов возрастанию

- ThenByDescending: задает дополнительные критерии для упорядочивания элементов по убыванию

- Join: соединяет две коллекции по определенному признаку

- Aggregate: применяет к элементам последовательности агрегатную функцию, которая сводит их к одному объекту

- GroupBy: группирует элементы по ключу

- ToLookup: группирует элементы по ключу, при этом все элементы добавляются в словарь

- GroupJoin: выполняет одновременно соединение коллекций и группировку элементов по ключу

- Reverse: располагает элементы в обратном порядке

- All: определяет, все ли элементы коллекции удовлятворяют определенному условию

- Any: определяет, удовлетворяет хотя бы один элемент коллекции определенному условию

- Contains: определяет, содержит ли коллекция определенный элемент

- Distinct: удаляет дублирующиеся элементы из коллекции

- Except: возвращает разность двух коллекцию, то есть те элементы, которые создаются только в одной коллекции

- Union: объединяет две однородные коллекции

- Intersect: возвращает пересечение двух коллекций, то есть те элементы, которые встречаются в обоих коллекциях

- Count: подсчитывает количество элементов коллекции, которые удовлетворяют определенному условию

- Sum: подсчитывает сумму числовых значений в коллекции

- Average: подсчитывает cреднее значение числовых значений в коллекции

- Min: находит минимальное значение

- Max: находит максимальное значение

- Take: выбирает определенное количество элементов

- Skip: пропускает определенное количество элементов

- TakeWhile: возвращает цепочку элементов последовательности, до тех пор, пока условие истинно

- SkipWhile: пропускает элементы в последовательности, пока они удовлетворяют заданному условию, и затем возвращает оставшиеся элементы

- Concat: объединяет две коллекции

- Zip: объединяет две коллекции в соответствии с определенным условием

- First: выбирает первый элемент коллекции

- FirstOrDefault: выбирает первый элемент коллекции или возвращает значение по умолчанию

- Single: выбирает единственный элемент коллекции, если коллекция содержит больше или меньше одного элемента, то генерируется исключение

- SingleOrDefault: выбирает единственный элемент коллекции. Если коллекция пуста, возвращает значение по умолчанию. Если в коллекции больше одного элемента, генерирует исключение

- ElementAt: выбирает элемент последовательности по определенному индексу

- ElementAtOrDefault: выбирает элемент коллекции по определенному индексу или возвращает значение по умолчанию, если индекс вне допустимого диапазона

- Last: выбирает последний элемент коллекции

- LastOrDefault: выбирает последний элемент коллекции или возвращает значение по умолчанию.


# Проекция данных

Проекция позволяет преобразовать объект одного типа в объект другого типа. Для проекции используется оператор select. Допустим, у нас есть набор объектов следующего класса, представляющего пользователя:

```Csharp
record class Person(string Name, int Age);
```
Но, допустим, нам нужен не весь объект, а только его свойство Name:

```Csharp
var people = new List<Person>
{
    new Person ("Tom", 23),
    new Person ("Bob", 27),
    new Person ("Sam", 29),
    new Person ("Alice", 24)
};
 
var names = from p in people select p.Name;
 
foreach (string n in names)
     Console.WriteLine(n);
```
Результат выражения LINQ будет представлять набор строк, поскольку выражение select p.Name выбирает в результирующую выборку только значения свойства Name.

```
Tom
Bob
Sam
Alice
```
В качестве альтернативы мы могли бы использовать метод расширения Select():

```Csharp
Select(Func<TSource,TResult> selector)
```
Этот метод принимает функцию преобразования в виде делегата Func<TSource,TResult>. Функция преобразования получает каждый объект выборки типа TSource и с его помощью создает объект TResult. Метод Select возвращает коллекцию преобразованных объектов.

Перепишем предыдущий пример с применением метода Select:

```Csharp
var people = new List<Person>
{
    new Person ("Tom", 23),
    new Person ("Bob", 27),
    new Person ("Sam", 29),
    new Person ("Alice", 24)
};
var names = people.Select(u => u.Name);
 
foreach (string n in names)
     Console.WriteLine(n);
```
Аналогично можно создать объекты другого типа, в том числе анонимного:

```Csharp
var people = new List<Person>
{
    new Person ("Tom", 23),
    new Person ("Bob", 27)
};
 
var personel = from p in people
            select new
            {
                FirstName = p.Name,
                Year = DateTime.Now.Year - p.Age
            };
 
foreach (var p in personel)
    Console.WriteLine($"{p.FirstName} - {p.Year}");
 
 
record class Person(string Name, int Age);
```
Здесь оператор select создает объект анонимного типа, используя текущий объект Person. И теперь результат будет содержать набор объектов данного анонимного типа, в котором определены два свойства: FirstName и Year (год рождения). Консольный вывод программы:

```
Tom - 1999
Bob - 1995
```
В качестве альтернативы мы могли бы использовать метод расширения Select():

```Csharp
// проекция на объекты анонимного типа
var personel = people.Select(p => new
{ 
    FirstName = p.Name, 
    Year = DateTime.Now.Year - p.Age 
});
```
# Переменые в запросах и оператор let
Иногда возникает необходимость произвести в запросах LINQ какие-то дополнительные промежуточные вычисления. Для этих целей мы можем задать в запросах свои переменные с помощью оператора let:

```Csharp
var people = new List<Person>
{
    new Person ("Tom", 23),
    new Person ("Bob", 27)
};
 
var personnel = from p in people
               let name = $"Mr. {p.Name}"
               let year = DateTime.Now.Year - p.Age
               select new
            {
                Name = name,
                Year = year
            };
 
foreach (var p in personnel)
    Console.WriteLine($"{p.Name} - {p.Year}");
 
record class Person(string Name, int Age);
```
В данном случае создаются две переменных. Переменная name, значение которой равно $"Mr. {p.Name}".

Возможность определения переменных наверное одно из главных преимуществ операторов LINQ по сравнению с методами расширения.

# Выборка из нескольких источников
В LINQ можно выбирать объекты не только из одного, но и из большего количества источников. Например, возьмем классы:

```Csharp
record class Course(string Title);  // учебный курс
record class Student(string Name);  // студент
```
Класс Course представляет учебный курс и хранит его название. Класс Student представляет студента и хранит его имя.

Допустим, нам надо из списка курсов и списка студентов получить набор пар студент-курс (условно говоря сущность, которая представляет учебу студента на данном курсе):

```Csharp
var courses = new List<Course> { new Course("C#"), new Course("Java") };
var students = new List<Student> { new Student("Tom"), new Student("Bob") };
 
var enrollments = from course in courses    //  выбираем по одному курсу
             from student in students       //  выбираем по одному студенту
             select new { Student = student.Name, Course = course.Title};   // соединяем каждого студента с каждым курсом
 
foreach (var enrollment in enrollments)
    Console.WriteLine($"{enrollment.Student} - {enrollment.Course}");
 
record class Course(string Title);  // учебный курс
record class Student(string Name);  // студент
```
Консольный вывод:

```
Tom - C#
Bob - C#
Tom - Java
Bob - Java
```
Таким образом, при выборке из двух источников каждый элемент из первого источника будет сопоставляться с каждым элементом из второго источника. То есть получиться 4 пары.

# SelectMany и сведение объектов
Метод SelectMany позволяет свести набор коллекций в одну коллекцию. Он имеет ряд перегруженных версий. Возьмем одну из них:

```Csharp
SelectMany(Func<TSource, IEnumerable<TResult>> selector);
SelectMany(Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource,TCollection,TResult> resultSelector);
```
Первая версия метода принимает функцию преобразования в виде делегата Func<TSource,IEnumerable<TResult>>. Функция преобразования получает каждый объект выборки типа TSource и с его помощью создает набор объектов TResult. Метод SelectMany возвращает коллекцию преобразованных объектов.

Вторая версия принимает функцию преобразования в виде делегата Func<TSource,IEnumerable<TResult>>. Функция преобразования получает каждый объект выборки типа TSource и возвращает некоторую промежуточную коллекцию типа TCollection. Второй параметр - то же функция функция преобразования в виде делегата Func<TSource,TCollection,TResult>, которая получает два параметра - каждый элемент текущей выборки и каждый элемент промежуточной коллекции и на их основе создает некоторый объект типа TResult.

Рассмотрим следующий пример:

```Csharp
var companies = new List<Company>
{
    new Company("Microsoft", new List<Person> {new Person("Tom"), new Person("Bob")}),
    new Company("Google", new List<Person> {new Person("Sam"), new Person("Mike")}),
};
var employees = companies.SelectMany(c => c.Staff);
 
foreach (var emp in employees)
    Console.WriteLine($"{emp.Name}");
 
record class Company(string Name, List<Person> Staff);
record class Person(string Name);
```
Здесь нам дан список компаний, в каждой компании имеет набор сотрудников в виде списка объектов Person. И на выходе мы получаем список сотрудников всех компаний, то есть по сути коллекцию объектов Person. Консольный вывод:

```
Tom
Bob
Sam
Mike
```
Аналогичный пример с помощью операторов LINQ:

```Csharp
var companies = new List<Company>
{
    new Company("Microsoft", new List<Person> {new Person("Tom"), new Person("Bob")}),
    new Company("Google", new List<Person> {new Person("Sam"), new Person("Mike")}),
};
var employees = from c in companies
                from emp in c.Staff
                select emp;
 
foreach (var emp in employees)
    Console.WriteLine($"{emp.Name}");
 
record class Company(string Name, List<Person> Staff);
record class Person(string Name);
```
Теперь добавим к сотрудникам их компанию:

```Csharp
var companies = new List<Company>
{
    new Company("Microsoft", new List<Person> {new Person("Tom"), new Person("Bob")}),
    new Company("Google", new List<Person> {new Person("Sam"), new Person("Mike")}),
};
 
var employees = companies.SelectMany(c => c.Staff,
                                    (c, emp)=> new { Name = emp.Name, Company = c.Name });
 
foreach (var emp in employees)
    Console.WriteLine($"{emp.Name} - {emp.Company}");
 
record class Company(string Name, List<Person> Staff);
record class Person(string Name);
```

Здесь применяется другая версия метода SelectMany. Первый делегат в виде c => c.Staff создает промежуточную коллекцию - фактически просто возвращаем набор сотрудников каждой компании. Второй делегат - (c, emp)=> new { Name = emp.Name, Company = c.Name } получает каждую компанию и каждый элемент промежуточной коллекции - объект Person и на их основе создает анонимный объект с двумя свойствами Name и Company. Консольный вывод программы:

```
Tom - Microsoft
Bob - Microsoft
Sam - Google
Mike - Google
```
Аналогичный пример с помощью операторов запросов:

```Csharp
var companies = new List<Company>
{
    new Company("Microsoft", new List<Person> {new Person("Tom"), new Person("Bob")}),
    new Company("Google", new List<Person> {new Person("Sam"), new Person("Mike")}),
};
var employees = from c in companies
             from emp in c.Staff
             select new { Name = emp.Name, Company = c.Name };
 
foreach (var emp in employees)
    Console.WriteLine($"{emp.Name} - {emp.Company}");
 
record class Company(string Name, List<Person> Staff);
record class Person(string Name);
```

# Фильтрация коллекции

Для выбора элементов из некоторого набора по условию используется метод Where:

```Csharp
Where<TSource> (Func<TSource,bool> predicate)
```
Этот метод принимает делегат Func<TSource,bool>, который в качестве параметра принимает каждый элемент последовательности и возвращает значение bool. Если элемент соответствует некоторому условию, то возвращается true, и тогда этот элемент передаетсяв коллекцию, которая возвращается из метода Where.

Например, выберем все строки, длина которых равна 3:

```Csharp
string[] people = { "Tom", "Alice", "Bob", "Sam", "Tim", "Tomas", "Bill" };
 
var selectedPeople = people.Where(p => p.Length == 3); // "Tom", "Bob", "Sam", "Tim"
 
foreach (string person in selectedPeople)
    Console.WriteLine(person);
```

Если выражение в методе Where для определенного элемента будет равно true (в данном случае выражение p.Length == 3), то данный элемент попадает в результирующую выборку.

Аналогичный запрос с помощью операторов LINQ:

```Csharp
string[] people = { "Tom", "Alice", "Bob", "Sam", "Tim", "Tomas", "Bill" };
 
var selectedPeople = from p in people
                     where p.Length == 3
                     select p;
```
Другой пример - выберем все четные элементы, которые больше 10.

Фильтрация с помощью операторов LINQ:

```Csharp
int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
// методы расширения
var evens1 = numbers.Where(i => i % 2 == 0 && i > 10);
// операторы запросов
var evens2 = from i in numbers
            where i%2==0 && i>10
            select i;
```
# Выборка сложных объектов
Допустим, у нас есть класс пользователя:

```Csharp
record class Person(string Name, int Age, List<string> Languages);
```
Свойство Name представляет имя, свойство Age - возраст пользователя, а список Languages - список языков, которыми владеет пользователь.

Создадим набор пользователей и выберем из них тех, которым больше 25 лет:

```Csharp
var people = new List<Person>
{
    new Person ("Tom", 23, new List<string> {"english", "german"}),
    new Person ("Bob", 27, new List<string> {"english", "french" }),
    new Person ("Sam", 29, new List<string>  { "english", "spanish" }),
    new Person ("Alice", 24, new List<string> {"spanish", "german" })
};
 
var selectedPeople = from p in people
                     where p.Age > 25
                     select p;
 
foreach (Person person in selectedPeople)
    Console.WriteLine($"{person.Name} - {person.Age}");
```
Консольный вывод:

```
Bob - 27
Sam - 29
```
Аналогичный запрос с помощью метода расширения Where:

```Csharp
var selectedPeople = people.Where(p=> p.Age > 25);
```
# Сложные фильтры
Теперь рассмотрим более сложные фильтры. Например, в классе пользователя есть список языков, которыми владеет пользователь. Что если нам надо отфильтровать пользователей по языку:

```Csharp
var selectedPeople = from person in people
                    from lang in person.Languages
                    where person.Age < 28
                    where lang == "english"
                    select person;
```

Результат:

```
Tom - 23
Bob - 27
```
Для создания аналогичного запроса с помощью методов расширения применяется метод SelectMany:

```Csharp
var selectedPeople = people.SelectMany(u => u.Languages,
                            (u, l) => new { Person = u, Lang = l })
                          .Where(u => u.Lang == "english" && u.Person.Age < 28)
                          .Select(u=>u.Person);
```
Метод SelectMany() в качестве первого параметра принимает последовательность, которую надо проецировать, а в качестве второго параметра - функцию преобразования, которая применяется к каждому элементу. На выходе она возвращает 8 пар "пользователь - язык" (new { Person = u, Lang = l }), к которым потом применяется фильтр с помощью Where.

# Фильтрация по типу данных
Дополнительный метод расширения - OfType() позволяет отфильтровать данные коллекции по определенному типу:

```Csharp
var people= new List<Person>
{
    new Student("Tom"),
    new Person("Sam"),
    new Student("Bob"),
    new Employee("Mike")
};
 
var students = people.OfType<Student>();
 
foreach (var student in students)
    Console.WriteLine(student.Name);
 
 
record class Person(string Name);
record class Student(string Name): Person(Name);
record class Employee(string Name) : Person(Name);
```

В данном случае список people содержит объекты трех типов - класса Person и производных типов Student и Employee. И в примере производится фильтрация данных типа Student - для этого метод OfType() типизируется типом Student. Консольный вывод:

```
Tom
Bob
```

# Сортировка

## Оператор orderby и метод OrderBy
Для сортировки набора данных в LINQ можно применять оператор orderby:

```Csharp
int[] numbers = { 3, 12, 4, 10};
var orderedNumbers = from i in numbers
                     orderby i
                     select i;
foreach (int i in orderedNumbers)
    Console.WriteLine(i);
```
Оператор orderby принимает критерий сортировки. В данном случае в качестве критерия выступает само число. Результат работы программы:

```
3
4
10
12
```
Если числа сортируются стандартным образом, как принято в математике, то строки сортируются исходя из алфавитного порядка:

```Csharp
string[] people = { "Tom", "Bob", "Sam" };
var orderedPeople = from p in people orderby p select p;
foreach (var p in orderedPeople)
    Console.WriteLine(p);       // Bob Sam Tom
```
Вместо оператора orderby можно применять метод расширения OrderBy():

```Csharp
OrderBy (Func<TSource,TKey> keySelector)
OrderBy (Func<TSource,TKey> keySelector, IComparer<TKey>? comparer);
```
Первая версия метода получает делегат, который через параметр получает элемент коллекции и который возвращает значение, применяемое для сортировки. Вторая версия позволяет также задать принцип сортировки через реализацию интерфейса IComparer.

Перепишем предыдущие два примера с помощью метода OrderBy:

```Csharp
int[] numbers = { 3, 12, 4, 10 };
var orderedNumbers = numbers.OrderBy(n=>n);
foreach (int i in orderedNumbers)
    Console.WriteLine(i);
 
string[] people = { "Tom", "Bob", "Sam" };
var orderedPeople = people.OrderBy(p=>p);
foreach (var p in orderedPeople)
    Console.WriteLine(p);
```
# Сортировка сложных объектов
Возьмем посложнее пример. Допустим, надо отсортировать выборку сложных объектов. Тогда в качестве критерия мы можем указать свойство класса объекта:

```Csharp
var people = new List<Person>
{
    new Person("Tom", 37),
    new Person("Sam", 28),
    new Person("Tom", 22),
    new Person("Bob", 41),
};
// с помощью оператора orderby
var sortedPeople1 = from p in people
                  orderby p.Name
                  select p;
 
foreach (var p in sortedPeople1)
    Console.WriteLine($"{p.Name} - {p.Age}");
 
// с помощью метода OrderBy
var sortedPeople2 = people.OrderBy(p => p.Name);
 
foreach (var p in sortedPeople2)
    Console.WriteLine($"{p.Name} - {p.Age}");
 
record class Person(string Name, int Age);
```

# Сортировка по возрастанию и убыванию
По умолчанию оператор orderby и метод OrderBy производят сортировку по возрастанию. С помощью ключевых слов ascending (сортировка по возрастанию) и descending (сортировка по убыванию) для оператора orderby можно явным образом указать направление сортировки. Например, отсортируем массив чисел по убыванию:

```Csharp
int[] numbers = { 3, 12, 4, 10 };
var orderedNumbers = from i in numbers
                     orderby i descending
                     select i;
foreach (int i in orderedNumbers)
    Console.WriteLine(i);   // 12 10 4 3
```
Для сортировки по убыванию можно применять метод OrderByDescending(), который работает аналогично OrderBy за исключением направления сортировки:

```Csharp
int[] numbers = { 3, 12, 4, 10 };
var orderedNumbers = numbers.OrderByDescending(n => n);
foreach (int i in orderedNumbers)
    Console.WriteLine(i);   // 12 10 4 3
```

# Множественные критерии сортировки
В наборах сложных объектов иногда встает ситуация, когда надо отсортировать не по одному, а сразу по нескольким полям. Для этого в запросе LINQ все критерии указываются в порядке приоритета через запятую:

```Csharp
var people = new List<Person>
{
    new Person("Tom", 37),
    new Person("Sam", 28),
    new Person("Tom", 22),
    new Person("Bob", 41),
};
// с помощью оператора orderby
var sortedPeople1 = from p in people
                  orderby p.Name, p.Age
                  select p;
 
foreach (var p in sortedPeople1)
    Console.WriteLine($"{p.Name} - {p.Age}");
```
Результат программы:

```
Bob - 41
Sam - 28
Tom - 22
Tom - 37
```
Для разных критериев сортировки можно установить направление:

```Csharp
// с помощью оператора orderby
var sortedPeople1 = from p in people
                  orderby p.Name, p.Age descending  // сортировка по возрасту по убыванию
                  select p;
```
С помощью методов расширения то же самое можно сделать через метод ThenBy()(для сортировки по возрастанию) и ThenByDescending() (для сортировки по убыванию):

```Csharp
var sortedPeople2 = people.OrderBy(p => p.Name).ThenByDescending(p=>p.Age);
```
Результат будет аналогичен предыдущему.

# Переопределение критерия сортировки
С помощью реализации IComparer мы можем переопределить критерии сортировки, если они нас не устраивают. Например, строки по умолчанию сортируются в алфавитном порядке. Но что, если мы хотим сортировать строки исходя из их длины? Решим данную задачу:

```Csharp
string[] people = new[]{"Kate", "Tom", "Sam", "Mike", "Alice"};
var sortedPeople = people.OrderBy(p => p, new CustomStringComparer());
 
foreach (var p in sortedPeople)
    Console.WriteLine(p);
 
// сравнение по длине строки
class CustomStringComparer : IComparer<String>
{
    public int Compare(string? x, string? y)
    {
        int xLength = x?.Length ?? 0; // если x равно null, то длина 0
        int yLength = y?.Length ?? 0;
        return xLength - yLength;
    }
}
```

Интерфейс IComparer типизируется типов сортируемых данных (в данном случае типом String). Для реализации этого интерфейса необходимо определить метод Compare. Он возвращает число: если первый параметр больше второго, то число больше 0, если меньше - то число меньше 0. Если оба параметра равны, то возвращается 0.

В данном случае, если параметр равен null, будем считать что длина строки равна 0. И с помощью разницы длин строк из обоих параметров определяем, какой из них больше.

Результат программы:

```
Tom
Sam
Kate
Mike
Alice
```

# Объединение, пересечение и разность коллекций 

LINQ предоставляет несколько методов для работы с коллекциями как с множествами, а именно находить их разность, объединение и пересечение.

# Разность последовательностей
С помощью метода Except() можно получить разность двух последовательностей:

```Csharp
string[] soft = { "Microsoft", "Google", "Apple"};
string[] hard = { "Apple", "IBM", "Samsung"};
 
// разность последовательностей
var result = soft.Except(hard);
 
foreach (string s in result)
    Console.WriteLine(s);
```
В данном случае из массива soft убираются все элементы, которые есть в массиве hard. Результатом операции будут два элемента:

```
Microsoft
Google
```
# Пересечение последовательностей
Для получения пересечения последовательностей, то есть общих для обоих наборов элементов, применяется метод Intersect:

```Csharp
string[] soft = { "Microsoft", "Google", "Apple"};
string[] hard = { "Apple", "IBM", "Samsung"};
 
// пересечение последовательностей
var result = soft.Intersect(hard);
 
foreach (string s in result)
    Console.WriteLine(s);
```
Так как оба набора имеют только один общий элемент, то соответственно только он и попадет в результирующую выборку:

```
Apple
```

# Удаление дубликатов
Для удаления дублей в наборе используется метод Distinct:

```Csharp
string[] soft = { "Microsoft", "Google", "Apple", "Microsoft", "Google" };
 
// удаление дублей
var result = soft.Distinct();
 
foreach (string s in result)
    Console.WriteLine(s);
```

```
Microsoft
Google
Apple
```

# Объединение последовательностей
Для объединения двух последовательностей используется метод Union. Его результатом является новый набор, в котором имеются элементы, как из первой, так и из второй последовательности. Повторяющиеся элементы добавляются в результат только один раз:

```Csharp
string[] soft = { "Microsoft", "Google", "Apple"};
string[] hard = { "Apple", "IBM", "Samsung"};
 
// объединение последовательностей
var result = soft.Union(hard);
 
foreach (string s in result)
    Console.WriteLine(s);
```
Результатом операции будет следующий набор:

```
Microsoft
Google
Apple
IBM
Samsung
```
Если же нам нужно простое объединение двух наборов, то мы можем использовать метод Concat:

```
var result = soft.Concat(hard);
```
В этом случае те элементы, которые встречаются в обоих наборах, дублируются в резутирующей последовательности.

Последовательное применение методов Concat и Distinct будет подобно действию метода Union.

# Работа со сложными объектами
Для сравнения объектов в последовательностях применяются реализации методов GetHeshCode() и Equals(). Поэтому если мы хотим работать с последовательностями, которые содержат объекты своих классов и структур, то нам необходимо определить для них подобные методы:

```Csharp
Person[] students = { new Person("Tom"), new Person("Bob"), new Person("Sam") };
Person[] employees = { new Person("Tom"), new Person("Bob"), new Person("Mike") };
 
// объединение последовательностей
var people = students.Union(employees);
 
foreach (Person person in people)
    Console.WriteLine(person.Name);
 
 
class Person
{
    public string Name { get;}
    public Person(string name) => Name = name;
 
    public override bool Equals(object? obj)
    {
        if (obj is Person person) return Name == person.Name;
        return false;
    }
    public override int GetHashCode() => Name.GetHashCode();
}
```
Здесь объекты Person сравниваются исходя из значения их свойства Name - если имена равны, то и объекты Person равны. Консольный вывод:

```
Tom
Bob
Sam
Mike
```
# Агрегатные операции

К агрегатным операциям относят различные операции над выборкой, например, получение числа элементов, получение минимального, максимального и среднего значения в выборке, а также суммирование значений.

# Метод Aggregate
Метод Aggregate выполняет общую агрегацию элементов коллекции в зависимости от указанного выражения. Например:

```Csharp
int[] numbers = { 1, 2, 3, 4, 5};
 
int query = numbers.Aggregate((x,y)=> x - y);
Console.WriteLine(query);   // -13
```
Переменная query будет представлять результат агрегации массива. В качестве условия агрегации используется выражение (x,y)=> x - y, то есть вначале из первого элемента вычитается второй, потом из получившегося значения вычитается третий и так далее. То есть будет эквивалентно выражению:

```Csharp
int query = 1 - 2 - 3 - 4 - 5
```
В итоге мы получим число -13. Соответственно мы бы могли использовать любые другие операции, например, сложение:

```Csharp
int query = numbers.Aggregate((x,y)=> x + y); // аналогично 1 + 2 + 3 + 4 + 5
```
Еще одна версия метода позволяет задать начальное значение, с которого начинается цепь агрегатных операций:

```Csharp
string[] words = { "Gaudeamus", "igitur", "Juvenes", "dum", "sumus" };
var sentence = words.Aggregate("Text:", (first, next) => $"{first} {next}");
 
Console.WriteLine(sentence);  // Text: Gaudeamus igitur Juvenes dum sumus
```
В данном случае объединяются все элементы массива words, но первым элемент агрегатной операции будет строка "Text:".

# Получение размера выборки. Метод Count
Для получения числа элементов в выборке используется метод Count():

```Csharp
int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
int size = numbers.Count();  // 10
Console.WriteLine(size);
```
Метод Count() в одной из версий также может принимать лямбда-выражение, которое устанавливает условие выборки. Поэтому мы можем в данном случае не использовать выражение Where:

```Csharp
int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
//  количество четных чисел, которые больше 10
int size = numbers.Count(i => i % 2 == 0 && i > 10);
Console.WriteLine(size);    // 3
```

# Получение суммы
Для получения суммы значений применяется метод Sum:

```Csharp
int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
 
int sum = numbers.Sum();
Console.WriteLine(sum);     // 340
```

Метод Sum() имеет ряд перегрузок. В частности, если у нас набор сложных объектов, как в примере выше, то мы можем указать свойство, значения которого будут суммироваться:

```Csharp
Person[] people = { new Person("Tom", 37), new Person("Sam", 28), new Person("Bob", 41) };
 
int ageSum = people.Sum(p => p.Age);
Console.WriteLine(ageSum);     // 106
 
record class Person(string Name, int Age);
```

В данном случае вычисляется сумма значений свойств Age объектов Person из массива people.

# Максимальное, минимальное и среднее значения
Для нахождения минимального значения применяется метод Min(), для получения максимального - метод Max(), а для нахождения среднего значения - метод Average(). Их действие похоже на методы Sum и Count:

```Csharp
int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
 
int min = numbers.Min();
int max = numbers.Max();
double average = numbers.Average();
 
Console.WriteLine($"Min: {min}");           // Min: 1
Console.WriteLine($"Max: {max}");           // Max: 88
Console.WriteLine($"Average: {average}");   // Average: 34
```

Если мы работаем со сложными объектами, то в эти методы передается делегат, который принимает свойство, применяемое в вычислениях:

```Csharp
Person[] people = { new Person("Tom", 37), new Person("Sam", 28), new Person("Bob", 41) };
 
int minAge = people.Min(p => p.Age); // минимальный возраст
int maxAge = people.Max(p => p.Age); // максимальный возраст
double averageAge = people.Average(p => p.Age); //средний возраст
 
Console.WriteLine($"Min Age: {minAge}");           // Min Age: 28
Console.WriteLine($"Max Age: {maxAge}");           // Max Age: 41
Console.WriteLine($"Average Age: {averageAge}");   // Average Age: 35,33
 
record class Person(string Name, int Age);
```
В данном случае для вычислений применяется свойство Age, то есть вычисляется минимальный, максимальный и средний возраст.

# Методы Skip и Take

Ряд методов в LINQ позволяют получить часть коллекции, в частности, такие методы как Skip, Take, SkipWhile, TakeWhile.

## Skip
Метод Skip() пропускает определенное количество элементов. Количество пропускаемых элементов передается в качестве параметра в метод:

```Csharp
string[] people = { "Tom", "Sam", "Bob", "Mike", "Kate" };
// пропускаем первые два элемента
var result = people.Skip(2);    // "Bob", "Mike", "Kate"
 
foreach (var person in result)
    Console.WriteLine(person);
```
В данном случае пропускаем первые два элемента. Консольный вывод:

```
Bob
Mike
Kate
```
Если необходимо пропустить определенное количество элементов с конца коллекции, то применяется метод SkipLast():

```Csharp
string[] people = { "Tom", "Sam", "Bob", "Mike", "Kate" };
// пропускаем последние два элемента
var result = people.SkipLast(2);    // "Tom", "Sam", "Bob"
 
foreach (var person in result)
    Console.WriteLine(person);
```
В данном случае пропускаем последние два элемента. Консольный вывод:

```
Tom
Sam
Bob
```

# SkipWhile
Метод SkipWhile() пропускает цепочку элементов, начиная с первого элемента, пока они удовлетворяют определенному условию:

```Csharp
SkipWhile(Func<TSource, bool> predicate);
```
В метод передается делегат, который представляет условие, он получает каждый элемент коллекции и возвращает значение true, если элемент соответствует условию. Например:

```Csharp
string[] people = { "Tom", "Sam", "Mike", "Kate", "Bob" };
// пропускаем первые элементы, длина которых равна 3
var result = people.SkipWhile(p=> p.Length == 3);    // "Mike", "Kate", "Bob"
 
foreach (var person in result)
    Console.WriteLine(person);
```
Здесь метод SkipWhile пропускает элементы, длина которых равна 3 символам. Первые два элемента массива people ("Tom", "Sam") соответствуют этому условию и поэтому будут пропущены. На третьем элементе ("Mike") цепочка обрывается, поэтому последний элемент ("Bob"), длина которго тоже равна 3-м символам, не будет пропущен и будет включен в выходную коллекцию:

```
Mike
Kate
Bob
```

Если в массиве первый элемент имел бы длину больше или меньше 3 символов, то цепочка пропускаемых элементов прервалась бы уже на первом элементе, и поэтому метод SkipWhile возвратил бы все элементы массива.

## Take
Метод Take() извлекает определенное число элементов. Количество извлекаемых элементов передается в метод в качестве параметра. Например, извлечем три первых элемента:

```Csharp
string[] people = { "Tom", "Sam", "Mike", "Kate", "Bob" };
// извлекаем первые 3 элемента
var result = people.Take(3);    // "Tom", "Sam", "Mike"
 
foreach (var person in result)
    Console.WriteLine(person);
```
Метод TakeLast() извлекает определенное количество элементов с конце коллекции:

```Csharp
string[] people = { "Tom", "Sam", "Mike", "Kate", "Bob" };
// извлекаем последние 3 элемента
var result = people.TakeLast(3);    // "Mike", "Kate", "Bob"
 
foreach (var person in result)
    Console.WriteLine(person);
```
## TakeWhile
Метод TakeWhile() выбирает цепочку элементов, начиная с первого элемента, пока они удовлетворяют определенному условию:

```Csharp
TakeWhile(Func<TSource, bool> predicate);
```
В метод передается делегат, который представляет условие, он получает каждый элемент коллекции и возвращает значение true, если элемент соответствует условию. Например:

```Csharp
string[] people = { "Tom", "Sam", "Mike", "Kate", "Bob" };
// извлекаем первые элементы, длина которых равна 3
var result = people.TakeWhile(p=> p.Length == 3);    // "Tom", "Sam"
 
foreach (var person in result)
    Console.WriteLine(person);
```

Здесь метод TakeWhile выбирает элементы, длина которых равна 3 символам. Первые два элемента массива people ("Tom", "Sam") соответствуют этому условию и поэтому будут выбраны в выходную коллекцию. На третьем элементе ("Mike") цепочка обрывается, поэтому последний элемент ("Bob"), длина которго тоже равна 3-м символам, не будет включен в выходную коллекцию:

```
Tom
Sam
```
Если бы первый элемент в массиве имел бы длину больше или меньше 3 символов, то в этом случае метод TakeWhile возвратил бы нам 0 элементов.

## Постраничный вывод
Совмещая оба метода - Take и Skip, мы можем выбрать определенное количество элементов начиная с определенного элемента. Например, выберем два элемента, начиная со четвертого (то есть пропустим 3 первых элемента):

```Csharp
string[] people = { "Tom", "Sam", "Mike", "Kate", "Bob", "Alice" };
// пропускаем 3 элемента и выбираем 2 элемента
var result = people.Skip(3).Take(2);    // "Kate", "Bob"
 
foreach (var person in result)
    Console.WriteLine(person);
```

# Группировка

Для группировки данных по определенным параметрам применяется оператор group by и метод GroupBy().

## Оператор group by
Допустим, у нас есть набор из объектов следующего типа:

```Csharp
record class Person(string Name, string Company);
```
Данный класс представляет пользователя и имеет два свойства: Name (имя пользователя) и Company (компания, где работает пользователь). Сгруппируем набор пользователей по компании:

```Csharp
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
    new Person("Kate", "JetBrains"), new Person("Alice", "Microsoft"),
};
 
var companies = from person in people
                group person by person.Company;
 
foreach(var company in companies)
{
    Console.WriteLine(company.Key);
 
    foreach(var person in company)
    {
        Console.WriteLine(person.Name);
    }
    Console.WriteLine(); // для разделения между группами
}
 
record class Person(string Name, string Company);
```
Если в выражении LINQ последним оператором, выполняющим операции над выборкой, является group, то оператор select не применяется.

Оператор group принимает критерий по которому проводится группировка:

```Csharp
group person by person.Company
```
в данном случае группировка идет по свойству Company. Результатом оператора group является выборка, которая состоит из групп. Каждая группа представляет объект IGrouping<K, V>: параметр K указывает на тип ключа - тип свойства, по которому идет группировка (здесь это тип string). А параметр V представляет тип сгруппированных объектов - в данном случае группируем объекты Person.

Каждая группа имеет ключ, который мы можем получить через свойство Key: g.Key. Здесь это будет название компании.

Все элементы внутри группы можно получить с помощью дополнительной итерации. Элементы группы имеют тот же тип, что и тип объектов, которые передавались оператору group, то есть в данном случае объекты типа Person.

В итоге мы получим следующий вывод:

```
Microsoft
Tom
Mike
Alice

Google
Sam

JetBrains
Bob
Kate
```

## GroupBy
В качестве альтернативы можно использовать метод расширения GroupBy. Он имеет ряд перегрузок, возьмем самую простую из них:

```Csharp
GroupBy<TSource,TKey> (Func<TSource,TKey> keySelector);
```
Данная версия получает делегат, которые в качестве параметра принимает каждый элемент коллекции и возвращает критерий группировки.

Перепишем предыдущий пример с помощью метода GroupBy:

```Csharp
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
    new Person("Kate", "JetBrains"), new Person("Alice", "Microsoft"),
};
 
var companies = people.GroupBy(p => p.Company);
 
foreach(var company in companies)
{
    Console.WriteLine(company.Key);
 
    foreach(var person in company)
    {
        Console.WriteLine(person.Name);
    }
    Console.WriteLine(); // для разделения между группами
}
 
record class Person(string Name, string Company);
```

## Создание нового объекта при группировке
Теперь изменим запрос и создадим из группы новый объект:

```Csharp
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
    new Person("Kate", "JetBrains"), new Person("Alice", "Microsoft"),
};
 
var companies = from person in people
                group person by person.Company into g
                select new { Name = g.Key, Count = g.Count() }; ;
 
foreach(var company in companies)
{
    Console.WriteLine($"{company.Name} : {company.Count}");
}
 
record class Person(string Name, string Company);
```
Выражение

```Csharp
group person by person.Company into g
```
определяет переменную g, которая будет содержать группу. С помощью этой переменной мы можем затем создать новый объект анонимного типа (хотя также можно под данную задачу определить новый класс):

```Csharp
select new { Name = g.Key, Count = g.Count() }
```
Теперь результат запроса LINQ будет представлять набор объектов таких анонимных типов, у которых два свойства Name и Count.

Результат программы:

```
Microsoft : 3
Google : 1
JetBrains : 2
```
Аналогичная операция с помощью метода GroupBy():

```Csharp
var companies = people
                    .GroupBy(p=>p.Company)
                    .Select(g => new { Name = g.Key, Count = g.Count() });
```

## Вложенные запросы
Также мы можем осуществлять вложенные запросы:

```Csharp
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
    new Person("Kate", "JetBrains"), new Person("Alice", "Microsoft"),
};
var companies = from person in people
                group person by person.Company into g
                select new
                {
                    Name = g.Key,
                    Count = g.Count(),
                    Employees = from p in g select p
                };
 
foreach (var company in companies)
{
    Console.WriteLine($"{company.Name} : {company.Count}");
    foreach(var employee in company.Employees)
    {
        Console.WriteLine(employee.Name);
    }
    Console.WriteLine(); // для разделения компаний
}
 
record class Person(string Name, string Company);
```
Здесь свойство Employees каждой группы формируется с помощью дополнительного запроса, который выбирает всех пользователей в этой группе. Консольный вывод программы:

```
Microsoft : 3
Tom
Mike
Alice

Google : 1
Sam

JetBrains : 2
Bob
Kate
```
Аналогичный запрос с помощью метода GroupBy:

```Csharp
var companies = people
                    .GroupBy(p=>p.Company)
                    .Select(g => new
                    {
                        Name = g.Key,
                        Count = g.Count(),
                        Employees = g.Select(p=> p) 
                    });
```

# Соединение коллекций

Соединение в LINQ используется для объединения двух разнотипных наборов в один. Для соединения используется оператор join или метод Join(). Как правило, данная операция применяется к двум наборам, которые имеют один общий критерий.

## Оператор join
Оператор join имеет следующий формальный синтаксис:

```
from объект1 in набор1
join объект2 in набор2 on объект2.свойство2 equals объект1.свойство1
```
После оператора join идет выборка объектов из второй коллекции. После оператора on указывается критерий соединения - свойство объекта из второй выборки, а после оператора equals - свойство объекта из первой выборки, которому должно быть равно свойство объекта из второй выборки. Если эти свойства равны, то оба объекта попадают в финальный результат.

Например, у нас есть два класса:

```Csharp
record class Person(string Name, string Company);
record class Company(string Title, string Language);
```
Класс Person представляет пользователя и хранит два свойства: Name (имя) и Company (компания пользователя). Класс Company представляет компанию и хранит два свойства: Title (название компании) и Language (основной язык программирования в компании)

Объекты обоих классов будет иметь один общий критерий - название компании. Соединим по этому критерию два набора этих классов:

```Csharp
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
};
Company[] companies =
{
    new Company("Microsoft", "C#"),
    new Company("Google", "Go"),
    new Company("Oracle", "Java")
};
var employees = from p in people
             join c in companies on p.Company equals c.Title
             select new { Name = p.Name, Company = c.Title, Language = c.Language };
 
foreach (var emp in employees)
    Console.WriteLine($"{emp.Name} - {emp.Company} ({emp.Language})");
 
record class Person(string Name, string Company);
record class Company(string Title, string Language);
```

С помощью выражения

```Csharp
join c in companies on p.Company equals c.Title
```
объект p из списка people (то есть объект Person) соединяется с объектом c из списка companies (то есть с объектом Company), если значение свойства p.Company совпадает со значением свойства c.Title. Результатом соединения будет объект анонимного типа, который будет содержать три свойства. В итоге мы получим следующий вывод:

```
Tom - Microsoft (C#)
Sam - Google (Go)
Mike - Microsoft (C#)
```
Обратите внимание, что в массиве people есть объект new Person("Bob", "JetBrains"), но в массиве компаний компании с именем "JetBrains" нет, соответственно он не попал с результат. Аналогично в списке people нет объектов Person, которые бы соотствовали компании new Company("Oracle", "Java").

# Метод Join
В качестве альтернативы можно было бы использовать метод Join():

```Csharp
Join(IEnumerable<TInner> inner, 
    Func<TOuter,TKey> outerKeySelector, 
    Func<TInner,TKey> innerKeySelector, 
    Func<TOuter,TInner,TResult> resultSelector);
```

Метод Join() принимает четыре параметра:

- второй список, который соединяем с текущим

- делегат, который определяет свойство объекта из текущего списка, по которому идет соединение

- делегат, который определяет свойство объекта из второго списка, по которому идет соединение

- делегат, который определяет новый объект в результате соединения

Перепишем предыдущий пример с использованием метода Join:

```Csharp
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
};
Company[] companies =
{
    new Company("Microsoft", "C#"),
    new Company("Google", "Go"),
    new Company("Oracle", "Java")
};
var employees = people.Join(companies, // второй набор
             p => p.Company, // свойство-селектор объекта из первого набора
             c => c.Title, // свойство-селектор объекта из второго набора
             (p, c) => new { Name = p.Name, Company = c.Title, Language = c.Language }); // результат
 
foreach (var emp in employees)
    Console.WriteLine($"{emp.Name} - {emp.Company} ({emp.Language})");
 
record class Person(string Name, string Company);
record class Company(string Title, string Language);
```

## GroupJoin
Метод GroupJoin() кроме соединения последовательностей также выполняет и группировку.

```Csharp
GroupJoin(IEnumerable<TInner> inner, 
        Func<TOuter,TKey> outerKeySelector, 
        Func<TInner,TKey> innerKeySelector, 
        Func<TOuter, IEnumerable<TInner>,TResult> resultSelector);
```
Метод GroupJoin() принимает четыре параметра:

- второй список, который соединяем с текущим

- делегат, который определяет свойство объекта из текущей коллекции, по которому идет соединение и по которому будет идти группировка

- делегат, который определяет свойство объекта из второй коллекции, по которому идет соединение

- делегат, который определяет новый объект в результате соединения. Этот делегат получает группу - объект текущей коллекции, по которому шла группировка, и набор объектов из второй коллекции, которые сооставляют группу

Например, возьмем выше определенные массивы people и companies и сгуппируем всех пользователей по компаниям:

```Csharp
Person[] people =
{
    new Person("Tom", "Microsoft"), new Person("Sam", "Google"),
    new Person("Bob", "JetBrains"), new Person("Mike", "Microsoft"),
};
Company[] companies =
{
    new Company("Microsoft", "C#"),
    new Company("Google", "Go"),
    new Company("Oracle", "Java")
};
var personnel = companies.GroupJoin(people, // второй набор
             c => c.Title, // свойство-селектор объекта из первого набора
             p => p.Company, // свойство-селектор объекта из второго набора
             (c, employees) => new   // результат
             {
                 Title = c.Title,
                 Employees = employees
            });
 
foreach (var company in personnel)
{
    Console.WriteLine(company.Title);
    foreach(var emp in company.Employees)
    {
        Console.WriteLine(emp.Name);
    }
    Console.WriteLine();
}
 
record class Person(string Name, string Company);
record class Company(string Title, string Language);
```
Результатом выполнения программы будет следующий вывод:

```
Microsoft
Tom
Mike

Google
Sam

Oracle
```
Метод GroupJoin, также как и метод Join, принимает все те же параметры. Только теперь в последний параметр - делегат передаются объект компании и набор пользователей этой компании.

Обратите внимание, что для компании "Oracle" в массиве people нет пользователей, хотя для нее также создается группа.

Аналогичного результата можно добитьс и с помощью оператора join:

```Csharp
var personnel = from c in companies
                join p in people on c.Title equals p.Company into g
                select new   // результат
                {
                    Title = c.Title,
                    Employees = g
                };
```

## Метод Zip
Метод Zip() последовательно объединяет соответствующие элементы текущей последовательности со второй последовательностью, которая передается в метод в качестве параметра. То есть первый элемент из первой последовательности объединяется с первым элементом из второй последовательности, второй элемент из первой последовательности соединяется со вторым элементом из второй последовательности и так далее. Результатом метода является коллекция кортежей, где каждый кортеж хранит пару соответствующих элементов из обоих последовательностей:

```Csharp
var courses = new List<Course> { new Course("C#"), new Course("Java") };
var students = new List<Student> { new Student("Tom"), new Student("Bob") };
 
var enrollments = courses.Zip(students);
 
foreach (var enrollment in enrollments)
    Console.WriteLine($"{enrollment.First} - {enrollment.Second}");
 
record class Course(string Title);  // учебный курс
record class Student(string Name);  // студент
```

Здесь метод Zip объединяет соответствующие элементы из списков courses и students. В результате создается новая коллекция, которая хранит набор кортежей. Каждый кортеж в ней имеет два элемента. Первый элемент из свойства First представляет объект текущей коллекции (в данном случае объект Course), а второй элемент (в свойстве Second) хранит объект второй последовательности (в данном случае объект Student). Консольный вывод:

```
Course { Title = C# } - Student { Name = Tom }
Course { Title = Java } - Student { Name = Bob }
```

# Проверка наличия и получение элементов

Ряд методов в LINQ позволяют проверить наличие элементов в коллекции и получить их.

## All
Метод All() проверяет, соответствуют ли все элементы условию. Если все элементы соответствуют условию, то возвращается true. Например:

```Csharp
string[] people = { "Tom", "Tim", "Bob", "Sam" };
 
// проверяем, все ли элементы имеют длину в 3 символа
bool allHas3Chars = people.All(s => s.Length == 3);     // true
Console.WriteLine(allHas3Chars);
 
// проверяем, все ли строки начинаются на T
bool allStartsWithT = people.All(s => s.StartsWith("T"));   // false
Console.WriteLine(allStartsWithT);
```
В первом случае проверяем, все ли строки в массиве people имеют длину в три символа. Поскольку это условие верно, то метод All возвращает true. Во втором случае смотрим, все ли строки начинаются с буквы "T". это условие ложно, поэтому метод All возвращает false.

## Any
Метод Any() действует подобным образом, только возвращает true, если хотя бы один элемент коллекции определенному условию:

```Csharp
string[] people = { "Tom", "Tim", "Bob", "Sam" };
 
// проверяем, все ли элементы имеют длину больше 3 символов
bool allHasMore3Chars = people.Any(s => s.Length > 3);     // false
Console.WriteLine(allHasMore3Chars);
 
// проверяем, все ли строки начинаются на T
bool allStartsWithT = people.Any(s => s.StartsWith("T"));   // true
Console.WriteLine(allStartsWithT);
```
Первое выражение вернет false, поскольку все строки имеют длину в 3 символа. Второе выражение возвратит true, так как у нас есть в коллекции есть строки, которые начинаются на букву T.

## Contains
Метод Contains() возвращает true, если коллекция содержит определенный элемент.

```Csharp
string[] people = { "Tom", "Tim", "Bob", "Sam" };
 
// проверяем, есть ли строка Tom
bool hasTom = people.Contains("Tom");     // true
Console.WriteLine(hasTom);
 
// проверяем, есть ли строка Mike
bool hasMike = people.Contains("Mike");     // false
Console.WriteLine(hasMike);
```
Стоит отметить, что для сравнения объектов применяется реализация метода Equals. Соответственно если мы работаем с объектами своих типов, то мы можем реализовать данный метод:

```Csharp
Person[] people = { new Person("Tom"), new Person("Sam"), new Person("Bob")};
 
var tom = new Person("Tom");
var mike = new Person("Mike");
 
// проверяем, есть ли Tom
bool hasTom = people.Contains(tom);     // true
Console.WriteLine(hasTom);
 
// проверяем, есть ли строка Mike
bool hasMike = people.Contains(mike);     // false
Console.WriteLine(hasMike);
 
class Person
{
    public string Name { get;}
    public Person(string name) => Name = name;
 
    public override bool Equals(object? obj)
    {
        if (obj is Person person) return Name == person.Name;
        return false;
    }
    public override int GetHashCode() => Name.GetHashCode();
}
```
Но стоит отметить, что Contains не всегда может вернуть ожидаемые данные. Например:

```Csharp
string[] people = { "tom", "Tim", "bOb", "Sam" };
 
// проверяем, есть ли строка Tom
bool hasTom = people.Contains("Tom");     // false
Console.WriteLine(hasTom);
 
// проверяем, есть ли строка Bob
bool hasMike = people.Contains("Bob");     // false
Console.WriteLine(hasMike);
```
В данном случае в массиве нет строки "Tom", а есть строка "tom". Поэтому вызов people.Contains("Tom") возвратит false. Но подобное поведение не всегда может быть желательным. И в этом случае мы можем задать логику сравнения с помощью реализации интерфейса IComparer и затем передать ее в качестве второго параметра в метод Contains:

```Csharp
using System.Diagnostics.CodeAnalysis;
 
string[] people = { "tom", "Tim", "bOb", "Sam" };
 
// проверяем, есть ли строка Tom
bool hasTom = people.Contains("Tom", new CustomStringComparer());     // true
Console.WriteLine(hasTom);
 
// проверяем, есть ли строка Bob
bool hasMike = people.Contains("Bob", new CustomStringComparer());     // true
Console.WriteLine(hasMike);
 
class CustomStringComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        if (x is null || y is null) return false;
        return x.ToLower() == y.ToLower();
 
    }
 
    public int GetHashCode(string obj) => obj.ToLower().GetHashCode();
}
```
Интерфейс IEqualityComparer типизируется типом сравниваемых данных (в данном случае типом String). Для реализации этого интерфейса необходимо определить методы Equals и GetHashCode. В методе Equals сравниваем строки в нижнем регистре, а в методе GetHashCode возвращаем возвращаем хеш-код строки в нижнем регистре.

## First/FirstOrdefault
Метод First() возвращает первый элемент последовательности:

```Csharp
string[] people = { "Tom", "Bob", "Tim", "Sam" };
 
// проверяем, есть ли строка Tom
var first = people.First();  // Tom
Console.WriteLine(first);
```
Также в метод First можно передать метод, который представляет условие. В этом случае метод возвращает первый элемент, который соответствует условию:

```Csharp
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
 
// первая строка, длина которой равна 4 символам
var firstWith4Chars = people.First(s=> s.Length == 4);  // Kate
Console.WriteLine(firstWith4Chars);
```
Здесь выбираем первый элемент, длина которого - 4 символа.

Стоит учитывать, что если коллекция пуста или в коллекции нет элементов, который соответствуют условию, то будет сгенерировано исключение.

```Csharp
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
 
// первая строка, длина которой равна 5 символам
var firstWith5Chars = people.First(s => s.Length == 5);  // ! исключение
Console.WriteLine(firstWith5Chars);
 
var first = new string[] {}.First();  // ! исключение
Console.WriteLine(first);
```

## FirstOrdefault
Метод FirstOrDefault() также возвращает первый элемент и также может принимать условие, только если коллекция пуста или в коллекции не окажется элементов, которые соответствуют условию, то метод возвращает значение по умолчанию:

```Csharp
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
 
// первый элемент
var first = people.FirstOrDefault();  // Tom
Console.WriteLine(first);
 
// первая строка, длина которой равна 4 символам
var firstWith4Chars = people.FirstOrDefault(s => s.Length == 4);  // Kate
Console.WriteLine(firstWith4Chars);
 
// первый элемент из пустой коллекции
var firstOrDefault = new string[] {}.FirstOrDefault();
Console.WriteLine(firstOrDefault);  // null
```

Стоит учитывать, что для коллекций ссылочных типов FirstOrDefault возвращает значение типа T? (в примере выше - string?), то есть значение, которое может быть равно null, а значение по умолчанию - null. Для коллекций числовых типов возвращается непосредственно значение типа T, а значение по умолчанию - 0.

Но мы можем настроить значение по умолчанию, передав его в качестве одного из аргументов:

```Csharp
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
 
string? firstWith5Chars = people.FirstOrDefault(s => s.Length == 5, "Undefined");
Console.WriteLine(firstWith5Chars); // Undefined
 
// первый элемент из пустой коллекции строк
string? firstOrDefault = new string[] {}.FirstOrDefault("hello"); // hello - значение по умолчанию
Console.WriteLine(firstOrDefault);  // hello
 
// первый элемент из пустой коллекции int
int firstNumber = new int[] {}.FirstOrDefault(100); // 100 - значение по умолчанию
Console.WriteLine(firstNumber); // 100
```
## Last и LastOrDefault
Метод Last() аналогичен по работе методу First, только возвращает последний элемент. Если коллекция не содержит элемент, который соответствуют условию, или вообще пуста, то метод генерирует исключение.

```Csharp
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
 
string last = people.Last();
Console.WriteLine(last); // Sam
 
string lastWith4Chars = people.Last(s => s.Length == 4);
Console.WriteLine(lastWith4Chars); // Mike
```
Метод LastOrDefault() возвращает последний элемент или значение по умолчанию, если коллекция не содержит элемент, который соответствуют условию, или вообще пуста:

```Csharp
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
 
string? last = people.LastOrDefault();
Console.WriteLine(last); // Sam
 
string? lastWith4Chars = people.LastOrDefault(s => s.Length == 4);
Console.WriteLine(lastWith4Chars); // Mike
 
string? lastWith5Chars = people.LastOrDefault(s => s.Length == 5);
Console.WriteLine(lastWith5Chars); // null
 
string? lastWith5CharsOrDefault = people.LastOrDefault(s => s.Length == 5, "Undefined");
Console.WriteLine(lastWith5CharsOrDefault); // Undefined
 
// первый элемент из пустой коллекции строк
string? lastOrDefault = new string[] {}.LastOrDefault("hello");
Console.WriteLine(lastOrDefault);  // hello
```

# Отложенное и немедленное выполнение LINQ

Есть два способа выполнения запроса LINQ: отложенное (deferred) и немедленное (immediate) выполнение.

При отложенном выполнении LINQ-выражение не выполняется, пока не будет произведена итерация или перебор по выборке, например, в цикле foreach. Обычно подобные операции возвращают объект IEnumerable<T> или IOrderedEnumerable<T>. Полный список отложенных операций LINQ:

- AsEnumerable

- Cast

- Concat

- DefaultIfEmpty

- Distinct

- Except

- GroupBy

- GroupJoin

- Intersect

- Join

- OfType

- OrderBy

- OrderByDescending

- Range

- Repeat

- Reverse

- Select

- SelectMany

- Skip

- SkipWhile

- Take

- TakeWhile

- ThenBy

- ThenByDescending

- Union

- Where

Рассмотрим отложенное выполнение:

```Csharp
string[] people = { "Tom", "Sam", "Bob" };
 
var selectedPeople = people.Where(s=>s.Length == 3).OrderBy(s=>s);
 
// выполнение LINQ-запроса
foreach (string s in selectedPeople)
    Console.WriteLine(s);
```

То есть фактическое выполнение запроса происходит не в строке определения: var selectedPeople = people.Where..., а при переборе в цикле foreach.

Фактически LINQ-запрос разбивается на три этапа:

1. Получение источника данных

2. Создание запроса

3. Выполнение запроса и получение его результатов

Как это происходит в нашем случае:

1. Получение источника данных - определение массива teams:

```Csharp
string[] people = { "Tom", "Sam", "Bob" };
```

2. Создание запроса - определение переменной selectedTeams:

```Csharp
var selectedPeople = people.Where(s=>s.Length == 3).OrderBy(s=>s);
```

3. Выполнение запроса и получение его результатов:

```Csharp
foreach (string s in selectedPeople)
    Console.WriteLine(s);
```
После определения запроса он может выполняться множество раз. И до выполнения запроса источник данных может изменяться. Чтобы более наглядно увидеть это, мы можем изменить какой-либо элемент до перебора выборки:

```Csharp
string[] people = { "Tom", "Sam", "Bob" };
 
var selectedPeople = people.Where(s=>s.Length == 3).OrderBy(s=>s);
 
people[2] = "Mike";
// выполнение LINQ-запроса
foreach (string s in selectedPeople)
    Console.WriteLine(s);
```
Теперь выборка будет содержать два элемента, а не три, так как последний элемент после изменения не будет соответствовать условию.

Важно понимать, что переменная запроса сама по себе не выполняет никаких действий и не возвращает никаких данных. Она только хранит набор команд, которые необходимы для получения результатов. То есть выполнение запроса после его создания откладывается. Само получение результатов производится при переборе в цикле foreach.

## Немедленное выполнение запроса
С помощью ряда методов мы можем применить немедленное выполнение запроса. Это методы, которые возвращают одно атомарное значение или один элемент или данные типов Array, List и Dictionary. Полный список подобных операций в LINQ:

- Aggregate

- All

- Any

- Average

- Contains

- Count

- ElementAt

- ElementAtOrDefault

- Empty

- First

- FirstOrDefault

- Last

- LastOrDefault

- LongCount

- Max

- Min

- SequenceEqual

- Single

- SingleOrDefault

- Sum

- ToArray

- ToDictionary

- ToList

- ToLookup

Рассмотрим пример с методом Count(), который возвращает число элементов последовательности:

```Csharp
string[] people = { "Tom", "Sam", "Bob" };
// определение и выполнение LINQ-запроса
var count = people.Where(s=>s.Length == 3).OrderBy(s=>s).Count();
 
Console.WriteLine(count);   // 3 - до изменения коллекции
 
people[2] = "Mike";
Console.WriteLine(count);   // 3 - после изменения коллекции
```

Результатом метода Count будет объект int, поэтому сработает немедленное выполнение.

Сначала создается запрос: people.Where(s=>s.Length == 3).OrderBy(s=>s). Далее к нему применяется метод Count(), который выполняет запрос, неявно выполняет перебор по последовательности элементов, генерируемой этим запросом, и возвращает число элементов в этой последовательности.

Также мы можем изменить код таким образом, чтобы метод Count() учитывал изменения и выполнялся отдельно от определения запроса:

```Csharp
string[] people = { "Tom", "Sam", "Bob" };
// определение LINQ-запроса
var selectedPeople = people.Where(s=>s.Length == 3).OrderBy(s=>s);
// выполнение запроса
Console.WriteLine(selectedPeople.Count());   // 3 - до изменения коллекции
 
people[2] = "Mike";
// выполнение запроса
Console.WriteLine(selectedPeople.Count());   // 2 - после изменения коллекции
```
Также для немедленного выполнения LINQ-запроса и кэширования его результатов мы можем применять методы преобразования ```ToArray<T>()```, ```ToList<T>()```, ToDictionary() и т.д.. Эти методы получают результат запроса в виде объектов Array, List и Dictionary соответственно. Например:

```Csharp
string[] people = { "Tom", "Sam", "Bob" };
 
// определение и выполнение LINQ-запроса
var selectedPeople = people.Where(s=>s.Length == 3).OrderBy(s=>s).ToList();
 
// изменение массива никак не затронет список selectedPeople
people[2] = "Mike";
 
// выполнение запроса
foreach (string s in selectedPeople)
    Console.WriteLine(s);
```


# Делегаты в запросах LINQ

Если мы обратимся к определению многих методов расширений LINQ, то увидим, что в качестве параметра многие из них принимают делегаты например, ```Func<TSource, bool>```, например, определение метода Where:

```Csharp
public static IEnumerable<TSource> Where<TSource>(
    this IEnumerable<TSource> source,
    Func<TSource, bool> predicate
)
```
Хотя, как правило, в качестве делегата в подобные методы удобно передавать лямбда-выражения. Но тем не менее мы также можем передать полноценные методы. Например:

```Csharp
string[] people = { "Tom", "Bob", "Kate", "Tim", "Mike", "Sam" };
 
var result = people.Where(LenghtIs3);
 
foreach (var person in result)
    Console.WriteLine(person);
 
bool LenghtIs3(string name) => name.Length == 3;
```
Здесь метод LenghtIs3 проверяет, равна ли длина строки 3 символам. Так как в данном случае набор элементов, к которому применяется метод Where, содержит объекты string, то в метод в качестве параметра передается объект этого типа. Возвращаемый тип должен представлять тип bool: если true, то объект string соответствует условию и попадает в выходную коллекцию.

Рассмотрим другой пример. Пусть метод Select() применяется к коллекции целых чисел и преобразует каждое число в его квадрат:

```Csharp
int[] numbers = { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 };
 
var result = numbers.Where(i => i > 0).Select(Square);
 
foreach (int i in result)
    Console.WriteLine(i);
 
int Square(int n) => n * n;
```

Метод Select в качестве параметра принимает тип ```Func<TSource```, TResult> selector. Так как у нас набор объектов int, то входным параметром делегата также будет объект типа int. В качестве типа выходного параметра выберем int, так как здесь квадрат числа - это целочисленное значение.