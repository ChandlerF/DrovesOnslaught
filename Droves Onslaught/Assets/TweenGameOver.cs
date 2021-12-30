using DG.Tweening;
using UnityEngine;

public class TweenGameOver : MonoBehaviour
{
    [SerializeField] private Transform[] Stars, Buttons;

    [SerializeField] private float Length1, Length2, Delay;


    void Start()
    {
        var sequence = DOTween.Sequence();



        /*foreach (var star in Stars)
        {
            sequence.Append(star.DOScale(new Vector3(1.2f, 1.2f, 1.2f), Length1).SetEase(Ease.InBounce).SetDelay(Delay).OnComplete(() =>
            {
                star.DOScale(new Vector3(1, 1, 1), Length2).SetEase(Ease.InOutSine);
            }));
        }

        sequence.OnComplete(() => 
        {
            TweenButtons();
        });*/
    }




    private void TweenButtons()
    {
        foreach (var button in Buttons)
        {
            button.DOScale(new Vector3(1.2f, 1.2f, 1.2f), Length1).SetEase(Ease.InBounce).SetDelay(Delay).OnComplete(() =>
            {
                button.DOScale(new Vector3(1, 1, 1), Length2).SetEase(Ease.InOutSine);
            });
        }
    }
}
