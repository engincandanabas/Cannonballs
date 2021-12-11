using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotCounterText : MonoBehaviour
{
    public AnimationCurve scaleCurve;
    public CanvasGroup shotCount;
    private CanvasGroup cg;
    public Text shotText,countText;
    // Start is called before the first frame update
    void Start()
    {
        cg=GetComponent<CanvasGroup>();
        transform.localScale=Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTopText(string text)
    {
        shotText.text=text;
    }
    public void SetBottomText(string text)
    {
        countText.text=text;
    }
    public void Flash()
    {
        cg.alpha=1;
        transform.localScale=Vector3.zero;
        StartCoroutine(FlashRoutine());
    }
    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < 60; i++)
        {
                transform.localScale=Vector3.one*scaleCurve.Evaluate((float)i/50);

                if(i>=40)
                {
                    cg.alpha=(float)(60-i)/20; 
                }
                yield return null;
        }
        yield break;
    }
}
