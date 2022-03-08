using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace otherfile
{  // 這不是預設的 namespace,  預設的 namespace 和 Program.cs 一樣 ( = ConsoleApp_EX7 ) 
    class Teacher4{
        string name;
        int age;
        public void Teaching4() {
            Console.WriteLine("I am teaching 4 money");
        }
        public void Teaching4(string a, int b) {
            name = a;
            age = b;
        }
        
    }

    class Student
    {
        public string Name;   // Instance Variables 實體變數   // 不打public的話，無法直接修改，預設應該是private?
        public int Age;       // string 的初始值 null  ,  int 的初始值 0 , bool 的初始值 false 
                              // float 的初始值 0.0  ,  object 的初始值 null  , char 的初始值 "\x0000"

        public static int StudentNum = 0;    // Static Variables  靜態變數   
                                             // 不需要 new 出來，在 Class 沒有實體時，已經存在，並且是唯一的。

        // constructor 建構涵式/建構子
        /*
         沒有回傳值
         和這個class的名稱一樣      
         */
        public Student() //<--這個不寫出來，就是隱藏的預設，寫了出來，就可以多型了。 // default constructor
        { Console.WriteLine("我是new出來的Student"); }  // 建構時會第一時間做的事。
        public Student(string Name, int Age)
        {                                                                             // loverload constructor
            this.Name = Name;    // this 指明是這個class而不是這個參數(區域變數)的
            this.Age = Age;
        }

        public string Study() {
            return "I am studying";
        }

        public string Study(string subject)        // 方法的多型
        {
            return "I am studying " + subject;
        }


    }


}

//class main3                                                                同一個project 不可以在第二個 Main
//{
//    private static void Main(string[] args) { }
//}
