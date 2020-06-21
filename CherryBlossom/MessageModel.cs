using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CherryBlossom
{
    public class MessageModel : ValidatableBase
    {
        private bool _selected = false;
        private string _messageId = string.Empty;
        private string _message = string.Empty;
        private string _remark = string.Empty;
        private uint _placeholders = 0;

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        [Required]
        [RegularExpression(@"^[EWIQ][0-9]{3}$", ErrorMessage = "接頭辞にEWIQ、続く連番に4桁を指定" )]
        public string MessageId
        {
            get { return _messageId; }
            set { SetProperty(ref _messageId, value); }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                SetProperty(ref _message, value);
                var reg = new System.Text.RegularExpressions.Regex(@"\{\w*\}");
                Placeholders = (uint)reg.Matches(_message).Count();
            }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public uint Placeholders
        {
            get { return _placeholders; }
            set { SetProperty(ref _placeholders, value); }
        }
    }
}
