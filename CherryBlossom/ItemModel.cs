using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace CherryBlossom
{
    public enum ItemDataType
    {
        Text,
        Numeric
    }
       
    public class ItemModel : ValidatableBase
    {
        private bool _selected = false;
        private string _itemExpression = string.Empty;
        private string _itemAlpha = string.Empty;
        private ItemDataType _dataType;
        private uint _length;
        private int _rangeMin;
        private int _rangeMax;
        private uint _precition;
        private uint _scale;

        public ObservableCollection<MorphModel> Morphs { get; set; }

        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref this._selected, value); }
        }

        [Required(ErrorMessage ="必須")]
        [RegularExpression(@"^([ ]|[\u4e00-\u9fcf\uF900-\uFAFF\u3400-\u4DBF]|[\p{IsHiragana}]|[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F])*$", ErrorMessage = "全角ひらがな、全角カタカナ、漢字が入力できます。")]
        public string ItemExpression
        {
            get { return _itemExpression; }
            set
            {
                var reg = new System.Text.RegularExpressions.Regex(" +");
                SetProperty(ref this._itemExpression, reg.Replace(value, " ").Trim());
                OnPropertyChanged(nameof(ItemReference));
            }
        }

        public string ItemAlpha
        {
            get { return _itemAlpha; }
            set { SetProperty(ref this._itemAlpha, value); }
        }

        public string ItemReference
        {
            get { return _itemExpression; }
        }

        public ItemDataType ItemDataType
        {
            get { return _dataType; }
            set
            {
                SetProperty(ref this._dataType, value);
            }
        }


    }
}
