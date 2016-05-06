using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace StreamStats
{
	public class DashboardViewModel : INotifyPropertyChanged
	{
		int _viewerCount = 7;

		public DashboardViewModel()
		{
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
	}
}

