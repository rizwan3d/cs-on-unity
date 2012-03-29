using UnityEngine;
using System.Collections;

public class PlayerWeapons : MonoBehaviour 
{
	Transform machineGuns;
	int currentMachineGun = 1;
	// Use this for initialization
	void Start () 
	{
		machineGuns = ((Transform)((Transform)transform.GetChild(1)));
		for(int i=0;i<machineGuns.childCount;i++)
		{
			machineGuns.GetChild(i).gameObject.SetActiveRecursively(false);
		}
	}
	
	void Awake()
	{
		SelectWeapon(0);
	}
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SelectWeapon(0);
		}else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			SelectWeapon(1);
		}else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			SelectWeapon(2);
		}else if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			SelectWeapon(3);
		}		
	}
		
	void SelectWeapon(int index)
	{
		for(int i=0; i < transform.childCount; i++)
		{
			//active selected weapon
			if(i==index)
			{
				transform.GetChild(i).gameObject.SetActiveRecursively(true);
				if(i==1)
				{
					for(int j=0; j<machineGuns.childCount;j++)
					{
						machineGuns.GetChild(j).gameObject.SetActiveRecursively(false);
					}
					machineGuns.GetChild(currentMachineGun).gameObject.SetActiveRecursively(true);
				}
			}else //deactive all others weapons
			{
				transform.GetChild(i).gameObject.SetActiveRecursively(false);
			}
		}
	}
}