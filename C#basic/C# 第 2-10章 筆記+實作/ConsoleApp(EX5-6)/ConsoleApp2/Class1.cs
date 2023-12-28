using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    // -- 泛型 

    public class SquareBoxMaker
    {
        public SquareBoxMaker()                    // 建構子
        {
            Console.WriteLine("正方型箱子");
        }
    }

    public class TriangleBoxMaker
    {
        public TriangleBoxMaker()
        {
            Console.WriteLine("三角型箱子");
        }
    }

    // 非泛型的例子︰

    public class BoxFactory1
    {

        public object GetSelfDefBox(string shape)    // 在用 method 建做時，已定下型狀。
        {
            if (shape == "triangle")
            {
                return new TriangleBoxMaker();
            }
            else                                    // 一定要是 else, C# 要保證 shape 不論是什麼，都會有 object 可以回傳。
            {                                       // 否則會發生︰ 不是所有程式碼路徑都有回傳值  的 error
                return new SquareBoxMaker();
            }
        }
    }

    /*
     非泛型的 呼叫方法︰

         BoxFactory Factory1 = new BoxFactory();
         object sbm = Factory1.GetSelfDefBox("square");
         object tbm = Factory1.GetSelfDefBox("triangle");
     */

    // 使用泛型的例子︰

    public class BoxFactory2
    {
        /*
             泛型的製造機: 
             在定義時使用泛型先暫緩型別的規格定義，到了實體化時再定義其型別規格
        */
        public object GetBox<T>() where T : new()     // T 的型別，是在呼叫時才輸入。
        {
            return new T();   // 如果 T 不是箱子？
        }

    }

    /*
    泛型的方便例子 呼叫方法︰         

         BoxFactory2 Factory2 = new BoxFactory2();
         Factory2.GetBox<SquareBoxMaker>();           
         Factory2.GetBox<TriangleBoxMaker>();   // 但問題是，就算丟進去的東西不是箱子，它還是會 new 出來。

    */

}



