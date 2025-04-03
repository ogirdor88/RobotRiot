using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCandy : MonoBehaviour
{
    [SerializeField] private AudioSource powerUpSound;
    private AudioSource newAudio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.AddComponent<PowerCandyEffect>();
            newAudio = Instantiate(powerUpSound, other.gameObject.transform);
            newAudio.gameObject.transform.parent = other.gameObject.transform;
            newAudio.Play();
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        //gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Audio Length: " + newAudio.clip.length);
        yield return new WaitForSeconds(newAudio.clip.length);
        Destroy(gameObject);
        Destroy(newAudio.gameObject);
        Debug.Log("Killed Audio");
    }
}
