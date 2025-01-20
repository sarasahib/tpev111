using Microsoft.Maui.Controls;

namespace MauiApp1ATM
{
    public partial class MainPage : ContentPage
    {
        int clickCount = 0;

        public MainPage()
        {
            InitializeComponent();  // Initialise la page
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            clickCount++;  

            string buttonText = clickCount == 1
                ? $"Clicked {clickCount} time"  
                : $"Clicked {clickCount} times"; // Texte pour plusieurs clics

            CounterBtn.Text = buttonText;

            // Annonce du texte pour les lecteurs d'écran
            SemanticScreenReader.Announce(buttonText);
        }
    }
}

