using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.GameLogics
{
    class SPSLogics
    {
       

        string PlayerOneChoice;
        string PlayerTwoChoice;
        //Heel simpel
        public void PlayGame()
        {
            string OneWin = "Player one won!!";
            string TwoWin = "Player two won!!";
            String Tie = "Its a Tie!!";
            if(PlayerOneChoice == "Stone")
            {
                if (PlayerTwoChoice == "Stone")
                {
                    Console.WriteLine(Tie);
                }
                else if (PlayerTwoChoice == "Paper")
                {
                    Console.WriteLine(TwoWin);
                }
                else if (PlayerTwoChoice == "Scissor")
                {
                    Console.WriteLine(OneWin);
                }             
            }
            if (PlayerOneChoice == "Paper")
            {
                if (PlayerTwoChoice == "Stone")
                {
                    Console.WriteLine(OneWin);
                }
                else if (PlayerTwoChoice == "Paper")
                {
                    Console.WriteLine(Tie);
                }
                else if (PlayerTwoChoice == "Scissor")
                {
                    Console.WriteLine(TwoWin);
                }
            }
            if (PlayerOneChoice == "Scissor")
            {
                if (PlayerTwoChoice == "Stone")
                {
                    Console.WriteLine(TwoWin);
                }
                else if (PlayerTwoChoice == "Paper")
                {
                    Console.WriteLine(OneWin);
                }
                else if (PlayerTwoChoice == "Scissor")
                {
                    Console.WriteLine(Tie);
                }
            }
        }
    }
}
