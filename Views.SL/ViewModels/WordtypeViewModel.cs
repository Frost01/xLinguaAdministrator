using System;
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
    public class WordtypeViewModel : BaseViewModel
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            private set {SetPropertyValue(ref _id, value, () => Id);}
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set {SetPropertyValue(ref _text, value, () => Text);}
        }

        public WordtypeViewModel(Wordtype wordtype)
        {
            Id = wordtype.Id;
            Text = wordtype.Text;
        }

        public WordtypeViewModel(Entity wordtype):this(wordtype as Wordtype)
        {
            
        }
    }
}
