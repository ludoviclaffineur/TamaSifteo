using System;

namespace _Sorter
{
	public class Nourriture
	{
		string name;

		public Nourriture (string name)
		{
			this.name = name;
		}


		private float getCoeffAge (Tamagotchi tama)
		{
			float CoeffAge=1;
			Console.WriteLine(tama.TamaRulesTabRT[4].ExplicationString);
			switch(tama.TamaRulesTabRT[4].ExplicationString){
			case "Bebe":
				CoeffAge = 3;
				Console.WriteLine("Youps");
				break;
			case "Vieux":
				CoeffAge = 0.9f;
				break;
			case "Tres vieux":
				CoeffAge = 0.8f;
				break;
			case "Invalide":
				CoeffAge = 0.5f;
				break;
			}
			return CoeffAge;

		}

		public void setParameters (Tamagotchi tama)
		{
			float CoeffAge=getCoeffAge(tama);
			Console.WriteLine(CoeffAge);
			switch (name) {
			case "hamburger":
				tama.SanteDouble -= (10*CoeffAge);
				tama.HumeurDouble += (20*CoeffAge);
				tama.FormeDouble -= (20*CoeffAge);
				tama.FaimDouble -= (40*CoeffAge);
				break;
			case "fruit":
				tama.SanteDouble+=(20*CoeffAge);
				tama.FaimDouble-=(20*CoeffAge);
				tama.HumeurDouble+=(20*CoeffAge);
				break;
			case "legume":
				tama.SanteDouble+=(20*CoeffAge);
				tama.FaimDouble-=(20*CoeffAge);
				tama.HumeurDouble+=20*CoeffAge;
				break;
			case "viande":
				tama.SanteDouble += (40*CoeffAge);
				tama.FaimDouble -= (40*CoeffAge);
				break;
			case "glace":
				tama.SanteDouble-=(20*CoeffAge);
				tama.FaimDouble-=(20*CoeffAge);
				tama.HumeurDouble += (40*CoeffAge);
				break;
			case "cafe":
				tama.SanteDouble -= (20*CoeffAge);
				tama.FormeDouble += (40*CoeffAge);
				break;
			case "soda":
				tama.SanteDouble -= (20*CoeffAge);
				tama.FormeDouble+=(10*CoeffAge);
				break;
			case "alcool":
				tama.SanteDouble -= (20*CoeffAge);
				tama.FormeDouble -= (20*CoeffAge);
				break;
			case "eau":
				tama.SanteDouble+=(20*CoeffAge);
				tama.FormeDouble+=(20*CoeffAge);
				break;
			}
		}

	}
}

