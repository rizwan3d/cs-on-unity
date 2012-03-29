using UnityEngine;
using System.Collections;

/*
 * Lop nay khai bao cac thuoc tinh va cac phuong thuc can ban mot khau sung can phai co
 * Controller cua sung se phai ke thua lop nay
 * 
*/

public class GunBehaviour : MonoBehaviour{
	public int MAX_CARRYING_BULLETS = 70;			// so dan toi' da co the mang theo
	public int currentCarryingBullet = 70;			// so dan hien dang mang theo
		
	protected int _MAX_BULLET_CAN_FIRE = 12;			// so dan toi da sung' co the chua'
	public int currentBulletCanFire = 12;			// so dan trong sung dang co
	
	public float MAX_TIME_FOR_ONE_SHOOT = 0.1f;		// thoi gian roi da ban' 1 vien dan	
	
	public float MAX_TIME_TO_RELOAD = 2;			// thoi gian toi da de nap. lai dan.
	
	public float maxExecuteTime = 0;				// thoi gian toi da de thuc hien 1 trang thai
	public float currentExecuteTime = 1.0f;			// thoi gian da thuc hien cua trang thai hien tai
	
	public float shootRange = 100;					// khoang cach toi da co ban'
	public float force = 10;						// luc. tac dung len rigidBody bi ban' trung'
	
	public float lagShootAngle = 1;						// goc' lech khi ban' do bi giat
	public int bulletCost = 50;						// gia tien mua 1 vien dan cho sung' loai nay`
	public int gunCost = 2000;						// gia tien mua 1 khau sung loai nay
	
	public GameObject bulletHole;					// bullet hole
	
	
	// cac' bien trang thai
	bool isReloading = false;						// dang nap dan phai ko?
	
	
	public int MAX_BULLET_CAN_FIRE
	{
		get
		{
			return _MAX_BULLET_CAN_FIRE;
		}
		set
		{
			if(value > 0)
			{
				_MAX_BULLET_CAN_FIRE = value;
				currentBulletCanFire = value;
			}
		}
	}
	
	public bool IsReloading
	{
		get
		{
			return isReloading;
		}
		set
		{
			isReloading = value;
		}	
	}
	
	public bool IsEmptyBulletCarrying				// con dan mang theo ko?
	{
		get
		{
			return currentCarryingBullet <= 0;
		}
	}
	
	public bool IsReadyToShoot						// san sang ban'
	{
		get
		{
			if(!IsReloading && currentBulletCanFire > 0 && currentExecuteTime >= maxExecuteTime)
				return true;
			return false;
		}
	}
	
	public bool IsReadyToReload
	{
		get
		{
			if(currentExecuteTime >= maxExecuteTime && 
				currentBulletCanFire < MAX_BULLET_CAN_FIRE &&
				!IsEmptyBulletCarrying)
				return true;
			return false;
		}
	}
	
	public bool IsExecuteCompletely
	{
		get
		{
			return currentExecuteTime >= maxExecuteTime;
		}
	}
	
	protected void StartNewState(float timeToExecute)
	{
		this.maxExecuteTime = timeToExecute;
		this.currentExecuteTime = 0;
	}
	
	// can goi ham` nay` trong ShootABullet() ngay truoc khi tien hanh ban' 1 vien dan
	protected void StartShootABullet()
	{
		StartNewState(MAX_TIME_FOR_ONE_SHOOT);
		currentBulletCanFire--;
		GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
		System.Random ran = new System.Random();
		Vector3 direction = Quaternion.AngleAxis(ran.Next(-(int)(lagShootAngle*1000),(int)(lagShootAngle*1000))*0.001f, Vector3.up)
							*Quaternion.AngleAxis(ran.Next(-(int)(lagShootAngle*1000),(int)(lagShootAngle*1000))*0.001f, Vector3.left)
							*Quaternion.AngleAxis(ran.Next(-(int)(lagShootAngle*1000),(int)(lagShootAngle*1000))*0.001f, Vector3.forward)
							*((Vector3)cam.transform.TransformDirection(Vector3.forward));
		
		Vector3 startRayPoint = cam.transform.position + direction*0.5f;/*+ 
								new Vector3(ran.Next(-(int)(exactPoint*1000),(int)(exactPoint*1000))*0.001f,
											ran.Next(-(int)(exactPoint*1000),(int)(exactPoint*1000))*0.001f,
											ran.Next(-(int)(exactPoint*1000),(int)(exactPoint*1000))*0.001f);*/
		RaycastHit hit = new RaycastHit();
		
		//Did we hit anything?
		if(Physics.Raycast(startRayPoint, direction,out hit,shootRange))
		{	
			if(hit.rigidbody)
			{
				hit.rigidbody.AddForceAtPosition(force * direction, hit.point);
			}
			Instantiate(bulletHole, hit.point + hit.normal*(0.001f), Quaternion.FromToRotation(Vector3.up, hit.normal));					
		}
	}
	
	protected virtual void ShootABullet()
	{
		StartShootABullet();		
	}
	
	// can goi ham` nay` trong Reload() ngay truoc khi tien hanh thay dan
	protected void StartReload()
	{
		isReloading = true;
		StartNewState(MAX_TIME_TO_RELOAD);
		// 
	}
	
	protected virtual void Reload()
	{
		StartReload();
		currentBulletCanFire = MAX_BULLET_CAN_FIRE;
	}
	
	public virtual int BuyBullet()							// tra ve so tien da dung de mua dan.
	{
		return 0;
	}
}
