using TMPro;
using UnityEngine;
using Archero.Repositories;
using DG.Tweening;

namespace Archero.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MonneyCollectedView : DisplayerTextInfo
    {
        [SerializeField, Min(0)] private float _durationFade = 0.5f;

        private Color _defaultColor;

        private LevelRepository _levelRepository;

        private void Awake()
        {
            _levelRepository = Startup.GetRepository<LevelRepository>();
        }

        protected override void Start()
        {
            base.Start();

            _defaultColor = TextGUI.color;
        }

        private void OnKillAward(int currentMoney)
        {
            PlayFadeAnimation();

            SetText(string.Format("Money: {0}", currentMoney));
        }

        private void PlayFadeAnimation()
        {
            TextGUI.color = Color.clear;
            TextGUI.DOColor(_defaultColor, _durationFade);
        }

        private void OnEnable()
        {
            _levelRepository.OnKillAward += OnKillAward;
        }

        private void OnDisable()
        {
            _levelRepository.OnKillAward -= OnKillAward;
        }
    }
}