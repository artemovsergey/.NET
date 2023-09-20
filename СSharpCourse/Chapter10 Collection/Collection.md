# Коллекции

## Список List<T>

Хотя в языке C# есть массивы, которые хранят в себе наборы однотипных объектов, но работать с ними не всегда удобно. Например, массив хранит фиксированное количество объектов, однако что если мы заранее не знаем, сколько нам потребуется объектов. И в этом случае намного удобнее применять коллекции. Еще один плюс коллекций состоит в том, что некоторые из них реализует стандартные структуры данных, например, стек, очередь, словарь, которые могут пригодиться для решения различных специальных задач. Большая часть классов коллекций содержится в пространстве имен System.Collections.Generic.

Класс List<T> из пространства имен System.Collections.Generic представляет простейший список однотипных объектов. Класс List типизируется типом, объекты которого будут хранится в списке.

Мы можем создать пустой список:

```Csharp
List<string> people = new List<string>();
```

В данном случае объект List типизируется типом string. А это значит, что хранить в этом списке мы можем только строки.

Можно сразу при создании списка инициализировать его начальными значениями. В этом случае элементы списка помещаются после вызова конструктора в фигурных скобках

```Csharp
List<string> people = new List<string>() { "Tom", "Bob", "Sam" };
```

В данном случае в список помещаются три строки

Также можно при создании списка инициализировать его элементами из другой коллекции, например, другого списка:

```Csharp
var people = new List<string>() { "Tom", "Bob", "Sam" };
var employees = new List<string>(people);
```

Можно совместить оба способа:

```Csharp
var people = new List<string>() { "Tom", "Bob", "Sam" };
var employees = new List<string>(people){"Mike"};
```

В данном случае в списке employees будет четыре элемента ({ "Tom", "Bob", "Sam", "Mike" }) - три добавляются из списка people и один элемент задается при инициализации.

Подобным образом можно работать со списками других типов, например:

```Csharp
List<Person> people = new List<Person>() 
{ 
    new Person("Tom"),
    new Person("Bob"), 
    new Person("Sam") 
};
 
class Person
{
    public string Name { get;}
    public Person(string name) => Name = name;
}
```

## Установка начальной емкости списка

Еще один конструктор класса List принимает в качестве параметра начальную емкость списка:

```Csharp
List<string> people = new List<string>(16);
```

Указание начальной емкости списка позволяет в будущем увеличить производительность и уменьшить издержки на выделение памяти при добавлении элементов. Поскольку динамическое добавление в список может приводить на низком уровне к дополнительному выделению памяти, что снижает производительность. Если же мы знаем, что список не будет превышать некоторый размер, то мы можем передать этот размер в качестве емкости списка и избежать дополнительных выделений памяти.

Также начальную емкость можно установить с помощью свойства Capacity, которое имеется у класса List.

## Обращение к элементам списка

Как и массивы, списки поддерживают индексы, с помощью которых можно обратиться к определенным элементам:

```Csharp
var people = new List<string>() { "Tom", "Bob", "Sam" };
 
string firstPerson = people[0]; // получаем первый элемент
Console.WriteLine(firstPerson); // Tom
people[0] = "Mike";     // изменяем первый элемент
Console.WriteLine(people[0]); // Mike
```

## Длина списка

С помощью свойства Count можно получить длину списка:

```Csharp
var people = new List<string>() { "Tom", "Bob", "Sam" };
Console.WriteLine(people.Count);    // 3
```

## Перебор списка

C# позволяет осуществить перебор списка с помощью стандартного цикла foreach:/p>

```Csharp
var people = new List<string>() { "Tom", "Bob", "Sam" };
 
foreach (var person in people)
{
    Console.WriteLine(person);
}
// Вывод программы:
// Tom
// Bob
// Sam
```

Также можно использовать другие типы циклов и в комбинации с индексами перебирать списки:

```Csharp
var people = new List<string>() { "Tom", "Bob", "Sam" };
 
for (int i = 0; i < people.Count; i++)
{
    Console.WriteLine(people[i]);
}
```

## Методы списка

Среди его методов можно выделить следующие:

- void Add(T item): добавление нового элемента в список

- void AddRange(IEnumerable<T> collection): добавление в список коллекции или массива

- int BinarySearch(T item): бинарный поиск элемента в списке. Если элемент найден, то метод возвращает индекс этого элемента в коллекции. При этом список должен быть отсортирован.

- void CopyTo(T[] array): копирует список в массив array

- void CopyTo(int index, T[] array, int arrayIndex, int count): копирует из списка начиная с индекса index элементы, количество которых равно count, и вставляет их в массив array начиная с индекса arrayIndex

- bool Contains(T item): возвращает true, если элемент item есть в списке

- void Clear(): удаляет из списка все элементы

- bool Exists(Predicate<T> match): возвращает true, если в списке есть элемент, который соответствует делегату match

- T? Find(Predicate<T> match): возвращает первый элемент, который соответствует делегату match. Если элемент не найден, возвращается null

- T? FindLast(Predicate<T> match): возвращает последний элемент, который соответствует делегату match. Если элемент не найден, возвращается null

- List<T> FindAll(Predicate<T> match): возвращает список элементов, которые соответствуют делегату match

- int IndexOf(T item): возвращает индекс первого вхождения элемента в списке

- int LastIndexOf(T item): возвращает индекс последнего вхождения элемента в списке

- List<T> GetRange(int index, int count): возвращает список элементов, количество которых равно count, начиная с индекса index.

- void Insert(int index, T item): вставляет элемент item в список по индексу index. Если такого индекса в списке нет, то генерируется исключение

- void InsertRange(int index, collection): вставляет коллекцию элементов collection в текущий список начиная с индекса index. Если такого индекса в списке нет, то генерируется исключение

- bool Remove(T item): удаляет элемент item из списка, и если удаление прошло успешно, то возвращает true. Если в списке несколько одинаковых элементов, то удаляется только первый из них

- void RemoveAt(int index): удаление элемента по указанному индексу index. Если такого индекса в списке нет, то генерируется исключение

