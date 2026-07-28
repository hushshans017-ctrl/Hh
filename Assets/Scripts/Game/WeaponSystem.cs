using UnityEngine;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour
{
    [System.Serializable]
    public class Gun
    {
        public string gunName;
        public string gunType; // Pistol, Rifle, Shotgun, Sniper, etc.
        public float fireRate;
        public float bulletSpeed;
        public int bulletDamage;
        public int bulletsPerShot;
        public float spread;
        public AudioClip shootSound;
        public GameObject bulletPrefab;
        public Color bulletColor;
        public int ammoCapacity;
        public float reloadTime;
        public Sprite gunSprite;
    }

    [SerializeField] private List<Gun> availableGuns = new List<Gun>();
    [SerializeField] private Transform firePoint;
    
    private int currentGunIndex = 0;
    private Gun currentGun;
    private float fireTimer = 0f;
    private float reloadTimer = 0f;
    private int currentAmmo;
    private bool isReloading = false;

    private void Start()
    {
        InitializeGuns();
        SelectGun(0);
    }

    private void InitializeGuns()
    {
        // ============ PISTOLS ============
        
        // Standard Pistol
        Gun standardPistol = new Gun
        {
            gunName = "Enforcer Pistol",
            gunType = "Pistol",
            fireRate = 0.3f,
            bulletSpeed = 15f,
            bulletDamage = 1,
            bulletsPerShot = 1,
            spread = 0f,
            bulletColor = Color.yellow,
            ammoCapacity = 30,
            reloadTime = 1.5f
        };
        availableGuns.Add(standardPistol);

        // Heavy Pistol
        Gun heavyPistol = new Gun
        {
            gunName = "Deagle (Heavy Pistol)",
            gunType = "Pistol",
            fireRate = 0.5f,
            bulletSpeed = 20f,
            bulletDamage = 3,
            bulletsPerShot = 1,
            spread = 2f,
            bulletColor = new Color(1f, 0.8f, 0f), // Gold
            ammoCapacity = 12,
            reloadTime = 2.0f
        };
        availableGuns.Add(heavyPistol);

        // ============ RIFLES ============

        // Assault Rifle
        Gun assaultRifle = new Gun
        {
            gunName = "AR-15 Assault Rifle",
            gunType = "Rifle",
            fireRate = 0.15f,
            bulletSpeed = 22f,
            bulletDamage = 2,
            bulletsPerShot = 1,
            spread = 3f,
            bulletColor = new Color(0.8f, 0.8f, 0.8f), // Silver
            ammoCapacity = 45,
            reloadTime = 2.0f
        };
        availableGuns.Add(assaultRifle);

        // Burst Rifle
        Gun burstRifle = new Gun
        {
            gunName = "Burst Rifle (3-Tap)",
            gunType = "Rifle",
            fireRate = 0.1f,
            bulletSpeed = 23f,
            bulletDamage = 2,
            bulletsPerShot = 3,
            spread = 4f,
            bulletColor = new Color(0.2f, 0.8f, 0.2f), // Green
            ammoCapacity = 39,
            reloadTime = 2.5f
        };
        availableGuns.Add(burstRifle);

        // ============ MACHINE GUNS ============

        // Light Machine Gun
        Gun lmg = new Gun
        {
            gunName = "M249 Light Machine Gun",
            gunType = "Machine Gun",
            fireRate = 0.08f,
            bulletSpeed = 20f,
            bulletDamage = 1,
            bulletsPerShot = 1,
            spread = 6f,
            bulletColor = Color.white,
            ammoCapacity = 120,
            reloadTime = 3.0f
        };
        availableGuns.Add(lmg);

        // Minigun
        Gun minigun = new Gun
        {
            gunName = "Vulcan Minigun",
            gunType = "Machine Gun",
            fireRate = 0.05f,
            bulletSpeed = 18f,
            bulletDamage = 1,
            bulletsPerShot = 3,
            spread = 8f,
            bulletColor = Color.magenta,
            ammoCapacity = 200,
            reloadTime = 4.0f
        };
        availableGuns.Add(minigun);

        // ============ SHOTGUNS ============

        // Combat Shotgun
        Gun combatShotgun = new Gun
        {
            gunName = "Combat Shotgun",
            gunType = "Shotgun",
            fireRate = 0.8f,
            bulletSpeed = 25f,
            bulletDamage = 3,
            bulletsPerShot = 6,
            spread = 30f,
            bulletColor = new Color(1f, 0.5f, 0f), // Orange
            ammoCapacity = 20,
            reloadTime = 2.0f
        };
        availableGuns.Add(combatShotgun);

        // Tactical Shotgun
        Gun tacticalShotgun = new Gun
        {
            gunName = "Tactical Shotgun",
            gunType = "Shotgun",
            fireRate = 1.0f,
            bulletSpeed = 28f,
            bulletDamage = 4,
            bulletsPerShot = 8,
            spread = 40f,
            bulletColor = new Color(0.9f, 0.6f, 0.1f), // Dark Orange
            ammoCapacity = 16,
            reloadTime = 2.5f
        };
        availableGuns.Add(tacticalShotgun);

        // ============ SNIPER RIFLES ============

        // Standard Sniper
        Gun sniper = new Gun
        {
            gunName = "Sniper Rifle",
            gunType = "Sniper",
            fireRate = 1.5f,
            bulletSpeed = 40f,
            bulletDamage = 5,
            bulletsPerShot = 1,
            spread = 0f,
            bulletColor = new Color(0.5f, 0.5f, 0.5f), // Gray
            ammoCapacity = 15,
            reloadTime = 3.0f
        };
        availableGuns.Add(sniper);

        // Heavy Sniper
        Gun heavySniper = new Gun
        {
            gunName = "Heavy Sniper",
            gunType = "Sniper",
            fireRate = 2.0f,
            bulletSpeed = 45f,
            bulletDamage = 7,
            bulletsPerShot = 1,
            spread = 0f,
            bulletColor = new Color(0.3f, 0.3f, 0.3f), // Dark Gray
            ammoCapacity = 10,
            reloadTime = 3.5f
        };
        availableGuns.Add(heavySniper);

        // ============ EXPLOSIVE WEAPONS ============

        // Rocket Launcher
        Gun rocketLauncher = new Gun
        {
            gunName = "Rocket Launcher",
            gunType = "Explosive",
            fireRate = 1.2f,
            bulletSpeed = 15f,
            bulletDamage = 8,
            bulletsPerShot = 1,
            spread = 0f,
            bulletColor = Color.red,
            ammoCapacity = 10,
            reloadTime = 3.5f
        };
        availableGuns.Add(rocketLauncher);

        // Grenade Launcher
        Gun grenadeLauncher = new Gun
        {
            gunName = "Grenade Launcher",
            gunType = "Explosive",
            fireRate = 0.9f,
            bulletSpeed = 12f,
            bulletDamage = 6,
            bulletsPerShot = 1,
            spread = 5f,
            bulletColor = new Color(1f, 0.5f, 0f), // Orange
            ammoCapacity = 12,
            reloadTime = 3.0f
        };
        availableGuns.Add(grenadeLauncher);

        // ============ ENERGY WEAPONS ============

        // Laser Gun
        Gun laserGun = new Gun
        {
            gunName = "Laser Gun",
            gunType = "Energy",
            fireRate = 0.4f,
            bulletSpeed = 35f,
            bulletDamage = 2,
            bulletsPerShot = 1,
            spread = 0f,
            bulletColor = Color.red,
            ammoCapacity = 50,
            reloadTime = 1.2f
        };
        availableGuns.Add(laserGun);

        // Plasma Rifle
        Gun plasmaRifle = new Gun
        {
            gunName = "Plasma Rifle",
            gunType = "Energy",
            fireRate = 0.5f,
            bulletSpeed = 20f,
            bulletDamage = 4,
            bulletsPerShot = 2,
            spread = 10f,
            bulletColor = new Color(0f, 1f, 1f), // Cyan
            ammoCapacity = 40,
            reloadTime = 2.0f
        };
        availableGuns.Add(plasmaRifle);

        // ============ SPECIAL WEAPONS ============

        // Freeze Ray
        Gun freezeRay = new Gun
        {
            gunName = "Freeze Ray",
            gunType = "Special",
            fireRate = 0.6f,
            bulletSpeed = 12f,
            bulletDamage = 2,
            bulletsPerShot = 1,
            spread = 0f,
            bulletColor = new Color(0f, 0.5f, 1f), // Light Blue
            ammoCapacity = 35,
            reloadTime = 2.0f
        };
        availableGuns.Add(freezeRay);

        // Pulse Rifle
        Gun pulseRifle = new Gun
        {
            gunName = "Pulse Rifle",
            gunType = "Special",
            fireRate = 0.3f,
            bulletSpeed = 25f,
            bulletDamage = 3,
            bulletsPerShot = 2,
            spread = 6f,
            bulletColor = new Color(1f, 0f, 1f), // Magenta
            ammoCapacity = 32,
            reloadTime = 2.2f
        };
        availableGuns.Add(pulseRifle);

        // Initialize ammo for current gun
        currentAmmo = currentGun.ammoCapacity;
    }

    public void Update()
    {
        fireTimer -= Time.deltaTime;
        reloadTimer -= Time.deltaTime;

        // Check for gun swap
        if (Input.GetKeyDown(KeyCode.E))
            NextGun();
        if (Input.GetKeyDown(KeyCode.Q))
            PreviousGun();
        
        for (int i = 0; i < availableGuns.Count && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectGun(i);
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    public void Shoot()
    {
        if (fireTimer > 0 || isReloading || currentAmmo <= 0)
            return;

        for (int i = 0; i < currentGun.bulletsPerShot; i++)
        {
            SpawnBullet();
        }

        currentAmmo--;
        fireTimer = currentGun.fireRate;

        if (currentAmmo <= 0 && currentAmmo < currentGun.ammoCapacity)
        {
            Reload();
        }

        AudioManager.Instance.PlaySFX("shoot");
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(
            currentGun.bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            float spreadAngle = Random.Range(-currentGun.spread, currentGun.spread);
            float angle = spreadAngle * Mathf.Deg2Rad;
            
            Vector2 direction = new Vector2(
                Mathf.Sin(angle),
                Mathf.Cos(angle)
            ).normalized;

            bulletRb.velocity = direction * currentGun.bulletSpeed;
        }

        // Set bullet color and damage
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = currentGun.bulletColor;
        }

        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDamage(currentGun.bulletDamage);
            projectile.SetGunType(currentGun.gunName);
        }
    }

    public void SelectGun(int index)
    {
        if (index >= 0 && index < availableGuns.Count)
        {
            currentGunIndex = index;
            currentGun = availableGuns[index];
            currentAmmo = currentGun.ammoCapacity;
            isReloading = false;
            reloadTimer = 0f;
            
            Debug.Log("Selected: " + currentGun.gunName + " (" + currentGun.gunType + ")");
            UIManager.Instance.UpdateWeaponDisplay(currentGun.gunName, currentAmmo, currentGun.ammoCapacity);
        }
    }

    public void NextGun()
    {
        currentGunIndex = (currentGunIndex + 1) % availableGuns.Count;
        SelectGun(currentGunIndex);
    }

    public void PreviousGun()
    {
        currentGunIndex--;
        if (currentGunIndex < 0)
            currentGunIndex = availableGuns.Count - 1;
        SelectGun(currentGunIndex);
    }

    public void Reload()
    {
        if (!isReloading && currentAmmo < currentGun.ammoCapacity)
        {
            isReloading = true;
            reloadTimer = currentGun.reloadTime;
            Debug.Log("Reloading " + currentGun.gunName);
        }

        if (isReloading && reloadTimer <= 0)
        {
            currentAmmo = currentGun.ammoCapacity;
            isReloading = false;
            Debug.Log("Reload complete!");
            UIManager.Instance.UpdateWeaponDisplay(currentGun.gunName, currentAmmo, currentGun.ammoCapacity);
        }
    }

    public Gun GetCurrentGun()
    {
        return currentGun;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public List<Gun> GetAllGuns()
    {
        return availableGuns;
    }
}
