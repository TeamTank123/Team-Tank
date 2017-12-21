using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoOpoption : MonoBehaviour {

     private bool shield = true;
     private bool canister = true;
     private int map = 1; // 1 summer 2 winter



    void Start()
    {
        print(map+" "+shield);
    }
	// Update is called once per frame
	public void loadScene() {
        SceneManager.LoadScene(map+1);
        print(shield + " " + canister + " " + map);
        PlayerPrefs.SetString("shield", shield.ToString());
        PlayerPrefs.SetString("canister", canister.ToString());
    }

    public void mapChnaged(int map)
    {
        this.map = map;
    }

    public void shieldChanged(bool shield)
    {
        this.shield = shield;
    }
    public void canisterChanged(bool canister)
    {
        this.canister = canister;
    }
}
