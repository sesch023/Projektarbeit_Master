using System.Collections.Generic;
using MapDrawer.EventSystem;
using Microsoft.Xna.Framework.Input;

namespace MapDrawer.ControlSystem
{
    public enum ActionType
    {
        KeyHold,
        KeyNotHold,
        KeyDown,
        KeyUp
    }

    public class KeyboardAction : TimeSpacedEvent
    {
        private static readonly Dictionary<ActionType, CheckType> TypeDelegate;
        private readonly ActionType _actionType;
        private readonly Keys _key;

        private bool _wasHold;

        static KeyboardAction()
        {
            TypeDelegate = new Dictionary<ActionType, CheckType>();
            TypeDelegate[ActionType.KeyHold] = keyAction =>
            {
                keyAction._wasHold = CheckKeyHold(keyAction);
                return keyAction._wasHold;
            };
            TypeDelegate[ActionType.KeyDown] = keyAction =>
            {
                var down = CheckKeyHold(keyAction);
                var ret = !keyAction._wasHold && down;
                keyAction._wasHold = down;
                return ret;
            };
            TypeDelegate[ActionType.KeyUp] = keyAction =>
            {
                var up = !CheckKeyHold(keyAction);
                var ret = keyAction._wasHold && up;
                keyAction._wasHold = !up;
                return ret;
            };
            TypeDelegate[ActionType.KeyNotHold] = keyAction =>
            {
                keyAction._wasHold = CheckKeyHold(keyAction);
                return !keyAction._wasHold;
            };
        }

        public KeyboardAction(Keys key, ActionType actionType, long timeSpacing = 0) : base(timeSpacing)
        {
            _actionType = actionType;
            _key = key;
        }
        
        public KeyboardAction(Keys key, ActionType actionType, Subscriber subscriber, long timeSpacing = 0) 
            : base(timeSpacing, subscriber)
        {
            _actionType = actionType;
            _key = key;
        }
        
        public KeyboardAction(Keys key, ActionType actionType, IEnumerable<Subscriber> subscribers, long timeSpacing = 0) 
            : base(timeSpacing, subscribers)
        {
            _actionType = actionType;
            _key = key;
        }

        public override void Update()
        {
            if (TypeDelegate[_actionType](this)) base.Update();
        }

        private static bool CheckKeyHold(KeyboardAction keyboardAction)
        {
            return Keyboard.GetState().IsKeyDown(keyboardAction._key);
        }

        private delegate bool CheckType(KeyboardAction keyboardAction);
    }
}