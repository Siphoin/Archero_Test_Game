using Archero.Services;
using Zenject;

namespace Archero.UI
{
    public partial class DieWindow : Window
    {
        private IPlayer _player;

        protected override void Awake()
        {
            base.Awake();

            _player.OnDealth += OnDealth;
        }

        private void OnDealth(object sender, System.EventArgs e)
        {
            _player.OnDealth -= OnDealth;

            gameObject.SetActive(true);
        }

        public override void Exit()
        {
            Startup.GetService<LevelService>().RestartLevel();

            base.Exit();
        }

        [Inject]
        private void Construct (IPlayer player)
        {
            _player = player;
        }
    }
}