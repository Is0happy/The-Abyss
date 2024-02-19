using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int DeadsceneIndex;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(DeadScene(0.2f));
    }

    IEnumerator DeadScene(float duration)
    {
        yield return new WaitForSeconds(duration);
        AsyncOperation operation = SceneManager.LoadSceneAsync(DeadsceneIndex);
    }
}