- void RemoveRange(int index, int count): параметр index задает индекс, с которого надо удалить элементы, а параметр count задает количество удаляемых элементов.

- int RemoveAll((Predicate<T> match)): удаляет все элементы, которые соответствуют делегату match. Возвращает количество удаленных элементов

- void Reverse(): изменяет порядок элементов

- void Reverse(int index, int count): изменяет порядок на обратный для элементов, количество которых равно count, начиная с индекса index

- void Sort(): сортировка списка

- void Sort(IComparer<T>? comparer): сортировка списка с помощью объекта comparer, который передается в качестве параметра

## Добавление в список

```Csharp
List<string> people = new List<string> () { "Tom" };
 
people.Add("Bob"); // добавление элемента
// people = { "Tom", "Bob" };
 
people.AddRange(new[] { "Sam", "Alice" });   // добавляем массив
// people = { "Tom", "Bob", "Sam", "Alice" };
// также можно было бы добавить другой список
// people.AddRange(new List<string>(){ "Sam", "Alice" });
 
people.Insert(0, "Eugene"); // вставляем на первое место
// people = { "Eugene", "Tom", "Bob", "Sam", "Alice" };
 
people.InsertRange(1, new string[] {"Mike", "Kate"}); // вставляем массив с индекса 1
// people = { "Eugene", "Mike", "Kate", "Tom", "Bob", "Sam", "Alice" };
 
// также можно было бы добавить другой список
// people.InsertRange(1, new List<string>(){ "Mike", "Kate" });
</string></string>
```

## Удаление из списка

```Csharp
var people = new List<string> () { "Eugene", "Mike", "Kate", "Tom", "Bob", "Sam", "Tom", "Alice" };
 
people.RemoveAt(1); //  удаляем второй элемент
// people = { "Eugene", "Kate", "Tom", "Bob", "Sam", "Tom", "Alice" };
 
people.Remove("Tom"); //  удаляем элемент "Tom"
// people = { "Eugene", "Kate", "Bob", "Sam", "Tom", "Alice" };
 
// удаляем из списка все элементы, длина строки которых равна 3
people.RemoveAll(person => person.Length == 3);
// people = { "Eugene", "Kate", "Alice" };
 
// удаляем из списка 2 элемента начиная с индекса 1
people.RemoveRange(1, 2);
// people = { "Eugene"};
 
// полностью очищаем список
people.Clear();
// people = {  };
```

## Поиск и проверка элемента

```Csharp
var people = new List<string> () { "Eugene", "Mike", "Kate", "Tom", "Bob", "Sam" };
 
var containsBob = people.Contains("Bob");     //  true
var containsBill = people.Contains("Bill");    // false
 
// проверяем, есть ли в списке строки с длиной 3 символа
var existsLength3 = people.Exists(p => p.Length == 3);  // true
 
// проверяем, есть ли в списке строки с длиной 7 символов
var existsLength7 = people.Exists(p => p.Length == 7);  // false
 
// получаем первый элемент с длиной в 3 символа
var firstWithLength3 = people.Find(p => p.Length == 3); // Tom
 
// получаем последний элемент с длиной в 3 символа
var lastWithLength3 = people.FindLast(p => p.Length == 3);  // Sam
 
// получаем все элементы с длиной в 3 символа в виде списка
List<string> peopleWithLength3 = people.FindAll(p => p.Length == 3);
// peopleWithLength3 { "Tom", "Bob", "Sam"}
```

## Получение диапазона и копирование в массив

```Csharp
List<string> people = new List<string>() {"Eugene", "Tom", "Mike", "Sam", "Bob" };
 
// получаем диапазон со второго по четвертый элемент
var range = people.GetRange(1, 3);
// range = { "Tom", "Mike", "Sam"};
 
// копируем в массив первые три элемента
string[] partOfPeople = new string[3];
people.CopyTo(0, partOfPeople, 0, 3);
// partOfPeople = { "Eugene", "Tom", "Mike"};
```

## Расположение элементов в обратном порядке

```Csharp
var people = new List<string> () { "Eugene", "Tom", "Mike", "Sam", "Bob" };
 
// переворачиваем весь список
people.Reverse();
// people = { "Bob","Sam", "Mike", "Tom", "Eugene"};
 
var people2 = new List<string>() { "Eugene", "Tom", "Mike", "Sam", "Bob" };
// переворачиваем часть только 3 элемента с индекса 1
people2.Reverse(1, 3);
// people2 = { "Eugene","Sam", "Mike", "Tom", "Bob" };
```

# Двухсвязный список LinkedList<T>

Класс LinkedList<T> представляет двухсвязный список, в котором каждый элемент хранит ссылку одновременно на следующий и на предыдущий элемент.

## Создание связанного списка

Для создания связного списка можно принименять один из его конструктора. Например, создадим пустой связный список:

```Csharp
LinkedList<string> people = new LinkedList<string>();
```

В данном случае связанный список people предназначен для хранения строк.

Также можно в конструктор передать коллекцию элементов, например, список List, по которому будет создан связный список:

```Csharp
var employees = new List<string> { "Tom", "Sam", "Bob" };
 
LinkedList<string> people = new LinkedList<string>(employees);
foreach (string person in people)
{
    Console.WriteLine(person);
}
```

## LinkedListNode

Если в простом списке List<T> каждый элемент представляет объект типа T, то в LinkedList<T> каждый узел представляет объект класса LinkedListNode<T>. А добавляемые в связанный список элементы T фактически обертываются в объект LinkedListNode.

Класс LinkedListNode имеет следующие свойства:

- Value: возвращает или устанавливает само значение узла, представленное типом T

- Next: возвращает ссылку на следующий элемент типа LinkedListNode<T> в списке. Если следующий элемент отсутствует, то имеет значение null

- Previous: возвращает ссылку предыдущий элемент типа LinkedListNode<T> в списке. Если предыдущий элемент отсутствует, то имеет значение null

### Свойства LinkedList

Класс LinkedList определяет следующие свойства:

- Count: количество элементов в связанном списке

