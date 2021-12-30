using DG.Tweening;
using UnityEngine;

public class TweenMenu : MonoBehaviour
{
    [SerializeField] private Transform Title;
    [SerializeField] private Transform[] Buttons;
    [SerializeField] private float FirstLength, SecondLength, BackgroundLength, TitleLength;

    void Start()
    {
        //Title
        Title.DORotate(new Vector3(0, 0, 8), TitleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        Title.DOScale(new Vector3(1.1f, 1.1f, 1.1f), TitleLength * 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);



        //Scale Background up, afterwards, loop through buttons and scale them too large, then set them back to normal
        transform.DOScale(new Vector3(1, 1, 1), BackgroundLength).SetEase(Ease.OutBack).SetDelay(0.5f).OnComplete(() =>
        {
            var sequence = DOTween.Sequence();


            foreach (var button in Buttons)
            {
                sequence.Append(button.DOScale(new Vector3(1.2f, 1.2f, 1.2f), FirstLength).SetEase(Ease.InBounce).SetDelay(0.1f).OnComplete(() =>
                {
                    button.DOScale(new Vector3(1, 1, 1), SecondLength);
                }));
            }
        });
    }
}
