using System;
namespace UrlShorter.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string LongLink { get; set; }
        public string ShortLink { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Link(string longLink,string shortLink)
        {
            LongLink = longLink;
            ShortLink = shortLink;
            CreationDateTime = DateTime.Now;
        }
    }
}
