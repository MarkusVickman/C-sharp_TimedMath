//Application TimedMath is all located in namespace TimedMath
namespace TimedMath
{
    //The application uses only one page. This partial class is also used by MainPage.xaml.cs highscoretofile.xaml.cs startandstopp.xaml.cs
    public partial class MainPage : ContentPage
    {

        //Method that returns a string array with math method choosen.
        private string[] CheckMathMethod()
        {
            //Declares variables for the boxes that needs to be "checked"
            VerticalStackLayout checkBoes = (VerticalStackLayout)FindByName("checkBoxes");
            CheckBox plus = (CheckBox)FindByName("plusCheckbox");
            CheckBox minus = (CheckBox)FindByName("minusCheckbox");
            CheckBox divided = (CheckBox)FindByName("dividedCheckbox");
            CheckBox multiply = (CheckBox)FindByName("multiplyCheckbox");

            //string array to store calculation methods
            string[] method = { };

            //Multiple if statments to check which boxes are checked, adds one to array length and adds them to an array
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
            //This one is special. if plus or no one is checked plus will be added. That is for the program to always have a calculation method to work with
            else if (plus.IsChecked || (!minus.IsChecked && !divided.IsChecked && !multiply.IsChecked))
            {
                Array.Resize(ref method, method.Length + 1);

                method[method.Length - 1] = "+";
            }
            return method;
        }

        //method to change number to solve
        private void ChangeLabel()
        {
            var random = new Random();

            Switch switchMultiTable = (Switch)FindByName("switchMultiTable");

            //if the swith to calculate multiplication table is not active
            if (!switchMultiTable.IsToggled)
            {
                //Reads string array with calculation methods choosen
                string[] method = CheckMathMethod();

                //The if and if else statments below is to increase the difficulty based on total points accumulated.
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

                //Initiad 2 random numbers for the calculation
                int num1 = random.Next(maxRandomNumb);
                int num2 = random.Next(maxRandomNumb);

                //when a new number is to be displayed on the screen a random calculation method is selected from the choosen methods 
                int calcMethod = random.Next(method.Length);
                string calc = method[calcMethod];

                //if statments to calculate the numbers based on calculation method.
                if (calc == "+")
                {
                    answer = (num1 + num2).ToString();
                }
                else if (calc == "-")
                {
                    //To not make minus hard for younger users the largest number should always be first
                    if (num1 < num2)
                    {
                        int tempNum = num1;
                        num1 = num2;
                        num2 = tempNum;
                    }
                    answer = (num1 - num2).ToString();
                }
                else if (calc == "/")
                {
                    //in math a number can not be divaded by zero. To eliminate that posibility num2 cant be zero. 
                    if (num2 == 0)
                    {
                        num2 = 1;
                    }
                    /*the application only uses whole numbers/integers so the numbers are check in a
                     loop by modulus for a rest number. If not 0 new numbers are randomized*/
                    while ((num1 % num2) != 0)
                    {
                        num1 = random.Next(maxRandomNumb);
                        num2 = random.Next(maxRandomNumb);
                        //Again checks so num2 aint zero.
                        if (num2 == 0)
                        {
                            num2 = 1;
                        }
                    }
                    answer = (num1 / num2).ToString();
                }
                //Multiply is always the same so the numbers don't gets to big.
                else if (calc == "*")
                {
                    num1 = random.Next(11);
                    num2 = random.Next(11);
                    answer = (num1 * num2).ToString();
                }

                //writes the math question to screen
                string retVal = $"{num1} {calc} {num2} = ?";
                Label ansLabel = (Label)FindByName("question");
                ansLabel.Text = retVal;
            }
            //if the swith to calculate multiplication table is active only myltiplay numbers between 0 and 10 is used
            else
            {
                int num1 = random.Next(11);
                int num2 = random.Next(11);

                //Calculates right answer
                answer = (num1 * num2).ToString();

                //writes the math question to screen
                string retVal = $"{num1} * {num2} = ?";
                Label ansLabel = (Label)FindByName("question");
                ansLabel.Text = retVal;
            }
        }
    }
}
