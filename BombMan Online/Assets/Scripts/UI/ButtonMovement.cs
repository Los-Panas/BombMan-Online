using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    // Start is called before the first frame update
    bool change = false;
    bool change2 = true;
    float time = 0.0f;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    Vector3 start = new Vector3(0.5f, 0.5f, 1.0f);
    Vector3 end = new Vector3(1.5f, 1.5f, 1.0f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (change2)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;


            if (!change)
                this.transform.localScale = Vector3.Lerp(start, end, interpolationRatio);

            if (change)
                this.transform.localScale = Vector3.Lerp(end, start, interpolationRatio);

            if (this.transform.localScale.x == end.x)
                change = true;

            if (this.transform.localScale.x == start.x)
                change = false;


            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);// reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
        }

        if (change2)
            change2 = false;
        else
            change2 = true;
        
    }
}
