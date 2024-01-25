# Работа с XML в C#
## XML-документы

На сегодняшний день XML является одним из распространенных стандартов документов, который позволяет в удобной форме сохранять сложные по структуре данные. Поэтому разработчики платформы .NET включили в фреймворк широкие возможности для работы с XML.

Прежде чем перейти непосредственно к работе с XML-файлами, сначала рассмотрим, что представляет собой xml-документ и как он может хранить объекты, используемые в программе на c#.

Например, у нас есть следующий класс:

```Csharp
class Person
{
    public string Name { get;}
    public int Age { get; set; }
    public string Company { get; set; }
    public Person(string name, int age, string company)
    {
        Name = name;
        Age = age;
        Company = company;
    }
}
```
В программе на C# мы можем создать список объектов класса Person:

```Csharp
var employees = new List<Person>
{
    new Person ("Tom", 37, "Microsoft"),
    new Person ("Bob", 41, "Google")
};
```
Чтобы сохранить список в формате xml мы могли бы использовать следующий xml-файл:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```

XML-документ объявляет строка <?xml version="1.0" encoding="utf-8" ?>. Она задает версию (1.0) и кодировку (utf-8) xml. Далее идет собственно содержимое документа.

XML-документ должен иметь один единственный корневой элемент, внутрь которого помещаются все остальные элементы. В данном случае таким элементом является элемент <people>. Внутри корневого элемента <people> задан набор элементов <person>. Вне корневого элемента мы не можем разместить элементы person.

Каждый элемент определяется с помощью открывающего и закрывающего тегов, например, <person> и </person>, внутри которых помещается значение или содержимое элементов. Также элемент может иметь сокращенное объявление: <person /> - в конце элемента помещается слеш.

Элемент может иметь вложенные элементы и атрибуты. В данном случае каждый элемент person имеет два вложенных элемента company и age и атрибут name.

Атрибуты определяются в теле элемента и имеют следующую форму: название="значение". Например, <person name="Bill Gates">, в данном случае атрибут называется name и имеет значение Bill Gates

Внутри простых элементов помещается их значение. Например, <company>Google</company> - элемент company имеет значение Google.

Названия элементов являются регистрозависимыми, поэтому <company> и <COMPANY> будут представлять разные элементы.

Таким образом, весь список Users из кода C# сопоставляется с корневым элементом <people>, каждый объект Person - с элементом <person>, а каждое свойство объекта Person - с атрибутом или вложенным элементом элемента <person>

Что использовать для свойств - вложенные элементы или атрибуты? Это вопрос предпочтений - мы можем использовать как атрибуты, так и вложенные элементы. Так, в предыдущем примере вполне можно использовать вместо атрибута вложенный элемент:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<people>
  <person>
    <name>Tom</name>
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person>
    <name>Bob</name>
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```
Теперь рассмотрим основные подходы для работы с XML, которые имеются в C#.

## Работа с XML с помощью классов System.Xml

Для работы с XML в C# можно использовать несколько подходов. В первых версиях фреймворка основной функционал работы с XML предоставляло пространство имен System.Xml. В нем определен ряд классов, которые позволяют манипулировать xml-документом:

- XmlNode: представляет узел xml. В качестве узла может использоваться весь документ, так и отдельный элемент

- XmlDocument: представляет весь xml-документ

- XmlElement: представляет отдельный элемент. Наследуется от класса XmlNode

- XmlAttribute: представляет атрибут элемента

- XmlText: представляет значение элемента в виде текста, то есть тот текст, который находится в элементе между его открывающим и закрывающим тегами

- XmlComment: представляет комментарий в xml

- XmlNodeList: используется для работы со списком узлов

Ключевым классом, который позволяет манипулировать содержимым xml, является XmlNode, поэтому рассмотрим некоторые его основные методы и свойства:

- Свойство Attributes возвращает объект XmlAttributeCollection, который представляет коллекцию атрибутов

