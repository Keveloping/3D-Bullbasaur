using System;
using System.Collections.Generic;
using Aiv.Fast2D;

namespace Aiv.Fast3D.Component {


    public enum MouseButton {
        leftButton,
        rightButton,
        middleButton,
        button1,
        button2,
        button3,
        button4,
        button5,
        button6,
        button7,
        button8,
        button9
    }

    public enum JoystickButton {
        buttonA,
        buttonB,
        buttonX,
        buttonY,
        buttonLeft,
        buttonRight,
        buttonUp,
        buttonDown,
        shoulderLeft,
        shoulderRight,
        buttonStart,
        buttonBack,
        buttonBig
    }

    public enum JoystickAxis {
        leftStick_Horizontal,
        leftStick_Vertical,
        rightStick_Horizontal,
        rightStick_Vertical,
        shoulderTriggerLeft,
        shoulderTriggerRight
    }


    public static class Input {

        private static Array keyCodeValues;
        private static Array mouseButtonValues;
        private static Array joystickButtonValues;

        private static Dictionary<KeyCode , bool> lastKeyValue;
        private static Dictionary<MouseButton , bool> lastMouseButtonValue;
        private static Dictionary<JoystickButton , bool>[] lastJoystickButtonValues;
        private static Dictionary<string , UserButton> userButtons;
        private static Dictionary<string , UserAxis> userAxies;


        static Input () {
            keyCodeValues = Enum.GetValues (typeof (KeyCode));
            mouseButtonValues = Enum.GetValues (typeof (MouseButton));
            joystickButtonValues = Enum.GetValues (typeof (JoystickButton));

            lastKeyValue = new Dictionary<KeyCode , bool> ();
            foreach (KeyCode kc in keyCodeValues) {
                lastKeyValue.Add (kc , false);
            }
            lastMouseButtonValue = new Dictionary<MouseButton , bool> ();
            foreach (MouseButton mb in mouseButtonValues) {
                lastMouseButtonValue.Add (mb , false);
            }
            lastJoystickButtonValues = new Dictionary<JoystickButton , bool>[Window.Joysticks.Length];
            for (int i =0; i < lastJoystickButtonValues.Length; i++) {
                lastJoystickButtonValues[i] = new Dictionary<JoystickButton , bool> ();
                foreach (JoystickButton jb in joystickButtonValues) {
                    lastJoystickButtonValues[i].Add (jb , false);
                }
            }
            userButtons = new Dictionary<string , UserButton> ();
            userAxies = new Dictionary<string , UserAxis> ();

        }


        public static void PerformLastKey () {
            foreach (KeyCode key in keyCodeValues) {
                lastKeyValue[key] = Game.Win.GetKey (key);
            }
            foreach (MouseButton mb in mouseButtonValues) {
                lastMouseButtonValue[mb] = FromMouseButtonToBool (mb);
            }
            for (int i = 0; i < lastJoystickButtonValues.Length; i++) {
                foreach (JoystickButton jb in joystickButtonValues) {
                    lastJoystickButtonValues[i][jb] = FromJoysticButtonToBool (jb , i);
                }
            }
        }


        //Key
        public static bool GetKeyDown (KeyCode keyCode) {
            return !lastKeyValue[keyCode] && Game.Win.GetKey (keyCode);
        }

        public static bool GetKey (KeyCode keyCode) {
            return Game.Win.GetKey (keyCode);
        }

        public static bool GetKeyUp (KeyCode keyCode) {
            return lastKeyValue[keyCode] && !Game.Win.GetKey (keyCode);
        }

        //Mouse
        public static bool GetMouseButtonDown (MouseButton mouseButton) {
            return !lastMouseButtonValue[mouseButton] && FromMouseButtonToBool (mouseButton);
        }

        public static bool GetMouseButton (MouseButton mouseButton) {
            return FromMouseButtonToBool (mouseButton);
        }

        public static bool GetMouseButtonUp (MouseButton mouseButton) {
            return lastMouseButtonValue[mouseButton] && !FromMouseButtonToBool (mouseButton);
        }

