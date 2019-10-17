using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.Data
{
	public struct PlayerScore
	{
		public string Name { get; set; }
		public int Score { get; set; }

		public PlayerScore(string name, int score)
		{
			this.Name = name;
			this.Score = score;
		}

		public void AddWin()
		{
			this.Score++;
		}
	}
}