- Свойство ChildNodes возвращает коллекцию дочерних узлов для данного узла

- Свойство HasChildNodes возвращает true, если текущий узел имеет дочерние узлы

- Свойство FirstChild возвращает первый дочерний узел

- Свойство LastChild возвращает последний дочерний узел

- Свойство InnerText возвращает текстовое значение узла

- Свойство InnerXml возвращает всю внутреннюю разметку xml узла

- Свойство Name возвращает название узла. Например, <user> - значение свойства Name равно "user"

- Свойство ParentNode возвращает родительский узел у текущего узла

Применим эти классы и их функционал. И вначале для работы с xml создадим новый файл. Назовем его people.xml и определим в нем следующее содержание:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```
Теперь пройдемся по этому документу и выведем его данные на консоль:

```Csharp
using System.Xml;
 
XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
// получим корневой элемент
XmlElement? xRoot = xDoc.DocumentElement;
if (xRoot != null)
{
    // обход всех узлов в корневом элементе
    foreach (XmlElement xnode in xRoot)
    {
        // получаем атрибут name
        XmlNode? attr = xnode.Attributes.GetNamedItem("name");
        Console.WriteLine(attr?.Value);
 
        // обходим все дочерние узлы элемента user
        foreach (XmlNode childnode in xnode.ChildNodes)
        {
            // если узел - company
            if (childnode.Name == "company")
            {
                Console.WriteLine($"Company: {childnode.InnerText}");
            }
            // если узел age
            if (childnode.Name == "age")
            {
                Console.WriteLine($"Age: {childnode.InnerText}");
            }
        }
        Console.WriteLine();
    }
}
```
В итоге я получу следующий вывод на консоли:

```
Tom
Company: Microsoft
Age: 37

Bob
Company: Google
Age: 41
```

Чтобы начать работу с документом xml, нам надо создать объект XmlDocument и затем загрузить в него xml-файл: xDoc.Load("people.xml");

При разборе xml для начала мы получаем корневой элемент документа с помощью свойства xDoc.DocumentElement. Далее уже происходит собственно разбор узлов документа.

В цикле foreach(XmlNode xnode in xRoot) пробегаемся по всем дочерним узлам корневого элемента. Так как дочерние узлы представляют элементы <person>, то мы можем получить их атрибуты: XmlNode attr = xnode.Attributes.GetNamedItem("name"); и вложенные элементы: foreach(XmlNode childnode in xnode.ChildNodes)

Чтобы определить, что за узел перед нами, мы можем сравнить его название: if(childnode.Name=="company")

Подобным образом мы можем создать объекты классов и структур по данным из xml:

```Csharp
using System.Xml;
 
var people = new List<Person>();
 
XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
// получим корневой элемент
XmlElement? xRoot = xDoc.DocumentElement;
if (xRoot != null)
{
    foreach (XmlElement xnode in xRoot)
    {
        Person person = new Person();
        XmlNode? attr = xnode.Attributes.GetNamedItem("name");
        person.Name = attr?.Value;
 
        foreach (XmlNode childnode in xnode.ChildNodes)
        {
            if (childnode.Name == "company")
                person.Company = childnode.InnerText;
 
            if (childnode.Name == "age")
                person.Age = int.Parse(childnode.InnerText);
        }
        people.Add(person);
    }
    foreach (var person in people)
        Console.WriteLine($"{person.Name} ({person.Company}) - {person.Age}");
}
 
