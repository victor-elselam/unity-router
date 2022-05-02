using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.History
{
    public class HistoryManager : IHistory
    {
        private readonly List<Stack<ScreenScheme>> history;
        private Stack<ScreenScheme> mainFlow => history.First();
        private Stack<ScreenScheme> currentFlow => history.Last();

        public bool HasHistory => HasHistoryInFlow(mainFlow);
        private bool HasHistoryInFlow(Stack<ScreenScheme> flow) => flow.Count > 0;

        [Inject]
        public HistoryManager()
        {
            history = new List<Stack<ScreenScheme>>();
            history.Add(new Stack<ScreenScheme>());
        }

        public bool Add(ScreenScheme screenScheme)
        {
            if (screenScheme == null)
                return false;

            currentFlow.Push(screenScheme);
            return true;
        }

        public ScreenScheme Back()
        {
            if (currentFlow == mainFlow && !HasHistoryInFlow(mainFlow))
            {
                Debug.LogWarning($"[Unity-Router] No Screens to go back");
                return null;
            }
            else
            {
                if (!HasHistoryInFlow(currentFlow))
                    CloseSubflow();
            }

            return currentFlow.Pop();
        }

        public void OpenSubflow() => history.Add(new Stack<ScreenScheme>());

        public bool CloseSubflow()
        {
            if (history.Count == 1)
            {
                Debug.LogWarning("[Unity-Router] Trying to close MainFlow! This is not allowed");
                return false;
            }

            history.Remove(currentFlow);
            return true;
        }
    }
}