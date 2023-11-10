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

            _player.OnDeath += OnDeath;
        }

        private void OnDeath(object sender, System.EventArgs e)
        {
            _player.OnDeath -= OnDeath;

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