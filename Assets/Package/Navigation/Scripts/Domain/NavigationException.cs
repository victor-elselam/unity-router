using System;

namespace Elselam.UnityRouter.Domain
{
    public class NavigationException : Exception
    {
        public NavigationException(string message) : base(message)
        {

        }
    }
}