class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Company { get; set; }
}
```
В данном случае определен класс Person с тремя свойствами. При переборе узлов файла xml значения элементов и их атрибутов передается объекту класса Person.

Консольный вывод программы:

```
Tom (Microsoft) - 37
Bob (Google) - 41
```

# Изменение XML-документа

Для редактирования xml-документа (изменения, добавления, удаления элементов) мы можем воспользоваться методами класса XmlNode:

- AppendChild: добавляет в конец текущего узла новый дочерний узел

- InsertAfter: добавляет новый узел после определенного узла

- InsertBefore: добавляет новый узел до определенного узла

- RemoveAll: удаляет все дочерние узлы текущего узла

- RemoveChild: удаляет у текущего узла один дочерний узел и возвращает его

- Класс XmlDocument добавляет еще ряд методов, которые позволяют создавать новые узлы:

- CreateNode: создает узел любого типа

- CreateElement: создает узел типа XmlDocument

- CreateAttribute: создает узел типа XmlAttribute

- CreateTextNode: создает узел типа XmlTextNode

- CreateComment: создает комментарий

Возьмем xml-документ people.xml из прошлой темы:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```
Добавим в этот xml-документ новый элемент <person>:

```Csharp
using System.Xml;
 
XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
XmlElement? xRoot = xDoc.DocumentElement;
 
// создаем новый элемент person
XmlElement personElem = xDoc.CreateElement("person");
 
// создаем атрибут name
XmlAttribute nameAttr = xDoc.CreateAttribute("name");
 
// создаем элементы company и age
XmlElement companyElem = xDoc.CreateElement("company");
XmlElement ageElem = xDoc.CreateElement("age");
 
// создаем текстовые значения для элементов и атрибута
XmlText nameText = xDoc.CreateTextNode("Mark");
XmlText companyText = xDoc.CreateTextNode("Facebook");
XmlText ageText = xDoc.CreateTextNode("30");
 
//добавляем узлы
nameAttr.AppendChild(nameText);
companyElem.AppendChild(companyText);
ageElem.AppendChild(ageText);
 
// добавляем атрибут name
personElem.Attributes.Append(nameAttr);
// добавляем элементы company и age
personElem.AppendChild(companyElem);
personElem.AppendChild(ageElem);
// добавляем в корневой элемент новый элемент person
xRoot?.AppendChild(personElem);
// сохраняем изменения xml-документа в файл
xDoc.Save("people.xml");
```

Добавление элементов происходит по одной схеме. Сначала создаем элемент (xDoc.CreateElement("person")). Если элемент сложный, то есть содержит в себе другие элементы, то создаем эти элементы. Если элемент простой, содержащий внутри себя некоторое текстовое значение, то создаем этот текст (XmlText companyText = xDoc.CreateTextNode("Facebook");).

Затем все элементы добавляются в основной элемент person, а тот добавляется в корневой элемент (xRoot.AppendChild(personElem);).

Чтобы сохранить измененный документ на диск, используем метод Save: xDoc.Save("people.xml")

После этого в xml-файле появится следующий элемент:

```xml
<?xml version="1.0" encoding="utf-8"?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
  <people name="Mark">
    <company>Facebook</company>
    <age>30</age>
  </people>
</people>
```
## Удаление узлов
Удалим первый узел xml-документа:

```Csharp
using System.Xml;
 
XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
XmlElement? xRoot = xDoc.DocumentElement;
XmlNode? firstNode = xRoot?.FirstChild;
if(firstNode is not null) xRoot?.RemoveChild(firstNode);
xDoc.Save("people.xml");
```

# XPath

XPath представляет язык запросов в XML. Он позволяет выбирать элементы, соответствующие определенному селектору.

Рассмотрим некоторые наиболее распространенные селекторы:

- .

выбор текущего узла

- ..

выбор родительского узла

- *

выбор всех дочерних узлов текущего узла

- person

выбор всех узлов с определенным именем, в данном случае с именем "person"

- @name

выбор атрибута текущего узла, после знака @ указывается название атрибута (в данном случае "name")

- @*

выбор всех атрибутов текущего узла

- element[3]

выбор определенного дочернего узла по индексу, в данном случае третьего узла

- //person

выбор в документе всех узлов с именем "person"

- person[@name='Tom']

выбор элементов с определенным значением атрибута. В данном случае выбираются все элементы "person" с атрибутом name='Tom'

