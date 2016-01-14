using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System;

public class Listener : MonoBehaviour {

	string returnData;
	public int listenerPort;
	public string listenerIP;
	private TcpClient listenerClient;
	private NetworkStream listenerStream;
	private StreamReader srReceiver;
	private Socket listenerSocket;
	private bool connected = false;
	private bool followObject = false;
	public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		
		Console.Write ("test");
		this.listenerClient = new TcpClient();	
		IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.listenerIP), this.listenerPort);
		try {
			this.listenerClient.Connect(endPoint);
			this.listenerStream = listenerClient.GetStream();
			this.connected = true;
		} catch (SocketException e) {
			Debug.Log (e);
		}

	}


	private void processMessage(string p)
	{
		string lastSeven = p.Substring((p.Length-7),7);

		if (lastSeven.Equals("created")) {
			var tempObject = GameObject.Find(correctObject(p.Substring(0,(p.Length-7))));
			tempObject.SetActive(true);
			//var copyObject = gameObject.GetComponent<AVIECursor>().lastHit;
			//tempObject.transform.rotation = copyObject.transform.rotation;
			//tempObject.transform.position = copyObject.transform.position;
		} else if (lastSeven.Equals("deleted")) {
			//var tempObject = gameObject.GetComponent<AVIECursor>().lastHit;
			//tempObject.SetActive(false);
		} else if (lastSeven.Equals("dropped")) {
			followObject = false;
		} else if (p.Substring((p.Length-8),8).Equals("selected")) {
			followObject = true; 
		} else if (p.Substring(0,6).Equals("Colour")){
			//var tempObject = gameObject.GetComponent<AVIECursor>().lastHit;
			var tempObject = GameObject.Find("sofa_2");
			tempObject.transform.position = new Vector3 (0,0,0);
			string colour = correctColour(p.Substring(14,p.Length-14));
			if (colour.Equals("blue")) {
			//	tempObject.renderer.material.color = Color.blue;
			} else if (colour.Equals("red")) {
			//	tempObject.renderer.material.color = Color.red;
			} else if (colour.Equals("white")) {
			//	tempObject.renderer.material.color = Color.white;
			} else if (colour.Equals("black")) {
			//	tempObject.renderer.material.color = Color.black;
			} else if (colour.Equals("green")) {
			//	tempObject.renderer.material.color = Color.green;
			} else if (colour.Equals("yellow")) {
			//	tempObject.renderer.material.color = Color.yellow;
			}
		}
	}

	private String correctColour(String word) {
		string correctCol = "";
		if ((word.Equals("blue")) || (word.Equals("bloom"))) {
			correctCol = "blue";
		} else if ((word.Equals("red")) || (word.Equals("read")) || (word.Equals("ran"))) {
			correctCol = "red";
		} else if ((word.Equals("white")) || (word.Equals("wait")) || (word.Equals("what")) 
		           || (word.Equals("why"))) {
			correctCol = "white";
		} else if (word.Equals("black")) {
			correctCol = "black";
		} else if ((word.Equals("green")) || (word.Equals("queen"))) {
			correctCol = "green";
		} else if ((word.Equals("yellow")) || (word.Equals("alaw")) || (word.Equals("you"))) {
			correctCol = "yellow";
		}
		return correctCol;
	}

	private String correctObject(string word){
		string correctWord = "";
		if ((word.Equals ("couch")) || (word.Equals ("carwhich")) || (word.Equals ("culture")) 
				|| (word.Equals ("catch")) || (word.Equals ("out")) || (word.Equals ("sofa")) || (word.Equals ("self"))
				|| (word.Equals ("andsaw"))) {
				correctWord = "sofa_2";
		} else if ((word.Equals("table")) || (word.Equals("cable")) || (word.Equals("people")) 
		           || (word.Equals("Timor"))) {
				correctWord = "coffee_table_3";
		} else if ((word.Equals("chair")) || (word.Equals("share"))) {
				correctWord = "chair_4";
		} else if ((word.Equals("lamp")) || (word.Equals("land")) || (word.Equals("lamb")) 
		           || (word.Equals("lab"))) {
				correctWord = "torchere_1";
		}


		return correctWord;
	}
	// Update is called once per frame
	void Update () {
		if (!this.connected) return;
		Console.WriteLine ("test1");
		if (this.listenerStream.CanRead) {
			Console.WriteLine ("test2");
			int bytesRead;
			int i;
			string con;
			byte[] buffer = new byte[4096];
			byte[] msg = new byte[4];

						//listenerStream.Write(msg, 0, msg.Length);
						//listenerStream.Flush();
			while (this.listenerStream.DataAvailable) {
				bytesRead = listenerStream.Read (buffer, 0, buffer.Length);
				if (bytesRead > 1) {
					byte[] tmp = new byte[bytesRead];
					for (i = 0; i < bytesRead; i++) {
							tmp [i] = buffer [i];
					}
					String rcv = System.Text.Encoding.ASCII.GetString (tmp);
					rcv = rcv.Trim ();
					Console.WriteLine ("test");
					Console.WriteLine (rcv);
					processMessage (rcv);
				}
			}
		}
	}
}
