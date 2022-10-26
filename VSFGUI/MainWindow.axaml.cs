using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace VSFGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Youtube_Button(object? sender, RoutedEventArgs e)
        {
            ConsoleBox.Text += "\n Youtube Button";
        }

        private void Twitch_Button(object? sender, RoutedEventArgs e)
        {
            ConsoleBox.Text += "\n Twitch Button";

        }

        public TextBox GetConsole()
        {
            return ConsoleBox;
        }
    }
}