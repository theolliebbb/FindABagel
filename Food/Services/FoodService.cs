using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Food.Models;
using Food.Views;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Food.Services
{
    public static class FoodService
    {
        
        public static SQLiteConnection db;
        static async Task Init()
        {
            db = null;
            if (db != null)
                return;


            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Restaurants.db");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<LocalResult2>();
        }
        public static async Task Initminus()
        {
            if (db != null)
                return;


            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Restaurants.db");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<LocalResult2>();
        }

        public static async Task AddFood(string Title, string Address, string Phone, string Price, double Rating, string Hours, int Position, string Thumbnail)
        {
            await Initminus();

            var food = new LocalResult2
            {
                title = Title,
                address = Address,
                phone = Phone,
                price = Price,
                rating = Rating,
                hours = Hours,
                position = Position,
                thumbnail = Thumbnail,
                




            };

            var position= db.Insert(food);
        }



        public static async Task ClearFood()
        {

            await Initminus();

            db.DeleteAll<LocalResult2>();
        }
        
        public static async Task RemoveFood(int id)
        {

            await Init();

            db.Delete<LocalResult2>(id);
        }


        public static async Task<IEnumerable<LocalResult2>> GetFood()
        {
            await Initminus();

            var food = db.Table<LocalResult2>().ToList();
            return food;
        }
        public static void Back()
        {
            
            App.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}