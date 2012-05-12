using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels.Enums;

namespace ViewModels.EnumsMetadata
{
    public class LanguageMetadata
    {
        public static IList<LanguageMetadata> Data { get; private set; }
 
        static LanguageMetadata()
        {
            Data = new List<LanguageMetadata>();
            Data.Add(new LanguageMetadata{Language = Language.All, DisplayText = "Alle", BusinessValue = 0});
            Data.Add(new LanguageMetadata{Language = Language.German, DisplayText = "German", BusinessValue = 1});
            Data.Add(new LanguageMetadata{Language = Language.Polish, DisplayText = "Polish", BusinessValue = 2});
            Data.Add(new LanguageMetadata{Language = Language.English, DisplayText = "English", BusinessValue = 3});
            Data.Add(new LanguageMetadata{Language = Language.Russian, DisplayText = "Russian", BusinessValue = 4});
            Data.Add(new LanguageMetadata{Language = Language.French, DisplayText = "French",BusinessValue = 5});
            Data.Add(new LanguageMetadata{Language = Language.Wolof, DisplayText = "Wolof", BusinessValue = 6});
            Data.Add(new LanguageMetadata{Language = Language.Swahili, DisplayText = "Swahili",BusinessValue = 7});
            Data.Add(new LanguageMetadata{Language = Language.Igbo, DisplayText = "Igbo",BusinessValue = 8});
            Data.Add(new LanguageMetadata{Language = Language.Yoruba, DisplayText = "Yoruba", BusinessValue = 9});
        }

        public string DisplayText { get; private set; }

        public int BusinessValue { get; private set; }

        public Language Language { get; private set; }
    }
}
