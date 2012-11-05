using System;

namespace _Sorter
{
	public class RulesObj
	{
		String nom;
		Int16 min;
		Int16 max;
		bool endofRules;
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
		
		public Int16 MinInt16
		{
			get { return min; }
			set { min = value; }
		}
		
		public Int16 MaxInt16
		{
			get { return max; }
			set { max = value; }
		}
		
		public bool EndofRulesBool
		{
			get { return endofRules; }
			set { endofRules = value; }
		}
		
		public RulesObj() { 
			
			
		}
	}
}

