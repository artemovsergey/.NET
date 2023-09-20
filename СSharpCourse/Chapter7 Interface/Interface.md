# Интерфейсы

## Определение интерфейсов

Интерфейс представляет ссылочный тип, который может определять некоторый функционал - набор методов и свойств без реализации. Затем этот функционал реализуют классы и структуры, которые применяют данные интерфейсы.

## Определение интерфейса

Для определения интерфейса используется ключевое слово interface. Как правило, названия интерфейсов в C# начинаются с заглавной буквы I, например, IComparable, IEnumerable (так называемая венгерская нотация), однако это не обязательное требование, а больше стиль программирования.

Что может определять интерфейс? В целом интерфейсы могут определять следующие сущности:

- Методы

- Свойства

- Индексаторы

- События

- Статические поля и константы (начиная с версии C# 8.0)

Однако интерфейсы не могут определять нестатические переменные. Например, простейший интерфейс, который определяет все эти компоненты:

```Csharp
interface IMovable
{
    // константа
    const int minSpeed = 0;     // минимальная скорость
    // статическая переменная
    static int maxSpeed = 60;   // максимальная скорость
    // метод
    void Move();                // движение
    // свойство
    string Name { get; set; }   // название
     
    delegate void MoveHandler(string message);  // определение делегата для события
    // событие
    event MoveHandler MoveEvent;    // событие движения
}
```

В данном случае определен интерфейс IMovable, который представляет некоторый движущийся объект. Данный интерфейс содержит различные компоненты, которые описывают возможности движущегося объекта. То есть интерфейс описывает некоторый функционал, который должен быть у движущегося объекта.

Методы и свойства интерфейса могут не иметь реализации, в этом они сближаются с абстрактными методами и свойствами абстрактных классов. В данном случае интерфейс определяет метод Move, который будет представлять некоторое передвижение. Он не имеет реализации, не принимает никаких параметров и ничего не возвращает.

То же самое в данном случае касается свойства Name. На первый взгляд оно похоже на автоматическое свойство. Но в реальности это определение свойства в интерфейсе, которое не имеет реализации, а не автосвойство.

### Модификаторы доступа

Еще один момент в объявлении интерфейса: если его члены - методы и свойства не имеют модификаторов доступа, то фактически по умолчанию доступ public, так как цель интерфейса - определение функционала для реализации его классом. Это касается также и констант и статических переменных, которые в классах и структурах по умолчанию имееют модификатор private. В интерфейсах же они имеют по умолчанию модификатор public. И например, мы могли бы обратиться к константе minSpeed и переменной maxSpeed интерфейса IMovable:

```Csharp
Console.WriteLine(IMovable.maxSpeed);   // 60
Console.WriteLine(IMovable.minSpeed);   // 0
```

Но также, начиная с версии C# 8.0, мы можем явно указывать модификаторы доступа у компонентов интерфейса:

```Csharp
interface IMovable
{
    public const int minSpeed = 0;     // минимальная скорость
    private static int maxSpeed = 60;   // максимальная скорость
    public void Move();
    protected internal string Name { get; set; }    // название
    public delegate void MoveHandler(string message);  // определение делегата для события
    public event MoveHandler MoveEvent;    // событие движения
}
```

Как и классы, интерфейсы по умолчанию имеют уровень доступа internal, то есть такой интерфейс доступен только в рамках текущего проекта. Но с помощью модификатора public мы можем сделать интерфейс общедоступным:

```Csharp
public interface IMovable
{
    void Move();
}
```

## Реализация по умолчанию

Также начиная с версии C# 8.0 интерфейсы поддерживают реализацию методов и свойств по умолчанию. Это значит, что мы можем определить в интерфейсах полноценные методы и свойства, которые имеют реализацию как в обычных классах и структурах. Например, определим реализацию метода Move по умолчанию:

```Csharp
interface IMovable
{
    // реализация метода по умолчанию
    void Move()
    {
        Console.WriteLine("Walking");
    }
}
```

С реализацией свойств по умолчанию в интерфейсах дело обстоит несколько сложнее, поскольку мы не можем определять в интерфейсах нестатические переменные, соответственно в свойствах интерфейса мы не можем манипулировать состоянием объекта. Тем не менее реализацию по умолчанию для свойств мы тоже можем определять:

```Csharp
interface IMovable
{
    // реализация метода по умолчанию
    void Move() => Console.WriteLine("Walking");
    // реализация свойства по умолчанию
    // свойство только для чтения
    int MaxSpeed { get { return 0; } }
}
```

Стоит отметить, что если интерфейс имеет приватные методы и свойства (то есть с модификатором private), то они должны иметь реализацию по умолчанию. То же самое относится к статическим методам (не обязательно приватным):

```Csharp
Console.WriteLine(IMovable.MaxSpeed);   // 60
IMovable.MaxSpeed = 65;
Console.WriteLine(IMovable.MaxSpeed);   // 65
double time = IMovable.GetTime(500, 10);
Console.WriteLine(time);    // 50
 
interface IMovable
{
    public const int minSpeed = 0;     // минимальная скорость
    private static int maxSpeed = 60;   // максимальная скорость
    // находим время, за которое надо пройти расстояние distance со скоростью speed
    static double GetTime(double distance, double speed) => distance / speed;
    static int MaxSpeed
    {
        get => maxSpeed;
        set
        {
            if (value > 0) maxSpeed = value;
        }
    }
}
```

## Добавление интерфейса

Стоит отметить, что в Visual Studio есть специальный компонент для добавления нового интерфейса в отдельном файле. Для добавления интерфейса в проект можно нажать правой кнопкой мыши на проект и в появившемся контекстном меню выбрать Add-> New Item... и в диалоговом окне добавления нового компонента выбрать пункт Interface:

![](https://metanit.com/sharp/tutorial/pics/3.2.png)

Хотя мы также может добавить стандартный файл класса или любой другой файл кода C# и в нем определить интерфейс.

# Применение интерфейсов

Интерфейс представляет некое описание типа, набор компонентов, который должен иметь тип данных. И, собственно, мы не можем создавать объекты интерфейса напрямую с помощью конструктора, как например, в классах:

```Csharp
IMovable m = new IMovable(); // ! Ошибка, так сделать нельзя
 
interface IMovable
{
    void Move();
}
```

В конечном счете интерфейс предназначен для реализации в классах и структурах. Например, реализуем выше определенный интерфейс IMovable:

```Csharp
// применение интерфейса в классе
class Person : IMovable
{
    public void Move()
    {
        Console.WriteLine("Человек идет");
    }
}
// применение интерфейса в структуре
struct Car : IMovable
{
    public void Move()
    {
        Console.WriteLine("Машина едет");
    }
}
```

При применении интерфейса, как и при наследовании после имени класса или структуры указывается двоеточие и затем идут названия применяемых интерфейсов. При этом класс должен реализовать все методы и свойства применяемых интерфейсов, если эти методы и свойства не имеют реализации по умолчанию.

Если методы и свойства интерфейса не имеют модификатора доступа, то по умолчанию они являются публичными, при реализации этих методов и свойств в классе и структуре к ним можно применять только модификатор public.

Применение интерфейса в программе:

```Csharp
Person person = new Person();
Car car = new Car();
DoAction(person);
DoAction(car);
 
void DoAction(IMovable movable) => movable.Move();
 
interface IMovable
{
    void Move();
}
class Person : IMovable
{
    public void Move() => Console.WriteLine("Человек идет");
}
struct Car : IMovable
{
    public void Move() => Console.WriteLine("Машина едет");
}

```

В данной программе определен метод DoAction(), который в качестве параметра принимает объект интерфейса IMovable. На момент написания кода мы можем не знать, что это будет за объект - какой-то класс или структура. Единственное, в чем мы можем быть уверены, что этот объект обязательно реализует метод Move и мы можем вызвать этот метод.

Иными словами, интерфейс - это контракт, что какой-то определенный тип обязательно реализует некоторый функционал.

Консольный вывод данной программы:

```
Человек идет
Машина едет
```

## Реализация интерфейсов по умолчанию

Начиная с версии C# 8.0 интерфейсы поддерживают реализацию методов и свойств по умолчанию. Зачем это нужно? Допустим, у нас есть куча классов, которые реализуют некоторый интерфейс. Если мы добавим в этот интерфейс новый метод, то мы будем обязаны реализовать этот метод во всех классах, применяющих данный интерфейс. Иначе подобные классы просто не будут компилироваться. Теперь вместо реализации метода во всех классах нам достаточно определить его реализацию по умолчанию в интерфейсе. Если класс не реализует метод, будет применяться реализация по умолчанию.

```Csharp
IMovable tom = new Person();
Car tesla = new Car();
tom.Move();     // Walking
tesla.Move();   // Driving
interface IMovable
{
    void Move() => Console.WriteLine("Walking");
}
class Person : IMovable { }
class Car : IMovable
{
    public void Move() => Console.WriteLine("Driving");
}
```

В данном случае интерфейс IMovable определяет реализацию по умолчанию для метода Move. Класс Person не реализует этот метод, поэтому он применяет реализацию по умолчанию в отличие от класса Car, который определяет свою реализацию для метода Move.

Стоит отметить, что хотя для объекта класса Person мы можем вызвать метод Move - ведь класс Person применяет интерфейс IMovable, тем не менее мы не можем написать так:

```Csharp
Person tom = new Person();
tom.Move();     // Ошибка - метод Move не определен в классе Person
```

## Множественная реализация интерфейсов

Интерфейсы имеют еще одну важную функцию: в C# не поддерживается множественное наследование, то есть мы можем унаследовать класс только от одного класса, в отличие, скажем, от языка С++, где множественное наследование можно использовать. Интерфейсы позволяют частично обойти это ограничение, поскольку в C# классы и структуры могут реализовать сразу несколько интерфейсов. Все реализуемые интерфейсы указываются через запятую:

```Csharp
class myClass: myInterface1, myInterface2, myInterface3, ...
{
     
}
```

Рассмотрим на примере:

```Csharp
Message hello = new Message("Hello World");
hello.Print();  // Hello World
 
interface IMessage
{
    string Text { get; set; }
}
interface IPrintable
{
    void Print();
}
class Message : IMessage, IPrintable
{
    public string Text { get; set; }
    public Message(string text) => Text = text;
    public void Print()=> Console.WriteLine(Text);
}
```

В данном случае определены два интерфейса. Интерфейс IMessage определяет свойство Text, которое представляет текст сообщения. А интерфейс IPrintable определяет метод Print.

Класс Message реализует оба интерфейса и затем применяется в программе.

## Интерфейсы в преобразованиях типов

Все сказанное в отношении преобразования типов характерно и для интерфейсов. Поскольку класс Message реализует интерфейс IMessage, то переменная типа IMessage может хранить ссылку на объект типа Message:

```Csharp
// Все объекты Message являются объектами IMessage
IMessage hello = new Message("Hello METANIT.COM");
Console.WriteLine(hello.Text); // Hello METANIT.COM
 
// Не все объекты IMessage являются объектами Message, необходимо явное приведение
// Message someMessage = hello; // ! Ошибка
 
// Интерфейс IMessage не имеет свойства Print, необходимо явное приведение
// hello.Print();  // ! Ошибка
 
// если hello представляет класс Message, выполняем преобразование
if (hello is Message someMessage) someMessage.Print();
```

Преобразование от класса к его интерфейсу, как и преобразование от производного типа к базовому, выполняется автоматически. Так как любой объект Message реализует интерфейс IMessage.

Обратное преобразование - от интерфейса к реализующему его классу будет аналогично преобразованию от базового класса к производному. Так как не каждый объект IMessage является объектом Message (ведь интерфейс IMessage могут реализовать и другие классы), то для подобного преобразования необходима операция приведения типов. И если мы хотим обратиться к методам класса Message, которые не определены в интерфейсе IMessage, но являются частью класса Message, то нам надо явным образом выполнить преобразование типов:

```Csharp
if (hello is Message someMessage) someMessage.Print();
```

# Явная реализация интерфейсов

Кроме неявного применения интерфейсов, которое было рассмотрено в прошлой статье, сушествует также явная реализация интерфейса. При явной реализации указывается название метода или свойства вместе с названием интерфейса, при этом мы не можем использовать модификатор public, то есть методы являются закрытыми:

```Csharp
interface IAction
{
    void Move();
}
class BaseAction : IAction
{
    void IAction.Move() => Console.WriteLine("Move in Base Class");
}
```

Следует учитывать, что при явной реализации интерфейса его методы и свойства не являются частью интерфейса класса. Поэтому напрямую через объект класса мы к ним обратиться не сможем:

```Csharp
BaseAction baseAction1 = new BaseAction();
 
// baseAction1.Move();  // ! Ошибка - в BaseAction нет метода Move
// необходимо приведение к типу IAction
// небезопасное приведение
((IAction)baseAction1).Move();   
// безопасное приведение 
if (baseAction1 is IAction action) action.Move();
// или так
IAction baseAction2 = new BaseAction();
baseAction2.Move();
```

В какой ситуации может действительно понадобиться явная реализация интерфейса? Например, когда класс применяет несколько интерфейсов, но они имеют один и тот же метод с одним и тем же возвращаемым результатом и одним и тем же набором параметров:

```Csharp
class Person : ISchool, IUniversity
{
    public void Study() => Console.WriteLine("Учеба в школе или в университете");
}
interface ISchool
{
    void Study();
}
interface IUniversity
{
    void Study();
}
```

Класс Person определяет один метод Study(), создавая одну общую реализацию для обоих примененных интерфейсов. И вне зависимости от того, будем ли мы рассматривать объект Person как объект типа ISchool или IUniversity, результат метода будет один и тот же.

Чтобы разграничить реализуемые интерфейсы, надо явным образом применить интерфейс:

```Csharp
class Person : ISchool, IUniversity
{
    void ISchool.Study() => Console.WriteLine("Учеба в школе");
    void IUniversity.Study() => Console.WriteLine("Учеба в университете");
}
```

Использование:

```Csharp
Person person = new Person();
 
((ISchool)person).Study();
((IUniversity)person).Study();
```

Другая ситуация, когда в базовом классе уже реализован интерфейс, но необходимо в производном классе по-своему реализовать интерфейс:

```Csharp
interface IAction
{
    void Move();
}
class BaseAction : IAction
{
    public void Move() =>Console.WriteLine("Move in BaseAction");
}
class HeroAction : BaseAction, IAction
{
    void IAction.Move() => Console.WriteLine("Move in HeroAction");
}
```

Несмотря на то, что базовый класс BaseAction уже реализовал интерфейс IAction, но производный класс по-своему реализует его. Применение классов:

```Csharp
HeroAction action1 = new HeroAction();
action1.Move();            // Move in BaseAction
((IAction)action1).Move(); // Move in HeroAction
 
IAction action2 = new HeroAction();
action2.Move();             // Move in HeroAction
```

## Модификаторы доступа

Члены интерфейса могут иметь разные модификаторы доступа. Если модификатор доступа не public, а какой-то другой, то для реализации метода, свойства или события интерфейса в классах и структурах также необходимо использовать явную реализацию интерфейса.

```Csharp
IMovable tom = new Person("Tom");
// подписываемся на событие
tom.MoveEvent += () => Console.WriteLine($"{tom.Name} is moving");
tom.Move();
 
interface IMovable
{
    protected internal void Move();
    protected internal string Name { get;}
    delegate void MoveHandler();
    protected internal event MoveHandler MoveEvent;
}
class Person : IMovable
{
    string name;
    // явная реализация события - дополнительно создается переменная
    IMovable.MoveHandler? moveEvent;
    event IMovable.MoveHandler IMovable.MoveEvent
    {
        add => moveEvent += value;
        remove => moveEvent -= value;
    }
    // явная реализация свойства - в виде автосвойства
    string IMovable.Name { get => name; }
    public Person(string name) => this.name = name;
    // явная реализация метода
    void IMovable.Move()
    {
        Console.WriteLine($"{name} is walking");
        moveEvent?.Invoke();
    }
}
```

В данном случае опять же надо учитывать, что напрямую мы можем обратиться к подобным методам, свойствам и событиям через переменную интерфейса, но не переменную класса.

# Реализация интерфейсов в базовых и производных классах

Если класс применяет интерфейс, то этот класс должен реализовать все методы и свойства интерфейса, которые не имеют реализации по умолчанию. Однако также можно и не реализовать методы, сделав их абстрактными, переложив право их реализации на производные классы:

```Csharp
interface IMovable
{
    void Move();
}
abstract class Person : IMovable
{
    public abstract void Move();
}
class Driver : Person
{
    public override void Move() => Console.WriteLine("Шофер ведет машину");
}
```

При реализации интерфейса учитываются также методы и свойства, унаследованные от базового класса. Например:

```Csharp
IAction action = new HeroAction();
action.Move();  // Move in BaseAction
 
interface IAction
{
    void Move();
}
class BaseAction
{
    public void Move() => Console.WriteLine("Move in BaseAction");
}
class HeroAction : BaseAction, IAction{ }
```

Здесь класс HeroAction реализует интерфейс IAction, однако для реализации метода Move из интерфейса применяется метод Move, унаследованный от базового класса BaseAction. Таким образом, класс HeroAction может не реализовать метод Move, так как этот метод уже определен в базовом классе BaseAction.

Следует отметить, что если класс одновременно наследует другой класс и реализует интерфейс, как в примере выше класс HeroAction, то название базового класса должно быть указано до реализуемых интерфейсов:

```
class HeroAction : BaseAction, IAction
```

## Изменение реализации интерфейсов в производных классах

Может сложиться ситуация, что базовый класс реализовал интерфейс, но в классе-наследнике необходимо изменить реализацию этого интерфейса. Что в этом случае делать? В этом случае мы можем использовать либо переопределение, либо скрытие метода или свойства интерфейса.

Первый вариант - переопределение виртуальных/абстрактных методов:

```Csharp
interface IAction
{
    void Move();
}
class BaseAction : IAction
{
    public virtual void Move() => Console.WriteLine("Move in BaseAction");
}
class HeroAction : BaseAction
{
    public override void Move() => Console.WriteLine("Move in HeroAction");
}
```

В базовом классе BaseAction реализованный метод интерфейса определен как виртуальный (можно было бы также сделать его абстрактным), а в производном классе он переопределен.

При вызове метода через переменную интерфейса, если она ссылается на объект производного класса, будет использоваться реализация из производного класса:

```Csharp
BaseAction action1 = new HeroAction();
action1.Move();            // Move in HeroAction
 
IAction action2 = new HeroAction();
action2.Move();             // Move in HeroAction
```

Второй вариант - скрытие метода в производном классе:

```Csharp
interface IAction
{
    void Move();
}
class BaseAction : IAction
{
    public void Move() => Console.WriteLine("Move in BaseAction");
 
}
class HeroAction : BaseAction
{
    public new void Move() => Console.WriteLine("Move in HeroAction");
}
```

Также используем эти классы:

```Csharp
BaseAction action1 = new HeroAction();
action1.Move();            // Move in BaseAction
 
IAction action2 = new HeroAction();
action2.Move();           // Move in BaseAction
```

Так как интерфейс реализован именно в классе BaseAction, то через переменную action2 можно обратиться только к реализации метода Move из базового класса BaseAction.

Третий вариант - повторная реализация интерфейса в классе-наследнике:

```Csharp
interface IAction
{
    void Move();
}
class BaseAction : IAction
{
    public void Move() => Console.WriteLine("Move in BaseAction");
}
class HeroAction : BaseAction, IAction
{
    public new void Move() =>  Console.WriteLine("Move in HeroAction");
}
```

В этом случае реализации этого метода из базового класса будет игнорироваться:

```Csharp
BaseAction action1 = new HeroAction();
action1.Move();            // Move in BaseAction
 
IAction action2 = new HeroAction();
action2.Move();             // Move in HeroAction
 
HeroAction action3 = new HeroAction();
action3.Move();             // Move in HeroAction
```

Также стоит отметить, что в случае с переменной action1 по-прежнему действует ранее связывание, в силу которого через эту переменную можно вызвать реализацию метода Move только из базового класса, который эта переменная представляет.

Четвертый вариант: явная реализация интерфейса:

```Csharp
interface IAction
{
    void Move();
}
class BaseAction : IAction
{
    public void Move() => Console.WriteLine("Move in BaseAction");
}
class HeroAction : BaseAction, IAction
{
    public new void Move() => Console.WriteLine("Move in HeroAction");
    // явная реализация интерфейса
    void IAction.Move() => Console.WriteLine("Move in IAction");
}
```

В этом случае для переменной IAction будет использоваться явная реализация интерфейса IAction, а для переменной HeroAction по прежнему будет использоваться неявная реализация:

```Csharp
BaseAction action1 = new HeroAction();
action1.Move();            // Move in BaseAction
 
IAction action2 = new HeroAction();
action2.Move();             // Move in IAction
 
HeroAction action3 = new HeroAction();
action3.Move();             // Move in HeroAction
```

# Наследование интерфейсов

Интерфейсы, как и классы, могут наследоваться:

```Csharp
interface IAction
{
    void Move();
}
interface IRunAction : IAction
{
    void Run();
}
class BaseAction : IRunAction
{
    public void Move()
    {
        Console.WriteLine("Move");
    }
    public void Run()
    {
        Console.WriteLine("Run");
    }
}
```

При применении этого интерфейса класс BaseAction должен будет реализовать как методы и свойства интерфейса IRunAction, так и методы и свойства базового интерфейса IAction, если эти методы и свойства не имеют реализации по умолчанию.

Однако в отличие от классов мы не можем применять к интерфейсам модификатор sealed, чтобы запретить наследование интерфейсов.

Также мы не можем применять к интерфейсам модификатор abstract, поскольку интерфейс фактически итак, как правило, предоставляет абстрактный функционал, который должен быть реализован в классе или структуре (за исключением методов и свойств с реализацией по умолчанию).

Однако методы интерфейсов могут использовать ключевое слово new для скрытия методов из базового интерфейса:

```Csharp
IAction action1 = new RunAction();
action1.Move(); // I am moving
 
IRunAction action2 = new RunAction();
action2.Move(); // I am running
 
interface IAction
{
    void Move() => Console.WriteLine("I am moving");
}
interface IRunAction : IAction
{
    // скрываем реализацию из IAction
    new void Move() => Console.WriteLine("I am running");
}
class RunAction : IRunAction { }
```

Здесь метод Move из IRunAction скрывает метод Move из базового интерфейса IAction. Это имеет смысл, если в базовом интерфейсе определена реализация по умолчанию, как в случае выше, которую нужно переопределить. И в случае выше, если переменная представляет тип IRunAction, то для метода Move вызывается реализация этого интерфейса:

```Csharp
IRunAction action2 = new RunAction();
action2.Move(); // I am running
```

Иначе если переменная представляет тип IAction, то для метода Move применяется реализация этого интерфейса:

```Csharp
IAction action1 = new RunAction();
action1.Move(); // I am moving
```

Но класс RunAction может переопределить метод Move сразу для обоих интерфейсов.

```Csharp
IAction action1 = new RunAction();
action1.Move(); // I am tired
 
IRunAction action2 = new RunAction();
action2.Move(); // I am tired
 
interface IAction
{
    void Move() => Console.WriteLine("I am moving");
}
interface IRunAction : IAction
{
    new void Move() => Console.WriteLine("I am running");
}
class RunAction : IRunAction
{
    public void Move() => Console.WriteLine("I am tired");
}
```

При наследовании интерфейсов следует учитывать, что, как и при наследовании классов, производный интерфейс должен иметь тот же уровень доступа или более строгий, чем базовый интерфейс. Например:

```Csharp
public interface IAction
{
    void Move();
}
internal interface IRunAction : IAction
{
    void Run();
}
```

Но не наоборот. Например, в следующем случае мы получим ошибку, и программа не скомпилируется, так как производный интерфейс имеет менее строгий уровень доступа, нежели базовый:

```Csharp
internal interface IAction
{
    void Move();
}
public interface IRunAction : IAction   // ошибка IRunAction может быть только internal
{
    void Run();
}
```

# Интерфейсы в обобщениях

## Интерфейсы как ограничения обобщений

Интерфейсы могут выступать в качестве ограничений обобщений. При этом если в качестве ограничения можно указаь только один класс, то интерфейсов можно указать несколько.

Допустим, у нас есть следующие интерфейсы и класс, который их реализует:

```Csharp
interface IMessage
{
    string Text { get; } // текст сообщения
}
interface IPrintable
{
    void Print();
}
class Message : IMessage, IPrintable
{
    public string Text { get; }
    public Message(string text) => Text = text;
 
    public void Print() => Console.WriteLine(Text);
}
```

Интерфейс IMessage представляет интерфейс сообщения и определяет свойство Text для хранения текста сообщения. Интерфейс IPrintable определяет метод Print для условной печати сообщения. И непосредственно класс сообщения - класс Message реализует эти интерфейсы.

Используем выше перечисленные интерфейсы в качестве ограничений обобщенного класса:

```Csharp
class Messenger<T> where T: IMessage, IPrintable
{
    public void Send(T message)
    {
        Console.WriteLine("Отправка сообщения:");
        message.Print();
    }
}
```

В данном случае класс условного мессенджера использует параметр T - тип, который который реализует сразу два интерфейса IMessage и IPrintable. Например, выше определен класс Message, который реализует оба интерфейса, поэтому мы можем данным типом типизировать объекты Messenger:

```Csharp
// создаем мессенджер
var telegram = new Messenger<Message>();
// создаем сообщение
var message = new Message("Hello World!");
// отправляем сообщение
telegram.Send(message);
```

Также параметр T может представлять интерфейс, который наследуется от обоих интерфейсов:

```Csharp
interface IPrintableMessage: IPrintable, IMessage { }
class PrintableMessage : IPrintableMessage
{
    public string Text { get; }
    public PrintableMessage(string text) => Text = text;
    public void Print() => Console.WriteLine(Text);
}
```

В этом случае объекты Messenger мы можем типизировать типом IPrintableMessage:

```Csharp
var telegram = new Messenger<IPrintableMessage>();
var message = new PrintableMessage("Hello World!");
telegram.Send(message);
```

## Обобщенные интерфейсы

Как и классы, интерфейсы могут быть обобщенными:

```Csharp
interface IUser<T>
{
    T Id { get; }
}
class User<T> : IUser<T>
{
    public T Id { get; }
    public User(T id) => Id = id;
}
```

Интерфейс IUser типизирован параметром T, который при реализации интерфейса используется в классе User. В частности, переменная _id определена как T, что позволяет нам использовать для id различные типы.

Определим две реализации: одна в качестве параметра будет использовать тип int, а другая - тип string:

```Csharp
IUser<int> user1 = new User<int>(6789);
Console.WriteLine(user1.Id);    // 6789
 
IUser<string> user2 = new User<string>("12345");
Console.WriteLine(user2.Id);    // 12345
```

Также при реализации интерфейса мы можем явным образом указать, какой тип будет использоваться для параметра T:

```Csharp
class IntUser : IUser<int>
{
    public int Id { get; }
    public IntUser(int id) => Id = id;
}
```

Применение:

```Csharp
IUser<int> user1 = new IntUser(2345);
Console.WriteLine(user1.Id);    // 2345
 
IntUser user2 = new IntUser(9840);
Console.WriteLine(user2.Id);    // 9840
```

# Копирование объектов. Интерфейс ICloneable

Поскольку классы представляют ссылочные типы, то это накладывает некоторые ограничения на их использование. В частности, допустим, у нас есть следующий класс:

```Csharp
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

Создадим один объект Person и попробуем скопировать его данные в другой объект Person:

```Csharp
var tom = new Person("Tom", 23);
var bob = tom;
bob.Name = "Bob";
Console.WriteLine(tom.Name); // Bob
```

В данном случае объекты tom и bob будут указывать на один и тот же объект в памяти, поэтому изменения свойств для переменной bob затронут также и переменную tom.

Чтобы переменная bob указывала на новый объект, но при этом имела значения из переменной tom, мы можем применить клонирование с помощью реализации интерфейса ICloneable:

```Csharp
public interface ICloneable
{
    object Clone();
}
```

## Поверхностное копирование

Реализация интерфейса в классе Person могла бы выглядеть следующим образом:

```Csharp
class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public object Clone()
    {
        return new Person(Name, Age);
    }
}
```

Использование:

```Csharp
var tom = new Person("Tom", 23);
var bob = (Person)tom.Clone();
bob.Name = "Bob";
Console.WriteLine(tom.Name); // Tom
```

Теперь все нормально копируется, изменения в свойствах переменной bob не сказываются на свойствах из переменной tom.

Для сокращения кода копирования мы можем использовать специальный метод MemberwiseClone(), который возвращает копию объекта:

```Csharp
class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
}
```

Этот метод реализует поверхностное (неглубокое) копирование. Однако данного копирования может быть недостаточно. Например, пусть класс Person содержит ссылку на объект класса Company:

```Csharp
class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Company Work { get; set; }
    public Person(string name, int age, Company company)
    {
        Name = name;
        Age = age;
        Work = company;
    }
    public object Clone() => MemberwiseClone();
}
class Company
{
    public string Name { get; set; }
    public Company(string name) => Name = name;
}
```

В этом случае при копировании новая копия будет указывать на тот же объект Company:

```Csharp
var tom = new Person("Tom", 23, new Company("Microsoft"));
var bob = (Person)tom.Clone();
bob.Work.Name = "Google";
Console.WriteLine(tom.Work.Name); // Google - а должно быть Microsoft
```

## Глубокое копирование

Поверхностное копирование работает только для свойств, представляющих примитивные типы, но не для сложных объектов. И в этом случае надо применять глубокое копирование:

```Csharp
class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Company Work { get; set; }
    public Person(string name, int age, Company company)
    {
        Name = name;
        Age = age;
        Work = company;
    }
    public object Clone() => new Person(Name, Age, new Company(Work.Name));
}
class Company
{
    public string Name { get; set; }
    public Company(string name) => Name = name;
}
```

## Сортировка объектов. Интерфейс IComparable

Большинство встроенных в .NET классов коллекций и массивы поддерживают сортировку. С помощью одного метода, который, как правило, называется Sort() можно сразу отсортировать по возрастанию весь набор данных. Например:

```Csharp
int[] numbers = new int[] { 97, 45, 32, 65, 83, 23, 15 };
Array.Sort(numbers);
foreach (int n in numbers)
    Console.WriteLine(n);
