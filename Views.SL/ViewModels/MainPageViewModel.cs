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
using Views.SL.ModelServiceReference;

namespace Views.SL.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Properties
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

        #endregion

        private void UpdateSearchedBasewords()
        {
            _client.GetBasewordsByTextOrIdAsync(SearchText);
        }

        private readonly ModelServiceClient _client;

        public MainPageViewModel()
        {
            if (!DesignerProperties.IsInDesignTool)
            {
                _client = new ModelServiceClient();
                _client.GetBasewordsByTextOrIdCompleted += GetBasewordsByTextOrIdCompleted;
                _client.GetSupportedLanguagesCompleted += GetSupportedLanguagesCompleted;
                _client.GetWordtypesCompleted += GetWordtypesCompleted;
                _client.GetSupportedLanguagesAsync();
                _client.GetWordtypesAsync();
            }
        }

        #region Callbacks
        private void GetWordtypesCompleted(object sender, GetWordtypesCompletedEventArgs e)
        {
            var wordtypes = e.Result;
            Wordtypes = new ObservableCollection<WordtypeViewModel>();
            foreach (WordtypeDto wordtypeDto in wordtypes)
            {
                Wordtypes.Add(new WordtypeViewModel(wordtypeDto));
            }
        }

        private void GetSupportedLanguagesCompleted(object sender, GetSupportedLanguagesCompletedEventArgs e)
        {
            var languages = e.Result;
            Languages = new ObservableCollection<LanguageViewModel>();
            foreach (LanguageDto languageDto in languages)
            {
                Languages.Add(new LanguageViewModel(languageDto));
            }
        }

        private void GetBasewordsByTextOrIdCompleted(object sender, GetBasewordsByTextOrIdCompletedEventArgs e)
        {
            var basewordDtos = e.Result;
            SearchedBasewords = new ObservableCollection<BasewordViewModel>();
            foreach (BasewordDto basewordDto in basewordDtos)
            {
                SearchedBasewords.Add(new BasewordViewModel(basewordDto));
            }
        }
        #endregion

    }
}
