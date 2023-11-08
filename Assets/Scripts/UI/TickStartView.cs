using Archero.Services;
using DG.Tweening;
using UnityEngine;

namespace Archero.UI
{
    public class TickStartView : DisplayerTextInfo
    {
        [SerializeField, Min(0)] private float _durationAnimation = 0.3f;

        private Color _defaultColor;

        private LevelService _levelService;

        private void Awake()
        {
            _levelService = Startup.GetService<LevelService>();

        }
        protected override void Start()
        {
            base.Start();

            _defaultColor = TextGUI.color;

            SetText(string.Empty);
        }
        private void OnTickEnd()
        {
            gameObject.SetActive(false);
        }

        private void OnTick(int tick)
        {
            TextGUI.color = Color.clear;

            Sequence sequence = DOTween.Sequence();

            sequence.Join(transform.DOPunchScale(Vector3.one * 1.5f, _durationAnimation));
            sequence.Join(TextGUI.DOColor(_defaultColor, _durationAnimation));
            sequence.Play();

            SetText(tick.ToString());
        }

        private void OnEnable()
        {
            _levelService.OnTick += OnTick;
            _levelService.OnTickEnd += OnTickEnd;
        }

        private void OnDisable()
        {
            _levelService.OnTick -= OnTick;
            _levelService.OnTickEnd -= OnTickEnd;
        }
    }
}