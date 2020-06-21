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
    /// TreeNavigation.xaml の相互作用ロジック
    /// </summary>
    public partial class TreeNavigation : UserControl
    {
        private AppModel _appModel;

        private MessageListWindow _wndMessage;

        public AppModel AppModel
        {
            get { return _appModel; }
            set
            {
                _appModel = value;
                if (_wndMessage != null)
                {
                    _wndMessage.Close();
                    _wndMessage = null;
                }
            }
        }
        public TreeNavigation()
        {
            InitializeComponent();
        }

        #region メッセージ一覧

        private void _tviMessageList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_wndMessage == null)
            {

                _wndMessage = new MessageListWindow(_appModel);
                _wndMessage.Owner = Window.GetWindow(this);
                _wndMessage.Closed += _wndMessage_Closed;
                _wndMessage.Show();
            }
            else
            {
                _wndMessage.Activate();
                _wndMessage.Topmost = true;
                _wndMessage.Topmost = false;
                _wndMessage.Focus();
            }
        }

        private void _wndMessage_Closed(object sender, EventArgs e)
        {
            _wndMessage = null;
        }

        #endregion
    }
}
