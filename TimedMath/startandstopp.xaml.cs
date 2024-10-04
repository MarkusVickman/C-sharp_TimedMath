using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimedMath
{

    public partial class MainPage : ContentPage
    {



        public bool checkIfPressed = false;
        public bool checkIfTimerStarted = false;


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

                    StartButtonTimer();

                    await Task.Delay(120000/*, token*/);

                    StartButtonTimer();

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

        public void StartButtonTimer()
        {
            if (checkIfTimerStarted)
            {

            }
            else
            {

            }

        /* EGEN FUNKTION??

        //Skriva ut tid.
        DateTime startTime = DateTime.Now;

        TimeSpan elapsedTime = (DateTime.Now - startTime);

        int secondsRemaining = (int)(120000 - elapsedTime.Seconds);

        StartBtn.Text = $"Stopp {secondsRemaining}";


                                Action tickAction = () => Console.WriteLine(DateTime.Now);
            int lastTick = DateTime.Now.TimeOfDay.Seconds;
            while (true)
            {
                var seconds = DateTime.Now.TimeOfDay.Seconds;
                if(lastTick != seconds)
                {
                    lastTick = seconds;
                    tickAction();
                }
            }


        ELLER DENNA?


        static System.Threading.Timer timer = new(CallBack, null, 0, 0);

        static void CallBack(object? state)
        {
        var now = DateTime.Now;
        timer.Change(1000 - now.Millisecond, 0);

        // Console.WriteLine(now + " " + now.Millisecond);
        }
         */

    }



    }

}
