using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : Card
{
    private int atk;
    
	public WeaponCard(string aName, Kind aKind, int anAtk)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.atk = anAtk;
    }

    public int getAtk()
    {
        return this.atk;
    }

	public void setAtk(int anAtk){
		this.atk = anAtk;
	}
	
}