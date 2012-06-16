using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DTO
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
