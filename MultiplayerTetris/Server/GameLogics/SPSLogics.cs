using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.GameLogics
{
    class SPSLogics
    {
       

 
        //Heel simpel
        string OneWin = "Player one won!!";
        string TwoWin = "Player two won!!";
        String Tie = "Its a Tie!!";
        string answer;
        string answer2;
        public void Case1()
        {
            switch (answer2)
            {
                case "Stone":
                    Console.WriteLine(Tie);
                    break;
                case "Paper":
                    Console.WriteLine(TwoWin);
                    break;
                case "Scissor":
                    Console.WriteLine(OneWin);
                    break;
            }
        }
        public void Case2()
        {
            switch (answer2)
            {
                case "Stone":
                    Console.WriteLine(OneWin);
                    break;
                case "Paper":
                    Console.WriteLine(Tie);
                    break;
                case "Scissor":
                    Console.WriteLine(TwoWin);
                    break;
            }
        }
        public void Case3()
        {
            switch (answer2)
            {
                case "Stone":
                    Console.WriteLine(TwoWin);
                    break;
                case "Paper":
                    Console.WriteLine(OneWin);
                    break;
                case "Scissor":
                    Console.WriteLine(Tie);
                    break;
            }
        }
        public void PlayGame()
        {
           
            switch (answer)
            {
                case "Stone":
                    Case1();
                    break;
            
                case "Paper":
                    Case2();
                    break;

                case "Scissor":
                    Case3();
                    break;
            }
          
        }
    }
}