// 15 23 32 45 65 83 97
```

Однако метод Sort по умолчанию работает только для наборов примитивных типов, как int или string. Для сортировки наборов сложных объектов применяется интерфейс IComparable. Он имеет всего один метод:

```Csharp
public interface IComparable
{
    int CompareTo(object? o);
}
```

Метод CompareTo предназначен для сравнения текущего объекта с объектом, который передается в качестве параметра object? o. На выходе он возвращает целое число, которое может иметь одно из трех значений:

- Меньше нуля. Значит, текущий объект должен находиться перед объектом, который передается в качестве параметра

- Равен нулю. Значит, оба объекта равны

- Больше нуля. Значит, текущий объект должен находиться после объекта, передаваемого в качестве параметра

Например, имеется класс Person:

```Csharp
class Person : IComparable
{
    public string Name { get;}
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name; Age = age;
    }
    public int CompareTo(object? o)
    {
        if(o is Person person) return Name.CompareTo(person.Name);
        else throw new ArgumentException("Некорректное значение параметра");
    }
}
```

Здесь в качестве критерия сравнения выбрано свойство Name объекта Person. Поэтому при сравнении здесь фактически идет сравнение значения свойства Name текущего объекта и свойства Name объекта, переданного через параметр. Если вдруг объект не удастся привести к типу Person, то выбрасывается исключение.

Применение:

```Csharp
var tom = new Person("Tom", 37);
var bob = new Person("Bob", 41);
var sam = new Person("Sam", 25);
 
