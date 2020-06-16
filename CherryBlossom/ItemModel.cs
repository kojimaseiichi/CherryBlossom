using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows.Controls;

namespace CherryBlossom
{
    public class ItemModel : BindableBase
    {
        private bool _selected;
        private string _name;

        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref this._selected, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref this._name, value); }
        }

    }
}