- First: первый узел в списке в виде объекта LinkedListNode<T>

- Last: последний узел в списке в виде объекта LinkedListNode<T>

Используем эти свойства:

```Csharp
var employees = new List<string> { "Tom", "Sam", "Bob" };
 
LinkedList<string> people = new LinkedList<string>(employees);
Console.WriteLine(people.Count);            // 3
Console.WriteLine(people.First?.Value);    // Tom
Console.WriteLine(people.Last?.Value);    // Bob
```

Используя свойства LinkedList и LinkedListNode, можно пройтись по всем элементам списка в прямом или обратном порядке:

```Csharp
LinkedList<string> people = new LinkedList<string>(new[] { "Tom", "Sam", "Bob" });
 
// от начала до конца списка
var currentNode = people.First;
while(currentNode != null)
{
    Console.WriteLine(currentNode.Value);
    currentNode = currentNode.Next;
}
 
// с конца до начала списка
currentNode = people.Last;
while (currentNode != null)
{
    Console.WriteLine(currentNode.Value);
    currentNode = currentNode.Previous;
}
```

## Методы LinkedList

Используя методы класса LinkedList<T>, можно обращаться к различным элементам, как в конце, так и в начале списка:

- AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode): вставляет узел newNode в список после узла node.

- AddAfter(LinkedListNode<T> node, T value): вставляет в список новый узел со значением value после узла node.

- AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode): вставляет в список узел newNode перед узлом node.

- AddBefore(LinkedListNode<T> node, T value): вставляет в список новый узел со значением value перед узлом node.

- AddFirst(LinkedListNode<T> node): вставляет новый узел в начало списка

- AddFirst(T value): вставляет новый узел со значением value в начало списка

- AddLast(LinkedListNode<T> node): вставляет новый узел в конец списка

- AddLast(T value): вставляет новый узел со значением value в конец списка

- RemoveFirst(): удаляет первый узел из списка. После этого новым первым узлом становится узел, следующий за удаленным

- RemoveLast(): удаляет последний узел из списка

Применим некоторые из этих методов:

```Csharp
var people = new LinkedList<string>();
people.AddLast("Tom"); // вставляем узел со значением Tom на последнее место
                        //так как в списке нет узлов, то последнее будет также и первым
people.AddFirst("Bob"); // вставляем узел со значением Bob на первое место
 
// вставляем после первого узла новый узел со значением Mike
if (people.First != null) people.AddAfter(people.First, "Mike");
 
// теперь у нас список имеет следующую последовательность: Bob Mike Tom
foreach (var person in people) Console.WriteLine(person);
```

Подобным образом можно создавать связанные списки и других типов:

```Csharp
var company = new LinkedList<Person>();
 
company.AddLast(new Person("Tom"));
company.AddLast(new Person("Sam"));
company.AddFirst(new Person("Bill"));
 
foreach (var person in company) Console.WriteLine(person.Name);
 
class Person
{
    public string Name { get; }
    public Person(string name) => Name = name;
}
```

# Очередь Queue

Класс Queue<T> представляет обычную очередь, которая работает по алгоритму FIFO ("первый вошел - первый вышел").

## Создание очереди

Для создания очереди можно использовать один из трех ее конструкторов. Прежде всего можно создать пустую очередь:

```Csharp
Queue<string> people = new Queue<string>();
```

При создании пустой очереди можно указать емкость очереди:

```Csharp
Queue<string> people = new Queue<string>(16);
```

Также можно инициализировать очередь элементами из другой коллекции или массивом:

```Csharp
var employees = new List<string> { "Tom", "Sam", "Bob" };
Queue<string> people = new Queue<string>(employees);
foreach (var person in people) Console.WriteLine(person);
 
Console.WriteLine(people.Count); // 3
```

Для перебора очереди можно использовать стандартный цикл foreach.

Для получения количества элементов в очереди в классе определено свойство Count.

## Методы Queue

У класса Queue<T> можно отметить следующие методы:

- void Clear(): очищает очередь

- bool Contains(T item): возвращает true, если элемент item имеется в очереди

- T Dequeue(): извлекает и возвращает первый элемент очереди

- void Enqueue(T item): добавляет элемент в конец очереди

- T Peek(): просто возвращает первый элемент из начала очереди без его удаления

Посмотрим применение очереди на практике:

```Csharp
var people = new Queue<string>();
 
// добавляем элементы
people.Enqueue("Tom");  // people = { Tom }
people.Enqueue("Bob");  // people = { Tom, Bob }
people.Enqueue("Sam");  // people = { Tom, Bob, Sam }
 
// получаем элемент из самого начала очереди 
var firstPerson = people.Peek();
Console.WriteLine(firstPerson); // Tom
 
// удаляем элементы
var person1 = people.Dequeue();  // people = { Bob, Sam  }
Console.WriteLine(person1); // Tom
var person2 = people.Dequeue();  // people = { Sam  }
Console.WriteLine(person2); // Bob
var person3 = people.Dequeue();  // people = {  }
Console.WriteLine(person3); // Sam
```

Стоит отметить, что если с помощью методов Peek или Dequeue мы попытаемся получить первый элемент очереди, которая пуста, то программа выдаст исключение. Соответственно перед получением элемента мы можем 
проверять количество элементов в очереди:

```Csharp
if(people.Count > 0)
{
    var person = people.Peek();
    people.Dequeue();
}
```

Либо можно использовать пару методов:

- bool TryDequeue(out T result): передает в переменную result первый элемент очереди с его удалением из очереди, возвращает true, если очередь не пуста и элемент успешно получен.

- bool TryPeek(out T result): передает в переменную result первый элемент очереди без его извлечения из очереди, возвращает true, если очередь не пуста и элемент успешно получен.

Применение методов:

```Csharp
var people = new Queue<string>();
 
// добавляем элементы
people.Enqueue("Tom");  // people = { Tom }
 
// удаляем элементы
var success1 = people.TryDequeue(out var person1);  // success1 = true
if (success1) Console.WriteLine(person1); // Tom
 
var success2 = people.TryPeek(out var person2);  // success2 = false
if (success2) Console.WriteLine(person2); 
```

