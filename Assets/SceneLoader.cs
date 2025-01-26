using System.Collections;
using System.Collections.Generic;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public SceneField SceneField;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(SceneField.Name, LoadSceneMode.Single);
    }
}
