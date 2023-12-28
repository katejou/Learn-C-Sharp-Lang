/*
----------------------------------------ch8----------------------------------------------------------------

  // P 248

  封裝﹑繼承﹑多型

  封裝屬性  :                                                     // 見 行 764-897   補充︰實測 modifier 的範圍
  
  Modifier : private , internal , protected , public

  屬性 = field

  private 限同 class 
  internal 限同 project        
  protected 同 type         
  public 沒有限
  protected internal 同 type 或 同 project
  private protected 同 class 或 同 type

  //   https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/accessibility-levels
  //   下面那個有圖︰
  //   http://blog.ja-anything.com/2017/07/31/c%E7%89%A9%E4%BB%B6%E5%B0%8E%E5%90%91%E7%A8%8B%E5%BC%8F%E8%A8%AD%E8%A8%88%E5%85%AD%E5%AD%98%E5%8F%96%E4%BF%AE%E9%A3%BE%E8%A9%9E%E5%AD%98%E5%8F%96%E5%B1%A4%E7%B4%9A/

  封好了的屬性，只能由特別的方法來操作︰
  set{}   
  get{}    

  它們可以內建條件，審視輸入值的合理性，不合理就不改或給值。
  分開兩個動作的權限
  呼叫時，不用真的打get或set, C# 會自已判斷

  get set 存取會預設接收(它們的屬性)本身的權限。  
  get set 權限只可比(它們的屬性)小。
  get / set 要有另外一個的存在才可以改權限。兩個之中只可改一個。          //  見 行 574-634 

  
  索引子 ( 我認為翻作 索引器 比較好) Indexer                              // 見 Group 實作
  Dictionary                                                              // 見 Company 實作



  -----------------------------
  P246
  繼承  Inheritance                                                         

  只能有一個父類別。 (但所有Object都自動是System.Object的兒子。)
  子類別承繼父親的所有，而且還可以擴充﹑覆寫成員。                        // 見 shape 實作

  base 用來參考父類的做法。

  // https://www.runoob.com/csharp/csharp-inheritance.html
  // https://dotblogs.com.tw/mvp/2018/04/25/164427
  
  new override virtual  覆寫的分別                                        // 見 ObjA等 的實作
  
  覆寫 + 索引器                                                           // 見 Employee5等 的實作
  
  父轉子的型別  // P272
  explicit
  容易有Runtime error, 所以要 exception 或 用 is 查詢 (它是不是我的子孫？是哪一個？)


  */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_forEX8AddTest2_;

namespace ConsoleApp_EX8_
{

    public class Employee1
    {
        public int getSetBaseSalary
        {
            set { }
            get
            {
                return 30000;
            }
        }

        public readonly int readonlyBaseSalary = 30001;     // 上述的意思，其實等同這個？ 
                                                            // 是，https://www.pluralsight.com/guides/const-vs-readonly-vs-static-csharp
                                                            // readonly不是 const,   const 是隱含 static 的，直用class名呼叫出來，不需要new
                                                            // 在 class 之中的 readonly 沒有 new 還是不會出來。
                                                            // readonly也可以放在class Program中，Main 之前 ，自己獨立，還不一定要給常數，所以比 const 更加的多用途。
        public const int constBaseSalary = 30002;

        public string GetSetReplaceAttributeString { get; private set; } = "GetSetReplaceAttributeString";
        public int GetSetReplaceAttributeint { get; protected set; } = 123;
        public bool GetSetReplaceAttributeBool { get; internal set; } = true;
        public Object GetSetReplaceAttributeObject { get; private protected set; } = new Object();
    }


    public class Employee2
    {


        public int BaseSalary2;  // 它被下面的方法操作
        public int DetailFirstGetSet    // 這是一個專門的方法，但是呼叫起來像屬性，因為沒有()
        {     
            get { return BaseSalary2; }     // <-- 注意︰這個不能是DetailFirstGetSet它自己。如果寫做它自己，會有Runtime Error
            set { BaseSalary2 = value; }    // set含有特殊的keyword: value , 當有值傳入時，都會存入value中
        }
        public int ShortSecondUnknowGetSet { get; set; }  // 它所影響的，是另一個不知名的private值，而不是BaseSalary2

        private int BaseSalary4;    // 如果這個是private的話，會灰色，但是要寫出來，給DetailPointToPrivateGetSet指名。
        public int DetailPointToPrivateGetSet
        {
            get { return BaseSalary4; }
            set { BaseSalary4 = value; }
        }                    // 下一步︰ 行 899 見上方的 Employee1 如何用 GET SET 特殊方法 完全取代 一般屬性。

    }

    public class Employee3
    {
        private string EmpName;

        public string getSetEmpName
        {
            get                                   // 所有人都可以知道他的名字
            {
                if (EmpName != "")
                {
                    return EmpName;
                }
                else
                {
                    return "not set yet";
                }
            }
            internal set         // private / protected 的話，會連自己產生的obj都不能用 emp1.EmpName = "mary" 設定名字。 ?  
                                 // 只可以改為 internal ....或不改  
            {

                if (value is string)  // 不是string 的話，根本就會在complie已經卡，所以沒有用。
                {
                    EmpName = value;
                }
                else                             // 如果是建構子傳入的value呢？
                {
                    EmpName = "fail";
                }
            }
        }
    }

    public class Employee4
    {
        private int EmpID;

