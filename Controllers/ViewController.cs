using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShorter.Models;
using UrlShorter.Logic;

namespace UrlShorter.Controllers
{
    public class ViewController : Controller
    {
        private readonly ShorterUsage _shorterUsage;

        public ViewController(LinksContext context)
        {
            _shorterUsage = new ShorterUsage(context);
        }

        [Route("/{shortLink?}")]
        public IActionResult Index(string shortLink)
        {
            if (!string.IsNullOrWhiteSpace(shortLink))
            {
                var longLink = _shorterUsage.GetLongLinkByShort(shortLink);
                return Redirect(longLink);
            }
            return View("Index", "");
        }

        public IActionResult Generate(string longLink)
        {
            ViewBag.Long = longLink;
            string fullShortLink = String.Format ("https://{0}/{1}",Request.Host.Value,_shorterUsage.GenerateOrReturnExisting(longLink));
            return View("Index",fullShortLink);
        }

        public IActionResult DeleteAllLinks()
        {
            _shorterUsage.DeleteAllLinks();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}