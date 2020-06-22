using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Accessibility;
using System.Security.Policy;

namespace CherryBlossom
{
    public enum ItemDataType
    {
        Char,
        Text,
        Decimal,
        Int,
        Long,
        YYYYMM,
        YYYYMMDD
    }
       
    public class ItemModel : ValidatableBase
    {
        private bool _selected = false;
        private string _itemExpression = string.Empty;
        private string _itemAlpha = string.Empty;
        private ItemDataType _dataType;
        private string _length = string.Empty;
        private string _precision = string.Empty;
        private string _scale = string.Empty;
        private string _rangeMin = string.Empty;
        private string _rangeMax = string.Empty;
        private string _regularExpression = string.Empty;

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

        [Required]
        public string ItemAlpha
        {
            get { return _itemAlpha; }
            set { SetProperty(ref this._itemAlpha, value); }
        }

        public string ItemReference
        {
            get { return _itemExpression; }
        }

        public string DataType
        {
            get { return _dataType.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _dataType = ItemDataType.Text;
                else
                    _dataType = ItemDataType.Parse<ItemDataType>(value);
            }
        }

        [Required]
        public ItemDataType ItemDataType
        {
            get { return _dataType; }
            set
            {
                SetProperty(ref this._dataType, value);
                OnPropertyChanged(nameof(IsEnabledLength));
                OnPropertyChanged(nameof(IsEnabledPrecision));
                OnPropertyChanged(nameof(IsEnabledScale));
                OnPropertyChanged(nameof(IsEnabledRangeMin));
                OnPropertyChanged(nameof(IsEnabledRangeMax));
                OnPropertyChanged(nameof(IsEnabledRegularExpression));
            }
        }

        public string Length
        {
            get { return _length; }
            set { SetProperty(ref this._length, value); }
        }

        public bool IsEnabledLength
        {
            get
            {
                return _dataType == ItemDataType.Text ||
                    _dataType == ItemDataType.Char;
            }
        }

        public string Precision
        {
            get { return _precision; }
            set { SetProperty(ref this._precision, value); }
        }

        public bool IsEnabledPrecision
        {
            get { return _dataType == ItemDataType.Decimal; }
        }

        public string Scale
        {
            get { return _scale; }
            set { SetProperty(ref this._scale, value); }
        }

        public bool IsEnabledScale
        {
            get { return _dataType == ItemDataType.Decimal; }
        }

        public string RangeMin
        {
            get { return _rangeMin; }
            set { SetProperty(ref this._rangeMin, value); }
        }

        public bool IsEnabledRangeMin
        {
            get { return _dataType == ItemDataType.Decimal ||
                    _dataType == ItemDataType.Int ||
                    _dataType == ItemDataType.Long; }
        }

        public string RangeMax
        {
            get { return _rangeMax; }
            set { SetProperty(ref this._rangeMax, value); }
        }

        public bool IsEnabledRangeMax
        {
            get
            {
                return _dataType == ItemDataType.Decimal ||
                  _dataType == ItemDataType.Int ||
                  _dataType == ItemDataType.Long;
            }
        }

        public string RegularExpression
        {
            get { return _regularExpression; }
            set { SetProperty(ref this._regularExpression, value); }
        }

        public bool IsEnabledRegularExpression
        {
            get { return _dataType == ItemDataType.Text; }
        }
    }
}
