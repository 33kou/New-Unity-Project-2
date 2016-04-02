#pragma strict

var x : int =5;

function Start () {
Debug.Log("DAFADF-->"+x);
}

function Update () {

var x :float=Input.GetAxis("Horizontal");
//transform.position.z +=0.1;
//transform.position += Vector3(0,0,0,1);
//transform.Translate(0,0,0.1);
transform.Translate(x*0.2,0,0);
if(Input.GetButtonUp("Jump")){
  Debug.Log("jumped");
  }


}


function OnCollisionEnter(obj : Collision) {
if(obj.gameObject.name == "Rightwall"){
Debug.Log("hitright");
}

}