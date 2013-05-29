var Sound : AudioClip;

/*function Start() {
	AudioSource.PlayClipAtPoint(Sound, transform.position);
}*/

function OnTriggerEnter(c:Collider) {
	AudioSource.PlayClipAtPoint(Sound, transform.position);
	//yield WaitForSeconds(2);
	//audio.PlayOneShot(Sound);
}
