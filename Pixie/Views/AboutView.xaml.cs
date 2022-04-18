// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis.GUI.Views
{
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Navigation;
	using Anamnesis.GUI.Dialogs;
	using Anamnesis.Services;

	public partial class AboutView : UserControl
	{
		public AboutView()
		{
			this.InitializeComponent();

			if (VersionInfo.Date.Year <= 2000)
			{
				this.VersionLabel.Text = "Developer";
			}
			else
			{
				this.VersionLabel.Text = VersionInfo.Date.ToString("yyyy-MM-dd HH:mm");
			}
		}

		private void OnNavigate(object sender, RequestNavigateEventArgs e)
		{
			UrlUtility.Open(e.Uri.AbsoluteUri);
		}

		private void OnLogsClicked(object sender, RoutedEventArgs e)
		{
			LogService.ShowLogs();
		}

		private void OnSetingsClicked(object sender, RoutedEventArgs e)
		{
			SettingsService.ShowDirectory();
		}
	}
}
