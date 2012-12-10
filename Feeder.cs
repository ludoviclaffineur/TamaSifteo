using System;
using System.Collections.Generic;

namespace _Sorter
{
	public class Feeder
	{
		public List<Nourriture> nourriture_ingurgite = new List<Nourriture>();
		public List<Activite> activite_realisees= new List<Activite>();
		public Tamagotchi tama;
		public List<Int16> ratio_food_activities = new List<Int16>();

		public Feeder ()
		{
		}

		public Feeder (Tamagotchi tama)
		{
			this.tama=tama;
		}

		float getCoeffDejaMange (string name)
		{
			int count = 0;
			if (nourriture_ingurgite.Count < 5) {
				foreach (Nourriture nourri in nourriture_ingurgite) {
					if (nourri.name.Equals (name)) {
						count++;
					}
				}
			} else {

				for (int i =nourriture_ingurgite.Count-1; i>=nourriture_ingurgite.Count-5; i--) {
					if (nourriture_ingurgite [i].name.Equals (name)) {
						count++;
					}
				}
			}
			return (float)(1.0/(float)(1.0+count));
		}

		private float getCountInteraction ()
		{
			float count = 0;
			foreach (Int16 a in ratio_food_activities) {
				count += a;
			}
			return count;
		}

		public void add (Nourriture nourri)
		{
			float coeffDejaMange = getCoeffDejaMange (nourri.name);
			float CoeffAge = nourri.getCoeffAge (tama);
			float count = getCountInteraction ();
			if (count > 2) {
				CoeffAge*=1.0f/count;
			}
			CoeffAge*=coeffDejaMange;
			Random rand=new Random(90);
			switch (nourri.name) {
			case "hamburger":
				tama.SanteDouble -= ((10-rand.Next(10))*CoeffAge);
				tama.HumeurDouble += ((20-rand.Next(20))*CoeffAge);
				tama.FormeDouble -= ((20-rand.Next(20))*CoeffAge);
				tama.FaimDouble -= ((40-rand.Next(40))*CoeffAge);
				break;
			case "fruit":
				tama.SanteDouble+=((20-rand.Next(20))*CoeffAge);
				tama.FaimDouble-=((20-rand.Next(20))*CoeffAge);
				tama.HumeurDouble+=((20-rand.Next(20))*CoeffAge);
				break;
			case "legume":
				tama.SanteDouble+=((20-rand.Next(20))*CoeffAge);
				tama.FaimDouble-=((20-rand.Next(20))*CoeffAge);
				tama.HumeurDouble+=(20-rand.Next(20))*CoeffAge;
				break;
			case "viande":
				tama.SanteDouble += ((20-rand.Next(20))*CoeffAge);
				tama.FaimDouble -= ((20-rand.Next(20))*CoeffAge);
				break;
			case "glace":
				tama.SanteDouble-=((20-rand.Next(20))*CoeffAge);
				tama.FaimDouble-=((20-rand.Next(20))*CoeffAge);
				tama.HumeurDouble += ((40-rand.Next(40))*CoeffAge);
				break;
			case "cafe":
				tama.SanteDouble -= ((20-rand.Next(20))*CoeffAge);
				tama.FormeDouble += ((40-rand.Next(40))*CoeffAge);
				break;
			case "soda":
				tama.SanteDouble -= ((20-rand.Next(20))*CoeffAge);
				tama.FormeDouble+= ((10-rand.Next(10))*CoeffAge);
				break;
			case "alcool":
				tama.SanteDouble -= ((20-rand.Next(20))*CoeffAge);
				tama.FormeDouble -= ((20-rand.Next(20))*CoeffAge);
				break;
			case "eau":
				tama.SanteDouble+=((20-rand.Next(20))*CoeffAge);
				tama.FormeDouble+=((20-rand.Next(20))*CoeffAge);
				break;
			}
			nourriture_ingurgite.Add(nourri);
			ratio_food_activities.Add(1);
		}
		public void add (Activite acti)
		{
			float coeffDejaMange = getCoeffDejaMange(acti.name);
			float CoeffAge=acti.getCoeffAge(tama);
			float count = getCountInteraction ();
			if (count < -2) {
				CoeffAge*=1.0f/-count;
			}
			CoeffAge*=coeffDejaMange;
			Random rand=new Random(91);
			switch (acti.name)
			{
			case "parc": 
				tama.FaimDouble +=(20-rand.Next(20))*CoeffAge;
				tama.FormeDouble -=(20-rand.Next(20))*CoeffAge;
				tama.SanteDouble +=(20-rand.Next(20))*CoeffAge;
				tama.PropreteDouble -=(10-rand.Next(10))*CoeffAge;
				tama.HumeurDouble+=(20-rand.Next(20))*CoeffAge;
				break;
			case "culture":
				tama.HumeurDouble+=(10-rand.Next(10))*CoeffAge;
				tama.IntelligenceDouble +=(20-rand.Next(20))*CoeffAge;
				tama.SanteDouble-=(20-rand.Next(20))*CoeffAge;
				tama.FaimDouble+=(20-rand.Next(20))*CoeffAge;
				break;
			case "boite_de_nuit":
				tama.FormeDouble-=(30-rand.Next(30))*CoeffAge;
				tama.PropreteDouble-=(30-rand.Next(30))*CoeffAge;
				tama.HumeurDouble +=(20-rand.Next(20))*CoeffAge;
				break;
			case "cinema":
				tama.SanteDouble-=(20-rand.Next(20))*CoeffAge;
				tama.FormeDouble+=(20-rand.Next(20))*CoeffAge;
				tama.HumeurDouble+=(30-rand.Next(30))*CoeffAge;
				tama.IntelligenceDouble -=(20-rand.Next(20))*CoeffAge;
				tama.FaimDouble +=(10-rand.Next(10))*CoeffAge;
				
				break;
			case "sdb":
				tama.PropreteDouble+=(20-rand.Next(20))*CoeffAge;
				tama.HumeurDouble+=(20-rand.Next(20))*CoeffAge;
				break;
			case "lit":
				tama.FormeDouble+=(30-rand.Next(30))*CoeffAge;
				break;
			}
			activite_realisees.Add(acti);
			ratio_food_activities.Add(-1);
		}
		float getCoeffDejaPartique (string name)
		{
			int count = 0;
			if (activite_realisees.Count < 5) {
				foreach (Activite acti in activite_realisees) {
					if (acti.name.Equals (name)) {
						count++;
					}
				}
			} else {
				
				for (int i = activite_realisees.Count-1; i>=activite_realisees.Count-5; i--) {
					if (activite_realisees [i].name.Equals (name)) {
						count++;
					}
				}
			}
			return (float)(1.0/(float)(1.0+count));
		}

	}
}

