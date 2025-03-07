using System;
using DG.Tweening;
using EnterpriceLogic.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVC.View
{
    [Serializable]
    public class CurrencyViewWithImage : CurrencyView
    {
        [SerializeField] private Image _ammoImage;
        [SerializeField] private Image _weaponImage;
        [SerializeField] private StartScroolTween _startScroolTween;
        [SerializeField] private EndScroolTween _endScroolTween;
        //private Sprite _currentAmmoSprite;
        public Transform ImageTransform => _ammoImage.transform;

        public override void UpdateCurrency(long value)
        {
            base.UpdateCurrency(value);
        }

        public void UpdateAmmoImage(Sprite sprite)
        {
            //_currentAmmoSprite = sprite;
            _ammoImage.sprite = sprite;
        }
        

        public void UpdateWeaponImage(Sprite sprite)
        {
            _weaponImage.sprite = sprite;
        }

        public void SetDefaultCurrency()
        {
            _text.text = PrefabPath.DEFAULT_WEAPON_AMMO_TEXT;
        }

        public Sequence AnimateTextImageStart()
        {
            return DOTween
                .Sequence()
                .Append(_text.transform.DOScale(_startScroolTween.Scale, _startScroolTween.Duration))
                .SetEase(_startScroolTween.AnimationLine)
                .Insert(0, _ammoImage.transform.DOScale(_startScroolTween.Scale, _startScroolTween.Duration))
                .SetEase(_startScroolTween.AnimationLine)
                .Insert(0, _weaponImage.transform.DOScale(_startScroolTween.Scale, _startScroolTween.Duration))
                .SetEase(_startScroolTween.AnimationLine);
        }

        public Sequence AnimateTextImageEnd()
        {
            return DOTween
                .Sequence()
                .Append(_text.transform.DOScale(_endScroolTween.Scale, _endScroolTween.Duration))
                .SetEase(_endScroolTween.AnimationLine)
                .Insert(0, _ammoImage.transform.DOScale(_endScroolTween.Scale, _endScroolTween.Duration))
                .SetEase(_endScroolTween.AnimationLine)
                .Insert(0, _weaponImage.transform.DOScale(_endScroolTween.Scale, _endScroolTween.Duration))
                .SetEase(_endScroolTween.AnimationLine);
        }
    }

    [Serializable]
    public struct StartScroolTween
    {
        public Vector3 Scale;
        public float Duration;
        public Ease AnimationLine;
    }

    [Serializable]
    public struct EndScroolTween
    {
        public Vector3 Scale;
        public float Duration;
        public Ease AnimationLine;
    }
}