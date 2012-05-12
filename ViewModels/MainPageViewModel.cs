using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
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
        
        public MainPageViewModel()
        {
            //UpdateBasewords();
            Wordtypes = new ObservableCollection<WordtypeViewModel>();
            using (var context = new xLingua_StagingEntities())
            {
                var wordtypes = from w in context.Wordtypes
                                orderby w.Text
                                select w;
                foreach (Wordtype wordtype in wordtypes)
                {
                    Wordtypes.Add(new WordtypeViewModel(wordtype));
                }
            }
        }

        private void UpdateBasewords()
        {
            Basewords = new ObservableCollection<BasewordViewModel>();
            using (var context = new xLingua_StagingEntities())
            {
                var basewords = (from b in context.Basewords1.Include("Wordtype")
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
        }
    }
}
