
using Microsoft.Maui.Controls;
using System;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly Button[,] buttons = new Button[5, 5];
        private readonly bool[,] states = new bool[5, 5];

        public MainPage()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var button = new Button
                    {
                        Text = "",
                        BackgroundColor = Colors.LightGray,
                        WidthRequest = 60,
                        HeightRequest = 60,
                    };
                    button.Clicked += Button_Clicked;
                    buttons[i, j] = button;
                    buttonsGrid.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    states[i, j] = false; 
                }
            }

            
            states[2, 2] = true;
            UpdateButtons();
        }

        private bool AreAllButtonsPressed()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!states[i, j])
                    {
                        return false; 
                    }
                }
            }
            return true; 
        }


        private void CheckGameCompletion()
        {
            if (AreAllButtonsPressed())
            {
                DisplayAlert("GRATULACJE", "Udało Ci się wciśnąć wszystkie przyciski!", "OK");
                
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var position = GetButtonPosition(button);
            ToggleButtonState(position.row, position.column);
            ToggleButtonState(position.row - 1, position.column);
            ToggleButtonState(position.row + 1, position.column);
            ToggleButtonState(position.row, position.column - 1);
            ToggleButtonState(position.row, position.column + 1);
            UpdateButtons();
            CheckGameCompletion();
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void ResetGame()
        {
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    states[i, j] = false;
                    buttons[i, j].BackgroundColor = Colors.LightGray;
                }
            }

            
            states[2, 2] = true;

            UpdateButtons(); 
        }

        private (int row, int column) GetButtonPosition(Button button)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (buttons[i, j] == button)
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

            private void ToggleButtonState(int x, int y)
        {
            if (x >= 0 && x < 5 && y >= 0 && y < 5)
            {
                states[x, y] = !states[x, y];
            }
        }

        private void UpdateButtons()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j].BackgroundColor = states[i, j] ? Colors.Blue : Colors.DarkBlue;
                }
            }
        }

    }
}


