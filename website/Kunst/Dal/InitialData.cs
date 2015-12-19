using Kunst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kunst.Dal
{
    public class InitialData : System.Data.Entity.DropCreateDatabaseIfModelChanges<Kunst.Models.ApplicationDbContext>
    {
        protected override void Seed(Models.ApplicationDbContext context)
        {
            var drieDWerkenList = new List<DrieDWerken>
            {
                new DrieDWerken{ Categorie="beeld",
                                 Titel = "beeld",
                                 BeschrijvingNL="nl",
                                 BeschrijvingFR="fr",
                                 BeschrijvingEN="en",
                                 BeschrijvingDE="de",
                                 Code=2000,
                                 Materiaal="",
                                 Hoogte=20,
                                 Breedte=20,
                                 Diepte=10, 
                                 Foto="bray.jpg"                                 
                }
            };

            drieDWerkenList.ForEach(s => context.DrieDWerken.Add(s));
            context.SaveChanges();


            var tweeDWerkenList = new List<TweeDWerken>
            {
                new TweeDWerken{
                                 Categorie="collage",
                                 Titel = "collage",
                                 BeschrijvingNL="nl",
                                 BeschrijvingFR="fr",
                                 BeschrijvingEN="en",
                                 BeschrijvingDE="de",
                                 Code="2001",
                                 Materiaal="",
                                 Hoogte=20,
                                 Breedte=20,
                                 Foto="closed.jpg"                                   
                }
            };

            tweeDWerkenList.ForEach(s => context.TweeDWerken.Add(s));
            context.SaveChanges();
        }


    }
}