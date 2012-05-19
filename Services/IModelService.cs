using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using DTO;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IModelService" in both code and config file together.
    [ServiceContract]
    public interface IModelService
    {
        [OperationContract]
        IList<BasewordDto> GetBasewordsByText(string text);

        [OperationContract]
        IList<LanguageDto> GetSupportedLanguages();

        [OperationContract]
        IList<WordtypeDto> GetWordtypes();

        [OperationContract]
        bool UpdateBaseword(BasewordDto baseword);
    }
}
