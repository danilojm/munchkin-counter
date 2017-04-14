using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botoes : MonoBehaviour {

	[SerializeField]
	private Text placar = null;

	[SerializeField]
	private Text total = null;

	private int counterNivel = 0;
	private string totalCounter;

	public void btnMenos(){
		counterNivel = counterNivel - 1;
		placar.text = counterNivel.ToString();


		totalCounter = total.text;
		int totalC = int.Parse (totalCounter);
		totalC = totalC - 1;
		total.text = totalC.ToString();
	}

	public void btnMais(){
		counterNivel = counterNivel + 1;
		placar.text = counterNivel.ToString();

		totalCounter = total.text;
		int totalC = int.Parse (totalCounter);
		totalC = totalC + 1;
		total.text = totalC.ToString();
	}
}
