﻿using System;
using System.Collections.Generic;
using UrlShorter.Models;

namespace UrlShorter.Logic
{
    public class ShorterUsage
    {

        private readonly ShorterAlgorithm _shorterAlgorithm;
        private readonly LinksDirectory _linksDirectory;

        public ShorterUsage(LinksContext context)
        {
            _linksDirectory = new LinksDirectory(context);
            _shorterAlgorithm = new ShorterAlgorithm();
        }

        public void GenerateAndSave(string longLink)
        {
            string shortLink;
            do
            {
                shortLink = _shorterAlgorithm.Encode();
            } 
            while (_linksDirectory.ShortExist(shortLink));

            Link link = new Link(longLink,shortLink);
            _linksDirectory.AddLink(link);
        }
        public string GetLongLinkByShort(string shortLink)
        {
            return _linksDirectory.GetLongLinkByShort(shortLink);
        }

        public List<Link> GetAllLinks()
        {
            return _linksDirectory.GetAllLinks();
        }

        public void DeleteAllLinks()
        {
            _linksDirectory.DeleteAllLinks();
        }
    }
}
