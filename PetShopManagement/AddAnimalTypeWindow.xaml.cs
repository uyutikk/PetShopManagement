using System;
using System.Data;
using System.Windows;

namespace PetShopManagement
{
    public partial class AddAnimalTypeWindow : Window
    {
        private PetShopDataSet petShopDataSet;
        private PetShopDataSetTableAdapters.AnimalTypesTableAdapter animalTypesAdapter;

        public AddAnimalTypeWindow(PetShopDataSet dataSet, PetShopDataSetTableAdapters.AnimalTypesTableAdapter adapter)
        {
            InitializeComponent();
            petShopDataSet = dataSet;
            animalTypesAdapter = adapter;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    DataRow newRow = petShopDataSet.AnimalTypes.NewRow();
                    newRow["TypeName"] = TypeNameTextBox.Text;
                    petShopDataSet.AnimalTypes.Rows.Add(newRow);
                    animalTypesAdapter.Update(petShopDataSet.AnimalTypes);
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
            if (string.IsNullOrWhiteSpace(TypeNameTextBox.Text))
            {
                MessageBox.Show("Введите название вида", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}