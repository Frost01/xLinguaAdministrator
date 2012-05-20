using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class BasewordViewModel : BaseViewModel
    {
        private int _id;
        private string _text;
        private LanguageViewModel _language;
        private WordtypeViewModel _wordtype;
        private string _comment;
        private bool _isLocked;
        private int _status;
        private string _serviceMessage;
        private readonly ModelServiceClient _client;
        private ObservableCollection<TranslationViewModel> _translations; 

        public int Id
        {
            get { return _id; }
            private set {SetPropertyValue(ref _id, value, () => Id);}
        }

        public string Text
        {
            get { return _text; }
            set { SetPropertyValue(ref _text,value, () => Text);}
        }

        public LanguageViewModel Language
        {
            get { return _language; }
            set { SetPropertyValue(ref _language,value, () => Language);}
        }

        public WordtypeViewModel Wordtype
        {
            get { return _wordtype; }
            set { SetPropertyValue(ref _wordtype,value, () => Wordtype);}
        }

        public string Comment
        {
            get { return _comment; }
            set { SetPropertyValue(ref _comment, value, () => Comment); }
        }

        public bool IsLocked
        {
            get { return _isLocked; }
            set { SetPropertyValue(ref _isLocked,value, () => IsLocked);}
        }

        public int Status
        {
            get { return _status; }
            set { SetPropertyValue(ref _status, value, () => Status);}
        }

        public string ServiceMessage
        {
            get { return _serviceMessage; }
            set { SetPropertyValue(ref _serviceMessage, value, () => ServiceMessage);}
        }

        public ObservableCollection<TranslationViewModel> Translations
        {
            get { return _translations; }
            private set { SetPropertyValue(ref _translations, value, () => Translations);}
        } 

        public BasewordViewModel(BasewordDto basewordDto)
        {
            Id = basewordDto.Id;
            Text = basewordDto.Text;
            Language = new LanguageViewModel(basewordDto.Language);
            Wordtype = new WordtypeViewModel(basewordDto.Wordtype);
            Comment = basewordDto.Comment;
            IsLocked = basewordDto.IsLocked;
            Translations = GetTranslations(basewordDto);
            _client = new ModelServiceClient();
            _client.UpdateBasewordCompleted += UpdateBasewordCallback;
            _client.DeleteBasewordCompleted += DeleteBasewordCallback;
            _updateBasewordCommand = new RelayCommand(param=>UpdateBaseword());
            _deleteBasewordCommand = new RelayCommand(param => this.DeleteBaseword());

        }

        private ObservableCollection<TranslationViewModel> GetTranslations(BasewordDto basewordDto)
        {
            var result = new ObservableCollection<TranslationViewModel>();
            if (basewordDto.Translations!= null)
                foreach (var translationDto in basewordDto.Translations)
                {
                    result.Add(new TranslationViewModel(this, translationDto));
                }
            return result;
        }

        private void DeleteBasewordCallback(object sender, DeleteBasewordCompletedEventArgs e)
        {
            ServiceMessage = e.Result ? string.Format("Baseword {0} mit id: {1} erfolgreich gelöscht!", this.Text, this.Id) : string.Format("Löschen des Basewords {0} mit id: {1} fehlgeschlagen", this.Text, this.Id);
        }

        private void UpdateBasewordCallback(object sender, UpdateBasewordCompletedEventArgs e)
        {
            ServiceMessage = string.Format(e.Result ?string.Format("Baseword {0} mit id: {1} efolgreich aktualisiert",this.Text,this.Id) : string.Format("Aktualisierung von Baseword {0} mit id: {1} fehlgeschlagen", this.Text, this.Id));
        }

        public BasewordDto CopyToDto()
        {
            var basewordDto = new BasewordDto
            {
                Id = this.Id,
                Text = this.Text,
                Language = new LanguageDto
                {
                    Id = this.Language.Id,
                    Text = this.Language.Text
                },
                Wordtype = new WordtypeDto
                {
                    Id = this.Wordtype.Id,
                    Text = this.Wordtype.Text
                },
                Comment = this.Comment,
                IsLocked = this.IsLocked
            };
            return basewordDto;
        }

        private readonly RelayCommand _updateBasewordCommand;

        public RelayCommand UpdateBasewordCommand
        {
            get { return _updateBasewordCommand; }
        }

        private void UpdateBaseword()
        {
            ServiceMessage = "Aktualisiere Baseword";
            _client.UpdateBasewordAsync(CopyToDto());
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
                _client.DeleteBasewordAsync(CopyToDto());
                //SelectedBaseword = null;
                //SearchText = string.Empty;
            }
        }
    }
}