Person[] people = { tom, bob, sam};
Array.Sort(people);
 
foreach (Person person in people)
{
    Console.WriteLine($"{person.Name} - {person.Age}");
}
```

И в данном случае мы получим следующий консольный вывод:

```Csharp
Bob - 41
Sam - 25
Tom - 37
```

Интерфейс IComparable имеет обобщенную версию, поэтому мы могли бы сократить и упростить его применение в классе Person:

```Csharp
class Person : IComparable<Person>
{
    public string Name { get;}
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name; Age = age;
    }
    public int CompareTo(Person? person)
    {
        if(person is null) throw new ArgumentException("Некорректное значение параметра");
        return Name.CompareTo(person.Name);
    }
}
```

Аналогичным образом мы мошли сравнивать по возрасту:

```Csharp
class Person : IComparable<Person>
{
    public string Name { get;}
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name; Age = age;
    }
    public int CompareTo(Person? person)
    {
        if(person is null) throw new ArgumentException("Некорректное значение параметра");
        return Age - person.Age;
    }
}
```

## Применение компаратора

Кроме интерфейса IComparable платформа .NET также предоставляет интерфейс IComparer:

```Csharp
public interface IComparer<in T>
{
    int Compare(T? x, T? y);
}
```

Метод Compare предназначен для сравнения двух объектов o1 и o2. Он также возвращает три значения, в зависимости от результата сравнения: если первый объект больше второго, то возвращается число больше 0, если меньше - то число меньше нуля; если оба объекта равны, возвращается ноль.

Создадим компаратор объектов Person. Пусть он сравнивает объекты в зависимости от длины строки - значения свойства Name:

```Csharp
class PeopleComparer : IComparer<Person>
{
    public int Compare(Person? p1, Person? p2)
    {
        if(p1 is null || p2 is null) 
            throw new ArgumentException("Некорректное значение параметра");
        return p1.Name.Length - p2.Name.Length;
    }
}
 
