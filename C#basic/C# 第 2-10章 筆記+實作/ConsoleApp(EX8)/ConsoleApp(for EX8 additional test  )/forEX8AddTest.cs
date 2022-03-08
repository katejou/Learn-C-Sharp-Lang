using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_EX8_;

namespace ConsoleApp_for_EX8_additional_test___
{        /*
          private 限同 class 
          internal 限同 project        
          protected 同 type         
          public 沒有限
          protected internal 同 type | 同 project
          private protected 同 class & 同 type        
        */

    class OtherPorjectSon : TestOnModifire
    {
        public string OtherPorjectSonGetFields()
        {
            TestOnModifire t1 = new TestOnModifire();

            return // private_String + "\n" +  // <----  父親的private, 兒子沒有權限
                   // internal_String + "\n"  // <----- 第二個 Project 的兒子沒有權限
            protected_String + "\n"
            + public_String + "\n"
            + protected_Internal_String + "\n";   // protected V , Internal X  = OK
            //+ private_protected_Stirng ;        //  protected V , Internal X = NO

        }
    }
    class forEX8AddTest
    {
        static void Main(string[] args)
        {

            OtherPorjectSon OPson = new OtherPorjectSon();

            Console.WriteLine("\nMain other project Son get 3 : \n");
            // Console.WriteLine(OPson.pirvate_String);  
            // Console.WriteLine(OPson.protected_String);
            // Console.WriteLine(OPson.private_protected_String); 
            // Console.WriteLine(OPson.internal_String);
            Console.WriteLine(OPson.public_String);
            //Console.WriteLine(OPson.protected_Internal_String);

            Console.WriteLine("\nSon Of Other Project Get 3  : \n\n" + OPson.OtherPorjectSonGetFields());  // 不同 Project 的兒子 權限 有差。 沒了 internal
            Console.WriteLine("\nOther Project's Son DO Dad's Method Get 6  :  \n\n" + OPson.DadPublicMethodGetFields());   // 同 Project 不同 flie 的兒子 權限沒有差。
            

        }
    }
}
