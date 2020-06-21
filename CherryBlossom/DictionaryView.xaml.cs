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
    /// DictionaryView.xaml の相互作用ロジック
    /// </summary>
    public partial class DictionaryView : UserControl
    {

        private AppModel _appModel;

        public AppModel AppModel
        {
            get { return _appModel; }
        }

        public DictionaryView(AppModel appModel)
        {
            InitializeComponent();
            _appModel = appModel;
            _dictView.AppModel = _appModel;
            _treeNav.AppModel = _appModel;
        }

    }
}
