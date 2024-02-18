using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using UnityEngine.Windows.WebCam;
using Microsoft.MixedReality.Toolkit;

public class CrackMeasurement : MonoBehaviour
{
    public string text;
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    public float DTC;
    public float threshold1;
    public float threshold2;
    public float MaxThicknessIn_mm;
    public float Length_mm;
    public float AverageThicknessIn_mm;
    public float Area;
    public int val;
    public float median = 20.0f;
    public Material OutputMaterial;
    public Texture2D StaticTexture;
    int n1, n2, m1, m2;
    public string EvaluationResult;
    CameraParameters cameraParameters = new CameraParameters();
    Renderer rndr;
    Texture texture = null;
    WebCamTexture webcamTexture;
    public float p, l;
    void Start()
    {
        rndr = GetComponent<Renderer>();
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject)
        {
            photoCaptureObject = captureObject;
            cameraParameters.hologramOpacity = 0.5f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;
            print(cameraResolution.width);
            print(cameraResolution.height);

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result)
            {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        });
    }
    private void Update()
    {
        // Take a picture
        photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
    }
    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into our target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);
        // Copy the raw image data into our target texture
    RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, float.PositiveInfinity, LayerMask.GetMask("Spatial Awareness")))
        {
            float distanceToCrack = Vector3.Distance(hit.point, Camera.main.transform.position);
            DTC = distanceToCrack;
        }
        if (DTC > 1.5)
        {
            threshold1 = 7000;
            threshold2 = 5000;
        }
        else if (DTC < 0.9 & DTC > 0.5)
        {
            threshold1 = 23000;
            threshold2 = 15000;
        }
        else if (DTC < 0.5)
        {
            threshold1 = 27500;
            threshold2 = 17500;
        }
        else
        {
            threshold1 = 13500;
            threshold2 = 9500;
        }
        // duplicate the original texture and assign to the material
        // tint each mip level
        var cols = targetTexture.GetPixels32(0);
        float[] values = new float[cols.Length];
        float[] Gx = new float[cols.Length];
        float[] Gy = new float[cols.Length];
        float[] G = new float[cols.Length];
        float[] GG = new float[cols.Length];
        float[] teta = new float[cols.Length];
        n1 = targetTexture.height / 10 * ((Mathf.FloorToInt((10 - p) / 2)))+150;
        n2 = targetTexture.height / 10 * (10 - (Mathf.FloorToInt((10 - p) / 2)))-150;
        m1 = targetTexture.width / 10 * ((Mathf.FloorToInt((10 - l) / 2)))+50;
        m2 = targetTexture.width / 10 * (10 - (Mathf.FloorToInt((10 - l) / 2)))-50;
        float[] k1 = new float[n2 - n1 + 2];
        float[] k2 = new float[n2 - n1+2];
        float[] k7 = new float[n2 - n1 + 2];
        float[] k9 = new float[n2 - n1 + 2];
        int[] s1 = new int[n2 - n1 + 2];
        int[] s2 = new int[n2 - n1 + 2];
        int[] s4 = new int[n2 - n1 + 2];
        float sum = 0, sum1 = 0, sum2 = 0, sum3=0, sum4=0, max = 0; 
        for (int i = 0; i < cols.Length; ++i)
        {
            values[i] = 0.587f * cols[i].g + 0.299f * cols[i].r + 0.114f * cols[i].b;
        }
        for (int i = n1; i < n2; ++i)
        {
            for (int j = m1; j < m2; j += 1)
            {
                float[] val = new float[8] { values[i * targetTexture.width + j - 1], values[i * targetTexture.width + j + 1], 
                    values[i * targetTexture.width + j - 1*targetTexture.width], values[i * targetTexture.width + j + 1*targetTexture.width], 
                    values[i * targetTexture.width + j - 1*targetTexture.width + 1], values[i * targetTexture.width + j - 1*targetTexture.width-1], 
                    values[i * targetTexture.width + j + 1*targetTexture.width - 1], values[i * targetTexture.width + j + 1*targetTexture.width + 1] };
                Array.Sort(val);
                values[i * targetTexture.width + j] = (val[3] + val[4]) / 2;
            }
        }

        for (int i = n1; i < n2; ++i)
        {
            for (int j = m1; j < m2; j += 1)
            {
                Gx[i * targetTexture.width + j] = -1 * values[i * targetTexture.width + j - targetTexture.width - 1] + values[i * targetTexture.width + j - targetTexture.width + 1] - 2 * values[i * targetTexture.width + j - 1] + 2 * values[i * targetTexture.width + j + 1] - 1 * values[i * targetTexture.width + j + targetTexture.width - 1] + values[i * targetTexture.width + j + targetTexture.width + 1];
                Gy[i * targetTexture.width + j] = -1 * values[i * targetTexture.width + j - targetTexture.width - 1] - 1 * values[i * targetTexture.width + j - targetTexture.width + 1] - 2 * values[i * targetTexture.width + j - targetTexture.width] + 2 * values[i * targetTexture.width + j + targetTexture.width] + 1 * values[i * targetTexture.width + j + targetTexture.width - 1] + values[i * targetTexture.width + j + targetTexture.width + 1];
                G[i * targetTexture.width + j] = Mathf.Pow(Gx[i * targetTexture.width + j], 2) + Mathf.Pow(Gy[i * targetTexture.width + j], 2);
                GG[i * targetTexture.width + j] = Mathf.Sqrt(Mathf.Pow(Gx[i * targetTexture.width + j], 2) + Mathf.Pow(Gy[i * targetTexture.width + j], 2));
                teta[i * targetTexture.width + j] = Mathf.Atan(Gy[i * targetTexture.width + j] / Gx[i * targetTexture.width + j]);
            }
        }

        // Initialize the measurement algorithm for horizontal direction using the Canny parameters.
        for (int i = n1; i < n2; ++i)
        {
            // Set the starting and ending columns to blue to mark the measurement area.
            cols[i * targetTexture.width + m1] = Color.blue;
            cols[i * targetTexture.width + m2] = Color.blue;
            // Iterate through each column between m1 and m2.
            for (int j = m1; j < m2; j += 1)
            {
                // Check if the gradient at the current position is greater than the first threshold.
                if (G[i * targetTexture.width + j] > threshold1)
                {
                    // Check neighboring pixels against the second threshold to confirm edge presence.
                    if ((G[i * targetTexture.width + j + 1] > threshold2 & G[i * targetTexture.width + j - 1] > threshold2) |
                        (G[(i + 1) * targetTexture.width + j] > threshold2 & G[(i - 1) * targetTexture.width + j] > threshold2) |
                        (G[(i + 1) * targetTexture.width + j - 1] > threshold2 & G[(i - 1) * targetTexture.width + j + 1] > threshold2) |
                        (G[(i + 1) * targetTexture.width + j + 1] > threshold2 & G[(i - 1) * targetTexture.width + j - 1] > threshold2))
                    {
                        // Mark the start of an edge.
                        s1[i - n1] = j;
                        ++j; // Move to the next column.
                             // Continue moving right until the gradient is less than the first threshold, indicating the end of the edge.
                        while (G[i * targetTexture.width + j] > threshold1)
                        {
                            ++j;
                        }
                        // Mark the end of the edge.
                        s2[i - n1] = j;
                        // Skip columns until finding another edge or exceeding the maximum gap.
                        while (G[i * targetTexture.width + j] < threshold1 & (j - s2[i - n1]) < 25)
                        {
                            ++j;
                        }
                        // If another edge is found within the gap threshold, mark it.
                        if (j - s2[i - n1] < 25 & j < m2)
                        {
                            while (G[i * targetTexture.width + j] > threshold1)
                            {
                                ++j;
                            }
                            // Record the final edge position and calculate the cosine of the angle at the start and end of the edge.
                            s4[i - n1] = j - 1;
                            k7[i - n1] = Mathf.Cos(teta[i * targetTexture.width + s1[i - n1]]);
                            k9[i - n1] = Mathf.Cos(teta[i * targetTexture.width + j - 1]);
                            // Skip additional columns to ensure separation from the next edge.
                            while (G[i * targetTexture.width + j] < threshold1 & (j - s2[i - n1]) < 25)
                            {
                                ++j;
                            }
                            // Repeat the coloring and calculation if another valid edge is found.
                            if (j - s2[i - n1] < 25)
                            {
                                while (G[i * targetTexture.width + j] > threshold1)
                                {
                                    ++j;
                                }
                                s4[i - n1] = j - 1;
                                k7[i - n1] = Mathf.Cos(teta[i * targetTexture.width + s1[i - n1]]);
                                k9[i - n1] = Mathf.Cos(teta[i * targetTexture.width + j - 1]);
                            }
                            // Calculate the length and cosine values for the detected edges.
                            k2[i - n1] = (s4[i - n1] - s2[i - n1]);
                            // Color the measured crack in red.
                            for (int d = s2[i - n1]; d <= s4[i - n1]; ++d)
                            {
                                cols[i * targetTexture.width + d] = Color.red;
                            }
                        }
                    }
                }
            }
        }

        int n = 0; // Count of measured segments.
        float ll = 0.0f; // Variable for length calculation.
        float area = 0; // Area covered by the edges.
        int z = 0; // Unused variable in the provided code snippet.
                   // Calculate the average thickness, maximum thickness, and total length of the detected edges.
        for (int i = 0; i < n2 - n1; i++)
        {
            // Filter out edge segments based on their length.
            if (k2[i] < 29 & k2[i] > 2)
            {
                n++; // Increment the count of valid edges.
                     // Sum the lengths and cosine values of the edges.
                sum += k2[i] * (k7[i] + k9[i]) / 2;
                area += k2[i]; // Total area covered by the edges.
                ll += 1.0f / ((k7[i] + k9[i]) / 2); // Length calculation based on the cosine of the angles.
                // Update the maximum thickness if the current segment is thicker.
                if (k2[i] * (k7[i] + k9[i]) / 2 > max)
                {
                    max = k2[i] * (k7[i] + k9[i]) / 2;
                }
            }
        }
        // Calculate the average thickness in millimeters.
        // The distance from camera and Raycast hit hardware is ~0.07m and 0.4 is the calibration coeficient. 
        AverageThicknessIn_mm = ((sum / n) * 0.4f) * Mathf.Pow((DTC - 0.07f), 1f); //Mathf.Pow((DTC - 0.07f), 0.83f); works better in practice for concrete cracks
        // Calculate the maximum thickness in millimeters.
        MaxThicknessIn_mm = (max * 0.4f) * Mathf.Pow((DTC - 0.07f), 1f); //Mathf.Pow((DTC - 0.07f), 0.83f); works better in practice for concrete cracks
        // Calculate the length in millimeters.
        Length_mm = (n * 100 / (n2 - n1));
        // Set the area to DTC, though it seems to be a placeholder or an unused variable in this context.
        Area = (DTC);
        // Apply the colored pixels to the target texture and update the material's texture.
        targetTexture.SetPixels32(cols);
        targetTexture.Apply();
        rndr.material.mainTexture = targetTexture;

    }
}