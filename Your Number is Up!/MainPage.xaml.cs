﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

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
        
        int _countSeconds = 10;

        //User score
        int score = 0;
        //initial score(keeping track to increment levels)
        int initial_score = 0;
        //User lives
        int lives = 3;
        //User level
        int level = 1;
        public MainPage()
        {
            InitializeComponent();

            Initialize_values();

            Timer(_countSeconds);
        }


        public void Timer(int count)
        {
            count = _countSeconds;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                count--;
                Time.Text = "Time: " + count.ToString();

                if (_countSeconds == 0)
                {
                    lives--;
                    
                    Display_Lives();
                    RestartTimer();
                    return false;

                }
                else
                {
                    return true;
                }

                


            });
        }
        public void RestartTimer()
        {
            Timer(_countSeconds);
        }
        //Setting variables to appropraite values, only called when first stareted, or user enters correct response
        public void Initialize_values()
        {
            //check to see if level should be incremented
            if(score == initial_score + 3)
            {
                Increment_Level();
            }
            EquationCreator();
            MainResult.Text = "Main Result: "+ Result.ToString();
            Score.Text = "Score: "+score.ToString();
            Time.Text = "Time: " + _countSeconds.ToString();
            
            Display_Lives();
            Level.Text = "Level: " + level.ToString();
            SecondOp.Text = "Second Operand: " + SecondOperand.ToString();

            Addition.Text = "Addition" +UserNumber.ToString();

            Multiplication.Text = "Multiplication: "+UserNumber.ToString();

            Division.Text = "Divison: "+UserNumber.ToString();

            Subtraction.Text = "Subtraction "+UserNumber.ToString();
        }

        
        public void Display_Lives()
        {
            Lives.Text = "Lives: " + lives.ToString();
            if (lives == 0)
            {
                GameDone();
            }
            
        }
        
        //This function will determin which button was clicked, and from there determin if the user was correct or incorrect
        
        
        private void ButtonClicked(object sender, EventArgs e)
        {
            Button Button = sender as Button;
            if(Button.ClassId == "Addition")
            {
                if(Result == UserNumber + SecondOperand)
            {
                    score += 1;

                    Initialize_values();
                }
                else
                {
                    lives--;
                    Display_Lives();
                }
            }
            else if (Button.ClassId == "Subtraction")
            {
                if (Result == UserNumber - SecondOperand)
                {
                    score += 1;

                    Initialize_values();
                }
                else
                {
                    lives--;
                    Display_Lives();
                }
            }
            else if (Button.ClassId == "Multiplication")
            {
                if (Result == UserNumber * SecondOperand)
                {
                    score += 1;

                    Initialize_values();
                }
                else
                {
                    lives--;
                    Display_Lives();
                }
            }
            else if (Button.ClassId == "Division")
            {
                if (Result == UserNumber / SecondOperand)
                {
                    score += 1;

                    Initialize_values();
                }
                else
                {
                    lives--;
                    Display_Lives();
                }
            }
          

            

        }
        
        public void Increment_Level()
        {
            initial_score = score;
            _countSeconds -= 2;
            level++;

        }
        public void GameDone()
        {
            Navigation.PushAsync(new GameOver());
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
