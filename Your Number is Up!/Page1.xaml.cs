using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Your_Number_is_Up;

namespace Your_Number_is_Up_
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Page1 : ContentPage
{
    public Page1()
    {
        InitializeComponent();
    }
        async void NavigateTo(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new MainPage());
        }

        
        async void Rules_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Rules());
        }
       
    }
}