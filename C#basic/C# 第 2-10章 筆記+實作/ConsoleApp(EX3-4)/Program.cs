/*

String  XX; <- 單個                                                             -- 宣告
String XX,YY; <- 多個

modifier(權限)+ datatype(型別) + Name(identifiers\reference\命名)  =  value(值)

變數命名:   P79
must be : character or number or _
start with character or _ (not number)

identifiers =  variable , class , method 's  名字 !!
如果要使用 C# 關鍵字 為變數名請加 @ 在前面
關鍵字 見 P80

---------------------------      P 81                                           -- 區域
local variable : 

在方法﹑判斷式﹑迴圏中宣告 的變數
當他們完成後，會消失。
不需要給予modifier(權限)

--------------------------     P 82                                             -- 全域
Readonly and constant : 
見下方  實作
---------------------------------------    P85                                  -- 實值和參考 型別

實值型別 - 直接存在變數位置中
參考型別 - 在Heap記憶體中instance, 把''地址''存到變數位置中

實值型別 -  = (給予) 會直接copy那個「值」，到另一個新的變數。
參考型別 -  = (給予) 會把它存的「地址」，copy到另一個新的變數名稱
		   ，但它們實際上是在操作「同一個地址」的「值」。

實值型別 - 
sbyte byte short ushort int unit long ulong
float double decimal
char
bool

參考型別 -
string
object
-------------  P 88 
Command Type System (CTS) > 跨平台和語言的型別識別系統                             -- CTS

all data see as obj (inherited from System.Object)
data type defined by runtime envrionment <--- 例如區域變數，在叫到它出現時，才由CRL決定。

對應表︰見 P89
---------------------------------------------------------------------------------           -- 類別
之後的都見實作︰  P91