- person[company='Microsoft']

выбор элементов с определенным значением вложенного элемента. В данном случае выбираются все элементы "person", у которых дочерний элемент "company" имеет значение 'Microsoft'

- //person/company

выбор в документе всех узлов с именем "company", которые находятся в элементах "person"

Действие запросов XPath основано на применении двух методов класса XmlElement:

- SelectSingleNode(): выбор единственного узла из выборки. Если выборка по запросу содержит несколько узлов, то выбирается первый

- SelectNodes(): выборка по запросу коллекции узлов в виде объекта XmlNodeList

Для запросов возьмем xml-документ из прошлых тем:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```
Теперь выберем все узлы корневого элемента, то есть все элементы person:

```Csharp
using System.Xml;
 
XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
XmlElement? xRoot = xDoc.DocumentElement;
 
// выбор всех дочерних узлов
XmlNodeList? nodes = xRoot?.SelectNodes("*");
if (nodes is not null)
{
    foreach (XmlNode node in nodes)
        Console.WriteLine(node.OuterXml);
}
```
Консольный вывод:

```
<person name="Tom"><company>Microsoft</company><age>37</age></person>
<person name="Bob"><company>Google</company><age>41</age></person>
```

Выберем все узлы ```<person>```:

```Csharp
XmlNodeList? personNodes = xRoot?.SelectNodes("person");
```
Выведем на консоль значения атрибутов name у элементов person:

```Csharp
using System.Xml;
 
XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
XmlElement? xRoot = xDoc.DocumentElement;
XmlNodeList? personNodes = xRoot?.SelectNodes("person");
if(personNodes is not null)
{
    foreach (XmlNode node in personNodes)
        Console.WriteLine(node.SelectSingleNode("@name")?.Value);
}
```
Результатом выполнения будет следующий вывод:

```
Tom
Bob
```
Выберем узел, у которого атрибут name имеет значение "Tom":

```Csharp
XmlNode? tomNode = xRoot?.SelectSingleNode("person[@name='Tom']");
Console.WriteLine(tomNode?.OuterXml);
```
Допустим, нам надо получить только компании. Для этого надо осуществить выборку вниз по иерархии элементов:

```Csharp
using System.Xml;
 
XmlDocument xDoc = new XmlDocument();
xDoc.Load("people.xml");
XmlElement? xRoot = xDoc.DocumentElement;
 
XmlNodeList? companyNodes = xRoot?.SelectNodes("//person/company");
if(companyNodes is not null)
{
    foreach (XmlNode node in companyNodes)
        Console.WriteLine(node.InnerText);
}
```

# Linq to Xml. Создание документа XML

Еще один подход к работе с Xml представляет технология LINQ to XML. Вся функциональность LINQ to XML содержится в пространстве имен System.Xml.Linq. Рассмотрим основные классы этого пространства имен:

- XAttribute: представляет атрибут xml-элемента

- XComment: представляет комментарий

- XDocument: представляет весь xml-документ

- XElement: представляет отдельный xml-элемент

Ключевым классом является XElement, который позволяет получать вложенные элементы и управлять ими. Среди его методов можно отметить следующие:

- Add(): добавляет новый атрибут или элемент

- Attributes(): возвращает коллекцию атрибутов для данного элемента

- Elements(): возвращает все дочерние элементы данного элемента

- Remove(): удаляет данный элемент из родительского объекта

- RemoveAll(): удаляет все дочерние элементы и атрибуты у данного элемента

Итак, используем функциональность LINQ to XML и создадим новый XML-документ:

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = new XDocument();
// создаем первый элемент person
XElement tom = new XElement("person");
// создаем атрибут name
XAttribute tomNameAttr = new XAttribute("name", "Tom");
// создаем два элемента company и age 
XElement tomCompanyElem = new XElement("company", "Microsoft");
XElement tomAgeElem = new XElement("age", 37);
// добавляем атрибут и элементы в первый элемент person
tom.Add(tomNameAttr);
tom.Add(tomCompanyElem);
tom.Add(tomAgeElem);
 
// создаем второй элемент person
XElement bob = new XElement("person");
// создаем для него атрибут name
XAttribute bobNameAttr = new XAttribute("name", "Bob");
// и два элемента - company и age
XElement bobCompanyElem = new XElement("company", "Google");
XElement bobAgeElem = new XElement("age", 41);
bob.Add(bobNameAttr);
bob.Add(bobCompanyElem);
bob.Add(bobAgeElem);
// создаем корневой элемент
XElement people = new XElement("people");
// добавляем два элемента person в корневой элемент
people.Add(tom);
people.Add(bob);
// добавляем корневой элемент в документ
xdoc.Add(people);
//сохраняем документ
xdoc.Save("people.xml");
 
Console.WriteLine("Data saved");
```

