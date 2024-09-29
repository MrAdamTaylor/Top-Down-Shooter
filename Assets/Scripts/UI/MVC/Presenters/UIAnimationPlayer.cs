using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using UnityEngine;

public class UIAnimationPlayer
{

    private Sequence _sequence;
    
    public void Play(AnimationConfigs animationConfigs, Transform image, Transform text, TweenerCore<long,long, NoOptions> tweenerCore = null)
    {
        _sequence?.Kill(true);
        _sequence = DOTween.Sequence();
        Vector3 imagePosition = image.position;
        Vector3 textPosition = text.position;
        if (tweenerCore.IsNull())
            _sequence.Append(ReturnStarterSequence(animationConfigs.ImageConfigs.StartConfigs,
                    animationConfigs.TextConfigs.StartConfigs, text, image))
                .Append(ReturnEndedSequence(animationConfigs.ImageConfigs.EndConfigs, 
                    animationConfigs.TextConfigs.EndConfigs, text, image, textPosition, imagePosition));
        else
        {
            _sequence.Append(ReturnStarterSequence(animationConfigs.ImageConfigs.StartConfigs,
                    animationConfigs.TextConfigs.StartConfigs, text, image))
                .Append(tweenerCore)
                .Append(ReturnEndedSequence(animationConfigs.ImageConfigs.EndConfigs, 
                    animationConfigs.TextConfigs.EndConfigs, text, image, textPosition, imagePosition));
        }
    }

    private Tween ReturnEndedSequence(TweenConfigs imageConfigsEndConfigs, TweenConfigs textConfigsEndConfigs, Transform text, Transform image, Vector3 textPosition, Vector3 imagePosition)
    {
        return DOTween
            .Sequence()
            .Append(text.transform.DOMove(textPosition,textConfigsEndConfigs.RillRate))
            .SetEase(textConfigsEndConfigs.EaseType)
            .Insert(0, image.transform.DOMove(imagePosition, imageConfigsEndConfigs.RillRate))
            .SetEase(imageConfigsEndConfigs.EaseType);
    }

    private Tween ReturnStarterSequence(TweenConfigs imageConfigsStartConfigs, TweenConfigs textConfigsStartConfigs, Transform text, Transform image)
    {
        return DOTween
            .Sequence()
            .Append(text.transform.DOShakePosition(textConfigsStartConfigs.RillRate, textConfigsStartConfigs.Scale, Constants.UI_ELEMENT_VIBRATION))
            .SetEase(textConfigsStartConfigs.EaseType)
            .Insert(0, image.transform.DOShakePosition(imageConfigsStartConfigs.RillRate, imageConfigsStartConfigs.Scale, Constants.UI_ELEMENT_VIBRATION))
            .SetEase(imageConfigsStartConfigs.EaseType);
    }
    
}