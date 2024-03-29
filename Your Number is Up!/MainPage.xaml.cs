﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
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

        //symbolic constants used for moving blocks around at later levels
        const int pattern1 = 0;
        const int pattern2 = 1;
        const int pattern3 = 2;
        const int pattern4 = 3;
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
        //boolean varible that keeps track of wheter or not the timer needs to be restarted
        bool RestartTimer = false;
        //Controls total range of values that can be used for the main equation
        int Maxrange = 11;
        int Minrange = 1;
        //constructor
        public MainPage()
        {
            InitializeComponent();

            Timer(_countSeconds);
            Initialize_values();

        }

        //Will keep track of time, and has the ability to reset when necessary
        public void Timer(int count)
        {
            
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                
                
                if(RestartTimer == false)
                {
                    Time.Text = "Time: " + count.ToString();
                    count--;
                }
                

            if (count == 0)
            {
                lives--;

                
                Initialize_values();
                Timer(_countSeconds);
                return false;

            }
            else if (RestartTimer == true)
                {
                    RestartTimer = false;
                    
                    return false;
                    
                }
                else
                {
                    return true;
                }

                


            });
        }
        
        //Setting variables to appropraite values, only called when first stareted, or user enters correct response
        //will also change things depending on what level the user has reached
        public void Initialize_values()
        {
            
            //check to see if level should be incremented
            if(score == initial_score + 3)
            {
                Increment_Level();
            }
            //Restart Timer
            
            RestartTimer = true;
            Timer(_countSeconds);
            //create values that fill equation fields
            EquationCreator();
            MainResult.Text = Result.ToString();
            Score.Text = "Score: "+score.ToString();
            Time.Text = "Time: " + _countSeconds.ToString();
            
            Display_Lives();
            Level.Text = "Level: " + level.ToString();
            SecondOp.Text = SecondOperand.ToString();
            //if level is less than 4, make every block have the same number 
            if(level <= 4)
            {

                Addition.Text = UserNumber.ToString();

                Multiplication.Text = UserNumber.ToString();

                Division.Text = UserNumber.ToString();

                Subtraction.Text = UserNumber.ToString();
                
            }

            //if the level is greator than 4, the numbers within the blocks will now not all be the same, each will be an individual number.
            else
            {
                
                int offset1 = new Random().Next(-5, 5);

                int offset2 = new Random().Next(-5, 5);

                int offset3 = new Random().Next(-5, 5);

                if (Operation == ADDITION)
                {
                    Addition.Text = UserNumber.ToString();
                    
                    Multiplication.Text = (UserNumber + offset1).ToString();

                    Division.Text = (UserNumber + offset2).ToString();

                    Subtraction.Text = (UserNumber + offset3).ToString();
                }
                else if (Operation == SUBTRACTION)
                {
                    Subtraction.Text = UserNumber.ToString();

                    Addition.Text = (UserNumber + offset1).ToString();

                    Division.Text = (UserNumber + offset2).ToString();

                    Multiplication.Text = (UserNumber + offset3).ToString();
                }
                else if (Operation == MULTIPLICATION)
                {
                    Multiplication.Text = UserNumber.ToString();

                    Addition.Text = (UserNumber + offset1).ToString();

                    Division.Text = (UserNumber + offset2).ToString();

                    Subtraction.Text = (UserNumber + offset3).ToString();
                }
                else if (Operation == DIVISION)
                {
                    Division.Text = UserNumber.ToString();

                    Multiplication.Text = (UserNumber + offset1).ToString();

                    Addition.Text = (UserNumber + offset2).ToString();

                    Subtraction.Text = (UserNumber + offset3).ToString();
                }


            }
            //Once the user gets to level 7, the blocks will start to move positions
            if(level > 6)
            {
                MoveBlocks();
            }
            

            
        }

        //Displays lives to the screen
        public void Display_Lives()
        {
            Lives.Text = "Lives: " + lives.ToString();
            
            if (lives == 0)
            {
                GameDone();
            }
            
        }
        
        
        
        
        //This function increments the level, decreasing the time, and increasing the range of numbers used in the equations
        public void Increment_Level()
        {

            initial_score = score;
            if (level > 4)
            {
                //make other numbers random numbers within range of correct number except for correct block
                //if you think its worth it, also make the block operations move positions at another level milestone
                //also at higher levels dont divide number by itself, ensure that if it picks divison, it picks harder divison with a wider range of numbers

                
                level++;
            }

            else
            {
                _countSeconds -= 1;
                level++;
                Maxrange += 5;
                Minrange -= 5;
            }
            

        }
        //If the game is over, this function will be called and display the game over screen
        public void GameDone()
        {
            Navigation.PushAsync(new GameOver());
        }

        //moves blocks to unique new positions
        
        public void MoveBlocks()
        {
            int pattern_choice = new Random().Next(0, 4);

            MainGrid.Children.Clear();
            

            //original pattern
            if (pattern_choice == pattern1)
            {
                
                //move addition
                
                MainGrid.Children.Add(Addition, 0, 0);
                //move subtraction
                
                MainGrid.Children.Add(Subtraction, 0, 1);
                //move multiplication
                
                MainGrid.Children.Add(Multiplication, 1, 0);
                //move division
                
                MainGrid.Children.Add(Division, 1, 1);
            }
            //pattern 2
            else if (pattern_choice == pattern2)
            {
                //move addition
                
                MainGrid.Children.Add(Addition, 1, 1);
                //move subtraction
                
                MainGrid.Children.Add(Subtraction, 1, 0);
                //move multiplication
                
                MainGrid.Children.Add(Multiplication, 0, 1);
                //move division
                
                MainGrid.Children.Add(Division, 0, 0);
            }
            //pattern 3
            else if (pattern_choice == pattern3)
            {
                //move addition
                MainGrid.Children.Add(Addition, 0, 1);
                //move subtraction
                
                MainGrid.Children.Add(Subtraction, 0, 0);
                //move multiplication
                
                MainGrid.Children.Add(Multiplication, 1, 1);
                //move division
                
                MainGrid.Children.Add(Division, 1, 0);
            }
            //pattern 4
            else if (pattern_choice == pattern4)
            {
                //move addition
                
                MainGrid.Children.Add(Addition, 1, 1);
                //move subtraction
                
                MainGrid.Children.Add(Subtraction, 0, 1);
                //move multiplication
                
                MainGrid.Children.Add(Multiplication, 1, 0);
                //move division
                
                MainGrid.Children.Add(Division, 0, 0);
            }
            

        }
        

        






        //Create equations display values to mainpage 
        public void EquationCreator()
        {
            //Generating random number that user will click for the equation
            UserNumber = new Random().Next(Minrange, Maxrange);
            //Generating random number that will be used as the second operand in the equation 
            SecondOperand = new Random().Next(Minrange, Maxrange);


            //need to ensure that SecondOperand is not zero
            while(SecondOperand == 0)
            {
                SecondOperand = new Random().Next(Minrange, Maxrange);
            }




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
                while (Result * SecondOperand != UserNumber)
                  {
                     //This will decremnt SecondOperand until the Result becomes an integer
                     SecondOperand -= 1;
                     Result = UserNumber / SecondOperand;
                  }
                
                
                
                
            }

            
                
            
            
            


        }
        //When drag starts, save data that it contains 
        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            var label = (sender as Element)?.Parent as Label;
            e.Data.Properties.Add("ClassId", label.ClassId);
        }
        //When drag is dropped, check to see if right answer
        private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
            Boolean Correct = false;
           
            
            String TypeOfOp  = (String) e.Data.Properties["ClassId"];
            var frame = (sender as Element)?.Parent as Frame;
            


            if (TypeOfOp == "AdditionDrag")
            {
                if (Result == UserNumber + SecondOperand)
                {
                    Correct = true;
                }
            }



            else if (TypeOfOp == "SubtractionDrag")
            {
                if (Result == UserNumber - SecondOperand)
                {
                    Correct = true;
                }
            }


            else if (TypeOfOp == "MultiplicationDrag")
            {
                if (Result == UserNumber * SecondOperand)
                {
                    Correct = true;                
                }

            }


            else if (TypeOfOp == "DivisionDrag")
            {
                if (Result == UserNumber / SecondOperand)
                {
                    Correct = true;
                }
            }


            if(Correct == true)
            {
                //If correct, reinitialize values and make black block green
                frame.BackgroundColor = Color.Green;
                Device.StartTimer(new TimeSpan(0, 0, 1), () => {
                    frame.BackgroundColor = Color.Black;
                    return false;
                });
                
                score += 1;
                Initialize_values();
                
            }
           
            else
            {
                //if wrong, decrement lives, make black box red
                frame.BackgroundColor = Color.Red;
                Device.StartTimer(new TimeSpan(0, 0, 1), () => {
                    frame.BackgroundColor = Color.Black;
                    return false;
                });
                
                lives--;
                Display_Lives();
                
            }


        }
    }
}
