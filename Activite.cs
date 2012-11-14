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
				tama.FaimDouble += 2;
				tama.FormeDouble -=2;
				tama.SanteDouble +=2;
				tama.PropreteDouble -=1;
				tama.HumeurDouble+=2;
				break;
			case "culture":
				tama.HumeurDouble++;
				tama.IntelligenceDouble +=2;
				tama.SanteDouble--;
				tama.FaimDouble+=2;
				break;
			case "boite_de_nuit":
				break;
			case "cinema":
				tama.SanteDouble--;
				tama.FormeDouble++;
				tama.HumeurDouble+=2;
				tama.IntelligenceDouble -=2;
				tama.FaimDouble ++;

				break;
			case "sdb":
				tama.PropreteDouble+=2;
				tama.HumeurDouble++;
				break;
			}
		}

	}
}

