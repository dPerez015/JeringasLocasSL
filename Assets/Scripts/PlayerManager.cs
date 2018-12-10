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
                        if(currentRegion!= null)
                            currentRegion.deactivate();
                        if (currentContinent != null)
                            currentContinent.deactivate();
                        currentRegionGO = hit.transform.gameObject;
                        currentRegion = currentRegionGO.GetComponent<Region>();
                        currentRegionName.text = currentRegionGO.name;
                        currentRegion.activate();
                        currentContinent = null;
                    }
                    else
                    {
                        currentContinent = currentRegion.continent;
                        currentRegionName.text = currentContinent.gameObject.name;
                        currentContinent.activate();
                    }
                }
                else
                {
                    currentRegion = null;
                    currentContinent = null;
                    currentRegionName.text = "World";
                }

            }

        }

        //currency
        currency += world.getGrowth()*Time.deltaTime;


        //print values
        currencyText.text = currency.ToString();

        if (currentContinent != null)
        {
            float belivers = currentContinent.getBelivers();
            float population = currentContinent.getPopulation();

            currentRegionConverted.text = Mathf.Floor(belivers).ToString();
            currentRegionGrowth.text = currentContinent.getGrowth().ToString();
            currentRegionPopulation.text = population.ToString();

            hpVar.fillAmount = belivers / population;
        }
        else if (currentRegion != null)
        {
            float belivers= currentRegion.actualBelivers;
            float population = currentRegion.totalPopulation;

            currentRegionConverted.text = Mathf.Floor(belivers).ToString();
            currentRegionGrowth.text = currentRegion.beliversGrowthPS.ToString();
            currentRegionPopulation.text = population.ToString();

            hpVar.fillAmount = belivers / population;
        }
        else
        {
            float belivers = world.getBelivers();
            float population = world.getPopulation();

            currentRegionConverted.text = Mathf.Floor(belivers).ToString();
            currentRegionGrowth.text = world.getGrowth().ToString();
            currentRegionPopulation.text = population.ToString();

            hpVar.fillAmount = belivers / population;
        }
    }

}
