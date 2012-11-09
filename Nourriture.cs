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

		public void setParameters (Tamagotchi tama)
		{
			switch (name) {
			case "hamburger": 
				tama.SanteDouble -= 10;
				tama.HumeurDouble += 20;
				tama.FormeDouble -= 20;
				tama.FaimDouble -= 30;

				break;
			case "fruit":
				tama.SanteDouble+=20;
				tama.FaimDouble+=20;
				tama.HumeurDouble+=20;
				break;
			case "legume":
				tama.SanteDouble++;
				tama.FaimDouble--;
				tama.HumeurDouble++;
				break;
			case "viande":
				tama.SanteDouble += 2;
				tama.FaimDouble += 2;
				break;
			case "glace":
				tama.SanteDouble--;
				tama.FaimDouble--;
				tama.HumeurDouble += 2;
				break;
			case "cafe":
				tama.SanteDouble -= 2;
				tama.FormeDouble += 2;
				break;
			case "soda":
				tama.SanteDouble -= 2;
				tama.FormeDouble++;
				break;
			case "alcool":
				tama.SanteDouble -= 2;
				tama.FormeDouble -= 2;
				break;
			case "eau":
				tama.SanteDouble++;
				tama.FormeDouble++;
				break;
			}
		}

	}
}

