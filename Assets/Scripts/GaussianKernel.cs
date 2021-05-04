//Source: http://haishibai.blogspot.com/2009/09/image-processing-c-tutorial-4-gaussian.html
// Generates Gaussian kernel which is passed to the Outline shader for a blur effect

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GaussianKernel
{
    public static float[] Calculate(double sigma, int size)
    {
        float[] ret = new float[size];
        double sum = 0;
        int half = size / 2;
        for (int i = 0; i < size; i++)
        {
            double val = -(i - half) * (i - half) / (2 * sigma * sigma);
            float floatVal = (float) val;
            ret[i] = (float)(1 / (Mathf.Sqrt(2 * Mathf.PI) * sigma) * Mathf.Exp(floatVal));
            sum += ret[i];
        }
        return ret;
    }
}
