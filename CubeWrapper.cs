using System;
using Sorter;
using Sifteo;

namespace _Sorter
{
	public class CubeWrapper {
		
		public SorterApp mApp;
		public Cube mCube;
		public int mIndex;
		public int mXOffset = 0;
		public int mYOffset = 0;
		public int mScale = 1;
		public int mRotation = 0;
		public int mCubeType = 0;  //for this code I have 3 cubes, color display(0), color selector(1), and color Palette(2) 
		public int R;  //red
		public int G;  //green
		public int B;  //blue
		public int mXval=0;
		public int mYval=0;
		public static int selectedColor;
		public myText mText = new myText();
		// This flag tells the wrapper to redraw the current image on the cube. (See Tick, below).
		public bool mNeedDraw = false;
		
		public CubeWrapper(SorterApp app, Cube cube) {
			mApp = app;
			mCube = cube;
			mCube.userData = this;
			mIndex = 0;
			// mText = new myText();
			// mText.setText("test this");
			// Here we attach more event handlers for button and accelerometer actions.
			mCube.ButtonEvent += OnButton;
			mCube.TiltEvent += OnTilt;
			mCube.ShakeStartedEvent += OnShakeStarted;
			mCube.ShakeStoppedEvent += OnShakeStopped;
			mCube.FlipEvent += OnFlip;
		}
		
		
		// ## Button ##
		// This is a handler for the Button event. It is triggered when a cube's
		// face button is either pressed or released. The `pressed` argument
		// is true when you press down and false when you release.
		private void OnButton(Cube cube, bool pressed) {
			if (pressed) {
				Log.Debug("Button pressed");
			} else {
				Log.Debug("Button released");
				
				
				mRotation = 0;
				mScale = 1;
				mNeedDraw = true;
				
			}
		}
		
		// ## Tilt ##
		// This is a handler for the Tilt event. It is triggered when a cube is
		// tilted past a certain threshold. The x, y, and z arguments are filtered
		// values for the cube's three-axis acceleromter. A tilt event is only
		// triggered when the filtered value changes, i.e., when the accelerometer
		// crosses certain thresholds.
		private void OnTilt(Cube cube, int tiltX, int tiltY, int tiltZ) {
			Log.Debug("Tilt: {0} {1} {2}", tiltX, tiltY, tiltZ);
			
			// If the X axis tilt reads 0, the cube is tilting to the left. <br/>
			// If it reads 1, the cube is centered. <br/>
			// If it reads 2, the cube is tilting to the right.
			if (tiltX == 0) {
				mXOffset = -8;
			} else if (tiltX == 1) {
				mXOffset = 0;
			} else if (tiltX == 2) {
				mXOffset = 8;
			}
			
			// If the Y axis tilt reads 0, the cube is tilting down. <br/>
			// If it reads 1, the cube is centered. <br/>
			// If it reads 2, the cube is tilting up.
			if (tiltY == 0) {
				mYOffset = 8;
			} else if (tiltY == 1) {
				mYOffset = 0;
			} else if (tiltY == 2) {
				mYOffset = -8;
			}
			
			// If the Z axis tilt reads 2, the cube is face up. <br/>
			// If it reads 1, the cube is standing on a side. <br/>
			// If it reads 0, the cube is face down.
			if (tiltZ == 1) {
				mXOffset *= 2;
				mYOffset *= 2;
			}
			
			mNeedDraw = true;
		}
		
		// ## Shake Started ##
		// This is a handler for the ShakeStarted event. It is triggered when the
		// player starts shaking a cube. When the player stops shaking, a
		// corresponding ShakeStopped event will be fired (see below).
		//
		// Note: while a cube is shaking, it will still fire tilt and flip events
		// as its internal accelerometer goes around and around. If your game wants
		// to treat shaking separately from tilting or flipping, you need to add
		// logic to filter events appropriately.
		private void OnShakeStarted(Cube cube) {
			Log.Debug("Shake start");
			if (mCubeType ==0){
				selectedColor = mApp.mRandom.Next(256);
				OSC MessageOSC = new OSC ("127.0.0.1",65000);
				MessageOSC.sendMsg("/sifteo",mCubeType,selectedColor);
			}
			
		}
		
		// ## Shake Stopped ##
		// This is a handler for the ShakeStarted event. It is triggered when the
		// player stops shaking a cube. The `duration` argument tells you
		// how long (in milliseconds) the cube was shaken.
		private void OnShakeStopped (Cube cube, int duration)
		{
			Log.Debug ("Shake stop: {0}", duration);
			mRotation = 0;
			mNeedDraw = true;
			
			
		}
		
