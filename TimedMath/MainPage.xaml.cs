/*Math application to help people get faster att easy math. I believe that repetition helps people to se patterna in math. 
  The application let the user choose calculation method minus, divided, plus and multipication. If the user calulate math with the timelimit enabled a final score will be provided and can be appied to a highscore that is stored localy on a file
  Based on the number of total points the numbers to calculate gets larger and larger. The user can skip without penalty
 CREATED BY MARKUS VICKMAN OCTOBER 2024*/

//Application TimedMath is all located in namespace TimedMath
namespace TimedMath
{
    //The application uses only one page. This partial class is also used by math.xaml.cs highscoretofile.xaml.cs startandstopp.xaml.cs
    public partial class MainPage : ContentPage
    {
        //sting that stores the right answer, the string is replaced with every new number to calculate 
        private string answer;
        //integer that gets +1 for every right answer
        private int totalPoints;

        //class for creating player objects. Used before storing or loading to file and when sorting highscore is done.
        public class Player
        {
            public int Score { get; set; }
            public string User { get; set; }

            //Constructor for player object. Includes player namn and player score
            public Player(int score, string user)
            {
                Score = score;
                User = user;
            }
        }
            
        //MainPage() runs when the applications starts
        public MainPage()
        {
            InitializeComponent();

            //Method starts to load high score
            this.LoadHighScore();
        }

        //Method to check if input is the right answer for the math question. initiates from xaml entry field
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((Entry)sender).Text;
            // If the answer matches the right answer and if the test is running (checkIfPressed) 
            if (text == answer && checkIfPressed)
            {
                //adds one point and initiate a method to change numbers to calculate then sets entryfield to an empty string
                totalPoints++;
                ChangeLabel();
                ((Entry)sender).Text = "";
            }
        }

        //When skip button is clicked a method to change numbers to calculate is initiated
        private void OnSkipClicked(object sender, EventArgs e)
        {
            if (checkIfPressed)
            {
                ChangeLabel();
            }                       
        }

        //The method is used to hide math coices when the switch for multiplication table is active
        private void MultiplicationTableActive(object sender, EventArgs e)
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
