using System;
using System.Net;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
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
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private readonly RelayCommand _validateUserCommand;
        private ModelServiceClient _client;

        public string Email
        {
            get { return _email; }
            set { SetPropertyValue(ref _email, value, () => Email);}
        }

        public string Password
        {
            get { return _password; }
            set { SetPropertyValue(ref _password, value, () => Password);}
        }

        public RelayCommand ValidateUserCommand
        {
            get { return _validateUserCommand; }
        }
        
        public LoginViewModel()
        {
            _client = new ModelServiceClient();
            _validateUserCommand = new RelayCommand(param=> ValidateUser(), param=> CanValidateUser());
            _client.ValidateUserCompleted += ValidateUserCallback;
        }

        private void ValidateUserCallback(object sender, ValidateUserCompletedEventArgs e)
        {
            if (e.Result)
            {
                var formsAuthentication = new FormsAuthentication();
                formsAuthentication.Login(Email, Password);
            }
        }

        private bool CanValidateUser()
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ValidateUser()
        {
            _client.ValidateUserAsync(Email, Password);
        }


    }
}
