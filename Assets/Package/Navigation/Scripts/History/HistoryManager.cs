using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace elselam.Navigation.History {
    public class HistoryManager : IHistory {
        private readonly Stack<ScreenScheme> history;
        public bool HasHistory => history.Count > 0;

        [Inject]
        public HistoryManager() {
            history = new Stack<ScreenScheme>();
        }
        
        public bool Add(ScreenScheme screenScheme) {
            if (screenScheme == null)
                return false;

            history.Push(screenScheme);
            return true;
        }

        public ScreenScheme Back() {
            if (!HasHistory) {
                Debug.LogWarning("[UNavScreen] No Screens to go back");
                return null;
            }
            
            return history.Pop();
        }
    }
}