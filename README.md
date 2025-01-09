CM Project
Project Description
This Mixed Reality app integrates a crack characterization algorithm developed for AR headsets. The algorithm allows the conversion from pixel to engineering scale, which is essential in image-based crack characterization. The approach considers Camera-to-Crack-Distance (CCD) and camera obliquity angles for accurate perspective correction.
The application entails the following processes:

Conversion of pixel information to the AR platform.

Edge extraction using the Canny algorithm.
Measurement of crack at a pixel level horizontally. Measurement of CCD and camera angle by utilizing the orientation capabilities of AR. Computation of engineering-scale dimensions, perspective correction.
A scaler equation for perspective correction is indispensable for streamlining the algorithm in real-time applications. However, the implementation here is not complete. The equation for perspective transformation has not been considered in this version, and hence its accuracy is low for non-vertical views of the camera. The built file has also not been provided, and the user needs to build the application using the source code provided.
Development Stage

This project is currently under development and suffers from some limitations:
The perspective transformation formula has not been implemented, so it works best for vertical camera views.

It doesn't contain automatic ROI detection; a region of interest is supplied manually, which can affect the processing time and overall operational efficiency.
The parameters of the Canny algorithm are not normalized. This may lead to unstable results on the quality of edge detection.
Summary
The algorithm captures images, converts them to grayscale, applies a Sobel Kernel to determine the gradients, which are then used in a Canny-based framework for edge detection. These results show the maximum errors in laboratory and field experiments of 8.45% and 12.05%, respectively.
This implementation serves as a base prototype for real-time, nonstationary image-based crack characterization methods. Future versions will further enhance the perspective transformation and the inclusion of automatic ROI detection to deliver superior performance and user experience.

Demonstration
The functionality of the current application is introduced in the video below:
https://github.com/KaMa85/CM/blob/main/Untitled%20video.mp4
