using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    public float delay;

    async void Start()
    {
        // delay for seconds
        await Task.Delay((int)(delay * 1000f));

        // disable object
        gameObject.SetActive(false);
    }
}
