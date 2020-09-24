using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshPro tmp;
    public AudioClip deathEffect;
    public List<AudioClip> coinClips;
    public List<AudioClip> damagedClips;
    public AudioSource source;
    private int score;

    private void Awake()
    {
        CharacterController.onDeath += DeathHandler;
        Obstacle.onObstacleHit += ObstacleHitHandler;
        Coin.onCoinCollected += AddToText;
    }

    private void ObstacleHitHandler()
    {
        int index = UnityEngine.Random.Range(0, damagedClips.Count);
        source.PlayOneShot(damagedClips[index]);
    }

    private void DeathHandler()
    {
        source.PlayOneShot(deathEffect);
        StartCoroutine(GameReset());
    }

    private IEnumerator GameReset()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void AddToText()
    {
        int index = UnityEngine.Random.Range(0, coinClips.Count);
        source.PlayOneShot(coinClips[index]);

        score++;
        tmp.text = score.ToString();
    }
}
