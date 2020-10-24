using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMath : MonoBehaviour
{
    public float speed;
    public Object path;
    public Object end;
    public GameObject ballObject;
    public Rigidbody2D ball;
    public Rigidbody2D opponent;
    public BoxCollider2D topBoundary;
    public BoxCollider2D bottomBoundary;
    public float maxDiffInPos;
    private float topBarrier;
    private float bottomBarrier;
    private float targetY;
    private float vertDirection;
    private float timeToPlayer;
    private bool prevState;

    // Start is called before the first frame update
    void Start()
    {
        topBarrier = topBoundary.offset.y - topBoundary.size.y;
        bottomBarrier = bottomBoundary.offset.y + bottomBoundary.size.y;
        prevState = false;
    }

    // Update is called once per frame
    void Update()
    {
        Object.Instantiate(path, ballObject.transform.position, ballObject.transform.rotation);
        if(towardPlayer()){
            timeToPlayer = Mathf.Abs((transform.position.x - transform.localScale.x/2 - (ballObject.transform.position.x + ballObject.transform.localScale.x/2)) / ball.velocity.x);
            if (prevState != towardPlayer()) {
                targetY = timeToPlayer * ball.velocity.y + ballObject.transform.position.y;
                prevState = towardPlayer();
            }
            if(targetY > topBarrier - ballObject.transform.localScale.y/2){
                targetY = topBarrier - ballObject.transform.localScale.y/2 - Mathf.Abs(targetY - (topBarrier - ballObject.transform.localScale.y/2));
            } else if (targetY < bottomBarrier + ballObject.transform.localScale.y/2){
                targetY = bottomBarrier + ballObject.transform.localScale.y/2 + Mathf.Abs(targetY - (bottomBarrier + ballObject.transform.localScale.y/2));
            }
            Object.Instantiate(end, new Vector3(transform.position.x, targetY, -.2f), ballObject.transform.rotation);
        }

        if (prevState != towardPlayer()) {
            prevState = towardPlayer();
        }
        if(!prevState){
            targetY = 0;
        }

        if (Mathf.Abs (targetY - transform.position.y) < maxDiffInPos) { //little bit of a space so it doesn't shake around as much
            transform.position = transform.position;
        } else if (targetY < transform.position.y) {
            transform.position = new Vector2 (transform.position.x, transform.position.y + down());
        } else if (targetY > transform.position.y) {
            transform.position = new Vector2 (transform.position.x, transform.position.y + up());
        }

    }

    void FixedUpdate(){
        /*if(towardPlayer()){
            if (prevState != towardPlayer()) {
                targetY = timeToPlayer * ball.velocity.y + ballObject.transform.position.y;
                prevState = towardPlayer();
            }
            if(targetY > topBarrier - ballObject.transform.localScale.y){
                targetY = topBarrier - ballObject.transform.localScale.y - Mathf.Abs(targetY - (topBarrier - ballObject.transform.localScale.y));
            } else if (targetY < bottomBarrier + ballObject.transform.localScale.y){
                targetY = bottomBarrier + ballObject.transform.localScale.y + Mathf.Abs(targetY - (bottomBarrier + ballObject.transform.localScale.y));
            }
        }
        if (prevState != towardPlayer()) {
            prevState = towardPlayer();
        }
        if(!prevState){
            targetY = 0;
        }*/
    }

    bool towardPlayer(){
        if(opponent.transform.position.x < transform.position.x && ball.velocity.x > 0){ //if the opponent is to the left, and the ball is moving to the right, its true
            return true;
        } else if(opponent.transform.position.x > transform.position.x && ball.velocity.x < 0){ //if the opponent is to the right and the ball is moving left, its true
            return true;
        } else{
            return false;
        }
    }

    bool hitWall(){
        //print(ball.velocity.y + "\t" + vertDirection);
        if(vertDirection == 0){
            vertDirection = ball.velocity.y;
            return false;
        } else if(ball.velocity.y > 0 && vertDirection < 0){
            vertDirection = ball.velocity.y;
            return true;
        } else if(ball.velocity.y < 0 && vertDirection > 0){
            vertDirection = ball.velocity.y;
            return true;
        } else {
            return false;
        }
    }

    float up () {
        return speed * Time.deltaTime;
    }

    float down () {
        return -1 * speed * Time.deltaTime;
    }
}
