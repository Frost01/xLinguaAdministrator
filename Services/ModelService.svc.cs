using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using DTO;
using Models.EF;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ModelService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ModelService : IModelService
    {
        readonly xLingua_StagingEntities _context = new xLingua_StagingEntities();

        public IList<BasewordDto> GetBasewordsByText(string text)
        {
            var resultList = new List<BasewordDto>();
            var basewords = _context.Basewords1.Include("Language").Where(b => b.Text.StartsWith(text)).Take(20);
            foreach (Baseword baseword in basewords)
            {
                var languageDto = new LanguageDto { Id = baseword.LanguageId, Text = baseword.Language.EnglishName };
                var basewordDto = new BasewordDto {Id = baseword.Id, Text = baseword.Text, Language = languageDto};
                resultList.Add(basewordDto);
            }
            return resultList;
        }
    }
}
