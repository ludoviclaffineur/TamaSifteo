using System;
using System.Threading;
using System.Collections.Generic;
using System.Xml;

namespace _Sorter
{
	public class ThreadRules
	{

		RulesTama[] tamaRules;
		Tamagotchi tama;
		DateTime looping;
		List<RulesObj> RulesList;
		List<ObjNomVal> ObjNomValK;
		List<InteracObj> ObjInterac;
		public List<CubeWrapper> CubeList;
		public bool start = true;
		Double K = 1;
		
		public Double KDouble {
			get { return K; }
			set { K = value; }
		}
		
		internal List<InteracObj> ObjInteracList {
			get { return ObjInterac; }
			set { ObjInterac = value; }
		}
		
		internal List<ObjNomVal> ObjNomValKList {
			get { return ObjNomValK; }
			set { ObjNomValK = value; }
		}
		
		internal List<RulesObj> rulesList {
			get { return RulesList; }
			set { RulesList = value; }
		}
		
		public Tamagotchi TamaRT {
			get { return tama; }
			set { tama = value; }
		}
		
		public RulesTama[] TamaRulesRT {
			get { return tamaRules; }
			set { tamaRules = value; }
		}
		
		public ThreadRules ()
		{
			
			XmlDocument document = new XmlDocument ();
			document.Load ("./rules.xml");
			
			XmlNode node = document.DocumentElement;
			
			XmlNodeList nodeList = node.SelectNodes ("rules");
			
			RulesList = new List<RulesObj> ();
			ObjNomValK = new List<ObjNomVal> ();
			ObjInterac = new List<InteracObj> ();
			
			for (int i = 0; i < nodeList.Count; i++) {
				for (int w = 0; w < nodeList[i].ChildNodes.Count; w++) {
					RulesObj ro = new RulesObj ();
					
					ro.NomString = nodeList [i].Attributes [0].Value;
					ro.MinInt16 = Int16.Parse (nodeList [i].ChildNodes [w].Attributes [0].Value);
					ro.MaxInt16 = Int16.Parse (nodeList [i].ChildNodes [w].Attributes [1].Value);
					ro.ExplicationString = nodeList [i].ChildNodes [w].Attributes [2].Value;
					
					RulesList.Add (ro);
					
					if (w == nodeList [i].ChildNodes.Count - 1) {
						RulesList [RulesList.Count - 1].EndofRulesBool = true;
					}
					
				}
				
			}
			
			nodeList = node.SelectNodes ("facteur");
			
			for (int i = 0; i < nodeList.Count; i++) {
				for (int w = 0; w < nodeList[i].ChildNodes.Count; w++) {
					ObjNomVal nv = new ObjNomVal ();
					
					nv.NomString = nodeList [i].ChildNodes [w].Attributes [0].Value;
					nv.ValeurDouble = Double.Parse (nodeList [i].ChildNodes [w].Attributes [1].Value);
					
					ObjNomValKList.Add (nv);
				}
			}
			nodeList = node.SelectNodes("interac");
			
			for (int i = 0; i < nodeList.Count; i++)
			{
				for (int w = 0; w < nodeList[i].ChildNodes.Count; w++)
				{
					InteracObj oi = new InteracObj();
					
					oi.CategorieString = nodeList[i].Attributes[0].Value;
					
					oi.TypeString = nodeList[i].ChildNodes[w].Attributes[0].Value;
					oi.ResultString = nodeList[i].ChildNodes[w].Attributes[1].Value;
					oi.ProportionString = this.TranslateProportion(nodeList[i].ChildNodes[w].Attributes[2].Value);
					ObjInteracList.Add(oi);
					
				}
			}
			
			
		}
		
		private Int16 TranslateProportion(String propor)
		{
			Int16 result = 0;
			
			
			switch (propor)
			{
			case "-":
				result = -1;
				break;
			case "--":
				result = -2;
				break;
			case "---":
				result = -3;
				break;
			case "----":
				result = -4;
				break;
			case "+":
				result = 1;
				break;
			case "++":
				result = 2;
				break;
			case "+++":
				result = 3;
				break;
			case "++++":
				result = 4;
				break;
			default:
				break;
			}

			return result;
			
		}

