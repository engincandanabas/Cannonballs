 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Text countText;
    private int count;
    private AudioSource bounceSound;
    // Start is called before the first frame update
    void Start()
    {
        bounceSound=GameObject.Find("BounceSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<=-7)
        {
            Destroy(gameObject);
        }
    }
    public void SetStartingCount(int count)
    {
        this.count=count;
        countText.text=count.ToString();
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.collider.name=="Ball" && count>0)
        {
            count--;
            Camera.main.GetComponent<CameraTransitions>().Shake();
            countText.text=count.ToString();
            bounceSound.Play();
            if(count==0)
            {
                Destroy(gameObject);
                Camera.main.GetComponent<CameraTransitions>().MediumShake();
                GameObject.Find("ExtraBallProgress").GetComponent<Progress>().IncreaseCurrentWidth();
            }
        }
    }
}
