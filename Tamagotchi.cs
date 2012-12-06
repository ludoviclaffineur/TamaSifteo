using System;

namespace _Sorter
{
	public class Tamagotchi
	{
		RulesTama[] TamaRulesTab;
		Double sante;
		Double forme;
		Double humeur;
		Double proprete;
		Double age;
		Double faim;
		Double intelligence;
		public RulesTama[] TamaRulesTabRT
		{
			get { return TamaRulesTab; }
			set { TamaRulesTab = value;
				
				SanteDouble = TamaRulesTabRT[0].ValeurDouble;
				FormeDouble = TamaRulesTabRT[1].ValeurDouble;
				HumeurDouble = TamaRulesTabRT[2].ValeurDouble;
				PropreteDouble = TamaRulesTabRT[3].ValeurDouble;
				AgeDouble = TamaRulesTabRT[4].ValeurDouble;
				FaimDouble = TamaRulesTabRT[5].ValeurDouble;
				IntelligenceDouble = TamaRulesTabRT[6].ValeurDouble;
				
			}
		}
		
		public Double IntelligenceDouble
		{
			get { return intelligence; }
			set { intelligence = value;
				RulesTama rt = new RulesTama();
				rt.NomString = "Intelligence";
				rt.ValeurDouble = value;
				TamaRulesTabRT[6] = rt;
				
			}
		}
		
		public Double FaimDouble
		{
			get { return faim; }
			set
			{
				faim = value; 
				RulesTama rt = new RulesTama();
				rt.NomString = "Faim";
				rt.ValeurDouble = value;
				TamaRulesTabRT[5] = rt;
			}
		}
		
		public Double AgeDouble
		{
			get { return age; }
			set { age = value;
				RulesTama rt = new RulesTama();
				rt.NomString = "Age";
				rt.ValeurDouble = value;
				TamaRulesTabRT[4] = rt;
			}
		}
		
		public Double PropreteDouble
		{
			get { return proprete; }
			set { proprete = value;
				RulesTama rt = new RulesTama();
				rt.NomString = "Proprete";
				rt.ValeurDouble = value;
				TamaRulesTabRT[3] = rt;
			}
		}
		
		public Double HumeurDouble
		{
			get { return humeur; }
			set { humeur = value;
				
				RulesTama rt = new RulesTama();
				rt.NomString = "Humeur";
				rt.ValeurDouble = value;
				TamaRulesTabRT[2] = rt;
				
			}
		}
		
		public Double FormeDouble
		{
			get { return forme; }
			set { forme = value;
				RulesTama rt = new RulesTama();
				rt.NomString = "Forme";
				rt.ValeurDouble = value;
				TamaRulesTabRT[1] = rt;
				
				
			}
		}
		
		
		public Double SanteDouble
		{
			get { return sante; }
			set { sante = value;
				
				RulesTama rt = new RulesTama();
				rt.NomString = "Sante";
				rt.ValeurDouble = value;
				TamaRulesTabRT[0] = rt;
				
			}
		}
		
		public Tamagotchi()
		{
			//nbr rules
			TamaRulesTab = new RulesTama[7];
			
		}
	}
}

