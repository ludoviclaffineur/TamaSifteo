using System;
using Sorter;
using Sifteo;
using System.Collections;

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
		public int activite_index=0;
		public int nourriture_index=0;
		public int mXval=0;
		public int mYval=0;
		public string [] Nourritures = {"hamburger","fruit","legume", "soda", "viande", "glace","cafe", "alcool", "eau"};
		public string [] Activites = {"parc","culture","boite_de_nuit","cinema","sdb"};
		public static int selectedColor;
		public Tamagotchi tama;
		public myText mText = new myText();
		public ArrayList NourrituresRecues;
		public ArrayList ActivitesEnCours;
		int mSpriteIndex;

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
			NourrituresRecues = new ArrayList();
			ActivitesEnCours = new ArrayList();
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
		private void OnShakeStarted (Cube cube)
		{
			Log.Debug ("Shake start");
			if (mCubeType == 0) {
				mSpriteIndex = 1;
			}
			if (mCubeType == 1) {
				activite_index = (activite_index + 1) % 5;
			}
			if (mCubeType == 2) {
				nourriture_index = (nourriture_index + 1) % 9;
			}
			mNeedDraw = true;
			
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
			mSpriteIndex = 0;

			
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
		public void DrawCube ()
		{

			Color bgColor = new Color (36, 182, 255);
			if (mCubeType == 0) {// color display(0) 
				mCube.FillScreen (bgColor);
				mCube.Image ("buddy", 40, 24, 0, mSpriteIndex * 48, 32, 48, 1, 0);
				
				
			} else if (mCubeType == 1) {// color selector(1),
					
				mCube.FillScreen (bgColor);
				mCube.Image (Activites [activite_index], 0, 0, 0, mSpriteIndex * 48, 110, 110, 1, 0);

				
				
			} else if (mCubeType == 2) {// color Palette(2)

				mCube.FillScreen (bgColor);
				mCube.Image (Nourritures [nourriture_index], 5, 5, 0, mSpriteIndex * 48, 110, 110, 1, 0);
			} else if (mCubeType == 3) {
				mCube.FillScreen(bgColor);
				mText.setText("SANTE "+ tama.SanteDouble);
				mText.setStringOrig(2,16);
				mText.writeText(mCube);	
				mText.setText("HUMEUR "+ tama.HumeurDouble);
				mText.setStringOrig(2,32);
				mText.writeText(mCube);
				mText.setText("FAIM "+ tama.FaimDouble);
				mText.setStringOrig(2,48);
				mText.writeText(mCube);
				mText.setText("INTEL "+ tama.IntelligenceDouble);
				mText.setStringOrig(2,64);
				mText.writeText(mCube);
				mText.setText("PROPR "+ tama.PropreteDouble);
				mText.setStringOrig(2,80);
				mText.writeText(mCube);
				mText.setText("AGE "+ tama.AgeDouble);
				mText.setStringOrig(2,96);
				mText.writeText(mCube);
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