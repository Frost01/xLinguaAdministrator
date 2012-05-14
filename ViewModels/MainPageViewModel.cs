using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Models.EF;

namespace ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<BasewordViewModel> _basewords;
        private string _searchText;
        private ObservableCollection<WordtypeViewModel> _wordtypes;
        private WordtypeViewModel _selectedWordtype;
        private Enums.Language _selectedLanguage;
        private RelayCommand _updateBasewordCommand;
        private BasewordViewModel _selectedBaseword;
        private xLingua_StagingEntities _context;


        public ObservableCollection<BasewordViewModel> Basewords
        {
            get { return _basewords; }
            set
            {
                SetPropertyValue(ref _basewords, value, () => Basewords);
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SearchText != value)
                {
                    SetPropertyValue(ref _searchText, value, () => SearchText);
                    UpdateBasewords();
                }
            }
        }

        public ObservableCollection<WordtypeViewModel> Wordtypes
        {
            get { return _wordtypes; }
            set { SetPropertyValue(ref _wordtypes, value, () => Wordtypes);}
        } 

        public WordtypeViewModel SelectedWordtype
        {
            get { return _selectedWordtype; }
            set
            {
                if (SelectedWordtype != value)
                {
                    SetPropertyValue(ref _selectedWordtype, value, () => SelectedWordtype);
                    UpdateBasewords();
                }
            }
        }

        public Enums.Language SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (SelectedLanguage != value)
                {
                    SetPropertyValue(ref _selectedLanguage, value, () => SelectedLanguage);
                    UpdateBasewords();
                }
            }
        }
        
        public ICommand UpdateBasewordCommand
        {
            get { return _updateBasewordCommand; }
        }

        public BasewordViewModel SelectedBaseword
        {
            get { return _selectedBaseword; }
            set { SetPropertyValue(ref _selectedBaseword, value, () => SelectedBaseword);}
        }

        public MainPageViewModel()
        {
            _context = new xLingua_StagingEntities();
            Wordtypes = new ObservableCollection<WordtypeViewModel>();
            var wordtypes = from w in _context.Wordtypes
                            orderby w.Text
                            select w;
            foreach (Wordtype wordtype in wordtypes)
            {
                Wordtypes.Add(new WordtypeViewModel(wordtype));
            }
            _updateBasewordCommand = new RelayCommand(param => this.UpdateBaseword(), param => this.CanUpdateBaseword());
        }

        private void UpdateBasewords()
        {
            Basewords = new ObservableCollection<BasewordViewModel>();
            var basewords = (from b in _context.Basewords1.Include("Wordtype")
                            where b.Text.StartsWith(SearchText)
                            orderby b.Text
                            select b).Take(50);
            if (SelectedLanguage != 0) basewords = basewords.Where(b => b.LanguageId == (int) SelectedLanguage);
            if (SelectedWordtype != null) basewords = basewords.Where(b => b.Wordtype.Id == SelectedWordtype.Id);
            foreach (var baseword in basewords)
            {
                Basewords.Add(new BasewordViewModel(baseword));
            }
        }

        private void UpdateBaseword()
        {
            var basewordModel = _context.Basewords1.FirstOrDefault(b => b.Id == SelectedBaseword.Id);
            if (basewordModel != null)
            {
                basewordModel.Text = SelectedBaseword.Text;
                basewordModel.LanguageId = (int)SelectedBaseword.Language;
                basewordModel.WordtypeId = SelectedBaseword.Wordtype.Id;
                basewordModel.Comment = SelectedBaseword.Comment;
                _context.SaveChanges();
                SelectedBaseword = null;
            }
        }

        private bool CanUpdateBaseword()
        {
            return SelectedBaseword != null;
        }
    }
}
