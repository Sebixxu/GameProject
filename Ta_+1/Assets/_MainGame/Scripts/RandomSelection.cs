using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public struct RandomSelection2
//{
//    private Monster _monster;
//    public float probability;

//    public RandomSelection2(Monster monster, float probability)
//    {
//        this._monster = monster;
//        this.probability = probability;
//    }

//    //public int GetValue() { return Random.Range(minValue, maxValue + 1); }
//}

//public static class RandomSelection
//{
//    public static int GetRandomValue(params RandomSelection2[] selections)
//    {
//        float rand = Random.value;
//        float currentProb = 0;
//        foreach (var selection in selections)
//        {
//            currentProb += selection.probability;
//            if (rand <= currentProb)
//                return selection.GetValue();
//        }

//        //will happen if the input's probabilities sums to less than 1
//        //throw error here if that's appropriate
//        return -1;
//    }
//}
