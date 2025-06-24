using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetShopManagement
{
    public partial class BreedsWindow : Window
    {
        private PetShopDataSet petShopDataSet;
        private PetShopDataSetTableAdapters.BreedsTableAdapter breedsAdapter;
        private PetShopDataSetTableAdapters.AnimalTypesTableAdapter animalTypesAdapter;

        public BreedsWindow()
        {
            InitializeComponent();
            petShopDataSet = new PetShopDataSet();
            breedsAdapter = new PetShopDataSetTableAdapters.BreedsTableAdapter();
            animalTypesAdapter = new PetShopDataSetTableAdapters.AnimalTypesTableAdapter();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                breedsAdapter.Fill(petShopDataSet.Breeds);
                animalTypesAdapter.Fill(petShopDataSet.AnimalTypes);
                BreedsDataGrid.ItemsSource = petShopDataSet.Breeds.DefaultView;
                StatusTextBlock.Text = "Данные загружены";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddBreedWindow addWindow = new AddBreedWindow(petShopDataSet, breedsAdapter, animalTypesAdapter);
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
                StatusTextBlock.Text = "Порода добавлена";
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (BreedsDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)BreedsDataGrid.SelectedItem;
                EditBreedWindow editWindow = new EditBreedWindow(petShopDataSet, breedsAdapter, animalTypesAdapter, selectedRow.Row);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                    StatusTextBlock.Text = "Порода обновлена";
                }
            }
            else
            {
                MessageBox.Show("Выберите породу для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (BreedsDataGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить породу?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataRowView selectedRow = (DataRowView)BreedsDataGrid.SelectedItem;
                        selectedRow.Row.Delete();
                        breedsAdapter.Update(petShopDataSet.Breeds);
                        LoadData();
                        StatusTextBlock.Text = "Порода удалена";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void BreedsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BreedsDataGrid.SelectedItem != null)
            {
                EditButton_Click(sender, e);
            }
        }
    }
}