using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace CherryBlossom
{
    public class MorphModel : ValidatableBase
    {
        private bool _selected = false;
        private string _morph = string.Empty;
        private string _alpha = string.Empty;

        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }
        [Required]
        [RegularExpression(@"^([\u4e00-\u9fcf\uF900-\uFAFF\u3400-\u4DBF]|[\p{IsHiragana}]|[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F])*$", ErrorMessage = "全角ひらがな、全角カタカナ、漢字が入力できます。")]
        public string Morph
        {
            get { return _morph; }
            set { SetProperty(ref _morph, value.Trim()); }
        }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]{1,20}$", ErrorMessage = "半角英数字が入力できます。")]
        public string Alpha
        {
            get { return _alpha; }
            set { SetProperty(ref _alpha, value.Trim()); }
        }
    }
}
