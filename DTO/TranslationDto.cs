using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DTO
{
    [DataContract]
    public class TranslationDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public BasewordDto Baseword { get; set; }
    }
}
