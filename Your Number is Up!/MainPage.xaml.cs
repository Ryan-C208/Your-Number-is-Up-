using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Your_Number_is_Up_
{


    
    public partial class MainPage : ContentPage
    {
        //symbolic constants used in operations
        const int ADDITION = 0;
        const int SUBTRACTION = 1;
        const int MULTIPLICATION = 2;
        const int DIVISION = 3;
        //Numbers used in the equation
        int UserNumber;
        int SecondOperand;
        int Result;
        int Operation;

        //User score
        int score = 0;
        
        public MainPage()
        {
            InitializeComponent();

            Initialize_values();
        }

        //Setting variables to appropraite values, only called when first stareted, or user enters correct response
        public void Initialize_values()
        {
            EquationCreator();
            MainResult.Text = "Main Result: "+ Result.ToString();
            Score.Text = "Score: "+score.ToString();

            SecondOp.Text = "Second Operand: " + SecondOperand.ToString();

            Addition.Text = "Addition" +UserNumber.ToString();

            Multiplication.Text = "Multiplication: "+UserNumber.ToString();

            Division.Text = "Divison: "+UserNumber.ToString();

            Subtraction.Text = "Subtraction "+UserNumber.ToString();
        }

        //This function will determin which button was clicked, and from there determin if the user was correct or incorrect
        
        
        private void AdditionButtonClicked(object sender, EventArgs e)
        {
            Button Button = sender as Button;
            if (Result == UserNumber + SecondOperand)
            {
                score += 1;

                Initialize_values();
            }
        }
        private void SubtractionButtonClicked(object sender, EventArgs e)
        {
            Button Button = sender as Button;
            if (Result == UserNumber - SecondOperand)
            {
                score += 1;
                Initialize_values();
            }
        }
        private void MultiplicationButtonClicked(object sender, EventArgs e)
        {
            Button Button = sender as Button;
            if (Result == UserNumber * SecondOperand)
            {
                score += 1;
                Initialize_values();
            }
        }
        private void DivisionButtonClicked(object sender, EventArgs e)
        {
            Button Button = sender as Button;
            if (Result == UserNumber / SecondOperand)
            {
                score += 1;
                Initialize_values();
            }
        }


        //Create equations display values to mainpage 
        public void EquationCreator()
        {
            //Generating random number that user will click for the equation
            UserNumber = new Random().Next(1, 11);
            //Generating random number that will be used as the second operand in the equation 
            SecondOperand = new Random().Next(1, 11);
            //Randomly generating operation that will be used on the two numbers to get the result
            Operation = new Random().Next(0, 4);

            //if statements that do operation based on what it was randomly assigned
            if(Operation == ADDITION)
            {
                Result = UserNumber + SecondOperand;
            }

            else if (Operation == SUBTRACTION)
            {
                Result = UserNumber - SecondOperand;
            }
            else if (Operation == MULTIPLICATION)
            {
                Result = UserNumber * SecondOperand;
            }
            else if (Operation == DIVISION)
            {
                Result = UserNumber / SecondOperand;
                //If the result is not an integer
                while(Result * SecondOperand != UserNumber)
                {
                    //This will decremnt SecondOperand until the Result becomes an integer
                    SecondOperand -= 1;
                    Result = UserNumber / SecondOperand;
                }
                
            }

            
            
            //need to implement geters and setters for all variables
            //Also need to ensure that if division, the result does not end up with any decimal points


        }
    }
}
