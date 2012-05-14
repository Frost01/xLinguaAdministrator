using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.EF;

namespace ViewModels
{
    public class BasewordViewModel : BaseViewModel
    {
        private int _id;
        private string _text;
        private Enums.Language _language;
        private WordtypeViewModel _wordtype;
        private string _comment;

        public int Id
        {
            get { return _id; }
            private set
            {
                SetPropertyValue(ref _id, value, () => Id);
            }
        }

        public string Text
        {
            get { return _text; }
            set {
                SetPropertyValue(ref _text, value, () => Text);
            }
        }

        public Enums.Language Language
        {
            get { return _language; }
            set
            {
                SetPropertyValue(ref _language, value, () => Language);
            }
        }

        public WordtypeViewModel Wordtype
        {
            get { return _wordtype; }
            set { SetPropertyValue(ref _wordtype, value, () => Wordtype);}
        }

        public string Comment
        {
            get { return _comment; }
            set { SetPropertyValue(ref _comment, value, ()=> Comment);}
        }

        public BasewordViewModel(Baseword baseword)
        {
            Id = baseword.Id;
            Text = baseword.Text;
            Language = (Enums.Language) baseword.LanguageId;
            Wordtype = new WordtypeViewModel(baseword.Wordtype);
            Comment = baseword.Comment;
        }
    }
}
