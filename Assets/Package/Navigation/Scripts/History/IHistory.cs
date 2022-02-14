namespace Elselam.UnityRouter.History
{
    public interface IHistory
    {
        /// <summary>
        /// Return if we have any screen to go back
        /// </summary>
        bool HasHistory { get; }

        /// <summary>
        /// Add screen scheme to history
        /// </summary>
        /// <param name="screenScheme">screen scheme to add in history</param>
        /// <returns>successful added in history</returns>
        bool Add(ScreenScheme screenScheme);

        /// <summary>
        /// Get last screen scheme and remove it from history
        /// </summary>
        /// <returns></returns>
        ScreenScheme Back();
    }
}