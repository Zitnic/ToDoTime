using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimerTodo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public CurrentTimeViewModel Clock { get; } = new();

        public ObservableCollection<ToDoItem> Items { get; } = new();


        public void Add(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                Items.Add(new ToDoItem { Text = text });
        }


        private void AddButton_Click(object sender, RoutedEventArgs e) => AddItem();

        private void NewItemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AddItem();
        }

        private void AddItem()
        {
            Add(NewItemTextBox.Text);
            NewItemTextBox.Clear();
        }

    }


    public class CurrentTimeViewModel : INotifyPropertyChanged
    {
        private string _currentTime = DateTime.Now.ToString("HH:mm:ss");

        public CurrentTimeViewModel()
        {
            UpdateTime();
        }

        private async void UpdateTime()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            await Task.Delay(1000);
            UpdateTime();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; OnPropertyChanged(); }
        }
    }

}


public class ToDoItem : INotifyPropertyChanged
{
    private bool _isDone;
    public string? Text { get; set; }

    public bool IsDone
    {
        get => _isDone;
        set { _isDone = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}