		// ## Flip ##
		// This is a handler for the Flip event. It is triggered when the player
		// turns a cube face down or face up. The `newOrientationIsUp` argument
		// tells you which way the cube is now facing.
		//
		// Note that when a Flip event is triggered, a Tilt event is also
		// triggered.
		private void OnFlip(Cube cube, bool newOrientationIsUp) {
			if (newOrientationIsUp) {
				Log.Debug("Flip face up");
				mScale = 1;
				mNeedDraw = true;
			} else {
				Log.Debug("Flip face down");
				mScale = 2;
				mNeedDraw = true;
			}
		}
		
		// move color when cube is added 
		public void onCubeAdd(Cube cube, Cube.Side side1){
			int tempColor=0;
			if (mCubeType == 2) {
				// move marker towards side
				switch ((int)side1){ 
				case  0: // * TOP = 0
					
				mYval= (mYval == 0)? 15: (mYval-1);
					break;
				case 1: // * LEFT = 1
				mXval= (mXval ==0)? 15: (mXval -1);
					break;
				case 2: // * BOTTOM = 2
					
					mYval= (mYval +1)%16;
					break;
				case 3: // * RIGHT = 3
					mXval= (mXval +1)%16;
					break;
				default : // * NONE = 4
					// do nothing
					break;
				}
				tempColor = mXval << 4 | mYval; 
				selectedColor = tempColor;
				
				// redraw cubes
				foreach (CubeWrapper wrapper in mApp.mWrappers) {
					wrapper.mNeedDraw = true;
				}
			}
			
		}		
		
		
		// ## Cube.Image ##
		// This method draws the current image to the cube's display. The
		// Cube.Image method has a lot of arguments, but many of them are optional
		// and have reasonable default values.
		public void DrawCube() {
			int x;
			int y;
			int tempColor = 0;
			Color color;
			if (mCubeType == 0){// color display(0) 
				
				color = new Color(selectedColor);
				mCube.FillScreen(color);
				
				
			}
			else if (mCubeType == 1){// color selector(1),
				// Clear off whatever was previously on the display before drawing the new image.
				mCube.FillScreen(Color.Black);
				color = new Color(0,182,0);
				
				mCube.FillRect(color,0,58,16,16);
				
				color = new	Color(255,0,0);		
				mText.printChar(mCube, 'R', 25, 30, color);
				
				color = new Color(0,255,0);	
				mText.printChar(mCube, 'G', 25, 60, color);
				
				color = new Color(0,0,255);		
				mText.printChar(mCube, 'B', 25, 90, color);	
				
				R = (int)selectedColor >> 5 & 0x07;//Red bits are 3msb of x 
				R = (int)(R *36.43);
				mText.setText(	R.ToString());
				mText.setStringOrig(45,30);
				mText.writeText(mCube);
				G = ((int)selectedColor >> 2) & 0x07;//Green bits are 2 msbs of y + lsb of x
				G = (int)(G *36.43);
				mText.setText(	G.ToString());
				mText.setStringOrig(45,60);
				mText.writeText(mCube);		
				B = (int)selectedColor & 0x03;// blue bits are 2lsb of y
				B = (int)(B *85);
				mText.setText(	B.ToString());
				mText.setStringOrig(45,90);
				mText.writeText(mCube);	
				
				
				Log.Debug("R = {0}, G = {1}, B = {2}",R,G,B);		
				
				
				
			} 
			else if (mCubeType ==2){// color Palette(2)
				for (x=0; x<16; x++){
					for(y=0; y<16; y++){
						R = x >> 1;//Red bits are 3msb of x 
						G = (y >> 2) | ((x << 3) & 0x4);//Green bits are 2 msbs of y + lsb of x
						B = y & 0x03;// blue bits are 2lsb of y
						//Log.Debug("R = {0}, G = {1}, B = {2}, x = {3}, y = {4}",R,G,B,x,y);
						
						tempColor = x << 4 | y; 		
						
						color  = new Color(tempColor);
						mCube.FillRect(color,x*8,y*8,8,8);
						
						//draw marker on selected color
						if (tempColor == selectedColor){
							tempColor = ~tempColor & 0xF;
							mXval = x;
							mYval = y;
							
							color  = new Color(tempColor);
							mCube.FillRect(color,x*8,y*8,8,3); //draw complement of selected color as marker
						}
						
					}
					
				}
				
			}
			
			
			
			
			
			// Remember: always call Paint if you actually want to see anything on the cube's display.
			mCube.Paint();
		}
		
		// This method is called every frame by the Tick in SlideShowApp (see above.)
		public void Tick() {
			
			// You can check whether a cube is being shaken at this moment by looking
			// at the IsShaking flag.
			if (mCube.IsShaking && mCubeType == 0) {
				mRotation = mApp.mRandom.Next(4);
				foreach (CubeWrapper wrapper in mApp.mWrappers) {
					wrapper.mNeedDraw = true;
				}
			}
			
			// If anyone has raised the mNeedDraw flag, redraw the image on the cube.
			if (mNeedDraw) {
				mNeedDraw = false;
				DrawCube();
			}
		}
}

}