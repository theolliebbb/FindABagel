using System;
using System.Collections.Generic;
using Food.Models;
using HtmlAgilityPack;
using Xamarin.Forms;
using GoogleApi;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.IO;
using SQLite;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.CommunityToolkit.Extensions;
using System.Threading.Tasks;

namespace Food.Views
{
    public partial class BagelMap : ContentPage
    {
        string img;
        string web;
        string name;
        LocalResult2 foodDetails = new LocalResult2();
        public Models.Root resultBig = new Models.Root();
        public double longi;
        public double lati;
        public List<Models.LocalResult> resultList = new List<Models.LocalResult>();
        public Models.LocalResult result = new Models.LocalResult();
        public List<BagelShops> Shops = new List<BagelShops>();
        static SQLiteConnection db;
        public List<Pin> pins;
        public BagelMap(Models.Root details)
        {
            InitializeComponent();
            try
            {
                foreach (var results in details.local_results)
                {
                    resultList.Add(results);
                }
                List<Pin> pinpin = new List<Pin>();
                foreach (Models.LocalResult place in resultList)
                {
                    var bagelPin = new Pin
                    {
                        Position = new Position(place.gps_coordinates.latitude, place.gps_coordinates.longitude),
                        Label = place.title,
                        Address = place.address,
                        Type = PinType.Place
                    };
                    bagelPin.MarkerClicked += async (s, args) =>
                    {
                        args.HideInfoWindow = true;
                        string pinName = ((Pin)s).Label;

                        title.Text = place.title;
                        address.Text = place.address;
                        rating.Text = place.rating.ToString();
                        price.Text = place.price;
                        hours.Text = place.hours;
                        phone.Text = place.phone;
                        description.Text = place.description;
                        image.Source = place.thumbnail;
                        save.IsVisible = true;
                        nav.IsVisible = true;
                        img = place.thumbnail;
                        address.FontFamily = null;
                        web = place.website;
                        longi = place.gps_coordinates.longitude;
                        lati = place.gps_coordinates.latitude;
                        name = place.title;
                        
                        SnackBarOptions options = new SnackBarOptions
                        {
                            MessageOptions = new MessageOptions
                            {
                                Foreground = Color.Black,
                                Message = $"{place.title} has been selected.",
                                Padding = 15,
                            },
                            BackgroundColor = Color.White,
                            Duration = TimeSpan.FromSeconds(2),
                            CornerRadius = 15,
                            IsRtl = true,
                        };
                       await Application.Current.MainPage.DisplaySnackBarAsync(options);
                    };
                    pinpin.Add(bagelPin);
                        map.Pins.Add(bagelPin);
                    };
                var position = new Position(26.3344, 127.8056);
                map.MoveToRegion(new MapSpan(position, 43.2203, 142.8635));

            }
            catch
            {
                SnackBarOptions options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"No Results!!",
                        Padding = 15,
                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                Application.Current.MainPage.DisplaySnackBarAsync(options);
            }
        }
        async void Navigate(Object sender, EventArgs e)
        {
            var location = new Location(lati, longi);
            var options = new MapLaunchOptions { Name = name, NavigationMode = NavigationMode.Transit };

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
            }
        }
        public BagelMap(Models.LocalResult2 place)
        {
            InitializeComponent();
            try
            {
                
                List<Pin> pinpin = new List<Pin>();
                
                    var bagelPin = new Pin
                    {
                        Position = new Position(place.latitude, place.longitude),
                        Label = place.title,
                        Address = place.address,
                        Type = PinType.Place

                    };
                    bagelPin.MarkerClicked += async (s, args) =>
                    {
                        args.HideInfoWindow = true;
                        string pinName = ((Pin)s).Label;

                        title.Text = place.title;
                        address.Text = place.address;
                        rating.Text = place.rating.ToString();
                        price.Text = place.price;
                        hours.Text = place.hours;
                        if (place.phone != null);
                        {
                            phone.Text = place.phone;
                        }
                        description.Text = place.description;
                        image.Source = place.thumbnail;
                        save.IsVisible = true;
                        nav.IsVisible = true;
                        img = place.thumbnail;
                        address.FontFamily = null;
                        web = place.website;
                        longi = place.longitude;
                        lati = place.latitude;
                        name = place.title;
                       
                        

                        SnackBarOptions options = new SnackBarOptions
                        {
                            MessageOptions = new MessageOptions
                            {
                                Foreground = Color.Black,
                                Message = $"{place.title} has been selected.",
                                Padding = 15,



                            },
                            BackgroundColor = Color.White,
                            Duration = TimeSpan.FromSeconds(2),
                            CornerRadius = 15,
                            IsRtl = true,


                        };
                        await Application.Current.MainPage.DisplaySnackBarAsync(options);
                    };
                    pinpin.Add(bagelPin);
                    map.Pins.Add(bagelPin);


                
                var position = new Position(26.3344, 127.8056);
                map.MoveToRegion(new MapSpan(position, 43.2203, 142.8635));


            }
            catch
            {
                SnackBarOptions options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"No Results!!",
                        Padding = 15,
                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                Application.Current.MainPage.DisplaySnackBarAsync(options);
            }
        }
        async void phone_Clicked(Object sender, EventArgs e)
            {
                try
                {
                    PhoneDialer.Open(phone.Text);
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
            }
            bool ValidateUser(string title)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Restaurants.db");

                db = new SQLiteConnection(databasePath);

                var results = db.Table<LocalResult2>().Where(v => v.title == title).ToList();

                return (results.Count > 0);
            }
            async void save_Clicked(Object sender, EventArgs e)
            {
            if (ValidateUser(title.Text) == false)
            {

                var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Restaurants.db");

                db = new SQLiteConnection(databasePath);



                foodDetails.title = title.Text;
                foodDetails.address = address.Text;
                foodDetails.hours = hours.Text;
                foodDetails.price = price.Text;
                foodDetails.thumbnail = img;
                foodDetails.phone = phone.Text;
                foodDetails.description = description.Text;
                foodDetails.longitude = longi;
                foodDetails.latitude = lati;
                foodDetails.website = web;

                db.Insert(foodDetails);
                SnackBarOptions options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"{title.Text} has been saved.",
                        Padding = 15,



                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                await Application.Current.MainPage.DisplaySnackBarAsync(options);

            }
            else
            {
                SnackBarOptions options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"{title.Text} has already been saved!",
                        Padding = 15,



                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                await Application.Current.MainPage.DisplaySnackBarAsync(options);
            }

        }





        }


    
}

