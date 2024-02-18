Unity Project: Experimental Edge Detection and Measurement

Overview
This Unity project incorporates an experimental edge detection and measurement algorithm designed for defect quantification. It utilizes Canny parameters as a foundational basis, with modifications tailored to specific experimental needs. The following video and the three mp4 files in this repository (Media1, Media2 and Media3) shows the application of the project in a Microsoft HoloLens headset.
https://github.com/KaMa85/CM/assets/82784239/e7f72931-05df-4e24-a158-c927e17aaa96

This README outlines the current state of the project, its capabilities, and areas pending further development. 

Incomplete Features:
The project lacks perspective correction capabilities, which is required for analysis of the angled and distorted images.
An automatic Region of Interest (ROI) algorithm has not been incorporated yet and therefore the processing time is not optimized.
Pixel to millimeter (mm) conversion is not precisely calibrated. As such, new users should calibrate it to reflect exact real-world dimensions. 
Also, this app includes continous photo capturing and image processing and the process images and the result of the measurement is not saved in a separate document in this version.

Algorithmic Approach:
While the project employs Canny parameters for edge detection, it does not adhere strictly to the traditional Canny edge detection algorithm. Instead, it has been modified based on personal experimentation, particularly with a horizontal approach for measurement.
These alterations aim to adapt the algorithm for specific use cases within the project, primarily focusing on defect quantification.

Acknowledgments
This project is the result of extensive research and experimentation during the first and second years of my PHD at the University of New Mexico under suppervision of Dr. Fernando Moreu. We appreciate the contributions and insights from my other colleagues specially John-Wesley  (jack) Hanson.