class Person
{
    public string Name { get;}
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name; Age = age;
    }
}
```

В данном случае используется обобщенная версия интерфейса IComparer, чтобы не делать излишних преобразований типов. Применение компаратора:

```Csharp
var alice = new Person("Alice", 41);
var tom = new Person("Tom", 37);
var kate = new Person("Kate", 25);
 
Person[] people = { alice, tom, kate};
Array.Sort(people, new PeopleComparer());
 
foreach (Person person in people)
{
    Console.WriteLine($"{person.Name} - {person.Age}");
}
```

Объект компаратора указывается в качестве второго параметра метода Array.Sort(). При этом не важно, реализует ли класс Person интерфейс IComparable или нет. Правила сортировки, установленные компаратором, будут иметь больший приоритет. В начале будут идти объекты Person, у которых имена меньше, а в конце - у которых имена длиннее:

```
Tom - 37
Kate - 25
Alice - 41
```

# Ковариантность и контравариантность обобщенных интерфейсов

Понятия ковариантности и контравариантности связаны с возможностью использовать в приложении вместо некоторого типа другой тип, который находится ниже или выше в иерархии наследования.

Имеется три возможных варианта поведения:

- Ковариантность: позволяет использовать более конкретный тип, чем заданный изначально

- Контравариантность: позволяет использовать более универсальный тип, чем заданный изначально

- Инвариантность: позволяет использовать только заданный тип

C# позволяет создавать ковариантные и контравариантные обобщенные интерфейсы. Эта функциональность повышает гибкость при использовании обобщенных интерфейсов в программе. По умолчанию все обобщенные интерфейсы являются инвариантными.

Для рассмотрения ковариантных и контравариантных интерфейсов возьмем следующие классы:

```Csharp
class Message
{
    public string Text { get; set; }
    public Message(string text) => Text = text;
}
class EmailMessage : Message
{
    public EmailMessage(string text): base(text) { }
}
```

Здесь определен класс сообщения Message, который получает через конструктор текст и сохраняет его в свойство Text. А класс EmailMessage представляет условное email-сообщение и просто вызывает конструктор базового класса, передавая ему текст сообщения.

# Ковариантные интерфейсы

Обобщенные интерфейсы могут быть ковариантными, если к универсальному параметру применяется ключевое слово out. Такой параметр должен представлять тип объекта, который возвращается из метода. Например:

```Csharp
interface IMessenger<out T>
{
    T WriteMessage(string text);
}
class EmailMessenger : IMessenger<EmailMessage>
{
    public EmailMessage WriteMessage(string text)
    {
        return new EmailMessage($"Email: {text}");
    }
}
```

Здесь обобщенный интерфейс IMessenger представляет интерфейс мессенджера и определяет метод WriteMessage() для создания сообщения. При этом на момент определения интерфейса мы не знаем, объект какого типа будет возвращаться в этом методе. Ключевое слово out в определении интерфейса указывает, что данный интерфейс будет ковариантным.

Класс EmailMessenger, который представляет условную программу для отправки email-сообщений, реализует этот интерфейс и возвращает из метода WriteMessage() объект EmailMessage.

Применим данные типы в программе:

```Csharp
IMessenger<Message> outlook = new EmailMessenger();
Message message = outlook.WriteMessage("Hello World");
Console.WriteLine(message.Text);    // Email: Hello World
 
 
IMessenger<EmailMessage> emailClient = new EmailMessenger();
IMessenger<Message> messenger = emailClient;
Message emailMessage = messenger.WriteMessage("Hi!");
Console.WriteLine(emailMessage.Text);    // Email: Hi!
```

То есть мы можем присвоить более общему типу IMessenger<Message> объект более конкретного типа EmailMessenger или IMessenger<EmailMessage>.

В то же время если бы мы не использовали ключевое слово out:

```Csharp
interface IMessenger<T>
```

то мы столкнулись бы с ошибкой в строке

```Csharp
IMessenger<Message> outlook = new EmailMessenger();  // ! Ошибка
 
