using System.Text;
using System.Text.Json;

//Application TimedMath is all located in namespace TimedMath
namespace TimedMath

{ //The application uses only one page. This partial class is also used by math.xaml.cs MainPage.xaml.cs startandstopp.xaml.cs
    public partial class MainPage : ContentPage
    {
        //Method for filepath
        private static string FileLocation()
        {
            string fileName = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "highScore.json");
            return fileName;
        }

        //Reads highscore from file
        private void LoadHighScore()
        {
            //If file does not exists a new one is created and closed
            if (!File.Exists(FileLocation()))
            {
                File.Create(FileLocation()).Dispose();
            }

            //reads the text from the document
            string jsonString = File.ReadAllText(FileLocation());

            //If file has writen text
            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                //Counter used to index high score data
                int counter = 0;

                Label highScore = (Label)FindByName("highScore");
                //Resets textfield
                highScore.Text = "";

                //stringbuilder string to add every line of high score data to
                StringBuilder highScoreString = new StringBuilder();

                //"Heading"
                highScoreString.Append($"HighScore\n");

                //Tries to read lines of json and convert it to player objects. 
                try
                {
                    //Alla rader i filen skrivs ut en efter en
                    foreach (var line in File.ReadAllLines(FileLocation()))
                    {
                        //Check så att inga tomma rader tas med (har dock begränsat det i metoden för skriva nya inlägg)
                        if (line.Length > 0)
                        {
                            //Varje rad deserializeras och objekt görs i Guest konstruktorn lägg till felhantering
                            MainPage.Player player = JsonSerializer.Deserialize<Player>(line)!;

                            //Index counter
                            counter++;

                            //Index, name och score adds to stringbuilder string.
                            highScoreString.Append($"{counter}. {player.User}: {player.Score}\n");
                        }
                    }
                }

                //Error has in my testing only occured if the file is corrupt or tampered with. Creates a new file if error
                catch
                {
                    File.Create(FileLocation()).Dispose();
                }
                //the high score string writes to screen
                highScore.Text = highScoreString.ToString();
            }
        }

        //Method to sort high scores 
        private void SortHighScore()
        {
            //empty Array of player objects
            Player[] players = { };

            //the loop runs through every line in the file
            foreach (var line in File.ReadAllLines(FileLocation()))
            {
                //only adds line in not empty
                if (line.Length > 0)
                {
                    // Resize the array
                    Array.Resize(ref players, players.Length + 1);

                    //every row deserialize and a player object is created in Player constructor
                    MainPage.Player player = JsonSerializer.Deserialize<Player>(line)!;

                    // Add the new object to the array of Player objects.
                    players[players.Length - 1] = player;
                }
            }

            //Sort the player object array based on score
            Array.Sort(players, (x, y) => x.Score.CompareTo(y.Score));
            Array.Reverse(players);

            //Emptys the file.
            File.WriteAllText(FileLocation(), string.Empty);

            //If else to set max high score legth. change the number if nessesary.
            int arrLength = 10;
            if (players.Length < 10)
            {
                arrLength = players.Length;
            }

            //A new Player array with the lenght of high scorer or max 10
            Player[] toSavePlayers = new Player[arrLength];
            Array.Copy(players, toSavePlayers, arrLength);

            //Every player object in the new array is Json serialized and written to file
            foreach (var playerObject in toSavePlayers)
            {
                string player = JsonSerializer.Serialize(playerObject);
                File.AppendAllText(FileLocation(), player + Environment.NewLine);
            }

            //A methot is initiated to write high score to screen 
            LoadHighScore();
        }

        //method writes high score to file when submit is clicked or enter is pressed when writing name.
        private void SubmitAnswerClicked(object sender, EventArgs e)
        {

            Entry userName = (Entry)FindByName("enterName");
            string user = userName.Text;

            //Needs atlease one character
            if (!string.IsNullOrWhiteSpace(user))
            {
                //Creates a new player object from total points and user input name. The player object is Json serialized and written to file
                MainPage.Player player = new Player(totalPoints, user);
                string player2 = JsonSerializer.Serialize(player);
                File.AppendAllText(FileLocation(), player2 + Environment.NewLine);

                //Hides submit button and text field
                Entry enterName = (Entry)FindByName("enterName");
                Button submitName = (Button)FindByName("EnterNameBtn");
                enterName.IsVisible = false;
                submitName.IsVisible = false;

                //Initiate method to sort high score
                SortHighScore();
            }
        }
    }
}
