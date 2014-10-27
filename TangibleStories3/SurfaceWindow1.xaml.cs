// Galen Chuang, CS320, Fall 2014, 10/27/14

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.IO;

namespace TangibleStories3
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        private ObservableCollection<MediaData> SurfaceListBoxPhotos;
        private ObservableCollection<MediaData> SurfaceListBoxVideos;

        private string pwd;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            SurfaceListBoxPhotos = new ObservableCollection<MediaData>();
            SurfaceListBoxVideos = new ObservableCollection<MediaData>();

            pwd = System.IO.Directory.GetCurrentDirectory();
            pwd = pwd.Remove(pwd.IndexOf("bin\\Debug"));

            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

            InfoLabel.Content = "Showing photos";
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = this;

            SurfaceListBoxPhotos.Add(new MediaData("Images/Chicken.png", "Chicken dance", "The Bluths make chicken noises and motions at home.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Family.jpg", "The Bluths", "The Bluths look nice for once.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Jail.jpg", "Oscar in jail", "What a surprise... Oscar's back in jail.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Lindsey.png", "Lindsey", "This is Lindsey's resting face. Or basically the face she makes when Tobias says anything.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Magic.png", "Gob and Buster perform", "Another one of Gob's magic shows. Don't think too much about what Buster is wearing, though.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Musclesuit.png", "Muscle suit", "George Michael wears a muscle suit for the Bluths' annual live tableau re-creation.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Sheep.jpg", "Buster and a friend", "A sheep appears in a photobooth with Buster. What went on later is anybody's guess.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Wheelchair.png", "Maeby", "Once, Maeby pretended to be disabled to prove beauty pageants were only about looks... Much to her displeasure, she won.", "none"));
            SurfaceListBoxPhotos.Add(new MediaData("Images/Wink.png", "Wink", "It never ends well when Lucille tries to wink.", "none"));

            SurfaceListBoxVideos.Add(new MediaData("Thumbs/Blue_thumb.jpg", "\"I blue myself,\" was perhaps not the most well-thought out thing Tobias has ever said.", "video", "Videos/Blue.mp4"));
            SurfaceListBoxVideos.Add(new MediaData("Thumbs/Chicken_thumb.png", "The Bluths do their chicken dances and noises.", "video", "Videos/Chicken.mp4"));
            SurfaceListBoxVideos.Add(new MediaData("Thumbs/Wink_thumb.png", "Lucille winks... why do we have a video of this?", "video", "Videos/Wink.mp4"));

            surfaceListBox1.ItemsSource = SurfaceListBoxPhotos;
        }
        
        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {
            String tag = e.TagVisualization.VisualizedTag.Value.ToString();

            if (tag.Equals("9")) 
            {
                InfoLabel.Content = "Showing videos";
                foreach (MediaData item in SurfaceListBoxPhotos)
                {
                    item.Visibility = Visibility.Hidden;
                    surfaceListBox1.ItemsSource = SurfaceListBoxVideos;
                }
            }
            else if (tag.Equals("8")) { //default
                InfoLabel.Content = "Showing photos";
            
                surfaceListBox1.ItemsSource = SurfaceListBoxPhotos;
            }
        }     

        private void ClearMedia(object sender, RoutedEventArgs e)
        {
            scatterView1.Items.Clear();
        }

        #region AddMedia
        private void AddMedia(object sender, RoutedEventArgs e)
        {
            string targetVideo = pwd;

                Image image = new Image(); //to add to scatterview
                //System.Type type = sender.GetType();
                Image photo = (Image)sender;
                String uri = photo.Source.ToString();
                String junk = "pack://application:,,,/TangibleStories3;component/";
                int index = junk.Length;
                String actualUri = uri.Substring(index);



            //check if should add image or video
                if (SurfaceListBoxPhotos.Any(item => item.Source.Equals(actualUri))) //add image
                {
                    image.Source = new BitmapImage(new Uri(actualUri, UriKind.Relative));
                    //scatterView1.Items.Add(image);
                    //add caption
                    foreach (MediaData p in SurfaceListBoxPhotos)
                    {
                        if (p.Source.Equals(actualUri))
                        {
                        string story = p.Story;
                            
                        scatterView1.Items.Add(new MediaData(p.Source,p.Caption, p.Story, p.LinkedVid));
                        }
                    }
                }
                else //add video
                {
                    foreach (MediaData movie in SurfaceListBoxVideos)
                    {
                        if (movie.Source.Equals(actualUri))
                        {
                            targetVideo += movie.LinkedVid;
                        }
                    }
                    ScatterViewItem vid = new ScatterViewItem();
                    scatterView1.Items.Add(vid);

                    // Create a MediaElement object.
                    MediaElement video = new MediaElement();

                    video.LoadedBehavior = MediaState.Manual;
                    //video.UnloadedBehavior = MediaState.Manual;

                    // The media dimensions are not available until the MediaOpened event.
                    video.MediaOpened += delegate
                    {
                        // Size the ScatterViewItem control according to the video size.
                        vid.Width = 500;
                        vid.Height = 281.25;

                    };

                    // Set the Content to the video.
                    vid.Content = video;

                    // Get the video if it exists.
                    if (System.IO.File.Exists(targetVideo))
                    {
                        video.Source = new Uri(targetVideo);
                        video.Play();
                    }
                    else
                    {
                        vid.Content = "Video not found";
                    }
                }

            ////for video
            //MediaElement video = new MediaElement(); //to add to scatterview

            //MediaElement vid = (MediaElement)sender;

        }
        #endregion

        #region pre-added
        /// Pre-added methods.-------------------------------------------------------------------------
        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TagVisualizer1_VisualizationInitialized(object sender, TagVisualizerEventArgs e)
        {

        }
    }

        #endregion
}