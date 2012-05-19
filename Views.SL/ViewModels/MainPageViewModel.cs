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
using Views.SL.ModelService;

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
            _client.GetBasewordsByTextAsync(SearchText);
        }

        private ModelServiceClient _client;

        public MainPageViewModel()
        {
            if (!DesignerProperties.IsInDesignTool)
            {
                _client = new ModelServiceClient();
                _client.GetBasewordsByTextCompleted += new EventHandler<GetBasewordsByTextCompletedEventArgs>(GetBasewordsByTextCompleted);
                _client.GetSupportedLanguagesCompleted += GetSupportedLanguagesCompleted;
                _client.GetWordtypesCompleted += GetWordtypesCompleted;
                _client.GetSupportedLanguagesAsync();
                _client.GetWordtypesAsync();
                _updateBasewordCommand = new RelayCommand(param => this.UpdateBaseword(),param => this.CanUpdateBaseword());
            }
        }

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

        void GetBasewordsByTextCompleted(object sender, GetBasewordsByTextCompletedEventArgs e)
        {
            var basewordDtos = e.Result;
            SearchedBasewords = new ObservableCollection<BasewordViewModel>();
            foreach (BasewordDto basewordDto in basewordDtos)
            {
                SearchedBasewords.Add(new BasewordViewModel(basewordDto));
            }
        }


        private RelayCommand _updateBasewordCommand;

        public RelayCommand UpdateBasewordCommand
        {
            get { return _updateBasewordCommand; }
        }

        private void UpdateBaseword()
        {
            _client.UpdateBasewordAsync(SelectedBaseword.CopyToDto());
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
                    UpdateBasewordCommand.UpdateCanExecute();
                }
            }
        }

    }
}
