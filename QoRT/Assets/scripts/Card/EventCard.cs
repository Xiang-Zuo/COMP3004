﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCard : Card {
    //private Ability ability;
    public EventCard(string aName, Kind aKind)
    {
        this.setName(aName);
        this.setKind(aKind);      
    }
}

