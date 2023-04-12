using UnityEngine;
using System.Collections;
using InfimaGames.LowPolyShooterPack;
using Random = UnityEngine.Random;

namespace InfimaGames.LowPolyShooterPack.Legacy
{
	public class Projectile : MonoBehaviour
	{
		[Range(5, 100)]
		[Tooltip("Golyo prefab eltunesi ideje")]
		public float destroyAfter;
		[Tooltip("Ha true a golyo eltunik utkozeskor")]
		public bool destroyOnImpact = false;
		[Tooltip("Minimum eltunesi ido utkozes utan")]
		public float minDestroyTime;
		[Tooltip("Maximum eltunesi ido utkozes utan")]
		public float maxDestroyTime;
		[Header("Impact Effect Prefabs")]
		public Transform[] bloodImpactPrefabs;
		public Transform[] metalImpactPrefabs;
		public Transform[] dirtImpactPrefabs;
		public Transform[] concreteImpactPrefabs;
		public GameObject hitEffect;


		private void Start()
		{
			var gameModeService = ServiceLocator.Current.Get<IGameModeService>();
			Physics.IgnoreCollision(gameModeService.GetPlayerCharacter().GetComponent<Collider>(),
			GetComponent<Collider>());

			StartCoroutine(DestroyAfter());
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.GetComponent<Projectile>() != null)
				return;
			if (!destroyOnImpact)
			{
				StartCoroutine(DestroyTimer());
			}
			else
			{
				Destroy(gameObject);
			}

			if (collision.transform.tag == "Blood")
			{
				Instantiate(bloodImpactPrefabs[Random.Range
				(0, bloodImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
				Destroy(gameObject);
			}
			
			AiController targetHealth = collision.gameObject.GetComponent<AiController>();
			if (collision.gameObject.CompareTag("Opponent"))
        	{
				targetHealth.numHits++;
				if (targetHealth.numHits >= 4)
				{
					targetHealth.isDead = true;
					targetHealth.Die();
				}
				else
				{
					targetHealth.TakeDamage();
					Instantiate(bloodImpactPrefabs[Random.Range(0, bloodImpactPrefabs.Length)], transform.position,Quaternion.LookRotation(collision.contacts[0].normal));
				}
       		}	

			if (collision.transform.tag == "Metal")
			{
				Instantiate(metalImpactPrefabs[Random.Range
				(0, bloodImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
				Destroy(gameObject);
			}

			if (collision.transform.tag == "Dirt")
			{
				Instantiate(dirtImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				Destroy(gameObject);
			}

			if (collision.transform.tag == "Concrete")
			{
				Instantiate(concreteImpactPrefabs[Random.Range
				(0, bloodImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
				Destroy(gameObject);
			}

			if (collision.transform.tag == "Target")
			{
				collision.transform.gameObject.GetComponent
					<TargetScript>().isHit = true;
				Destroy(gameObject);
			}

			if (collision.transform.tag == "ExplosiveBarrel")
			{
				collision.transform.gameObject.GetComponent
				<ExplosiveBarrelScript>().explode = true;
				Destroy(gameObject);
			}

			if (collision.transform.tag == "GasTank")
			{
				collision.transform.gameObject.GetComponent
				<GasTankScript>().isHit = true;
				Destroy(gameObject);
			}
		}

		private IEnumerator DestroyTimer()
		{
			yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
			Destroy(gameObject);
		}

		private IEnumerator DestroyAfter()
		{
			yield return new WaitForSeconds(destroyAfter);
			Destroy(gameObject);
		}
	}
}