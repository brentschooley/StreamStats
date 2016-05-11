using System;
using System.ComponentModel;
using Xamarin.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp;
using FireSharp.Interfaces;

namespace StreamStats
{
	public class DashboardViewModel : INotifyPropertyChanged
	{
		int _viewerCount = 0;
		int _follower = 0;
		int _newFollowers = 0;
		string _duration = "";
		string _totalDonations = "";

		// If you want to test this yourself head over to Firebase and create a free account.
		// Your database structure should be 
		//
		// streamers 
		//       > [streamer_name] 
		//				> viewercount
		//				> duration
		//				> followers
		//				> newfollowers
		//				> totaldonations
		private const string BasePath = "https://[your_firebase_database_name].firebaseio.com/";
		private const string FirebaseSecret = "[your_firebase_url]";

		private FirebaseClient _client;

		public DashboardViewModel()
		{
		}

		public async void StartUpdates()
		{
			IFirebaseConfig config = new FirebaseConfig
			{
				AuthSecret = FirebaseSecret,
				BasePath = BasePath
			};

			_client = new FirebaseClient(config);

			InitializeStats ();

			await _client.OnAsync("streamers/[your_streamer_name]",
				added: (s, args, context) =>
				{
					//Console.WriteLine("added");	
				},
				changed: (s, args, context) =>
				{
					if(args.Path == "/viewercount")
					{
						this.ViewerCount = int.Parse(args.Data);
					}
					else if (args.Path == "/followers") 
					{
						this.Followers = int.Parse(args.Data);
					}
					else if (args.Path == "/duration")
					{
						this.Duration = args.Data;
					}
					else if (args.Path == "/newfollowers")
					{
						this.NewFollowers = int.Parse(args.Data);
					}
					else if (args.Path == "/totaldonations")
					{
						this.TotalDonations = args.Data;
					}
				},
				removed: (s, args, context) =>
				{
					//Console.WriteLine("removed");	
				});
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public int ViewerCount
		{
			get
			{
				return _viewerCount;
			}

			set
			{
				if (_viewerCount != value)
				{
					_viewerCount = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("ViewerCount"));
					}
				}
			}
		}

		public int Followers
		{
			get
			{
				return _follower;
			}

			set
			{
				if (_follower != value)
				{
					_follower = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Followers"));
					}
				}
			}
		}

		public int NewFollowers
		{
			get
			{
				return _newFollowers;
			}

			set
			{
				if (_newFollowers != value)
				{
					_newFollowers = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("NewFollowers"));
					}
				}
			}
		}

		public string Duration
		{
			get
			{
				return _duration;
			}

			set
			{
				if (_duration != value)
				{
					_duration = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
					}
				}
			}
		}

		public string TotalDonations
		{
			get
			{
				return _totalDonations;
			}

			set
			{
				if (_totalDonations != value)
				{
					_totalDonations = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("TotalDonations"));
					}
				}
			}
		}

		async void InitializeStats ()
		{
			var response = await _client.GetAsync ("streamers/[your_streamer_name]");
			var streamer = response.ResultAs<Streamer> ();
			this.ViewerCount = streamer.ViewerCount;
			this.Followers = streamer.Followers;
			this.Duration = streamer.Duration;
			this.TotalDonations = streamer.TotalDonations;
			this.NewFollowers = streamer.NewFollowers;
		}
	}
}

