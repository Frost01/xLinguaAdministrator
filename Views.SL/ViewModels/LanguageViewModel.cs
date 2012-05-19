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
using Views.SL.ModelService;

namespace Views.SL.ViewModels
{
    public class LanguageViewModel : BaseViewModel
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetPropertyValue(ref _id, value, () =>Id);}
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set {SetPropertyValue(ref _text, value, () => Text);}
        }

        public LanguageViewModel(LanguageDto languageDto)
        {
            Id = languageDto.Id;
            Text = languageDto.Text;
        }

    } 
}