        private static bool FromMouseButtonToBool (MouseButton mouseButton) {
            switch (mouseButton) {
                case MouseButton.leftButton:
                    return Game.Win.MouseLeft;
                case MouseButton.rightButton:
                    return Game.Win.MouseRight;
                case MouseButton.middleButton:
                    return Game.Win.MouseMiddle;
                case MouseButton.button1:
                    return Game.Win.MouseButton1;
                case MouseButton.button2:
                    return Game.Win.MouseButton2;
                case MouseButton.button3:
                    return Game.Win.MouseButton3;
                case MouseButton.button4:
                    return Game.Win.MouseButton4;
                case MouseButton.button5:
                    return Game.Win.MouseButton5;
                case MouseButton.button6:
                    return Game.Win.MouseButton6;
                case MouseButton.button7:
                    return Game.Win.MouseButton7;
                case MouseButton.button8:
                    return Game.Win.MouseButton8;
                case MouseButton.button9:
                    return Game.Win.MouseButton9;
                default:
                    return false;
            }
        }

        //Joystick button

        
        public static bool GetJoystickButtonDown (JoystickButton jb, int index) {
            return !lastJoystickButtonValues[index][jb] && FromJoysticButtonToBool (jb , index);
        }

        public static bool GetJoystickButton (JoystickButton jb, int index) {
            return FromJoysticButtonToBool (jb , index);
        }

        public static bool GetJoystickButtonUp (JoystickButton jb, int index) {
            return lastJoystickButtonValues[index][jb] && !FromJoysticButtonToBool (jb , index);
        }

        private static bool FromJoysticButtonToBool (JoystickButton jb, int index) {
            switch (jb) {
                case JoystickButton.buttonA:
                    return Game.Win.JoystickA (index);
                case JoystickButton.buttonB:
                    return Game.Win.JoystickB (index);
                case JoystickButton.buttonX:
                    return Game.Win.JoystickX (index);
                case JoystickButton.buttonY:
                    return Game.Win.JoystickY (index);
                case JoystickButton.buttonUp:
                    return Game.Win.JoystickUp (index);
                case JoystickButton.buttonDown:
                    return Game.Win.JoystickDown (index);
                case JoystickButton.buttonLeft:
                    return Game.Win.JoystickLeft (index);
                case JoystickButton.buttonRight:
                    return Game.Win.JoystickRight (index);
                case JoystickButton.shoulderLeft:
                    return Game.Win.JoystickShoulderLeft (index);
                case JoystickButton.shoulderRight:
                    return Game.Win.JoystickShoulderRight (index);
                case JoystickButton.buttonStart:
                    return Game.Win.JoystickStart (index);
                case JoystickButton.buttonBack:
                    return Game.Win.JoystickBack (index);
                case JoystickButton.buttonBig:
                    return Game.Win.JoystickBigButton (index);
                default:
                    return false;

            }
        }


        //Joystick axis
        public static float GetJoystickAxis (JoystickAxis joystickAxis, int index) {
            switch (joystickAxis) {
                case JoystickAxis.leftStick_Horizontal:
                    return Game.Win.JoystickAxisLeft (index).X;
                case JoystickAxis.leftStick_Vertical:
                    return Game.Win.JoystickAxisLeft (index).Y;
                case JoystickAxis.rightStick_Horizontal:
                    return Game.Win.JoystickAxisRight (index).X;
                case JoystickAxis.rightStick_Vertical:
                    return Game.Win.JoystickAxisRight (index).Y;
                case JoystickAxis.shoulderTriggerLeft:
                    return Game.Win.JoystickTriggerLeft (index);
                case JoystickAxis.shoulderTriggerRight:
                    return Game.Win.JoystickTriggerLeft (index);
                default:
                    return 0;
            }
        }

        //Userbuttons
        public static void AddUserButton (string name, UserButton userButton) {
            userButtons.Add (name , userButton);
        }

        public static bool GetButtonDown (string name) {
            return userButtons[name].GetButtonDown ();
        }

        public static bool GetButton (string name) {
            return userButtons[name].GetButton ();
        }