        public int getSetEmpID
        {
            get
            {
                if (EmpID == 123)
                {
                    return EmpID;
                }
                else
                {
                    return 1;
                }
            }
            protected set
            {
                EmpID = value;
            }
        }

        public Employee4(int empid)
        {
            EmpID = empid;
        }

        public Employee4() { }

    }

    // -------------------------------- 索引器 --------------------------------------------------------------


    // P254 拆解
    public class Indexer
    {

        private int[] numArray = { 1, 2, 3, 4, 5 };
        public int this[int index]     // Indexer 沒有名字。 只要是this的後面有[] 就是它了。 instance之後，它的名字就是物件的名字。
        {                             //  [] 中設下的 是將來作為index(/key) 的參數  是什麼類型和名稱。
            get
            {
                int tmp;
                if (index >= 0 && index <= 4)
                {
                    tmp = numArray[index];
                }
                else
                {
                    tmp = 0;
                }

                return tmp;
            }
            set
            {
                if (index >= 0 && index <= 4)
                {
                    numArray[index] = value;
                }
            }
        }
    }


    // P254  拆解
    public class Person
    {
        public string Name;
        public int Age;
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public class Group
    {   // 設下 Dictionary 的泛型格式 key 要 int, value 要 Person
        private Dictionary<int, Person> Members = new Dictionary<int, Person>();   
        // int 要放在 person 的前面，Dictionary才會把它當 key 
        // http://code2study.blogspot.com/2012/01/c-dictionary.html
        public Person this[int no]
        {  // 設定了 no 為 indexer 的 int key 參數。

            get
            {
                return Members[no];       // 取用成員的某個屬性時︰ studyGroup[0].Name
            }
            set
            {
                Members.Add(no, value);   // 新增成員時︰  studyGroup[0] = mary;
            }
        }
    };


    // P254 


    public class Employee
    {
        public string Name;
        public int ID;
        public Employee(string name, int ID)
        {
            Name = name;
            this.ID = ID;
        }

        virtual public void GetDetail()   // virtual 的方法。
        { }

        public string Print()
        {
            return "ID = " + ID + ", Name = " + Name;
        }
    }
    public class Company
    {
        protected Dictionary<int, Employee> department = new Dictionary<int, Employee>();
        public Employee this[int id]
        {  // 回傳的是 Employee 所以上方不是放 Company / Department 喔
            get
            {
                return department[id];
            }
            set
            {
                try
                {
                    department.Add(id, value);   // 因為泛型指定了 id 是 int, value 是 Employee
                }
                catch (ArgumentException)   // 因為 Dictionary 的 key 不能重覆，所以要設。
                {
                    Console.WriteLine("Empolyee already exit.\n");
                }


            }
        }
    }
    // -------------------------------- 繼承 --------------------------------------------------------------


    public class Shape     // 爸爸
    {
        public double length;
        public double width;
        public double GetArea()    //方法一
        {
            return length * width;
        }
        public void GetDetail()   // 方法二
        {
            Console.WriteLine("length   " + length);
            Console.WriteLine("width   " + width);
        }

        public Shape(double l, double w)   // 建構子
        {
            length = l;
            width = w;
        }
    }

    public class Rectangle : Shape
    {  // 這個 : 的意思是從右給左的。 

        public Rectangle(double l, double w) : base(l, w) { }   // 這個 : 的意思是從左給右的。
        //如果爸爸的建構子有改寫的話。兒子的也一定要重新定建構子的名稱，並把爸爸的參數都對上。

    }


    public class Circle : Shape
    {
        public double radius;    // 新增屬性。
        new public double GetArea()    // 覆寫方法  :  new = 隱藏爸爸的方法。 override ?  見下方另一個例子。
        {
            double area = Math.PI * Math.Pow(radius, 2);  //pi * r ^ 2;

            return Math.Round(area, 2, MidpointRounding.AwayFromZero);          // 實作 Math 方法！
        }
        new public void GetDetail()     // 要加上 new 這個字，否則覆寫的方法名會有綠底線
        {
            base.GetDetail();     // 做了爸爸的這個動作，再加上下方  (expand 舊的方法, 而不是完全覆寫。)
            Console.WriteLine("radius   " + radius);
        }

        public Circle(double r) : base(r * 2, r * 2)    // 修改建構子，父親的引數要符合個數。 圓的長寛都一樣。
        { radius = r; }

    }

    // P263 ----------------------------------------------------------------------
    public class ObjA
    {
        public void DO()
        {
            Console.WriteLine("我是父的 DO");
        }
    }

    public class ObjB : ObjA
    {
        new public void DO()
        {
            Console.WriteLine("我是 new 的子 DID");
        }
    }

    //-------- https://dotblogs.com.tw/mvp/2018/04/25/164427

    public class ObjA1
    {
        virtual public void DO()              // virtual 配搭 override 使用。當子類再被逼轉回父型時，它還是會在做子的方法。
        {
            Console.WriteLine("我是 virtual 父的 DO");
        }
    }

    public class ObjB1 : ObjA1
    {
        override public void DO()
        {
            Console.WriteLine("我是 override 子的 DID");
        }
    }

    // P270 實作  -----------------------------------------------------------------

    public class Employee5 : Employee    // Employee 在之前的 例子。
    {

        public int Level;
        int baseSalary = 1000;

        public Employee5(int ID, string Name) : base(Name, ID)
        { }
        override public void GetDetail()    // 覆寫方法
        {
            Console.WriteLine(Name + " ID  : " + ID);
            Console.WriteLine(Name + " Level  : " + Level);
            Console.WriteLine(Name + " Salary : " + ComputeSalary());
        }

