using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefactoringMessage : MonoBehaviour
{
    [Header("This is objects don't trash. We do refactoring in this code")]
    public string AnyWords;

    
    public void DoExplain()
    {
        Debug.Log("Here is the improved AI architecture. ");
        Debug.Log("It is not finished. In it I am working on the fact that NPCs do not collide with each other and at the same time the gameplay is entertaining. ");
        Debug.Log("There are some shortcomings, but I will fix it later");
    }
}
