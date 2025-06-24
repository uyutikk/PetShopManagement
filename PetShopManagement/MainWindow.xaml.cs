using System.Windows;

namespace PetShopManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AnimalTypesMenu_Click(object sender, RoutedEventArgs e)
        {
            AnimalTypesWindow window = new AnimalTypesWindow();
            window.Show();
        }

        private void BreedsMenu_Click(object sender, RoutedEventArgs e)
        {
            BreedsWindow window = new BreedsWindow();
            window.Show();
        }
    }
}