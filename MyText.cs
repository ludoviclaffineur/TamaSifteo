using System;
using Sifteo;

namespace _Sorter
{
	public class myText
	{ 
		private int mX = 0;
		private int mY = 20;
		private int mTextH = 14;
		private int mTextW = 7;
		private bool mExtend = false; //extend allows the text string to be wrote across many cubes
		private bool mWrap = false;   //wrap allows the text to wrap across the screen if extend is 
		//enabled wrap will only wrap after reaching the end of the cubes
		private string mString = "0123456789";
		private Color mColor = new Color(0,0,0);	
		//writes/draws text to cube
		
		public void writeText(Cube cube){
			int i;
			int rowCount = 0;
			int nextX, nextY;
			
			for( i = 0; i < mString.Length; i++){
				
				nextX=mX + i*(mTextW + 3);
				nextY=mY;
				if(mWrap == true && nextX > 128){//when extending is added need to revisit wrapping detection
					rowCount++;
					nextY=mY + rowCount *mTextH;
				}
				
				printChar(cube,mString[i],nextX,nextY);			}
		}
		
		public void setStringOrig(int x, int y){
			mX = x;
			mY = y;
			
		}
		
		public void setText(string lString){
			mString = string.Copy (lString);
		}
		
		
		//draws all chars and numbers A-Z 0-9	
		public void printChar(Cube cube, char ch, int x,int y, Color c){
			mColor = c;
			printChar( cube, ch, x,y);
		}
		
