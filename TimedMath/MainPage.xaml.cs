using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace TimedMath
{
    public partial class MainPage : ContentPage
    {


        private static Timer timer;


        int count = 0;

        public MainPage()
        {
            InitializeComponent();


        }

        private static System.Timers.Timer aTimer;
        public event System.Timers.ElapsedEventHandler Elapsed;
       
        private void OnCounterClicked(object sender, EventArgs e)
        {
            ChangeLabel();
            Task.Delay(5000).Wait();

            CounterBtn.Text = "Nu har det gått 5 sekunder";


        }

        public void ChangeLabel()
        {

            var rand = new Random();
            int num1 = rand.Next(51);
            int num2 = rand.Next(51);


            string retVal = $"{num1} + {num2} = {num1 + num2}";
            Label ansLabel = (Label)FindByName("question");
            ansLabel.Text = retVal;
        }

    }

}
