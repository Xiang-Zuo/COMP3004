using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : Card
{
    private Ability ability;

    public TestCard(string aName, Kind aKind, Ability anAbility)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.ability = anAbility;
    }
    public Ability getAbility()
    {
        return this.ability;
    }
}
