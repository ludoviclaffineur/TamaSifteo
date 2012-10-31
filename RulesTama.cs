using System;

namespace _Sorter
{
	public class RulesTama
	{

		String nom;
		Double valeur;
		String explication;
		
		public String ExplicationString
		{
			get { return explication; }
			set { explication = value; }
		}
		
		public String NomString
		{
			get { return nom; }
			set { nom = value; }
		}
		
		public Double ValeurDouble
		{
			get { return valeur; }
			set { valeur = value; }
		}
		
		public RulesTama() { 
			
			
		}
	}
}

