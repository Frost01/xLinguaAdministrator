﻿using System;
using System.Net;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Models.EF;

namespace Views.SL.ViewModels
{
    public class BasewordViewModel : BaseViewModel
    {
        private int _id;
        private string _text;
        private LanguageViewModel _language;
        private WordtypeViewModel _wordtype;

        public int Id
        {
            get { return _id; }
            private set {SetPropertyValue(ref _id, value, () => Id);}
        }

        public string Text
        {
            get { return _text; }
            set {SetPropertyValue(ref _text,value, () => Text);}
        }

        public LanguageViewModel Language
        {
            get { return _language; }
            set {SetPropertyValue(ref _language,value, () => Language);}
        }

        public WordtypeViewModel Wordtype
        {
            get { return _wordtype; }
            set { SetPropertyValue(ref _wordtype,value, () => Wordtype);}
        }

        public BasewordViewModel(Baseword baseword)
        {
            Id = baseword.Id;
            Text = baseword.Text;
        }

        public BasewordViewModel(Entity baseword):this(baseword as Baseword){}

    }
}