IMessenger<EmailMessage> emailClient = new EmailMessenger();
IMessenger<Message> messenger = emailClient;  // ! Ошибка

```

Поскольку в этом случае невозможно было бы привести объект IMessenger<EmailMessage> к типу IMessenger<Message>

При создании ковариантного интерфейса надо учитывать, что универсальный параметр может использоваться только в качестве типа значения, возвращаемого методами интерфейса. Но не может использоваться в качестве типа аргументов метода или ограничения методов интерфейса.

## Контравариантные интерфейсы

Для создания контравариантного интерфейса надо использовать ключевое слово in. Например, возьмем те же классы Message и EmailMessage и определим следующие типы:

```Csharp
interface IMessenger<in T>
{
    void SendMessage(T message);
}
class SimpleMessenger : IMessenger<Message>
{
    public void SendMessage(Message message)
    {
        Console.WriteLine($"Отправляется сообщение: {message.Text}");
    }
}
```

Здесь опять же интерфейс IMessenger представляет интерфейс мессенджера и определяет метод SendMessage() для отправки условного сообщения. Ключевое слово in в определении интерфейса указывает, что этот интерфейс - контравариантный.

Класс SimpleMessenger представляет условную программу отправки сообщений и реализует этот интерфейс. Причем в качестве типа используемого этот класс использует тип Message. То есть SimpleMessenger фактически представляет тип IMessenger<Message>.

Применим эти типы в программе:

```Csharp
IMessenger<EmailMessage> outlook = new SimpleMessenger();
outlook.SendMessage(new EmailMessage("Hi!"));
 
