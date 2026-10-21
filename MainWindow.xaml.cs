using System;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        private TodoService _todoService;

        public MainWindow()
        {
            InitializeComponent();

            ITaskRepository repository = new JsonTaskRepository("tasks.json");
            _todoService = new TodoService(repository);

            CmbPriority.ItemsSource = Enum.GetValues(typeof(TaskPriority));
            CmbStatus.ItemsSource = Enum.GetValues(typeof(TaskStatus));

            CmbPriority.SelectedIndex = 0;
            CmbStatus.SelectedIndex = 0;
            DpDueDate.SelectedDate = DateTime.Now;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            GridTasks.ItemsSource = null;
            GridTasks.ItemsSource = _todoService.GetAllTasks();
        }

        private void ClearForm()
        {
            TxtTitle.Text = "";
            TxtDescription.Text = "";
            DpDueDate.SelectedDate = DateTime.Now;
            CmbPriority.SelectedIndex = 0;
            CmbStatus.SelectedIndex = 0;
            GridTasks.SelectedItem = null;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newTask = new TodoModel
                {
                    Title = TxtTitle.Text,
                    Description = TxtDescription.Text,
                    DueDate = DpDueDate.SelectedDate ?? DateTime.Now,
                    Priority = (TaskPriority)CmbPriority.SelectedItem,
                    Status = (TaskStatus)CmbStatus.SelectedItem
                };

                _todoService.AddTask(newTask);
                RefreshGrid();
                ClearForm();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GridTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridTasks.SelectedItem is TodoModel selectedTask)
            {
                TxtTitle.Text = selectedTask.Title;
                TxtDescription.Text = selectedTask.Description;
                DpDueDate.SelectedDate = selectedTask.DueDate;
                CmbPriority.SelectedItem = selectedTask.Priority;
                CmbStatus.SelectedItem = selectedTask.Status;
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (GridTasks.SelectedItem is TodoModel selectedTask)
            {
                try
                {
                    selectedTask.Title = TxtTitle.Text;
                    selectedTask.Description = TxtDescription.Text;
                    selectedTask.DueDate = DpDueDate.SelectedDate ?? DateTime.Now;
                    selectedTask.Priority = (TaskPriority)CmbPriority.SelectedItem;
                    selectedTask.Status = (TaskStatus)CmbStatus.SelectedItem;

                    RefreshGrid();
                    ClearForm();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу в таблице для редактирования.");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridTasks.SelectedItem is TodoModel selectedTask)
            {
                var result = MessageBox.Show($"Удалить задачу '{selectedTask.Title}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _todoService.DeleteTask(selectedTask);
                    RefreshGrid();
                    ClearForm();
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления.");
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _todoService.SaveTasks();
            MessageBox.Show("Задачи успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            _todoService.LoadTasks();
            RefreshGrid();
            MessageBox.Show("Задачи успешно загружены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}