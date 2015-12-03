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
                                 CreatieMat="creatie materiaal",
                                 UitvoeringsMat="uitvoeringsmateriaal",
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
                                 Code="2000",
                                 CreatieMatDrager="creatie materiaal drager",
                                 CreatieMatGebruikt="creatie materiaal gebruikt",
                                 Signatie="signatie",
                                 Foto="closed.jpg"                                   
                }
            };

            tweeDWerkenList.ForEach(s => context.TweeDWerken.Add(s));
            context.SaveChanges();


            var geschrevenWerkenList = new List<GeschrevenWerken>
            {
                new GeschrevenWerken{
                      Categorie="boek",
                                 Titel = "boek",
                                 BeschrijvingNL="nl",
                                 BeschrijvingFR="fr",
                                 BeschrijvingEN="en",
                                 BeschrijvingDE="de",                                 
                                 Foto="clown.jpg" 
                }
            };

            geschrevenWerkenList.ForEach(s => context.GeschrevenWerken.Add(s));
            context.SaveChanges();


           








        }


    }
}