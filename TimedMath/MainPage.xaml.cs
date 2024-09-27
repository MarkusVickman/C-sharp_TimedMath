using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

//using Windows.ApplicationModel.Calls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TimedMath
{
    public partial class MainPage : ContentPage
    {

      /*  public class Player
        {

            public string User { get; set; }
            public string Post { get; set; }

            //Konstruktorn som skapar gästinläggs-objekt
            public Player(string user, string post)
            {
                User = user;
                Post = post;
            }
        }*/

        public string answer;
        public int totalPoints;

        //private static Timer timer;


        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            Entry entry = new Entry { Placeholder = "Enter text" };
            entry.TextChanged += OnEntryTextChanged!;
            entry.Completed += OnEntryCompleted!;

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

        //private static System.Timers.Timer aTimer;
        // public event System.Timers.ElapsedEventHandler Elapsed;

        public bool checkIfPressed = false;

        public async void OnCounterClicked(object sender, EventArgs e)
        {

            if (!checkIfPressed)
            {
                /*CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;
                */
                totalPoints = 0;
                CounterBtn.Text = "Stopp";
                checkIfPressed = true;
                ChangeLabel();
                await Task.Delay(30000/*, token*/);

            }

            // Player player1 = new Player("test", "guestPost");

            CounterBtn.Text = "Start";

            //Task.

            Label ansLabel = (Label)FindByName("question");
            ansLabel.Text = "Total points: " + totalPoints;
            checkIfPressed = false;
            }

        public void OnSkipClicked(object sender, EventArgs e)
        {
            if (checkIfPressed)
            {
                ChangeLabel();
            }

            // Player player1 = new Player("test", "guestPost");

        
        }


        private void SubmitAnswerClicked(object sender, EventArgs e)
        {

            Entry ansLabel = (Entry)FindByName("entry");
            string inputAnswer = ansLabel.Text;

            // if (inputAnswer == ull ) {


        }
        /*
        public string RightAnswer(string answer, bool check)
        {
            string rightAnswer;

            if (!check)
            {
              rightAnswer = answer; ;

            }

            
            return rightAnswer;
        }*/



        public void ChangeLabel()
        {

            var rand = new Random();
            int num1 = rand.Next(51);
            int num2 = rand.Next(51);



            answer = (num1 + num2).ToString();

            string retVal = $"{num1} + {num2} = ?";
            Label ansLabel = (Label)FindByName("question");
            ansLabel.Text = retVal;

            //bool check = false;

            //RightAnswer(answer, check);

        }
    }

    }
