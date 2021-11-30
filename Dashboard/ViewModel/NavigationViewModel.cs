using Dashboard.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Dashboard.ViewModel
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource MenuItemsCollection;

        public ICollectionView SourceCollection => MenuItemsCollection.View;


        public NavigationViewModel()
        {
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                     new MenuItems{ MenuName = "Home",MenuImage = @"/Assets/Home_Icon.png"},
                     new MenuItems{ MenuName = "Desktop",MenuImage = @"/Assets/Desktop_Icon.png"},
                     new MenuItems{ MenuName = "Document",MenuImage = @"/Assets/Document_Icon.png"},
                     new MenuItems{ MenuName = "Download",MenuImage = @"/Assets/Download_Icon.png"},
                     new MenuItems{ MenuName = "Pictures",MenuImage = @"/Assets/Picture_Icon.png"},
                     new MenuItems{ MenuName = "Music",MenuImage = @"/Assets/Music_Icon.png"},
                     new MenuItems{ MenuName = "Movies",MenuImage = @"/Assets/Movies_Icon.png"},
                     new MenuItems{ MenuName = "Trash",MenuImage = @"/Assets/Trash_Icon.png"}
            };
            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            MenuItemsCollection.Filter += MenuItems_Filter;
            SelectViewModel = new StartupViewModel();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string fillterText;
        public string FilterText
        {
            get => fillterText;
            set
            {

                fillterText = value;
                MenuItemsCollection.View.Refresh();
                OnPropertyChanged("Filter Text");
            }
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }
            MenuItems _items = e.Item as MenuItems;
            if (_items.MenuName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }


        private object _selectViewModel;
        public object SelectViewModel
        {
            get => _selectViewModel;
            set { _selectViewModel = value; OnPropertyChanged("SelectViewModel"); }
        }

        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {

                case "Home":
                    SelectViewModel = new HomeViewModel();
                    break;
                case "Desktop":
                    SelectViewModel = new DesktopViewModel();
                    break;
                case "Document":
                    SelectViewModel = new DocumentViewModel();
                    break;
                case "Download":
                    SelectViewModel = new DownloadViewModel();
                    break;
                case "Pictures":
                    SelectViewModel = new PictureViewModel();
                    break;
                case "Music":
                    SelectViewModel = new MusicViewModel();
                    break;
                case "Movies":
                    SelectViewModel = new MovieViewModel();
                    break;
                case "Trash":
                    SelectViewModel = new TrashViewModel();
                    break;
                default:
                    SelectViewModel = new HomeViewModel();
                    break;
            }

        }


        public ICommand _menucommand;
        public ICommand MenuCommand
        {
            get
            {

                if (_menucommand == null)
                    _menucommand = new RelayCommand(param => SwitchViews(param));
                return _menucommand;
            }
        }

        public void PCView()
        {
            SelectViewModel = new PCViewModel();
        }
        public ICommand _pccommand;
        public ICommand ThisPCCommand
        {
            get
            {

                if (_pccommand == null) 
                    _pccommand = new RelayCommand(param => PCView());
                return _pccommand;
            }
        }
        public void ShowHome()
        {
            SelectViewModel = new HomeViewModel();
        }
        public ICommand _backhomecommand;
        public ICommand BackHomeCommand
        {
            get
            {

                if (_backhomecommand == null) 
                    _backhomecommand = new RelayCommand(param => ShowHome ());
                return _backhomecommand;
            }
        }
        public void CloseApp(object obj)
        {
            MainWindow mainWindow = obj as MainWindow;
            mainWindow.Close();

        }
        public ICommand _closecommand;
        public ICommand CloseAppCommand
        {
            get
            {

                if (_closecommand == null)
                    _closecommand = new RelayCommand(p => CloseApp(p));
                return _closecommand;
            }
        }
    }
}
