using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StreamStats
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			var content = new DashboardPage();
			MainPage = new NavigationPage(content) { BarBackgroundColor = Color.FromHex("#4F037F"), BarTextColor = Color.White };
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