Очереди - довольно часто встречаемая стуктура в реальной жизни. Например, очередь пациентов на прием к врачу. Реализуем данную ситуацию:

```Csharp
var patients = new Queue<Person>();
patients.Enqueue(new Person("Tom"));
patients.Enqueue(new Person("Bob"));
patients.Enqueue(new Person("Sam"));
 
var practitioner = new Doctor();
practitioner.TakePatients(patients);
 
class Person
{
    public string Name { get; }
    public Person(string name) => Name = name;
}
 
class Doctor
{
    public void TakePatients(Queue<Person> patients)
    {
        while(patients.Count > 0)
        {
            var patient = patients.Dequeue();
            Console.WriteLine($"Осмотр пациента {patient.Name}");
        }
        Console.WriteLine("Доктор закончил осматривать пациентов");
    }
}
```

Здесь класс врача - класс Doctor в методе TakePatients принимает очередь пациентов в виде объектов Person. И пока в очереди есть объекты извлекает по одному объекту. Консольный вывод:

```Csharp
Осмотр пациента Tom
Осмотр пациента Bob
Осмотр пациента Sam
Доктор закончил осматривать пациентов
```

# Коллекция Stack<T>

Класс Stack<T> представляет коллекцию, которая использует алгоритм LIFO ("последний вошел - первый вышел"). При такой организации каждый следующий добавленный элемент помещается поверх предыдущего. Извлечение из коллекции происходит в обратном порядке - извлекается тот элемент, который находится выше всех в стеке.

Стек - довольно часто встречаемая структура данных в реальной жизни. Банальные примеры стеков - стопка книг или тарелок, где каждую новую книгу или тарелку помещают поверх предыдущей. А извлекают из этой стопки книги/тарелки в обратном порядке - сначала самую верхнюю и так далее. Другой пример - одежда: допустим, человек выходит на улицу в зимнюю погоду и для этого сначала одевает майку, потом рубашку, затем свитер, и в конце куртку. Когда человек снимает с себя одежду - он делает это в обратном порядке: сначала снимает куртку, потом свитер и так далее.

## Создание стека

Для создания стека можно использовать один из трех конструкторов. Прежде всего можно создать пустой стек:

```Csharp
Stack<string> people = new Stack<string>();
```

При создании пустого стека можно указать емкость стека:

```Csharp
Stack<string> people = new Stack<string>(16);
```

Также можно инициализировать стек элементами из другой коллекции или массивом:

```Csharp
var employees = new List<string> { "Tom", "Sam", "Bob" };
Stack<string> people = new Stack<string>(employees);
foreach (var person in people) Console.WriteLine(person);
 
Console.WriteLine(people.Count); // 3
```

Для перебора стека можно использовать стандартный цикл foreach. Причем в цикле в соответствии с аалгоритмом стека LIFO данные извлекаются в порядке, обратном их добавлению. Консольный вывод в данном случае:

```
Bob
Sam
Tom
3
```

Для получения количества элементов стека применяется свойство Count.

## Методы Stack

В классе Stack можно выделить следующие методы:

- Clear: очищает стек

- Contains: проверяет наличие в стеке элемента и возвращает true при его наличии

- Push: добавляет элемент в стек в верхушку стека

- Pop: извлекает и возвращает первый элемент из стека

- Peek: просто возвращает первый элемент из стека без его удаления

Посмотрим на примере:

```Csharp
var people = new Stack<string>();
people.Push("Tom");
// people = { Tom }
people.Push("Sam");
// people = { Sam, Tom }
people.Push("Bob");
// people = { Bob, Sam, Tom }
 
// получаем первый элемент стека без его удаления 
string headPerson = people.Peek();
Console.WriteLine(headPerson);  // Bob
 
string person1 = people.Pop();
// people = { Sam, Tom }
Console.WriteLine(person1);  // Bob
 
string person2 = people.Pop();
// people = { Tom }
Console.WriteLine(person2);  // Sam
 
string person3 = people.Pop();
// people = { }
Console.WriteLine(person3);  // Tom
```

Работу стека можно представить следующей иллюстрацией:

