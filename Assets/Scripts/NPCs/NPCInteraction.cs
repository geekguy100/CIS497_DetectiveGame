/*****************************************************************************
// File Name :         NPCInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    [Tooltip("The name of the character. Must match a name exactly as written in the JSON file.")]
    [SerializeField] private string characterName;

    private Character character;

    // The character the player is currently talking to.
    public static Character activeCharacter;

    private void Start()
    {
        character = DialogueHandler.GetCharacter(characterName);
        if (character == null)
        {
            Debug.LogWarning(gameObject.name + ": Could not get character named " + characterName);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Enables dialogue interaction with the NPC.
    /// </summary>
    /// <param name="interactor">The IInteractor that interacted with the NPC.</param>
    public void Interact(IInteractor interactor)
    {
        // TODO: We'll need a new JSON field for IntroDialogue. For now, I'm hard coding it.
        activeCharacter = character;
        bool questionPanelActive = UIManager.Instance.ToggleQuestionPanel();

        if (questionPanelActive)
            UIManager.Instance.DisplayDialoguePanel(character.Name, character.Intro);
        else
            UIManager.Instance.HideDialoguePanel();
    }

    public void OnAssigned(IInteractor interactor)
    {
        
    }

    public void OnUnassigned(IInteractor interactor)
    {
        
    }
}