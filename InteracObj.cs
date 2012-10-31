using System;
using System.Collections.Generic;

namespace _Sorter
{
	public class InteracObj
	{
		String categorie;
		List<String> type;
		List<String> result;
		Int16[] proportion;
		
		public Int16[] ProportionString
		{
			get { return proportion; }
			set { proportion = value; }
		}
		
		public List<String> ResultString
		{
			get { return result; }
			set { result = value; }
		}
		
		public List<String> TypeString
		{
			get { return type; }
			set { type = value; }
		}
		
		public String  CategorieString
		{
			get { return categorie; }
			set { categorie = value; }
		}


		public InteracObj ()
		{
		}
	}
}