![](https://metanit.com/sharp/tutorial/pics/4.1.png)

Стоит отметить, что если с помощью методов Peek или Pop мы попытаемся получить первый элемент стека, который пуст, то программа выдаст исключение. Соответственно перед получением элемента мы можем проверять количество элементов в стеке:

```Csharp
if(people.Count > 0)
{
    var person = people.Peek();
    people.Pop();
}
```

Либо можно использовать пару методов:

- bool TryPop(out T result): удаляет из стека первый элемент и передает его в переменную result, возвращает true, если очередь не пуста и элемент успешно получен.

- bool TryPeek(out T result): передает в переменную result первый элемент стека без его извлечения, возвращает true, если элемент успешно получен.

Применение методов:

```Csharp
var people = new Stack<string>();
people.Push("Tom");
// people = { Tom }
 
// удаляем элементы
var success1 = people.TryPop(out var person1);  // success1 = true
if (success1) Console.WriteLine(person1); // Tom
 
var success2 = people.TryPeek(out var person2);  // success2 = false
if (success2) Console.WriteLine(person2);
```

# Коллекция Dictionary<K, V>

Еще один распространенный тип коллекции представляют словари. Словарь хранит объекты, которые представляют пару ключ-значение. Класс словаря Dictionary<K, V> типизируется двумя типами: параметр K представляет тип ключей, а параметр V предоставляет тип значений.

## Создания и инициализация словаря

Класс Dictionary предоставляет ряд конструкторов для создания словаря. Например, мы можем создать пустой словарь:

```Csharp
Dictionary<int, string> people = new Dictionary<int, string>();
```

Здесь словарь people в качестве ключей принимает значения типа int, а в качестве значений - строки.

При определении словаря его сразу же можно инициализировать значениями:

```Csharp
var people = new Dictionary<int, string>()
{
    { 5, "Tom"},
    { 3, "Sam"},
    { 11, "Bob"}
};
```

При инициализации применяется инициализитор - в фигурных скобках после вызова конструктора объекту передаются начальные данные. В случае со словаем мы можем передать в инициализаторе набор элементов, где каждый элемент заключается в фигурные скобки, например:

```Csharp
{ 5, "Tom"}
```

Каждый элемент представляет два значения: первое значение представляет ключ, а второе значение - собственно значение элемента. Поскольку при объявлении словаря people для ключей указан тип int, а для значений - тип string, то в элементе словаря сначала указывается число int, а затем строка. То есть в случае выше элемент имеет ключ 5, а значение - "Tom". Затем по ключу элемента мы сможем получить его значение.

Также мы можем применять другой способ инициализации:

```Csharp
var people = new Dictionary<int, string>()
{
    [5] = "Tom",
    [6] = "Sam",
    [7] = "Bob"
}; 
```

При таком способе инициализации в квадратных скобках указывается ключ и ему присваивается значение элемента. Но в целом этот способ инициализации будет равноценен предыдущему.

## KeyValuePair

Стоит отметить, что каждый элемент в словаре представляет структуру KeyValuePair<TKey, TValue>, где параметр TKey представляет тип ключа, а параметр TValue - тип значений элементов. Эта структура предоставляет свойства Key и Value, с помощью которых можно получить соответственно ключ и значение элемента в словаре. И одна из версий конструктора Dictionary позволяет инициализировать словарь коллекцией объектов KeyValuePair:

```Csharp
var mike = new KeyValuePair<int, string>(56, "Mike"); 
var employees = new List<KeyValuePair<int, string>>() { mike};
var people = new Dictionary<int, string>(employees);
```

Конструктор типа KeyValuePair принимает два параметра - ключ элемента и его значения. То есть в данном случае создается один такой элемент - mike с ключом 56 и значением "Mike". И этот элемент добавляется в список employees, которым затем инициализируется словарь.

Можно совместить оба способа инициализации:

```Csharp
var mike = new KeyValuePair<int, string>(56, "Mike");
var employees = new List<KeyValuePair<int, string>>() { mike };
var people = new Dictionary<int, string>(employees)
{
    [5] = "Tom",
    [6] = "Sam",
    [7] = "Bob",
};
```

В данном случае в словаре people будет четыре элемента.

## Перебор словаря

Для перебора словаря можно применять цикл foreach:

```Csharp
var people = new Dictionary<int, string>()
{
    [5] = "Tom",
    [6] = "Sam",
    [7] = "Bob"
};
foreach(var person in people)
{
    Console.WriteLine($"key: {person.Key}  value: {person.Value}");
}
```

При переборе каждый элемент будет помещаться в переменную, которая представляет тип KeyValuePair, соответственно с помощью свойств Key и Value мы сможем получить ключ и значение элемента. Консольный вывод программы:

```
key: 5  value: Tom
key: 6  value: Sam
key: 7  value: Bob
```

## Получение элементов

Для обращения к элементам из словаря применяется их ключ, который передается в квадратных скобках:

```
словарь[ключ]
```

Таким образом мы можем получить и изменить элементы словаря

```Csharp
var people = new Dictionary<int, string>()
{
    [5] = "Tom",
    [6] = "Sam",
    [7] = "Bob",
};
// получаем элемент по ключу 6
string sam = people[6];  // Sam
Console.WriteLine(sam);  // Sam
// переустанавливаем значение по ключу 6
people[6] = "Mike";
Console.WriteLine(people[6]);  // Mike
 
// добавляем новый элемент по ключу 22
people[22] = "Eugene";
Console.WriteLine(people[22]);  // Eugene
```

Более того, таким образом мы можем также добавить новый элемент в словарь. При установке значения по ключу, если элемент с таким ключом уже есть в словаре, то значение переустанавливается. Если же элемента с подобным ключом нет в словаре, то элемент добавляется.:

## Методы и свойства Dictionary

Среди методов класса Dictionary можно выделить следующие:

- void Add(K key, V value): добавляет новый элемент в словарь

- void Clear(): очищает словарь

- bool ContainsKey(K key): проверяет наличие элемента с определенным ключом и возвращает true при его наличии в словаре

- bool ContainsValue(V value): проверяет наличие элемента с определенным значением и возвращает true при его наличии в словаре

- bool Remove(K key): удаляет по ключу элемент из словаря

Другая версия этого метода позволяет получить удленный элемент в выходной параметр: bool Remove(K key, out V value)

- bool TryGetValue(K key, out V value): получает из словаря элемент по ключу key. При успешном получении передает значение элемента в выходной параметр value и возвращает true

- bool TryAdd(K key, V value): добавляет в словарь элемент с ключом key и значением value. При успешном добавлении возвращает true

Из свойств следует отметить свойство Count, которое возвращает количество элементов в словаре.

Применение методов:

```Csharp
// условная телефонная книга
var phoneBook = new Dictionary<string, string>();
 
// добавляем элемент: ключ - номер телефона, значение - имя абонента
phoneBook.Add("+123456", "Tom");
// альтернативное добавление
// phoneBook["+123456"] = "Tom";
 
// Проверка наличия
var phoneExists1 = phoneBook.ContainsKey("+123456");    // true
Console.WriteLine($"+123456: {phoneExists1}");
var phoneExists2 = phoneBook.ContainsKey("+567456");    // false
Console.WriteLine($"+567456: {phoneExists2}");
var abonentExists1 = phoneBook.ContainsValue("Tom");      // true
Console.WriteLine($"Tom: {abonentExists1}");
var abonentExists2 = phoneBook.ContainsValue("Bob");      // false
Console.WriteLine($"Bob: {abonentExists2}");
 
// удаление элемента
phoneBook.Remove("+123456");
 
// проверяем количество элементов после удаления
Console.WriteLine($"Count: {phoneBook.Count}"); // Count: 0
```

# Класс ObservableCollection

Кроме стандартных классов коллекций типа списков, очередей, словарей, стеков .NET также предоставляет специальный класс ObservableCollection<T>. В отличие от ранее рассмотренных коллекций данный класс определен в пространстве имен System.Collections.ObjectModel. По функциональности коллекция ObservableCollection похожа на список List за тем исключением, что позволяет известить внешние объекты о том, что коллекция была изменена.

## Создание и инициализация ObservableCollection

Для создания объекта класс ObservableCollection предоставляет ряд конструкторов. Прежде всего мы можем создать пустую коллекцию:

```Csharp
using System.Collections.ObjectModel;
 
ObservableCollection<string> people = new ObservableCollection<string>();
```

В данном случае коллекция people типизируется типом string, поэтому может хранить только строки.

Другая версия конструктора позволяет передать в ObservableCollection объекты из другой коллекции или массива:

```Csharp
var people = new ObservableCollection<string>( new string[] {"Tom", "Bob", "Sam"});
```

Для инициализации можно через инициализатор в фигурных скобках передать значения

```Csharp
var people = new ObservableCollection<string>
{
    "Tom", "Bob", "Sam"
};
```

Также можно сочетать предыдущие два способа:

```Csharp
var people = new ObservableCollection<string>( new string[] {"Mike", "Alice", "Kate" })
{
    "Tom", "Bob", "Sam"
};
```

## Обращение к элементам коллекции

Для обращения к элементам ObservableCollection можно применять индексы на манер массивов или списков List:

```Csharp
var people = new ObservableCollection<string>
{
    "Tom", "Bob", "Sam"
};
 
// получаем первый элемент
Console.WriteLine(people[0]);   // Tom
// изменяем первый элемент
people[0] = "Tomas";
Console.WriteLine(people[0]);   // Tomas
```

## Перебор коллекции

Для перебора коллекции можно применять стандартные циклы:

```Csharp
using System.Collections.ObjectModel;
 
var people = new ObservableCollection<string>
{
    "Tom", "Bob", "Sam"
};
 
foreach(var person in people)
{
    Console.WriteLine(person);
}
for (int i =0; i < people.Count; i++)
{
    Console.WriteLine(people[i]);
}
```

С помощью свойства Count можно получить количество элементов в коллекции.

## Методы ObservableCollection

Среди методов класса ObservableCollection можно отметить следующие:

- void Add(T item): добавление нового элемента в коллекцию

- void CopyTo(T[] array, int index,): копирует в массив array элементы из коллекции начиная с индекса index

- bool Contains(T item): возвращает true, если элемент item есть в коллекции

- void Clear(): удаляет из коллекции все элементы

- int IndexOf(T item): возвращает индекс первого вхождения элемента в коллекции

- void Insert(int index, T item): вставляет элемент item в коллекцию по индексу index. Если такого индекса в коллекции нет, то генерируется исключение

- bool Remove(T item): удаляет элемент item из коллекции, и если удаление прошло успешно, то возвращает true. Если в коллекции несколько одинаковых элементов, то удаляется только первый из них

- void RemoveAt(int index): удаление элемента по указанному индексу index. Если такого индекса в коллекции нет, то генерируется исключение

- void Move(int oldIndex, int newIndex): перемещает элемент с индекса oldIndex на позицию по индексу newIndex

Применение методов:

```Csharp
using System.Collections.ObjectModel;
 
var people = new ObservableCollection<string>();
 
// добавляем элемент
people.Add("Bob");
// вставляем элемент по индексу 0
people.Insert(0, "Tom");
 
// проверка наличия элемента 
bool bobExists = people.Contains("Bob");     // true
Console.WriteLine($"Bob exists: {bobExists}");
bool mikeExists = people.Contains("Mike");   // false
Console.WriteLine($"Mike exists: {mikeExists}");
 
// удаляем элемент
people.Remove("Tom");
// удаляем элемент по индексу 0
people.RemoveAt(0);
```

## Уведомление об измении коллекции

Класс ObservableCollection определяет событие CollectionChanged, подписавшись на которое, мы можем обработать любые изменения коллекции. Данное событие представляет делегат NotifyCollectionChangedEventHandler:

```Csharp
void NotifyCollectionChangedEventHandler(object? sender, NotifyCollectionChangedEventArgs e);
```

Второй параметр делегата - объект NotifyCollectionChangedEventArgs хранит всю информацию о событии. В частности, его свойство Action позволяет узнать характер изменений. Оно хранит одно из значений из перечисления NotifyCollectionChangedAction:

- NotifyCollectionChangedAction.Add: добавление

- NotifyCollectionChangedAction.Remove: удаление

- NotifyCollectionChangedAction.Replace: замена

- NotifyCollectionChangedAction.Move: перемещение объекта внутри коллекции на новую позицию

- NotifyCollectionChangedAction.Reset: сброс содержимого коллекции (например, при ее очистке с помощью метода Clear())

Кроме того, свойства NewItems и OldItems позволяют получить соответственно добавленные и удаленные объекты. Таким образом, мы получаем полный контроль над обработкой добавления, удаления и замены объектов в коллекции.

Допустим, у нас будет следующий класс Person, который представляет пользователя:

```Csharp
class Person
{
    public string Name { get; }
    public Person(string name) => Name = name;
}
```

Для управления коллекцией объектов Person определим следующую программу:

```Csharp
using System.Collections.ObjectModel;
using System.Collections.Specialized;
 
var people = new ObservableCollection<Person>() 
{ 
    new Person("Tom"), 
    new Person("Sam") 
};
// подписываемся на событие изменения коллекции
people.CollectionChanged += People_CollectionChanged;
 
people.Add(new Person("Bob"));  // добавляем новый элемент
 
people.RemoveAt(1);                 // удаляем элемент
people[0] = new Person("Eugene");   // заменяем элемент
 
Console.WriteLine("\nСписок пользователей:");
foreach (var person in people)
{
    Console.WriteLine(person.Name);
}
// обработчик изменения коллекции
void People_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
{
    switch (e.Action)
    {
        case NotifyCollectionChangedAction.Add: // если добавление
            if(e.NewItems?[0] is Person newPerson)
                Console.WriteLine($"Добавлен новый объект: {newPerson.Name}");
            break;
        case NotifyCollectionChangedAction.Remove: // если удаление
            if (e.OldItems?[0] is Person oldPerson)
                Console.WriteLine($"Удален объект: {oldPerson.Name}");
            break;
        case NotifyCollectionChangedAction.Replace: // если замена
            if ((e.NewItems?[0] is Person replacingPerson)  && 
                (e.OldItems?[0] is Person replacedPerson))
                Console.WriteLine($"Объект {replacedPerson.Name} заменен объектом {replacingPerson.Name}");
            break;
    }
}
```

Здесь в качестве обработчика изменений коллекции выступает метод People_CollectionChanged, в котором с помощью параметра NotifyCollectionChangedEventArgs получаем информацию об изменении. Консольный вывод программы:

```
Добавлен новый объект: Bob
Удален объект: Sam
Объект Tom заменен объектом Eugene

Список пользователей:
Eugene
Bob
```

# Интерфейсы IEnumerable и IEnumerator

Как мы увидели, основной для большинства коллекций является реализация интерфейсов IEnumerable и IEnumerator. Благодаря такой реализации мы можем перебирать объекты в цикле foreach:

```Csharp
foreach(var item in перечислимый_объект)
{
     
}
```

Перебираемая коллекция должна реализовать интерфейс IEnumerable.

Интерфейс IEnumerable имеет метод, возвращающий ссылку на другой интерфейс - перечислитель:

```Csharp
public interface IEnumerable
{
    IEnumerator GetEnumerator();
}
```

А интерфейс IEnumerator определяет функционал для перебора внутренних объектов в контейнере:

```Csharp
public interface IEnumerator
{
    bool MoveNext(); // перемещение на одну позицию вперед в контейнере элементов
    object Current {get;}  // текущий элемент в контейнере
    void Reset(); // перемещение в начало контейнера
}
```

Метод MoveNext() перемещает указатель на текущий элемент на следующую позицию в последовательности. Если последовательность еще не закончилась, то возвращает true. Если же последовательность закончилась, то возвращается false.

Свойство Current возвращает объект в последовательности, на который указывает указатель.

Метод Reset() сбрасывает указатель позиции в начальное положение.

Каким именно образом будет осуществляться перемещение указателя и получение элементов зависит от реализации интерфейса. В различных реализациях логика может быть построена различным образом.

Например, без использования цикла foreach перебирем массив с помощью интерфейса IEnumerator:

```Csharp
using System.Collections;
 
string[] people = {"Tom", "Sam", "Bob"};
 
IEnumerator peopleEnumerator = people.GetEnumerator(); // получаем IEnumerator
while (peopleEnumerator.MoveNext())   // пока не будет возвращено false
{
    string item = (string)peopleEnumerator.Current; // получаем элемент на текущей позиции
    Console.WriteLine(item);
}
peopleEnumerator.Reset(); // сбрасываем указатель в начало массива
```

## Реализация IEnumerable и IEnumerator

Рассмотрим простешую реализацию IEnumerable на примере:

```Csharp
using System.Collections;
 
Week week = new Week();
foreach (var day in week)
{
    Console.WriteLine(day);
}
 
class Week : IEnumerable
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
                         "Friday", "Saturday", "Sunday" };
    public IEnumerator GetEnumerator() => days.GetEnumerator();
}
```

В данном случае класс Week, который представляет неделю и хранит все дни недели, реализует интерфейс IEnumerable. Однако в данном случае мы поступили очень просто - вместо реализации IEnumerator мы просто возвращаем в методе GetEnumerator объект IEnumerator для массива.

```Csharp
public IEnumerator GetEnumerator() => days.GetEnumerator();
```

Благодаря этому мы можем перебрать все дни недели в цикле foreach.

В то же время стоит отметить, что для перебора коллекции через foreach в принципе необязательно реализовать интерфейс IEnumerable. Достаточно в классе определить публичный метод GetEnumerator, который бы возвращал объект IEnumerator. Например:

```Csharp
class Week
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", 
                        "Friday", "Saturday", "Sunday" };
    public IEnumerator GetEnumerator() =>days.GetEnumerator();
}
```

Однако это было довольно просто - мы просто используем уже готовый перчислитель массива. Однако, возможно, потребуется задать свою собственную логику перебора объектов. Для этого реализуем интерфейс IEnumerator:

```Csharp
using System.Collections;
 
class WeekEnumerator : IEnumerator
{
    string[] days;
    int position = -1;
    public WeekEnumerator(string[] days) => this.days = days;
    public object Current
    {
        get
        {
            if (position == -1 || position >= days.Length)
                throw new ArgumentException();
            return days[position];
        }
    }
    public bool MoveNext()
    {
        if (position < days.Length - 1)
        {
            position++;
            return true;
        }
        else
            return false;
    }
    public void Reset() => position = -1;
}
class Week
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
                            "Friday", "Saturday", "Sunday" };
    public IEnumerator GetEnumerator() => new WeekEnumerator(days);
}
```

Здесь теперь класс Week использует не встроенный перечислитель, а WeekEnumerator, который реализует IEnumerator.

Ключевой момент при реализации перечислителя - перемещения указателя на элемент. В классе WeekEnumerator для хранения текущей позиции определена переменная position. Следует учитывать, что в самом начале (в исходном состоянии) указатель должен указывать на позицию условно перед первым элементом. Когда будет производиться цикл foreach, то данный цикл вначале вызывает метод MoveNext и фактически перемещает указатель на одну позицию в перед и только затем обращается к свойству Current для получения элемента в текущей позиции.

Затем в программе мы можем аналогичным образом перебирать объект Week с помощью цикла foreach:

```Csharp
Week week = new Week();
foreach(var day in week)
{
    Console.WriteLine(day);
}
```

## Обобщенная версия IEnumerator

В примерах выше использовались необобщенные версии интерфейсов, однако мы также можем использовать их обобщенные двойники:

```Csharp
using System.Collections;
 
class WeekEnumerator : IEnumerator<string>
{
    string[] days;
    int position = -1;
    public WeekEnumerator(string[] days) => this.days = days;
    public string Current
    {
        get
        {
            if (position == -1 || position >= days.Length)
                throw new ArgumentException();
            return days[position];
        }
    }
    object IEnumerator.Current => Current;
    public bool MoveNext()
    {
        if (position < days.Length - 1)
        {
            position++;
            return true;
        }
        else
            return false;
    }
    public void Reset() => position = -1;
    public void Dispose() { }
}
class Week
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
                            "Friday", "Saturday", "Sunday" };
    public IEnumerator<string> GetEnumerator() => new WeekEnumerator(days);
}
```

В данном случае реализуем интерфейс IEnumerator<string>, соответственно в свойстве Current нам надо возвратить объект string. В этом случае при переборе в цикле foreach перебираемые объекты будут автоматически представлять тип string:

```Csharp
Week week = new Week();
foreach (string day in week)
{
    Console.WriteLine(day);
}
```

# Итераторы и оператор yield

Итератор по сути представляет блок кода, который использует оператор yield для перебора набора значений. Данный блок кода может представлять тело метода, оператора или блок get в свойствах.
Итератор использует две специальных инструкции:

- yield return: определяет возвращаемый элемент

- yield break: указывает, что последовательность больше не имеет элементов

Рассмотрим небольшой пример:

```Csharp
Numbers numbers = new Numbers();
foreach (int n in numbers)
{
    Console.WriteLine(n);
}
 
class Numbers
{
    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return i * i;
        }
    }
}
```

В классе Numbers метод GetEnumerator() фактически представляет итератор. С помощью оператора yield return возвращается некоторое значение (в данном случае квадрат числа).

В программе с помощью цикла foreach мы можем перебрать объект Numbers как обычную коллекцию. При получении каждого элемента в цикле foreach будет срабатывать оператор yield return, который будет возвращать один элемент и запоминать текущую позицию.

Благодаря итераторам мы можем пойти дальше и легко реализовать перебор числа в цикле foreach:

```Csharp
foreach(var n in 5) Console.WriteLine(n);
foreach (var n in -5) Console.WriteLine(n);
 
static class Int32Extension
{
    public static IEnumerator<int> GetEnumerator(this int number)
    {
        int k = (number > 0)? number: 0;
        for (int i = number - k; i <= k; i++) yield return i;
    }
}
```

В данном случае итератор реализован как метод расширения для типа int или System.Int32. В методе итератора фактически возвращаем все целочисленные значения от 0 до текущего числа. Консольный вывод:

```
0
1
2
3
4
5
-5
-4
-3
-2
-1
0
```

Другой пример: пусть у нас есть коллекция Company, которая представляет компанию и которая хранит в массиве personnel штат сотрудников - объектов Person. Используем оператор yield для перебора этой коллекции:

```Csharp
class Person
{
    public string Name { get; }
    public Person(string name) =>Name = name;
}
class Company
{
    Person[] personnel;
    public Company(Person[] personnel) => this.personnel = personnel;
    public int Length => personnel.Length;
    public IEnumerator<Person> GetEnumerator()
    {
        for (int i = 0; i < personnel.Length; i++)
        {
            yield return personnel[i];
        }
    }
}
```

Метод GetEnumerator() представляет итератор. И когда мы будем осуществлять перебор в объекте Company в цикле foreach, то будет идти обращение к вызову yield return personnel[i];. При обращении к оператору yield return будет сохраняться текущее местоположение. И когда метод foreach перейдет к следующей итерации для получения нового объекта, итератор начнет выполнения с этого местоположения.

Ну и в основной программе в цикле foreach выполняется собственно перебор, благодаря реализации итератора:

```Csharp
var people = new Person[] 
{ 
    new Person("Tom"), 
    new Person("Bob"), 
    new Person("Sam") 
};
var microsoft = new Company(people);
 
foreach(Person employee in microsoft)
{
    Console.WriteLine(employee.Name);
}
```

Хотя при реализации итератора в методе GetEnumerator() применялся перебор массива в цикле for, но это необязательно делать. Мы можем просто определить несколько вызовов оператора yield return:

```Csharp
public IEnumerator<Person> GetEnumerator()
{
    yield return personnel[0];
    yield return personnel[1];
    yield return personnel[2];
}
```

В этом случае при каждом вызове оператора yield return итератор также будет запоминать текущее местоположение и при последующих вызовах начинать с него.

## Именованный итератор

Выше для создания итератора мы использовали метод GetEnumerator. Но оператор yield можно использовать внутри любого метода, только такой метод должен возвращать объект интерфейса IEnumerable. Подобные методы еще называют именованными итераторами.

Создадим такой именованный итератор в классе Company и используем его:

```Csharp
class Person
{
    public string Name { get; }
    public Person(string name) =>Name = name;
}
class Company
{
    Person[] personnel;
    public Company(Person[] personnel) => this.personnel = personnel;
    public int Length => personnel.Length;
    public IEnumerable<Person> GetPersonnel(int max)
    {
        for (int i = 0; i < max; i++)
        {
            if (i == personnel.Length)
            {
                yield break;
            }
            else
            {
                yield return personnel[i];
            }
        }
    }
}
```

Определенный здесь итератор - метод IEnumerable GetPersonnel(int max) в качестве параметра принимает количество выводимых объектов. В процессе работы программы может сложиться, что его значение будет больше, чем длина массива personnel. И чтобы не произошло ошибки, используется оператор yield break. Этот оператор прерывает выполнение итератора.

Применение итератора:

```Csharp
var people = new Person[]
{
    new Person("Tom"), 
    new Person("Bob"),
    new Person("Sam")
};
var microsoft = new Company(people);
 
foreach(Person employee in microsoft.GetPersonnel(5))
{
    Console.WriteLine(employee.Name);
}
```

Вызов microsoft.GetPersonnel(3) будет возвращать набор из не более чем 5 объектов Person. Но так как у нас всего три таких объекта, то в методе GetPersonnel после трех операций сработает оператор yield break.