		void RefreshCubes ()
		{
			foreach (CubeWrapper cube in CubeList) {
				if (cube.mIndex == 0) {
					cube.mNeedDraw = true;
				}
			}
		}

		// Méthode boucle du thread
		public void ThreadLoop ()
		{
			DateTime testing;
			looping = DateTime.Now;
			
			while (start) {
				testing = DateTime.Now;
				
				if (testing.Subtract (looping).Seconds > 1) {
					tama.TamaRulesTabRT = TamaRulesRT;
					Console.WriteLine ("--------------");
					looping = DateTime.Now;
					for (int i = 0; i < TamaRulesRT.Length; i++) {
						
						for (int x = 0; x < RulesList.Count; x++) {
							
							// first pass
							if (TamaRulesRT [i].NomString.Equals (RulesList [x].NomString) &&
								TamaRulesRT [i].ValeurDouble >= RulesList [x].MinInt16) {
								
								if ((TamaRulesRT [i].ValeurDouble < RulesList [x].MaxInt16)
									|| (TamaRulesRT [i].ValeurDouble == RulesList [x].MaxInt16 && RulesList [x].EndofRulesBool == true)) {
									
									TamaRulesRT [i].ExplicationString = RulesList [x].ExplicationString;
									tama.TamaRulesTabRT[i].ExplicationString=RulesList [x].ExplicationString;
									//Console.WriteLine("BLABLA" + tama.TamaRulesTabRT[i].ExplicationString);
									
								}
								
							}
							
						}
						
						for (int w = 0; w < ObjNomValK.Count; w++) { 
							
							if (TamaRulesRT [i].ExplicationString != null &&
								TamaRulesRT [i].ExplicationString.Equals (ObjNomValK [w].NomString)) {
								
								K = ObjNomValK [w].ValeurDouble;
								
								Console.WriteLine ("Valeur k :" + K);
								
							}
							
						}
						
						Console.WriteLine ("Nom : " + TamaRulesRT [i].NomString);
						Console.WriteLine ("Num : " + TamaRulesRT [i].ValeurDouble);
						
						if (TamaRulesRT [i].NomString.Equals ("Age")) { 
							
							
							TamaRulesRT [i].ValeurDouble+=0.2d;
							
						}
						if (TamaRulesRT [i].NomString.Equals ("Faim")) { 
							
							
							TamaRulesRT [i].ValeurDouble+=5;
							
						}

						if (tama.TamaRulesTabRT [i].ValeurDouble <= 0) {
							tama.TamaRulesTabRT [i].ValeurDouble = 0.0;
						} else if (tama.TamaRulesTabRT [i].ValeurDouble >= 100) {
							tama.TamaRulesTabRT [i].ValeurDouble = 100.0;
						}

						
						for (int ww = 0; ww < ObjInteracList.Count; ww++) {
							
							if (ObjInteracList [ww].CategorieString.Equals (TamaRulesRT [i].NomString)) {

									if (ObjInteracList [ww].TypeString.Equals (TamaRulesRT [i].ExplicationString)) {
										
											for (int xox = 0; xox < TamaRulesRT.Length; xox++) {
												if (TamaRulesRT [xox].NomString.Equals (ObjInteracList [ww].ResultString.ToString ())) {
													
													Double checkValeur = TamaRulesRT [xox].ValeurDouble + Double.Parse (ObjInteracList [ww].ProportionString.ToString ()) * K;
													
													if (checkValeur <= 0) {
														TamaRulesRT [xox].ValeurDouble = 0.0;
													} else if (checkValeur >= 100) {
														TamaRulesRT [xox].ValeurDouble = 100.0;
													} else if (checkValeur < 100 && checkValeur > 0) {
														TamaRulesRT [xox].ValeurDouble += Double.Parse (ObjInteracList [ww].ProportionString.ToString ()) * K;
														
													}
													
												}
											}

									}
									
								
								
								
							}
							
						}
						
						
					}
					RefreshCubes();
				}
				
				//RefreshCubes();
				
				//break;
			}
		}
		
	}
}
