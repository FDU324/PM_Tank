using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ShooterController : MonoBehaviour {

    TankModel tank;

    private Scrollbar bar;
    // Use this for initialization
    void Start () {
        tank = GetComponent<TankModel>();
        bar = GameObject.Find("ShooterCanvas/Scrollbar Vertical").GetComponent<Scrollbar>();
        GameObject.Find("ShooterCanvas/ShootButton").GetComponent<Button>().onClick.AddListener(delegate {
            tank.Shoot();
        });
    }
	
	// Update is called once per frame
	void Update () {
        
        float v = CrossPlatformInputManager.GetAxisRaw("RotateV_Mobile");
        tank.CannonRotate(v);
        float h = CrossPlatformInputManager.GetAxisRaw("RotateH_Mobile");
        tank.TowerRotate(h);
        
        /*
        float v = Input.GetAxis("RotateV");
        tank.CannonRotate(v);
        float h = Input.GetAxis("RotateH");
        tank.TowerRotate(h);
        */
        
        tank.ShooterViewFieldChange(bar.value);

    }


}
