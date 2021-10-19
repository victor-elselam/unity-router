using System;

namespace elselam.Navigation.Domain {
    public class NavigationException : Exception {
        public NavigationException(string message) : base(message) {
            
        }
    }
}