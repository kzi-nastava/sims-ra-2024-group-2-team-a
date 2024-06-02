using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.Utils.Commands
{
    public static class RoutedCommands {
        public static readonly RoutedUICommand Profile = new RoutedUICommand("Profile", "Profile", typeof(RoutedCommand));
        public static readonly RoutedUICommand Help = new RoutedUICommand("Help", "Help", typeof(RoutedCommand));
        public static readonly RoutedUICommand Cancel = new RoutedUICommand("Cancel", "Cancel", typeof(RoutedCommand));
        public static readonly RoutedUICommand Confirm = new RoutedUICommand("Confirm", "Confirm", typeof(RoutedCommand));
        public static readonly RoutedUICommand Clear = new RoutedUICommand("Clear", "Clear", typeof(RoutedCommand));
        public static readonly RoutedUICommand Filter = new RoutedUICommand("Filter", "Filter", typeof(RoutedCommand));
        public static readonly RoutedUICommand Clear1 = new RoutedUICommand("Clear1", "Clear1", typeof(RoutedCommand));
        public static readonly RoutedUICommand Filter1 = new RoutedUICommand("Filter11", "Filter", typeof(RoutedCommand));
        public static readonly RoutedUICommand AddDateTime = new RoutedUICommand("Add Date & Time", "AddDateTime", typeof(RoutedCommand));
        public static readonly RoutedUICommand AddPointOfInterest = new RoutedUICommand("Add Point of Interest", "AddPointOfInterest", typeof(RoutedCommand));
        public static readonly RoutedUICommand AddPictures = new RoutedUICommand("Add Pictures", "AddPictures", typeof(RoutedCommand));
        public static readonly RoutedUICommand DeleteDateTime = new RoutedUICommand("Delete Date & Time", "DeleteDateTime", typeof(RoutedCommand));
        public static readonly RoutedUICommand DeletePointOfInterest = new RoutedUICommand("Delete Point of Interest", "DeletePointOfInterest", typeof(RoutedCommand));
        public static readonly RoutedUICommand DeletePictures = new RoutedUICommand("Delete Pictures", "DeletePictures", typeof(RoutedCommand));
        public static readonly RoutedUICommand Stats = new RoutedUICommand("Stats", "Stats", typeof(RoutedCommand));
        public static readonly RoutedUICommand HamburgerBar = new RoutedUICommand("HamburgerBar", "HamburgerBar", typeof(RoutedCommand));
        public static readonly RoutedUICommand Add = new RoutedUICommand("Add", "Add", typeof(RoutedCommand));
        public static readonly RoutedUICommand Reviews = new RoutedUICommand("Reviews", "Reviews", typeof(RoutedCommand));
        public static readonly RoutedUICommand Home = new RoutedUICommand("Home", "Home", typeof(RoutedCommand));
        public static readonly RoutedUICommand Live = new RoutedUICommand("Live", "Live", typeof(RoutedCommand));
        public static readonly RoutedUICommand Finished = new RoutedUICommand("Finished", "Finished", typeof(RoutedCommand));
        public static readonly RoutedUICommand Requests = new RoutedUICommand("Requests", "Requests", typeof(RoutedCommand));
        public static readonly RoutedUICommand RequestsTabMenu = new RoutedUICommand("RequestsTabMenu", "RequestsTabMenu", typeof(RoutedCommand));
    }
}
