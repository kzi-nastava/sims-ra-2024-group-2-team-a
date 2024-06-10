using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for VideoPlayerControl.xaml
    /// </summary>
    public partial class VideoPlayerControl : UserControl {
        private DispatcherTimer timer;
        public Uri VideoSource {
            get { return mediaElement.Source; }
            set { mediaElement.Source = value; }
        }
        public VideoPlayerControl() {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            mediaElement.Play();
            timer.Start();
        }

        private void playButton_Click(object sender, RoutedEventArgs e) {
            mediaElement.Play();
            timer.Start();
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e) {
            mediaElement.Pause();
            timer.Stop();
        }

        private void progressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (mediaElement.NaturalDuration.HasTimeSpan) {
                TimeSpan ts = TimeSpan.FromSeconds(progressSlider.Value);
                mediaElement.Position = ts;
            }
        }

        private void Timer_Tick(object sender, EventArgs e) {
            if (mediaElement.NaturalDuration.HasTimeSpan) {
                progressSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                progressSlider.Value = mediaElement.Position.TotalSeconds;
            }
        }
    }
}
