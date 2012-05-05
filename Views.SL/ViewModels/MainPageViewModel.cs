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
            private set
            {
                if (_languages != value)
                {
                    SetPropertyValue(ref _languages, value, () => Languages);
                }
            }
        }

        private ObservableCollection<WordtypeViewModel> _wordtypes;

        public ObservableCollection<WordtypeViewModel> Wordtypes
        {
            get { return _wordtypes; }
            private set
            {
                if (_wordtypes != value)
                {
                    SetPropertyValue(ref _wordtypes, value, () => Wordtypes);
                }
            }
        }

        private ObservableCollection<BasewordViewModel> _basewords;
 
        public ObservableCollection<BasewordViewModel> Basewords
        {
            get { return _basewords; }
            set { SetPropertyValue(ref _basewords, value, () => Basewords);}
        }

        private ObservableCollection<BasewordViewModel> _searchedBasewords;

        public ObservableCollection<BasewordViewModel> SearchedBasewords
        {
            get { return _searchedBasewords; }
            set { SetPropertyValue(ref _searchedBasewords, value, () => SearchedBasewords); }
        }

        private LanguageViewModel _selectedLanguage;

        public LanguageViewModel SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (value != _selectedLanguage)
                {
                    SetPropertyValue(ref _selectedLanguage, value, () => SelectedLanguage);
                    RefreshBasewords();
                }
            }
        }

        private WordtypeViewModel _selectedWordtype;
        
        public WordtypeViewModel SelectedWordtype
        {
            get { return _selectedWordtype; }
            set
            {
                if (_selectedWordtype != value)
                {
                    SetPropertyValue(ref _selectedWordtype, value, () => SelectedWordtype);
                    RefreshBasewords();
                }
            }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    SetPropertyValue(ref _searchText, value, () => SearchText);
                    UpdateSearchedBasewords();
                }
            }
        }

        private void UpdateSearchedBasewords()
        {
            var basewordContext = new BasewordContext();
            basewordContext.Load(basewordContext.GetBasewordByTextQuery(SearchText), SearchedBasewordsLoadOpCallback,null);
        }

        private void SearchedBasewordsLoadOpCallback(LoadOperation<Baseword> loadOperation )
        {
            SearchedBasewords = new ObservableCollection<BasewordViewModel>();
            foreach (var entity in loadOperation.AllEntities)
            {
               SearchedBasewords.Add(new BasewordViewModel(entity));
            }
        }

        public MainPageViewModel()
        {
            var languageContext = new LanguageContext();
            languageContext.Load(languageContext.GetLanguagesQuery(), LoadOpLanguageCallback, null);
            var wordtypeContext = new WordtypeContext();
            wordtypeContext.Load(wordtypeContext.GetWordtypesQuery(), LoadOpWordtypeCallback, null);
            Basewords = new ObservableCollection<BasewordViewModel>();
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

        private void RefreshBasewords()
        {
            if (SelectedLanguage != null && SelectedWordtype != null)
            {
                var basewordContext = new BasewordContext();
                basewordContext.Load(
                    basewordContext.GetBasewordsByLanguageIdAndWordtypeIdQuery(SelectedLanguage.Id, SelectedWordtype.Id),
                    BasewordLoadOpCallback, null);
            }
        }

        private void BasewordLoadOpCallback(LoadOperation<Baseword> loadOperation )
        {
            Basewords = new ObservableCollection<BasewordViewModel>();
            var loadedBasewords = loadOperation.Entities;
            foreach (var loadedBaseword in loadedBasewords)
            {
                Basewords.Add(new BasewordViewModel(loadedBaseword));
            }
        }
    }
}
