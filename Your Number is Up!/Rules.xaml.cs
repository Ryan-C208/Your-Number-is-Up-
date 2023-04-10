using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Your_Number_is_Up_;

namespace Your_Number_is_Up 
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Rules : ContentPage
{
	public Rules()
	{
		InitializeComponent();
	}
        
        async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }
        
    }
}
