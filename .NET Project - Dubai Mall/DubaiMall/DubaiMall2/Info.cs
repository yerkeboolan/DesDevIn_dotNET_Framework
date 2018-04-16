using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubaiMall2
{
    public class JsonInfo
    { 
        public JsonClass[] Property1 { get; set; }
    }

    public class JsonClass
    {
        public int id { get; set; }
        public Metadata[] metaData { get; set; }
        public string encryptedId { get; set; }
        public int state { get; set; }
        public int flags { get; set; }
        public string creationDate { get; set; }
        public string updateDate { get; set; }
        public string unitNumber { get; set; }
        public string level { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string logo { get; set; }
        public string description { get; set; }
        public object googleMiniUrl { get; set; }
        public string editorial { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Video video { get; set; }
        public Category[] categories { get; set; }
        public Subcategory[] subCategories { get; set; }
        public Tag[] tags { get; set; }
        public bool giftCard { get; set; }
        public Location location { get; set; }
        public int floorId { get; set; }
        public int?[] closestParking { get; set; }
        public string serviceTitle { get; set; }
        public string serviceDescription { get; set; }
        public int? serviceCtaType { get; set; }
        public string serviceCta { get; set; }
        public string elevatedPic { get; set; }
        public string elevatedPic1 { get; set; }
        public string elevatedPic2 { get; set; }
        public string elevatedPic3 { get; set; }
        public string elevatedPicAlt { get; set; }
        public string mainPic { get; set; }
        public string mainPicAlt { get; set; }
        public string plus1Pic { get; set; }
        public string plus1PicAlt { get; set; }
        public string plus2Pic { get; set; }
        public string plus2PicAlt { get; set; }
        public string plus3Pic { get; set; }
        public string plus3PicAlt { get; set; }
        public bool draft { get; set; }
        public bool deleted { get; set; }
        public int displayOrder { get; set; }
        public Point P { get; set; }
    }

    public class Video
    {
        public string title { get; set; }
        public string preview { get; set; }
        public string videoAlt { get; set; }
        public string link { get; set; }
    }

    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Metadata
    {
        public int id { get; set; }
        public string tagType { get; set; }
        public string name { get; set; }
        public string content { get; set; }
    }

    public partial class Category
    {
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public bool discovery { get; set; }
        public string slug { get; set; }
    }

    public class Subcategory
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public bool deleted { get; set; }
    }
}