IMessenger<Message> telegram = new SimpleMessenger();
IMessenger<EmailMessage> emailClient = telegram;
emailClient.SendMessage(new EmailMessage("Hello"));
```

Так как интерфейс IMessenger использует универсальный параметр с ключевым словом in, то он является контравариантным, поэтому в коде мы можем переменной типа IMessenger<EmailMessage> передать объект IMessenger<Message> или SimpleMessenger

Если бы ключевое слово in не использовалось бы, то мы не смогли бы это сделать. То есть объект интерфейса с более универсальным типом приводится к объекту интерфейса с более конкретным типом.

При создании контрвариантного интерфейса надо учитывать, что универсальный параметр контрвариантного типа может применяться только к аргументам метода, но не может применяться к возвращаемому результату метода.

## Совмещение ковариантности и контравариантности

Также мы можем совместить ковариантность и контравариантность в одном интерфейсе. Например:

```Csharp
interface IMessenger<in T, out K>
{
    void SendMessage(T message);
    K WriteMessage(string text);
}
class SimpleMessenger : IMessenger<Message, EmailMessage>
{
    public void SendMessage(Message message)
    {
        Console.WriteLine($"Отправляется сообщение: {message.Text}");
    }
    public EmailMessage WriteMessage(string text)
    {
        return new EmailMessage($"Email: {text}");
    }
}
```

Фактически здесь объединены два предыдущих примера. Благодаря ковариантности/контравариантности объект класса SimpleMessenger может представлять типы IMessenger<EmailMessage, Message>, IMessenger<Message, EmailMessage>, IMessenger<Message, Message> и IMessenger<EmailMessage, EmailMessage>. Применение классов:

```Csharp
IMessenger<EmailMessage, Message> messenger = new SimpleMessenger();
Message message = messenger.WriteMessage("Hello World");
Console.WriteLine(message.Text);
messenger.SendMessage(new EmailMessage("Test"));
 
IMessenger<EmailMessage, EmailMessage> outlook = new SimpleMessenger();
EmailMessage emailMessage = outlook.WriteMessage("Message from Outlook");
outlook.SendMessage(emailMessage);
 
IMessenger<Message, Message> telegram = new SimpleMessenger();
Message simpleMessage = telegram.WriteMessage("Message from Telegram");
telegram.SendMessage(simpleMessage);
```



