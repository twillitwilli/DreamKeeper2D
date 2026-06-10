using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneUpdates : MonoBehaviour
{
    public GameobjectCollection chests;

    // Start is called before the first frame update
    void Start()
    {
        UpdateChests();
    }

    private void UpdateChests()
    {
        for (int i = 0; i < chests.objects.Length;  i++)
        {

        }
    }
}
