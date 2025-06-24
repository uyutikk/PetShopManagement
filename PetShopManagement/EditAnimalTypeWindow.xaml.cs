using System;
using System.Data;
using System.Windows;

namespace PetShopManagement
{
    public partial class EditAnimalTypeWindow : Window
    {
        private PetShopDataSet petShopDataSet;
        private PetShopDataSetTableAdapters.AnimalTypesTableAdapter animalTypesAdapter;
        private DataRow selectedRow;

        public EditAnimalTypeWindow(PetShopDataSet dataSet, PetShopDataSetTableAdapters.AnimalTypesTableAdapter adapter, DataRow row)
        {
            InitializeComponent();
            petShopDataSet = dataSet;
            animalTypesAdapter = adapter;
            selectedRow = row;
            TypeNameTextBox.Text = selectedRow["TypeName"].ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    selectedRow["TypeName"] = TypeNameTextBox.Text;
                    animalTypesAdapter.Update(petShopDataSet.AnimalTypes);
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
            if (string.IsNullOrWhiteSpace(TypeNameTextBox.Text))
            {
                MessageBox.Show("Введите название вида", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}