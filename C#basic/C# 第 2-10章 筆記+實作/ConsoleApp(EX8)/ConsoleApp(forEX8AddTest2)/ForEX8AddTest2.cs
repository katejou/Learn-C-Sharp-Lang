using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_forEX8AddTest2_
{

    public class OtherProjectTestOnModifire
    {
        /*
          private 限同 class 
          internal 限同 project        
          protected 同 type         
          public 沒有限
          protected internal 同 type 或 同 project
          private protected 同 class 或 同 type        
        */
        private string private_String = "Other_Project_private_String";
        internal string internal_String = "Other_Project_internal_String";
        protected string protected_String = "Other_Project_protected_String";
        public string public_String = "Other_Project_public_String";
        protected internal string protected_Internal_String = "Other_Project_protected_Internal_String";
        private protected string private_protected_Stirng = "Other_Project_private_protected_Stirng";


        // -------------------
        public string OtherProjectDadPublicMethodGetFields()
        {
            return private_String + "\n"
                + internal_String + "\n"
                + protected_String + "\n"
                + public_String + "\n"
                + protected_Internal_String + "\n"
                + private_protected_Stirng;

        }
    }

    public class IntListIndexer
    {

        private List<int> IntList = new List<int>();
        public int this[int index]
        {
            get { return IntList[index]; }
            set
            {
                if (index == IntList.Count) { IntList.Add(value); }    // index等於數量= 新增
                else { IntList[index] = value; }  // index小於數量 = 改變數值
            }                                     // index大於數量，在 List 的型別中，會出現 run time error : 不在範圍內
        }

        public void PrintAll()
        {
            foreach (int i in IntList)
            {
                Console.Write(i);
            };

        }
    }

    public class Student
    {
        public string name;
        public int marks;

    }
    public class Classroom
    {
        List<Student> class1 = new List<Student>()  // 在原地就做資料輸入。
        {
            new Student{ name = "mary",marks = 100},   // 由一個class去實體另一個class, 所以個我實體這classroom時，實體了4個object
            new Student{ name = "peter",marks = 90},   //注意這裡是逗號。
            new Student{ name = "tom",marks = 80},
         };

        public IEnumerable<string> GetStudent(int marks)
        {
            
            IEnumerable<string> s =
                (from Student someone in class1 where someone.marks > marks select someone.name);

            return s;

        }
        // https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/select-clause
    }



    public class Father_Father
    {
        public int testAttri;
        public int GetTestAttri()
        {
            return testAttri + 2000;
        }
    }

    public class Son_Son : Father_Father
    {
        new public int GetTestAttri()
        {
            return testAttri + 1000;
        }
    }


    public class AddtestConstructor
    {
        public AddtestConstructor() { Console.WriteLine(" I am the father constructor"); }
    }

    public class AddTestSonCons : AddtestConstructor
    {
        public AddTestSonCons() { Console.WriteLine(" I am the son constructor"); }
    }
    public class AddTestSonCons2 : AddtestConstructor
    {
        public AddTestSonCons2() : base() { Console.WriteLine(" I am the son2 constructor"); }
    }

    public class AddTestSonCons3 : AddtestConstructor
    {
        public AddTestSonCons3(int a) { Console.WriteLine(" I am the son3 constructor"); }
    }

    public class AddTestGrandSonCons : AddTestSonCons3
    {
        public AddTestGrandSonCons(int b, int c) : base(c){ Console.WriteLine(" I am the GrandSon constructor."); }
    }




    class ForEX8AddTest2
    {
        static void Main(string[] args)
        {
        }
    }
}

