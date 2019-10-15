using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.GameLogics
{
    public class SPSLogics
    {
		// -1: player1 wins
		// 0: tie
		// 1: player2 wins

        public int PlayGame(Weapon wPlayer1, Weapon wPlayer2)
        {
            if (wPlayer1 == Weapon.Rock)
            {
                if (wPlayer2 == Weapon.Rock)
                {
					return 0;
                }
                else if (wPlayer2 == Weapon.Paper)
                {
					return 1;
                }
                else if (wPlayer2 == Weapon.Scissors)
                {
					return -1;
                }             
            }
            else if (wPlayer1 == Weapon.Paper)
            {
                if (wPlayer2 == Weapon.Rock)
                {
					return -1;
                }
                else if (wPlayer2 == Weapon.Paper)
                {
					return 0;
                }
                else if (wPlayer2 == Weapon.Scissors)
                {
					return 1;
                }
            }
            else if (wPlayer1 == Weapon.Scissors)
            {
                if (wPlayer2 == Weapon.Rock)
                {
					return 1;
                }
                else if (wPlayer2 == Weapon.Paper)
                {
					return -1;
                }
                else if (wPlayer2 == Weapon.Scissors)
                {
					return 0;
                }
            }

			return 0;
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