Чтобы создать документ, нам нужно создать объект класса XDocument. Это объект самого верхнего уровня в хml-документе.

Элементы создаются с помощью конструктора класса XElement. Конструктор имеет ряд перегруженных версий. Первый параметр конструктора передает название элемента, например, person. Второй параметр передает значение этого элемента.

Создание атрибута аналогично созданию элемента. Затем все атрибуты и элементы добавляются в элементы person с помощью метода Add().

Так как документ xml должен иметь один корневой элемент, то затем все элементы person добавляются в один контейнер - элемент people.

В конце корневой элемент добавляется в объект XDocument, и этот объект сохраняется на диске в xml-файл с помощью метода Save().

Если мы откроем сохраненный файл people.xml, то увидим в нем следующее содержание:

```xml
<?xml version="1.0" encoding="utf-8"?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```
Конструктор класса XElement позволяют задать набор объектов, которые будут входить в элемент. И предыдущий пример мы могли бы сократить следующим способом:

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = new XDocument(new XElement("people",
    new XElement("person",
        new XAttribute("name", "Tom"),
        new XElement("company", "Microsoft"),
        new XElement("age", 37)),
    new XElement("person",
        new XAttribute("name", "Bob"),
        new XElement("company", "Google"),
        new XElement("age", 41))));
xdoc.Save("people2.xml");
 
Console.WriteLine("Data saved");
```

# Выборка элементов в LINQ to XML

Большим преимуществом LINQ to XML является то, что эта технология позволяет легко извлекать нужные элементы из документа xml. Например, возьмем xml-файл people.xml, созданный в прошлой теме:

```xml
<?xml version="1.0" encoding="utf-8"?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```
Переберем его элементы и выведем их значения на консоль:

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = XDocument.Load("people.xml");
// получаем корневой узел
XElement? people = xdoc.Element("people");
if (people is not null)
{
    // проходим по всем элементам person
    foreach (XElement person in people.Elements("person"))
    {
 
        XAttribute? name = person.Attribute("name");
        XElement? company = person.Element("company");
        XElement? age = person.Element("age");
 
        Console.WriteLine($"Person: {name?.Value}");
        Console.WriteLine($"Company: {company?.Value}");
        Console.WriteLine($"Age: {age?.Value}");
 
        Console.WriteLine(); //  для разделения при выводе на консоль
    }
}
```
И мы получим следующий вывод:

```
Person: Tom
Company: Microsoft
Age: 37

Person: Bob
Company: Google
Age: 41
```
Чтобы начать работу с имеющимся xml-файлом, надо сначала загрузить его с помощью статического метода XDocument.Load(), в который передается путь к файлу.

```Csharp
XDocument xdoc = XDocument.Load("people.xml");
```
Поскольку xml хранит иерархически выстроенные элементы, то и для доступа к элементам надо идти начиная с высшего уровня в этой иерархии и далее вниз. Так, для получения элементов person и доступа к ним надо сначала обратиться к корневому элементу, а через него уже к элементам person:

