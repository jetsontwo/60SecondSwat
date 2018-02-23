using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Controller : MonoBehaviour {


    public GameObject explosion, end_bomb;

    private float time;
    public Text timer;
    public GameObject death_screen, win_screen, UI, player;
    private bool running = false;
    private AudioSource game_music, start_music;
    public float music_fadein_steps;


    public float time_limit;

    [Header("Music")]
    public GameObject start_music_holder;
    public GameObject game_music_holder;
    public GameObject end_music_holder;
    public GameObject win_music_holder;


    private bool gameover = false; 

    void Start()
    {
        Application.targetFrameRate = 60;
        start_music = start_music_holder.GetComponent<AudioSource>();
        game_music = game_music_holder.GetComponent<AudioSource>();
        Time.timeScale = 1;
        time = time_limit;
        StartCoroutine(FadeInMusic(start_music));
    }

	// Update is called once per frame
	void Update () {
        if(running)
        {
            time -= Time.deltaTime;
            timer.text = time.ToString("F2") + "\"";
            if(time >= (time_limit/2) && time < time_limit)
            {
                timer.color = new Color(1 - ((time- (time_limit / 2)) / (time_limit / 2)), 0.5f, 0);
            }
            else if(time < (time_limit / 2) && time > 0)
            {
                timer.color = new Color(1, (time / time_limit), 0);
                if(time <= 10)
                {
                    timer.transform.localScale += new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime);
                }
            }
            else if (time <= 0)
            {
                if(!gameover)
                    Game_Over();
                gameover = true;
                timer.text = "00:00" + "\"";
            }
        }
	}

    public void Win()
    {
        UI.SetActive(false);
        player.GetComponent<Player_Movement>().enabled = false;
        player.GetComponent<Player_Shooting>().enabled = false;
        Time.timeScale = 0.2f;
        running = false;
        win_screen.SetActive(true);
        game_music.Stop();
        win_music_holder.GetComponent<AudioSource>().Play();
    }

    public void Game_Over()
    {
        UI.SetActive(false);
        player.GetComponent<Player_Movement>().enabled = false;
        player.GetComponent<Player_Shooting>().enabled = false;
        Instantiate(explosion, end_bomb.transform.position, Quaternion.identity);
        end_bomb.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(end_bomb.GetComponent<Bomb_Controller>().expand_circle());
        Time.timeScale = 0.2f;
        running = false;
        death_screen.SetActive(true);
        end_music_holder.GetComponent<AudioSource>().Play();
        game_music.Stop();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player") &&  !running)
        {
            running = true;
            StartCoroutine(FadeInGameMusic());
        }
    }

    IEnumerator FadeInGameMusic()
    {
        StopCoroutine(FadeInMusic(start_music));
        game_music.Play();
        while (game_music.volume <= 0.3f)
        {
            game_music.volume = game_music.volume + music_fadein_steps;
            start_music.volume = start_music.volume - (music_fadein_steps * 2);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeInMusic(AudioSource source)
    {
        source.Play();
        while (source.volume <= 0.3f)
        {
            source.volume = source.volume + music_fadein_steps * 2;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
