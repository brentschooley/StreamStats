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

		// If you want to test this yourself head over to Firebase and create a free account.
		// Your database structure should be streamers > [streamer_name] > viewer_count
		private const string BasePath = "https://[your_firebase_database_name].firebaseio.com/";
		private const string FirebaseSecret = "[your_firebase_secret]";

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

			await _client.OnAsync("streamers/[streamer_name_to_test]",
				added: (s, args, context) =>
				{
					//Console.WriteLine("added");	
				},
				changed: (s, args, context) =>
				{
					if(args.Path == "/viewer_count")
					{
						this.ViewerCount = int.Parse(args.Data);
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

		async void InitializeStats ()
		{
			var response = await _client.GetAsync ("streamers/brentschooley/viewer_count");
			this.ViewerCount = int.Parse (response.Body);
		}
	}
}

