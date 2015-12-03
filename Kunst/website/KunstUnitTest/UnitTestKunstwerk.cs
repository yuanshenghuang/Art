using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kunst.Controllers;
using Kunst.Models;
using System.Collections.Generic;
using System.Linq;


namespace KunstUnitTest
{
    [TestClass]
    public class UnitTestKunstwerk
    {
       

        [TestMethod]
        public void TestAddKunstwerk()
        {
            KunstwerkController controller = new KunstwerkController();
            Kunstwerk kunstwerk = new Kunstwerk();

            kunstwerk.Categorie = "beeld";
            kunstwerk.Titel = "titel beeld";
            kunstwerk.BeschrijvingDE = "de";
            kunstwerk.BeschrijvingEN = "en";
            kunstwerk.BeschrijvingFR = "fr";
            kunstwerk.BeschrijvingNL = "nl";

            controller.db.Kunstwerk.Add(kunstwerk);           
            controller.db.SaveChanges();

            IQueryable<Kunstwerk> list = from x in controller.db.Kunstwerk select x;

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void TestKunstwerkController()
        {
            KunstwerkController controller = new KunstwerkController();
            Kunstwerk kunstwerk = new Kunstwerk();

            string cultureName = null;
            string category = "beeld";

            IQueryable<Kunstwerk> list = from x in controller.db.Kunstwerk select x;

            if (cultureName == "fr")
            {
                list = list.Where(x => x.Categorie == category).ToList().Select(item => new Kunstwerk { KunstwerkId = item.KunstwerkId, Categorie = item.Categorie, Titel = item.Titel, BeschrijvingFR = item.BeschrijvingFR }).AsQueryable();
                Assert.AreEqual("fr", list.Select(item => new Kunstwerk { BeschrijvingFR = item.BeschrijvingFR }));
                Assert.IsNotNull(list);
            }
            else if (cultureName == "en")
            {
                list = list.Where(x => x.Categorie == category).ToList().Select(item => new Kunstwerk { KunstwerkId = item.KunstwerkId, Categorie = item.Categorie, Titel = item.Titel, BeschrijvingEN = item.BeschrijvingEN }).AsQueryable();
                Assert.AreEqual("en", list.Select(item => new Kunstwerk { BeschrijvingEN = item.BeschrijvingEN }));
                Assert.IsNotNull(list);
            }
            else if (cultureName == "de")
            {
                list = list.Where(x => x.Categorie == category).ToList().Select(item => new Kunstwerk { KunstwerkId = item.KunstwerkId, Categorie = item.Categorie, Titel = item.Titel, BeschrijvingDE = item.BeschrijvingDE }).AsQueryable();
                Assert.AreEqual("de", list.Select(item => new Kunstwerk { BeschrijvingDE = item.BeschrijvingDE }));
                Assert.IsNotNull(list);
            }
            else if(cultureName == "nl")
            {
                list = list.Where(x => x.Categorie == category).ToList().Select(item => new Kunstwerk { KunstwerkId = item.KunstwerkId, Categorie = item.Categorie, Titel = item.Titel, BeschrijvingNL = item.BeschrijvingNL }).AsQueryable();
                Assert.AreEqual("nl", list.Select(item => new Kunstwerk { BeschrijvingNL = item.BeschrijvingNL }));
                Assert.IsNotNull(list);
            }

           
        }
    }
}
