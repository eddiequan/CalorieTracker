using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using RestSharp;

namespace CalorieTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RestClient RestClient = new RestClient("http://api.nal.usda.gov/ndb");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FindButtonMouseUp(object sender, RoutedEventArgs e)
        {
            string apiKey = "";
            string query = GetFoodItemQuery();
            var request = new RestRequest($"search/?q={query}&max=4&api_key={apiKey}", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = RestClient.Execute(request);

            var json = response.Content;
            FoodItemQueryRootObject queryResponse = JsonConvert.DeserializeObject<FoodItemQueryRootObject>(json);
            List<FoodItem> queryResults = queryResponse.list.item;
            foreach (var result in queryResults)
            {
                //                FoodItemResults.Items.Add(result.name);
                FoodItemResults.Items.Add(AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        private string GetFoodItemQuery()
        {
            return FoodItemQuery.Text;
        }
    }

    public class ApplicationConfiguration
    {
        public string api_key { get; set; }
    }

    public class FoodItem {
        public string name { get; set; }
    }

    public class FoodItemQueryList {
        public string q { get; set; }
        public string group { get; set; }
        public string sort { get; set; }
        public List<FoodItem> item { get; set; }
    }

    public class FoodItemQueryRootObject {
        public FoodItemQueryList list { get; set; }
    }
}
