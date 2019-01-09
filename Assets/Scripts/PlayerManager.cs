using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public GameObject currentUpgrade;
    public GameObject currentRegionGO;
    Region currentRegion;
    Continent currentContinent;

    public Text currentRegionPopulation;
    public Text currentRegionConverted;
    public Text currentRegionGrowth;
    public Text currentRegionName;
    public Text currencyText;

    public Image hpVar;

    float currency;
    World world;
    bool paused;

    public GameObject mailcontainter;
    public float[] percentagesForMailAppear;
    int currentMailAppeared=0;
    Queue<GameObject> mailList;
    public GameObject mailAlert;

    // Use this for initialization
    void Reset()
    {
        gameObject.name = "Player Manager";
    }


    private void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        currency = 10;

        currentRegion = null;
        currentContinent = null;
        currentRegionName.text = "World";

        //mailAlert.SetActive(false);
        mailList = new Queue<GameObject>(mailcontainter.transform.childCount);

        foreach (Transform mail in mailcontainter.transform)
        {
            mail.gameObject.SetActive(false);
            mailList.Enqueue(mail.gameObject);
        }

        paused = false;
    }

    public bool buyUpgrade(float value)
    {
        if (value <= currency)
        {
            currency -= value;
            return true;
        }
        return false;
            
    }

    public void activateMail()
    {
        currentMailAppeared++;

        //sacamos un mail aleatorio de la cola
        GameObject mail = mailList.Dequeue();

        //lo activamos 
        mail.SetActive(true);

        //activamos la alerta
        mailAlert.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //control
        if (Input.GetMouseButtonDown(0))
        {
            if (currentUpgrade != null)
            {
                //raycast 
                RaycastHit2D hit= Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
                if (hit.collider!=null)
                {
                    Upgrade upgrade = currentUpgrade.GetComponent<Upgrade>();
                    //apply the upgrade
                    hit.transform.gameObject.GetComponent<Region>().getUpgrade(upgrade);


                    //destroy upgrade
                    Destroy(currentUpgrade);        
                }
                
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
                if (hit.collider != null)
                {
                    if (hit.transform.gameObject != currentRegionGO)
                    {
                        if (currentRegion != null)
                            currentRegion.deactivate();
                        if (currentContinent != null)
                            currentContinent.deactivate();
                        currentRegionGO = hit.transform.gameObject;
                        currentRegion = currentRegionGO.GetComponent<Region>();
                        currentRegionName.text = currentRegionGO.name;
                        currentRegion.activate();
                        currentContinent = null;
                    }
                    else if (currentContinent == null)
                    {
                        currentContinent = currentRegion.continent;
                        currentRegionName.text = currentContinent.gameObject.name;
                        currentContinent.activate();
                    }
                    else
                    {

                    }
                    
                     
                    
                }
                else
                {
                    if (currentContinent != null)
                    {
                        currentContinent.deactivate();
                        currentContinent = null;
                        currentRegion = null;
                        currentRegionGO = null;

                    }
                    else if (currentRegion != null)
                    {
                        currentRegion.deactivate();
                        currentRegion = null;
                        currentRegionGO = null;
                    }
                    currentRegionName.text = "World";
                }

            }

        }

        //updateWorld
        if(!paused)
            world.updateContinents();

        //currency
        currency += world.getGrowth()*Time.deltaTime;

        //mail arrival
        if (currentMailAppeared<percentagesForMailAppear.Length)
            if (world.getPercentConverted() > percentagesForMailAppear[currentMailAppeared])
                activateMail();
        

        //print values
        currencyText.text = currency.ToString("f2");

        if (currentContinent != null)
        {
            float belivers = currentContinent.getBelivers();
            float population = currentContinent.getPopulation();

            currentRegionConverted.text = Mathf.Floor(belivers).ToString();
            currentRegionGrowth.text = currentContinent.getGrowth().ToString();
            currentRegionPopulation.text = population.ToString("f0");

            hpVar.fillAmount = belivers / population;
        }
        else if (currentRegion != null)
        {
            float belivers= currentRegion.actualBelivers;
            float population = currentRegion.totalPopulation;

            currentRegionConverted.text = Mathf.Floor(belivers).ToString();
            currentRegionGrowth.text = currentRegion.getGrowth().ToString();
            currentRegionPopulation.text = population.ToString("f0");

            hpVar.fillAmount = belivers / population;
        }
        else
        {
            float belivers = world.getBelivers();
            float population = world.getPopulation();

            currentRegionConverted.text = Mathf.Floor(belivers).ToString();
            currentRegionGrowth.text = world.getGrowth().ToString();
            currentRegionPopulation.text = population.ToString("f0");

            hpVar.fillAmount = belivers / population;
        }
    }

}
