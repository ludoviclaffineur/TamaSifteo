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
using _Sorter;

namespace Sorter {

  public class SorterApp : BaseApp {

    public List<CubeWrapper> mWrappers = new List<CubeWrapper>();
    public Random mRandom = new Random();

    // Here we initialize our app.
    public override void Setup() {
	  int i =0;		
	  //Log.Debug("YOUOOOOUUUUPS");

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
  

  

	
 //class presently writes text at x,y location 
  
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

