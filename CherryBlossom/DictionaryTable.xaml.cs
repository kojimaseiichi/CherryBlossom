using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Resources;
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
using System.Linq;

namespace CherryBlossom
{
    public class UniqueNameItemRule : ValidationRule
    {

        public CollectionViewSource CurrentCollection { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                ObservableCollection<ItemModel> castedCollection = CurrentCollection.Source as ObservableCollection<ItemModel>;
                var itemExpression = value as string;
                if (castedCollection.Count(x => itemExpression == x.ItemExpression) > 1)
                    return new ValidationResult(false, value);
            }
            return new ValidationResult(true, null);
        }
    }
    public class UniqueMorphRule : ValidationRule
    {
        public CollectionViewSource CurrentCollection { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                    ObservableCollection<MorphModel> castedCollection = CurrentCollection.Source as ObservableCollection<MorphModel>;
                var morphName = value as string;
                if (castedCollection.Count(x => morphName == x.Morph) > 1)
                    return new ValidationResult(false, value);
            }
            return new ValidationResult(true, null);
        }
    }

    public class UniqueMorphAlphaRule : ValidationRule
    {
        public CollectionViewSource CurrentCollection { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                ObservableCollection<MorphModel> castedCollection = CurrentCollection.Source as ObservableCollection<MorphModel>;
                var morphAlpha = value as string;
                if (castedCollection.Count(x => morphAlpha == x.Alpha) > 1)
                    return new ValidationResult(false, value);
            }
            return new ValidationResult(true, null);
        }
    }

    public class ItemExpressionRule : ValidationRule
    {
        public CollectionViewSource MorphCollection { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null)
            {
                ObservableCollection<MorphModel> morphCollection = MorphCollection.Source as ObservableCollection<MorphModel>;
                var itemExp = value as string;
                var regws = new System.Text.RegularExpressions.Regex(" +");
                foreach (var morph in regws.Split(itemExp))
                {
                    if (morphCollection.Any(x => x.Morph == morph) == false)
                        return new ValidationResult(false, value);
                }
            }
            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// DictionaryView.xaml の相互作用ロジック
    /// </summary>
    public partial class DictionaryTable : UserControl
    {
        public DictionaryTable()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private AppModel _appModel;

        public AppModel AppModel
        {
            get { return _appModel; }
            set
            {
                _appModel = value;
                _dgItems.ItemsSource = _appModel.Items;
                _dgMorphs.ItemsSource = _appModel.Morphs;
            }
        }

        private void _dgItems_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var col = e.Column as DataGridTextColumn;
            if (col != null)
            {
                var item = e.Row.Item as ItemModel;
                var bindingPath = (col.Binding as Binding).Path.Path;
                _dgItems_CellEditEnding_SetItemAlpha(e, item, bindingPath);
            }
        }

        private void _dgItems_CellEditEnding_SetItemAlpha(DataGridCellEditEndingEventArgs e, ItemModel item, string bindingPath)
        {
            if (bindingPath == nameof(item.ItemExpression))
            {
                var tb = e.EditingElement as TextBox;
                var reg = new System.Text.RegularExpressions.Regex("[ ]");
                var alpha = reg.Split(tb.Text).Select(x =>
                {
                    var first = _appModel.Morphs.FirstOrDefault(p => p.Morph == x);
                    if (first == null) return "?";
                    return first.Alpha;
                })
                    .Aggregate((a, b) => a + "_" + b);
                item.ItemAlpha = alpha;
            }
        }
    }
}
