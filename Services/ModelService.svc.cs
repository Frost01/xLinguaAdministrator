using System;
using System.Collections.Generic;
using System.Data.Objects;
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

        public IList<BasewordDto> GetBasewordsByTextOrId(string text)
        {
            int id;
            IQueryable<Baseword> basewords;
            basewords = Int32.TryParse(text, out id) ? 
                _context.Basewords1.Include("Language").Include("Wordtype").Where(b => b.Id == id).Take(20) : 
                _context.Basewords1.Include("Language").Include("Wordtype").Where(b => b.Text.StartsWith(text)).Take(20);
            var resultList = new List<BasewordDto>();
            foreach (Baseword baseword in basewords)
            {
                var languageDto = new LanguageDto {Id = baseword.LanguageId, Text = baseword.Language.EnglishName };
                var wordtypeDto = new WordtypeDto {Id = baseword.WordtypeId, Text = baseword.Wordtype.Text};
                var basewordDto = new BasewordDto {Id = baseword.Id, Text = baseword.Text, Language = languageDto, Wordtype = wordtypeDto, Comment = baseword.Comment, IsLocked = baseword.IsLocked.GetValueOrDefault()};
                resultList.Add(basewordDto);
            }
            return resultList;
        }

        public IList<LanguageDto> GetSupportedLanguages()
        {
            var resultList = new List<LanguageDto>();
            foreach (Language language in _context.Languages1)
            {
                var languageDto = new LanguageDto();
                languageDto.Id = language.Id;
                languageDto.Text = language.EnglishName;
                resultList.Add(languageDto);
            }
            return resultList;
        }

        public IList<WordtypeDto> GetWordtypes()
        {
            var resultList = new List<WordtypeDto>();
            foreach (Wordtype wordtype in _context.Wordtypes)
            {
                var wordtypeDto = new WordtypeDto();
                wordtypeDto.Id = wordtype.Id;
                wordtypeDto.Text = wordtype.Text;
                resultList.Add(wordtypeDto);
            }
            return resultList;
        }

        public bool UpdateBaseword(BasewordDto basewordDto)
        {
            var baseword = _context.Basewords1.FirstOrDefault(b => b.Id == basewordDto.Id);
            if (baseword != null)
            {
                baseword.Text = basewordDto.Text;
                baseword.LanguageId = basewordDto.Language.Id;
                baseword.WordtypeId = basewordDto.Wordtype.Id;
                baseword.Comment = basewordDto.Comment;
                baseword.IsLocked = basewordDto.IsLocked;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteBaseword(BasewordDto basewordDto)
        {
            var baseword = _context.Basewords1.FirstOrDefault(b => b.Id == basewordDto.Id);
            if (baseword != null)
            {
                var translations =
                    _context.Translations1.Where(t => t.BasewordFromId == baseword.Id || t.BasewordToId == baseword.Id);
                foreach (var translation in translations)
                {
                    _context.DeleteObject(translation);
                }
                var connections =
                    _context.Connections1.Where(c => c.BasewordId == baseword.Id);
                foreach (var connection in connections)
                {
                    _context.DeleteObject(connection);
                }
                _context.DeleteObject(baseword);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
