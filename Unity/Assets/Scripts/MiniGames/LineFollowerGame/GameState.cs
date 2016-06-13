using System;

namespace LineFollower
{
	public class GameState
	{
		public bool isStarted;
		public bool isEnded;

		public GameState ()
		{
			
		}

		public bool IsWin()
		{
			return isStarted && isEnded;
		}

		public void Reset()
		{
			isStarted = false;
			isEnded = false;
		}
	}
}

