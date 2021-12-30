using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int LevelNumber;

    private TextMeshProUGUI ButtonText;

    //[SerializeField] List<bool> StarsEarned = new List<bool>();

    [SerializeField] private float TweenLength1, TweenLength2, TweenDelay;

    [SerializeField] bool LevelNumberFromSiblings = true;

    [SerializeField] Color InactiveColor;

    private ChaptersManager Chapters;

    //Is called when enabled 
    private void Start()
    {
        if (LevelNumberFromSiblings)
        {
            //LevelNumber = transform.GetSiblingIndex() + 1;    //Commented out but not deleted because the else statement etc.
            Chapters = transform.parent.GetComponent<ChaptersManager>();
        }
        else
        {
            LevelNumber = LevelManager.instance.CurrentLevel;
        }

        SetStars();
    }




    public void SetStars()
    {
        GameObject StarsParent = transform.GetChild(0).GetChild(1).gameObject;

        if (LevelNumberFromSiblings)
        {
            ButtonText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

            ButtonText.text = LevelNumber.ToString();
        }

        
        //Set all stars
        for (int i = 0; i < 3; i++)
        {
            //Set color to inactive
            StarsParent.transform.GetChild(i).GetComponent<Image>().color = InactiveColor;



            

            //Tween (Animate) stars
            var sequence = DOTween.Sequence();


            float newDelay = TweenDelay + (LevelNumber - (Chapters.ActiveChapter * 10)) * 1.2f;

            //Debug.Log(LevelNumber - (Chapters.ActiveChapter * 10));
            Debug.Log(Chapters.ActiveChapter * 10);  
            sequence.Append(StarsParent.transform.GetChild(i).DOScale(new Vector3(0.8f, 1f, 0.8f), TweenLength1).SetEase(Ease.OutBounce)).SetDelay(newDelay * 0.09f + (i * 0.08f));
            sequence.Append(StarsParent.transform.GetChild(i).DOScale(new Vector3(0.6f, 0.8f, 0.6f), TweenLength2).SetEase(Ease.InOutSine)).SetDelay(newDelay * 0.12f + (i * 0.08f));

        }




        //Debug.Log(LevelNumber + LevelManager.instance.Stars[LevelNumber][0].ToString());
        //If the first star is earned
        if (LevelManager.instance.Stars[LevelNumber][0])
        {
            StarsParent.transform.GetChild(0).GetComponent<Image>().color = Color.black;


            for (int i = 0; i < 3; i++)
            {
                //If Star is earned
                if (LevelManager.instance.Stars[LevelNumber][i])
                {
                    StarsParent.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
            }
        }
    }


    public void LoadScene()
    {
        LevelManager.instance.CurrentLevel = LevelNumber;

        SceneManager.LoadScene("Level" + LevelNumber.ToString());
    }
}
