using System.Xml.Serialization;

namespace OriginAPI.Models
{
    [XmlRoot(ElementName = "avatar")]
    public class Avatar
    {
        [XmlElement(ElementName = "avatarId")]
        public string AvatarId { get; set; }
        [XmlElement(ElementName = "orderNumber")]
        public string OrderNumber { get; set; }
        [XmlElement(ElementName = "isRecent")]
        public string IsRecent { get; set; }
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        [XmlElement(ElementName = "typeId")]
        public string TypeId { get; set; }
        [XmlElement(ElementName = "typeName")]
        public string TypeName { get; set; }
        [XmlElement(ElementName = "statusId")]
        public string StatusId { get; set; }
        [XmlElement(ElementName = "statusName")]
        public string StatusName { get; set; }
        [XmlElement(ElementName = "galleryId")]
        public string GalleryId { get; set; }
        [XmlElement(ElementName = "galleryName")]
        public string GalleryName { get; set; }
    }

    [XmlRoot(ElementName = "user")]
    public class User
    {
        [XmlElement(ElementName = "userId")]
        public string UserId { get; set; }
        [XmlElement(ElementName = "avatar")]
        public Avatar Avatar { get; set; }
    }

    [XmlRoot(ElementName = "users")]
    public class Users
    {
        [XmlElement(ElementName = "user")]
        public User User { get; set; }
    }

}
