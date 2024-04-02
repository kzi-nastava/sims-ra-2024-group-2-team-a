using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Commands {
    public static class RoutedCommands {

        public static readonly RoutedUICommand AcceptButtonCommand = new RoutedUICommand(
            "Disables accept/decline button",
            "AcceptButtonCommand",
            typeof(RoutedCommands),
            null
            );

        public static readonly RoutedUICommand DeclineButtonCommand = new RoutedUICommand(
            "Disables accept/decline button",
            "DeclineButtonCommand",
            typeof(RoutedCommands),
            null
            );
    }
}
