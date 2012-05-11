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

        public MainPageViewModel()
        {
            //UpdateBasewords();
        }

        private void UpdateBasewords()
        {
            Basewords = new ObservableCollection<BasewordViewModel>();
            using (var context = new xLingua_StagingEntities())
            {
                var basewords = (from b in context.Basewords1
                                where b.Text.StartsWith(SearchText)
                                select b).Take(50);
                foreach (var baseword in basewords)
                {
                    Basewords.Add(new BasewordViewModel(baseword));
                }
            }
        }
    }
}
