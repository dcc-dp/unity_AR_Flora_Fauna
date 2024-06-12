using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    #region Deklarasi
    public List<ListSoal> listSoal ;
    public GameObject[] options;
    public int CurrentSoal;

    public GameObject PanelSelesai;
    public GameObject Gopanel;

    public Text SoalText;
    public Text skor;
    public Text papanSkor;

    public Button buttonSkor;
    

    public Text textWaktu;
    public Text textKesempatan;

    private AudioSource audioSource;

    int TotalSoal = 0;
    public int score;

    public int maxKesempatan = 3;
    public float waktuBermain = 180f;
    private int sisaKesempatan;
    private float sisaWaktu;
    #endregion
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        // buttonSkor.GetComponentInChildren<Text>().text = "1000";
        TotalSoal = listSoal.Count;
        sisaKesempatan = maxKesempatan; // Inisialisasi kesempatan
        sisaWaktu = waktuBermain; // Inisialisasi waktu

        PanelSelesai.SetActive(false);
        generateSoal();
        StartCoroutine(Timer());
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (skor != null)
        {
            skor.text = "Score: " + score;
        }

        if (papanSkor != null)
        {
            papanSkor.text  = "Score: " + score;
        }

        if (buttonSkor.GetComponentInChildren<Text>().text != null)
        {
            buttonSkor.GetComponentInChildren<Text>().text = "Score: " + score;
        }

        if (textKesempatan != null)
        {
            textKesempatan.text = sisaKesempatan.ToString();
        }

        if (textWaktu != null)
        {
            textWaktu.text = Mathf.RoundToInt(sisaWaktu).ToString();
        }
    }

    IEnumerator Timer()
    {
        while (sisaWaktu > 0)
        {
            yield return new WaitForSeconds(1f);
            sisaWaktu -= 1f;
            UpdateUI();
        }
        gameOver();
    }

    void gameOver()
    {
        Debug.Log("Game over dipanggil.");
        PanelSelesai.SetActive(true);
        skor.text = "Score anda : " + score + "/" + "100";
        papanSkor.text = "Score = " + score;
        buttonSkor.GetComponentInChildren<Text>().text = "Score = " + score;

        StopAllCoroutines(); // Menghentikan timer saat game over
    }

    public void gameOverSelesai()
    {
        Debug.Log("Game over dipanggil.");
        PanelSelesai.SetActive(true);
        skor.text = "Score anda : " + score + "/" + "100";
        papanSkor.text = "Score = " + score;
        buttonSkor.GetComponentInChildren<Text>().text = "Score = " + score;

        StopAllCoroutines(); // Menghentikan timer saat game over
    }

    public void retry()
    {
        StopAllCoroutines(); // Hentikan semua coroutine sebelum memulai ulang
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void benar()
    {
        PlaySound("benarSound");

        score += 10;
        listSoal.RemoveAt(CurrentSoal);
        generateSoal();
        UpdateUI(); // Perbarui UI setelah jawaban benar
    }

    public void salah()
    {
        PlaySound("salahSound");

        if (score <= 0)
        {
            score = 0;
        }
        else
        {
            score -= 5;
        }
        listSoal.RemoveAt(CurrentSoal);
        sisaKesempatan--; // Kurangi kesempatan jika jawaban salah
        UpdateUI(); // Perbarui UI setelah jawaban salah

        if (sisaKesempatan <= 0)
        {
            gameOver();
        }
        else
        {
            generateSoal();
        }
    }

    void setJawaban()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<JawabHandler>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = listSoal[CurrentSoal].Jawab[i];

            if (listSoal[CurrentSoal].JawabBenar == i + 1)
            {
                options[i].GetComponent<JawabHandler>().isCorrect = true;
            }
        }
    }

    void generateSoal()
    {
        if (listSoal != null && listSoal.Count > 0)
        {
            CurrentSoal = Random.Range(0, listSoal.Count);
            SoalText.text = listSoal[CurrentSoal].Soal;
            setJawaban();
        }
        else
        {
            Debug.Log("Soal sudah habis");
            gameOver();
        }
    }

    void PlaySound(string soundName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + soundName);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound file not found: " + soundName);
        }
    }
}
