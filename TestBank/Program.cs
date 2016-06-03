using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbcBank;
using AbcBank.Test;
namespace TestBank
{
    class Program
    {
        static void Main(string[] args)
        {
           CustomerTest cust = new CustomerTest();
         
            cust.testApp();
            //cust.testOneAccount();
            //cust.testTwoAccount();
            //cust.testThreeAcounts();

           

        }
    }
}
