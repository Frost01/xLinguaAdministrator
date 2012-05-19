using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DTO
{
    [DataContract]
    public class LanguageDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
