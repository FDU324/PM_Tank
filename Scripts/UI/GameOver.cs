using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    
    private GameManager gm;

    public GameObject[] rank;

    void Start() {
        gm = GameManager.GetGMInstance();
        
        for(int i = gm.tank_list.Count; i <4; i++)
        {
            rank[i].SetActive(false);
        }

        

        string[] tankNames = new string[gm.tank_list.Count];
        int index = 0;
        foreach(string tankName in gm.dead_tank_list)
        {
            tankNames[index] = tankName;
            index++;
        }
        if(gm.dead_tank_list.Count != gm.tank_list.Count)
        {
            gm.tank_list.Sort((tank1, tank2) =>
            {
                return tank1.GetComponent<TankModel>().health - tank2.GetComponent<TankModel>().health;
            });
            foreach (GameObject tank in gm.tank_list)
            {
                if(!gm.dead_tank_list.Contains(tank.name))
                {
                    tankNames[index] = tank.name;
                    index++;
                }
            }
        }

        for (int i = 0; i < tankNames.Length; i++)
        {
            rank[i].GetComponentInChildren<Text>().text = tankNames[tankNames.Length - i-1];
        }

    }



	public void clickBtn() {
        gm.LeaveRoom();
	}
}
