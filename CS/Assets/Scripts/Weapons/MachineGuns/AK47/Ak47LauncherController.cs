using UnityEngine;
using System.Collections;

public class Ak47LauncherController: MonoBehaviour 
{
	
	public int BULLETS_CAN_CARRY = 45;		// co the mang toi da 120 vien dan theo
	int bulletsCarrying;					// so vien dan hien dang mang theo
	
	public int MAX_BULLETS = 30;			// toi da 30 vien dan
	int	currentBullets = 30;				// so dan hien tai dang co
	
	public float SHOT_DURATION = 0.1f;		// khoang thoi gian ban' giua 2 vien dan
	public float RELOADING_DURATION = 2;	// thoi gian nap dan
	
	float currentMaxWaitingTime;				// thoi gian cho qua trang thai hien tai
	float currentWaitingTime;					// khoang thoi gian tu lan ban truoc den hien tai
	
	float shootRange = 100;
	float force = 10;						// effect to hit rigidBody
	public float exactPoint = 0.05f;
	public float MAX_LAG_POINT = 0.75f;
	public float lagShootAngle = 1f;
	public GameObject bulletHole;
	
	
	bool isReloading = false;
	
	public AudioClip[] reloadSequences;
	int nextPlayingIndex = 0;
	GameObject sequencePlayerRefObj;
	AudioSource sequencePlayer;
	
	public AudioClip shotSound;			
	public AudioClip reload_clipin;
	public AudioClip reload_clipout;
	public AudioClip emptyShot;
	
	
	public bool IsReadyToShoot
	{
		get
		{
			return currentBullets > 0;
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
	
	public bool IsStillHaveBullets
	{
		get
		{
			return bulletsCarrying > 0;
		}
	}
	
	
	
	
	// Use this for initialization
	void Start () {
		currentMaxWaitingTime = SHOT_DURATION;
		currentWaitingTime = SHOT_DURATION;
		bulletsCarrying = BULLETS_CAN_CARRY;
		
		InitSequencePlayer();
		
		if(audio)
		{
			audio.loop = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(Input.GetKeyDown(KeyCode.G))			
		{
			BuyBullets();
		}
		
		if(Input.GetKeyDown(KeyCode.R)&&!IsReloading && IsStillHaveBullets && currentBullets < MAX_BULLETS)
		{
			Reload();
		}
		
		
		currentWaitingTime += Time.deltaTime;	
		
		// neu khong dang trong trang thai nap dan thi cho phep ban'
		if(!IsReloading)
		{		
			// kiem tra xem co san sang cho viec ban' hay khong
			// neu khong san sang thi se tien hanh nap dan
			// auto reloading
			if(!IsReadyToShoot)
			{
				//neu con` dan mang theo thi moi' nap. duoc
				if(IsStillHaveBullets)
				{
					if(currentWaitingTime >= currentMaxWaitingTime)
					{
						Reload();
					}
				}else
				{
					if(Input.GetButton("Fire1"))
					{
						ShotABullet();
						if(exactPoint < MAX_LAG_POINT)
						{
							exactPoint += Time.deltaTime*0.1f;
						}else
							exactPoint = MAX_LAG_POINT;
					}else
					{
						exactPoint = 0.05f;
					}
				}
			}else //neu san sang thi cho ban'
			{
				if(Input.GetButton("Fire1"))
				{
					ShotABullet();
					if(exactPoint < MAX_LAG_POINT)
					{
						exactPoint += Time.deltaTime*0.1f;
					}else
						exactPoint = MAX_LAG_POINT;
				}else
				{
					exactPoint = 0.05f;
				}
			}		
		}else
		{
			// thoi gian doi cua trang thai hien tai da het
			// tien hanh dat lai thoi gian doi cho trang thai tiep theo
			if(currentWaitingTime >= currentMaxWaitingTime)
			{
				currentWaitingTime = 0;
				currentMaxWaitingTime = SHOT_DURATION;
				isReloading = false;
			}
		}
		if(sequencePlayer)
		{
			if(!sequencePlayer.isPlaying)
			{
				if(nextPlayingIndex < reloadSequences.Length)
				{
					sequencePlayer.clip = reloadSequences[nextPlayingIndex];
					sequencePlayer.Play();
					nextPlayingIndex++;
				}
			}
		}
		//Debug.Log("Dan trong bang: "+currentBullets + ", dan mang theo: "+bulletsCarrying);
	}
	float pe = 0;
	void InitSequencePlayer()
	{
		sequencePlayerRefObj = new GameObject("Temporary object (sequence sound)");
		sequencePlayer = (AudioSource)sequencePlayerRefObj.AddComponent("AudioSource");
		
		sequencePlayer.volume = 1;
		sequencePlayer.pitch = 1;
		
		sequencePlayerRefObj.transform.parent = transform;
		sequencePlayerRefObj.transform.position = transform.position;
		nextPlayingIndex = 100;
	}
	
	void ShotABullet()
	{		
		if(currentWaitingTime >= currentMaxWaitingTime)
		{
			currentWaitingTime = 0;
			if(currentBullets > 0)
			{
				//ban'
				currentBullets--;
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
				
				
				
				
				
				
				// am thanh ban' 1 vien dan
				if(audio)
				{
					audio.clip = shotSound;
					audio.Play();
				}
				//play shoot animation
				transform.GetChild(0).animation.Stop();
				transform.GetChild(0).animation.Play("shoot");
				
				
			}else
			{
				// am thanh bop' co` ma` ko co' dan
				if(audio)
				{					
					audio.clip = emptyShot;
					audio.Play();
				}
				//PlaySound(emptyShot);
			}
		}
	}
	
	void Reload()
	{
		// play animation
		transform.GetChild(0).animation.Play("reload");
		
		// do the logic
		currentWaitingTime = 0;
		currentMaxWaitingTime = RELOADING_DURATION;
		isReloading = true;
		
		int reloadAmount = MAX_BULLETS - currentBullets; 
		if(reloadAmount <= bulletsCarrying)
		{
			currentBullets += reloadAmount;
			bulletsCarrying -= reloadAmount;
		}else
		{
			currentBullets += bulletsCarrying;
			bulletsCarrying = 0;
		}
		nextPlayingIndex = 0;
	}
	
	void BuyBullets()
	{
		bulletsCarrying = 120;
	}	
}

/* Can cac bien luu tru cac thong tin
 * - luong dan toi da co the mang theo cua loai sung nay
 * - luong dan hien dang mang theo
 * - luong dan toi da co the co trong sung'
 * - luong dan hien con trong sung
 * - thoi gian de thuc hien viec ban' 1 vien dan
 * - khoang thoi gian da troi di ke tu khi vien dan dc ban
 * - thoi gian thuc hien viec nap dan
 * - khoang thoi gian da troi di ke tu khi bat thuc hien viec nap dan
 * 
 * 
 * Can cung cap cac phuong thuc
 * - ShotABullet()
 * - Reload()
 * - PlaySequenceSounds()
 * - BuyBullets() : se chuyen ham nay sang weapons-object ==> mua dan cung luc cho nhieu loai vu khi
 *note
 *
*/
