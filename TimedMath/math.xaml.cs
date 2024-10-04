using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimedMath
{
    public partial class MainPage : ContentPage
    {

        public string[] CheckMathMethod()
        {
            VerticalStackLayout checkBoes = (VerticalStackLayout)FindByName("checkBoxes");

            string[] method = { };

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
            else if (plus.IsChecked || (!minus.IsChecked && !divided.IsChecked && !multiply.IsChecked))
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
                    if (num1 < num2)
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