```Csharp
// получаем корневой узел
XElement? people = xdoc.Element("people");
if (people is not null)
{
    // проходим по всем элементам person
    foreach (XElement person in people.Elements("person"))
    {
```
Метод Element("имя_элемента") возвращает первый найденный элемент с таким именем. Метод Elements("имя_элемента") возвращает коллекцию одноименных элементов. В данном случае мы получаем коллекцию элементов person и поэтому можем перебрать ее в цикле.

Спускаясь дальше по иерархии вниз, мы можем получить атрибуты или вложенные элементы, например, получение элемента ```<company>```

```Csharp
XElement? company = person.Element("company");
```
Значение простых элементов, которые содержат один текст, можно получить с помощью свойства Value:7

```Csharp
string? companyValue = person.Element("company")?.Value;
```

Сочетая операторы Linq и LINQ to XML можно довольно просто извлечь из документа данные и затем обработать их. Например:

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = XDocument.Load("people.xml");
 
var microsoft = xdoc.Element("people")?   // получаем корневой узел people
    .Elements("person")             // получаем все элементы person
    // получаем все person, у которого company = Microsoft
    .Where(p => p.Element("company")?.Value == "Microsoft")    
    .Select(p => new        // для каждого объекта создаем анонимный объект
    {
      name = p.Attribute("name")?.Value,
      age = p.Element("age")?.Value,
      company = p.Element("company")?.Value
  });
 
if (microsoft is not null)
{
    foreach (var person in microsoft)
    {
        Console.WriteLine($"Name: {person.name}  Age: {person.age}");
    }
}
```

В данном случае выбираем все элементы person, у которых вложенный элемент "company" равен "Microsoft". Далее на основе полученной выборке создаем набор анонимных объектов с тремя свойствами. Под вывод также можно было бы создать специально какой-нибудь класс или структуру и использовать их вместо анонимного объекта.

Другой пример - выберем элемент person, в котором атрибут name равен "Tom":

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = XDocument.Load("people.xml");
 
var tom = xdoc.Element("people")?   // получаем корневой узел people
    .Elements("person")             // получаем все элементы person
    .FirstOrDefault(p => p.Attribute("name")?.Value == "Tom");
 
var name = tom?.Attribute("name")?.Value;
var age = tom?.Element("age")?.Value;
var company = tom?.Element("company")?.Value;
 
Console.WriteLine($"Name: {name}  Age: {age}  Company: {company}");
```

# Изменение документа в LINQ to XML

Возьмем xml-файл people.xml из прошлых тем:

```xml
<?xml version="1.0" encoding="utf-8"?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```

## Добавление данных
Для добавления данных в документ xml у объекта XElement применяется метод Add(), в который передается добавляемый объект:

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = XDocument.Load("people.xml");
XElement? root = xdoc.Element("people");
 
if(root != null)
{
    // добавляем новый элемент
    root.Add(new XElement("person",
                new XAttribute("name", "Sam"),
                new XElement("company", "JetBrains"),
                new XElement("age", 28)));
 
    xdoc.Save("people.xml");
}
 
// выводим xml-документ на консоль
Console.WriteLine(xdoc);
```
В результате сформируется и сохранится на диск новый документ:

```
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
  <person name="Sam">
    <company>JetBrains</company>
    <age>28</age>
  </person>
</people>
```

## Изменение данных
Для изменения данных в документ xml необходимо получить элемент, который надо изменить, и затем можно отредактировать значения его отдельных атрибутов или вложенных элементов. Изменим элемент person, в котором атрибут name = "Tom":

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = XDocument.Load("people.xml");
 
// получим элемент person с name = "Tom"
var tom = xdoc.Element("people")?
    .Elements("person")
    .FirstOrDefault(p => p.Attribute("name")?.Value == "Tom");
 
if(tom != null)
{
    //  меняем атрибут name
    var name = tom.Attribute("name");
    if (name != null) name.Value = "Tomas";
 
 
    //  меняем вложенный элемент age
    var age = tom.Element("age");
    if (age != null) age.Value = "22";
 
    xdoc.Save("people.xml");
}
 
// выводим xml-документ на консоль
Console.WriteLine(xdoc);
```
В результате сформируется и сохранится на диск новый документ:

