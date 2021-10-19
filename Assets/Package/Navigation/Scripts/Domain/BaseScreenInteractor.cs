using System.Collections.Generic;

namespace elselam.Navigation.Domain {
    public abstract class BaseScreenInteractor : IScreenInteractor {
        public virtual void OnEnter() {
        }
        public virtual void WithParameters(IDictionary<string, string> parameters) {
        }
        
        public virtual IDictionary<string, string> OnExit() { 
            return null;
        }
    }
}