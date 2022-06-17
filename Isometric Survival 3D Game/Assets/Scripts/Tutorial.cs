using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Tutorial : MonoBehaviour
{
    bool isTutorial;
    int stage; //nr akcji oczekiwanej
    //0 - odkrycie bloku
    //1 - zebranie czêœci
    //2 - pójœcie spaæ
    //3 - odkrycie pierwszego przejœcia
    //4 - zebranie jedzenia
    //5 - nakarmienie postaci
    //6 - odkrycie drugiego przejœcia
    //7 - przeniesienie jagód do drugiego ekwipunku
    //8 - nakarmienie drugiej postaci
    //9 - zebranie 5 drewna
    //10 - rozbicie obozu
    //11 - upieczenie ciasta
    //12 - spanie w obozie
    //13 - odkrycie trzeciego przejœcia
    //14 - pierwszy atak na paj¹ka
    //15 - drugi atak na paj¹ka
    //16 - odkrycie czwartego przejœcia
    //17 - naprawienie statku
    bool dialogueShown;

    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject char1Image;
    [SerializeField] GameObject char2Image;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject sleepButton;
    [SerializeField] GameObject girl;
    [SerializeField] GameObject spaceship;
    [SerializeField] GameObject skillSelectionUI;
    Map map;

    string[] dialogues = new string[] { "Where am I? The spaceship should be nearby [Press ENTER]",
                                        "[Click on the grey block to unlock new terrain]",
                                        "A metal part? It may be useful",
                                        "[Click twice to pick up the metal part]",
                                        "I’m kind of tired",
                                        "[The green bar is your energy. Click Sleep to end the day.]",
                                        "I hope that’s edible",
                                        "[Click twice to pick up the berries]",
                                        "[The red bar is your health. Drag the berry from your inventory onto the character image to eat]",
                                        "So sour! Let's take them all and go further",
                                        "Oh! There you are! Are you hurt?",
                                        "No… Do you have anything to eat?",
                                        "[Press space to change character. Come closer and drag the food between their inventories]",
                                        "[Drag the berry from your inventory onto the character to eat]",
                                        "Thanks. It’s not very filling. It’d be better to cook something",
                                        "Should we make a camp?",
                                        "[Get five wood]",
                                        "[Right-click on an empty spot to build a camp]",
                                        "Let’s cook something yummy",
                                        "[Click on the fireplace to make a blueberry pie]",
                                        "This shoud be more filling. We can leave it in the camp or take with us.",
                                        "Sleeping in the camp should make us well rested.",
                                        "[Go into the tent and sleep]",
                                        "Let’s go further",
                                        "Oh! What a big bush!",
                                        "Does it have eyes?",
                                        "[Click on the bush to pick up the berries]",
                                        "I bites!",
                                        "Let's try again",
                                        "It worked!",
                                        "Let’s fix the ship",
                                        "[Click on the spaceship to use the metal parts to fix it]",
                                        "It’s very damaged. We need more metal parts! And more food!",
                                        "Let's divide the responsibilities. What do you do best?"};
    int[] dialogueCharacter = {1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 2, 0, 0, 2, 1, 0, 0, 
                               2, 0, 2, 1, 0, 2, 2, 1, 0, 1, 2, 2, 1, 0, 1, 2};
    int[] dialogueStages = {0, 0, 1, 1, 2, 2, 4, 4, 5, 6, 7, 7, 7, 8, 9, 9, 9, 10, 11,
                            11, 12, 12, 12, 13, 14, 14, 14, 15, 15, 16, 17, 17, 18, 18, 19}; // w oczekiwaniu na któr¹ akcjê jest dialog 
    int dialogueIndex = 0;
    Queue <int> dialogueQueue = new Queue<int>();
    bool itsDamagedMessageShown = false;

    // Start is called before the first frame update
    void Start()
    {
        SkillSelection.SetStartingSkills();
        map = FindObjectOfType<Map>();
        isTutorial = true;
        stage = 0;
        dialogueShown = false;
        dialogueQueue.Enqueue(0);
        dialogueQueue.Enqueue(1);
        dialogueIndex = 2;
        //girl.SetActive(false); 
        spaceship.SetActive(false);
    }

    void showDialogue(int nr)
    {
        if (nr == 33) itsDamagedMessageShown = true;
        int character = dialogueCharacter[nr];
        dialogueShown = true;
        dialoguePanel.SetActive(true);
        if (character == 1)
        {
            char1Image.SetActive(true);
            text.horizontalAlignment = HorizontalAlignmentOptions.Left;
        }
        if (character == 2)
        {
            char2Image.SetActive(true);
            text.horizontalAlignment = HorizontalAlignmentOptions.Right;
        }
        if (character == 0)
        {
            text.horizontalAlignment = HorizontalAlignmentOptions.Center;
        }
        text.SetText(dialogues[nr]);
        dialogueShown = true;
    }

    void hideDialogue()
    {
        if (dialogueShown)
        {
            dialoguePanel.SetActive(false);
            char1Image.SetActive(false);
            char2Image.SetActive(false);
            dialogueShown=false;
            if (itsDamagedMessageShown)
            {
                skillSelectionUI.SetActive(true);
                SkillSelection.ResetSkills();
                isTutorial = false;
                map.EndTutorial();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!dialogueShown && dialogueQueue.Count != 0)
        {
            showDialogue(dialogueQueue.Dequeue());
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            hideDialogue();
        }
    }

    public bool IsTutorial()
    {
        return isTutorial;
    }

    public int GetStage()
    {
        return stage;
    }

    public bool IsDialogueShown()
    {
        return dialogueShown;
    }
    
    //wykonanie danej akcji np. podniesienie czêœci, przechodzimy do nastêpnego etapu tutoriala
    public void TutorialAction(int nr)
    {
        if(stage == nr)
        {
            Debug.Log("stage: " + nr);
            stage++;
            hideDialogue();
            while (dialogueStages[dialogueIndex] == stage)
            {
                dialogueQueue.Enqueue(dialogueIndex);
                dialogueIndex++;
            }
            switch (stage)
            {
                case 2:
                    sleepButton.SetActive(true);
                    break;
                case 3:
                    map.UnlockPassage(0);
                    break;
                case 6:
                    map.UnlockPassage(1);
                    break;
                case 7:
                    girl.SetActive(true);
                    break;
                case 13:
                    map.UnlockPassage(2);
                    break;
                case 16:
                    map.UnlockPassage(3);
                    break;
                case 17:
                    spaceship.SetActive(true);
                    break;
            }
        }
    }
}
