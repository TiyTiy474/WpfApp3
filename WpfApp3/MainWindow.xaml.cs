
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private MenuItem _currentMenuItem;
        private Brush _defaultMenuItemBackground = Brushes.Transparent;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTopLevelMenu_Click(object sender, RoutedEventArgs e)
        {
            string menuName = TopLevelMenu.Text.Trim();
            if (string.IsNullOrEmpty(menuName))
            {
                StatusText.Text = "Введите имя для пункта верхнего уровня.";
                return;
            }

            // Проверка на дублирование
            foreach (var item in MainMenu.Items)
            {
                if (item is MenuItem mi && mi.Header?.ToString() == menuName)
                {
                    StatusText.Text = "Такой пункт меню уже существует.";
                    return;
                }
            }

            var menuItem = new MenuItem { Header = menuName };
            menuItem.Click += MenuItem_Click;
            MainMenu.Items.Add(menuItem);

            SetCurrentMenuItem(menuItem);

            StatusText.Text = $"Добавлен пункт меню: {menuName}";
            TopLevelMenu.Text = "";
        }

        private void AddSubMenu_Click(object sender, RoutedEventArgs e)
        {
            string subItemName = SubItem.Text.Trim();
            if (string.IsNullOrEmpty(subItemName))
            {
                StatusText.Text = "Введите имя для подменю.";
                return;
            }

            if (_currentMenuItem == null)
            {
                StatusText.Text = "Сначала выберите или создайте пункт верхнего уровня.";
                return;
            }

            // Проверка на дублирование подменю
            foreach (var item in _currentMenuItem.Items)
            {
                if (item is MenuItem mi && mi.Header?.ToString() == subItemName)
                {
                    StatusText.Text = "Такой подпункт уже существует.";
                    return;
                }
            }

            var subMenuItem = new MenuItem { Header = subItemName };
            _currentMenuItem.Items.Add(subMenuItem);
            StatusText.Text = $"Добавлен подпункт: {subItemName} в меню {_currentMenuItem.Header}";
            SubItem.Text = "";
        }

        // Метод для обработки клика по пункту меню верхнего уровня
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                SetCurrentMenuItem(menuItem);
                StatusText.Text = $"Текущий пункт меню: {menuItem.Header}";
            }
        }

        // Вспомогательный метод для выделения выбранного пункта меню
        private void SetCurrentMenuItem(MenuItem menuItem)
        {
            // Сброс выделения у всех пунктов
            foreach (var item in MainMenu.Items)
            {
                if (item is MenuItem mi)
                    mi.Background = _defaultMenuItemBackground;
            }

            _currentMenuItem = menuItem;
            menuItem.Background = new SolidColorBrush(Color.FromArgb(60, 79, 142, 247)); // Легкое выделение
        }

        // Реализация метода закрытия окна
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
