using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.EF;

namespace ViewModels
{
    public class WordtypeViewModel : BaseViewModel
    {
        private int _id;
        private string _text;

        public int Id
        {
            get { return _id; }
            set { SetPropertyValue(ref _id, value, () => Id);}
        }

        public string Text
        {
            get { return _text; }
            set { SetPropertyValue(ref _text, value, () => Text); }
        }

        public WordtypeViewModel(Wordtype wordtype)
        {
            Id = wordtype.Id;
            Text = wordtype.Text;
        }
    }
}
