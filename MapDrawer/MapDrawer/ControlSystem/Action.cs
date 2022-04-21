using System.Collections.Generic;
using MapDrawer.EventSystem;
using Microsoft.Xna.Framework.Input;

namespace MapDrawer.ControlSystem
{
    public enum Type
    {   
        KeyHold,
        KeyNotHold,
        KeyDown,
        KeyUp
    }

    public class KeyAction : UpdatableEvent
    {
        private readonly Keys _key;
        private readonly Type _type;

        private bool _wasHold;

        private delegate bool CheckType(KeyAction keyAction);

        private static readonly Dictionary<Type, CheckType> TypeDelegate;

        static KeyAction()
        {
            TypeDelegate = new Dictionary<Type, CheckType>();
            TypeDelegate[Type.KeyHold] = (keyAction) => { 
                keyAction._wasHold = CheckKeyHold(keyAction);
                return keyAction._wasHold;
            };
            TypeDelegate[Type.KeyDown] = (keyAction) => { 
                bool down = CheckKeyHold(keyAction);
                bool ret = !keyAction._wasHold && down;
                keyAction._wasHold = down;
                return ret;
            };
            TypeDelegate[Type.KeyUp] = (keyAction) => { 
                bool up = !CheckKeyHold(keyAction);
                bool ret = keyAction._wasHold && up;
                keyAction._wasHold = !up;
                return ret;
            };
            TypeDelegate[Type.KeyNotHold] = (keyAction) => { 
                keyAction._wasHold = CheckKeyHold(keyAction);
                return !keyAction._wasHold;
            };
        }

        public KeyAction(Keys key, Type type)
        {
            _type = type;
            _key = key;
        }
        
        public override void Update()
        {
            if (TypeDelegate[_type](this))
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