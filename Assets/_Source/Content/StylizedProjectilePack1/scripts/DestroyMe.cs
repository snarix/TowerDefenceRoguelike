using UnityEngine;

public class DestroyMe : MonoBehaviour{

    private float _timer;
    public float deathtimer = 10;
    
	private void Update ()
    {
        _timer += Time.deltaTime;

        if(_timer >= deathtimer)
            Destroy(gameObject);
	}
}
