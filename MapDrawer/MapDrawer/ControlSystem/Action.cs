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
        protected Keys Key;
        protected Type Type;

        private bool _wasPressed = false;

        public delegate bool CheckType(KeyAction keyAction);

        private static Dictionary<Type, CheckType> TypeDelegate;

        static KeyAction()
        {
            TypeDelegate = new Dictionary<Type, CheckType>();
            TypeDelegate[Type.KeyHold] = CheckKeyHold;
            TypeDelegate[Type.KeyDown] = CheckKeyDown;
            TypeDelegate[Type.KeyUp] = CheckKeyUp;
            TypeDelegate[Type.KeyNotHold] = CheckKeyNotHold;
        }

        public KeyAction(Keys key, Type type)
        {
            Type = type;
            Key = key;
        }
        
        public override void Update()
        {
            if (TypeDelegate[Type](this))
            {
                TriggerSubscribers();
            }
        }

        private static bool CheckKeyHold(KeyAction keyAction)
        {
            return Keyboard.GetState().IsKeyDown(keyAction.Key);
        }
        
        private static bool CheckKeyNotHold(KeyAction keyAction)
        {
            return !CheckKeyHold(keyAction);
        }

        private static bool CheckKeyDown(KeyAction keyAction)
        {
            return CheckKeyHold(keyAction) && !keyAction._wasPressed;
        }
        
        private static bool CheckKeyUp(KeyAction keyAction)
        {
            return CheckKeyNotHold(keyAction) && keyAction._wasPressed;
        }
    }
}