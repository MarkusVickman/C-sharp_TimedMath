using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TimedMath
{
    public partial class MainPage : ContentPage
    {

        public static string FileLocation()
        {

            string fileName;

            if (AndroidDevice())
            {
                fileName = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "highScore.json");
            }
            else
            {
                fileName = @"c:\windows\Temp\highscore.json";
            }

            return fileName;

    }



        private void LoadHighScore()
        {

            //Om filen inte redan finns så skapas en ny
            if (!File.Exists(FileLocation()))
            {
                File.Create(FileLocation()).Dispose();
            }

            string jsonString = File.ReadAllText(FileLocation());

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
                    foreach (var line in File.ReadAllLines(FileLocation()))
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
                }
                catch
                {
                    File.Create(FileLocation()).Dispose();
                }

                highScore.Text = highScoreString.ToString();


            }

        }






        private void SortHighScore()
        {

            //Används för att skriva ut ett index till varje inlägg (kanske en for-loop hade varit bättre men det här fungerar bra.)

            Player[] players = { };

            foreach (var line in File.ReadAllLines(FileLocation()))
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

            File.WriteAllText(FileLocation(), string.Empty);

            int arrLength;

            if (players.Length < 10)
            {
                arrLength = players.Length;
            }
            else
            {
                arrLength = 10;
            }

            Player[] toSavePlayers = new Player[arrLength];
            Array.Copy(players, toSavePlayers, arrLength);

            foreach (var playerObject in toSavePlayers)
            {
                string player = JsonSerializer.Serialize(playerObject);

                //En rad skrivs in i filen där objektet nu har json-format och avslutas på en tom rad.
                File.AppendAllText(FileLocation(), player + Environment.NewLine);
            }

            LoadHighScore();

        }


        private void SubmitAnswerClicked(object sender, EventArgs e)
        {

            Entry userName = (Entry)FindByName("enterName");
            string user = userName.Text;


            MainPage.Player player = new Player(totalPoints, user);

            //Objektet Json serializeras med hjälp av inställningarna i början av metoden.
            string player2 = JsonSerializer.Serialize(player);

            //En rad skrivs in i filen där objektet nu har json-format och avslutas på en tom rad.
            File.AppendAllText(FileLocation(), player2 + Environment.NewLine);

            Entry enterName = (Entry)FindByName("enterName");

            Button submitName = (Button)FindByName("EnterNameBtn");


            enterName.IsVisible = false;

            submitName.IsVisible = false;

            SortHighScore();
        }

    }
}
