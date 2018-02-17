using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemGenerator : MonoBehaviour {
    public GameObject central;
    public GameObject[] planets; //each planet will have a rotatearound

    public int planetCount;
    float initDistance = 5.0f;
    

    public int seed; // to be set from uniGen

	// Use this for initialization
	void Start () {
        GameObject sun = Instantiate(central, gameObject.transform.position, Quaternion.identity) as GameObject;
        sun.transform.parent = transform;

        Random.InitState(seed);
        foreach (GameObject planet in planets)
        {
            float planetSize = planet.GetComponent<Renderer>().bounds.size.x;
            for (int i = 0; i < Random.Range(planetCount - 2, planetCount); i++)
            {
                initDistance += Random.Range(planetSize * 2.5f + 1, planetSize * 2.5f + 10);
                GameObject currentPlanet = Instantiate(planet, gameObject.transform.position + new Vector3(initDistance/5, 0, 0),Quaternion.identity) as GameObject;
                currentPlanet.transform.parent = sun.transform;
                currentPlanet.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

                currentPlanet.GetComponent<OrbitRotator>().rotCenter = sun.transform.position;
                currentPlanet.GetComponent<OrbitRotator>().speed = Random.Range(-400, 400);
                if(currentPlanet.GetComponent<OrbitRotator>().speed <= 150 && currentPlanet.GetComponent<OrbitRotator>().speed >= -150)
                {
                    currentPlanet.GetComponent<OrbitRotator>().speed = 150;
                }
            }
            GetComponent<CircleCollider2D>().radius = initDistance / 5 + planetSize/2;
        }
    }
}
