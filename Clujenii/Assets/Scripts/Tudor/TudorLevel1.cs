using UnityEngine;
using System.Collections;

public class TudorLevel1 : Level
{
    private Dummy dummy;
    new void Awake()
    {
        var dummyObject = GameObject.Find("Dummy");
        dummy = dummyObject.GetComponent<Dummy>();
        base.Awake();
    }

    protected override bool LevelConditionsMet()
    {
        return dummy.IsCured();
    }
}
