using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSound : MonoBehaviour
{
    public void Play(string name) => AudioManager.instance.play(name);
}
