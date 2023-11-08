using Archero.Services;

namespace Archero.UI
{
    public partial class PauseWindow : Window
    {
        private TimeService _timeService;

        protected override void Awake()
        {
            base.Awake();

            _timeService = Startup.GetService<TimeService>();
        }

        private void OnEnable()
        {
            _timeService.Pause();
        }

        private void OnDisable() => _timeService.Resume();
    }
}