        public virtual int ComputeSalary()   // 新增方法
        {
            return baseSalary + (5000 * Level);
        }
    }

    public class Manager : Employee5   // 書中是要開到另一個file去繼承，但是這裡省略，方便看。
    {
        public Manager(int ID, string Name) : base(ID, Name)
        { }
        public override int ComputeSalary()   // 覆寫 Employee5 新增的方法。沒有覆寫GetDetail(), 但 Salary 值還是會改。
        {
            return base.ComputeSalary() + 6000;
        }
    }

    // P274 實作  -----------------------------------------------------------------

    public class Sales : Employee
    {

        public Sales(string Name, int ID, int Bonus) : base(Name, ID)
        {
            this.Bonus = Bonus;
        }
        public int Bonus;

    };
    public class SalsesManager : Employee
    {
        public SalsesManager(string Name, int ID, string Department) : base(Name, ID)
        {
            this.Department = Department;
        }
        public string Department;
    };

    public class Company2 : Company
    {

        public void SalaryReport()   // 新增方法
        {
            foreach (Employee emp in department.Values)  // .Values 是 Dictionary 獨有的方法。
            {
                if (emp is SalsesManager)   // is 可以判斷出被 implict 地 轉為父型的子類別 ！
                {
                    SalsesManager mng = (SalsesManager)emp;    //  explict 地 轉父型為子型
                    Console.WriteLine("Manager of " + mng.Department);   // 這是子型特有的屬性，如果是爸爸的話，取不出來。

                }
                if (emp is Sales)
                {
                    Sales sales = (Sales)emp;
                    Console.WriteLine("Salses's bouns " + sales.Bonus);
                }
                Console.WriteLine("emp.Name  " + emp.Name);
                // 其他資料就不是重點，省略了。
                Console.WriteLine();
            }
        }

    }


    // ------------------------------- additional -----------------------------------------

    public class TestOnModifire
    {
        /*
          private 限同 class 
          internal 限同 project        
          protected 同 type         
          public 沒有限
          protected internal 同 type 或 同 project : protected V , Internal X  = OK
          private protected  同 project 兼 同 type : protected V , Internal X  = NO    
        */
        private string private_String = "private_String";
        internal string internal_String = "internal_String";
        protected string protected_String = "protected_String";
        public string public_String = "public_String";
        protected internal string protected_Internal_String = "protected_Internal_String";
        private protected string private_protected_Stirng = "private_protected_Stirng";
        // private protected 適用於 C# 7.2 版及更新版本。  用下方連結所說的方法，修改語言版本
        // https://marcus116.blogspot.com/2018/08/HowtochangelanguageversioninVisualStudio.html

        // -------------------
        public string DadPublicMethodGetFields()
        {
            return private_String + "\n"
                + internal_String + "\n"
                + protected_String + "\n"
                + public_String + "\n"
                + protected_Internal_String + "\n"
                + private_protected_Stirng;
        }

    }

    class SonOfTestOnModifire : TestOnModifire
    {

        public string SonPublicMethodGetFields()
        {
            return // private_String + "\n" +  // <----  父親的private, 兒子沒有權限
                internal_String + "\n"
                + protected_String + "\n"
                + public_String + "\n"
                + protected_Internal_String + "\n"
                + private_protected_Stirng;

        }

    }

    class SonOfOtherProject : OtherProjectTestOnModifire
    {
        public string SonOfOtherProjectGetfields()
        {
            return // private_String + "\n" +  // <----  父親的private, 兒子沒有權限
                   // internal_String + "\n" + // <----  父親的internal, 隔了 project 的兒子沒有權限
                protected_String + "\n"
                + public_String + "\n"
                + protected_Internal_String; //+ "\n"
                                             //+ private_protected_Stirng;  // <---- 因為它不是爸爸，他們也不在同一個 project 

        }

    }

    class TestOnPrivateProtected : TestOnModifire
    {

        public string TrytoGetPrivateProtected()
        {
            return private_protected_Stirng;
        }

        public string TryToGetProtectedInternal()
        {
            return protected_Internal_String;
        }
    }

    class TestOnPrivateProtectedByProjectDad : OtherProjectTestOnModifire
    {

        public string TrytoGetPrivateProtected()
        {
            return "  private_protected_Stirng X ";
            // return private_protected_Stirng;  // error
        }
        public string TryToGetProtectedInternal()
        {
            return protected_Internal_String;
        }
    }



    class Program //: TestOnModifire
    {

        public static readonly int IndpententReadonlyBaseSalary = 30003;     // 不加static的話，complie 沒有錯，但是在 Main 中就引用不了。 
        public const int IndpententConstBaseSalary = 30004;

