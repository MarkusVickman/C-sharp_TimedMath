using System;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Storage;

//using Windows.ApplicationModel.Calls;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using static Android.Provider.Contacts;

namespace TimedMath
{
    public partial class MainPage : ContentPage
    {

        public static bool AndroidDevice()
        {
            return DeviceInfo.Current.Platform == DevicePlatform.Android;
        }

        public string answer;
        public int totalPoints;

        //private static Timer timer;


        int count = 0;

        public class Player
        {
            public int Score { get; set; }
            public string User { get; set; }

            //Konstruktorn som skapar gästinläggs-objekt
            public Player(int score, string user)
            {
                Score = score;
                User = user;
            }
        }

            public MainPage()
        {
            InitializeComponent();

            Entry entry = new Entry { Placeholder = "Enter text" };
            entry.TextChanged += OnEntryTextChanged!;
            entry.Completed += OnEntryCompleted!;

            //Metoden för programmets val körs
            this.LoadHighScore();
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((Entry)sender).Text;
            // Handle the text changed event here
            if (text == answer && checkIfPressed)
            {
                totalPoints++;
                ChangeLabel();
                ((Entry)sender).Text = "";
            }
        }

        
        private void OnEntryCompleted(object sender, EventArgs e)
        {

            // Handle the completed event here
        }


        public void OnSkipClicked(object sender, EventArgs e)
        {
            if (checkIfPressed)
            {
                ChangeLabel();
            }                       
        }


        public void MultiplicationTableActive(object sender, EventArgs e)
        {

            VerticalStackLayout checkBoxes = (VerticalStackLayout)FindByName("checkBoxes");


            if (checkBoxes.IsVisible == true)
            {
                checkBoxes.IsVisible = false;
            }
            else
            {
                checkBoxes.IsVisible = true;
            }
        }
    }
    }
