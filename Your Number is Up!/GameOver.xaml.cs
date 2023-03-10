using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Your_Number_is_Up_
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameOver : ContentPage
	{
		public GameOver ()
		{
			InitializeComponent();
		}
        async void Redo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}