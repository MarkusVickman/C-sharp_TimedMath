//Application TimedMath is all located in namespace TimedMath
namespace TimedMath

{ //The application uses only one page. This partial class is also used by math.xaml.cs MainPage.xaml.cs startandstopp.xaml.cs
    public partial class MainPage : ContentPage
    {
        //Bool variables to know if startbutton is pressed and if the timer for it har started
        private bool checkIfPressed = false;
        private CancellationTokenSource ?cts;

        //Method to start the math questions
        private async void OnStartClicked(object sender, EventArgs e)
        {
            //If start button is not pressed the math question starts
            if (!checkIfPressed)
            {
                //Shows and hides elements for what is needed in the "RUN" state
                coices.IsVisible = false;
                SkipBtn.IsVisible = true;
                entry.IsVisible = true;
                question.IsVisible = true;
                enterName.IsVisible = false;
                EnterNameBtn.IsVisible = false;

                //Resets total points
                totalPoints = 0;
                //Changes start button text to stop
                StartBtn.Text = "Stop";
            }

            //checks if the player has choosen to not have timer
            if (!checkTimeLimit.IsToggled)
            {            
                //If start button is not pressed the math question starts
                if (!checkIfPressed)
                {
                    // Create a CancellationTokenSource
                    cts = new CancellationTokenSource();
                    CancellationToken ct = cts.Token;

                    //now that the start button is press it checkes to true
                    checkIfPressed = true;

                    //initiate function to start math questoins and a method to write a ticking timer to the start button
                    ChangeLabel();
                    StartButtonTimer();

                    //the delay is asyncronus so it only stops this thread. Sets how long time the user have to solve math questions.! needs to be changed is StartButtonTimer() also! 
                    try
                    {
                        await Task.Delay(120000, ct);
                    }
                    //Try catch is used to be able to stop the delay if stop is pressed.
                    catch (TaskCanceledException)
                    {
                    }
                    StopRunningQuestions(true);
                }
                //If stop button is pressed the delay is canceled and a stopp method is starting.
                else
                {
                    cts?.Cancel();
                    StopRunningQuestions(true);
                }
            }

            //if the user choose to play without timer. if and else to start and stop the math questions.
            if (checkTimeLimit.IsToggled)
            {
                //If start is not already pressed, the math questions starts in the method ChangeLabel()
                if (!checkIfPressed)
                {
                    //now that the start button is press it checkes to true
                    checkIfPressed = true;
                    ChangeLabel();
                }              
                else
                {
                    StopRunningQuestions(false);
                }
            }
        }

        //Method to stop the math questions
        private void StopRunningQuestions(bool timedRun)
        {
            //reset text field for highscore
            highScore.Text = $"HighScore\n";
            LoadHighScore();

            //changes start button text and writes totals points to screen
            StartBtn.Text = "Start";
            question.Text = "Total points: " + totalPoints;

            //sets so the start button in not recognized as pressed
            checkIfPressed = false;

            //Shows and hides elements for what is needed in the "STOP" state                    
            SkipBtn.IsVisible = false;
            entry.IsVisible = false;
            coices.IsVisible = true;

            //if it was a timed run then the user gets shown the submit options
            if (timedRun)
            {
                EnterNameBtn.IsVisible = true;
                enterName.IsVisible = true;
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
                //one seconds delay. 
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
                StartBtn.Text = $"Stop - {secondsRemaining}";
            }
        }
        }
}
