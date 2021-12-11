using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
public class ShootScript : MonoBehaviour
{
    [SerializeField] private float power=2;
    private int dots=15;
    private Vector2 startPos;
    private bool shoot,aiming;
    private GameObject Dots;
    private List<GameObject> projectilesPath;
    private Rigidbody2D ballBody;
    public GameObject ballPrefab,ballsContainer;
    private GameController gameController;
    void Awake()
    {
        gameController=GameObject.Find("GameController").GetComponent<GameController>();
        Dots=GameObject.Find("Dots");
    }
    void Start()
    {
        Dots=GameObject.Find("Dots");
        projectilesPath=Dots.transform.Cast<Transform>().ToList().ConvertAll(t=>t.gameObject);
        HideDots();
    }

    // Update is called once per frame
    void Update()
    {
        ballBody=ballPrefab.GetComponent<Rigidbody2D>(); 
        if(gameController.shootCount<=3 && !IsMouseOverUI())
        {
            Rotate();
            Aim(); 
        }
    }
    void Aim()
    {
        if(shoot)
        {
            return;
        }
        if(Input.GetMouseButton(0))
        {
            if(!aiming)
            {
                print("1st Part");
                //cal
                aiming=true;
                startPos=Input.mousePosition;
                gameController.CheckShotCount();
            }
            else
            {
                //Aim cal path
                PathCalculation();
            }
        }
        else if(aiming && !shoot)
        {
            aiming=false;
            HideDots();
            if(gameController.shootCount==1 )
            {
                Camera.main.GetComponent<CameraTransitions>().RotateCameraToSide();
            }

            //Shoot
            StartCoroutine(Shoot());
            
            
        }
        
    }
    Vector2 ShootForce(Vector3 force)
    {
        return (new Vector2(startPos.x,startPos.y)-new Vector2(force.x,force.y)) * power;
    }
    Vector2 DotPath(Vector2 startP,Vector2 startVel,float t)
    { 
        return startP+startVel*t+0.5f*Physics2D.gravity*t*t;
    }
    void PathCalculation()
    {
        Vector2 velocity=ShootForce(Input.mousePosition)*Time.fixedDeltaTime/ballBody.mass;
        for(int i=0;i<projectilesPath.Count;i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled=true;
            float t=i/15f;
            Vector3 point =DotPath(transform.position,velocity,t);
            point.z=1;
            projectilesPath[i].transform.position=point;

        }
    }
    void ShowDots()
    {
        for(int i=0;i<projectilesPath.Count;i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled=true;
        }
    }
    void HideDots()
    {
        for(int i=0;i<projectilesPath.Count;i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled=false;
        }
    }
    void Rotate()
    {
        Vector2 dir=GameObject.Find("dot (1)").transform.position-transform.position;
        var angle=Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.AngleAxis(angle,Vector3.forward);
    }
    IEnumerator Shoot()
    {
        for (int i = 0; i < gameController.ballsCount; i++)
        {
            yield return new WaitForSeconds(0.07f);
            GameObject ball=Instantiate(ballPrefab,transform.position,Quaternion.identity);
            ball.name="Ball";
            ball.transform.SetParent(ballsContainer.transform);
            ballBody=ball.GetComponent<Rigidbody2D>();
            ballBody.AddForce(ShootForce(Input.mousePosition));


            int balls=gameController.ballsCount-i;
            gameController.ballsCountText.text=(gameController.ballsCount-i-1).ToString();
        }
        yield return new WaitForSeconds(0.5f);
        gameController.shootCount++;
        gameController.ballsCountText.text=gameController.ballsCount.ToString();
        
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
