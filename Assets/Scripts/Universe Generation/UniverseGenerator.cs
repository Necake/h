using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseGenerator : MonoBehaviour {

    int seed = _SEED.seed;
    public int density = 1;
    public int chunkSize = 16;
    public GameObject solarSystem;
    public GameObject player;

    Vector3 prevPlayerChunk;

	// Use this for initialization
	void Start () {
        prevPlayerChunk = new Vector3(player.transform.position.x/chunkSize, player.transform.position.y/chunkSize, 0);
        GenerateField((int)prevPlayerChunk.x, (int)prevPlayerChunk.y);
        
    }
	
    void GenerateChunk(int posx, int posy) //posx and posy being centers
    {
        GameObject chunk = new GameObject(posx.ToString() + posy.ToString()); //put them all in one object so they can be deleted later, the name will be handy for that

        posx *= chunkSize; posy *= chunkSize;
        

        Random.InitState(posx + posy + seed);
        for(int i = 0; i < density; i++)
        {
            Vector3 instancePos = new Vector3(Random.Range((float)(posx - chunkSize / 2), (float)(posx + chunkSize / 2)), Random.Range((float)(posy - chunkSize / 2), (float)(posy + chunkSize / 2))); //somewhere within the chunk
            GameObject instance = Instantiate(solarSystem, instancePos, Quaternion.identity) as GameObject;
            instance.transform.parent = chunk.transform;
            instance.name = (posx / chunkSize).ToString() + (posy / chunkSize).ToString() + i.ToString();
            instance.GetComponent<SolarSystemGenerator>().seed = posx / chunkSize + posy / chunkSize + seed + i;
        }
        
    }

    void DeleteChunk(int posx, int posy)
    {
        GameObject chunkToDelete = GameObject.Find(posx.ToString() + posy.ToString());
        if(chunkToDelete) //so it doesn't fuck up when there is no chunk
            Destroy(chunkToDelete);
    }

    
    /*a field would be 9 chunks sorrounding the player, so it covers the visible playfield
     [x][x][x]
     [x][x][x]
     [x][x][x]
         */
    void GenerateField(int posx, int posy)
    {
        Debug.Log("generating chunk around: " + posx + " " + posy);
        for (int x = -1; x<=1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(GameObject.Find((posx+x).ToString()+(posy+y).ToString()) == null)
                {
                    GenerateChunk(posx + x, posy + y);
                }
            }
        }
    }

    /*the "spare" chunks would be the one that don't need to be used anymore, graphically would look like:
    [x][x][x][x][x]
    [x][o][o][o][x]
    [x][o][o][o][x]  where x are the ones to be removed, this way every time the player moves all spare chunks
    [x][o][o][o][x]  will be deleted
    [x][x][x][x][x]
         
         */

    void DeleteSpare(int posx, int posy)
    {
        for (int x = -2; x <= 2; x++)
        {
            DeleteChunk(posx + x, posy + 2); //top row
        }
        for (int x = -2; x <= 2; x++)
        {
            DeleteChunk(posx + x, posy - 2); //bottom row
        }
        for(int y = 1; y >= -1; y--)
        {
            DeleteChunk(posx - 2, posy + y);
            DeleteChunk(posx + 2, posy + y); //sides
        }
    }

	// Update is called once per frame
	void Update () {
		Vector3 currentPlayerChunk = new Vector3(player.transform.position.x / chunkSize, player.transform.position.y / chunkSize, 0);
        if((int)currentPlayerChunk.x != (int)prevPlayerChunk.x || (int)currentPlayerChunk.y != (int)prevPlayerChunk.y)
        {
            //generate stuff
            GenerateField((int)currentPlayerChunk.x, (int)currentPlayerChunk.y);
            //also delete stuff lol
            DeleteSpare((int)currentPlayerChunk.x, (int)currentPlayerChunk.y);
            prevPlayerChunk = currentPlayerChunk;
        }
        
    }
}