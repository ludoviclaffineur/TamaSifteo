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
				tama.FaimDouble -= 40;
				break;
			case "fruit":
				tama.SanteDouble+=20;
				tama.FaimDouble-=20;
				tama.HumeurDouble+=20;
				break;
			case "legume":
				tama.SanteDouble+=20;
				tama.FaimDouble-=20;
				tama.HumeurDouble+=20;
				break;
			case "viande":
				tama.SanteDouble += 40;
				tama.FaimDouble -= 40;
				break;
			case "glace":
				tama.SanteDouble-=20;
				tama.FaimDouble-=20;
				tama.HumeurDouble += 40;
				break;
			case "cafe":
				tama.SanteDouble -= 20;
				tama.FormeDouble += 40;
				break;
			case "soda":
				tama.SanteDouble -= 20;
				tama.FormeDouble+=10;
				break;
			case "alcool":
				tama.SanteDouble -= 20;
				tama.FormeDouble -= 20;
				break;
			case "eau":
				tama.SanteDouble+=20;
				tama.FormeDouble+=20;
				break;
			}
		}

	}
}

