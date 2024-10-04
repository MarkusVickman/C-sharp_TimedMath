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

                highScore.Text = "";



                StringBuilder highScoreString = new StringBuilder();

                highScoreString.Append($"HighScore\n");

                try
                {
                    //Alla rader i filen skrivs ut en efter en
                    foreach (var line in File.ReadAllLines(fileName))
                    {
                        //Check så att inga tomma rader tas med (har dock begränsat det i metoden för skriva nya inlägg)
                        if (line.Length > 0)
                        {
                            //Varje rad deserializeras och objekt görs i Guest konstruktorn lägg till felhantering
                            MainPage.Player player = JsonSerializer.Deserialize<Player>(line)!;


                            counter++;


                            //Index, namn och inlägget skrivs ut på skärmen


                            highScoreString.Append($"{counter}. {player.User}: {player.Score}\n");

                            //Räknar upp index

                        }
                    }
                } catch 
                {
                    File.Create(fileName).Dispose();
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

        public bool checkIfPressed = false;

        public async void OnStartClicked(object sender, EventArgs e)
        {

            Button skipBtn = (Button)FindByName("SkipBtn");

            Entry entry = (Entry)FindByName("entry");

            Label ansLabel = (Label)FindByName("question");

            Entry enterName = (Entry)FindByName("enterName");

            Button submitName = (Button)FindByName("EnterNameBtn");

            Switch withoutTimeLimit = (Switch)FindByName("checkTimeLimit");
                       
            VerticalStackLayout mathCoices = (VerticalStackLayout)FindByName("coices");

            if (!checkIfPressed)
            {

                mathCoices.IsVisible = false;

                skipBtn.IsVisible = true;

                entry.IsVisible = true;

                question.IsVisible = true;

                totalPoints = 0;

                StartBtn.Text = "Stopp";

                enterName.IsVisible = false;

                submitName.IsVisible = false;

            }

            if (!withoutTimeLimit.IsToggled)
            {
                if (!checkIfPressed)
                {
                    checkIfPressed = true;

                    ChangeLabel();

                    await Task.Delay(120000/*, token*/);

                    enterName.IsVisible = true;

                    submitName.IsVisible = true;

                    StartBtn.Text = "Start";

                    ansLabel.Text = "Total points: " + totalPoints;

                    skipBtn.IsVisible = false;

                    entry.IsVisible = false;

                    checkIfPressed = false;

                    mathCoices.IsVisible = true;

                }
                else
                {
                    checkIfPressed = false;

                    StartBtn.Text = "Start";

                    ansLabel.Text = "Total points: " + totalPoints;

                    skipBtn.IsVisible = false;

                    entry.IsVisible = false;

                    enterName.IsVisible = true;

                    submitName.IsVisible = true;

                    mathCoices.IsVisible = true;

                }
            }
            if (withoutTimeLimit.IsToggled)
            {
                if (!checkIfPressed)
                {
                    checkIfPressed = true;

                    ChangeLabel();
                }
                else
                {
                    checkIfPressed = false;

                    StartBtn.Text = "Start";

                    ansLabel.Text = "Total points: " + totalPoints;

                    skipBtn.IsVisible = false;

                    entry.IsVisible = false;

                    mathCoices.IsVisible = true;
                }

            }

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

            HorizontalStackLayout checkBoxes = (HorizontalStackLayout)FindByName("checkBoxes");


            if (checkBoxes.IsVisible == true)
            {
                checkBoxes.IsVisible = false;
            }
            else
            {
                checkBoxes.IsVisible = true;
            }

        }


        private void SubmitAnswerClicked(object sender, EventArgs e)
        {

            Entry userName = (Entry)FindByName("enterName");
            string user = userName.Text;


                MainPage.Player player = new Player(totalPoints, user);

            //Objektet Json serializeras med hjälp av inställningarna i början av metoden.
            string player2 = JsonSerializer.Serialize(player);

            string fileName = /*"highscore.json"*/@"c:\windows\Temp\highscore.json";
            //En rad skrivs in i filen där objektet nu har json-format och avslutas på en tom rad.
            File.AppendAllText(fileName, player2 + Environment.NewLine);

            Entry enterName = (Entry)FindByName("enterName");

            Button submitName = (Button)FindByName("EnterNameBtn");


            enterName.IsVisible = false;

            submitName.IsVisible = false;

            SortHighScore();

            //}

        }
        private void SortHighScore()
        {

            //variabel med namnet på filen för inlägg som sparas på samma plats som program.cs
            string fileName = /*"highscore.json"*/@"c:\windows\Temp\highscore.json";

            //Används för att skriva ut ett index till varje inlägg (kanske en for-loop hade varit bättre men det här fungerar bra.)

            Player[] players = {};

            foreach (var line in File.ReadAllLines(fileName))
            {
                //Check så att inga tomma rader tas med (har dock begränsat det i metoden för skriva nya inlägg)
                if (line.Length > 0)
                {

                    // Resize the array
                    Array.Resize(ref players, players.Length + 1);


                    //Varje rad deserializeras och objekt görs i Guest konstruktorn
                    MainPage.Player player = JsonSerializer.Deserialize<Player>(line)!;


                    // Add the new element
                    players[players.Length - 1] = player;

                }
            }

            Array.Sort(players, (x, y) => x.Score.CompareTo(y.Score));
            Array.Reverse(players);

            File.WriteAllText(fileName, string.Empty);

            int arrLength;

            if(players.Length <5)
            {
                arrLength = players.Length;
            }
            else
            {
                arrLength = 5;
            }

            Player[] toSavePlayers = new Player[arrLength];
            Array.Copy(players, toSavePlayers, arrLength);
                       
            foreach (var playerObject in toSavePlayers)
            {
                string player = JsonSerializer.Serialize(playerObject);

                //En rad skrivs in i filen där objektet nu har json-format och avslutas på en tom rad.
                File.AppendAllText(fileName, player + Environment.NewLine);
            }

            LoadHighScore();

        }

        public string[] CheckMathMethod()
        {
            HorizontalStackLayout checkBoxes = (HorizontalStackLayout)FindByName("checkBoxes");

            string[] method = {};

                CheckBox plus = (CheckBox)FindByName("plusCheckbox");
                CheckBox minus = (CheckBox)FindByName("minusCheckbox");
                CheckBox divided = (CheckBox)FindByName("dividedCheckbox");
                CheckBox multiply = (CheckBox)FindByName("multiplyCheckbox");

                if (minus.IsChecked == true)
                {
                    Array.Resize(ref method, method.Length + 1);

                    method[method.Length - 1] = "-";
                }
                if (divided.IsChecked == true)
                {
                    Array.Resize(ref method, method.Length + 1);

                    method[method.Length - 1] = "/";
                }
                if (multiply.IsChecked == true)
                {
                    Array.Resize(ref method, method.Length + 1);

                    method[method.Length - 1] = "*";
                }
                else if(plus.IsChecked || (!minus.IsChecked && !divided.IsChecked && !multiply.IsChecked))
                {
                    Array.Resize(ref method, method.Length + 1);

                    method[method.Length - 1] = "+";
                }

                return method;            
        }

        public void ChangeLabel()
        {

            var random = new Random();

            Switch switchMultiTable = (Switch)FindByName("switchMultiTable");

            if (!switchMultiTable.IsToggled)
            {
                string[] method = CheckMathMethod();

                int maxRandomNumb = 7;

                if (totalPoints > 15 && totalPoints < 20)
                {
                    maxRandomNumb = 11;
                }
                else if (totalPoints > 20 && totalPoints < 25)
                {
                    maxRandomNumb = 16;
                }
                else if (totalPoints > 25 && totalPoints < 30)
                {
                    maxRandomNumb = 31;
                }
                else if (totalPoints > 30 && totalPoints < 40)
                {
                    maxRandomNumb = 51;
                }
                else if (totalPoints > 40)
                {
                    maxRandomNumb = 101;
                }

                int num1 = random.Next(maxRandomNumb);
                int num2 = random.Next(maxRandomNumb);

                
                int calcMethod = random.Next(method.Length);

                string calc = method[calcMethod];
                if (calc == "+")
                {
                    answer = (num1 + num2).ToString();
                }
                if (calc == "-")
                {
                    if(num1 < num2)
                    {
                        int tempNum = num1;
                        num1 = num2;
                        num2 = tempNum;
                    }
                    answer = (num1 - num2).ToString();
                }
                if (calc == "/")
                {
                    if (num2 == 0)
                    {
                        num2 = 1;
                    }

                    while ((num1 % num2) != 0)
                    {
                        num1 = random.Next(maxRandomNumb);
                        num2 = random.Next(maxRandomNumb);

                        if (num2 == 0)
                        {
                            num2 = 1;
                        }
                    }

                    answer = (num1 / num2).ToString();

                }
                if (calc == "*")
                {
                    num1 = random.Next(11);
                    num2 = random.Next(11);
                    answer = (num1 * num2).ToString();
                }


                string retVal = $"{num1} {calc} {num2} = ?";
                Label ansLabel = (Label)FindByName("question");
                ansLabel.Text = retVal;
            }
            else
            {
                int num1 = random.Next(11);
                int num2 = random.Next(11);

                answer = (num1 * num2).ToString();

                string retVal = $"{num1} * {num2} = ?";
                Label ansLabel = (Label)FindByName("question");
                ansLabel.Text = retVal;
            }

        }
    }

    }
