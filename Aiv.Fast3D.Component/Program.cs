using System;

namespace Aiv.Fast3D.Component {
    public enum RigidbodyType { player = 1, enemy = 2, bullet = 4 }
    public enum EventsEnum { EnemyDeath, PlayerShoot, BulletRebound, ChangeSensitivity, PlayerHit, ScoreIncrease, last }
    public enum AudioLayer { music, sfx, last }


    public class ChangeSensitivityArgs : EventArgs {
        public string button;

        public ChangeSensitivityArgs(string _button) {
            button = _button;
        }
    }


    class Program {
        static void Main (string[] args) {
            AudioClipMgr.Init((int)AudioLayer.last);
            EventHandlerManager.Init((int)EventsEnum.last);
            #region Buttons
            Input.AddUserAxis("Horizontal", new UserAxis(new AxisMatch[] { new KeyAxisMatch(Fast2D.KeyCode.A, Fast2D.KeyCode.D) }));
            Input.AddUserAxis("Vertical", new UserAxis(new AxisMatch[] { new KeyAxisMatch(Fast2D.KeyCode.S, Fast2D.KeyCode.W) }));
            Input.AddUserButton("Shoot", new UserButton(new ButtonMatch[] { new KeyButtonMatch(Fast2D.KeyCode.Space), new MouseButtonMatch(MouseButton.leftButton) }));
            Input.AddUserButton("IncreasedSensitivity", new UserButton(new ButtonMatch[] { new KeyButtonMatch(Fast2D.KeyCode.Z) }));
            Input.AddUserButton("ReduceSensitivity", new UserButton(new ButtonMatch[] { new KeyButtonMatch(Fast2D.KeyCode.C) }));
            Input.AddUserButton("ExitGame", new UserButton(new ButtonMatch[] { new KeyButtonMatch(Fast2D.KeyCode.Esc) }));
            Input.AddUserButton("PauseGame", new UserButton(new ButtonMatch[] { new KeyButtonMatch(Fast2D.KeyCode.Return) }));
            #endregion
            Game.Init ("3D Fish" , 1280 , 720 , new PlayScene ()  , 720 , 10 , -300 , 300);
            Game.Gravity = -9.8f;
            Game.Play ();
        }
    }
}
