using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerProject.GameLogics;

namespace ServerProject.UnitTests
{
	[TestClass]
	public class SPSLogicsTests
	{
		[TestMethod]
		public void PlayGame_RockRock_ReturnsZero()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Rock;
			Weapon weaponPlayer2 = Weapon.Rock;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, 0);
		}

		[TestMethod]
		public void PlayGame_RockPaper_ReturnsOne()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Rock;
			Weapon weaponPlayer2 = Weapon.Paper;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void PlayGame_RockScissors_ReturnsMinusOne()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Rock;
			Weapon weaponPlayer2 = Weapon.Scissors;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, -1);
		}

		[TestMethod]
		public void PlayGame_PaperRock_ReturnsMinusOne()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Paper;
			Weapon weaponPlayer2 = Weapon.Rock;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, -1);
		}

		[TestMethod]
		public void PlayGame_PaperPaper_ReturnsZero()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Paper;
			Weapon weaponPlayer2 = Weapon.Paper;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, 0);
		}

		[TestMethod]
		public void PlayGame_PaperScissors_ReturnsOne()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Paper;
			Weapon weaponPlayer2 = Weapon.Scissors;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void PlayGame_ScissorsRock_ReturnsOne()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Scissors;
			Weapon weaponPlayer2 = Weapon.Rock;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void PlayGame_ScissorsPaper_ReturnsMinusOne()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Scissors;
			Weapon weaponPlayer2 = Weapon.Paper;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, -1);
		}

		[TestMethod]
		public void PlayGame_ScissorsScissors_ReturnsZero()
		{
			SPSLogics gamelocics = new SPSLogics();
			Weapon weaponPlayer1 = Weapon.Scissors;
			Weapon weaponPlayer2 = Weapon.Scissors;

			int result = gamelocics.PlayGame(weaponPlayer1, weaponPlayer2);

			Assert.AreEqual(result, 0);
		}
	}
}
