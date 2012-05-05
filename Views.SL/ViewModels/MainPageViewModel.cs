using System;
using System.Collections.ObjectModel;
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
using Views.SL.Web.xLinguaService;

namespace Views.SL.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<LanguageViewModel> _languages;

        public ObservableCollection<LanguageViewModel> Languages
        {
            get { return _languages; }
            private set {SetPropertyValue(ref _languages, value, () => Languages);}
        }

        private ObservableCollection<WordtypeViewModel> _wordtypes;

        public ObservableCollection<WordtypeViewModel> Wordtypes
        {
            get { return _wordtypes; }
            private set {SetPropertyValue(ref _wordtypes, value, () => Wordtypes);}
        } 

        public MainPageViewModel()
        {
            var languageContext = new LanguageContext();
            languageContext.Load(languageContext.GetLanguagesQuery(), LoadOpLanguageCallback, null);
            var wordtypeContext = new WordtypeContext();
            wordtypeContext.Load(wordtypeContext.GetWordtypesQuery(), LoadOpWordtypeCallback, null);
        }

        private void LoadOpLanguageCallback(LoadOperation<Language> loadOperation)
        {
            Languages = new ObservableCollection<LanguageViewModel>();
            var loadedLanguages = loadOperation.AllEntities;
            foreach (var language in loadedLanguages)
            {
                Languages.Add(new LanguageViewModel(language));
            }
        }

        private void LoadOpWordtypeCallback(LoadOperation<Wordtype> loadOperation )
        {
            Wordtypes = new ObservableCollection<WordtypeViewModel>();
            var loadedWordtypes = loadOperation.AllEntities;
            foreach (var wordtype in loadedWordtypes)
            {
                Wordtypes.Add(new WordtypeViewModel(wordtype));
            }
        }
    }
}
