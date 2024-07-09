using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject effect;
    public Animator anim;
    public GameObject Button;
    public Animator menu;
    public GameObject menubutton;

    [SerializeField] private int score;
    [SerializeField] private int clickGain = 1;
    [SerializeField] private int autoClickGain = 0;
    [SerializeField] private int upgradeClickCost = 10;
    [SerializeField] private int upgradeAutoClickCost = 30;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text upgradeClickCostText;
    [SerializeField] private Text upgradeAutoClickCostText;

    private void Awake()
    {
        anim = Button.GetComponent<Animator>();
        menu = menubutton.GetComponent<Animator>();

        upgradeClickCost = PlayerPrefs.GetInt("UpgradeCost", 20);
        upgradeAutoClickCost = PlayerPrefs.GetInt("UpgradeAutoCost", 100);

        clickGain = PlayerPrefs.GetInt("gain", 1);
        autoClickGain = PlayerPrefs.GetInt("autogain", 0);
    }

    private void Start()
    {
        StartCoroutine(AutoClickCoroutine());
        score = PlayerPrefs.GetInt("money", 0);
    }

    private void Update()
    {

        scoreText.text = score.ToString();
        upgradeClickCostText.text = upgradeClickCost.ToString();
        upgradeAutoClickCostText.text = upgradeAutoClickCost.ToString();
    }

    public void Click()
    {
        StartCoroutine(click());
        score += clickGain;
        PlayerPrefs.SetInt("money", score);
    }

    public IEnumerator click()
    {
        anim.SetBool("Click", true);
        yield return new WaitForSeconds(0.25f);
        Instantiate(effect, transform.position, Quaternion.identity);
        anim.SetBool("Click", false);
    }

    public GameObject warn;

    public IEnumerator warning()
    {
        warn.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        warn.SetActive(false);
    }

    public void UpgradeClick()
    {
        if (score >= upgradeClickCost)
        {
            score -= upgradeClickCost;
            clickGain+=1;
            PlayerPrefs.SetInt("gain", clickGain);
            PlayerPrefs.SetInt("UpgradeCost", upgradeClickCost);

            switch (upgradeClickCost)
            {
                case 20:
                    upgradeClickCost += 80;
                    break;
                case 100:
                    upgradeClickCost += 100;
                    break;
                case 200:
                    upgradeClickCost += 150;
                    break;
                case 350:
                    upgradeClickCost += 150;
                    break;
                case 500:
                    upgradeClickCost += 300;
                    break;
                case >=800:
                    upgradeClickCost += 600;
                    break;
            }
        }
        else
        {
            StartCoroutine(warning());
            Debug.Log("Не хватает очков для покупки");
        }
    }

    public void UpgradeAutoClick()
    {
        if (score >= upgradeAutoClickCost)
        {
            score -= upgradeAutoClickCost;
            autoClickGain+=1;
            PlayerPrefs.SetInt("autogain", autoClickGain);
            PlayerPrefs.SetInt("UpgradeAutoCost", upgradeAutoClickCost);

            switch (upgradeAutoClickCost)
            {
                case 100:
                    upgradeAutoClickCost += 150;
                    break;
                case 250:
                    upgradeAutoClickCost += 150;
                    break;
                case 400:
                    upgradeAutoClickCost += 200;
                    break;
                case 600:
                    upgradeAutoClickCost += 250;
                    break;
                case 850:
                    upgradeAutoClickCost += 500;
                    break;
                case >=1350:
                    upgradeAutoClickCost += 1200;
                    break;
            }
        }
        else
        {
            StartCoroutine(warning());
            Debug.Log("Не хватает очков для покупки");
        }
    }

        IEnumerator AutoClickCoroutine()
        {
            while (true)
            {
                score += autoClickGain;
                yield return new WaitForSeconds(1);
            }
        }

        public void Menu()
        {
            menu.SetBool("Menu", true);
            menu.SetBool("Back", false);
        }

        public void BackToMenu()
        {
            menu.SetBool("Back", true);
            menu.SetBool("Menu", false);
        }
}