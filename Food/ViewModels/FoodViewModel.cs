using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Food.Models;
using Food.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SQLite;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace Food.ViewModels
{
    public class FoodViewModel
    {
        public static string photofilename;
        public static string imagePath;
        public ObservableRangeCollection<LocalResult2> Food { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand PhotoCommand { get; }
        public AsyncCommand AddCommand { get; }
        static SQLiteConnection db;
        public AsyncCommand ClearCommand { get; }
        public AsyncCommand LogoutCommand { get; }
        public AsyncCommand<LocalResult2> EditCommand { get; }
        public AsyncCommand<LocalResult2> RemoveCommand { get; }
        
        public LocalResult SelectedFood { get; set; }
        public ICommand MapCommand { get; private set; }
        public bool IsRefreshing { get; private set; }

        public FoodViewModel()
        {



            Food = new ObservableRangeCollection<LocalResult2>();
            ClearCommand = new AsyncCommand(Clear);
            RemoveCommand = new AsyncCommand<LocalResult2>(Remove);
            RefreshCommand = new AsyncCommand(Refresh);
      async Task Clear()
        {
                SnackBarOptions options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"All items cleared.",
                        Padding = 15,



                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                await Application.Current.MainPage.DisplaySnackBarAsync(options);
                await FoodService.ClearFood();
            await Refresh();
        }
        async Task Remove(Models.LocalResult2 food)
        {
                SnackBarOptions options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Black,
                        Message = $"{food.title} removed.",
                        Padding = 15,



                    },
                    BackgroundColor = Color.White,
                    Duration = TimeSpan.FromSeconds(2),
                    CornerRadius = 15,
                    IsRtl = true,


                };
                await Application.Current.MainPage.DisplaySnackBarAsync(options);
                await FoodService.RemoveFood(food.Id);
            await Refresh();
        }
        }

        

       
        
        public async Task Refresh()
        {

            IsRefreshing = true;

            await Task.Delay(200);

            Food.Clear();

            var foods = await FoodService.GetFood();

            Food.AddRange(foods);

            IsRefreshing = false;

        }
        public async Task MiniRefresh()
        {





            Food.Clear();

            var foods = await FoodService.GetFood();

            Food.AddRange(foods);



        }
 

    }
}
