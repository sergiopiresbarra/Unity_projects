using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;

    public Text playerSpeedValueText;
    public float playerSpeedValue;
    public Text wallJumpValueText;
    public float wallJumpValue;
    public GameObject gameOver;
    public GameObject nextLevel;
    public GameObject pausePanel;

    private bool onOptions = false;

    public Slider PlayerSpeedSlider;
    public Slider wallJumpSlider;

    public GameObject optionsPanel;

    public static GameController instance;

    public bool canShowInventory = true;
    
    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        //PlayerPrefs.SetInt("score", totalScore);
        
        if(PlayerPrefs.HasKey("vida")){

        }
        else{
            // if(PlayerPrefs.GetFloat("vida") <= 0f)
            PlayerPrefs.SetFloat("vida", 100f);
        }
        

        if(PlayerPrefs.HasKey("score")){

        }
        else{
            PlayerPrefs.SetInt("score", 0);
        }

        if(PlayerPrefs.HasKey("playerSpeedValue")){
            playerSpeedValue = PlayerPrefs.GetFloat("playerSpeedValue");
            wallJumpValue = PlayerPrefs.GetFloat("wallJumpValue");
            //playerSpeedValueText.text = playerSpeedValue.ToString();
        }
        else{
            PlayerPrefs.SetFloat("playerSpeedValue", 8f);
            PlayerPrefs.SetFloat("wallJumpValue", 8f);
            wallJumpValue = 8;
            playerSpeedValue = 8;
            //playerSpeedValueText.text = playerSpeedValue.ToString();
        }

        if(SceneManager.GetActiveScene().buildIndex != 0){
            Player.instance.life.value = PlayerPrefs.GetFloat("vida");
            totalScore = PlayerPrefs.GetInt("score");
            scoreText.text = totalScore.ToString();
        }
        if(SceneManager.GetActiveScene().buildIndex >=0){
            SoundManager.PlaySound(SoundManager.Sound.SongEmergenceLvl3, 1f, false);
        }
        //if(SceneManager.GetActiveScene().buildIndex ==0){
        //    SoundManager.PlaySound(SoundManager.Sound.SongEmergenceLvl3, 1f, false);
        //}
        //Debug.Log(PlayerPrefs.GetInt("score"));
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 1){
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else{
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if(onOptions ==true){
            checkOptionsMenu();
        }
        //Player.instance.life.value = PlayerPrefs.GetInt("vida");
    }

    public void Continue(){
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void UpdateScoreText(){
        //score++;
        //totalScore = PlayerPrefs.GetInt("score");
        scoreText.text = totalScore.ToString();
        //totalScore-=score;
        //PlayerPrefs.SetInt("score", totalScore);//==============!!
    }

    public void ShowGameOver(){
        gameOver.SetActive(true);
        canShowInventory = false;
        //PlayerPrefs.SetInt("score", 0); //===============!!
    }
    public void ResetPoints(){
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetFloat("vida", 100f);
        PlayerPrefs.SetInt("tipeWeapon", 0);
    }

    public void RestartGame(string lvlName){
        //Player.instance.inventory.GetInventarioInicialFase();
        canShowInventory =true;
        SceneManager.LoadScene(lvlName);
    }

    public void ShowNextLevel(){
        nextLevel.SetActive(true);
        PlayerPrefs.SetFloat("vida", Player.instance.life.value);
        PlayerPrefs.SetInt("score", totalScore);
    }

    public void NextGame(string lvlname){
        canShowInventory = true;
        SceneManager.LoadScene(lvlname);
        //Player.instance.Speed = 8;
    }

    public void Exit(){
        Application.Quit();
    }

    public void ShowOptions(){
        optionsPanel.SetActive(true);
        onOptions = true;
        checkOptions();
    }

    public void BackToMenu(){
        PlayerPrefs.SetFloat("playerSpeedValue", PlayerSpeedSlider.value);
        PlayerPrefs.SetFloat("wallJumpValue", wallJumpSlider.value);
        optionsPanel.SetActive(false);
        onOptions = false;
    }

    public void checkOptions(){
        PlayerSpeedSlider.value = playerSpeedValue;
        wallJumpSlider.value = wallJumpValue;
        playerSpeedValueText.text = playerSpeedValue.ToString();
        wallJumpValueText.text = wallJumpValue.ToString();
    }

    public void checkOptionsMenu(){
        playerSpeedValue = PlayerSpeedSlider.value;
        wallJumpValue = wallJumpSlider.value;
        playerSpeedValueText.text = playerSpeedValue.ToString();
        wallJumpValueText.text = wallJumpValue.ToString();
    }
   
}
