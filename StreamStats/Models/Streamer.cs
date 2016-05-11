using System;

namespace StreamStats
{
	public class Streamer
	{
		public int ViewerCount {
			get;
			set;
		}

		public int Followers {
			get;
			set;
		}

		public string Duration { get; set; }

		public int NewFollowers {
			get;
			set;
		}

		public string TotalDonations {
			get;
			set;
		}

		public Streamer ()
		{
		}
	}
}

