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

        private string _translationServiceText;

        public string TranslationServiceText
        {
            get { return _translationServiceText; }
            private set { SetPropertyValue(ref _translationServiceText, value, () => TranslationServiceText);}
        }

        private ObservableCollection<BasewordViewModel> _translations;
 
        public ObservableCollection<BasewordViewModel> Translations
        {
            get { return _translations; }
            private set { SetPropertyValue(ref _translations, value, () => Translations);}
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
                    DeleteBasewordCommand.UpdateCanExecute();
                    if (value!=null) UpdateTranslations();
                    ServiceMessage = string.Empty;
                }
            }
        }
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

        private string _serviceMessage;

        public string ServiceMessage
        {
            get { return _serviceMessage; }
            set { SetPropertyValue(ref _serviceMessage, value, () => ServiceMessage); }
        }

        #endregion

        private void UpdateSearchedBasewords()
        {
            _client.GetBasewordsByTextOrIdAsync(SearchText);
        }

        private void UpdateTranslations()
        {
            Translations = new ObservableCollection<BasewordViewModel>();
            TranslationServiceText = "Lade Übersetzungen ...";
            _client.GetTranslationsFromBasewordAsync(SelectedBaseword.CopyToDto());
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
                _client.UpdateBasewordCompleted += UpdateBasewordCompleted;
                _client.GetSupportedLanguagesAsync();
                _client.GetWordtypesAsync();
                _client.DeleteBasewordCompleted += DeleteBasewordCompleted;
                _client.GetTranslationsFromBasewordCompleted += GetTranslationsFromBasewordCompleted;
                _updateBasewordCommand = new RelayCommand(param => this.UpdateBaseword(),param => this.CanUpdateBaseword());
                _deleteBasewordCommand = new RelayCommand(param => this.DeleteBaseword(), param => this.CanDeleteBaseword());
            }
        }



        #region Callbacks
        private void GetTranslationsFromBasewordCompleted(object sender, GetTranslationsFromBasewordCompletedEventArgs e)
        {
            foreach (var basewordDto in e.Result)
            {
                Translations.Add(new BasewordViewModel(basewordDto));
            }
            TranslationServiceText = "Laden der Übersetzungen beendet.";
        }

        private void DeleteBasewordCompleted(object sender, DeleteBasewordCompletedEventArgs e)
        {
            ServiceMessage = e.Result ? "Baseword erfolgreich gelöscht!" : "Löschen des Basewords fehlgeschlagen";
        }

        private void UpdateBasewordCompleted(object sender, UpdateBasewordCompletedEventArgs e)
        {
            ServiceMessage = e.Result ? "Baseword erfolgreich aktualisiert" : "Aktualisierung des Basewords fehlgeschlagen";
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

        #region Commands
        private readonly RelayCommand _updateBasewordCommand;

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

        private readonly RelayCommand _deleteBasewordCommand;

        public RelayCommand DeleteBasewordCommand
        {
            get { return _deleteBasewordCommand; }
        }

        private void DeleteBaseword()
        {
            var result = MessageBox.Show("Hiermit wird das Baseword mit allen Referenzen aus der Datenbank gelöscht! Fortfahren?",
                            "Achtung!", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                _client.DeleteBasewordAsync(SelectedBaseword.CopyToDto());
                SelectedBaseword = null;
                SearchText = string.Empty;
            }
        }

        private bool CanDeleteBaseword()
        {
            return SelectedBaseword != null;
        }
        #endregion
    }
}
