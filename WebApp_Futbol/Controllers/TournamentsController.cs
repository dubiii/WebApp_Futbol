using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain;
using WebApp_Futbol.Helpers;
using WebApp_Futbol.Models;

namespace WebApp_Futbol.Controllers
{

    [Authorize]
    public class TournamentsController : Controller
    {
        
        private DataContextLocal db = new DataContextLocal();

        #region Dates
        // GET: Dates/Create
        public async Task<ActionResult> CreateDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournament = await db.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                return HttpNotFound();
            }

            var date = new Date { TournamentId = tournament.TournamentId};
            return View(date);
        }

        // POST: Dates/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDate(Date date)
        {
            if (ModelState.IsValid)
            {
                db.Dates.Add(date);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", date.TournamentId));
            }

            return View(date);
        }

        // GET: Dates/Edit/5
        public async Task<ActionResult> EditDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var date = await db.Dates.FindAsync(id);

            if (date == null)
            {
                return HttpNotFound();
            }

            return View(date);
        }

        // POST: Dates/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDate(Date date)
        {
            if (ModelState.IsValid)
            {
                db.Entry(date).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", date.TournamentId));
            }
            
            return View(date);
        }

        public async Task<ActionResult> DeleteDate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var date = await db.Dates.FindAsync(id);

            if (date == null)
            {
                return HttpNotFound();
            }

            db.Dates.Remove(date);
            await db.SaveChangesAsync();
            return RedirectToAction(string.Format("Details/{0}", date.TournamentId));
        }
        #endregion

        #region TournamentGroups
        // GET: TournamentGroups/Create
        public async Task<ActionResult> CreateGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournament = await db.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                return HttpNotFound();
            }

            var view = new TournamentGroup {TournamentId = tournament.TournamentId};
            return View(view);
        }
        

        // POST: TournamentGroups/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGroup(TournamentGroup tournamentGroup)
        {
            if (ModelState.IsValid)
            {
                db.TournamentGroups.Add(tournamentGroup);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", tournamentGroup.TournamentId));
            }

            return View(tournamentGroup);
        }

        public async Task<ActionResult> EditGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournamentGroup = await db.TournamentGroups.FindAsync(id);

            if (tournamentGroup == null)
            {
                return HttpNotFound();
            }
            //var view = new TournamentGroup { TournamentId = tournamentGroup.TournamentId };
            return View(tournamentGroup);
        }

        // POST: TournamentGroups/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGroup(TournamentGroup tournamentGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournamentGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("Details/{0}", tournamentGroup.TournamentId));
            }
            
            return View(tournamentGroup);
        }

        public async Task<ActionResult> DeleteGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournamentGroup = await db.TournamentGroups.FindAsync(id);

            if (tournamentGroup == null)
            {
                return HttpNotFound();
            }

            db.TournamentGroups.Remove(tournamentGroup);
            await db.SaveChangesAsync();
            return RedirectToAction(string.Format("Details/{0}", tournamentGroup.TournamentId));
        }

        public async Task<ActionResult> DetailsGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detailsgroup = await db.TournamentGroups.FindAsync(id);

            if (detailsgroup == null)
            {
                return HttpNotFound();
            }

            return View(detailsgroup); //devuelve el grupo completo
        }


        #endregion

        
        #region TournamentTeam
        public async Task<ActionResult> CreateTeam(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournament = await db.TournamentGroups.FindAsync(id);

            if (tournament == null)
            {
                return HttpNotFound();
            }

            ViewBag.LeagueId = new SelectList(db.Leagues.OrderBy(t => t.Name), "LeagueId", "Name");
            ViewBag.TeamId = new SelectList(db.Teams.Where(t => t.LeagueId == db.Leagues.FirstOrDefault().LeagueId).OrderBy(t => t.Name), "TeamId", "Name");
            var view = new TournamentTeamView {TournamentGroupId = tournament.TournamentGroupId};
            return View(view);
        }

        // POST: TournamentTeams/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTeam(TournamentTeam tournamentTeam)
        {
            if (ModelState.IsValid)
            {
                db.TournamentTeams.Add(tournamentTeam);
                await db.SaveChangesAsync();
                return RedirectToAction(string.Format("DetailsGroup/{0}", tournamentTeam.TournamentGroupId));
            }

            ViewBag.TeamId = new SelectList(db.Teams.OrderBy(t => t.Name), "TeamId", "Name", tournamentTeam.TeamId);
            return View(tournamentTeam);
        } 
        #endregion

        // GET: Tournaments
        public async Task<ActionResult> Index()
        {
            return View(await db.Tournaments.ToListAsync());
        }

        // GET: Tournaments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = await db.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // GET: Tournaments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournaments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Logos";

                if (view.LogoFile != null)
                {
                    pic = FileHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }


                var tournament = ToTournament(view);
                tournament.Logo = pic;

                db.Tournaments.Add(tournament);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Tournament ToTournament(TournamentView view)
        {
            return new Tournament
            {
                TournamentId = view.TournamentId,
                Logo = view.Logo,
                Name = view.Name,
                Groups = view.Groups,
                IsActive = view.IsActive,
                Order = view.Order
            };
        }

        // GET: Tournaments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournament = await db.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                return HttpNotFound();
            }
            var view = ToView(tournament);
            return View(view);
        }

        private TournamentView ToView(Tournament tournament)
        {
            return new TournamentView
            {
                TournamentId = tournament.TournamentId,
                Logo = tournament.Logo,
                Name = tournament.Name,
                Groups = tournament.Groups,
                IsActive = tournament.IsActive,
                Order = tournament.Order
            };
        }

        // POST: Tournaments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TournamentView view)
        {
            if (ModelState.IsValid)
            {

                var pic = view.Logo;
                var folder = "~/Content/Logos";

                if (view.LogoFile != null)
                {
                    pic = FileHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                view.Logo = pic;
                var tournament = ToTournament(view);

                db.Entry(tournament).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Tournaments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tournament = await db.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                return HttpNotFound();
            }
            
            return View(tournament);
        }

        

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tournament tournament = await db.Tournaments.FindAsync(id);
            db.Tournaments.Remove(tournament);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
