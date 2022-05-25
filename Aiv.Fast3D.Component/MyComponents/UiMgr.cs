using System;
using Aiv.Fast3D.Component.UI;

namespace Aiv.Fast3D.Component {
    class UiMgr : UserComponent {
        private int score;
        private PlayerController playerController;
        private EnemyMgr enemyMgr;
        private TextBox hpTextBox, enemiesTextBox, scoreTextBox;


        public UiMgr(GameObject owner) : base(owner) {
        }


        public override void Awake() {
            playerController = Game.CurrentScene.FindGameObject("Player").GetComponent<PlayerController>();
            enemyMgr = Game.CurrentScene.FindGameObject("EnemyMgr").GetComponent<EnemyMgr>();
            hpTextBox = Game.CurrentScene.FindGameObject("HpUI").GetComponent<TextBox>();
            enemiesTextBox = Game.CurrentScene.FindGameObject("EnemiesUI").GetComponent<TextBox>();
            scoreTextBox = Game.CurrentScene.FindGameObject("Score").GetComponent<TextBox>();
        }


        public override void Start() {
            #region FirstPrints
            hpTextBox.SetText($"HP = {playerController.Hp}");
            enemiesTextBox.SetText($"ENEMIES = {enemyMgr.NumOfEnemies}");
            scoreTextBox.SetText($"SCORE = {score}");
            #endregion
            EventHandlerManager.AddListener((int)EventsEnum.ScoreIncrease, OnScoreIncrease);
            EventHandlerManager.AddListener((int)EventsEnum.EnemyDeath, OnEnemyDeath);
            EventHandlerManager.AddListener((int)EventsEnum.PlayerHit, OnHpDecrease);
        }
        public override void OnDestroy() {
            EventHandlerManager.RemoveListener((int)EventsEnum.ScoreIncrease, OnScoreIncrease);
            EventHandlerManager.RemoveListener((int)EventsEnum.EnemyDeath, OnEnemyDeath);
            EventHandlerManager.RemoveListener((int)EventsEnum.PlayerHit, OnHpDecrease);
        }


        public void OnHpDecrease(EventArgs message) {
            playerController.Hp--;
            hpTextBox.SetText($"HP = {playerController.Hp}       ");
        }
        public void OnEnemyDeath(EventArgs message) {
            enemyMgr.NumOfEnemies--;
            enemiesTextBox.SetText($"ENEMIES = {enemyMgr.NumOfEnemies}");         
        }
        public void OnScoreIncrease(EventArgs message) {
             scoreTextBox.SetText($"SCORE = {score += 10}");
        }


        public override Component Clone(GameObject owner) {
            return new UiMgr(owner);
        }
    }
}
