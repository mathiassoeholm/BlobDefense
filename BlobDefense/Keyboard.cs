// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Gives easy accesebility to keyboard states with the provided static methods
//   Example taken from: http://www.switchonthecode.com/tutorials/winforms-accessing-mouse-and-keyboard-state
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TileEngine
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public abstract class Keyboard
    {
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        public static bool IsKeyDown(Keys key)
        {
            return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }

        public static bool IsKeyToggled(Keys key)
        {
            return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private static KeyStates GetKeyState(Keys key)
        {
            var state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            // If the high-order bit is 1, the key is down
            // otherwise, it is up.
            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            // If the low-order bit is 1, the key is toggled.
            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }
    }
}