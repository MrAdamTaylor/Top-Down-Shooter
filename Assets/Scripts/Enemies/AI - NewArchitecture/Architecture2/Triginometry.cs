using UnityEngine;

public static class Triginometry
{
    public static bool InBetween(float thetaRight, float thetaDif,float thetaLeft)
    {
        if(Mathf.Abs(thetaRight- thetaLeft)<= Mathf.PI)
        {
            if(thetaRight<=thetaDif && thetaDif<= thetaLeft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(thetaLeft <0 && thetaRight> 0)
            {
                thetaLeft += 2 * Mathf.PI;
                if (thetaDif < 0)
                {
                    thetaDif += 2 * Mathf.PI;
                }
                if (thetaRight <= thetaDif && thetaDif <= thetaLeft)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (thetaLeft > 0 && thetaRight< 0)
            {
                thetaRight += 2 * Mathf.PI;
                if (thetaDif < 0)
                {
                    thetaDif += 2 * Mathf.PI;
                }
                if (thetaLeft <= thetaDif && thetaDif <= thetaRight )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
}