using System.Collections.Generic;
using System.Xml.Serialization;

namespace OriginAPI.Models
{
    [XmlRoot(ElementName = "user")]
    public class AtomUser
    {
        [XmlElement(ElementName = "userId")]
        public string UserId { get; set; }
        [XmlElement(ElementName = "personaId")]
        public string PersonaId { get; set; }
        [XmlElement(ElementName = "EAID")]
        public string EAID { get; set; }
        public string Avatar { get; set; }
    }

    [XmlRoot(ElementName = "users")]
    public class AtomUsers
    {
        [XmlElement(ElementName = "user")]
        public List<AtomUser> Users { get; set; }
    }
}
