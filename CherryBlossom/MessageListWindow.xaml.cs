using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CherryBlossom
{
    public class UniqueMessageIdRule : ValidationRule
    {
        public CollectionViewSource CurrentCollection { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                ObservableCollection<MessageModel> castedCollection = CurrentCollection.Source as ObservableCollection<MessageModel>;
                var messageId = value as string;
                if (castedCollection.Count(x => messageId == x.MessageId) > 1)
                    return new ValidationResult(false, value);
            }
            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// MessageListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MessageListWindow : Window
    {
        public AppModel AppModel { get; set; }

        public MessageListWindow(AppModel appModel)
        {
            this.AppModel = appModel;
            InitializeComponent();
            this.DataContext = this;
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var m = e.Item as MessageModel;
            var prefix = new List<string>();
            if (_cbError.IsChecked == true) prefix.Add("E");
            if (_cbWarning.IsChecked == true) prefix.Add("W");
            if (_cbInfo.IsChecked == true) prefix.Add("I");
            if (_cbQuestion.IsChecked == true) prefix.Add("Q");
            if (m != null)
            {
                bool typeMatch = prefix.Any(x => m.MessageId.StartsWith(x));
                if (typeMatch == false)
                {
                    e.Accepted = false;
                    return;
                }
                var reg = new System.Text.RegularExpressions.Regex(" +");
                bool keywordMatch = reg.Split(_keywords.Text).All(x => m.Message.Contains(x));
                if (keywordMatch)
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void _keywords_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(_dgMessages.ItemsSource).Refresh();
        }

        private void _cbMessageType_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(_dgMessages.ItemsSource).Refresh();
        }
    }
}
