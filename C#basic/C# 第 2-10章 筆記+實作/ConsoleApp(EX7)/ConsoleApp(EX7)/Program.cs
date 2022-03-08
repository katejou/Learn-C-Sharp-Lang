// chapter 7   P 206
/*      設計流程理論 : 
 Object Oriented Programing 的 Object Oriented Analysis and Design 流程︰ (OOP , OOAD)
        分析系統需求 > 抽象化 > 定義物件規格 
 
        Class = 類別 = 藍圖
 Modifier = 修飾詞 (我個人稱為權限) e.g. public
 排列︰ Modifier Class identifier(自訂的名稱) {}
 
 instance menbers 
 藍圖中的元素 = 當它被實化(instance)時會存在的東東︰
 
 //  屬性
  Instance Variables  實體變數
  Static Variables  靜態變數         // * 順帶提變數的生命週期︰ 
                                          區域 = 在程式區段  
                                          實體 = 在物件實體中
                                          靜態 = 在應用程式執行環境中

  constructor 建構涵式/建構子         // * 物件的實體建立過程              
                                           1. 配置記憶體(在Heap區)
                                           2. 實體變數初始化
                                           3. 呼叫建構子
  default / loverload  constructor
  # 方法也可以多型                     
                 // constructor 見下方 student 的例子。


  // Class Diagram 程式關係的 圖形化介面    <-- 是另一個套件，因為工作上沒有人在用，所以略過。


  // Namespace   (見下方teacher的例子)
  
  同 project 同 file 多個 namespace                   // teacher1 例子
  巢狀class﹑namespace  <-- 感覺不實用                // teacher2-3 例子        --- // 以下的其實都不實用，看看就好。
    // 太細應該用不到，詳見︰ 
    // https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/namespaces/using-namespaces
  同 project 不同 file 的 namespace 引用 using         // teacher4 例子
  不同 solution 不同 project 的 namespace 引用         // teacher5 例子
  同 solution 不同 project 的 namespace 引用           // teacher6 例子

  teacher5 的步驟 : 

  從這個 solution 開 一個新 project ︰
  檔案 > 加入 > 新增專案

  teacher5 和 teacher6 的步驟 : 
  
  把project導入到這裡做參數 ︰ 
  在方案總管 > 左鍵目前的這個方案 > 加入 > 參考 > 專案 > 打勾
  然後才可以感應到隔壁project的namespace, 否則打 using otherSluForEX7 或 using otherProject;  會 error
  因為偵測不到

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using otherfile;     // <-- 第二個 .cs 檔的第二個自設 namespace
using otherSluForEX7;    // 不同 solution 不同 project 的 namespace 引用 
using otherProject;     // 同 solution 不同 project 的 namespace 引用 


namespace SecondNamespace
{         // 新增第二個 namespace 在同一個檔案。 
    class Teacher
    {
        public void Teaching()
        {
            Console.WriteLine("I am teaching.");
        }
    };

    // a nested namespace                 
    namespace NestedNamespace        // 巢狀 namespace 呼叫時 :new  SecondNamespace.NestedNamespace.Teacher2();
    {
        class Teacher2
        {
            public void Teaching2()
            {
                Console.WriteLine("I am teaching too.");
            }

        };

        class Teacher3              // 巢狀 class   
        {
            public class Teacher3_1        // 記得要public，預設的權限太緊了。
            {
                public void Teaching3()
                {
                    Console.WriteLine("I am teaching free.");
                }

            }
        }
    }

}




namespace ConsoleApp_EX7
{

    public class TestForThis
    {
        public string A;
        public string B;

        public TestForThis() : this("我是當多型建構子沒有參數時_用this預設的A", "預設的B") { }   //  P 233

        public TestForThis(string sth1, string sth2)
        {
            A = sth1;
            B = sth2;        
        }

    }
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("\ntesting  -- new \n");                                    //  new 
            Student student1 = new Student();
            Student student2 = new Student();                // using otherfile;     (other namespace)

            Console.WriteLine("\ntesting  -- Instance Variables  \n");                    //  Instance Variables   

            Console.WriteLine("實體變數在給予實值之前︰  \n");
            Console.WriteLine("student1.Name : " + student1.Name);
            Console.WriteLine("student1.Age  : " + student1.Age + "\n");

            student1.Name = "mary";
            student1.Age = 12;
            student2.Name = "peter";
            student2.Age = 13;

            Console.WriteLine("實體變數在給予實值之後︰  \n");
            Console.WriteLine("student1.Name : " + student1.Name);
            Console.WriteLine("student1.Age  : " + student1.Age);

            Console.WriteLine("student2.Name : " + student2.Name);
            Console.WriteLine("student2.Age  : " + student2.Age);

            Console.WriteLine("\ntesting  -- static Variables \n");                       //  static Variables 
            ++Student.StudentNum;
            ++Student.StudentNum;
            Console.WriteLine("How many student in this school : " + Student.StudentNum);

            Console.WriteLine("\ntesting  -- Constructor + Overload \n");                      //  Constructor
            Student student3 = new Student("tom", 14);
            Console.WriteLine("student3.Name : " + student3.Name);
            Console.WriteLine("student3.Age  : " + student3.Age);
            ++Student.StudentNum;

            Console.WriteLine("\ntesting -- Constructor + Overload + this\n");                    // Overload
            TestForThis tft = new TestForThis();
            Console.WriteLine("在用第一個建構子的時候︰ "+ tft.A);
            TestForThis tft2 = new TestForThis("我是多型建構子，有參數輸入而定的A","輸入的B");
            Console.WriteLine("在用第二個建構子的時候︰ " + tft2.A);

            Console.WriteLine("\ntesting  -- Method + Overload \n");
            Console.WriteLine(student1.Study());
            Console.WriteLine(student2.Study());
            Console.WriteLine(student3.Study());
            Console.WriteLine();
            Console.WriteLine(student1.Study("English"));
            Console.WriteLine(student2.Study("Math"));
            Console.WriteLine(student3.Study("Chinese"));

            Console.WriteLine("\ntesting  -- Second Name Space \n");   // 同file
            SecondNamespace.Teacher Teacher1 = new SecondNamespace.Teacher();
            Teacher1.Teaching();

            Console.WriteLine("\ntesting  -- Nested Name Space \n");  // 不實用，看看就好。
            SecondNamespace.NestedNamespace.Teacher2 Teacher2 = new SecondNamespace.NestedNamespace.Teacher2();
            Teacher2.Teaching2();

            Console.WriteLine("\ntesting  -- Nested Name Space +  Nested Class \n"); // 不實用，看看就好。
            SecondNamespace.NestedNamespace.Teacher3.Teacher3_1 Teacher3 = new SecondNamespace.NestedNamespace.Teacher3.Teacher3_1();
            Teacher3.Teaching3();


            Console.WriteLine("\ntesting  -- using other file's namespace \n");
            Teacher4 teacher4 = new Teacher4();    // 因為有 using otherfile;  才可以用的
            teacher4.Teaching4();

            Console.WriteLine("\ntesting  -- using other solution's namespace \n");
            Teacher5 teacher5 = new Teacher5();    // 因為有 using otherSluForEX7;  才可以用的
            teacher5.Teaching5();

            Console.WriteLine("\ntesting  -- using other project's namespace \n");
            Teacher6 teacher6 = new Teacher6();    // 因為有 using otherProject;  才可以用的
            teacher6.Teaching6();


            // 其他︰
            // 第二個新開的 Class, (同project, .cs) 預設的 Namespce 和 這邊的這個一樣。(可以看到他們的名稱相同)
            // 刪一個 Object 的方法︰  Obj = null;  

        }
    }
}