*/
using System;
using System.Collections.Generic;  // 灰色的，刪去也沒有關係。
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1  // = java's package  
{
    /*   P 93
    struct     (user defined structure) 

    不可繼承, 不可有方法， 
    在class中，未instance前，不可以給予實值。只可定型別。    
    (一個充滿空箱子的 class)

    */


    /// <summary>
    /// 這個訊息，可以在呼叫 Size 時顯示。(打三個斜線會自動出現<summary>)
    /// </summary>
    struct Size     // P 96  // 請用上方的  「 1 個參考 」 跳到實作的程式碼行。
    {
        public int small;
        public int large;
    }
    struct Mystruct
    {
        public int year;
        public string month;
        public int day;
    }


    /*
    ---------------------------------------------------
    eunm  P 98
    
    它是靜態的。
    它的內容型別，就是該class所命之名    (例: MYenum.one <-- 它的型別就是MYenum)。
    預設輸入值為int, 可註明以改為其他型別(但只可以是數字型)。 <-- 沒有實作，但教材說可以。
    可不設值，只有姓名。但C#會自動幫它們順序地編一個數字值 0123...
    當強迫一個eunm class 的內容變為數字時，該數值會顯現。

     */

    enum MYenum   //P 104
    { // default int, can change but must be number (but i don't know how to change yet)
        one = 1,
        two = 2,
        three = 3,  // to show value in Main, have to explicitly change type
        four,   // C# 會自動分配數值4給它，當explicit 時就會看到了。
        whatever
    }

    // 型別轉換 P100 
    //--------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------


    class Program     
    {
        // 以下是這個 class 的 method (包括 Main)             ------------------  方法


        public static void ShowTime()                                     // P111 實作static方法
        {
            string time = DateTime.Now.ToLongDateString();
            Console.WriteLine(time);
        }// 直接印


        public static string ShowTimeReturn()
        {
            return DateTime.Now.ToLongDateString();    // P113 實作 return
        }// 回傳

        public static int GetRandomNum()  // P 114
        {
            Random rnd = new Random();
            int data = rnd.Next(1, 7);  // between 1-6
            return data;
        }// 方法呼叫另一個物件的方法

        // 題外︰ web application 比較少用 static 容易出錯???



        public static void PassByValue(int i)                            // 四種參數 P 116 
        {
            i += 1;
        }
        public static void PassByRef(ref int i)
        {
            i += 1;
        }

        public static void PassByOut(out int i)
        {
            i = 100;
        }
        public static int PassByParam(params int[] i)
        {
            int sum = 0;
            for (int a = 0; a < i.Length; a++)
            {
                sum += i[a];
            }
            return sum;
        }


        //-------------------------------------------------------------------------------------- 實作開始 ------------
        /*
        Readonly and constant          // 全域，但是不可變的變數。

        public const int c = 100;              <---- 編繹時可以設定一次，然後不能改

        1. const 僅能用於數字(int、float) 和字串、列舉，而 readonly 可以是任意型態。
        2. const 能在方法中使用，readonly 不行。  (常數不應該是宣告在方法裡)
        3. readonly 運行時可以取得一次，然後不能改(GetNumber()是自設的方法)
           const 編繹時可以設定一次，然後不能改

        4. const常數在宣告的時候就要初始化(指定值),readonly可以延遲到運行時
        ##### GetRandomNum()這個方法正常是會每次不同，但是r這個值只可被賦與一次，所以不變︰ 見實作。
        5. 兩個都不是 new 出來，而是直接用名字叫出來的。

        詳見︰  https://sweetkikibaby.pixnet.net/blog/post/191538132

          */

        public string asfd = "";   // 在這個 Main中 全域，(這個 Program Class 的屬性)，是可變的變數。
        /// <summary>
        /// 這個訊息，可以在呼叫 r 的時候，滑鼠停在上面時顯示。
        /// </summary>        
        public static readonly int r = GetRandomNum(); // 運行時可以取得RandomNum一次，然後不能改 
        // public const int r14 = GetRandomNum();   //<-------------- error : 一定要給予 常數。
        public static readonly int r1 = 123;
        public const int cm = 4567;

        //---------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------

        static void Main(string[] args)            // starting point !
        {
            Console.WriteLine("\nTesting -- Readonly & Constain");                  // Readonly & Constain
            const int c1 = 4567;
            //readonly int r2 = 123;
            // 不可以在此寫readonly並定義它，要在任何的method之外，包括Main

            /*
             ---------------------------------------------------------------------------------
                Console.WriteLine () 中 string 和 int 可以以 + 號連接
                因為 WriteLine 它自動化()中內容為string
             -----------------------------------    
             */

            Console.WriteLine("public static readonly int r = GetRandomNum()  " + r);
            Console.WriteLine("public static readonly int r1 = " + r1);
            Console.WriteLine("const in Main  " + c1);
            Console.WriteLine("const in method " + cm);


            Console.WriteLine("\nTesting -- Struc");                                // Struc
            Size mysize;
            mysize.small = 10;
            mysize.large = 20;
            Console.WriteLine("struct : Size mysize : mysize.small   " + mysize.small);
            Console.WriteLine("struct : Size mysize : mysize.large   " + mysize.large);

            Mystruct date;
            date.year = 2020;
            date.month = "2";
            date.day = 19;
            Console.WriteLine("Mystruct date : (int)date.year + (str)date.month + (int)date.day");
            Console.WriteLine(date.year + date.month + date.day);



            Console.WriteLine("\nTesting -- Enum");                                //  Enum
            MYenum a = MYenum.one;
            MYenum b = MYenum.two;
            MYenum c = MYenum.three;
            MYenum d = MYenum.four;  // "four" only have not given value


            Console.Write("MYenum.one : ");  // can not concat using + , not a string or number
            Console.WriteLine(a);  // one
            Console.Write("MYenum.four : ");
            Console.WriteLine(d);  // four

            // int e = a + b;   <-- cannot do calculation
            int e = (int)a + Convert.ToInt32(b);     // <--------------- 兩種轉型別的方法
            Console.WriteLine("(int)a + Convert.ToInt32(b) =  " + e);

            /*
                     ---------------------------------------------------    P 100                -- 型別轉換
            implicit

            small to big (e.g. int > long)
	            int a = 123
	            long b = a

            sbyte < short int long float double decimal
            short < int long float double decimal
            int < long float double decimal
            long < float double decimal
            float < double

            byte < ushort unit ulong (+ short int long float double decimal)
            ushort  < unit ulong (+ int long float double decimal)
            unit < ulong (+ long float double decimal)
            ulong < float double decimal

            char < ushort unit ulong (+ int long float double decimal)

            #沒有可以implicit 為 char 的型別

            數字的範圍不同，詳見如下，尤其是轉型態要小心︰
            https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/builtin-types/integral-numeric-types

            ----------------------------
            explicit

            big to small

            no data lost > e.g. > 
            int a = 123;   
            byte b = (byte) a;

            data lost -> error message> e.g. > 
            int a = 12
            byte b = a // error ----------

            correct>
            int a = 12
            byte b = (byte)a;

            */

            Console.WriteLine("\nTesting -- implicit and explicit ");       // implicit and explicit
            int f = 123;
            long g = f;  // implicit
            f = (int)g; // explicit

            /*
             * 補充︰
            論Convert.ToInt32(X)和 int.Parse(X)的分別︰
            詳見︰
            http://www.365jz.com/article/24350
            https://exfast.me/2017/01/c-sharp-convert-transition-parse-forced-transition-tryparse-security-transition-and-the-operator-of-the-difference-between-transformation-performance/
            https://marcus0168.pixnet.net/blog/post/27578954-c%23-%E8%BD%89%E6%8F%9B%E6%88%90%E6%95%B8%E5%AD%97%E7%9A%84%E6%96%B9%E6%B3%95%EF%BC%9Aint.parse%28%29%E3%80%81int.tryparse%28%29
            https://dotblogs.com.tw/box5068/2011/01/20/20911
             
             */

            Console.WriteLine("\nTesting -- Convert.ToInt32(X) 和 int.Parse(X)的分別 ");
            int TCone = Convert.ToInt32(a);  // 1
            int TCfour2 = Convert.ToInt32(MYenum.four);  // 4
                                                         // int PXE = int.Parse(a);
                                                         // int PXEs = int.Parse(MYenum.four);

            string four = "4";
            // int str4 = Convert.ToInt32(four);  <- error
            int str4 = int.Parse(four);   // String專用Parse, Enum 不適用
            
            // X.Parse(  ) 只可以用放string 
            // Convert.To 可以放任何東西，除了 string，如果放了，還會立即出紅線。// <-- 所以不確定型別時，先用它。

            Console.WriteLine("TCone  " + TCone + "  Convert.To 適用於 Enum > int");
            Console.WriteLine("TCfour2  " + TCfour2 + "  Convert.To 適用於 Enum > int");
            Console.WriteLine("int.Parse 不適用於 Enum > int " + ", int.Parse 適用於 string > int");
            Console.WriteLine("int.Parse(string)  " + str4);
            

            Console.WriteLine("\nTesting -- static method");             // method + return
            ShowTime();

            Console.WriteLine("\nTesting -- static method + return");
            Console.WriteLine("ShowTimeReturn " + ShowTimeReturn());

            int data = GetRandomNum();
            Console.WriteLine("\nTesting -- static method + return +_ Random");
            Console.WriteLine("GetRandomNum " + data);


            Console.WriteLine("\nTesting -- parameter ");                  // Parameters  參數
            int A = 1;
            PassByValue(A);
            Console.WriteLine("PassByValue   " + A);   // A 和 i 是複製和獨立的

            A = 1;
            PassByRef(ref A);
            Console.WriteLine("PassByRef   " + A);  // A傳了地址出去做 i ，所以 A和i 事實是同一個 

            int B;
            PassByOut(out B);                        // P119 / 351
            Console.WriteLine("PassByOut   " + B);  // 也是傳 B 的地址出去，而且不先檢查內容，只要有型別對了就行。
                                                    // 所以沒有初始也沒有關係。

            /*
            PassByArray ---- new
            如果有有很多參數的話，它一定要放在最後一個。
            必須一維, 同型別, 
            優點︰可不知個數。
            可在方法中當陣列使用，在外傳入時卻不必...(回來結果就好)
            */
            // 方法一   PassByParam(1, 2, 3, 4)
            Console.WriteLine("PassByParam 1 : " + PassByParam(1, 2, 3, 4));
            // 方法二   PassByParam(ar)
            int[] ar = { 1, 2, 3, 4, 5 };  // 基本Array的做法。
            Console.WriteLine("PassByParam 2 : " + PassByParam(ar));


            /*
            to string  P124

            + 號 連接一個不是string的東西到string的前後時，會自動把 「不是string的東西」 > to string  
            # 所有obj都是從System.Object繼承而來，而System.Object具有to string功能。 
            -------
            Parse
            由 string 到其他型別 :
            但非常容易出ERROR, 因為不是什麼字元都可以轉為其他型別。
                         
             */

            Console.WriteLine("\nTesting --  ToString ");                   //  ToString
            string BtoString = B.ToString(); // int > ToString
            Console.WriteLine("intObj.ToString()  " + BtoString);

            string A_BtoString = A + BtoString;  // int A 因為 + 號 自動 ToString 了。
            Console.WriteLine("A = " + A);
            Console.WriteLine("A + BtoString  " + A_BtoString);

            Console.WriteLine("\nTesting -- Parse ");                      //  Parse + try catch

            string str = "123";
            try
            {
                int strTOnum = int.Parse(str);      // 型別.Parse(stringObj)    string > int
                Console.WriteLine("int.Parse(str)  " + strTOnum);
                // 會做這個，因為行得通。
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   // Obj.ToString      error Object > string
            }

            string str2 = "abc";
            try
            {
                int strTOnum = int.Parse(str2);
                Console.WriteLine("str TO num  " + strTOnum);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // Message 是 ToString() 的簡短版。
                // 會做這個，因為行不通。
            }



            Console.WriteLine("\nTesting -- Datetime");                      //  Datetime  P126 

            DateTime time1 = DateTime.Now;  // not new, only one
            Console.WriteLine("DateTime.Now_time1.ToShortTimeString()  " + time1.ToShortTimeString());
            // 下午 02:34
            Console.WriteLine("DateTime.Now_time1.ToString()  " + time1.ToString());
            // 2020/2/19 下午 02:35:05



            Console.WriteLine("\nTesting for String Handle");               //  String 基本處理 P 130  

            string toupper = "abc";
            Console.WriteLine("ToUpper  " + toupper.ToUpper());
            string tolower = "ABC";
            Console.WriteLine("ToLower  " + tolower.ToLower());
            string substring = "012345";
            Console.WriteLine("Substring  " + substring.Substring(1, 2));
            // from index 1, take 2 value.  # 12
            string replace = "012345";
            Console.WriteLine("Replace  " + replace.Replace("3", "x"));

            string startWith = "abcdefg";
            if (startWith.StartsWith("ac"))
            {
                Console.WriteLine("start with \"ac\"");
            }
            else
            {
                Console.WriteLine("not start with  \"ac\"");
            }

            string endWith = "abcdefg";
            if (endWith.EndsWith("fg"))
            {
                Console.WriteLine("end with \"fg\"");
            }
            else
            {
                Console.WriteLine("not end with  \"fg\"");
            }

            string str3 = "abcdefg";
            if (str3.Contains("de"))       // Contains 比較常用。
            {
                Console.WriteLine("str3.Contains(\"de\")");
            }
            else
            {
                Console.WriteLine("not str3.Contains(\"de\")");
            }


            /*
             
            operators                                                             // 運算子 operators  P 131 


            常用運算子 P131

            +-/*%
            & | ^ ! && || true false
            == != < > <= >=
            = += -= *= /= %= ( &= |= ^= <<= >>= ??  ) < --括號內不常用，知道有就好。
            .  (成員存取？應該是 obj.method 是會用到的意思。)
            []  index
            () explicit
            ?:    條件式   --   string strA =   "1" == "1" ? "b" : "c";     -->      strA = "b"
            new
            as is sizeof typeof
            -----------------

            邏輯運算子  P 133

            && & || | ^ 的分別

            & = 兩者皆真
            | = 兩者皆真 或 其一為真
            ^ = 兩者其一為真

            && 做了第一個是非，就不做第二個。
            &  都會做了再算。

            || 如果頭一個是真，就不做第二個
            |  都會做了再算。

            ^  都會做了再算, 沒有short cut

            -----------
            ++ -- 都是 1 的簡寫。  P134
            -------------
            
            關係運算子  P135

            == != < > <= >=  
            實值型別 - 在關係運算子之間 會直接比 value   eg int a = 1; int b = 1;    a == b
            參考型別 - 如 string 和 obj ，在關係運算子之間比的是地址 = 是不是同一個 物件。
           
            -----------------------
            指派運算子
             +=
             -=

            型別資訊運算子  P137  
            as    //https://dotblogs.com.tw/box5068/2011/01/20/20911  類型兼容測試用。  
            is    // 類型判斷  之後到會Class會再詳細說。
            sizeof
            typeof

                       

            運算子優先順序︰

            請多多使用括號，易於維護。

            除了指派和條件式(三元)運算子，還有先乘除後加減，其他都是由左到右進行。
            x + y + z    equals to     (x + y)+ z
            x + y * z    equals to     x + (y * z)
            x = y = z    equals to     x = (y = z)
             
             */


            Console.WriteLine("\nTesting -- Operators");                   //  Operators   P137

            string IsString = "IsString";
            if (IsString is string)
            {
                Console.WriteLine("IsString is string");
            };

            int intSize = sizeof(int);  // sizeof() = 取得多少空間 > 回 int
            Console.WriteLine("int size    " + intSize);  // 4

            System.Type type = typeof(int); // to get what type
            Console.WriteLine("what int type is ?  " + type);   //  System.Int32

            int type1 = 123;
            // System.Type type2 = typeof(type1);  // <-- typeof 不可放入 變數 或 實值
            System.Type type1stype = type1.GetType();
            Console.WriteLine("The type of type1 is : " + type1stype);    // System.Int32




            Console.WriteLine("\n ----- ----- Additional ------ ----  \n");           // Additional Basic

            /*

            論++i和i++的分別。

             */                                                                                                        // i++, ++i 

            Console.WriteLine("\n--  論++i和i++的分別 :   \n");

            int I = 1;
            Console.WriteLine("\ni = 1 , 印 ++i  = " + (++I));   //    加了再印 : 2
            Console.WriteLine("i = 2 ,印 i++  = " + (I++));  //        印了再加︰2
            Console.WriteLine("單純印出來現在的值 =  " + I); //        現在的值︰ 3

            I = 1;
            int addBefore = ++I;  // 他己經是 3 了，加 1 再給予。  
            int addAfter = I++;   // 給予了4 再加 1。
            Console.WriteLine("\nadd Before give to =  " + addBefore); //印出來︰ 2
            Console.WriteLine("add After give to =  " + addAfter);     //印出來︰ 1
            Console.WriteLine("現在的值 =   " + I);//印出來︰ 2



            // i++ ++i in loop

            Console.WriteLine("\n--  論++i和i++在loop :   \n");

            string[] items = { "a", "b", "c", "d" };
            int inc = 0;

            Console.WriteLine("同樣固定 loop 四次 : \n");
            Console.WriteLine("count items ++inc");
            foreach (string item in items)  
            {
                Console.Write(++inc);     // 1234
            }
            Console.WriteLine();

            Console.WriteLine("count items inc++");
            inc = 0;
            foreach (string item in items)
            {
                Console.Write(inc++);     // 0123
            }
            Console.WriteLine();

            Console.WriteLine("\n如果 i++ 是放在for loop的條件用, 沒有分別 : \n");
            Console.WriteLine("\nfor (int i = 0; i < 5; i++)");
            for (int i = 0; i < 5; i++) { Console.Write(i); }     //01234
            Console.WriteLine();

            Console.WriteLine("for (int i = 0; i < 5; ++i)");
            for (int i = 0; i < 5; ++i) { Console.Write(i); }     //01234
            Console.WriteLine("\n");

            Console.WriteLine("但是 如果隔了一個給予的動作 n = i++ / ++i , 就有差 : \n");
            Console.WriteLine("int i = 0; n < 5; n = i++");
            int n = 0;
            for (int i = 0; n < 5; n = i++) { Console.Write(1); }   // 6*1  
            Console.WriteLine();

            n = 0;
            Console.WriteLine("int i = 0; n < 5; n = ++i");
            for (int i = 0; n < 5; n = ++i) { Console.Write(1); }   // 5*1
            Console.WriteLine();

            // 論 | & ^
            Console.WriteLine("\n------------- 論 | & ^ ----------- \n");
            bool T = true;
            bool F = false;

            Console.WriteLine("T & T  = " + (T & T));     // < 要()包起來。   // V
            Console.WriteLine("T & F  = " + (T & F));
            Console.WriteLine("F & T  = " + (F & T));
            Console.WriteLine("F & F  = " + (F & F));

            Console.WriteLine("\n  &  等於要兩個皆是。\n");

            Console.WriteLine("T | T  = " + (T | T));   // V
            Console.WriteLine("T | F  = " + (T | F));   // V
            Console.WriteLine("F | T  = " + (F | T));   // V
            Console.WriteLine("F | F  = " + (F | F));

            Console.WriteLine("\n  |  等於中一個也好，除非全部都不是。\n");

            Console.WriteLine("T ^ T  = " + (T ^ T));
            Console.WriteLine("T ^ F  = " + (T ^ F));  // V
            Console.WriteLine("F ^ T  = " + (F ^ T));  // V
            Console.WriteLine("F ^ F  = " + (F ^ F));

            Console.WriteLine("\n  ^  等於只可以是一是一否，不能一致。\n");

            Console.WriteLine("\n------------- 論 | || & && ----------- \n");

            int cal1 = 0;
            int cal2 = 0;
            int cal3 = 1;
            int cal4 = 1;

            Console.WriteLine("(++cal1 == 1 || ++cal2 == 1)  =  " + (++cal1 == 1 || ++cal2 == 1));    //true
            Console.WriteLine("cal1  = " + cal1);    // 1
            Console.WriteLine("cal2  = " + cal2);    // 0     <----------- 做了第一個是對的，第二個就不做了。

            Console.WriteLine("做了第一個是對的，第二個就不做了。\n");

            Console.WriteLine("(++cal3 == 1 && ++cal4 == 1)  =  " + (++cal3 == 1 && ++cal4 == 2));    //false
            Console.WriteLine("cal3  = " + cal3);    // 2    
            Console.WriteLine("cal4  = " + cal4);    // 1     <--------------做了第一個是錯的，第二個就不做了。

            Console.WriteLine("做了第一個是錯的，第二個就不做了。\n");

            cal1 = 0;     // 回復原來的值
            cal2 = 0;
            cal3 = 1;
            cal4 = 1;

            Console.WriteLine("(++cal1 == 1 | ++cal2 == 1)  =  " + (++cal1 == 1 | ++cal2 == 1));    //true
            Console.WriteLine("cal1  = " + cal1);    // 1
            Console.WriteLine("cal2  = " + cal2);    // 1     <----------- 兩個都做。

            Console.WriteLine("兩個都做。\n");

            Console.WriteLine("(++cal3 == 1 & ++cal4 == 1)  =  " + (++cal3 == 1 & ++cal4 == 2));    //false
            Console.WriteLine("cal3  = " + cal3);    // 2    
            Console.WriteLine("cal4  = " + cal4);    // 2     <----------- 兩個都做。

            Console.WriteLine("兩個都做。\n");


            // 

            //  this two can hold the window if run it by F5 
            //  Console.ReadLine();
            //  Console.ReadKey();

            /*
             ------------------------------------------其他-----------------------



            1. 論如何可以輸入Main方法的參數。 > 從CMD中 cd 到 .exe 空一格後的文字就是輸入的參數。<--少用

            2. 經常要更新，減少中毒的可能︰
            檢查Google chrome 的更新 : 右上三點 > 說明 > 關於Google chrome
            visual studio enterprise 2017 的更新 : 開始 > 滑到 Visual Studio Installer > 是否？: 是 > 查看到是否最新版本
             
             */

        }
    }
}
