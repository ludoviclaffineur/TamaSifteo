using System;
using System.Collections.Generic;

namespace _Sorter
{
	public class Feeder
	{
		public List<Nourriture> nourriture_ingurgite = new List<Nourriture>();
		public List<Activite> activite_realisees= new List<Activite>();
		public Tamagotchi tama;

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

		public void add (Nourriture nourri)
		{
			float coeffDejaMange = getCoeffDejaMange(nourri.name);
			float CoeffAge=nourri.getCoeffAge(tama);
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
		}
		public void add (Activite acti)
		{

		}

	}
}

