using UnityEngine;
using System.Collections;
struct B43Animations
{
	public static string idle = "idle",
	idleNoSilencer = "idle_unsil",
	addSilencer = "add_silencer",
	detachSilencer = "detach_silencer",
	drawn = "drawn",
	drawnNoSilencer = "drawn_unsil",
	reload = "reload",
	reloadNoSilencer = "reload_unsil",
	shoot = "shoot",
	shootNoSilencer = "shoot_unsil";
};

public class B43LauncherController : GunBehaviour
{
	// shoot audio
	public AudioClip clipShootWithSilencer;
	public AudioClip clipShootNoSilencer;
	
	// reload audio
	public AudioClip clipReload;
	
	// pull audio
	public AudioClip clipPull;
	
	// drawn
	public AudioClip clipDrawn;
	
	// silencer
	public AudioClip clipAddSilencer;
	public AudioClip clipDetachSilencer;
	
	public bool isHasSilencer = false;
	public float MAX_TIME_TO_CHANGE_SILENCER;
	
	void ChangeSilencer()
	{		
		this.StartNewState(MAX_TIME_TO_CHANGE_SILENCER);
		if(isHasSilencer)
		{
			transform.GetChild(0).animation.Play(B43Animations.detachSilencer);
			audio.clip = clipDetachSilencer;
			audio.Play();
		}
		else
		{
			transform.GetChild(0).animation.Play(B43Animations.addSilencer);
			audio.clip = clipAddSilencer;
			audio.Play();
		}
		isHasSilencer = !isHasSilencer;
	}
	
	protected override void ShootABullet ()
	{
		base.ShootABullet ();
		// play audio
		if(audio)
		{
			if(audio.isPlaying)
				audio.Stop();
			if(!isHasSilencer)
				audio.clip = clipShootNoSilencer;
			else
				audio.clip = clipShootWithSilencer;
			audio.Play();
		}
		
		// play animation
		transform.GetChild(0).animation.Stop();
		if(isHasSilencer)
			transform.GetChild(0).animation.Play(B43Animations.shoot);
		else
			transform.GetChild(0).animation.Play(B43Animations.shootNoSilencer);
	}
	
	protected override void Reload ()
	{
		base.Reload ();
		if(isHasSilencer)
			transform.GetChild(0).animation.Play(B43Animations.reload);
		else
			transform.GetChild(0).animation.Play(B43Animations.reloadNoSilencer);
		if(audio)
		{
			audio.clip = clipReload;
			audio.Play();
		}
	}
	
	void OnEnable()
	{
		if(isHasSilencer)
		{
			transform.GetChild(0).animation.Play(B43Animations.drawn);
			audio.clip = clipDrawn;
			audio.Play();
		}else
		{
			transform.GetChild(0).animation.Play(B43Animations.drawnNoSilencer);
			audio.clip = clipDrawn;
			audio.Play();
		}
	}
	
	void Start()
	{
		this.MAX_BULLET_CAN_FIRE = 30;
		this.MAX_CARRYING_BULLETS = 120;
		this.MAX_TIME_TO_RELOAD = 2;
		this.MAX_TIME_FOR_ONE_SHOOT = 0.1f;
		this.MAX_TIME_TO_CHANGE_SILENCER = 2f;
		
		if(isHasSilencer)
		{
			transform.GetChild(0).animation.Play(B43Animations.drawn);
			audio.clip = clipDrawn;
			audio.Play();
		}else
		{
			transform.GetChild(0).animation.Play(B43Animations.drawnNoSilencer);
			audio.clip = clipDrawn;
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log("Execute time:" + currentExecuteTime + ", Max time: "+ maxExecuteTime + ", Has Silencer: " + isHasSilencer);
		currentExecuteTime += Time.deltaTime;				// tang thoi gian execute cua trang thai hien tai
		// auto reloading
		if(!IsReadyToShoot && IsReadyToReload) 				// khong trong trang thai reload & sung' da het dan
		{													// van con dan mang theo	
			Reload();
		}
		
		if(IsExecuteCompletely)
		{
			if(Input.GetMouseButtonDown(1))
			{
				ChangeSilencer();
			}
		}
		
		//reload theo yeu cau nguoi choi
		if(Input.GetKeyDown(KeyCode.R) && IsReadyToReload)
		{
			Reload();
		}
		
		if(Input.GetKeyDown(KeyCode.G))					// mua dan neu ng choi yeu cau`
		{
			BuyBullet();
		}
		
		if(!IsReloading)								// xu ly neu ko nam trong trang thai reload
		{
			if(IsReadyToShoot)
			{
				if(Input.GetButton("Fire1"))
				{
					ShootABullet();
				}
			}
		}else											// xu ly khi dang nam trong trang thai reload
		{
			if(IsExecuteCompletely)
			{
				IsReloading = false;		
			}
		}
	}
}
