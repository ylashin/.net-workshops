using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShouldITweet2.Data;
using ShouldITweet2.Models;
using Serilog;

namespace ShouldITweet2.Controllers
{
    public class AdminController : Controller
    {
        private IRepository Repository;

        public AdminController(IRepository repository)
        {
            Repository = repository;
        }
        // GET: Admin
        public ActionResult Index()
        {
            Log.Information("Viewing verboten phrase list");
            return View(Repository.GetAll<VerbotenPhrase>().ToList().Select(v => v.MapToDto()));
        }

        // GET: Admin/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var verbotenPhrase = Repository.GetById<VerbotenPhrase>(id.Value);

            if (verbotenPhrase == null)
            {
                return HttpNotFound();
            }
            Log.Information("Viewing verboten phrase {PhraseID}" , id);
            return View(verbotenPhrase.MapToDto());
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Phrase,LastModified")] VerbotenPhraseDto verbotenPhraseDto)
        {
            

            if (ModelState.IsValid)
            {
                var verbotenPhrase = verbotenPhraseDto.MapToModel();
                verbotenPhrase.SetLastModified(DateTimeOffset.UtcNow);
                verbotenPhrase.SetId(Guid.NewGuid());

                Repository.AddOrUpdate(verbotenPhrase);

                Log.Information("Created verboten phrase {PhraseText}", verbotenPhrase.Phrase);
                return RedirectToAction("Index");
            }

            return View(verbotenPhraseDto);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VerbotenPhrase verbotenPhrase = Repository.GetById<VerbotenPhrase>(id.Value);
            if (verbotenPhrase == null)
            {
                return HttpNotFound();
            }
            return View(verbotenPhrase.MapToDto());
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Phrase,LastModified")] VerbotenPhraseDto verbotenPhraseDto)
        {
            if (ModelState.IsValid)
            {
                VerbotenPhrase verbotenPhrase = Repository.GetById<VerbotenPhrase>(verbotenPhraseDto.Id);

                if (verbotenPhrase == null)
                {
                    return HttpNotFound();
                }

                verbotenPhrase.SetPhrase(verbotenPhraseDto.Phrase);

                verbotenPhrase.SetLastModified(DateTimeOffset.UtcNow);

                Log.Information("Updated verboten phrase {PhraseID}", verbotenPhrase.Id);
                return RedirectToAction("Index");
            }
            return View(verbotenPhraseDto);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VerbotenPhrase verbotenPhrase = Repository.GetById<VerbotenPhrase>(id.Value);
            if (verbotenPhrase == null)
            {
                return HttpNotFound();
            }
            return View(verbotenPhrase.MapToDto());
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VerbotenPhrase verbotenPhrase = Repository.GetById<VerbotenPhrase>(id);
            if (verbotenPhrase == null)
            {
                return HttpNotFound();
            }
            Repository.Delete(verbotenPhrase);
            Log.Information("Deleted verboten phrase {PhraseID}", id);
            return RedirectToAction("Index");
        }

        
    }
}