```
<people>
  <person name="Tomas">
    <company>Microsoft</company>
    <age>22</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
  <person name="Sam">
    <company>JetBrains</company>
    <age>28</age>
  </person>
</people>
```

## Удаление данных
Для удаления данных в документе xml у удаляемого объекта XElement вызывается метод Remove(). Например, удалим элемент person, в котором атрибут name = "Bob":

```Csharp
using System.Xml.Linq;
 
XDocument xdoc = XDocument.Load("people.xml");
XElement? root = xdoc.Element("people");
 
if (root != null)
{
    // получим элемент person с name = "Bob"
    var bob = root.Elements("person")
        .FirstOrDefault(p => p.Attribute("name")?.Value == "Bob");
    // и удалим его
    if (bob != null)
    {
        bob.Remove();
        xdoc.Save("people.xml");
    }
}
 
// выводим xml-документ на консоль
Console.WriteLine(xdoc);
```

В результате сформируется и сохранится на диск новый документ:

```
<people>
  <person name="Tomas">
    <company>Microsoft</company>
    <age>22</age>
  </person>
  <person name="Sam">
    <company>JetBrains</company>
    <age>28</age>
  </person>
</people>
```
Соответственно, если необходимо удалить атрибут, то у удаляемого объекта XAttribute также вызывается метод Remove.

# Сериализация в XML. XmlSerializer

Для удобного сохранения и извлечения объектов из файлов xml может использоваться класс XmlSerializer из пространства имен System.Xml.Serialization. Данный класс упрощает сохранение сложных объектов в формат xml и последующее их извлечение.

Для создания объекта XmlSerializer можно применять различные конструкторы, но почти все они требуют указания типа данных, которые будут сериализоваться и десериализоваться:

```xml
XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
 
//[Serializable]
class Person { }
```
В данном случае XmlSerializer будет работать только с объектами класса Person.

Следует учитывать, что XmlSerializer предполагает некоторые ограничения. Например, класс, подлежащий сериализации, должен иметь стандартный конструктор без параметров и должен иметь модификатор доступа public. Также сериализации подлежат только открытые члены. Если в классе есть поля или свойства с модификатором private, то при сериализации они будут игнорироваться. Кроме того, свойства должны иметь общедоступные геттеры и сеттеры.

## Сериализация
Для сериализации (то есть сохранения в форма xml) применяется метод Serialize(). Данный метод имеет ряд версий. Возьмем самую простую из них:

```Csharp
void Serialize (Stream stream, object? o);
```
В качестве первого параметра передается поток Stream (например, объект FileStream), в который будет идти сериализация. А второй параметр представляет собственно тот объект, который будет сохраняться в формат xml. Например:

```Csharp
using System.Xml.Serialization;
 
// объект для сериализации
Person person = new Person("Tom", 37);
 
// передаем в конструктор тип класса Person
XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
 
// получаем поток, куда будем записывать сериализованный объект
using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
{
    xmlSerializer.Serialize(fs, person);
 
    Console.WriteLine("Object has been serialized");
}
 
//[Serializable]
public class Person
{
    public string Name { get; set; } = "Undefined";
    public int Age { get; set; } = 1;
 
    public Person() { }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```
Итак, класс Person общедоступный, имеет общедоступные свойства и конструктор без параметров, поэтому объекты этого класса подлежат сериализации. При создании объекта XmlSerializer передаем в конструктор тип класса Person.

В метод Serialize передается файловый поток для сохранения данных в файл person.xml и сохраняемый в этот файл объект Person. И если по завершению программы мы откроем файл person.xml, то увидим содержание нашего объекта:

