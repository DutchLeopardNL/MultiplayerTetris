using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.Data
{
	public struct PlayerScore
	{
		public string HostName { get; set; }
		public int Score { get; set; }

		public PlayerScore(string hostName, int score)
		{
			this.HostName = hostName;
			this.Score = score;
		}

		public void AddWin()
		{
			this.Score++;
		}
	}
}
