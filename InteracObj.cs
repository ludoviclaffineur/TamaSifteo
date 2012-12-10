using System;
using System.Collections.Generic;

namespace _Sorter
{
	public class InteracObj
	{
		String categorie;
		String type;
		String result;
		Int16 proportion;
		
		public String TypeString
		{
			get { return type; }
			set { type = value; }
		}
		
		public String ResultString
		{
			get { return result; }
			set { result = value; }
		}
		
		public Int16 ProportionString
		{
			get { return proportion; }
			set { proportion = value; }
		}
		
		
		public String  CategorieString
		{
			get { return categorie; }
			set { categorie = value; }
		}
		
		public InteracObj() { 
			
		}
	}
}

