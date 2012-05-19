using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DTO
{
    [DataContract]
    public class BasewordDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public LanguageDto Language { get; set; }
        [DataMember]
        public WordtypeDto Wordtype { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public bool IsLocked { get; set; }
    }
}