```xml
<?xml version="1.0"?>
<Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Name>Tom</Name>
  <Age>37</Age>
</Person>
```
## Десериализация
Для десериализации данных xml в объект кода C# применяется метод Deserialize(). Отметим одну из версий этого метода:

```Csharp
object? Deserialize (Stream stream);
```

В качестве параметра в метод передается объект Stream, который содержит данные в формате xml. Результатом метода является десериализованный объект.

Например, десериализуем данные из выше созданного файла person.xml:

```Csharp
using System.Xml.Serialization;
 
XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
 
// десериализуем объект
using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
{
    Person? person = xmlSerializer.Deserialize(fs) as Person;
    Console.WriteLine($"Name: {person?.Name}  Age: {person?.Age}");
}
 
public class Person
{
    public string Name { get; set; } = "Undefined";
    public int Age { get; set; } = 1;
 
    public Person() { }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

## Сериализация и десериализация коллекций
Подобным образом мы можем сериализовать массив или коллекцию объектов:

```Csharp
using System.Xml.Serialization;
 
Person[] people = new Person[]
{
    new Person("Tom", 37),
    new Person("Bob", 41)
};
 
XmlSerializer formatter = new XmlSerializer(typeof(Person[]));
// сохранение массива в файл
using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
{
    formatter.Serialize(fs, people);
}
// восстановление массива из файла
using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
{
    Person[]? newpeople = formatter.Deserialize(fs) as Person[];
 
    if(newpeople != null)
    {
        foreach (Person person in newpeople)
        {
            Console.WriteLine($"Name: {person.Name} --- Age: {person.Age}");
        }
    }
}
 
public class Person
{
    public string Name { get; set; } = "Undefined";
    public int Age { get; set; } = 1;
 
    public Person() { }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

Консольный вывод:

```
Name: Tom --- Age: 37
Name: Bob --- Age: 41
```

Но это был простой объект. Однако с более сложными по составу объектами работать так же просто. Например:

```Csharp
using System.Xml.Serialization;
 
var microsoft = new Company("Microsoft");
var google = new Company("Google");
 
Person[] people = new Person[]
{
    new Person("Tom", 37, microsoft),
    new Person("Bob", 41, google)
};
 
XmlSerializer formatter = new XmlSerializer(typeof(Person[]));
 
using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
{
    formatter.Serialize(fs, people);
}
 
using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
{
    Person[]? newpeople = formatter.Deserialize(fs) as Person[];
 
    if(newpeople != null)
    {
        foreach (Person person in newpeople)
        {
            Console.WriteLine($"Name: {person.Name}");
            Console.WriteLine($"Age: {person.Age}");
            Console.WriteLine($"Company: {person.Company.Name}");
        }
    }
}
 
public class Company
{
    public string Name { get; set; } = "Undefined";
 
    // стандартный конструктор без параметров
    public Company() { }
 
    public Company(string name) => Name = name;
}
public class Person
{
    public string Name { get; set; } = "Undefined";
    public int Age { get; set; } = 1;
    public Company Company { get; set; } = new Company();
 
    public Person() { }
    public Person(string name, int age, Company company)
    {
        Name = name;
        Age = age;
        Company = company;
    }
}
```

Класс Person содержит свойство Company, которое будет хранить объект класса Company. Члены класса Company объявляются с модификатором public, кроме того также присутствует стандартный конструктор без параметров. В итоге после сериализации мы получим следующий xml-документ:

```xml
<?xml version="1.0"?>
<ArrayOfPerson xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Person>
    <Name>Tom</Name>
    <Age>37</Age>
    <Company>
      <Name>Microsoft</Name>
    </Company>
  </Person>
  <Person>
    <Name>Bob</Name>
    <Age>41</Age>
    <Company>
      <Name>Google</Name>
    </Company>
  </Person>
</ArrayOfPerson>
```
