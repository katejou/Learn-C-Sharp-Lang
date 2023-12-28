using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_EX8_    // <--- 這個 namespace 就是 Program.cs 的 namespce 
{
    class SamePorjOtherFileSon : TestOnModifire
    {
        public string SamePorjOtherFileSonGetFields() {
            return // private_String + "\n" +  // <----  父親的private, 兒子沒有權限
            internal_String + "\n"
            + protected_String + "\n"
            + public_String + "\n"
            + protected_Internal_String + "\n"
            + private_protected_Stirng;

        }
    }
}
