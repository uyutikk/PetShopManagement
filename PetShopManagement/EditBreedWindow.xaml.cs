using System;
using System.Data;
using System.Windows;

namespace PetShopManagement
{
    public partial class EditBreedWindow : Window
    {
        private PetShopDataSet petShopDataSet;
        private PetShopDataSetTableAdapters.BreedsTableAdapter breedsAdapter;
        private PetShopDataSetTableAdapters.AnimalTypesTableAdapter animalTypesAdapter;
        private DataRow selectedRow;

        public EditBreedWindow(PetShopDataSet dataSet, PetShopDataSetTableAdapters.BreedsTableAdapter breedAdapter,
            PetShopDataSetTableAdapters.AnimalTypesTableAdapter typeAdapter, DataRow row)
        {
            InitializeComponent();
            petShopDataSet = dataSet;
            breedsAdapter = breedAdapter;
            animalTypesAdapter = typeAdapter;
            selectedRow = row;

            animalTypesAdapter.Fill(petShopDataSet.AnimalTypes);
            AnimalTypeComboBox.ItemsSource = petShopDataSet.AnimalTypes.DefaultView;
            AnimalTypeComboBox.SelectedValue = selectedRow["AnimalTypeID"];
            BreedNameTextBox.Text = selectedRow["BreedName"].ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    selectedRow["AnimalTypeID"] = ((DataRowView)AnimalTypeComboBox.SelectedItem)["AnimalTypeID"];
                    selectedRow["BreedName"] = BreedNameTextBox.Text;
                    breedsAdapter.Update(petShopDataSet.Breeds);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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