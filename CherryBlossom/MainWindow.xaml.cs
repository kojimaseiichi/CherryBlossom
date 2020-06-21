using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CherryBlossom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppModel _appModel = new AppModel();

        public AppModel AppModel
        {
            get { return _appModel; }
        }

        public MainWindow()
        {
            InitializeComponent();
            var view = new DictionaryView(_appModel);
            _placeholder.Children.Add(view);
        }

        private void _menuSave_Click(object sender, RoutedEventArgs e)
        {
            _Save();
        }

        private void _Save()
        {
            SaveFileDialog d = new SaveFileDialog();
            if (d.ShowDialog() == true)
            {
                _appModel.Save(d.FileName);
            }
        }

        private void _menuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            _Load();
        }

        private void _Load()
        {
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() == true)
            {
                _appModel = new AppModel();
                _appModel.Load(d.FileName);
                var view = new DictionaryView(_appModel);
                _placeholder.Children.Clear();
                _placeholder.Children.Add(view);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var result = MessageBox.Show("保存しますか？", "項目辞書", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _Save();   
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }
    }
}
