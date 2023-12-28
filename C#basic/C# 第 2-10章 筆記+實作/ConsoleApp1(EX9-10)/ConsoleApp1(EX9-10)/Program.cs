
// 因為在工作上不常用，所以比較簡略，知道有存在就好。

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_EX9_
{
    public abstract class Human
    {  // 不可以實體。但可以什麼都不寫。
        public string action = "說 : ";  // 可以有正常的屬性和方法
        public void Walk(string name) { Console.WriteLine (  name + " 說  : 我走路。"); }

        //abstract int abc = 123;  // 沒有抽象的屬性。
        // 可以沒有抽象的動作，但是還是不能直接實體，一定要繼承者去實體。
        public abstract void Talk();  // 不可以實作，只可以強迫子孫實作，要不然子孫也不能實體。
    }

    public class Taiwanese : Human
    {
        public override void Talk() { Console.WriteLine(action + "中文"); }  // 要用override這個字。
    }
    public class American : Human
    {
        public override void Talk() { Console.WriteLine(action + "英文"); }  // 出現多型。
    }

    public interface ICanFly  // 我 不是 class, 也不是method, 更像是一個abstruct的method集合
    {
        // Console.WriteLine();   //不可以寫任何內容。
        // string Crash() { };  // 有{}都不行。
        string TakeOff();
        void Fly();
        void Landing();

    } // 多多少少有點 改善Class 只可以單一繼承。見之後的超人例子。

    // 先如同一般 Class 去繼承︰
    public class Bird : ICanFly
    {
        public Bird() { Console.WriteLine("I am a Bird."); }
        public string TakeOff() { Console.WriteLine("Wings"); return ""; }
        public void Fly() { Console.WriteLine("Wings"); }
        public void Landing() { Console.WriteLine("Feet"); }
    }
    public class Airplane : ICanFly
    {
        public void Fly() { Console.WriteLine("Engine"); }
        public void Landing() { Console.WriteLine("Wheels"); }
        public string TakeOff() { Console.WriteLine("Engine"); return ""; }

    }
    public interface ICanSwim
    {
        void Swimming();
    }
    public interface Unusual : ICanFly, ICanSwim
    {  // 介面可以繼承多個介面
        void LaserEye();  // 飛行的三個方法和游泳的一個方法，再加自己的一個。共五個方法要寫。
    }
    public interface TestOnMutiInterface { }

    public class Superman : American, Unusual, TestOnMutiInterface
    {
        public Superman() { Console.WriteLine("I am Superman."); }
        public void LaserEye() { Console.WriteLine("Kill on sight"); }

        public string TakeOff() { Console.WriteLine("One fist"); return ""; }

        public void Fly() { Console.WriteLine("Cape"); }

        public void Landing() { Console.WriteLine("Superman Landing"); }

        public void Swimming() { Console.WriteLine("To North Pole"); }

    }

    //  密封類別 (不能被繼承)

    public sealed class Evil_Superman : Superman
    {
        public Evil_Superman() { Console.WriteLine("I need to kill Batman."); }
    }
    // public class TestOnSealed : Evil_Superman { }  // error  不能繼承。



    // 靜態類別 (不能被new出來, 只有也是靜態的成員。如同 Math)

    public static class SuperGirl
    {
        public static string name = "Kara";
        // public string laser = "red";  // 必須也是static
        // public SuperGirl() { }  // 建構子不能覆寫。事實上，它一寫下來就實體了，還是唯一。
        public static void flying() { }
        // public void walking() { }  // 必須也是static
    }


    // Singleton Design Pattern  (也是不可以在外面 new 出來。)
    // https://xyz.cinc.biz/2013/07/singleton-pattern.html

    // Lazy initialization 懶漢方式。  (叫到才做一個，之後就不做了。)
    public class Kryptonite  // 氪氣石   
    {
        // Private Constructor︰
        private Kryptonite() { }
        // 一個靜態的，以自己的型別來產生屬性(尚未實體化)︰
        private static Kryptonite TheOnlyOneInstance;  // 這個 Kryptonite 是型別。 TheOnlyInstance 是名稱。
        // private static int abc = 123;  // 用來理解上述句子。

        public int amount = 10;  // 一個將來只有TheOnlyInstance可以操作的屬性。

        // 這個唯一產生氪氣石的方法。
        public static Kryptonite GetTheOnlyOneInstance()
        {
            // 如果氪氣石從來沒有被實體。
            if (TheOnlyOneInstance == null)
            {
                TheOnlyOneInstance = new Kryptonite();  //就實體化一次氪氣石
            }
            return TheOnlyOneInstance;
            // 如果是之後再呼叫這個方法，回傳的，還是第一個﹑唯一一個實體。
        }
    }

    // Eager initialization  餓漢方式  (我還沒有把自己寫完整，我就(實體)吃了我自己。)
    public class Batman
    {
        private static readonly Batman BruceWanye = new Batman();
        public string SuperPower = "SuperRich";
        private Batman() { }
        // 取代建構子的方法一定要static, 要不然因為建構子又private了，它整個物件就不可能使用了。
        public static Batman GetBatman()
        {
            return BruceWanye;  // 已經先建好的靜態物件，唯一一個自己, 就是自己的屬性。
        }

    }

    public class Outer
    {
        private string OuterString = "outer string";
        public string OuterPublicString = "OuterPublicString";
        public class Inner
        {
            public string TryToGetPrivateOuterString()
            {
                //return OuterString;  // 不可以拿外面的private
                return " Inner method cannot get outer string ";
            }
            public string InnerString = "inner string";
            public string TryToGetPublicOuterString()
            {
                //return OuterPublicString; // 也不行，死心吧，外內不通的。
                return " Inner method can only get inner things, outside and inside are independent.";
            }
        }
        public string TryToGetPublicInnererString()
        {
            // return InnerString;  //對這個級別來說不存在。
            return " Outer method cannot get inner string ";
        }
    }

    public class TestingOnEventArgs  // 傳遞相關狀態的資訊。
    {
        public string Name { private get; set; }
        public int Salary { private get; set; }
        public string Reason { private get; set; }

        public TestingOnEventArgs(string name, int salary, string reason)
        {
            Name = name;
            Salary = salary;
            Reason = reason;
        }

    }

    //----------------------------------------------------------------------------------
    public class ConcertEventArgs : EventArgs         // 建立演唱會這種活動。
    {
        public ConcertEventArgs(string concert_name)  // 有活動時，就必寫它的訊息。
        {
            Message = concert_name + "開辦了！！";
        }
        public string Message { get; private set; }
    }

    public class SuperStar                         // 建立明星這個職業。
    {

        public event EventHandler ConcertEventHandler;    // 明星會有舉辦活動的屬性，而且和粉絲相連。 每一個粉絲增加的時候，都會在這裡登記下。
        public void OnConcertEventCall(ConcertEventArgs e)   // 明星公佈 演唱會 的方法。(只可以輸入 演唱會 這種活動)
        {
            ConcertEventHandler(this, e);  // 預設輸入的兩個資料。她自己的身份 和 什麼活動
        }                                  // 激發明星和粉絲相連的屬性 > 粉絲們收到了，又會激發他們各自的動作。
    }

    public class FansType1                       // 第一類粉
    {
        public FansType1(SuperStar star)          // 建立時就要有粉的對像。
        {
            star.ConcertEventHandler += new EventHandler(this.GetTicket);   // 明星的EventHandler += 粉絲的EventHandler。
        }                                               // 設定當激發時，粉絲會馬上要去做的動作。
        private void GetTicket(object sender, EventArgs e)  // 這個動作預設了要這兩個參數, 不能改。
        {   
            string Msg = (e as ConcertEventArgs).Message;   // 父用as隱轉子型 回傳的是Null,但是這裡只是 做一下 子的動作, 回傳 動作的回傳值 。
            Console.WriteLine(Msg);                         // as 的新用法！
            Console.WriteLine("第一種 粉絲知道了，現在馬上去排隊！");
        }
    }

    public class FansType2
    {
        public FansType2(SuperStar star)
        {
            star.ConcertEventHandler += new EventHandler(this.GetTicket);
        }
        private void GetTicket(object sender, EventArgs e)
        {
            string Msg = (e as ConcertEventArgs).Message;
            Console.WriteLine(Msg);
            Console.WriteLine("第二種 粉絲知道了，現在馬上去網路訂票！");
        }
    }
    //----------------------------------------------------------------------------------








    class Program
    {


        // ------------------------------ chapter 10 -----------------------------------

        // Delegete

        // https://blog.xuite.net/autosun/study/32614006-%5BC%23%5D+%E4%BA%8B%E4%BB%B6%E8%88%87%E5%A7%94%E6%B4%BE%EF%BC%88Delegate%EF%BC%89

        // 裝方法的型別。這個型別是 C# 寫好的。 在這裡當作一個 特殊的 方法，用來 綜合方法。
        public delegate void GreetingDelegate(string a);
        // 我只「接受以 stringt 參數並且回傳 void」的方法為 輸入參數 。


        public static void SayHello(string name)
        {
            Console.WriteLine("Hello, " + name);
        }

        public static void SayHi(string name)
        {
            Console.WriteLine("Hi, " + name);
        }

        public delegate int FarewellDelegate(string a);
        public static int SayBye(string name)
        {
            Console.WriteLine("Bye, " + name);
            return 4;
        }

        //static void Manners(Delegate d , string person)
        //{
        //    d.Invoke(person);  // 不可以以Delegate作為類型，只可以以Delegate的物件作為類型。
        //}

        static void Greeting(GreetingDelegate g, string person)
        {
            g.Invoke(person);  // GreetingDelegate 有實體出 hi 和 hello+hi 的。
        }

        delegate void LambdaDelegate(string name);     // 他們都不會顯示參考

        delegate void anoMethod(string name);








        static void Main(string[] args)
        {
            Human john = new Taiwanese();  
            Human mary = new American();

            Console.WriteLine("testing -- 抽象abstract > 實體instanct > 多型overload\n");
            john.Talk(); // 中文
            mary.Talk(); // 英文

            Console.WriteLine("\ntesting -- interface 介面 與他們之間的多重繼承\n");

            Superman theOnlyOne = new Superman();
            theOnlyOne.LaserEye();
            theOnlyOne.Fly();

            Console.WriteLine();
            Bird bird = new Bird();
            bird.Fly();


            Console.WriteLine("\ntesting -- sealed class \n");

            Evil_Superman evil_Superman = new Evil_Superman();

            Console.WriteLine("\ntesting -- static class \n");

            // SuperGirl supergirl = new SuperGirl();  // 無法宣告靜態別。
            Console.WriteLine("SuperGirl.name  : " + SuperGirl.name);


            Console.WriteLine("\ntesting --  Lazy initialization Singleton Design Pattern \n");

            Console.WriteLine("宇宙創造了氪氣石");
            Kryptonite kryptonite = Kryptonite.GetTheOnlyOneInstance();
            // 這是一個方法，不是建構子，但操作了private的建構子。

            Console.WriteLine("我用了一塊氪氣石, 現在剩下︰ " + --kryptonite.amount);

            Console.WriteLine("我想再創造更多氪氣石");
            Kryptonite kryptonite2 = Kryptonite.GetTheOnlyOneInstance();

            Console.WriteLine("但是氪氣石的數量 ︰ " + kryptonite2.amount + "  , 維持不變。");

            // 新增的，只是同一個物件的別名，而不是本體。
            bool same = kryptonite2 == kryptonite;
            Console.WriteLine("kryptonite2 == kryptonite ? : " + same);

            Console.WriteLine("\ntesting --  Eager initialization Singleton Design Pattern \n");

            Batman playboy_billionaire = Batman.GetBatman();
            Console.WriteLine("花花公子的超能力 ︰  " + playboy_billionaire.SuperPower);

            Console.WriteLine("花花公子把錢花光了。");
            playboy_billionaire.SuperPower = "SuperBroke";

            Batman vigilante = Batman.GetBatman();
            Console.WriteLine("蝠蝙俠的超能力 ︰  " + vigilante.SuperPower);

            // 表面上，懶漢和餓漢是(在 Main 的使用方法)是沒有分別的，只是佔記憶體是時間不同。

            Console.WriteLine("\ntesting --  nested class \n");

            // Inner 在實體時，要透過外部的外名字。
            Outer.Inner innerObj = new Outer.Inner();
            Console.WriteLine(innerObj.TryToGetPrivateOuterString());
            Console.WriteLine(innerObj.TryToGetPublicOuterString());

            // nested class 的內外沒有什麼可以互通的，inner是outer的一屬性。
            Outer outer1 = new Outer();
            // Console.WriteLine(outer1.Inner.InnerString); // outer的物件也還是也拿不到內部的屬性。
            Console.WriteLine(outer1.TryToGetPublicInnererString());



            Console.WriteLine(" Inner Object 的類別是 ︰ " + innerObj.GetType());  // ConsoleApp1_EX9_.Outer+Inner  <-- 這個+不是我打的，是系統給的。
            Console.WriteLine(" Outer Object 的類別是 ︰ " + outer1.GetType());


            // ------------------------------ chapter 10 -----------------------------------

            // Delegete

            Console.WriteLine("\ntesting --  Delegete \n");

            Console.WriteLine("我可以直接以這個Main同級的方法去打招呼 ︰ ");
            SayHello("David");

            Console.WriteLine("我以這個 Delegete 的方法去打招呼 ︰ ");

            GreetingDelegate greeting = new GreetingDelegate(SayHello);
            // 一定要給他一個符合它格式的方法 做參數。
            greeting.Invoke("David");

            Console.WriteLine("我可以省略.Invoke");
            greeting("Esser");

            Console.WriteLine("\ntesting -- Multicast Delegete \n");

            GreetingDelegate greeting2 = new GreetingDelegate(SayHi);
            greeting += greeting2;   // 整合兩個相同類別的Delegate
            greeting("Arya");

            // Delegate 的最大好處，他可以一次過 組合 多個相同類別的 靜態方法 ！

            // 測試組合不同類別的方法，同樣入string, 但一個出int, 一個出void, 都會印一句話的。

            FarewellDelegate farewell = new FarewellDelegate(SayBye);
            // greeting += farewell; // 不可行，加入的時候，會做 隱轉 為GreetingDelegate的類型的動作。

            Console.WriteLine("\ntesting -- 以 Delegete(的物件) 為參數的類型 \n");
            Greeting(greeting, "Rock");
            Greeting(greeting2, "Loki");


            Console.WriteLine("\ntesting -- Delegete + Lambda 去 instance \n");

            // Lambda 省略了 Delegete 物件實體化時，中間所需求的 static 方法。
            LambdaDelegate LD = (string name) => { Console.WriteLine("Hey, " + name); };
            LD("Dennis");

            // Lambda 是一個沒有名稱的函式，即寫即有。但要找個東西存下來就是了。

            Console.WriteLine("\ntesting -- 以某個 物件的方法 去 instance Delegete \n");

            Taiwanese Ming = new Taiwanese();
            GreetingDelegate MingWalking = new GreetingDelegate(Ming.Walk);  // 這也是一個符合格式的方法。
            MingWalking("小明");

            Console.WriteLine("\ntesting -- Event \n");

            // https://jeffprogrammer.wordpress.com/2015/07/29/%E8%A7%80%E5%BF%B5-c-eventhandler/

            SuperStar LadyGaga = new SuperStar();   // 有了一個明星
            FansType1 littleMonster = new FansType1(LadyGaga);  // 有了一堆粉絲
            FansType2 littleBeast = new FansType2(LadyGaga);
            FansType1 litteFreak = new FansType1(LadyGaga);
            ConcertEventArgs WorldTour2020 = new ConcertEventArgs("Lady Gaga World Tour 2020");  // 有了一個演唱會

            LadyGaga.OnConcertEventCall(WorldTour2020);  // Lady Gaga 公佈了這個演唱會  > 粉絲們自動的得到消息，而且去買票了。
            FansType2 littleBeast2 = new FansType2(LadyGaga);   // 之後加入的粉絲，沒有收到這一個消息，所以沒有動作。

            Console.WriteLine("\ntesting -- Anonymous Method \n");
            // anoMethod 是之前宣告了，但是沒有內容，和Lamdba一樣。

            anoMethod ano = delegate (string name) { Console.WriteLine("Yo, " + name); };
            // 和Lamdba一樣 , 都是把它instance的當下才寫要做什麼事。
            ano("John");

            /*   看看和  Lambda 的對比。  
            LambdaDelegate LD = (string name) => { Console.WriteLine("Hey, " + name); };
            LD("Dennis");
            */
        }
    }
}


