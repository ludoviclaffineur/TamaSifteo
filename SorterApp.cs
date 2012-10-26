// This code was modified from the slideshow demo
// Modified By: Wilton Burke
// Modified on: Oct 7 2011

// Purpose:
// Slide Show Demo modified to show colors and color codes of displayed color
// one cube controls the selection of a color on the palette cube 
// just set the cube with the green dot on the palette cubes sides, the currently
// selected color is marked on the palette by a small slice of its inverted color.
// this means the selection marker changes colors but can always be seen.
// shake the solid color cube to get a new random color
// the final cube displays the color selected along with color code rrrgggbb


using System;
using System.Collections;
using System.Collections.Generic;
using Sifteo;

namespace Sorter {

  public class SorterApp : BaseApp {

    public List<CubeWrapper> mWrappers = new List<CubeWrapper>();
    public Random mRandom = new Random();

    // Here we initialize our app.
    public override void Setup() {
	  int i =0;		
      

      // Loop through all the cubes and set them up.
      foreach (Cube cube in CubeSet) {

        // Create a wrapper object for each cube. The wrapper object allows us
        // to bundle a cube with extra information and behavior.
        CubeWrapper wrapper = new CubeWrapper(this, cube);
				wrapper.mCubeType = i; //set each cube as a cube type 
				i++;
        mWrappers.Add(wrapper);
        wrapper.DrawCube();
      }

      // ## Event Handlers ##
      // Objects in the Sifteo API (particularly BaseApp, CubeSet, and Cube)
      // fire events to notify an app of various happenings, including actions
      // that the player performs on the cubes.
      //
      // To listen for an event, just add the handler method to the event. The
      // handler method must have the correct signature to be added. Refer to
      // the API documentation or look at the examples below to get a sense of
      // the correct signatures for various events.
      //
      // **NeighborAddEvent** and **NeighborRemoveEvent** are triggered when
      // the player puts two cubes together or separates two neighbored cubes.
      // These events are fired by CubeSet instead of Cube because they involve
      // interaction between two Cube objects. (There are Cube-level neighbor
      // events as well, which comes in handy in certain situations, but most
      // of the time you will find the CubeSet-level events to be more useful.)
      CubeSet.NeighborAddEvent += OnNeighborAdd;
      CubeSet.NeighborRemoveEvent += OnNeighborRemove;
    }

    // ## Neighbor Add ##
    // This method is a handler for the NeighborAdd event. It is triggered when
    // two cubes are placed side by side.
    //
    // Cube1 and cube2 are the two cubes that are involved in this neighboring.
    // The two cube arguments can be in any order; if your logic depends on
    // cubes being in specific positions or roles, you need to add logic to
    // this handler to sort the two cubes out.
    //
    // Side1 and side2 are the sides that the cubes neighbored on.
    private void OnNeighborAdd(Cube cube1, Cube.Side side1, Cube cube2, Cube.Side side2)  {
      Log.Debug("Neighbor add: {0}.{1} <-> {2}.{3}", cube1.UniqueId, side1, cube2.UniqueId, side2);

      CubeWrapper wrapper = (CubeWrapper)cube1.userData;
      if (wrapper != null) {
        // Here we set our wrapper's rotation value so that the image gets
        // drawn with its top side pointing towards the neighbor cube.
        //
        // Cube.Side is an enumeration (TOP, LEFT, BOTTOM, RIGHT, NONE). The
        // values of the enumeration can be cast to integers by counting
        // counterclockwise:
        //
        // * TOP = 0
        // * LEFT = 1
        // * BOTTOM = 2
        // * RIGHT = 3
        // * NONE = 4
		//wrapper.
			wrapper.onCubeAdd(cube1, side1);
        wrapper.mRotation = (int)side1;
        wrapper.mNeedDraw = true;
      }

      wrapper = (CubeWrapper)cube2.userData;
      if (wrapper != null) {
				wrapper.onCubeAdd(cube2, side2);
        wrapper.mRotation = (int)side2;
        wrapper.mNeedDraw = true;
      }

    }

    // ## Neighbor Remove ##
    // This method is a handler for the NeighborRemove event. It is triggered
    // when two cubes that were neighbored are separated.
    //
    // The side arguments for this event are the sides that the cubes
    // _were_ neighbored on before they were separated. If you check the
    // current state of their neighbors on those sides, they should of course
    // be NONE.
    private void OnNeighborRemove(Cube cube1, Cube.Side side1, Cube cube2, Cube.Side side2)  {
      Log.Debug("Neighbor remove: {0}.{1} <-> {2}.{3}", cube1.UniqueId, side1, cube2.UniqueId, side2);

      CubeWrapper wrapper = (CubeWrapper)cube1.userData;
      if (wrapper != null) {
        wrapper.mScale = 1;
        wrapper.mRotation = 0;
        wrapper.mNeedDraw = true;
      }

      wrapper = (CubeWrapper)cube2.userData;
      if (wrapper != null) {
        wrapper.mScale = 1;
        wrapper.mRotation = 0;
        wrapper.mNeedDraw = true;
      }
    }

    // Defer all per-frame logic to each cube's wrapper.
    public override void Tick() {
      foreach (CubeWrapper wrapper in mWrappers) {
        wrapper.Tick();
      }
    }

		
   // development mode only
    // start Dice as an executable and run it, waiting for Siftrunner to connect
    static void Main(string[] args) { new SorterApp().Run(); }
  }

  // ------------------------------------------------------------------------

  // ## Wrapper ##
  // "Wrapper" is not a specific API, but a pattern that is used in many Sifteo
  // apps. A wrapper is an object that bundles a Cube object with game-specific
  // data and behaviors.
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

  

	
 //class presently writes text at x,y location 
  
    public class myText
    { 
     private int mX = 0;
     private int mY = 20;
	 private int mTextH = 20;
	 private int mTextW = 10;
	 private bool mExtend = false; //extend allows the text string to be wrote across many cubes
	 private bool mWrap = false;   //wrap allows the text to wrap across the screen if extend is 
		                         //enabled wrap will only wrap after reaching the end of the cubes
     private string mString = "0123456789";
	 private Color mColor;	
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
}

// -----------------------------------------------------------------------
//
// SlideShowApp.cs
//
// Copyright &copy; 2011 Sifteo Inc.
//
// This program is "Sample Code" as defined in the Sifteo
// Software Development Kit License Agreement. By adapting
// or linking to this program, you agree to the terms of the
// License Agreement.
//
// If this program was distributed without the full License
// Agreement, a copy can be obtained by contacting
// support@sifteo.com.
//

