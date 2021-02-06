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
            ViewBag.Host = String.Format("https://{0}/", Request.Host.Value);
            return View(_shorterUsage.GetAllLinks());
        }

        public IActionResult Generate(string longLink)
        {
            _shorterUsage.GenerateAndSave(longLink);
            return RedirectToAction("Index");
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