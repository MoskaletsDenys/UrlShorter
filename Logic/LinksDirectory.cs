using System.Collections.Generic;
using System.Linq;
using UrlShorter.Models;

namespace UrlShorter.Logic
{
    public class LinksDirectory
    {
        private const int DeleteIfMoreThan = 5;
        private const int DeleteCount = 3;
        private readonly LinksContext _linksContext;

        public LinksDirectory(LinksContext context)
        {
            _linksContext = context;
        }

        public string SaveOrReturnExisting(string longLink,string shortLink)
        {
            if(LongExist(longLink))
            {
                return GetShortLinkByLong(longLink);
            }
            else
            {
                _linksContext.Links.Add(new Link(longLink,shortLink));            
                _linksContext.SaveChanges();            
                if (_linksContext.Links.Count() > DeleteIfMoreThan)
                    RemoveEarlyRecords(DeleteCount);
                return shortLink;
            }
        }

        public List<Link> GetAllLinks()
        {
            return _linksContext.Links.ToList();
        }

        public string GetLongLinkByShort(string shortLink)
        {
            return _linksContext.Links.FirstOrDefault(link => link.ShortLink == shortLink).LongLink;
        }
        
        public string GetShortLinkByLong(string longLink)
        {
            return _linksContext.Links.FirstOrDefault(link => link.LongLink == longLink).ShortLink;
        }

        public bool ShortExist(string shortLink)
        {
            if (_linksContext.Links.FirstOrDefault(link => link.ShortLink == shortLink) != null)
                return true;
            else
                return false;
        }

        public bool LongExist(string longLink)
        {
            if (_linksContext.Links.FirstOrDefault(link => link.LongLink == longLink) != null)
                return true;
            else
                return false;
        }

        public int GetLastId()
        {
            var link = _linksContext.Links.OrderByDescending(x => x.Id).FirstOrDefault();
            if (link == null)
                return 0;
            else
                return link.Id;
        }

        public void DeleteAllLinks()
        {
            _linksContext.RemoveRange(_linksContext.Links);
            _linksContext.SaveChanges();
        }
        public void RemoveEarlyRecords(int count)
        {
            foreach(var link in _linksContext.Links)
            {
                if (count > 0)
                {
                    _linksContext.Links.Remove(link);
                    count--;
                }
                else
                    break;
            }
            _linksContext.SaveChanges();
        }
    }
}