		public void printChar(Cube cube, char c, int x,int y){
			switch( c) {
			case 'A':   //Draw A
				cube.FillRect(mColor,x, y + 2 - mTextH, 2, mTextH - 2); 
				cube.FillRect(mColor,x+2,y - mTextH ,mTextW - 2,2);
				cube.FillRect(mColor,x+2,y - mTextH/2,mTextW,2);  
				cube.FillRect(mColor,x+mTextW,y + 2 - mTextH,2,mTextH - 2); 
				break;
			case 'B' :  //Draw B
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x, y - mTextH/2, mTextW, 2);
				cube.FillRect(mColor, x + mTextW, y + 2 -mTextH, 2, mTextH/2-2);
				cube.FillRect(mColor, x + mTextW, y +2 -mTextH/2, 2, mTextH/2-4);	
				cube.FillRect(mColor, x +2, y - 2 , mTextW - 2, 2);
				cube.FillRect(mColor, x +2, y - mTextH, mTextW - 2, 2);	
				break;
			case 'C' : //Draw C
				cube.FillRect(mColor, x, y + 2 - mTextH, 2, mTextH-4);
				cube.FillRect(mColor, x +2, y - 2 , mTextW, 2);
				cube.FillRect(mColor, x +2, y - mTextH, mTextW, 2);
				break;
			case 'D' : //Draw D
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + mTextW, y + 2 -mTextH, 2, mTextH-4);
				cube.FillRect(mColor, x +2, y - 2 , mTextW - 2, 2);
				cube.FillRect(mColor, x +2, y - mTextH, mTextW - 2, 2);
				break;
			case 'E' : //Draw E	
				cube.FillRect(mColor, x, y- mTextH, 2, mTextH);
				cube.FillRect(mColor, x+2, y - mTextH, mTextW, 2);
				cube.FillRect(mColor, x+2, y - 2- mTextH/2, mTextW/2, 2);
				cube.FillRect(mColor, x+2, y - 2, mTextW, 2);	
				break;	
			case 'F' : //Draw F	
				cube.FillRect(mColor, x, y- mTextH, 2, mTextH);
				cube.FillRect(mColor, x+2, y - mTextH, mTextW, 2);
				cube.FillRect(mColor, x+2, y - 2 - mTextH/2, mTextW/2, 2);
				break;
			case 'G' : //Draw G
				cube.FillRect(mColor, x, y + 2- mTextH, 2, mTextH - 4);
				cube.FillRect(mColor, x + mTextW, y - 2 -mTextH/3, 2, mTextH/3);
				cube.FillRect(mColor, x + mTextW, y - 4- 3*mTextH/4, 2, mTextH/4);
				cube.FillRect(mColor, x+ mTextW/2, y - mTextH/2, mTextW/2 + 4, 2);
				cube.FillRect(mColor, x +2, y - 2 , mTextW - 2, 2);
				cube.FillRect(mColor, x +2, y - mTextH, mTextW - 2, 2);	
				break;	
			case 'H' : //Draw H
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH  );   
				cube.FillRect(mColor, x + 2, y - mTextH/2, mTextW,2);  
				cube.FillRect(mColor, x+mTextW, y - mTextH, 2, mTextH ); 
				break;
			case 'I' :  //Draw I
				cube.FillRect(mColor, x + mTextW/2, y - mTextH, 2, mTextH  );
				cube.FillRect(mColor, x + 2,y - mTextH, mTextW-2,2);  
				cube.FillRect(mColor, x + 2,y - 2, mTextW-2,2);	
				break;
			case 'J' : //Draw J
				cube.FillRect(mColor, x + mTextW, y -2 - mTextH, 2, mTextH  );
				cube.FillRect(mColor, x + mTextW/2, y - mTextH, mTextW/2,2); 
				cube.FillRect(mColor, x + 2, y - 2, mTextW - 2, 2);
				cube.FillRect(mColor, x, y - 2  - mTextH/4, 2, mTextH/4 );
				break;
			case 'K' : //Draw K
				cube.FillRect(mColor,x, y - mTextH, 2, mTextH  );
				cube.FillRect(mColor,x + 2, y - 2 - mTextH/2,2, 2);
				cube.FillRect(mColor, x + 4, y - mTextH/2, 2,mTextH/4);
				cube.FillRect(mColor, x + 4, y - 3*mTextH/4, 2, mTextH/4 - 2);
				cube.FillRect(mColor, x + 6, y - mTextH, 2, mTextH/4);		
				cube.FillRect(mColor, x + 6, y - mTextH/4, 2, mTextH/4);	
				break;
			case 'L':  //Draw L
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + 2, y - 2, mTextW - 2, 2);
				
				break;
				
			case 'M' :  //Draw M
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + 2, y - mTextH, mTextW, 2);
				cube.FillRect(mColor, x + mTextW/2, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + mTextW, y- mTextH, 2, mTextH);	
				break;
				
			case 'N' :  //Draw N
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + 2, y + 2 - mTextH, 2,2);
				cube.FillRect(mColor, x + 4, y + 4 - mTextH, (mTextW-5)/2,(mTextH -5)/2);  
				cube.FillRect(mColor, x - 4 + mTextW, y -4 - (mTextH - 5)/2 , (mTextW-5)/2,(mTextH - 5)/2);
				cube.FillRect(mColor, x - 2 + mTextW, y -4, 2,2);
				cube.FillRect(mColor, x+ mTextW, y - mTextH, 2, mTextH);	
				break;
				
			case 'O' :	//Draw O
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + mTextW, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x+2, y - 2, mTextW, 2);
				cube.FillRect(mColor, x+2, y - mTextH, mTextW, 2);
				break;
				
			case 'P'  : //Draw P
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + mTextW, y + 2 - mTextH, 2, mTextH/2 - 2);
				cube.FillRect(mColor, x + 2, y - mTextH,   mTextW - 2, 2);
				cube.FillRect(mColor, x + 2, y - mTextH/2, mTextW - 2, 2);
				//cube.FillRect(mColor, x + mTextW, y - mTextH/2, 2, mTextH/2);
				break;
			case 'Q'  : //Draw Q
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + mTextW, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x+2, y - 2, mTextW, 2);
				cube.FillRect(mColor, x+2, y - mTextH, mTextW, 2);
				cube.FillRect(mColor, x - 4 + mTextW, y - mTextH/2, 2,mTextH/4);
				cube.FillRect(mColor, x -2 +  mTextW, y - mTextH/4, 2, mTextH/4);		
				break;
			case 'R'  : //Draw R
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x + mTextW, y + 2 - mTextH, 2, mTextH/2 - 2);
				cube.FillRect(mColor, x + 2, y - mTextH,   mTextW - 2, 2);
				cube.FillRect(mColor, x + 2, y - mTextH/2, mTextW - 2, 2);
				cube.FillRect(mColor, x - 2 + mTextW, y - mTextH/2, 2,mTextH/4);
				cube.FillRect(mColor, x     + mTextW, y - mTextH/4, 2, mTextH/4);	
				break;
			case 'S'  : //Draw S
				cube.FillRect(mColor, x + 2, y - mTextH, mTextW - 2, 2);	
				cube.FillRect(mColor, x, y + 2 - mTextH , 2, mTextH/2 - 4);
				cube.FillRect(mColor, x + 2, y - mTextH/2 - 2, mTextW - 4, 2);
				cube.FillRect(mColor, x + mTextW - 2, y - mTextH/2, 2, mTextH/2 - 2);	
				cube.FillRect(mColor, x , y - 2, mTextW - 2, 2);	
				break;
			case 'T'  : //Draw T
				cube.FillRect(mColor, x + mTextW/2 - 1, y - mTextH, 2, mTextH  );
				cube.FillRect(mColor, x ,y - mTextH, mTextW,2);  
				break;
			case 'U'  : //Draw U
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH - 2);
				cube.FillRect(mColor, x + mTextW, y- mTextH, 2, mTextH - 2);
				cube.FillRect(mColor, x+2, y - 2, mTextW - 2, 2);
				break;
			case 'V'  : //Draw V
				cube.FillRect(mColor, x - 4 + mTextW/2, y - mTextH, 2, mTextH - 4);	 
				cube.FillRect(mColor, x - 2 + mTextW/2,y - 4, 2, 2); 
				cube.FillRect(mColor, x + mTextW/2, y - 2, 2, 2);
				cube.FillRect(mColor, x + 2 + mTextW/2,y - 4, 2, 2);
				cube.FillRect(mColor, x + 4 + mTextW/2, y - mTextH, 2, mTextH - 4);	
				break;
			case 'W'  :	//Draw W			
				cube.FillRect(mColor, x, y- mTextH, 2, y);
				cube.FillRect(mColor, x + 2, y - 2 , mTextW, 2);
				cube.FillRect(mColor, x + mTextW/2, y- mTextH/2, 2, mTextH/2 );
				cube.FillRect(mColor, x + mTextW, y - mTextH, 2, y);
				break;
				
			case 'X'  : //Draw X
				cube.FillRect(mColor, x - 4 + mTextW/2, y - mTextH, 2, mTextH/2 - 2);	 
				cube.FillRect(mColor, x - 2 + mTextW/2, y - 2 - mTextH/2, 2, 2); 
				cube.FillRect(mColor, x + mTextW/2, y - mTextH/2, 2, 2);
				cube.FillRect(mColor, x + 2 + mTextW/2, y - 2 - mTextH/2, 2, 2);
				cube.FillRect(mColor, x + 4 + mTextW/2, y - mTextH, 2, mTextH/2 - 2);
				
				cube.FillRect(mColor, x - 4 + mTextW/2, y + 4- mTextH/2, 2, mTextH/2 - 4);	 
				cube.FillRect(mColor, x - 2 + mTextW/2, y + 2 - mTextH/2, 2, 2); 
				
				cube.FillRect(mColor, x + 2 + mTextW/2, y + 2 - mTextH/2, 2, 2);
				cube.FillRect(mColor, x + 4 + mTextW/2, y + 4- mTextH/2, 2, mTextH/2 - 4);	
				
				
				break;
			case 'Y'  : //Draw Y
				cube.FillRect(mColor, x - 4 + mTextW/2, y - mTextH, 2, mTextH/2 - 2);	 
				cube.FillRect(mColor, x - 2 + mTextW/2,y - 2 - mTextH/2, 2, 2); 
				cube.FillRect(mColor, x + mTextW/2, y - mTextH/2, 2, mTextH/2);
				cube.FillRect(mColor, x + 2 + mTextW/2,y - 2 - mTextH/2, 2, 2);
				cube.FillRect(mColor, x + 4 + mTextW/2, y - mTextH, 2, mTextH/2 - 2);
				break;
			case 'Z'  : //Draw Z
				cube.FillRect(mColor, x, y - mTextH, mTextW, 2);
				cube.FillRect(mColor, x - 2 + mTextW, y + 2 - mTextH, 2,2);
				cube.FillRect(mColor, x - 4 + mTextW, y + 4 - mTextH, (mTextW-5)/2,(mTextH -5)/2 -2);
				// cube.FillRect(mColor, x - 6 + mTextW, y + 4 - mTextH, 2, 2);	
				cube.FillRect(mColor, x + 4, y - 4 - (mTextH - 5)/2  , (mTextW-5)/2,(mTextH - 5)/2 - 4); 
				cube.FillRect(mColor, x + 2, y - 2 - (mTextH - 5)/2 ,  (mTextW-5)/2,(mTextH - 5)/2 - 2);	
				cube.FillRect(mColor, x, y -4, 2,2);
				cube.FillRect(mColor, x, y -2, mTextW, 2);	
				break;
			case '0'  : //Draw 0
				cube.FillRect(mColor, x, y + 2- mTextH, 2, mTextH - 4);
				cube.FillRect(mColor, x + mTextW, y + 2 - mTextH, 2, mTextH - 4);
				cube.FillRect(mColor, x+2, y - 2, mTextW - 2, 2);
				cube.FillRect(mColor, x+2, y - mTextH, mTextW - 2, 2);
				break;
			case '1'  : //Draw 1
				cube.FillRect(mColor, x + mTextW/2, y - mTextH, 2, mTextH  );
				cube.FillRect(mColor, x - 2 +mTextW/2 ,y - mTextH, 2,2);  
				cube.FillRect(mColor, x + 2,y - 2, mTextW-2,2);	
				break;		
			case '2'  : //Draw 2
				cube.FillRect(mColor, x , y - mTextH, mTextW - 2, 2);	
				cube.FillRect(mColor, x + mTextW - 2, y + 2 - mTextH , 2, mTextH/2 - 4);
				cube.FillRect(mColor, x + 2, y - mTextH/2 - 2, mTextW - 4, 2);
				cube.FillRect(mColor, x , y - mTextH/2, 2, mTextH/2 - 2);	
				cube.FillRect(mColor, x + 2, y - 2, mTextW - 4, 2);		
				break;
			case '3'  : //Draw 3
				cube.FillRect(mColor, x - 2 + mTextW, y + 2 - mTextH, 2, mTextH - 4);
				cube.FillRect(mColor, x, y - mTextH, mTextW - 2, 2);
				cube.FillRect(mColor, x - 2+ mTextW/2, y - mTextH/2, mTextW/2, 2);
				cube.FillRect(mColor, x, y - 2, mTextW - 2, 2);		
				break;
			case '4'  : //Draw 4
				cube.FillRect(mColor, x, y - mTextH, 2, mTextH/2  );   
				cube.FillRect(mColor, x + 2, y - mTextH/2, mTextW,2);  
				cube.FillRect(mColor, x -2 + mTextW, y - mTextH, 2, mTextH );	
				break;
			case '5'  : //Draw 5
				cube.FillRect(mColor, x , y - mTextH, mTextW , 2);	
				cube.FillRect(mColor, x, y + 2 - mTextH , 2, mTextH/2 - 4);
				cube.FillRect(mColor, x + 2, y - mTextH/2 - 2, mTextW - 4, 2);
				cube.FillRect(mColor, x + mTextW - 2, y - mTextH/2, 2, mTextH/2 - 2);	
				cube.FillRect(mColor, x , y - 2, mTextW - 2, 2);		
				break;
			case '6'  : //Draw 6
				cube.FillRect(mColor, x + 2, y - mTextH, mTextW - 2, 2);	
				cube.FillRect(mColor, x, y + 2 - mTextH , 2, mTextH - 4);
				cube.FillRect(mColor, x + 2, y - mTextH/2 - 2, mTextW - 4, 2);
				cube.FillRect(mColor, x + mTextW - 2, y - mTextH/2, 2, mTextH/2 - 2);	
				cube.FillRect(mColor, x + 2, y - 2, mTextW - 4, 2);		
				break;
			case '7'  : //Draw 7
				cube.FillRect(mColor, x - 2 + mTextW, y - mTextH, 2, mTextH);
				cube.FillRect(mColor, x , y - mTextH, mTextW - 2, 2);	
				break;
			case '8'  : //Draw 8
				cube.FillRect(mColor, x, y + 2- mTextH, 2, mTextH - 4);
				cube.FillRect(mColor, x - 2 + mTextW, y + 2 - mTextH, 2, mTextH - 4);
				cube.FillRect(mColor, x + 2, y - 1 - mTextH/2, mTextW - 4, 2);	
				cube.FillRect(mColor, x + 2, y - 2, mTextW - 4, 2);
				cube.FillRect(mColor, x + 2, y - mTextH, mTextW - 4, 2);	
				break;
			case '9'  : //Draw 9
				cube.FillRect(mColor, x - 2 + mTextW, y + 2 - mTextH, 2, mTextH - 2);
				cube.FillRect(mColor, x , y + 2 - mTextH, 2, mTextH/2 - 2);
				cube.FillRect(mColor, x + 2, y - mTextH,   mTextW - 4, 2);
				cube.FillRect(mColor, x + 2, y - mTextH/2, mTextW - 4, 2);	
				break;
			}
			
			
			
			
			
			
		}
		
		//todo: add support for sprite/image based fonts
		//todo: add support for extending and wrapping features
	}
}


