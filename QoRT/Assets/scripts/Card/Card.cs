using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    private string name;
    private Kind kind;
    
    public string getName()
    {
        return this.name;
    }

    public Kind getKind()
    {
        return this.kind;
    }

    public void setName(string aName)
    {
        this.name = aName;
    }

    public void setKind(Kind aKind)
    {
        this.kind = aKind;
    }
}
