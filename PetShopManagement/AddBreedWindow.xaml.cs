using System;
using System.Data;
using System.Windows;

namespace PetShopManagement
{
    public partial class AddBreedWindow : Window
    {
        private PetShopDataSet petShopDataSet;
        private PetShopDataSetTableAdapters.BreedsTableAdapter breedsAdapter;
        private PetShopDataSetTableAdapters.AnimalTypesTableAdapter animalTypesAdapter;

        public AddBreedWindow(PetShopDataSet dataSet, PetShopDataSetTableAdapters.BreedsTableAdapter breedAdapter,
            PetShopDataSetTableAdapters.AnimalTypesTableAdapter typeAdapter)
        {
            InitializeComponent();
            petShopDataSet = dataSet;
            breedsAdapter = breedAdapter;
            animalTypesAdapter = typeAdapter;
            animalTypesAdapter.Fill(petShopDataSet.AnimalTypes);
            AnimalTypeComboBox.ItemsSource = petShopDataSet.AnimalTypes.DefaultView;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    DataRow newRow = petShopDataSet.Breeds.NewRow();
                    newRow["AnimalTypeID"] = ((DataRowView)AnimalTypeComboBox.SelectedItem)["AnimalTypeID"];
                    newRow["BreedName"] = BreedNameTextBox.Text;
                    petShopDataSet.Breeds.Rows.Add(newRow);
                    breedsAdapter.Update(petShopDataSet.Breeds);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateInput()
        {
            if (AnimalTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите вид животного", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(BreedNameTextBox.Text))
            {
                MessageBox.Show("Введите название породы", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}