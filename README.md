TangibleStories
===============

Microsoft Surface application for CS320. 
The general design of this surface application contains a library of images and videos from which the user can select one media element at a time by clicking or touching the photo. The item them appears in the viewing area for the user to zoom in on and manipulate. Brief captions appear with the images and videos in the library, and extended captions appear when the images appear in the viewing area, allowing for a story to be added to each picture. The library only shows one media element type at a time (video or image), so to change between the two libraries, the user places either the camera  or video fiducial marker on the top left corner. To clear all media in the viewing area, the user presses the "Clear" button in the upper lefthand corner. 
These features are implemented using four surface controls: ScatterView, SurfaceListBox, SurfaceButton, and TagVisualizer. There is a separate class, MediaData, to hold the information for each media item.
