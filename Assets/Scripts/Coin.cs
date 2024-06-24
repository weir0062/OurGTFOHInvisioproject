using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Please put the coin sound here")]
    
    [Tooltip("The clip on the sound goes in here")]
    public AudioClip CoinSound;




    [Header("Dont Worry about")]
    public Vector2 CoinLocation = Vector2.zero;
    public bool Collected = false;
    public AudioSource audioSource;

    void Start()
    {
        Vector3 direction = Vector3.down;

        float rayLength = 10.0f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, rayLength))
        {
            Debug.Log("Hit: " + hit.collider.name);

            Tile CoinTile =  hit.collider.gameObject.GetComponent<Tile>();

            if(CoinTile != null)
            {
                if(CoinTile.state != TileState.MaxDamaged && CoinTile.state != TileState.NonActive)
                {
                    Debug.Log("Hit Vaild Tile at " + CoinTile.GetPosition().x + " " + CoinTile.GetPosition().y);
                    CoinLocation = CoinTile.GetPosition();
                }
                else
                    Debug.Log("Hit invalid tile in state: " + CoinTile.state.ToString());
            }
        }
        else
        {
            Debug.Log("No hit");
        }

        Debug.DrawRay(transform.position, direction * rayLength, Color.red, 200.0f);

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.clip= CoinSound;
        audioSource.Stop();
    }


    public void PlayCoinSound()
    {
        GetComponentInChildren<RawImage>().enabled= false;
        StartCoroutine(PlaySoundAndWait(CoinSound, OnSoundFinished));
    }


    private IEnumerator PlaySoundAndWait(AudioClip clip, System.Action callback)
    {
        // Play the audio clip
        audioSource.clip = clip;
        audioSource.Play();

        // Wait until the clip has finished playing
        yield return new WaitForSeconds(clip.length);

        // Call the callback method
        callback();
    }

    private void OnSoundFinished()
    {
        // This method is called when the sound has finished playing
        gameObject.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {

    }

}
