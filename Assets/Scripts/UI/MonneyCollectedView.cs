using System.Collections;
using TMPro;
using UnityEngine;
using System;
using Archero.Repositories;
using DG.Tweening;

namespace Archero.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MonneyCollectedView : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _durationFade = 0.5f;

        private Color _defaultColor;
        private TextMeshProUGUI _guiText;
        private LevelRepository _levelRepository;
        private void Awake()
        {
            _levelRepository = Startup.GetRepository<LevelRepository>();
        }

        private void Start()
        {
            if (!TryGetComponent(out _guiText))
            {
                throw new NullReferenceException("view monney collected must have component TMProGUI Text");
            }

            _defaultColor = _guiText.color;
        }

        private void OnKillAward(int currentMoney)
        {
            PlayFadeAnimation();

            _guiText.text = string.Format("Money: {0}", currentMoney);
        }

        private void PlayFadeAnimation()
        {
            _guiText.color = Color.clear;
            _guiText.DOColor(_defaultColor, _durationFade);
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