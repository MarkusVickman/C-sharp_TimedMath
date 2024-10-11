//Application TimedMath is all located in namespace TimedMath
namespace TimedMath

{ //The application uses only one page. This partial class is also used by math.xaml.cs MainPage.xaml.cs startandstopp.xaml.cs
    public partial class MainPage : ContentPage
    {
        //Bool variables to know if startbutton is pressed and if the timer for it har started
        private bool checkIfPressed = false;
        private bool checkIfTimerStarted = false;

        //Method to start the math questions
        private async void OnStartClicked(object sender, EventArgs e)
        {
            //Declares all XAML elements used in this method
            Button skipBtn = (Button)FindByName("SkipBtn");
            Entry entry = (Entry)FindByName("entry");
            Label ansLabel = (Label)FindByName("question");
            Entry enterName = (Entry)FindByName("enterName");
            Button submitName = (Button)FindByName("EnterNameBtn");
            Switch withoutTimeLimit = (Switch)FindByName("checkTimeLimit");
            VerticalStackLayout mathCoices = (VerticalStackLayout)FindByName("coices");

            //If start button is not pressed the math question starts
            if (!checkIfPressed)
            {
                //Shows and hides elements for what is needed in the "RUN" state
                mathCoices.IsVisible = false;
                skipBtn.IsVisible = true;
                entry.IsVisible = true;
                question.IsVisible = true;
                enterName.IsVisible = false;
                submitName.IsVisible = false;

                //Resets total points
                totalPoints = 0;
                //Changes start button text to stop
                StartBtn.Text = "Stop";
            }

            //checks if the player has choosen to not have timer
            if (!withoutTimeLimit.IsToggled)
            {
                // Create a CancellationTokenSource
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken ct = cts.Token;

                //If start button is not pressed the math question starts
                if (!checkIfPressed)
                {
                    //now that the start button is press it checkes to true
                    checkIfPressed = true;

                    //initiate function to start math questoins and a method to write a ticking timer to the start button
                    ChangeLabel();
                    StartButtonTimer();

                    //Sets how long time the user have to solve math questions.! needs to be changed is StartButtonTimer() also! 
                    await Task.Delay(120000, ct);
                }

                //changes start button text and writes totals points to screen
                StartBtn.Text = "Start";
                ansLabel.Text = "Total points: " + totalPoints;

                //sets so the start button in not recognized as pressed
                checkIfPressed = false;

                //Shows and hides elements for what is needed in the "STOP" state
                enterName.IsVisible = true;
                submitName.IsVisible = true;
                skipBtn.IsVisible = false;
                entry.IsVisible = false;
                mathCoices.IsVisible = true;

                //Cancels the delay timer so the app wont bug if stopped and restarted before 120 seconds.
                cts.Cancel();
            }

            //if the user choose to play without timer
            if (withoutTimeLimit.IsToggled)
            {
                //If start is now already pressed, the math questions starts in the method ChangeLabel()
                if (!checkIfPressed)
                {
                    //now that the start button is press it checkes to true
                    checkIfPressed = true;
                    ChangeLabel();
                }
                else
                {
                    //sets so the start button in not recognized as pressed
                    checkIfPressed = false;

                    //changes start button text and writes totals points to screen
                    StartBtn.Text = "Start";
                    ansLabel.Text = "Total points: " + totalPoints;

                    //Shows and hides elements for what is needed in the "STOP" state
                    skipBtn.IsVisible = false;
                    entry.IsVisible = false;
                    mathCoices.IsVisible = true;
                }
            }
        }

        //method to write time left to start button
        private async void StartButtonTimer()
        {
            //First sets the start time when the questions started
            DateTime startTime = DateTime.Now;
                                   
            //Only runs as long as the startbutton is mark as true.
            while (checkIfPressed)
            {
                //a seconds delay. 
                await Task.Delay(1000);

                //stops if the math questions has stoped
                if (!checkIfPressed)
                {
                    break;
                }

                //Calculate the time elapsed based on current time and starting time
                TimeSpan elapsedTime = (DateTime.Now - startTime);

                //Calculates the time remaining based on 120 seconds minus elaped time. 120 needs to be changed if another value for the main delay is changed in OnStartClicked()
                int secondsRemaining = (int)(120 - elapsedTime.TotalSeconds);

                //Writes text and seconds remaining to start button.
                Button startBtn = (Button)FindByName("StartBtn");
                startBtn.Text = $"Stop - {secondsRemaining}";
            }
        }
        }
}
