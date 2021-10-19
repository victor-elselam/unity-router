namespace elselam.Navigation.History {
    public interface IHistory {
        bool HasHistory { get; }
        bool Add(ScreenScheme screenScheme);
        ScreenScheme Back();
    }
}