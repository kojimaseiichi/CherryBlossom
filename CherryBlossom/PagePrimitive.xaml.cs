using System;
using System.Collections.Generic;
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

namespace CherryBlossom
{
    /// <summary>
    /// PagePrimitive.xaml の相互作用ロジック
    /// </summary>
    public partial class PagePrimitive : Page
    {
        private readonly string _kKoumokuJisho = "項目辞書";
        private readonly string _kKeitaisoJisho = "形態素辞書";

        private AppModel _appModel;
        public PagePrimitive(AppModel appModel)
        {
            InitializeComponent();
            _appModel = appModel;
            _dgKoumoku.ItemsSource = appModel.Items;
            _dgKeitaiso.ItemsSource = appModel.Morphs;
        }

    }
}
