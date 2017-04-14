using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;
using System;

public class Cliente : MonoBehaviour {

	public GameObject chatContainer;
	public GameObject messagePrefab; 
	public string clientName;

	private bool socketRead;
	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter write;
	private StreamReader reader;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (socketRead) {
			if (stream.DataAvailable) {
				string data = reader.ReadLine ();
				if (data != null) {
					OnIncomingData (data);
				}
			}
		}
		
	}

	private void OnIncomingData(string data){
	
		if (data == "%NAME") {

			Send("&NAME|" + clientName);
			return;
		}

		GameObject go = Instantiate (messagePrefab, chatContainer.transform) as GameObject;
		go.GetComponentInChildren<Text> ().text = data;

	}

	private void Send(string data){
	
		if (!socketRead)
			return;

		write.WriteLine (data);
		write.Flush ();

	}

	public void OnSendButton(){
		string message = GameObject.Find ("SendInput").GetComponent<InputField> ().text;
		Send (message);
	}


	public void ConnectToServer()
	{
		//If already connected, ignore this function
		if (socketRead)
			return;

		//Default host / port values
		string host = "127.0.0.1";
		int port = 6321;

		string h;
		int p;
		h = GameObject.Find ("HostInput").GetComponent<InputField> ().text;
		if (h != "")
			host = h;
		int.TryParse (GameObject.Find ("PortInput").GetComponent<InputField> ().text, out p);
		if (p != 0)
			port = p;

		//Create the socket

		try {
			socket = new TcpClient (host, port);
			stream = socket.GetStream ();
			write = new StreamWriter (stream);
			reader = new StreamReader (stream);
			socketRead = true;
		} catch (Exception e) {
			Debug.Log ("Socket Error: " + e.Message);
		}
	}

	private void CloseSocket(){
		if (!socketRead)
			return;

		write.Close ();
		reader.Close ();
		socket.Close ();
		socketRead = false;
	}

	private void OnApplicationQuit(){
		CloseSocket ();
	}

	private void OnDisable(){
		CloseSocket ();
	}
}
