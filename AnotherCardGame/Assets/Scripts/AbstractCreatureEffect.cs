using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCreatureEffect
{
    public Sprite IconSprite => Resources.Load<Sprite>(ToString());

    public new abstract string ToString();
}
