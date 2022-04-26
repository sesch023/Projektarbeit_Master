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

    public class KeyAction : UpdatableEvent
    {
        private static readonly Dictionary<ActionType, CheckType> TypeDelegate;
        private readonly ActionType _actionType;
        private readonly Keys _key;

        private bool _wasHold;

        static KeyAction()
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

        public KeyAction(Keys key, ActionType actionType)
        {
            _actionType = actionType;
            _key = key;
        }
        
        public KeyAction(Keys key, ActionType actionType, Subscriber subscriber) : base(subscriber)
        {
            _actionType = actionType;
            _key = key;
        }

        public override void Update()
        {
            if (TypeDelegate[_actionType](this)) TriggerSubscribers();
        }

        private static bool CheckKeyHold(KeyAction keyAction)
        {
            return Keyboard.GetState().IsKeyDown(keyAction._key);
        }

        private delegate bool CheckType(KeyAction keyAction);
    }
}