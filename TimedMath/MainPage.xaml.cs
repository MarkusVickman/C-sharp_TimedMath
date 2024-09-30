using System;
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


        public class Player
        {
            public string User { get; set; }
            public int Score { get; set; }

            //Konstruktorn som skapar gästinläggs-objekt
            public Player(string user, int score)
            {
                User = user;
                Score = score;
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

        private void LoadHighScore()
        {

            //variabel med namnet på filen för inlägg som sparas på samma plats som program.cs
            string fileName = /*"highscore.json"*/@"c:\windows\Temp\highscore.json";
            //Om filen inte redan finns så skapas en ny
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            string jsonString = File.ReadAllText(fileName);

            //Om filen inte är tom skrivs alla inlägg ut på skärmen
            if (!string.IsNullOrWhiteSpace(jsonString))
            {

                //Används för att skriva ut ett index till varje inlägg (kanske en for-loop hade varit bättre men det här fungerar bra.)
                int counter = 0;


                Label highScore = (Label)FindByName("highScore");




                StringBuilder highScoreString = new StringBuilder();

                highScoreString.Append($"HighScore\n");

                //Alla rader i filen skrivs ut en efter en
                foreach (var line in File.ReadAllLines(fileName))
                {
                    //Check så att inga tomma rader tas med (har dock begränsat det i metoden för skriva nya inlägg)
                    if (line.Length > 0)
                    {
                        //Varje rad deserializeras och objekt görs i Guest konstruktorn
                        MainPage.Player player = JsonSerializer.Deserialize<Player>(line)!;


                        counter++;


                        //Index, namn och inlägget skrivs ut på skärmen


                        highScoreString.Append($"{counter}. {player.User}: {player.Score}\n");

                        //Räknar upp index

                    }
                }

                highScore.Text = highScoreString.ToString();


            }

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

            Button skipBtn = (Button)FindByName("SkipBtn");

            Entry entry = (Entry)FindByName("entry");

            Label ansLabel = (Label)FindByName("question");

            Entry enterName = (Entry)FindByName("enterName");

            Button submitName = (Button)FindByName("EnterNameBtn");
            if (!checkIfPressed)
            {

                skipBtn.IsVisible = true;

                entry.IsVisible = true;

                question.IsVisible = true;

                /*CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;
                */
                totalPoints = 0;
                CounterBtn.Text = "Stopp";
                checkIfPressed = true;
                ChangeLabel();
                await Task.Delay(120000/*, token*/);

            }

            // Player player1 = new Player("test", "guestPost");

            CounterBtn.Text = "Start";

            //Task.


            ansLabel.Text = "Total points: " + totalPoints;
            checkIfPressed = false;

            skipBtn.IsVisible = false;

            entry.IsVisible = false;

            enterName.IsVisible = true;

            submitName.IsVisible = true;



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

            Entry userName = (Entry)FindByName("enterName");
            string user = userName.Text;

            
            //Check för att se att inget av fälten är tomma. Om något är tomt skrivs ett felmeddelande ut och metoden körs om från början.
           /* if (guestName.Length == 0 || guestPost.Length == 0)
            {
                Console.WriteLine("\r\nBåde gästnamn och inlägg måste skrivas!");
                Guest.WritePost(fileName);
            }
            //annars skapas ett nytt object för inlägg med namn och meddelande med hjälp av Guest konstruktorn.
            else
            {*/
                MainPage.Player player = new Player(user, totalPoints);

            //Objektet Json serializeras med hjälp av inställningarna i början av metoden.
            string player2 = JsonSerializer.Serialize(player);

            string fileName = /*"highscore.json"*/@"c:\windows\Temp\highscore.json";
            //En rad skrivs in i filen där objektet nu har json-format och avslutas på en tom rad.
            File.AppendAllText(fileName, player2 + Environment.NewLine);

            //}

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
            int maxRandomNumb = 11;

            if (totalPoints > 5 && totalPoints < 10)
            {
                maxRandomNumb = 21;
            }
            else if (totalPoints > 10 && totalPoints < 20)
            {
                maxRandomNumb = 51;
            }
            else if (totalPoints > 20)
            {
                maxRandomNumb = 101;
            }
            var rand = new Random();
            int num1 = rand.Next(maxRandomNumb);
            int num2 = rand.Next(maxRandomNumb);



            answer = (num1 + num2).ToString();

            string retVal = $"{num1} + {num2} = ?";
            Label ansLabel = (Label)FindByName("question");
            ansLabel.Text = retVal;

            //bool check = false;

            //RightAnswer(answer, check);

        }
    }

    }
