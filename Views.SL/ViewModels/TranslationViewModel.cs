using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Views.SL.ModelServiceReference;

namespace Views.SL.ViewModels
{
    public class TranslationViewModel : BaseViewModel
    {
        private int _id;
        private BasewordViewModel _basewordSource;
        private BasewordViewModel _baseword;

        public int Id { get { return _id; } private set {SetPropertyValue(ref _id, value, () => Id);} }
        public BasewordViewModel BasewordSource { get { return _basewordSource; } }
        public BasewordViewModel Baseword { get { return _baseword; } set { SetPropertyValue( ref _baseword, value, () => Baseword);}}

        public TranslationViewModel(BasewordViewModel basewordSource, TranslationDto translationDto)
        {
            _basewordSource = basewordSource;
            Id = translationDto.Id;
            Baseword = new BasewordViewModel(translationDto.Baseword);
        }
    }
}
