using System;

namespace Aiv.Fast3D.Component {


    public delegate void EventTemplate (EventArgs message);

    public static class EventHandlerManager {

        private static EventTemplate[] gameEvents;

        //deve essere chiamata prima di far partire il gioco, direi nel Main del program.
        public static void Init (int numberOfEvents) {
            gameEvents = new EventTemplate[numberOfEvents];
        }

        public static void AddListener (int eventID, EventTemplate listener) {
            gameEvents[eventID] += listener;
        }

        public static void RemoveListener (int eventID, EventTemplate listener) {
            gameEvents[eventID] -= listener;
        }

        public static void CastEvent (int eventID, EventArgs message) {
            gameEvents[eventID]?.Invoke (message);
        }

        public static void ClearListeners () {
            for (int i = 0; i < gameEvents.Length; i++) {
                gameEvents[i] = null;
            }
        }

    }
}

