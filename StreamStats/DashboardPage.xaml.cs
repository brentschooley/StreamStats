using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;



namespace StreamStats
{
	public partial class DashboardPage : ContentPage
	{
		DashboardViewModel _viewModel;

		public DashboardPage()
		{
			InitializeComponent();
			_viewModel = new DashboardViewModel ();
			_viewModel.StartUpdates ();
			this.BindingContext = _viewModel;
		}
	}
}