        public static bool GetButtonUp (string name) {
            return userButtons[name].GetButtonUp ();
        }

        //UserAxis
        public static void AddUserAxis (string name, UserAxis userAxis) {
            userAxies.Add (name , userAxis);
        }

        public static float GetAxis (string name) {
            return userAxies[name].GetAxis ();
        }
    }


    public class UserButton {

        private ButtonMatch[] associatedButtons;

        public UserButton (ButtonMatch[] associatedButtons) {
            this.associatedButtons = associatedButtons;
        }

        public bool GetButton () {
            foreach (ButtonMatch bm in associatedButtons) {
                if (bm.GetButton ()) return true;
            }
            return false;
        }

        public bool GetButtonDown () {
            foreach (ButtonMatch bm in associatedButtons) {
                if (bm.GetButtonDown ()) return true;
            }
            return false;
        }

        public bool GetButtonUp () {
            foreach (ButtonMatch bm in associatedButtons) {
                if (bm.GetButtonUp ()) return true;
            }
            return false;
        }

    }

    public abstract class ButtonMatch {
        public abstract bool GetButton ();
        public abstract bool GetButtonDown ();
        public abstract bool GetButtonUp ();
    }

    public class KeyButtonMatch : ButtonMatch {
        private KeyCode code;

        public KeyButtonMatch (KeyCode code) {
            this.code = code;
        }
        

        public override bool GetButton () {
            return Input.GetKey (code);
        }

        public override bool GetButtonDown () {
            return Input.GetKeyDown (code);
        }

        public override bool GetButtonUp () {
            return Input.GetKeyUp (code);
        }
    }

    public class MouseButtonMatch : ButtonMatch {
        private MouseButton code;

        public MouseButtonMatch (MouseButton code) {
            this.code = code;
        }


        public override bool GetButton () {
            return Input.GetMouseButton (code);
        }

        public override bool GetButtonDown () {
            return Input.GetMouseButtonDown (code);
        }

        public override bool GetButtonUp () {
            return Input.GetMouseButtonUp (code);
        }
    }

    public class JoystickButtonMatch : ButtonMatch {
        private JoystickButton code;
        private int index;

        public JoystickButtonMatch (JoystickButton code, int index) {
            this.code = code;
            this.index = index;
        }
        public override bool GetButton () {
            return Input.GetJoystickButton (code, index);
        }

        public override bool GetButtonDown () {
            return Input.GetJoystickButtonDown (code, index);
        }

        public override bool GetButtonUp () {
            return Input.GetJoystickButtonUp (code, index);
        }
    }


    public class UserAxis {

        public AxisMatch[] associatedAxis;

        public UserAxis (AxisMatch[] associatedAxis) {
            this.associatedAxis = associatedAxis;
        }

        public float GetAxis () {
            float value = 0;
            foreach (AxisMatch axis in associatedAxis) {
                value += axis.GetAxis ();
            }
            value = value < -1 ? -1 : value > 1 ? 1 : value; //mi assicuro che value sia un valore compreso tra - 1 e 1

            return value;
        }

    }

    public abstract class AxisMatch {
        public abstract float GetAxis ();
    }

    public class JoysticAxisMatch : AxisMatch {
        private JoystickAxis axis;
        private int index;

        public JoysticAxisMatch (JoystickAxis axis , int index) {
            this.axis = axis;
            this.index = index;
        }

        public override float GetAxis () {
            return Input.GetJoystickAxis (axis , index);
        }
    }

    public class KeyAxisMatch : AxisMatch {
        private KeyCode negativeKeyCode;
        private KeyCode positiveKeyCode;

        public KeyAxisMatch (KeyCode negativeKeyCode, KeyCode positiveKeyCode) {
            this.negativeKeyCode = negativeKeyCode;
            this.positiveKeyCode = positiveKeyCode;
        }

        public override float GetAxis () {
            float value = 0;
            value -= Input.GetKey (negativeKeyCode) ? 1 : 0;
            value += Input.GetKey (positiveKeyCode) ? 1 : 0;
            return value;
        }
    }

}
