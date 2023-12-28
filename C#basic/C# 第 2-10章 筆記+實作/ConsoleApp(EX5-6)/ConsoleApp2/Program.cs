using System;
using System.Collections;  //原本是︰using System.Collections.Generic; , 但是 ArrayList 的使用，超越了 Generic 範圍
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp2      
{
    enum Animal
    {
        dog,
        cat,
        fish
    }

    // 繼承 :
    // 自創的Exception在實務上不常用，知道有就好。
    public class BlackException : Exception { }


    class Program
    {

        public static void Rainbow()   //雖然必throw Exception, 但 Exception 不是return 的，所以還是寫 void
        {

            try
            {
                int zero = 0;
                int zroedivisionerror = 3 / zero;
            }
            catch
            {
                throw new BlackException();
            }

        }

        static void Main(string[] args)
        {

            // chapter 5     P141

            Console.WriteLine("\ntesting --  if \n");                 // if else else if

            Console.WriteLine("The time now is : ");


            if (DateTime.Now.Hour < 9)
            {
                Console.WriteLine("early morninig");
            }
            else if (DateTime.Now.Hour < 12)  //  else if 可以有很多。
            {
                Console.WriteLine("morninig");
            }
            else
            {
                Console.WriteLine("afternoon");
            }  // 不需要分號


            Console.WriteLine("\ntesting --  switch \n");

            Console.WriteLine("if i hv a cat : ");
            Animal pet = Animal.cat;                            // switch
            switch (pet)
            {                //如果pet這裡要運算，結果一定要是 整數﹑enum﹑char﹑string
                case Animal.cat:
                    Console.WriteLine("meow");
                    break;
                case Animal.dog:
                    Console.WriteLine("bark");
                    break;
                default:
                    Console.WriteLine("。");
                    break;  //<- even default need this break, must have to add at any "case"
            }

            Console.WriteLine("\nswitch的多重選擇");
            Console.WriteLine("my cat can : ");
            switch (pet)
            {
                case Animal.cat:
                case Animal.dog:                                // switch的多重選擇
                    Console.WriteLine("walk");
                    break;
                default:
                    Console.WriteLine("swim");
                    break;

            }

            Console.WriteLine("\nswitch的多重選擇 + goto ");
            Console.WriteLine("my cat can not : ");              // switch的多重選擇 + goto
            switch (pet)
            {

                case Animal.cat:
                    goto default;        // goto default
                case Animal.dog:
                    goto case Animal.cat;   // 我的狗和貓一樣不會游泳。
                case Animal.fish:
                    Console.WriteLine("walk");    // goto 在工作上不要用太多，易錯。
                    break;
                default:
                    Console.WriteLine("swim");
                    break;
            }



            Console.WriteLine("\ntesting --  for loop \n");              // for loop
                                                                         //已知次數的loop
            int loopTimes = 0;
            for (int i = 1; i < 100; i++)
            {
                loopTimes += 1;
            }
            Console.WriteLine("(int i = 1; i<100; i++)  loopTime : " + loopTimes);  //  99


            loopTimes = 0;
            for (int i = 0; i < 100; i++)
            {
                loopTimes += 1;
            }
            Console.WriteLine("(int i = 0; i<100; i++) loopTime : " + loopTimes);  // 100


            Console.WriteLine("\ntesting --  for each \n");              // for each 
                                                                         // 數 array
            Animal[] pets = { Animal.cat, Animal.dog, Animal.fish };
            Console.WriteLine("types of pet : ");
            foreach (Animal p in pets)
            {
                Console.Write(p + " ");
            }
            Console.WriteLine();


            Console.WriteLine("\nforeach (string name in Enum.GetNames(typeof(Animal)))");
            foreach (string name in Enum.GetNames(typeof(Animal)))    // 數 Enum
            {
                Console.Write(name + " ");
            }
            Console.WriteLine();

            Console.WriteLine("\nforeach (object value in Enum.GetValues(typeof(Animal))) ");
            foreach (object value in Enum.GetValues(typeof(Animal)))   // Enum 會自動的編數值
            {
                Console.Write((Int32)value + " ");   // 強轉型別
            }
            Console.WriteLine();



            Console.WriteLine("\ntesting --  while \n");                // while
                                                                        // 直到條件完成
            int start = 0;
            int stop = 5;

            while (start != stop)
            {
                start++;
                Console.Write("1");
            }                            //  11111
            Console.WriteLine();


            Console.WriteLine("\ntesting --  do while \n");              // do while
                                                                         // 直到條件完成前，起碼做一次
            start = 0;
            stop = 0;
            do
            {
                Console.WriteLine("do once while statment is false");
            } while (start != stop);  // <- false

            Console.WriteLine("\ntesting -- do while + Readline + break 猜數字 \n");


            //使用GUID作seed亂數h : https://blog.xuite.net/sunnysoap/r/193084992-c%23+-Guid.NewGuid+%28%E9%9A%A8%E6%A9%9F%E6%95%B8%E5%AD%97+Random%29

            Random rnd = new Random(Guid.NewGuid().GetHashCode());         //使用GUID作seed產生亂數   這個比Random ()更好  
            int bingo = rnd.Next(1, 2);   // 1-2

            string guess;
            do
            {
                Console.WriteLine("猜 1 或 2 :  ");

                guess = Console.ReadLine();
                int input = int.Parse(guess);

                if (input == bingo)
                {
                    Console.WriteLine("Bingo");
                    break;
                }

            } while (true);



            Console.WriteLine("\ntesting -- continue \n");    // continue

            int[] intArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int num in intArray)
            {
                if (num % 2 == 0)
                {
                    continue;      // 如雙，略過下方繼續   //要是 continue 打成 return 會跳出整個Main, 在其他method中也是這樣。
                }
                Console.Write(num + "  ");
            }
            Console.WriteLine();

            Console.WriteLine("\ntesting -- goto + Mark(/label) \n");          // goto 標記

            int[] intArray2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (int num in intArray2)
            {

                if (num == 9)
                {
                    goto nine;      // goto 會跳出迴圈，永遠也不可能輸到  "10"      
                }

                if (num == 5)
                {
                    Console.WriteLine("this is five");      //    不會跳出迴圏   
                }

                Console.WriteLine(num);

                //mark不要放在loop裡面

            }

        nine:     // 不可以放break/continue等東西，回不去迴圏了。
            Console.WriteLine("this is 9 and GOTO jump out of loop to LABEL , 10 不會被foreach輪到了。");



            Console.WriteLine("\ntesting -- try catch + print exception\n");                   // try catch

            try
            {
                object obj = null;  //  
                string @string = obj.ToString();  // NullReferenceException   //使用保留字命名變數要前面加 @
            }
            catch (Exception ex)   // 物件為 Null 無法ToString()
            {
                Console.WriteLine("Error Message :  " + ex.Message + "\n");
            } // Error Message :  並未將物件參考設定為物件的執行個體。  

            Console.WriteLine("\ntesting --  catch error type \n");            // try catch  + catch error type
            try
            {
                object obj = null;
                string @string = obj.ToString();  // NullReferenceException
            }
            catch (NullReferenceException nex)   // 中這一條
            {
                Console.WriteLine("NullReferenceException + nex.Message :  \n\n " + nex.Message + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN Error  :   " + ex.Message + "\n");
            }

            // 不能把範圍大的 Exception 放在小的Exception 前 , 試解封下方，會出現紅線。

            //catch (IndexOutOfRangeException iex)
            //{
            //    Console.WriteLine("IndexOutOfRangeException Error  :   " + iex.Message + "\n");
            //}



            Console.WriteLine("\ntesting -- ex.ToString() + finally \n");                 // try catch + finally 
            try
            {
                object obj = null;
                string @string = obj.ToString();  // NullReferenceException
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex.ToString()  :   \n\n" + ex.ToString() + "\n");
            }   // ToString 比 Message 長很多。
            finally
            {
                Console.WriteLine("finally : test over\n");
            };


            Console.WriteLine("\ntesting -- throw + self-def Exception \n");     // throw + self-def Exception

            //self-def Exception : https://dotblogs.com.tw/atowngit/2009/12/06/12298

            try
            {
                Rainbow();
            }
            catch (BlackException)    // self-def Exception在工作上比較少用，下一個例子會比較多。
            {
                Console.WriteLine(" I catch the self-def Exception. ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN Error  :   " + ex.Message + "\n");
            }



            Console.WriteLine("\ntesting -- throw + self-def Exception's message \n");     // throw + self-def Exception's message

            try
            {

                throw new Exception("self-def Exception's message");

            }
            catch (Exception ex)
            {
                Console.WriteLine("self-def Exception's message  :   " + ex.Message + "\n");
            }

            Console.WriteLine("\n----------------------------ch6------------------------------\n");
            // chapter 6     P178  
            Console.WriteLine("\ntesting -- Array \n");                                               // Array

            int[] oneD;    //  一維
            int[,] twoD;   //  二維

            int[] oneDA_threeElems = new int[3];  // 1 D array with 3 empty elements. default 0 = 0,0,0
            // 1D instance 包給值
            int[] dirEnterElem = { 1, 2, 3 };

            // 1D 只宣告後給值:
            // oneD = { 1 , 2 , 3 };     < ---------- error : 不可以這樣給值。
            // oneD = new int { 1, 2, 3 }; < ---------- error: 不可以這樣給值。
            oneD = new int[3];      // 0,0,0                                                                // 1 D Array
            oneD[0] = 1;
            oneD[1] = 2;
            oneD[1] = 3;

            // 2D 只宣告後給值:
            twoD = new int[3, 2];   // 3 row, 2 col = 6 elements , default 0 = 6*0
            // 2D instance 包給值
            int[,] dirEnterElem_2D = { { 1, 2 }, { 3, 4 }, { 5, 6 } };                              // 2 D Array
            

            // 不規則多維陣列                                                                       // Jagged Array

            int[][] irr2D = new int[2][];   // 2 row, ? col
                                            //  int[][] irrNO_RC = new int[][];    < ---------- error : 必需要有row, 只有col也不行
                                            //  int[][] irrNO_RC = new int[2][1];  < ---------- error : 也不可以兩個都有，因為是「不規則的」建立方法。
            irr2D[0] = new int[3];     // 1 row : 3 col
            irr2D[1] = new int[2];     // 2 row : 2 col

            /*
                    000
                    00
                            */

            irr2D[0] = new int[] { 0, 1, 2 };   // <-- 比較方便的做法
            irr2D[1] = new int[] { 0, 1 };

            /*
                    012
                    01
                            */

            // 不規則給值的方法詳見︰  (<-- 但實務上不常用。)
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/arrays/jagged-arrays


            Console.WriteLine("\ntesting -- Array 以 foreach 給值 \n");

            int val = 10;
            // foreach (int i in oneDA_threeElems) { i = jq++; } 
            // 不可以這樣寫，因為枚舉的Array值只可以判斷，不可以更改。

            // foreach (int i in oneDA_threeElems) { oneDA_threeElems[i] = jq++; }  
            // 這樣寫的話，只有第一個index會被改變。因為 i = 0 // 12,0,0
            int it = 0;
            foreach (int i in oneDA_threeElems) { oneDA_threeElems[it] = val++; it++; }
            // 用來控制index的，和數值的各自不同。

            Console.WriteLine("印出 1D Array 用 foreach 給值的成果︰");
            foreach (int i in oneDA_threeElems) { Console.Write(i + "  "); };
            Console.WriteLine();

            // ------------------2D 用 foreach 給值?---------------------------------

            it = 0;
            // foreach 無法分開 twoD Array 的一二層。
            // foreach (int i in twoD[0]) {};  
            Console.WriteLine("\n2D Array 無法用 foreach 給值, 但可以一次歷遍 ︰");
            foreach (int i in twoD) { Console.Write(i + "  "); };   // foreach 是可以一次歷遍"規則的"多維陣列的。
            Console.WriteLine();

            // ------------------irr 用 foreach 給值?---------------------------------

            // irr 可以分層 for each 給值
            it = 0;
            foreach (int i in irr2D[0]) { irr2D[0][it] = val++; it++; };
            it = 0;
            foreach (int i in irr2D[1]) { irr2D[1][it] = val++; it++; };

            Console.WriteLine("\n印出 irr 2D Array 用 foreach 給值的成果︰");
            // foreach(int i in irr2D) { Console.Write(i + "  "); };  
            // foreach 不可以一次歷遍不規則陣列。
            Console.WriteLine(irr2D);   // < ------------- 只印出它的類型，而不是內容。
            // System.Int32[][]

            it = 0;
            foreach (int i in irr2D[0]) { Console.Write(i + "  "); };
            Console.WriteLine();
            it = 0;
            foreach (int i in irr2D[1]) { Console.Write(i + "  "); };
            Console.WriteLine();



            Console.WriteLine("\ntesting -- Index \n");     // P186                                 // index

            // 二維index

            int[,] twoD_index = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            int index1 = twoD_index[0, 1];    // 2
            Console.WriteLine("twoD_index [0,1]  :  " + index1);

            // 不規則多維index

            /*
                13  14  15
                16  17
                            */
            Console.WriteLine("\nirr2D[0][0]  :  " + irr2D[0][0]);    // 13
            Console.WriteLine("irr2D[1][1]  :  " + irr2D[1][1]);    // 17


            // to aviod  IndexOutOfRangeException                                                          
            // -- .Length
            // -- .GetLength


            Console.WriteLine("\ntesting -- Length  + GetLength \n");
            /*
             oooooo
             */
            // row.GetLength(0) == 6
            // row.Length == 6

            int[] row = new int[6];
            Console.WriteLine("row.GetLength(0) : " + row.GetLength(0));
            Console.WriteLine("row.Lenght :  " + row.Length);

            /*
             oooo
             oooo
             */

            // int[][] grid = new int[2][4]; <--- error 
            // https://www.itread01.com/content/1545271049.html
            /*
                int[][]是不規則陣列的宣告方式，所以第一次給值只可以給row值，不可以給col值
                int[][] grid = new int[2][];
             */
            int[,] grid = new int[2, 4];  // <----- 這個才是二維規則陣列的宣告。

            Console.WriteLine("\ngrid.GetLength(0) : " + grid.GetLength(0));
            Console.WriteLine("grid.GetLength(1) : " + grid.GetLength(1));
            Console.WriteLine("grid.Length  : " + grid.Length);

            // grid.GetLength(0)  == 2  <-- how many row
            // grid.GetLength(1)  == 4  <-- how many column
            // grid.Length = 2*4 = 8    <-- how many element



            // 陣列大小無法調整，只可以選擇某些元素，再複製到另一個陣列                   // Array.Copy

            Console.WriteLine("\ntesting -- Array.Copy() \n");

            // 取代
            int[] beReplaceArray = { 1, 2, 3 };
            beReplaceArray = new int[10];         // 覆寫了原本的定義。
            Console.WriteLine("foreach int i in beReplaceArray : ");
            foreach (int i in beReplaceArray)  {  Console.Write(i);  };   // 0000000000
            Console.WriteLine();           

            int[] oldArray = { 1, 2, 3 };   // 123
            int[] newArray = new int[10];  // 0000000000
            Array.Copy(oldArray, newArray, oldArray.Length);   // <----- (從那,到那,搬多少個？.Length = 全搬)

            Console.WriteLine("\nforeach int i in newArray : ");
            foreach (int i in newArray)  
            {
                Console.Write(i);         // 1230000000
            };
            Console.WriteLine();


            int[] newArray2 = new int[10];
            Array.Copy(oldArray, 0, newArray2, 4, 2);  //<----- 從oldArray[0]開始，只搬2個，到newArray2[4]開始取代

            Console.WriteLine("\nforeach int i in newArray2 : ");
            foreach (int i in newArray2)
            {
                Console.Write(i);         // 0000120000
            };
            Console.WriteLine();



            Console.WriteLine("\ntesting -- ArrayList \n");                                     // ArrayList

            // 不同於Array，本身就可以自動調整大小，(Linked list ?)
            // ArrayList 元素的型別可不同。
            // ArrayList 可唯讀
            // 總︰Array的速度快，但ArrayList彈性好。

            /* .Add .Clear .Contain .IndexOf .Remove .RemoveAt .Count*/

            ArrayList myAL = new ArrayList();
            //原本是︰using System.Collections.Generic; , 但是ArrayList的使用，超越了Generic範圍
            //所以如果出錯，記得要改範本。

            myAL.Add("love");                        // .Add
            myAL.Add("patient");
            myAL.Add("hope");
            if (myAL.Contains("hope"))
            {             // .Contains()
                Console.WriteLine("myAL contain hope. ");
                Console.WriteLine("the index of hope is : " + myAL.IndexOf("hope"));

                // .IndexOf 
            }

            myAL.Remove("hope");                     // .Remove
            if (!myAL.Contains("hope"))
            {
                Console.WriteLine("myAL have removed hope. ");
            }

            myAL.RemoveAt(1);                        // .RemoveAt
            if (!myAL.Contains("patient"))
            {
                Console.WriteLine("myAL have removed patient by index. ");
            }

            myAL.Clear();                             // .Clear

            Console.WriteLine("The size of myAL is  : " + myAL.Count);   // 0

            // .Count


            // 唯讀
            ArrayList myReadOnlyAL = ArrayList.ReadOnly(myAL);
            // 把一個已建好的 A L用 ArrayList.ReadOnly 的方法，轉為另一個新的唯讀 AL

            try
            {
                myReadOnlyAL.Add("error");
            }
            catch (Exception ex)
            {
                Console.WriteLine("這是 myReadOnlyAL.Add() 的 ex.Message  : " + ex.Message);
            }


            Console.WriteLine("\ntesting -- var in foreach + ArrayList的缺點。");                                //     var
                                                                                               //當collection中不只有一個型態的物件, foreach的運用需要配合var

            ArrayList varTypeAL = new ArrayList();
            varTypeAL.Add("str1");
            varTypeAL.Add(2);
            varTypeAL.Add(true);  //True  <-- ToString 後，首字變成了大寫。
            varTypeAL.Add(DateTime.Now);

            foreach (var ele in varTypeAL)
            {
                //Console.WriteLine(ele);  // int,bool, DateTime 都可以由 WriteLine 去自動 ToString
                try { Console.WriteLine(Convert.ToInt32(ele)); }
                catch { Console.WriteLine("轉型失敗"); }
            };


            // Collection 不限型別 的缺點
            // 1. 就是當要取出使用時如果 轉型錯誤 的話，會容易出現 error。
            // 2. 可以覆寫Add限輸入類型, 但ArrayList還是存它們為Object。object 的 boxing, unboxing 很秏時間, 影響到效能。
            // 為什麼 ArrayList 會被 List 淘汰？ : https://ithelp.ithome.com.tw/articles/10227210
            // - 泛型 - 改善以上缺點 : 限製Collection的元素型別 ， 在.Add 的時候就已經在審核。
            // 那為什麼不用 Array ? -- 因為 Array 又沒有了可以自由增減元素的優點。

            // 解決的答案︰  P200
            // https://ithelp.ithome.com.tw/articles/10194019                                    // 泛型

            Console.WriteLine("\ntesting -- 不用泛型,用if...else判斷參數是要什箱子。 \n");

            BoxFactory1 Factory1 = new BoxFactory1();
            object sbm = Factory1.GetSelfDefBox("square");     // 正方型箱子 // if else 所設的是除了三角形以外，都是正方型。
            object tbm = Factory1.GetSelfDefBox("triangle");   // 三角型箱子

            Console.WriteLine("\ntesting -- 用泛型,在藍圖時先不設定要什麼，直接輸入要什麼箱子。 \n");

            BoxFactory2 Factory2 = new BoxFactory2();
            Factory2.GetBox<SquareBoxMaker>();                 // 正方型箱子
            Factory2.GetBox<TriangleBoxMaker>();               // 三角型箱子  // 說要什麼，就有什麼。
            Factory2.GetBox<BoxFactory1>();                    // 就連不是箱子也會照做出來。所以這個做法，沒有檢查型別的作用。


            // List 應用泛型的例子

            ArrayList NorL = new ArrayList();   // List 一定要配泛型才可以 new , 在實務上，List 因此比 ArrayList 常用。
            NorL.Add(123);
            // string getNorEle = (string)NorL[0];    
            //  ^ 這個在編寫時，看起來一點問題都沒有，但是會有 Run Time Error !  <-- 缺點 

            List<object> fsdf = new List<object>();          // 這個泛型就是做了等於沒有做。
            fsdf.Add("123");
            fsdf.Add(123);

            List<int> GintL = new List<int>();
            // GintL.Add("123"); // <-- error
            GintL.Add(123);

            List<string> GenStrL = new List<string>();
            GenStrL.Add("123");
            // GenStrL.Add(123);  // <-- 在加入時，已經會自動地檢查出錯誤。   Complie-Time Error !

            Console.WriteLine("\ntesting -- 不用泛型 + foreach \n");
            int total = 0;
            NorL.Add("123");
            NorL.Add(true);
            // foreach(var num in NorL){ total += num; }   // += 不可以套用到 int (total) 和 object (num) 之間
            // foreach (var num in NorL) { total += (int)num; } // Complie OK >  Run Time Error

            Console.WriteLine("\ntesting -- 用泛型 + foreach \n");
            GenStrL.Add("456");
            GenStrL.Add("789");
            // foreach (var num in GenL) { total += double.Parse(num); }   // += 不可以套用到 int(total) 和 double (num) 之間
            foreach (var num in GenStrL) { total += int.Parse(num); } // Complie OK =  Run Time OK = 減少BUG的發生
            Console.WriteLine("用泛型 + foreach 做 Total : " + total);



            //--------------------------------- additional -------------------------------------------------




            Console.WriteLine("\nadditional --  List<T> 的複製 \n");
            // http://wukuohsing.blogspot.com/2016/10/c-list-list.html

            List<char> List1 = new List<char>();

            List1.Add('A');
            List1.Add('B');
            List1.Add('C');
            List1.Add('D');

            Console.WriteLine("方法一 :  List2 = List1.ToList();");
            List<char> List2 = new List<char>();
            List2 = List1.ToList();

            Console.WriteLine("方法二 :  List<string> List3 = new List<string>(List1);");
            List<char> List3 = new List<char>(List1);

            Console.WriteLine("方法三 :  List4.AddRange(List1); ");
            List<char> List4 = new List<char>();
            List4.AddRange(List1);

            Console.WriteLine("方法四 (指定範圍):  List5.AddRange(List1.GetRange(0, 2)) ");
            List<char> List5 = new List<char>();
            List5.AddRange(List1.GetRange(0, 2));  // <--- 不包括第三個。

            Console.WriteLine("\ntesting : \n  ");
            foreach (char i in List2) { Console.Write(i); }; Console.WriteLine();  //ABCD
            foreach (char i in List3) { Console.Write(i); }; Console.WriteLine();  //ABCD
            foreach (char i in List4) { Console.Write(i); }; Console.WriteLine();  //ABCD
            foreach (char i in List5) { Console.Write(i); }; Console.WriteLine();  //AB


            Console.WriteLine("\nadditional -- 由 List 到 Array 的複製 \n");

            // https://docs.microsoft.com/zh-tw/dotnet/api/system.collections.generic.list-1.copyto?view=netframework-4.8

            Console.WriteLine(" 將整個 List<T> 複製到相容的一維陣列，從目標陣列的開頭開始 ");
            // 可能會出現的Excaption : 
            // ArgumentNullException  -- array 為 null。
            // ArgumentException  -- 來源 List<T> 中的項目數大於目的 array 可包含的項目數。

            Console.WriteLine("\ntesting : List1.CopyTo(Array); \n");

            char[] listToArray = new char[4];
            List1.CopyTo(listToArray);   
            foreach (char i in listToArray) { Console.Write(i); }; Console.WriteLine();  // ABCD

            char[] listToArray2 = new char[10];                        // <-- 即使Array長度不合，但可以。
            List1.CopyTo(listToArray2);
            foreach (char i in listToArray2) { Console.Write(i); }; Console.WriteLine();  // ABCD


            Console.WriteLine("\nadditional -- for + continue + break \n");               // for + continue + break

            int[] intArray3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            for (int i = 0; i < 9; i++)
            {
                if (intArray3[i] % 2 == 0) { continue; }  // skip when even number
                if (intArray3[i] == 7) { break; }     // jump out from the loop when 7 appears. 
                Console.Write(intArray3[i]);
            }
            Console.WriteLine();                // 135


            Console.WriteLine("\nadditional -- 9*9 table \n");                            // 9*9 table

            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    Console.Write(i + " * " + j + " = " + i * j + "  ");
                }// 如果 break 放在這裡 ?
                Console.WriteLine();
                // continue;  // 放不不放
            };


            Console.WriteLine("\nadditional -- id number format ?  1  \n");                  // substring + ? : + Char.IsLetter + .Parse + .Trim

            // https://www.itdaan.com/tw/58d31c99b1f8ab3adb2f7cda9dec3bc3
            // https://nwpie.blogspot.com/2017/03/3_4.html

            bool again = true;

            do
            {
                Console.Write("Please input a ID number : ");
                string input = Console.ReadLine();

                try
                {
                    if (input.Length != 10) { throw new Exception("長度不對"); }

                    string cap = input.Substring(0, 1);      // 從 0 到 1 ，不包含 1
                    // Console.WriteLine(cap);  // 可以印出來看看。

                    string numbers = input.Substring(1);    // 從 1 開始
                    // Console.WriteLine(numbers);

                    char c = char.Parse(cap);     // <----- 這個方法，竟然還是可以包括符號和數字。
                    if (!Char.IsLetter(c)) { throw new Exception("第一個字是符號或數字"); }  

                    double num = double.Parse(numbers);
                    // Console.WriteLine(num);

                    Console.WriteLine();
                    Console.WriteLine("ID format = True ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("ID format = False ");
                }

                Console.Write("\nAgain ? (y/n)    : ");
                again = Console.ReadLine().Trim() == "y" ? true : false;     // .Trim() 可以空個幾格再來打 y 

            } while (again);


            // 身份證有更多數字上的規則，詳見以下這兩個網址。
            // https://dotblogs.com.tw/sam/2010/03/29/14282
            // https://dotblogs.com.tw/hung-chin/2011/10/03/38633

        }
    }
}
