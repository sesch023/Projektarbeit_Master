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
        private readonly Keys _key;
        private readonly ActionType _actionType;

        private bool _wasHold;

        private delegate bool CheckType(KeyAction keyAction);

        private static readonly Dictionary<ActionType, CheckType> TypeDelegate;

        static KeyAction()
        {
            TypeDelegate = new Dictionary<ActionType, CheckType>();
            TypeDelegate[ActionType.KeyHold] = (keyAction) => { 
                keyAction._wasHold = CheckKeyHold(keyAction);
                return keyAction._wasHold;
            };
            TypeDelegate[ActionType.KeyDown] = (keyAction) => { 
                bool down = CheckKeyHold(keyAction);
                bool ret = !keyAction._wasHold && down;
                keyAction._wasHold = down;
                return ret;
            };
            TypeDelegate[ActionType.KeyUp] = (keyAction) => { 
                bool up = !CheckKeyHold(keyAction);
                bool ret = keyAction._wasHold && up;
                keyAction._wasHold = !up;
                return ret;
            };
            TypeDelegate[ActionType.KeyNotHold] = (keyAction) => { 
                keyAction._wasHold = CheckKeyHold(keyAction);
                return !keyAction._wasHold;
            };
        }

        public KeyAction(Keys key, ActionType actionType)
        {
            _actionType = actionType;
            _key = key;
        }
        
        public override void Update()
        {
            if (TypeDelegate[_actionType](this))
            {
                TriggerSubscribers();
            }
        }

        private static bool CheckKeyHold(KeyAction keyAction)
        {
            return Keyboard.GetState().IsKeyDown(keyAction._key);
        }
    }
}