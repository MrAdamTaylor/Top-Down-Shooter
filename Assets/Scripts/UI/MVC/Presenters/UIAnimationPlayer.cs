using System;
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
        {
            _sequence.Append(ReturnStarterSequence(animationConfigs.ImageConfigs.StartConfigs,
                    animationConfigs.TextConfigs.StartConfigs, text, image,
                    animationConfigs.ImageConfigs.AnimationType))
                .Append(ReturnEndedSequence(animationConfigs.ImageConfigs.EndConfigs, 
                    animationConfigs.TextConfigs.EndConfigs, text, image, textPosition, imagePosition));
        }
        else
        {
            _sequence.Append(ReturnStarterSequence(animationConfigs.ImageConfigs.StartConfigs,
                    animationConfigs.TextConfigs.StartConfigs, text, image, 
                    animationConfigs.ImageConfigs.AnimationType))
                .Append(tweenerCore)
                .Append(ReturnEndedSequence(animationConfigs.ImageConfigs.EndConfigs, 
                    animationConfigs.TextConfigs.EndConfigs, text, image, textPosition, imagePosition));
        }
    }

    private Sequence ReturnEndedSequence(TweenConfigs imageConfigsEndConfigs, TweenConfigs textConfigsEndConfigs, Transform text, Transform image, Vector3 textPosition, Vector3 imagePosition)
    {
        return DOTween
            .Sequence()
            .Append(text.transform.DOMove(textPosition,textConfigsEndConfigs.RillRate))
            .SetEase(textConfigsEndConfigs.EaseType)
            .Insert(0, image.transform.DOMove(imagePosition, imageConfigsEndConfigs.RillRate))
            .SetEase(imageConfigsEndConfigs.EaseType);
    }

    private Sequence ReturnStarterSequence(TweenConfigs imageStartConfigs, TweenConfigs textStartConfigs, 
        Transform text, Transform image, AnimationType animationType)
    {
        return DOTween
            .Sequence()
            .Append(ReturnAnimationByType(animationType,imageStartConfigs, image))
            .SetEase(textStartConfigs.EaseType)
            .Insert(0, (ReturnAnimationByType(animationType,textStartConfigs, text)))
            .SetEase(imageStartConfigs.EaseType);
    }

    private Sequence ReturnAnimationByType(AnimationType animationType ,TweenConfigs tweenConfigs, Transform transform)
    {
        switch (animationType)
        {
            case AnimationType.Scale:
                return DOTween.Sequence()
                    .Append(transform.DOMove(tweenConfigs.Scale, tweenConfigs.RillRate));
            break;
            case AnimationType.Shaking:
                return DOTween.Sequence()
                    .Append(transform.DOShakePosition(tweenConfigs.RillRate, tweenConfigs.Scale, Constants.UI_ELEMENT_VIBRATION));
                break;
            default:
                throw new Exception("Unknown Type of Tween Weapon UI Animation");
        }
    }

}