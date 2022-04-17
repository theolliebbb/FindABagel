using System;
using System.Collections.Generic;
using Food.Models;
using SQLite;
using Xamarin.Forms;
using Food.Services;
using System.IO;
using Xamarin.Essentials;
using MvvmHelpers;
using Food.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.CommunityToolkit.Extensions;

namespace Food.Views
{
    public partial class Saved : ContentPage
    {
        static SQLiteConnection db;
        public List<LocalResult2> Restaurants { get; set; }
        public ObservableRangeCollection<LocalResult2> Restaurant { get; set; }
        public Saved()
        {
            InitializeComponent();
           
            activity.IsEnabled = false;
            activity.IsVisible = false;
            activity.IsRunning = false;
            Init();
        }
        public async void Init()
        {

            await Services.FoodService.GetFood();
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Restaurants.db");

            db = new SQLiteConnection(databasePath);

        }
        protected override void OnAppearing()
        {
            if (BindingContext is FoodViewModel vm)
            {
                vm.Refresh();
            }
        }

        async void Button_Clicked(System.Object sender, EventArgs e)
        {
            bagelspin.IsVisible = true;
            bagelspin.RotateTo(360, 10000);
            bagelspin.Rotation = 0;
            var buttonClickHandler = (Button)sender;
            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;

            Button nameLabel = (Button)ParentStackLayout.Children[5];
            nameLabel.IsEnabled = false;
            var selected = sender;
            try
            {
                PhoneDialer.Open(nameLabel.Text);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
            bagelspin.IsVisible = false;
            
            nameLabel.IsEnabled = true;
        }

        private async void Button_Clicked2(System.Object sender, EventArgs e)
        {
            bagelspin.IsVisible = true;
            bagelspin.RotateTo(360, 10000);
            bagelspin.Rotation = 0;

            var buttonClickHandler = (Button)sender;
            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;

            Button nameLabel = (Button)ParentStackLayout.Children[1];
            nameLabel.IsEnabled = false;
            Button addressLabe = (Button)ParentStackLayout.Children[6];
            var string1 = nameLabel.Text;
            var string2 = string.Concat(addressLabe.Text.Where(c => !char.IsWhiteSpace(c)));
            var url1 = "google.com/maps/search/";
            var url2 = "/data=!3m1!4b1!4m5!3m4!1s0x60188cdad4996a37:0x6b56646621dba43d!8m2!3d35.6886064!4d139.7035134";

            /*var placemark = new Placemark
            {

                Thoroughfare = $"{nameLabel.Text} 日本"

            };
            var options = new MapLaunchOptions { Name = nameLabel.Text };

            try
            {
                await Xamarin.Essentials.Map.OpenAsync(placemark, options);
            }
            catch (Exception ex)
            {
                // No map application available to open or placemark can not be located
            }*/
            var results = db.Table<LocalResult2>().Where(v => v.title == nameLabel.Text).ToList();
            

            await Navigation.PushAsync(new BagelMap(results.First<LocalResult2>()));
            bagelspin.IsVisible = false;
            nameLabel.IsEnabled = true;
        }
        async void Navigate(Object sender, EventArgs e)
        {
            var buttonClickHandler = (Button)sender;
            buttonClickHandler.IsEnabled = false;
            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;
            Button nameLabel = (Button)ParentStackLayout.Children[1];
            var place = db.Table<LocalResult2>().Where(v => v.title == nameLabel.Text).ToList();
            var thisplace = place.First<LocalResult2>();
            var location = new Location(thisplace.latitude, thisplace.longitude);
            var options = new MapLaunchOptions { Name = nameLabel.Text, NavigationMode = NavigationMode.Transit };

            try
            {
                await Xamarin.Essentials.Map.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                SnackBarOptions option = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"Unable to Find Map Application!!",
                        Padding = 15,
                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                Application.Current.MainPage.DisplaySnackBarAsync(option);
                buttonClickHandler.IsEnabled = true;
            }
        }
        private async void Button_Clicked3(System.Object sender, EventArgs e)
        {
            bagelspin.IsVisible = true;
            bagelspin.RotateTo(360, 10000);
            bagelspin.Rotation = 0;
            var buttonClickHandler = (Button)sender;
            buttonClickHandler.IsEnabled = false;
            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;

            Button nameLabel = (Button)ParentStackLayout.Children[6];
            nameLabel.IsEnabled = false;
            var button = (Button)sender;
            var url = nameLabel.Text;

            await Browser.OpenAsync(url);
            bagelspin.IsVisible = false;
            nameLabel.IsEnabled = true;
            buttonClickHandler.IsEnabled = true;
        }

       async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            bagelspin.IsVisible = true;
            bagelspin.RotateTo(360, 10000);
            bagelspin.Rotation = 0;
            db = null;
            if (db != null)
                return;
           

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Restaurants.db");

            db = new SQLiteConnection(databasePath);
            var buttonClickHandler = (Button)sender;
            buttonClickHandler.IsEnabled = false;
            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;
           
            Button nameLabel = (Button)ParentStackLayout.Children[1];
            
            SnackBarOptions options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.Black,
                    Message = $"{nameLabel.Text} removed.",
                    Padding = 15,



                },
                BackgroundColor = Color.White,
                Duration = TimeSpan.FromSeconds(2),
                CornerRadius = 15,
                IsRtl = true,


            };
            var results = db.Table<LocalResult2>().Where(v => v.title == nameLabel.Text).ToList();
            await Application.Current.MainPage.DisplaySnackBarAsync(options);
           db.Delete(results.First<LocalResult2>());

            bagelspin.IsVisible = false;
            buttonClickHandler.IsEnabled = true;
        }
    }
}
