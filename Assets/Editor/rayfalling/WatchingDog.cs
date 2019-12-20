using System.Collections.Generic;
using System.IO;

namespace Assets.Editor.rayfalling {
    public class WatchingDog {
        public WatchingDog Instance;
        private List<string> _dict;
        public void Init() {
            if (Instance == null)
                Instance = this;
        }


    }
}