        static void Main(string[] args)
        {

            // P249

            Console.WriteLine("\ntesting  --  Get Set 對比 const,readonly \n");

            Employee1 emp1 = new Employee1();
            emp1.getSetBaseSalary = 40000;    // Set 並沒有影響 get , 因為 get 已經預設回傳一個實數。
            int gsbs = emp1.getSetBaseSalary;
            Console.WriteLine("物件中 的屬性 固定了Get的回傳數值 : " + gsbs);   // 30000

            int rbs = emp1.readonlyBaseSalary;
            Console.WriteLine("物件中 的屬性 readonly : " + rbs);

            int cbs = Employee1.constBaseSalary;
            Console.WriteLine("物件中 的屬性 const : " + cbs);   
            // 物件中 const 的屬性，不需要等物件 new 出來就可以使用，>= static 詳見第三章。
            // int cbs2 = emp1.constBaseSalary;  < ---- error  不要用物件名稱叫它出來。

            int irbs = IndpententReadonlyBaseSalary;
            Console.WriteLine("這個 class Program  的 readonly 屬性 : " + irbs);
            int icbs = IndpententConstBaseSalary;
            Console.WriteLine("這個 class Program  的 const 屬性 : " + icbs);


            Console.WriteLine("\ntesting  -- set + get \n");
            
            Employee2 emp2 = new Employee2();
            emp2.DetailFirstGetSet = 30005;   // set value
            Console.WriteLine("以 指定 屬性 的 set 設定 BaseSalary2 : " + emp2.DetailFirstGetSet);        //30005

            emp2.ShortSecondUnknowGetSet = 30006;
            Console.WriteLine("以 不指定 屬性 的 set 設定 另一個不知名值  " + emp2.ShortSecondUnknowGetSet);  //30006

            Console.WriteLine("\n再取 指定 屬性 的 GET SET : " + emp2.DetailFirstGetSet);            //30005  <-- 它並沒有被ShortSecondUnknowGetSet影響
            Console.WriteLine("再取 不指定 屬性 的 GET SET : " + emp2.ShortSecondUnknowGetSet);              //30006  <-- 它並沒有被DetailFirstGetSet影響
            Console.WriteLine("取被指定的屬性 值 : " + emp2.BaseSalary2);                     //30005  <-- 它只被DetailFirstGetSet影響

            //  DetailFirstGetSet = BaseSalary2  !=  ShortSecondUnknowGetSet
            //  結論︰ 不指定 屬性 的 get set 會自創一個 不知名的屬性，而不會影響到被其他get set方法指明的屬性 。 

            emp2.DetailPointToPrivateGetSet = 30007;
            Console.WriteLine("\nprivate 屬性 的 public Get Set  " + emp2.DetailPointToPrivateGetSet);
            // private 了，就不能以 屬性本身的名稱 來取或設值, 完全被GET SET代理。 
            // 在這個時候，private 的屬性名稱己不重要。
            // 所以以 不指定屬性的 GET SET 特殊方法, 代理 不知名的屬性，就可以完全等同於這個情況。還省了一行。

            Console.WriteLine("\ntesting  -- get set 不同權限 + 條件 \n");

            Employee3 emp3 = new Employee3();
            emp3.getSetEmpName = "mary";
            Console.WriteLine("以條件 set 設定名字，並以條件 get 取得名字  " + emp3.getSetEmpName);  // mary

            emp3.getSetEmpName = "";
            Console.WriteLine("以條件 set 設定名字，並以條件 get 取得名字  " + emp3.getSetEmpName);  // not set yet


            Console.WriteLine("\ntesting  -- get set 條件 + 建構子\n");

            Employee4 emp4 = new Employee4(123);
            Console.WriteLine("以 多型建構子 設定ID，並以條件 get 取得ID  " + emp4.getSetEmpID);    // 123
            Employee4 emp5 = new Employee4();  //多型建構子，不設ID值。
            Console.WriteLine("以 多型建構子 不設定ID，並以條件 get 取得ID  " + emp5.getSetEmpID);    // 1

            // 結論 ︰ 由建構子給值，和set不衝突。

            Console.WriteLine("\ntesting  -- 經過 indexer 修改 物件中的 private Array ，再以 for loop 取出 :\n");

            //https://www.runoob.com/csharp/csharp-indexer.html

            Indexer indexer1 = new Indexer();
            indexer1[0] = 9;   // 直接以物件的姓名去操作 indexer 改 裡面一個 private 了的 Array 屬性。
                               // 所以一個物件，只可以有一個indexer ?  我想應該是。

            for (int i = 0; i < 4; i++)   // 不能用foreach, 因為array private 了，而 get set 一次只能取一個 指定了index的值，
            {                             //  如果真的非常需要 foreach , 應該去 indexer 裡面，增加一個 method 去做。
                Console.Write(indexer1[i]);   // 直接以物件的姓名去 取出 裡面的一個屬性的Array。
            }
            Console.WriteLine();    // 9234


            Console.WriteLine("\ntesting  -- 索引器 + 泛型 Dictionary  \n");

            Person mary = new Person("Mary", 18);       // 建三個人
            Person peter = new Person("Peter", 19);
            Person tom = new Person("Tom", 20);

            Group studyGroup = new Group();             // 建一個indexer
            studyGroup[0] = mary;
            studyGroup[2] = peter;     // 可以不依傳統index的順序, 因為這不是Array, 而是 Dson_sontionary
            studyGroup[8] = tom;       // 在 Dson_sontionary 的 [] 中 是 key 而不是 index 

            Console.WriteLine("studyGroup[0]  =  " + studyGroup[0]);    // 只是印它的型別，而不是內容。
            Console.WriteLine("studyGroup[0].Name  =  " + studyGroup[0].Name);
            Console.WriteLine("studyGroup[2].Name  =  " + studyGroup[2].Name);
            Console.WriteLine("studyGroup[8].Name  =  " + studyGroup[8].Name);

            Console.WriteLine("\ntesting  -- 索引子 + 泛型 Dictionary + try catch  \n");

            Employee Mark = new Employee("Mark", 123);
            Employee Jenny = new Employee("Jenny", 456);
            Employee Sue = new Employee("Sue", 789);

            Company Askey = new Company();
            Askey[Mark.ID] = Mark;
            Askey[Jenny.ID] = Jenny;
            Askey[Sue.ID] = Sue;
            Askey[Sue.ID] = Sue;   // error message : Empolyee already exit.  <--- try catch 的作用

            Console.WriteLine("Askey[Mark.ID].Name  = " + Askey[Mark.ID].Name);
            Console.WriteLine("Askey[Jenny.ID].Name  = " + Askey[Jenny.ID].Name);
            Console.WriteLine("Askey[Sue.ID].Name  = " + Askey[Sue.ID].Name);

            //Console.WriteLine(Mark.Print());  //<--- 書中的這個方法是之前的章節寫的，這裡先不用。


            Console.WriteLine("\ntesting  --  Inheritance + expand + new (override) + base \n");

            Rectangle R1 = new Rectangle(10, 2);
            Console.WriteLine("長方形繼承形狀，以建構子設定長寛，取面積︰  " + R1.GetArea());     // 20
            Console.WriteLine("沒覆寫方法"); R1.GetDetail();    // 10 , 2

            Circle C1 = new Circle(10);
            Console.WriteLine("\n圓形繼承形狀，以建構子設定半徑，取面積︰  " + C1.GetArea());
            Console.WriteLine("用base覆寫方法"); C1.GetDetail();   // 20, 20, 10

            Console.WriteLine("\ntesting  --  new  v.s. virtual + override   \n");

            ObjA obja = new ObjA();
            ObjB objb = new ObjB();

            obja.DO();  // 父DO
            objb.DO();  // 子DID

            ObjA1 obja1 = new ObjA1();
            ObjB1 objb1 = new ObjB1();

            obja1.DO();  // 父DO
            objb1.DO();  // 子DID

            // new , virtual , overrride 在這個階段沒有差別，都是子覆寫了父。
            // 但當他們都被 implicit 地被轉回父型後︰ virtual + override 還是會在做子的方法。
            // 在一些地方，例如索引器的使用，會是種優勢。

            ObjA objc = objb;     // implicit 地被轉回父型
            ObjA1 objc1 = objb1;

            Console.Write("\n當用new去覆寫的方法，在物件被轉型回父型之後︰ ");
            objc.DO();  // 父DO
            Console.Write("當用override去覆寫的方法，在物件被轉型回父型之後︰ ");
            objc1.DO();  // 子DID

            // 結論︰ new 的子類有保留父的方法，所以做回父的事。overrride 的子類就完全忘記爸爸了，轉也轉不回去。\n");


            // P270 實作
            Console.WriteLine("\ntesting  --  override + 索引器   \n");

            Employee5 emp_1 = new Employee5(123, "John Chang");
            emp_1.Level = 4;
            Manager emp_2 = new Manager(456, "Mary Wang");
            emp_2.Level = 4;

            // 加入索引器 , 索引器是以父型容納他們。
            Company Askey2 = new Company();
            Askey2[emp_1.ID] = emp_1;
            Askey2[emp_2.ID] = emp_2;

            // 以索引器做出物件的動作，因為 子類 override 了父類的方法, 所以它們都還是各自做自已，而不是爸爸的動作。   

            Askey2[emp_1.ID].GetDetail(); //這是兒子1, 兒子1 override GetDetail()
            Console.WriteLine();
            Askey2[emp_2.ID].GetDetail(); //這是兒子2，兒子2沒有 override GetDetail(), 但 override 了 ComputeSalary()
                                          // 所以他的人工比較多。

            // 注意︰ 索引器的泛型型別，還是爸爸，沒有變喔。

            // P272
            // 父轉子的型別

            Console.WriteLine("\ntesting  --  索引器的繼承和擴張, foreach 用 Dictionary.Values, if is 判斷子型別 \n");

            Company2 Askey3 = new Company2();

            Sales john = new Sales("John", 987, 2000);      // 子類覆寫的建構子。
            SalsesManager maryL = new SalsesManager("Mary", 876, "Sales");     

            Askey3[john.ID] = john;
            Askey3[maryL.ID] = maryL;

            Askey3.SalaryReport();

            // P 277

            Console.WriteLine("--------------------------- additional -----------------------------");

            Console.WriteLine("additional  -- 實測 modifier 的範圍\n");
            /*
              private 限同 class 
              internal 限同 project        
              protected 同 type         
              public 沒有限
              protected internal 同 type 或 同 project : protected V , Internal X  = OK
              private protected  同 project 兼 同 type : protected V , Internal X  = NO    
            */

            TestOnModifire tm1 = new TestOnModifire();

            Console.WriteLine("Main Dad get 3 : \n");
            //Console.WriteLine(tm1.pirvate_String);  // 因為保護層，無法存取。
            //Console.WriteLine(tm1.protected_String);
            //Console.WriteLine(tm1.private_protected_String); 
            /*
            因為保護層，無法存取protected。
            這個 Main 方法 是 Class Program 的, 不是 TestOnModifire的 或 它的兒子。
            即使是在這個方法中實體了的，也叫不出來。
            */
            Console.WriteLine(tm1.internal_String);
            Console.WriteLine(tm1.public_String);
            Console.WriteLine(tm1.protected_Internal_String);

            Console.WriteLine("\nDad's Method Get 6 :  \n\n" + tm1.DadPublicMethodGetFields());

            Console.WriteLine("--------------------------------------------------------");

            SonOfTestOnModifire son = new SonOfTestOnModifire();

            Console.WriteLine("\nMain Son get 3 : \n");
            // Console.WriteLine(son.pirvate_String);  
            // Console.WriteLine(son.protected_String);
            // Console.WriteLine(son.private_protected_String); 
            Console.WriteLine(son.internal_String);
            Console.WriteLine(son.public_String);
            Console.WriteLine(son.protected_Internal_String);

            Console.WriteLine("\nSon's Method Get 5   : \n\n" + son.SonPublicMethodGetFields());  // 兒子寫的方法，取不到爸爸的 pirvate
            Console.WriteLine("\nSon DO Dad's Method Get 6  :  \n\n" + son.DadPublicMethodGetFields());   // 用繼承而來的 爸爸的方法 才可以取得 爸爸的 pirvate

            Console.WriteLine("--------------------------------------------------------");

            SamePorjOtherFileSon SPOFson = new SamePorjOtherFileSon();  // Same Porject Other File 其實是同 namespce

            Console.WriteLine("\nMain Other File Son get 3 : \n");
            // Console.WriteLine(SPOFson.pirvate_String);  
            // Console.WriteLine(SPOFson.protected_String);
            // Console.WriteLine(SPOFson.private_protected_String); 
            Console.WriteLine(SPOFson.internal_String);
            Console.WriteLine(SPOFson.public_String);
            Console.WriteLine(SPOFson.protected_Internal_String);

            Console.WriteLine("\nOther File's Son Get 5 : \n\n" + SPOFson.SamePorjOtherFileSonGetFields());
            Console.WriteLine("\nOther File's Son DO Dad's Method Get 6  :  \n\n" + SPOFson.DadPublicMethodGetFields());   // 同 Project 不同 flie 的兒子 權限沒有差。

            Console.WriteLine("--------------------------------------------------------");

            // OtherPorjectSon 不能在這裡實作，因為
            // 兩個專案的參考關係，只能是單邊的，不能互相的，而一個 solution 的運行，只會選做一個 Project 的 Main
            // 更改選做一個 Project 的 Main : 方案總管 > 右鍵專案 > 設定為啟始專案
            // 要去看 protected_Internal 和 pirvate_protected 的更多測試，請移往  ConsoleApp(for EX8 additional test  )

            // 開新 Project ，把第二個爸放在那，再引入它到這個地方繼承。
            // 開新Project步驟︰ 檔案 > 新增專案 > 加入至方案 ( 平常是新開 solution )

            SonOfOtherProject OPson = new SonOfOtherProject();

            Console.WriteLine("\nMain Other Project Son get 1 : \n");
            //Console.WriteLine(OPson.pirvate_String);  
            //Console.WriteLine(OPson.protected_String);
            // Console.WriteLine(OPson.internal_String);
            Console.WriteLine(OPson.public_String);
            // Console.WriteLine(OPson.protected_Internal_String);
            // Console.WriteLine(OPson.pirvate_protected_String);

            Console.WriteLine("\nOther Project Son Get 3  : \n\n" + OPson.SonOfOtherProjectGetfields());  // 不同 Project 的兒子 權限 有差。 沒了 internal
            Console.WriteLine("\nOther Project Son DO Dad's Method Get 6  :  \n\n" + OPson.OtherProjectDadPublicMethodGetFields());

            Console.WriteLine("--------------------------------------------------------");

            Console.WriteLine("\n\nadditional  -- 單獨實測 private protected 和 protected_Internal \n");

            TestOnPrivateProtected TOPP = new TestOnPrivateProtected();
            Console.Write("\n同Project的兒子方法可以拿到 ");
            Console.WriteLine(TOPP.TrytoGetPrivateProtected() + "  V");

            TestOnPrivateProtectedByProjectDad TOPPBPD = new TestOnPrivateProtectedByProjectDad();
            Console.Write("不同Project的兒子方法拿不到 ");
            Console.WriteLine(TOPPBPD.TrytoGetPrivateProtected());

            Console.Write("\n同Project的兒子 在Main 不可以直接拿到 ");
            // Console.WriteLine(TOPP.private_protected_Stirng);
            Console.WriteLine("  private_protected_Stirng X");

            Console.Write("不同Project的兒子 在Main 不可以直接拿到 ");
            // Console.WriteLine(TOPPBPD.private_protected_Stirng);
            Console.WriteLine("  private_protected_Stirng X ");

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("同Project的兒子方法可以拿到 ");
            Console.WriteLine(TOPP.TryToGetProtectedInternal() + "  V");

            Console.Write("不同Project的兒子方法也可以拿到 ");
            Console.WriteLine(TOPPBPD.TryToGetProtectedInternal() + "  V");

            Console.Write("\n同Project的兒子 在Main 可以直接拿到 ");
            Console.WriteLine(TOPP.protected_Internal_String + "  V");

            Console.Write("不同Project的兒子 在Main 不可以直接拿到 ");
            // Console.WriteLine(TOPPBPD.protected_Internal_String);
            Console.WriteLine("  protected_Internal_String X");

            // 總結︰ 

            // 兒子和隔file的兒子 他們寫的方法，得不到 private ; 
            // 隔 project 的兒子寫的方法，得不到 private + internal + pirvate protected; 

            // 實作的爸爸,兒子和隔file的兒子在 Main 不能直接得到 private 和 protected ; 
            // 隔 project 的兒子 在 Main 中不能直接得到 private + internal + protected + protected Internal + pirvate protected  

            // protected internal = 同 類別 | 同 project
            // private protected  = 同 類別 & 同 project  

            // ---------------------- 其他︰

            // 在開發程式時︰ 權限 (modifier) 一開始都先設最小的，然後再按需求改大。

            Console.WriteLine("\n--------------------------------------------------------\n");

            Console.WriteLine("\nadditional  -- 用GetSet完全取代一般屬性\n");

            //對比 Employee2, 這種做法更常用。
            //優點︰ 減低程式的複雜性，又可以很方便地分開設立 get set 的 權限。
            Employee1 GSA = new Employee1();
            Console.WriteLine("用 GetSet 一行設下的str 屬性︰  " + GSA.GetSetReplaceAttributeString);
            Console.WriteLine("用 GetSet 一行設下的int 屬性︰  " + GSA.GetSetReplaceAttributeint);
            Console.WriteLine("用 GetSet 一行設下的bool 屬性︰  " + GSA.GetSetReplaceAttributeBool);
            Console.WriteLine("用 GetSet 一行設下的obj 屬性︰  " + GSA.GetSetReplaceAttributeObject);

            Console.WriteLine("\n--------------------------------------------------------\n");

            Console.WriteLine("\nadditional  -- 測泛型 List 的索引器\n");
            IntListIndexer ili = new IntListIndexer();
            ili[0] = 1;
            ili[1] = 2;
            // ili[4] = 5;  // <-- runtime error 不在範圍內
            ili[2] = 3;
            ili[3] = 4;
            // ili[4] = "5";  // complie time error , 不符合泛型。
            ili.PrintAll();
            // 泛型 List 由 索引器代理 可以 限制更多 輸出輸入元素的條件? 但是 get set 就可以達到那個功能了。所以沒有什麼實用的必要。
            // 索引器 + Dictionary 會比較實用, 提供除了Key以外的取值條件。

            Console.WriteLine("\n--------------------------------------------------------\n");
            Console.WriteLine("additional  -- C# 中的 IEnumerable + form where select 作為 泛型List 的過濾  \n");

            List<int> before_filter = new List<int>() { 1, 2, 3, 4, 5 };
            // List<int> after_filter = from bf in before_filter  where bf > 3 select bf;  
            // 要用IEnumerable 才可以用 form ... 一次存取多個值。
            IEnumerable<int> after_filter = from bf in before_filter where bf > 3 select bf;

            foreach (int i in after_filter) { Console.Write(i); }  // 用foreach 印出 IEnumerable

            Console.WriteLine("\n\nadditional  -- 以object為泛型的List作為屬性,以 IEnumerable + form where select 作為方法，抽取資料  \n");
            Classroom class_1 = new Classroom();  // 內已建三個學生
            IEnumerable<string> answer = class_1.GetStudent(89);  //這個方法，回傳的是過於這個分數的學生名字。

            foreach (string i in answer) { Console.WriteLine(i); }  
            Console.WriteLine();

            Console.WriteLine("\n--------------------------------------------------------\n");

            Console.WriteLine("\n\nadditional  -- object 和 var  的差別？ \n");
            /*
             論最沒有特性的物件。                                  
            */
            Father_Father ov = new Father_Father();
            object oov = ov;
            var vov = ov;
            Console.WriteLine("implict 為 object 或 var =  沒有改變物件的型別︰");
            Console.WriteLine(oov.GetType()); //ConsoleApp_forEX8AddTest2_.Father_Father
            Console.WriteLine(vov.GetType()); //ConsoleApp_forEX8AddTest2_.Father_Father
            object ov2 = new Father_Father();
            var ov3 = new Father_Father();
            Console.WriteLine(ov2.GetType()); //ConsoleApp_forEX8AddTest2_.Father_Father
            Console.WriteLine(ov3.GetType()); //ConsoleApp_forEX8AddTest2_.Father_Father

            // 結綸︰沒有差別。

            //foreach ? 
            Console.WriteLine("\nforeach 中使用 var 或 object = 沒有差別︰");
            int[] numberArray = { 1, 2, };
            foreach (var i in numberArray) { Console.WriteLine(i.GetType()); };   // System.Int32
            foreach (object i in numberArray) { Console.WriteLine(i.GetType()); };  // System.Int32

            // 結綸︰沒有差別。

            Console.WriteLine("\n用 var 或 object 判斷沒有指定型別的實值︰ = 沒有差別");
            var number1 = 1;
            var number2 = 1.2;
            var bool3 = true;
            object number4 = 1;
            object number5 = 1.2;
            object bool6 = false;

            Console.WriteLine(number1.GetType());  // System.Int32
            Console.WriteLine(number2.GetType());  // System.Double
            Console.WriteLine(bool3.GetType());    // System.Boolean
            Console.WriteLine(number4.GetType());  // System.Int32
            Console.WriteLine(number5.GetType());  // System.Double
            Console.WriteLine(bool6.GetType());    // System.Boolean

            // 結綸︰沒有差別。
            // 他們都可以用作還沒有確切知道是什麼類型時，作為一個臨時的容納。

            Console.WriteLine("\n以 var 隱藏 型別, new 的時候悄悄按照填入屬性︰");
            var anaArray = new[] 
            {
                new { name = "melody", marks = 80 },  // 這是 Student 的屬性名稱, 雖然連 F12 都跳不過去。
              // new { name = "dog", marks = "nomark" },   // 不按照Student 的屬性型別會出錯。 
              new { name = "sam", marks = 70 }
              //, new { Name = "Tom", Age = 19 }  // 因為是一個Array, 如果和之前的類型不同，是會產生紅線的。(這個是Person的屬性)
            };   
            foreach (var s in anaArray) {
                Console.WriteLine(s.GetType());   // <>f__AnonymousType0`2[System.String,System.Int32]
                Console.WriteLine(s.name);        //  GetType 取不到這個物件的型別
                Console.WriteLine(s.marks);
            }
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types
            // https://www.itread01.com/content/1541247909.html

            Console.WriteLine("\n--------------------------------------------------------\n");

            Console.WriteLine("\nadditional  -- is 和 as 測類別相容\n");

            Son_Son son_son = new Son_Son();
            Father_Father father_father = new Father_Father();

            var son_sonisov = son_son is Father_Father;      // is = bool ture
            var father_father_is_son_son = father_father is Son_Son;
            var son_sonislong = son_son is long;  // 不要管這綠線。程式只是在提醒我它絕對不可能是true。

            Console.WriteLine("兒子is爸爸 : " + son_sonisov); // ture
            Console.WriteLine("爸爸is兒子 : " + father_father_is_son_son); // false
            Console.WriteLine("兒子is數字 : " + son_sonislong); // false
            Console.WriteLine("is 給出的類型是 ︰ " + son_sonisov.GetType()); // System.Boolean

            var son_son_as_father_father = son_son as Father_Father;      // 兒子變爸爸，相容通過了, as 把兒子的地址交了出去。
            var father_father_as_son_son = father_father as Son_Son;      //   爸爸變兒子， 相容通過了, 但as 沒有兒子交了出去，給了null值。
            // var son_sonaslong = son_son as long;  // complie time error : 沒有父子關係， 無法相容

            Console.WriteLine("\n兒子as爸爸 : " + son_son_as_father_father); // ConsoleApp_forEX8AddTest2_.Son_Son
            Console.WriteLine("爸爸as兒子 : " + father_father_as_son_son); // 什麼也沒有。 null
            if (father_father_as_son_son == null) { Console.Write("null"); }
            Console.WriteLine("as 給出的類型是 ︰ null 或 ... \n" ); 
            
            if (son_son_as_father_father != null) {
                Console.WriteLine("所測試對象的地址，所以 新值 變成 兒子 的另一個名稱。\n" +
                                  " !!!!!!!! 重點 !!!!!!!! ︰  \n" +
                                  "用 新值 來呼叫兒子，它會 隱藏地 變成了 爸爸 的型別 \n" +
                                  " !!!!!!!! 即使GetType()測出來還是兒子  !!!!!!!!  "); }

            Console.WriteLine("\n比如︰\n我改了as之前的兒子的屬性值。");
            son_son.testAttri = 1;
            Console.WriteLine("\n我以兒子的 新名稱 取出, 他用了爸爸的舊方法: ");
            Console.Write(son_son_as_father_father.GetTestAttri());

            Console.WriteLine("\n我以兒子的 原本名稱 取出來, 他用了自已的新方法 (用 new 覆寫的, 不是override):");
            Console.Write(son_son.GetTestAttri());

            Console.WriteLine("\n我用 新值 強轉型別，又為同一個物件創立了另一個 新2名稱︰ ");
            Son_Son son_son2 = (Son_Son)son_son_as_father_father;  
            Console.Write("\n我用新2名稱取值︰ \n" + son_son2.GetTestAttri());  // 做兒子的方法。

            Console.WriteLine("\n\n我用新2名稱去改值為   5  ");
            son_son2.testAttri = 5;

            Console.WriteLine("\n再用這三種名稱 用同一個方法取值︰ ");
            Console.WriteLine(son_son.GetTestAttri());
            Console.WriteLine(son_son_as_father_father.GetTestAttri());
            Console.WriteLine(son_son2.GetTestAttri());

            Console.WriteLine("\n證明了他們都是同一個人，只是用 不同的名稱 和 暗地裡用不同型別 去做事。 ");
            Console.WriteLine(" !!!!!!!! 像個間諜一樣  !!!!!!!! ");

           Console.WriteLine("\n--------------------------------------------------------\n");

            Console.WriteLine("\nadditional  -- 繼承與覆寫建構子\n");
            // https://csharpkh.blogspot.com/2017/09/class-inheritance-constructor.html

            AddtestConstructor Father = new AddtestConstructor();
            Console.WriteLine();
            AddTestSonCons Son = new AddTestSonCons();  // <-- 兒子的Constructor並沒有覆寫爸爸的，它寫了自己的Constructor, 實體化時，會先做爸爸的，再做自己的Constructor.
            Console.WriteLine();
            AddTestSonCons2 Son2 = new AddTestSonCons2();   //<-- 用了 : base()  還是沒有略過爸爸的Constructor.
            Console.WriteLine();
            AddTestSonCons3 Son3 = new AddTestSonCons3(1);  // 他覆寫並建立了一個 有參數 建構子。 還是沒有把爸爸的 沒有參數 的建構子略過。
            Console.WriteLine();

            // 結論︰爸爸的建構子，是沒有辨法略過去的。
            // 但是 孫子 要再覆寫任何的建構子 時，會被強迫去填上 兒子 的建構子參數值。

            AddTestGrandSonCons grandSonCons = new AddTestGrandSonCons(1,2); 
            Console.WriteLine();

            
        }
    }
}
