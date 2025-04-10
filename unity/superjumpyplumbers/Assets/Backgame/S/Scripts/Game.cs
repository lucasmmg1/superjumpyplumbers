using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityStandardAssets._2D;

public class Game : MonoBehaviour
{
    #region Variables

        #region Player Variables
            public int p_lives, p_score;

            public bool p_isAlive;

            public PlatformerCharacter2D Player;
            public GameObject playerSpawner;
        #endregion

        int level;

        public Spawner[] spawners;
        
        [SerializeField]
        private TextMeshProUGUI txtScore, txtHighscore, txtLives;
    #endregion

    #region Unity Methods

    void Start()
        {
            Load();
            UpdateHUD();
        }

    void Update()
        {
        
        }
    #endregion

    #region Personalized Methods
        
        public void LoseLife()
        {
            if (p_lives > 0)
            {
                StartCoroutine(Respawn());
            }
            else
            {

            }
        }

        public IEnumerator Respawn()
        {
            p_isAlive = true;
            yield return new WaitForSeconds(2);
            PlatformerCharacter2D playerInstance = Instantiate(Player, playerSpawner.transform.position, Quaternion.identity);
            StopCoroutine(Respawn());
        }

        public void AddPoints(int points)
        {
            p_score += points;
            UpdateHUD();
            StartCoroutine(CheckForLevelCompletion());
        }

        private IEnumerator CheckForLevelCompletion()
        {
            yield return new WaitForEndOfFrame();

            foreach (Spawner spawner in spawners)
            {
                if (!spawner.isCompleted)
                {
                    yield return null;
                }
                else
                {
                    if (FindObjectOfType<Enemy>() != null)
                    {
                        yield return null;
                    }
                    else
                    {
                        StopCoroutine(CheckForLevelCompletion());
                        CompleteLevel();
                    }
                }
            }

        }
        
        void CompleteLevel()
        {
            Save();
            level++;

            if (SceneManager.sceneCountInBuildSettings > level)
            {
                SceneManager.LoadScene(level);
            }
            else
            {
                Debug.Log("Game Won!");
                EndGame();
            }

        }

        void Save()
        {
            PlayerPrefs.SetInt("p_score", p_score);
            PlayerPrefs.SetInt("p_lives", p_lives);
        }

        void Load()
        {
            p_score = PlayerPrefs.GetInt("p_score", 0);
            p_lives = PlayerPrefs.GetInt("p_lives", 3);
            

            txtHighscore.text = "Highscore: " + PlayerPrefs.GetInt("p_highscore", 0);
        }

        void NewGame()
        {
            level = 0;
            SceneManager.LoadScene(level);

            PlayerPrefs.DeleteKey("p_score");
            PlayerPrefs.DeleteKey("p_lives");
        }

        void EndGame()
        {
            if (p_score > PlayerPrefs.GetInt("p_highscore", 0))
            {
                PlayerPrefs.SetInt("p_highscore", p_score);
            }
            NewGame();
        }

        public void UpdateHUD()
        {
            txtScore.text = "Score: " + p_score;
            txtLives.text = "Lives: " + p_lives;
        }
    #endregion
}
