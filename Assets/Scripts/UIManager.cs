using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    private bool hasBeenToLandScape;
    private int amountOfMoneyHave;
    
    //OLD UI ELEMENTS
    [SerializeField] private GameObject portraitModePanel;
    [SerializeField] private TextMeshProUGUI chosenGroupOld;
    [SerializeField] private TextMeshProUGUI operatorCostOld;

    //OPERATOR IMAGES
    [SerializeField] private Sprite[] banditPictures;
    [SerializeField] private Sprite[] smokePictures;
    [SerializeField] private Image operatorPortrait;
    [SerializeField] private Image operatorStats;
    [SerializeField] private Image operatorPower;
    [SerializeField] private Image operatorMainWeapon;
    [SerializeField] private Image operatorSecondaryWeapon;
    
    [Space]

    
    //UI-ELEMENT VARIABLES
    [SerializeField] private GameObject landscapeModePanel;
    private UIDocument _document;
    private VisualElement banditOperatorPic;
    private VisualElement smokeOperatorPic;
    
    //UI PICS
    private VisualElement smokeMainPowerPic;
    private VisualElement smokeMainWeaponPic;
    private VisualElement smokeSecondaryWeaponPic;
    private VisualElement smokeStats;
    
    //LABEL REFS
    private Label uiDocumentLabelMoney;
    private Label currentGroupChosen;
    private Label operatorCost;
    
    private Button newSettingsButton;
    private Button smokeOperatorButton;
    private Button banditOperatorButton;
    private Button USSRButton;
    private Button GSG9Button;

    void Update()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
                portraitModePanel.SetActive(true);
                landscapeModePanel.SetActive(false);
                Debug.Log("Portrait mode");
                hasBeenToLandScape = false;
                break;
            case ScreenOrientation.Landscape:
                landscapeModePanel.SetActive(true);
                portraitModePanel.SetActive(false);
                ChangeMoney();
                ActivateLandScapeMode();
                Debug.Log("Landscape mode");
                break;
        }
    }

    private void ChangeMoney()
    {
        uiDocumentLabelMoney.text = amountOfMoneyHave + " $";
        var mainUIElement = _document.rootVisualElement;

        uiDocumentLabelMoney = mainUIElement.Q<Label>("RenownAmount");
        uiDocumentLabelMoney.text = amountOfMoneyHave + " $";
    }

    private void GetMoreMoney()
    {
        amountOfMoneyHave += 150;
    }

    public void SettingsButton()
    {
        Debug.Log("Settings button hello yes?");
    }

    private void OperatorChange(int Operator)
    {
        _document = landscapeModePanel.GetComponent<UIDocument>();
        var mainUIElement = _document.rootVisualElement;
        banditOperatorPic = mainUIElement.Q<VisualElement>("CharacterPortrait");
        switch (Operator)
        {
            case 0:
                banditOperatorPic.visible = false;
                smokeOperatorPic.visible = true;
                smokeMainPowerPic.visible = true;
                smokeMainWeaponPic.visible = true;
                smokeSecondaryWeaponPic.visible = true;
                smokeStats.visible = true;
                operatorCost.text = "Cost: 5400";
                break;
            case 1:
                banditOperatorPic.visible = true;
                smokeOperatorPic.visible = false;
                smokeMainPowerPic.visible = false;
                smokeMainWeaponPic.visible = false;
                smokeSecondaryWeaponPic.visible = false;
                smokeStats.visible = false;
                operatorCost.text = "Cost: 6400";
                break;
        }
    }

    private void ChangeGroup(int groupChange)
    {
        currentGroupChosen.text = groupChange switch
        {
            0 => "USSR",
            1 => "GSG9",
            _ => currentGroupChosen.text
        };
    }

    public void ChangeGroupOldUI(int groupChange)
    {
        switch (groupChange)
        {
            case 0:
                chosenGroupOld.text = "GSG9";
                break;
            case 1:
                chosenGroupOld.text = "USSR";
                break;
        }
    }

    public void ChangeCharacterOld(int characterIndex)
    {
        Debug.Log("test");
        switch (characterIndex)
        {
            case 0:
                operatorPortrait.sprite = banditPictures[0];
                operatorStats.sprite = banditPictures[1];
                operatorPower.sprite = banditPictures[2];
                operatorMainWeapon.sprite = banditPictures[3];
                operatorSecondaryWeapon.sprite = banditPictures[4];
                operatorCostOld.text = "Cost: 6400";
                Debug.Log("Bandit");
                break;
            case 1:
                operatorPortrait.sprite = smokePictures[0];
                operatorStats.sprite = smokePictures[1];
                operatorPower.sprite = smokePictures[2];
                operatorMainWeapon.sprite = smokePictures[3];
                operatorSecondaryWeapon.sprite = smokePictures[4];
                operatorCostOld.text = "Cost: 5400";
                Debug.Log("Smoke");
                break;
        }
    }

    private void ActivateLandScapeMode()
    {
        if (!hasBeenToLandScape)
        {
            amountOfMoneyHave += 150;
            _document = landscapeModePanel.GetComponent<UIDocument>();
            var mainUIElement = _document.rootVisualElement;
            smokeStats = mainUIElement.Q<VisualElement>("SmokeStats");

            smokeOperatorPic = mainUIElement.Q<VisualElement>("CharacterSmoke");
            //LABEL REFS
            uiDocumentLabelMoney = mainUIElement.Q<Label>("RenownAmount");
            currentGroupChosen = mainUIElement.Q<Label>("GroupChosen");
            operatorCost = mainUIElement.Q<Label>("CostOperator");
            operatorCost.text = "Cost: 6400";
            currentGroupChosen.text = "USSR";
        
            //BUTTON REFS
            newSettingsButton = mainUIElement.Q<Button>("SettingsButton");
            smokeOperatorButton = mainUIElement.Q<Button>("SmokeButton");
            banditOperatorButton = mainUIElement.Q<Button>("BanditButton");
            USSRButton = mainUIElement.Q<Button>("USSRButton");
            GSG9Button = mainUIElement.Q<Button>("GSG9Button");
            smokeOperatorPic.visible = false;
        
            //IMAGE REFS
            smokeMainPowerPic = mainUIElement.Q<VisualElement>("MainPowerSmoke");
            smokeMainWeaponPic = mainUIElement.Q<VisualElement>("MainWeaponSmoke");
            smokeSecondaryWeaponPic = mainUIElement.Q<VisualElement>("SecondaryWeaponSmoke");
            smokeMainPowerPic.visible = false;
            smokeMainWeaponPic.visible = false;
            smokeSecondaryWeaponPic.visible = false;
            smokeStats.visible = false;

            //BUTTON CALLBACK
            newSettingsButton.RegisterCallback<ClickEvent>(ev => SettingsButton());
            smokeOperatorButton.RegisterCallback<ClickEvent>(ev => OperatorChange(0));
            banditOperatorButton.RegisterCallback<ClickEvent>(ev => OperatorChange(1));
            USSRButton.RegisterCallback<ClickEvent>(ev => ChangeGroup(0));
            GSG9Button.RegisterCallback<ClickEvent>(ev => ChangeGroup(1));
        
            uiDocumentLabelMoney.text = amountOfMoneyHave + " $";
        }

        hasBeenToLandScape = true;
    }
    private void OnEnable()
    {
        amountOfMoneyHave += 150;
        _document = landscapeModePanel.GetComponent<UIDocument>();
        var mainUIElement = _document.rootVisualElement;
        smokeStats = mainUIElement.Q<VisualElement>("SmokeStats");
        chosenGroupOld.text = "GSG9";
        operatorCostOld.text = "Cost: 6400";

        smokeOperatorPic = mainUIElement.Q<VisualElement>("CharacterSmoke");
        //LABEL REFS
        uiDocumentLabelMoney = mainUIElement.Q<Label>("RenownAmount");
        currentGroupChosen = mainUIElement.Q<Label>("GroupChosen");
        operatorCost = mainUIElement.Q<Label>("CostOperator");
        operatorCost.text = "Cost: 6400";
        currentGroupChosen.text = "USSR";
        
        //BUTTON REFS
        newSettingsButton = mainUIElement.Q<Button>("SettingsButton");
        smokeOperatorButton = mainUIElement.Q<Button>("SmokeButton");
        banditOperatorButton = mainUIElement.Q<Button>("BanditButton");
        USSRButton = mainUIElement.Q<Button>("USSRButton");
        GSG9Button = mainUIElement.Q<Button>("GSG9Button");
        smokeOperatorPic.visible = false;
        
        //IMAGE REFS
        smokeMainPowerPic = mainUIElement.Q<VisualElement>("MainPowerSmoke");
        smokeMainWeaponPic = mainUIElement.Q<VisualElement>("MainWeaponSmoke");
        smokeSecondaryWeaponPic = mainUIElement.Q<VisualElement>("SecondaryWeaponSmoke");
        smokeMainPowerPic.visible = false;
        smokeMainWeaponPic.visible = false;
        smokeSecondaryWeaponPic.visible = false;
        smokeStats.visible = false;

        //BUTTON CALLBACK
        newSettingsButton.RegisterCallback<ClickEvent>(ev => SettingsButton());
        smokeOperatorButton.RegisterCallback<ClickEvent>(ev => OperatorChange(0));
        banditOperatorButton.RegisterCallback<ClickEvent>(ev => OperatorChange(1));
        USSRButton.RegisterCallback<ClickEvent>(ev => ChangeGroup(0));
        GSG9Button.RegisterCallback<ClickEvent>(ev => ChangeGroup(1));
        
        uiDocumentLabelMoney.text = amountOfMoneyHave + " $";
    }
}
