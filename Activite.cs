using System;

namespace _Sorter
{
	public class Activite
	{
		string name;
		public Activite (string name)
		{
			this.name=name;
		}
		public void setParameters(Tamagotchi tama){
			switch (name)
			{
			case "parc": 
				tama.FaimDouble += 20;
				tama.FormeDouble -=20;
				tama.SanteDouble +=20;
				tama.PropreteDouble -=10;
				tama.HumeurDouble+=20;
				break;
			case "culture":
				tama.HumeurDouble+=10;
				tama.IntelligenceDouble +=20;
				tama.SanteDouble-=20;
				tama.FaimDouble+=20;
				break;
			case "boite_de_nuit":
				tama.FormeDouble-=30;
				tama.PropreteDouble-=15;
				tama.HumeurDouble +=10;
				break;
			case "cinema":
				tama.SanteDouble-=10;
				tama.FormeDouble+=10;
				tama.HumeurDouble+=20;
				tama.IntelligenceDouble -=20;
				tama.FaimDouble +=10;

				break;
			case "sdb":
				tama.PropreteDouble+=20;
				tama.HumeurDouble+=20;
				break;
			case "lit":
				tama.FormeDouble+=30;
				break;
			}
		}

	}
}

