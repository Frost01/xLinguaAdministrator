using System;
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
using Models.EF;
using Views.SL.Web.xLinguaService;

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

        public BasewordViewModel(Baseword baseword)
        {
            Id = baseword.Id;
            Text = baseword.Text;
            Language = ApplicationViewModel.Instance.GetLanguageById(baseword.LanguageId);
            Wordtype = ApplicationViewModel.Instance.GetWordtypeById(baseword.WordtypeId);
            Comment = baseword.Comment;
            IsLocked = baseword.IsLocked.GetValueOrDefault();
        }

        public BasewordViewModel(Entity baseword):this(baseword as Baseword){}


        internal void Update()
        {
            var context = new BasewordContext();
            var baseword = (from b in context.Basewords
                            where b.Id == Id
                            select b).SingleOrDefault();
            if (baseword != null)
            {
                baseword.Text = this.Text;
                baseword.LanguageId = this.Language.Id;
                baseword.WordtypeId = this.Wordtype.Id;
                baseword.Comment = this.Comment;
                baseword.IsLocked = this.IsLocked;
                context.SubmitChanges();
            }

        }
    }
}
