using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetShopManagement
{
    public partial class AnimalTypesWindow : Window
    {
        private PetShopDataSet petShopDataSet;
        private PetShopDataSetTableAdapters.AnimalTypesTableAdapter animalTypesAdapter;

        public AnimalTypesWindow()
        {
            InitializeComponent();
            petShopDataSet = new PetShopDataSet();
            animalTypesAdapter = new PetShopDataSetTableAdapters.AnimalTypesTableAdapter();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                animalTypesAdapter.Fill(petShopDataSet.AnimalTypes);
                AnimalTypesDataGrid.ItemsSource = petShopDataSet.AnimalTypes.DefaultView;
                StatusTextBlock.Text = "Данные загружены";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddAnimalTypeWindow addWindow = new AddAnimalTypeWindow(petShopDataSet, animalTypesAdapter);
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
                StatusTextBlock.Text = "Вид добавлен";
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalTypesDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)AnimalTypesDataGrid.SelectedItem;
                EditAnimalTypeWindow editWindow = new EditAnimalTypeWindow(petShopDataSet, animalTypesAdapter, selectedRow.Row);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                    StatusTextBlock.Text = "Вид обновлен";
                }
            }
            else
            {
                MessageBox.Show("Выберите вид для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalTypesDataGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить вид?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataRowView selectedRow = (DataRowView)AnimalTypesDataGrid.SelectedItem;
                        selectedRow.Row.Delete();
                        animalTypesAdapter.Update(petShopDataSet.AnimalTypes);
                        LoadData();
                        StatusTextBlock.Text = "Вид удален";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void AnimalTypesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AnimalTypesDataGrid.SelectedItem != null)
            {
                EditButton_Click(sender, e);
            }
        }
    }
}