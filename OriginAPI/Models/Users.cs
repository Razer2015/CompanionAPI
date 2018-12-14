using System.Collections.Generic;
using System.Xml.Serialization;

namespace OriginAPI.Models
{
    [XmlRoot(ElementName = "user")]
    public class User
    {
        [XmlElement(ElementName = "userId")]
        public string UserId { get; set; }
        [XmlElement(ElementName = "personaId")]
        public string PersonaId { get; set; }
        [XmlElement(ElementName = "EAID")]
        public string EAID { get; set; }
    }

    [XmlRoot(ElementName = "users")]
    public class AtomUsers
    {
        [XmlElement(ElementName = "user")]
        public List<User> Users { get; set; }
    }
}
