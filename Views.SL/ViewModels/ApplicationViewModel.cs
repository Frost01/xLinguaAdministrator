using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Collections.Generic;

namespace Views.SL.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        private IList<LanguageViewModel> _languages;
        private IList<WordtypeViewModel> _wordtypes;

        public IList<LanguageViewModel> Languages
        {
            get {return _languages;}
            private set { _languages = value; }
        }

        public IList<WordtypeViewModel> Wordtypes
        {
            get { return _wordtypes; }
            private set { _wordtypes = value;}
        } 

        public LanguageViewModel GetLanguageById(int id)
        {
            return Languages.FirstOrDefault(l => l.Id == id);
        }

        public WordtypeViewModel GetWordtypeById(int id)
        {
            return Wordtypes.FirstOrDefault(w => w.Id == id);
        }

        public ApplicationViewModel()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            if (!DesignerProperties.IsInDesignTool)
            {

            }
        }

        public static ApplicationViewModel Instance { get; private set; }




    }
}
