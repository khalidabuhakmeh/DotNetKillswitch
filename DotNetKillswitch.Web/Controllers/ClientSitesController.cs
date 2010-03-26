using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetKillswitch.Core;
using DotNetKillswitch.Core.Filters;
using DotNetKillswitch.Core.Services;

namespace DotNetKillswitch.Web.Controllers
{
    [NHibernateSession]
    public class ClientSitesController : Controller
    {
        private readonly IClientsService _clientsService;

        public ClientSitesController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        public ActionResult Index()
        {            
            return View(_clientsService.Get());
        }

        //
        // GET: /ClientSites/Create

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id, LastTimeBlackListed")]ClientSite clientSite)
        {
            if (ModelState.IsValid)
            {
                _clientsService.Add(clientSite);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clientSite);
            }

        }

        //
        // GET: /ClientSites/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var client = _clientsService.Get(id);
            return View(client);
        }

        //
        // POST: /ClientSites/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "LastTimeBlackListed")]ClientSite site)
        {
            if (ModelState.IsValid)
            {
                _clientsService.Add(site);
                return RedirectToAction("Index");
            }
            else
            {
                return View(site);
            }
        }

        public ActionResult Remove(Guid id)
        {
            _clientsService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
