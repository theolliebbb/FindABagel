using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Food.Models;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace Food.Views
{
    public partial class HomePage : ContentPage
    {
        public List<FoodPicker> pList = new List<FoodPicker>();
        public FoodPicker bagel = new FoodPicker();
        public FoodPicker mex = new FoodPicker();
        public FoodPicker beer = new FoodPicker();
        public LocationPicker tok = new LocationPicker();
        public LocationPicker sai = new LocationPicker();
        public LocationPicker osa = new LocationPicker();
        public List<LocationPicker> plist2 = new List<LocationPicker>();
        bool isTaskRunning;
        ActivityIndicator activityIndicator1 = new ActivityIndicator
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Fill
        };
        public HomePage()
        {
            InitializeComponent();
            gomap.IsEnabled = true;
            godb.IsEnabled = true;
            activity.IsEnabled = false;
            activity.IsVisible = false;
            activity.IsRunning = false;
            bagel.Name = "ベーグル";
            pList.Add(bagel);
            mex.Name = "メキシカン料理";
            pList.Add(mex);
            beer.Name = "クラフトビール";
            pList.Add(beer);
            foodpic.ItemsSource = pList;
            tok.Name = "東京";
            plist2.Add(tok);
            sai.Name = "埼玉";
            plist2.Add(sai);
            osa.Name = "大阪";
            plist2.Add(osa);
            locpic.ItemsSource = plist2;
            locpic.SelectedIndex = 0;
            foodpic.SelectedIndex = 0;
        }
        async void Button_Clicked(Object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var resultJson3 = await httpClient.GetStringAsync("https://serpapi.com/search.json?engine=google_maps&q=ベーグル東京&ll=%4040.7455096%2C-74.0083012%2C15.1z&type=search&api_key=7680d1b19169c265147e6bdda14de8a9c82e6ac3a8e6fa6434e72a2d59abfba1");
            var resultBig = JsonConvert.DeserializeObject<Models.Root>(resultJson3);
            var details = resultBig;
            await Navigation.PushAsync(new BagelMap(details));
        }
        async void Button_Clicked3(Object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var resultJson3 = await httpClient.GetStringAsync("https://serpapi.com/search.json?engine=google_maps&q=メキシカン東京&ll=%4040.7455096%2C-74.0083012%2C15.1z&type=search&api_key=7680d1b19169c265147e6bdda14de8a9c82e6ac3a8e6fa6434e72a2d59abfba1");
            var resultBig = JsonConvert.DeserializeObject<Models.Root>(resultJson3);
            var details = resultBig;
            await Navigation.PushAsync(new BagelMap(details));
        }
        async void Button_Clicked4(Object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var resultJson3 = await httpClient.GetStringAsync("https://serpapi.com/search.json?engine=google_maps&q=クラフトビール東京&ll=%4040.7455096%2C-74.0083012%2C15.1z&type=search&api_key=7680d1b19169c265147e6bdda14de8a9c82e6ac3a8e6fa6434e72a2d59abfba1");
            var resultBig = JsonConvert.DeserializeObject<Models.Root>(resultJson3);
            var details = resultBig;
            await Navigation.PushAsync(new BagelMap(details));
        }
        async void Button_Clicked5(Object sender, EventArgs e)
        {
            gomap.IsEnabled = false;
            godb.IsEnabled = false;
            bagelspin.IsVisible = true;
            bagelspin.RotateTo(360, 10000);
            bagelspin.Rotation = 0;
            
            var foodsel = foodpic.SelectedItem as FoodPicker;
            var string1 = foodsel.Name;
            var locsel = locpic.SelectedItem as LocationPicker;
            var string2 = locsel.Name;
            Debug.WriteLine(string1);
            var httpClient = new HttpClient();
            var url1 = "https://serpapi.com/search.json?engine=google_maps&q=";
            var url2 = "&ll=%4040.7455096%2C-74.0083012%2C15.1z&type=search&api_key=7680d1b19169c265147e6bdda14de8a9c82e6ac3a8e6fa6434e72a2d59abfba1";
            string resultJson3;
            Root resultBig = new Root();
            try
            {
                resultJson3 = await httpClient.GetStringAsync($"{url1}{string1}{string2}{url2}");
                resultBig = JsonConvert.DeserializeObject<Models.Root>(resultJson3);
                var details = resultBig;
                await Navigation.PushAsync(new BagelMap(details));
                bagelspin.IsVisible = false;
                gomap.IsEnabled = true;
                godb.IsEnabled = true;
            }
            catch
            {
                SnackBarOptions options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"Unable to Connect!!",
                        Padding = 15,
                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                await Application.Current.MainPage.DisplaySnackBarAsync(options);
                bagelspin.IsVisible = false;
                gomap.IsEnabled = true;
                godb.IsEnabled = true;
            }
            
        }
        async void Button_Clicked2(Object sender, EventArgs e)
        {
            gomap.IsEnabled = false;
            godb.IsEnabled = false;
            bagelspin.IsVisible = true;
            bagelspin.RotateTo(360, 10000);
            bagelspin.Rotation = 0;
           
            await Navigation.PushAsync(new Saved());
            bagelspin.IsVisible = false;
            gomap.IsEnabled = true;
            godb.IsEnabled = true;
        }
    }
}
