using System.Xml.Serialization;

namespace NewsAppMauiBlazor;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}
}
