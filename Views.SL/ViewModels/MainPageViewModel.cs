using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private ObservableCollection<BasewordViewModel> _searchedBasewords;

        public ObservableCollection<BasewordViewModel> SearchedBasewords
        {
            get { return _searchedBasewords; }
            set { SetPropertyValue(ref _searchedBasewords, value, () => SearchedBasewords); }
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
            if (!DesignerProperties.IsInDesignTool)
            {
                var languageContext = new LanguageContext();
                languageContext.Load(languageContext.GetLanguagesQuery(), LoadOpLanguageCallback, null);
                var wordtypeContext = new WordtypeContext();
                wordtypeContext.Load(wordtypeContext.GetWordtypesQuery(), LoadOpWordtypeCallback, null);
                _updateBasewordCommand = new RelayCommand(param => this.UpdateBaseword(),param => this.CanUpdateBaseword(), this);
            }
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

        private RelayCommand _updateBasewordCommand;

        public RelayCommand UpdateBasewordCommand
        {
            get { return _updateBasewordCommand; }
        }

        private void UpdateBaseword()
        {
            
        }

        private bool CanUpdateBaseword()
        {
            return SelectedBaseword != null;
        }

        private BasewordViewModel _selectedBaseword;

        public BasewordViewModel SelectedBaseword
        {
            get { return _selectedBaseword; }
            set
            {
                if (_selectedBaseword != value)
                {
                    SetPropertyValue(ref _selectedBaseword, value, () => SelectedBaseword);
                    
                }
            }
        }

    }
}
