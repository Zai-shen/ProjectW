using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagment : MonoBehaviour
{
    public string _buttonName= "DMG";

    private int _currentBread;

    private int _level1Price= 100;
    private int _level2Price= 150;
    private int _level3Price= 200;

    private int _level1Bonus= 15;
    private int _level2Bonus= 10;
    private int _level3Bonus= 5;



    private int _currentPrice= 100;

    private int _maxLevel = 3;

    private int _currentMetaLevel;
    private int _nextBonus=0;


    public Text PowerTitle;
    public Text CostNLevel; 
    public Text BonusAmount; 


    public string LevelText = "LVL";
    public string MaxedLeveled = "Maxed Out";
    private int tmpint;
    private string tmpstring;
    private string lvlBonusString ="_Level";
    private string lvlCostString = "_cost_Level";
    private MetaShop _ms;


    void Awake()
    {
        _currentBread = PlayerPrefs.GetInt("BakedBread");
        _currentMetaLevel=PlayerPrefs.GetInt(_buttonName);

        tmpstring = _buttonName+ lvlCostString+ 1.ToString();
        _level1Price= PlayerPrefs.GetInt(tmpstring);

        tmpstring = _buttonName+ lvlBonusString+ 1.ToString();
        _level1Bonus = PlayerPrefs.GetInt(tmpstring);

        tmpstring = _buttonName+ lvlCostString+ 2.ToString();
        _level2Price= PlayerPrefs.GetInt(tmpstring);
        tmpstring = _buttonName+ lvlBonusString+ 2.ToString();
        _level2Bonus = PlayerPrefs.GetInt(tmpstring);

        tmpstring = _buttonName+ lvlCostString+ 3.ToString();
        _level3Price= PlayerPrefs.GetInt(tmpstring);
        tmpstring = _buttonName+ lvlBonusString+ 3.ToString();
        _level3Bonus = PlayerPrefs.GetInt(tmpstring);

        Debug.Log(_currentMetaLevel);



        updateThisButton();
        checkAffordable();
        Button thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(metaLevelUp);
        updateThisButton();
    }    

    void updateThisButton(){


        _currentBread = PlayerPrefs.GetInt("BakedBread");
        _currentMetaLevel=PlayerPrefs.GetInt(_buttonName);


        switch(_currentMetaLevel) 
        {
            case 0:
                _currentPrice = _level1Price;
                _nextBonus = _level1Bonus;
                break;
            case 1:
                _currentPrice = _level2Price;
                _nextBonus = _level2Bonus;
                break;
            case 2:
                _currentPrice = _level3Price;
                _nextBonus = _level3Bonus;
                break;
            case 3:
                _currentPrice = 1000000;
                _nextBonus= 0;
                break;
            default:
                // code block
                break;
        }

        tmpint= _currentMetaLevel +1;
        PowerTitle.text = _buttonName;

        if (_currentMetaLevel>= _maxLevel){
            CostNLevel.text = MaxedLeveled;
            BonusAmount.text = "No more Bonus";

        } else {

            CostNLevel.text = _currentPrice.ToString()+ " Bread for "+LevelText + tmpint.ToString();
            BonusAmount.text = _nextBonus.ToString() + " % Bonus";
        }

        Button thisButton = GetComponent<Button>();
        if ((_currentBread < _currentPrice) |( _currentMetaLevel>= _maxLevel)){
            thisButton.enabled = false;
        } else {
            thisButton.enabled = true;
        }
    }

    void metaLevelUp () {
        // reduce breadcount and save to player pref
        _currentBread-= _currentPrice;
        PlayerPrefs.SetInt("BakedBread",_currentBread);

        // level up Metalevel and save to playerpref
        _currentMetaLevel +=1;
        PlayerPrefs.SetInt(_buttonName,_currentMetaLevel);
        PlayerPrefs.Save();

        // increase current price to next level
        switch(_currentMetaLevel) 
        {
            case 0:
                _currentPrice = _level1Price;
                _nextBonus = _level1Bonus;
                break;
            case 1:
                _currentPrice = _level2Price;
                _nextBonus = _level2Bonus;
                break;
            case 2:
                _currentPrice = _level3Price;
                _nextBonus = _level3Bonus;
                break;
            case 3:
                _currentPrice = 1000000;
                _nextBonus= 0;
                break;
            default:
                // code block
                break;
        }


        updateThisButton();

        checkAffordable();
        _ms.updateButtons();

        // for all buttons check if price is still high enough

    }




    public void checkAffordable(){


        _currentBread = PlayerPrefs.GetInt("BakedBread");
        _currentMetaLevel=PlayerPrefs.GetInt(_buttonName);

        Button thisButton = GetComponent<Button>();


        if ((_currentBread < _currentPrice) |( _currentMetaLevel>= _maxLevel)){
            thisButton.enabled = false;
        } else {
            thisButton.enabled = true;
        }
    }

    public void registerPanel(MetaShop ms){
        _ms= ms;
        Debug.Log("in button registerPanel");
    }







    



}
